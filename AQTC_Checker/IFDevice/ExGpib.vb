

Imports InterfaceCorpDllWrap


Module ExGpib
 

	Dim GPIB_OPEN		As Boolean
	Dim GPln		As Integer
	Dim GPga( 7 )		As Integer
	Dim GPdlm		As Integer
	Dim GPss		As String


	Public GPrs		As String



	Public Function ExGpib_Open				_
	(							_
		ByVal lno		As Integer,		_
		ByVal myad		As Integer,		_
		ByVal dlm		As String,		_
		ByVal tout		As Integer		_
	)	As Integer

		' lno:ボード番号 0～15
		' myad:一次アドレス 0～30
		' dlm:送信デリミタ ""はデフォルト　 "CRLF+EOI"、"CR"等
		' tout:送受信タイムアウト　単位ms（ボードへの設定は100ms） < 100はデフォルト

		Dim nRet		As Integer
		Dim SetPrm		As String = ""



		GPln			= lno


		Try

			nRet			= IFCGPIB_ANY.GpibOpen( GPln )

		Catch ex As Exception

			WriteLog( "", "LG", "ExGpib_Open GPIB:" + GPln.ToString + " ボードなし？" )

			GPIB_OPEN = False


			Return -100

		End Try

		GPIB_OPEN		= True


		'
		'	パラメータ設定
		'

		' タイムアウト
		If tout >= 100 Then

			SetPrm			= "/TMO=" + ( tout \ 100 ).ToString

		End If

		' デリミタ
		If dlm <> "" Then

			If SetPrm <> "" Then

				SetPrm			+= " /SDELIM=" + dlm

			Else

				SetPrm			+= "/SDELIM=" + dlm

			End If

		End If


		' 一次アドレス
		SetPrm			+= " /MA=" + myad.ToString

		If SetPrm <> "" Then

			nRet			= IFCGPIB_ANY.GpibSetConfig( GPln, SetPrm )

			If nRet < 0 Then

				WriteLog						_
				(							_
					"",						_
					"LG",						_
					"ExGpib_Open パラメータ設定：" +		_
					SetPrm +					_
					" エラー：" +					_
					ConvErrMessageGpib( nRet )			_
				)

				IFCGPIB_ANY.GpibClose( GPln )

				GPIB_OPEN		= False


				Return nRet

			End If

		End If


		' ＩＦＣ送出
		nRet			= IFCGPIB_ANY.GpibSetIfc( GPln )

		If nRet < 0 Then

			WriteLog						_
			(							_
				"",						_
				"LG",						_
				"ExGpib_Open ＩＦＣ送出エラー：" +		_
				ConvErrMessageGpib( nRet )			_
			)

			IFCGPIB_ANY.GpibClose( GPln )

			GPIB_OPEN		= False


			Return nRet

		End If


		' ＲＥＮ設定
		nRet			= IFCGPIB_ANY.GpibSetRen( GPln )

		If nRet < 0 Then

			WriteLog( "", "LG", "ExGpib_Open ＲＥＮ設定エラー：" + ConvErrMessageGpib( nRet ) )

		End If

		WriteLog( "", "LG", "ExGpib_Open AD:" + GPln.ToString + " オープン完了" )



		Return nRet

	End Function



	Public Function ExGpib_End() As Integer

		' デバイスクローズ
		Dim nRet		As Integer



		If GPIB_OPEN = True Then

			nRet			= IFCGPIB_ANY.GpibClose( GPln )

			If nRet = IFCGPIB_ANY.GPIB_SUCCESS Then

				WriteLog( "", "LG", "ExGpib_End クローズ完了" )

			Else

				WriteLog( "", "LG", "ExGpib_End クローズエラー" )

			End If

		Else

			WriteLog( "", "LG", "ExGpib_End 未オープン" )

			nRet			= -103

		End If



		Return nRet

	End Function



	Public Function ExGpib_sd			_
	(						_
		ByVal ga		As Integer,	_
		ByVal dp		As String	_
	)	As Integer

		' ga;		/* 送信先のGPIBｱﾄﾞﾚｽ		*/
		' dp;		/* 送信ﾃﾞｰﾀ			*/

		Dim nRet		As Integer



		'送信先のＧＰＩＢアドレス設定
		GPga( 0 )		= ga

		GPga( 1 )		= -1

		GPss			= dp


		Dim len			As Integer = GPss.Length


		' 送信データなし
		If len = 0 Then

			WriteLog( "", "LG", "ExGpib_sd 送信データなし" )

			Return 0

		End If

		'
		'	20200508 DBGmode = 1 で動作するようにする
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			nRet			= 0

		Else

			' データの送信
			nRet			= IFCGPIB_ANY.GpibSend( GPln, GPga, len, GPss )

			If nRet Then

				WriteLog( "", "LG", "ExGpib_sd 送信エラー：" + ConvErrMessageGpib( nRet ) )

			End If

		End If


		Return nRet

	End Function



	Public Function ExGpib_rd			_
	(						_
		ByVal ga		As Integer	_
	)	As Integer

		' ga;		受信先のGPIBｱﾄﾞﾚｽ
		' len;		受信ﾃﾞｰﾀ長        NULLのときはｾｯﾄしない()

		Dim nRet As Integer



		' 送信先のＧＰＩＢアドレス設定
		GPga( 0 )		= ga

		GPga( 1 )		= -1



		' データ受信
		Dim RecvLen		As Integer
		Dim RecvBuffer		As String = ""


		RecvLen			= 64

		'
		'	20200508 DBGmode = 1 で動作するようにする
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			nRet			= 0

		Else

			nRet			= IFCGPIB_ANY.GpibReceive( GPln, GPga, RecvLen, RecvBuffer )

			If nRet Then

				WriteLog( "", "LG", "ExGpib_rd 受信エラー：" + ConvErrMessageGpib( nRet ) )

			Else

				GPrs			= RecvBuffer

			End If

		End If


		Return nRet

	End Function



	Public Function ConvErrMessageGpib		_
	(						_
		ByVal uErrCode		As Integer	_
	) As String


		' 戻り値のメッセージ変換
		Dim strMsg		As String


		Select Case	uErrCode

		Case	2
			strMsg		= "正常終了:ＥＯＩを検出して終了しました"

		Case	1

			strMsg		= "正常終了:指定された受信データ数に達して終了しました"

		Case	0

			strMsg		= "正常終了"

		Case	-1

			strMsg		= "ボードアクセス番号が違います"

		Case	-4

			strMsg		= "スレーブモードでは使用できません"

		Case	-5

			strMsg		= "マスタモードでは使用できません"

		Case	-7

			strMsg		= "バスコマンドの送出に失敗しました"

		Case	-12

			strMsg		= "データ受信に失敗しました"

		Case	-13

			strMsg		= "データ送信に失敗しました"

		Case	-14

			strMsg		= "タイムアウトが発生しました"

		Case	-16

			strMsg		= "ＩＦＣ受信による強制終了"

		Case	-20

			strMsg		= "現在、バスが占有状態となっています"

		Case	-40

			strMsg		= "コールバックイベントの登録に失敗しました"

		Case	-41

			strMsg		= "コールバックイベントの登録解除に失敗しました"

		Case	-42

			strMsg		= "イベントオブジェクトが有効（シグナル状態）です"

		Case	-43

			strMsg		= "指定した時間内にイベントオブジェクトが有効にならなかったためタイムアウトしました"

		Case	-44

			strMsg		= "イベントオブジェクト待ちでエラーが発生しました"

		Case	-45

			strMsg		= "コールバックイベントが登録されていません"

		Case	-995

			strMsg		= "ボードの使用終了ができませんでした"

		Case	-996

			strMsg		= "ドライバ側のページ確保ができませんでした"

		Case	-997

			strMsg		= "タイマ設定に失敗しました"

		Case	-998

			strMsg		= "割り込みが使用できません"

		Case	-999

			strMsg		= "ボードが存在しない、またはボードのI/Oポートに異常があります"

		Case Else

			strMsg		= "予想外のエラーが発生しました errCode=&H" + uErrCode.ToString( "x8" )

		End Select



		Return strMsg


	End Function



End Module
