

Imports System.IO

Module TstDataRD


	Dim t1ct		As Integer
	Dim posv		As String



	Public Function CheckTestFile			_
	(						_
		ByVal agFName		As String	_
	) As Integer
		Dim FileName		As String

	'	20201102 s.harada
	'	AQTC対応でフォルダ名を変更
		FileName		=
			Directory.GetCurrentDirectory() +	_
			"\testTcl\" +				_
			agFName +				_
			".dat"					_


		Return File.Exists( FileName )

	End Function




	'*****
	'	検査設定ファイルを読み込む
	'*****
	Public Function GetTestData			_
	(						_
		ByRef agFName		As String,	_
		ByVal tstMod		As Integer	_
	) As Integer

		' agFName:テストパラメータファイル名　  拡張子.dat
	'	20201102 s.harada
	'	AQTC対応でtstModに残留吸着力を追加
		' tstMod:測定モード　0:自動　1:絶縁耐圧　2:吸着　3:Heリーク　4:残留吸着力


		Dim ret			As Integer = -1
		Dim TextFile		As IO.StreamReader
		Dim Buff		As String
		Dim FileName		As String


	'	20201102 s.harada
	'	AQTC対応でフォルダ名を変更
		FileName		=			_
			Directory.GetCurrentDirectory() +	_
			"\testTcl\" +				_
			agFName +				_
			".dat"

		' 測定データクリア
		clearMesrec()

		Try

			' ファイルをオープン
			TextFile		= New IO.StreamReader	_
			(						_
				FileName,				_
				System.Text.Encoding.Default		_
			)


		Catch ex As Exception

			Return ret

		End Try


		Try

			Do

				Buff			= TextFile.ReadLine()

				If Buff Is Nothing Then

					Exit Do

				End If


				Select Case Left( Buff, 2 )

				' システムデータ
				Case	"0,"

					ret			= GetSystemData( Buff )

				' 大気圧測定
				Case	"1,"

					ret			= GetMeasureData( 0, Buff, tstMod )

				' 真空　低温測定
				Case	"2,"

					ret			= GetMeasureData( 1, Buff, tstMod )

				' 真空　高温測定
				Case	"3,"

					ret			= GetMeasureData( 2, Buff, tstMod )

				' 真空　高温２測定
				Case	"4,"

					ret			= GetMeasureData( 3, Buff, tstMod )

				Case Else

				End Select

			Loop


			MESrec.type	= agFName

		Catch ex As Exception

			Throw ex

		Finally

			TextFile.Close()

		End Try



		Return ret

	End Function



	Private Sub clearMesrec()


		' 測定データの初期化
		MESrec.type		= ""

		MESrec.dh.tno		= ""

		MESrec.dh.mno		= ""

		MESrec.dh.sno		= ""

		MESrec.dh.inc		= ""

		MESrec.dh.vo		= ""

		MESrec.dh.sdt		= ""

		MESrec.dh.pcdt		= ""

		MESrec.dh.okng		= -1


		ReDim MESrec.dt( 3 )

	End Sub



	Private Function GetSystemData			_
	(						_
		ByVal rBuf		As String	_
	)	As Integer

		' システムデータを測定データにセット
		' rBuf:リードデータ

		Dim ret			As Integer = -1
		Dim aryBuf()		As String



		Try

			aryBuf		= rBuf.Split( "," )

			If aryBuf.Count >= 3 Then

				Select Case	aryBuf( 1 )

				' ESC-2000動作ﾓｰﾄﾞ (0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ)
				Case	"1"

					ESCmd			= CInt( aryBuf( 2 ) )

				' 20201102 s.harada	AQTC対応で削除
				'' ウエハ裏面圧力リミット値
				'Case	"2"

				'	BPRS			= CDbl( aryBuf( 2 ) ) * 1000.0

				' 20201102 s.harada	AQTC対応で削除
				'' Heﾘｰｸ測定時のｳｴｱ裏面圧力(PIDのｾｯﾄﾎﾟｲﾝﾄ)
				'Case	"3"

					'HLPRS			= CDbl( aryBuf( 2 ) ) * 1000.0

				' SMCサ－モチラ－使用CH　0:CH1 1:CH2 2:両方
				Case	"4"

					SCRUSE			= CInt( aryBuf( 2 ) )

				' 真空圧試験条件(温度１)
				Case	"5"

					TPRS1			= CDbl( aryBuf( 2 ) )

				' 真空圧試験条件(温度２)
				Case	"6"

					TPRS2			= CDbl( aryBuf( 2 ) )

				' ウエハ吸着停止するときのウエハ裏面圧
				Case	"7"

					BakPres			= CDbl( aryBuf( 2 ) )

				'
				'	20140127 y.goto
				'	電極ヘッド温度安定待ち時間 (分)
				'
				Case	"8"

					PrmTmpStbW		= CDbl( aryBuf( 2 ) )


				'	残留吸着　Ｈｅ流量
				Case	"9"

					PrmHeFlow		= CDbl( aryBuf( 2 ) )


				' 20201102 s.harada	AQTC対応で追加
				'	残留吸着電圧印加時間（秒）
				Case	"10"

					PrmVoltImp		= CDbl( aryBuf( 2 ) )

				' 20201102 s.harada	AQTC対応で追加
				'	残留吸着電圧印加停止後Ｈｅ流すまでの待ち時間（秒）
				Case	"11"

					PrmHeWait		= CDbl( aryBuf( 2 ) )

				' 20201102 s.harada	AQTC対応で追加
				'	吸着力測定時間上限（秒）
				Case	"12"

					PrmMaxTim		= CDbl( aryBuf( 2 ) )

				' 20201102 s.harada	AQTC対応で追加
				'	残留吸着安定判断傾き
				Case	"13"

					PrmStabVct		= CDbl( aryBuf( 2 ) )

				' 20201102 s.harada	AQTC対応で追加
				'	残留吸着安定判断時間（秒）
				Case	"14"

					PrmStabTim		= CDbl( aryBuf( 2 ) )

				' 20201124 y.goto	AQTC対応 20201120木村さんメール対応 1500Paをパラメータ化
				'	残留吸着電圧印可時裏面圧力 (Pa)
				Case	"15"

					PrmBakPrs		= CDbl( aryBuf( 2 ) )

				Case Else

					WriteLog( "", "LG", "システムデータ　該当なし dat=" + rBuf )

				End Select


				ret = 0

			End If


		Catch ex As Exception

			WriteLog( "", "LG", "システムデータエラー dat=" + rBuf + " ex=" + ex.ToString )

		End Try


		Return ret

	End Function




	'*****
	'	試験パラメータファイル読み込み
	'*****
	Private Function GetMeasureData			_
	(							_
		ByVal no		As Integer,		_
		ByVal rBuf		As String,		_
		ByVal tMod		As Integer		_
	)	As Integer

		' 測定方法データをセット
		' no:雰囲気　0:大気　1:真空低温　2:真空高温　3:真空高温２
		' tMod:測定モード　0:自動　1:絶縁耐圧　2:吸着　3:Heリーク　4:残留吸着力

		Dim ret			As Integer = -1
		Dim aryBuf()		As String



		Try

			aryBuf			= rBuf.Split( "," )

			If aryBuf.Count >= 3 Then

				Select Case	aryBuf( 1 )

				'
				'	20140123 変更
				'	サ－モチラ－設定情報
				'	CH1使用ﾌﾗｸﾞ, CH1設定温度, CH2使用ﾌﾗｸﾞ, CH2設定温度, 配管接続変更有無
				'
				Case	"0"

					ReDim MESrec.dt( no ).tmp( 1 )

					ReDim MESrec.dt( no ).schuse( 1 )


					' CH1使用フラグ
					MESrec.dt( no ).schuse( 0 )	= CInt( aryBuf( 2 ) )

					' CH1設定温度
					MESrec.dt( no ).tmp( 0 )	= CInt( aryBuf( 3 ) )

					' CH2使用フラグ
					MESrec.dt( no ).schuse( 1 )	= CInt( aryBuf( 4 ) )

					' CH2設定温度
					MESrec.dt( no ).tmp( 1 )	= CInt( aryBuf( 5 ) )

					' 配管接続変更有無フラグ
					MESrec.dt( no ).schchg		= CInt( aryBuf( 6 ) )


					t1ct				= -1


				' 電圧印可箇所
				Case	"1"

					posv			= aryBuf( 2 )

					t1ct			= -1

				' 絶縁耐圧 印可電圧（Ｖ）
				Case	"2"

					If tMod <> TST_AUTO And tMod <> TST_ZETS Then

						Exit Select

					End If


					If t1ct < 0 Then

						t1ct			= MESrec.dt( no ).t1siz

						If t1ct = 0 Then

							ReDim MESrec.dt( no ).t1( t1ct )
 
						Else

							ReDim Preserve MESrec.dt( no ).t1( t1ct )

						End If

						MESrec.dt( no ).t1siz		= t1ct + 1

						MESrec.dt( no ).t1( t1ct ).posv	= posv

					End If


					Dim ct			As Integer = MESrec.dt( no ).t1( t1ct ).dsiz


					If ct = 0 Then

						ReDim MESrec.dt( no ).t1( t1ct ).d( ct )

					Else

						ReDim Preserve MESrec.dt( no ).t1( t1ct ).d( ct )

					End If


					MESrec.dt( no ).t1( t1ct ).d( ct ).volt	= CDbl( aryBuf( 2 ) )

					MESrec.dt( no ).t1( t1ct ).dsiz		= ct + 1

				' 吸着力 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（kPa以上）
				Case	"3"

					If tMod <> TST_AUTO And tMod <> TST_KYUC Then

						Exit Select

					End If


					Dim ct			As Integer = MESrec.dt( no ).t2.dsiz


					If ct = 0 Then

						ReDim MESrec.dt( no ).t2.d( ct )

						MESrec.dt( no ).t2.posv		= posv

					Else

						ReDim Preserve MESrec.dt( no ).t2.d( ct )

					End If


					MESrec.dt( no ).t2.d( ct ).volt1		= CDbl( aryBuf( 2 ) )

					MESrec.dt( no ).t2.d( ct ).volt2		= CDbl( aryBuf( 3 ) )


					If IsNumeric( aryBuf( 4 ) ) Then

						' 20201102 s.harada	判定値を時間に変更
						'MESrec.dt( no ).t2.d( ct ).bs		= CDbl( aryBuf( 4 ) ) * 1000.0
						MESrec.dt( no ).t2.d( ct ).bs		= CDbl( aryBuf( 4 ) )

					Else

						MESrec.dt( no ).t2.d( ct ).bs		= -1.0

					End If


					ReDim MESrec.dt( no ).t2.d( ct ).pa( 2 )

					'	ヘリウム流量
					If IsNumeric( aryBuf( 5 ) ) Then

						MESrec.dt( no ).t2.d( ct ).he		= CDbl( aryBuf( 5 ) )

					Else

						MESrec.dt( no ).t2.d( ct ).he		= 5.0

					End If


					' 20201102 s.harada	裏面圧到達時間を確保
					ReDim MESrec.dt( no ).t2.d( ct ).tmr( 4 )

					MESrec.dt( no ).t2.dsiz		= ct + 1

				' Ｈｅリーク量 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（ml/min以下）
				Case	"4"

					If tMod <> TST_AUTO And tMod <> TST_HGAS Then

						Exit Select

					End If


					Dim ct			As Integer = MESrec.dt( no ).t3.dsiz


					If ct = 0 Then

						ReDim MESrec.dt( no ).t3.d( ct )

						MESrec.dt( no ).t3.posv		= posv

					Else

						ReDim Preserve MESrec.dt( no ).t3.d( ct )

					End If

					MESrec.dt( no ).t3.d( ct ).volt1	= CDbl( aryBuf( 2 ) )

					MESrec.dt( no ).t3.d( ct ).volt2	= CDbl( aryBuf( 3 ) )

					If IsNumeric( aryBuf( 4 ) ) Then

						MESrec.dt( no ).t3.d( ct ).bs	= CDbl( aryBuf( 4 ) )

					Else

						MESrec.dt( no ).t3.d( ct ).bs	= -1.0

					End If

					' 20201102 s.harada	AQTC対応で追加
					'	判定基準２
					If IsNumeric( aryBuf( 5 ) ) Then

						MESrec.dt( no ).t3.d( ct ).bs2		= CDbl( aryBuf( 5 ) ) 

					Else

						MESrec.dt( no ).t3.d( ct ).bs2		= -1.5

					End If


					' 20201102 s.harada	到達時MFC電圧、He流量を確保
					ReDim MESrec.dt( no ).t3.d( ct ).mfcvolt( 4 )
					ReDim MESrec.dt( no ).t3.d( ct ).cm( 4 )

					MESrec.dt(no).t3.dsiz = ct + 1


				' 20201102 s.harada	AQTC対応で追加
				' 残留吸着力 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（kPa以上）
				Case	"5"

					If tMod <> TST_AUTO And tMod <> TST_ZKYU Then

						Exit Select

					End If


					Dim ct			As Integer = MESrec.dt( no ).t4.dsiz


					If ct = 0 Then

						ReDim MESrec.dt( no ).t4.d( ct )

						MESrec.dt( no ).t4.posv		= posv

					Else

						ReDim Preserve MESrec.dt( no ).t4.d( ct )

					End If


					MESrec.dt( no ).t4.d( ct ).volt1	= CDbl( aryBuf( 2 ) )

					MESrec.dt( no ).t4.d( ct ).volt2	= CDbl( aryBuf( 3 ) )


					If IsNumeric( aryBuf( 4 ) ) Then

						MESrec.dt( no ).t4.d( ct ).bs	= CDbl( aryBuf( 4 ) )

					Else

						MESrec.dt(no).t4.d(ct).bs = -1.0

					End If


					MESrec.dt( no ).t4.dsiz		= ct + 1

				Case Else

					WriteLog("", "LG", "テストデータ　該当なし dat=" + rBuf)

				End Select


				ret = 0

			End If

		Catch ex As Exception

			WriteLog( "", "LG", "テストデータエラー dat=" + rBuf + " ex=" + ex.ToString )

		End Try


		Return ret


	End Function


End Module
