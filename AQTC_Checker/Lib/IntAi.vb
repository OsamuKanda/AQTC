

Imports System.IO



'*****
'	ｲﾝﾀｰﾊﾞﾙﾀｲﾏｰ割込処理内で、AD変換結果をこのﾘﾝｸﾞﾊﾞｯﾌｧにｾｯﾄしてゆく。
'	１つのﾃﾞｰﾀ領域には ADnAVG 回の変換ﾃﾞｰﾀが加算され格納される。
'	ADnAVG 回収録したら次ぎのｴﾘｱを0ｸﾘｱして同様に収録して行く。
'	格納エリアの最後まで収録したら、先頭に戻って格納する。
'*****
Module IntAi


	'
	'	ウエハ裏面圧測定データ波形サンプリング
	'	1800件では0.1秒毎に記録すると、最大で３分記録可能
	'

	' サンプリングデータ件数
	Public Const DefMaxNSmp			As Integer	= 1800

	' サンプリングバッファポインタ
	Public WavSmpPtr			As Integer	= 0

	' サンプリング制御フラグ
	Public WavSmpFlg			As Integer	= 0



	'
	'	波形サンプリングバッファ
	'

	' ウエハ裏面圧波形サンプリングバッファ
	Public WavBufBkp( DefMaxNSmp )		As Double

	' Ｈｅ流量波形サンプリングバッファ
	Public WavBufHeR( DefMaxNSmp )		As Double

	' Ｈｅ流量移動平均波形サンプリングバッファ
	Public WavBufHeA( DefMaxNSmp )		As Double



	'
	'	AD変換RAWﾃﾞｰﾀ･ﾘﾝｸﾞﾊﾞｯﾌｧ
	'	CHの0は未使用　1～ADnCHまでを使用
	'
	Public ADbfRNG( ADnRNG - 1, ADnCH )	As Integer


	'
	'	AD変換中RAWﾃﾞｰﾀ･ﾘﾝｸﾞﾊﾞｯﾌｧﾎﾟｲﾝﾀ
	'	現在収録中のﾘﾝｸﾞﾊﾞｯﾌｧ上の位置
	'
	Public ADptrR				As Integer	= 0


	'
	'	平均化回数カウンタ
	'
	Dim ADavgCNT			As Integer = 0
	Dim ADWK			As UShort


	'
	'	ﾘﾝｸﾞﾊﾞｯﾌｧﾃﾞｰﾀﾌﾙﾌﾗｸﾞ
	'
	Public ADrbFUL			As Integer = 0



	'*****
	'	波形サンプリング開始
	'*****
	Public Sub WaveSmpGo()


		'
		'	ポインタをクリアすることで、サンプリングがスタートする
		'

		' サンプリングバッファポインタ・クリア
		WavSmpPtr		= 0

		' サンプリング制御フラグ・セット
		WavSmpFlg		= 1


	End Sub



	'*****
	'	波形サンプリング停止
	'*****
	Public Sub WaveSmpStop()


		' サンプリング制御フラグ・クリア
		WavSmpFlg		= 0


	End Sub



	'*****
	'	波形サンプリングデータを保存
	'*****
	Public Function SaveWaveData			_
	(						_
		ByVal tstno		As Integer,	_
		ByVal mtp		As String,	_
		ByVal v1		As Double,	_
		ByVal v2		As Double	_
	)	As Integer

		Dim rtn			As Integer = -1
		Dim fp			As IO.StreamWriter
		Dim Buff		As String = ""
		Dim fpath		As String
		Dim i			As Integer



		' 戻り値初期値セット
		rtn			= -1

		Do

			fpath			=			_
				TstRstFolpth + "\" +			_
				TstRstFname +				_
				"_" +					_
				tstno.ToString() +			_
				mtp +					_
				"_" +					_
				v1.ToString() +				_
				"_" +					_
				v2.ToString() +
				".csv"


			Try

				'
				'	データ格納フォルダの作成
				'
				If Dir( TstRstFolpth, vbDirectory ) = "" Then

					MkDir( TstRstFolpth )

				End If

				' ファイルをオープン
				fp			=				_
					New IO.StreamWriter				_
					(						_
						New IO.FileStream			_
						(					_
							fpath, IO.FileMode.Create	_
						),					_
						System.Text.Encoding.Default		_
					)

			Catch ex As Exception

				WriteLog( "", "LG", "SaveTestData File=" + fpath + " " + ex.Message )

				Exit Do

			End Try


			'
			'	波形ファイルの書き出し
			'
			Try

				For i = 0 To WavSmpPtr - 1


					Buff			=			_
						WavBufBkp( i ).ToString( "0.000" ) +	_
						"," +					_
						WavBufHeR( i ).ToString( "0.000" ) +	_
						"," +					_
						WavBufHeA( i ).ToString( "0.000" )

					fp.WriteLine( Buff )


				Next i


				rtn		= 0

			Catch ex As Exception

				WriteLog( "", "LG", "SaveWaveData Buff=" + Buff + " " + ex.Message )

			Finally

				fp.Close()

			End Try


		Loop While False



		Return rtn


	End Function



	'*****
	'	ｲﾝﾀｰﾊﾞﾙ割込処理関数
	'
	'	<return>
	'	0	サンプリング中
	'	!0	サンプリングデータ確定
	'*****
	Public Function intad() As Integer

		Dim CH			As Integer
		Dim raw			As UShort
		Dim rtn			As Integer
		Dim ulng		As ULong


		' 戻り値初期セット
		rtn			= 0


		'
		'	チャンネル数分のＡ/Ｄ入力
		'
		ExAd_Input()


		'
		'	変換結果を取得
		'
		For CH = 1 To ADnCH

			'
			'	20200324 y.goto for unittest
			'	デバックモード動作時はAI入力値はデバックウインドウの設定値を使用する
			'
			If DBGmode = 1 Then

				'
				'	デバックモード時の動作
				'
				Select Case	CH

				Case	ESCaiMON1

					' 20200420 ESCﾓﾆﾀ電圧は出力指示値と同じにする
					ulng			= DaBuf( ESCaoVOLT1 ) And &Hffff
					ADWK			= CType( ulng, UShort )

				Case	ESCaiMON2

					' 20200420 ESCﾓﾆﾀ電圧は出力指示値と同じにする
					ulng			= DaBuf( ESCaoVOLT2 ) And &Hffff
					ADWK			= CType( ulng, UShort )

				Case	SCRaiTMP1

					' 20200420 サーモチラー･CH1ﾓﾆﾀ出力は出力指示と同じにする
					ulng			= DaBuf( SCRaoREMOTE1 ) And &Hffff
					ADWK			= CType( ulng, UShort )

				Case	SCRaiTMP2

					' 20200420 サーモチラー･CH2ﾓﾆﾀ出力は出力指示と同じにする
					ulng			= DaBuf( SCRaoREMOTE2 ) And &Hffff
					ADWK			= CType( ulng, UShort )

				Case	Else

					'
					'	AI入力はデバックウインドウからの模擬入力
					'
					ADWK			= FrmDBG.DBGAI( CH )

				End Select

			Else

				'
				'	通常動作時は AI の読み込み値を使用する
				'
				ADWK			= AdBuf( CH ) And &HFFFF

			End If

			' 収録エリアに加算
			ADbfRNG( ADptrR, CH )	+= ADWK

		Next

		' 平均化回数カウンタ・カウントアップ
		ADavgCNT		+= 1

		If ADnAVG <= ADavgCNT Then

			'
			'	サンプリングデータ確定
			'
			rtn			= 1


			'
			'	波形サンプリング処理
			'
			If					_
				WavSmpFlg And			_
				DefMaxNSmp > WavSmpPtr		_
			Then

				'
				'	ウエハ裏面圧測定データ波形サンプリング
				'
				raw			= aigetspf( GMaiPRS, ADptrR, 1 )

				WavBufBkp( WavSmpPtr )	= cvtr2GM_Pa( raw )


				'
				'	Ｈｅ流量測定データ波形サンプリング
				'
				raw			= aigetspf( MFCaiFLW, ADptrR, 1 )

				WavBufHeR( WavSmpPtr )	= cvtr2MFCop( raw )


				'
				'	Ｈｅ流量移動平均波形サンプリング
				'
				raw			= aigetspf( MFCaiFLW, ADptrR, DefFlowNAvg )

				WavBufHeA( WavSmpPtr )	= cvtr2MFCop( raw )


				' サンプリングポインタを次へ移動
				WavSmpPtr		+= 1


			End If



			' 平均化回数分の収録完了
			ADavgCNT		= 0

			' 次収録エリアへ移動
			ADptrR			+= 1


			If ( ADnRNG <= ADptrR ) Then

				' 収録エリアの最後。
				' 次収録エリアは先頭から。
				ADptrR			= 0

				ADrbFUL			= 1

			End If

			' 次収録エリアの０クリア	CH0は未使用だがクリア	
			For CH = 0 To ADnCH

				ADbfRNG( ADptrR, CH )		= 0

			Next

		End If



		return( rtn )


	End Function



	'*****
	'	収録エリアを初期化
	'*****
	Public Sub intai_ini()

		Dim CH			As Integer



		' 収録エリアポインタクリア
		ADptrR			= ADnRNG - 1

		' データフルフラグクリア
		ADrbFUL			= 0

		' 平均化回数分の収録完了クリア
		ADavgCNT		= 0

		' バッファをＡ／Ｄ入力０でクリア
		Dim dt			As Integer = &H8000 * ADnAVG
		Dim i			As Integer


		For i = 0 To ADnRNG - 1

			For CH = 1 To ADnCH

				ADbfRNG( i, CH )		= dt

			Next

		Next


		' 圧力バッファは５Ｖでクリア
		dt			= &HFFFF * ADnAVG

		For i = 0 To GPaiPRS

			ADbfRNG( i, GMaiPRS )		= dt

			ADbfRNG( i, GPaiPRS )		= dt

		Next


		' 次収録エリアの０クリア　CH0は未使用だがクリア
		For CH = 0 To ADnCH

			ADbfRNG( ADptrR, CH )		= 0

		Next

	End Sub



	'*****
	'	指定チャネルの最新データを取得する（移動平均）
	'	<return>
	'	uShort	RAWﾃﾞｰﾀの移動平均値
	'*****
	Public Function aiget				_
	(						_
		ByVal ch		As Integer,	_
		ByVal navg		As Integer	_
	)	As UShort

		'ch;		指定ﾁｬﾈﾙ (0～ADnCH)
		'navg;		移動平均回数
		'		1～ADnRNG
 
		' 現在集計中位置の１つ前の位置から取得(ﾘﾝｸﾞﾊﾞｯﾌｧ)
		 Dim ptr		As Integer = ADptrR - 1

		If ( ptr < 0 ) Then

			ptr			= ADnRNG - 1

		End If

		'ptr位置から移動平均を求める
		Return ( aigetspf( ch, ptr, navg ) )

	End Function



	'*****
	'	指定チャネルの指定データ位置より移動平均を計算する
	'	<return>
	'	uShort	RAWﾃﾞｰﾀの移動平均値
	'*****
	Private Function aigetspf			_
	(						_
		ByVal ch		As Integer,	_
		ByVal ptr		As Integer,	_
		ByVal navg		As Integer	_
	)	As UShort

		'ch;		指定ﾁｬﾈﾙ (0～ADnCH)
		'ptr;		指定位置 (ﾘﾝｸﾞﾊﾞｯﾌｧ上)
		'navg;		移動平均回数
		'		1～ADnRNG

		Dim sum			As Double
		Dim avg			As Double
		Dim raw			As UShort
		Dim i			As Integer
		Dim max			As Integer = navg - 1



		If max >= ADnRNG Then

			max			= ADnRNG - 1

		End If

		' 移動平均回数分の変換RAWﾃﾞｰﾀの合計値を計算	
		sum			= 0.0

		For i = 0 To max

			' RAWﾃﾞｰﾀの合計値を計算
			sum			+= CDbl( ADbfRNG( ptr, ch ) )

			' １つ前の位置へ移動(ﾘﾝｸﾞﾊﾞｯﾌｧ)
			ptr			-= 1

			If ( ptr < 0 ) Then

				ptr			= ADnRNG - 1

			End If

		Next

		' 平均値を計算 (小数点以下四捨五入)	
		avg			= sum / CDbl( navg * ADnAVG ) + 0.5

		If 65535.0 < avg Then

			avg			= 65535.0

		ElseIf 0.0 > avg Then

			avg			= 0.0

		End If

		raw			= CUShort( avg )



		Return ( raw )



	End Function

End Module
