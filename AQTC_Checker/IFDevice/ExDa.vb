

Imports		InterfaceCorpDllWrap

Imports		System.Configuration



Module ExDa

	' DEV0:PCI-3310 DEV1:LPC-340416
	' DaBuf,SmplChInfの( 0 )は未使用

	Public Const PCI3310	As Integer = 0

	Public Const LPC340416	As Integer = 1

	Public DaBuf( 8 )	As Short



	Dim hEXDA( 1 )		As IntPtr

	Dim DAInfo( 1 )		As IFCDA_ANY.DABOARDSPEC

	Dim DA_OPEN( 1 )	As Boolean

	Dim SmplChInf( 8 )	As IFCDA_ANY.DASMPLCHREQ



	Public Function ExDa_Open			_
	(						_
		ByVal DevNo		As Integer	_
	)	As Integer

		' Ｄ／Ａデバイスオープン

		Dim DevName		As String
		Dim nRet		As Integer



		If DevNo < 0 Or DevNo > 1 Then

			Return -4

		End If


		Dim itemName		As String = "DaDeviceName" + ( DevNo + 1 ).ToString

		DevName			= ConfigurationManager.AppSettings.Item( itemName )

		If DevName = "" Then

			DevName			= "FBIDA" + ( DevNo + 1 ).ToString

		End If
 

		Try

			hEXDA( DevNo )		= IFCDA_ANY.DaOpen( DevName )

		Catch ex As Exception

			WriteLog( "", "LG", "ExDa_Open DA:" + DevName + " ボードなし？" )

			DA_OPEN( DevNo )	= False


			Return -1

		End Try


		If hEXDA( DevNo ) = New IntPtr( -1 ) Then

			WriteLog( "", "LG", "ExDa_Open DA:" + DevName + " オープンエラー" )

			DA_OPEN( DevNo )	= False


			Return -2

		End If


		nRet			= IFCDA_ANY.DaGetDeviceInfo( hEXDA( DevNo ), DAInfo( DevNo ) )

		If nRet <> IFCDA_ANY.DA_ERROR_SUCCESS Then

			' MessageBox.Show(ConvErrMessageAd(nRet), "ＤＡボードエラー")
			WriteLog( "", "LG", "ExDa_Open デバイス仕様エラー：" + ConvErrMessageDa( nRet ) )

			IFCDA_ANY.DaClose( hEXDA( DevNo ) )

			DA_OPEN( DevNo )		= False


			Return -3

		End If


		WriteLog( "", "LG", "ExDa_Open DA:" + DevName + " オープン完了" )

		DA_OPEN( DevNo )	= True


		'出力レンジを５Ｖに仮設定
		Dim i			As Integer

		For i = 1 To 4

			SmplChInf( i ).ulChNo	= i

			SmplChInf( i ).ulRange	= IFCDA_ANY.DA_0_5V

		Next


		For i = 5 To 8

			SmplChInf( i ).ulChNo	= i - 4

			SmplChInf( i ).ulRange	= IFCDA_ANY.DA_0_5V

		Next


		Return nRet

	End Function



	Public Function ExDa_Close			_
	(						_
		ByVal DevNo		As Integer	_
	)	As Integer

		' Ｄ／Ａデバイスクローズ
		Dim nRet		As Integer



		If DA_OPEN( DevNo ) = True Then

			nRet			= IFCDA_ANY.DaClose( hEXDA( DevNo ) )

			If nRet = IFCDA_ANY.DA_ERROR_SUCCESS Then

				WriteLog( "", "LG", "ExDa_Close DA:" + DevNo.ToString + " クローズ完了" )

			Else

				WriteLog( "", "LG", "ExDa_Close DA:" + DevNo.ToString + " クローズエラー" )

			End If

		Else

			WriteLog( "", "LG", "ExDa_Close DA:" + DevNo.ToString + " 未オープン" )

			nRet			= -3

		End If



		Return nRet

	End Function


	Public Sub ExDa_SetRange			_
	(						_
		ByVal ch		As Integer,	_
		ByVal range		As Integer	_
	)


		' チャンネル番号毎のレンジの個別設定
		' SmplChInf(ch).ulChNo = ch + 1
		SmplChInf( ch ).ulRange	= range


	End Sub



	Public Function ExDa_Output			_
	(						_
		ByVal ch		As Integer,	_
		ByVal Dat		As Short	_
	)	As Integer

		' 個別出力
		Dim nRet		As Integer



		DaBuf( ch )		= Dat


		'
		'	20140120 拡張基板が無くても動作するようにする
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			nRet			= 0

		Else

			If ch <= 4 Then

				nRet			= IFCDA_ANY.DaOutputDA( hEXDA( 0 ), 1, SmplChInf( ch ), Dat )

			Else

				nRet			= IFCDA_ANY.DaOutputDA( hEXDA( 1 ), 1, SmplChInf( ch ), Dat )

			End If

		End If



		Return nRet

	End Function



	Public Function ConvErrMessageDa		_
	(						_
		ByVal uErrCode		As Integer	_
	)	As String


		' 戻り値のメッセージ変換
		Dim strMsg		As String



		Select Case	uErrCode

		Case	IFCDA_ANY.DA_ERROR_SUCCESS

			strMsg		= "正常終了"

		Case	IFCDA_ANY.DA_ERROR_NOT_DEVICE

			strMsg		= "ドライバが呼び出せません"

		Case	IFCDA_ANY.DA_ERROR_NOT_OPEN

			strMsg		= "ドライバがオープンできません"

		Case	IFCDA_ANY.DA_ERROR_INVALID_HANDLE

			strMsg		= "デバイスハンドルが不正です"

		Case	IFCDA_ANY.DA_ERROR_ALREADY_OPEN

			strMsg		= "既にオープンしているデバイスです"

		Case	IFCDA_ANY.DA_ERROR_NOT_SUPPORTED

			strMsg		= "製品がサポートしていない関数です"

		Case	IFCDA_ANY.DA_ERROR_NOW_SAMPLING

			strMsg		= "アナログ出力更新中です"

		Case	IFCDA_ANY.DA_ERROR_STOP_SAMPLING

			strMsg		= "アナログ出力更新中は停止中です"

		Case	IFCDA_ANY.DA_ERROR_START_SAMPLING

			strMsg		= "アナログ出力更新を開始できませんでした"

		Case	IFCDA_ANY.DA_ERROR_SAMPLING_TIMEOUT

			strMsg		= "アナログ出力更新においてタイムアウトが発生しました"

		Case	IFCDA_ANY.DA_ERROR_INVALID_PARAMETER

			strMsg		= "パラメーターが不正です"

		Case	IFCDA_ANY.DA_ERROR_ILLEGAL_PARAMETER

			strMsg		= "アナログ出力設定が正しくありません"

		Case	IFCDA_ANY.DA_ERROR_NULL_POINTER

			strMsg		= "NULLポインタを指定しました"

		Case	IFCDA_ANY.DA_ERROR_SET_DATA

			strMsg		= "アナログ出力データのセットができませんでした"

		Case	IFCDA_ANY.DA_ERROR_FILE_OPEN

			strMsg		= "ファイルのオープンに失敗しました"

		Case	IFCDA_ANY.DA_ERROR_FILE_CLOSE

			strMsg		= "ファイルのクローズに失敗しました"

		Case	IFCDA_ANY.DA_ERROR_FILE_READ

			strMsg		= "ファイルのリードに失敗しました"

		Case	IFCDA_ANY.DA_ERROR_FILE_WRITE

			strMsg		= "ファイルのライトに失敗しました"

		Case	IFCDA_ANY.DA_ERROR_INVALID_DATA_FORMAT

			strMsg		= "データ形式が無効です"

		Case	IFCDA_ANY.DA_ERROR_INVALID_AVERAGE_OR_SMOOTHING

			strMsg		= "平均またはスムージングの指定が正しくありません"

		Case	IFCDA_ANY.DA_ERROR_INVALID_SOURCE_DATA

			strMsg		= "データ変換元として指定されたデータが正しくありません"

		Case	IFCDA_ANY.DA_ERROR_NOT_ALLOCATE_MEMORY

			strMsg		= "メモリが確保できません"

		Case	IFCDA_ANY.DA_ERROR_NOT_LOAD_DLL

			strMsg		= "DLLがロードできませんでした"

		Case	IFCDA_ANY.DA_ERROR_CALL_DLL

			strMsg		= "DLLの呼び出しに失敗しました"

		Case	-1

			strMsg		= "ボードがありません"

		Case	-2

			strMsg		= "デバイスオープンできません"

		Case	-3

			strMsg		= "デバイスが見オープンです"

		Case	-4

			strMsg		= "デバイス指定が範囲外です。"

		Case	Else

			strMsg		= "未設定の戻り値です errCode=&H" + uErrCode.ToString("x8")

		End Select


		Return strMsg

	End Function



End Module
