

Imports		InterfaceCorpDllWrap

Imports		System.Configuration



Module ExAd


	' PCI-3135
	' AdBuf,SmplChInfの(0)は未使用
	Public AdBuf( ADnCH )	As Short

	Dim hEXAD		As IntPtr

	Dim ADInfo		As IFCAD_ANY.ADBOARDSPEC

	Dim AD_OPEN		As Boolean

	Dim SmplChInf( ADnCH )	As IFCAD_ANY.ADSMPLCHREQ



	Public Function ExAd_Open() As Integer

		' Ａ／Ｄデバイスオープン
		Dim DevName		As String = ConfigurationManager.AppSettings.Item( "AdDeviceName" )
		Dim nRet		As Integer


		If DevName = "" Then

			DevName			= "FBIAD1"

		End If


		Try

			hEXAD			= IFCAD_ANY.AdOpen( DevName )

		Catch ex As Exception

			WriteLog( "", "LG", "ExAd_Open AD:" + DevName + " ボードなし？" )

			AD_OPEN			= False


			Return -1

		End Try


		If hEXAD = New IntPtr( -1 ) Then

			WriteLog( "", "LG", "ExAd_Open AD:" + DevName + " オープンエラー" )

			AD_OPEN			= False


			Return -2

		End If


		nRet			= IFCAD_ANY.AdGetDeviceInfo( hEXAD, ADInfo )

		If nRet <> IFCAD_ANY.AD_ERROR_SUCCESS Then

			' MessageBox.Show(ConvErrMessageAd(nRet), "ＡＤボードエラー")

			WriteLog( "", "LG", "ExAd_Open デバイス仕様エラー：" + ConvErrMessageAd( nRet ) )

			IFCAD_ANY.AdClose( hEXAD )


			AD_OPEN			= False


			Return nRet

		End If


		WriteLog( "", "LG", "ExAd_Open AD:" + DevName + " オープン完了" )

		AD_OPEN			= True


		' 入力レンジを±１０Ｖに設定
		Dim i			As Integer


		For i = 0 To ADnCH

			SmplChInf( i ).ulChNo	= i

			SmplChInf( i ).ulRange	= IFCAD_ANY.AD_10V

		Next


		Return IFCAD_ANY.AD_ERROR_SUCCESS

	End Function



	Public Function ExAd_Close() As Integer

		' Ａ／Ｄデバイスクローズ
		Dim nRet As Integer


		If AD_OPEN = True Then

			nRet			= IFCAD_ANY.AdClose( hEXAD )

			If nRet = IFCAD_ANY.AD_ERROR_SUCCESS Then

				WriteLog( "", "LG", "ExAd_Close クローズ完了" )

			Else

				WriteLog( "", "LG", "ExAd_Close クローズエラー" )

			End If

		Else

			WriteLog( "", "LG", "ExAd_Close 未オープン" )

			nRet			= -3

		End If



		Return nRet

	End Function



	Public Sub ExAd_SetRange			_
	(						_
		ByVal ch		As Integer,	_
		ByVal range		As Integer	_
	)


		' チャンネル番号毎のレンジの個別設定
		' SmplChInf(ch).ulChNo = ch + 1
		If ch > 0 Or ch <= ADnCH Then

			SmplChInf( ch ).ulRange		= range

		End If


	End Sub



	Public Function ExAd_Input() As Integer


		' Ａ／Ｄ入力　ADnCH (MAX16CH)
		Dim nRet		As Integer



		'
		'	20140120 拡張基板が無くても動作するようにする
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			nRet			= 0

		Else

			nRet			= IFCAD_ANY.AdInputAD	_
			(						_
				hEXAD,					_
				ADnCH,					_
				IFCAD_ANY.AD_INPUT_SINGLE,		_
				SmplChInf( 1 ),				_
				AdBuf( 1 )				_
			)

		End If



		Return nRet

	End Function



	Public Function ExAd_Input			_
	(						_
		ByVal ch		As Integer	_
	)	As Integer

		' チャンネル番号毎のＡ／Ｄ入力　
		Dim nRet		As Integer



		If ch <= 0 Or ch > ADnCH Then

			Return -1

		End If


		nRet			= IFCAD_ANY.AdInputAD		_
		(							_
			hEXAD,						_
			1,						_
			IFCAD_ANY.AD_INPUT_SINGLE,			_
			SmplChInf( ch ),				_
			AdBuf( ch )					_
		)


		Return nRet

	End Function



	Public Function ConvErrMessageAd		_
	(						_
		ByVal uErrCode		As Integer	_
	)	As String

		' 戻り値のメッセージ変換
		Dim strMsg		As String


		Select Case	uErrCode

		Case	IFCAD_ANY.AD_ERROR_SUCCESS

			strMsg		= "正常終了"

		Case	IFCAD_ANY.AD_ERROR_NOT_DEVICE

			strMsg		= "ドライバが呼び出せません"

		Case	IFCAD_ANY.AD_ERROR_NOT_OPEN

			strMsg		= "ドライバがオープンできません"

		Case	IFCAD_ANY.AD_ERROR_INVALID_HANDLE

			strMsg		= "デバイスハンドルが正しくありません"

		Case	IFCAD_ANY.AD_ERROR_ALREADY_OPEN

			strMsg		= "既にオープンしているデバイスです"

		Case	IFCAD_ANY.AD_ERROR_NOT_SUPPORTED

			strMsg		= "製品がサポートしていない関数です"

		Case	IFCAD_ANY.AD_ERROR_NOW_SAMPLING

			strMsg		= "サンプリングを実行中です"

		Case	IFCAD_ANY.AD_ERROR_STOP_SAMPLING

			strMsg		= "サンプリングは停止中です"

		Case	IFCAD_ANY.AD_ERROR_START_SAMPLING

			strMsg		= "サンプリングを実行できませんでした"

		Case	IFCAD_ANY.AD_ERROR_SAMPLING_TIMEOUT

			strMsg		= "サンプリング中においてタイムアウトが発生しました"

		Case	IFCAD_ANY.AD_ERROR_INVALID_PARAMETER

			strMsg		= "パラメーターが不正です"

		Case	IFCAD_ANY.AD_ERROR_ILLEGAL_PARAMETER

			strMsg		= "サンプリング設定が正しくありません"

		Case	IFCAD_ANY.AD_ERROR_NULL_POINTER

			strMsg		= "NULLポインタを指定しました"

		Case	IFCAD_ANY.AD_ERROR_GET_DATA

			strMsg		= "サンプリングデータの取得ができませんでした"

		Case	IFCAD_ANY.AD_ERROR_FILE_OPEN

			strMsg		= "ファイルのオープンに失敗しました"

		Case	IFCAD_ANY.AD_ERROR_FILE_CLOSE

			strMsg		= "ファイルのクローズに失敗しました"

		Case	IFCAD_ANY.AD_ERROR_FILE_READ

			strMsg		= "ファイルのリードに失敗しました"

		Case	IFCAD_ANY.AD_ERROR_FILE_WRITE

			strMsg		= "ファイルのライトに失敗しました"

		Case	IFCAD_ANY.AD_ERROR_INVALID_DATA_FORMAT

			strMsg		= "データ形式が無効です"

		Case	IFCAD_ANY.AD_ERROR_INVALID_AVERAGE_OR_SMOOTHING

			strMsg		= "平均またはスムージングの指定が正しくありません"

		Case	IFCAD_ANY.AD_ERROR_INVALID_SOURCE_DATA

			strMsg		= "データ変換元として指定されたデータが正しくありません"

		Case	IFCAD_ANY.AD_ERROR_NOT_ALLOCATE_MEMORY

			strMsg		= "メモリが確保できません"

		Case	IFCAD_ANY.AD_ERROR_NOT_LOAD_DLL

			strMsg		= "DLLがロードできませんでした"

		Case	IFCAD_ANY.AD_ERROR_CALL_DLL

			strMsg		= "DLLの呼び出しに失敗しました"

		Case	-1

			strMsg		= "ボードがありません"

		Case	-2

			strMsg		= "デバイスオープンできません"

		Case	-3

			strMsg		= "デバイスが見オープンです"

		Case Else

			strMsg		= "未設定の戻り値です errCode=&H" + uErrCode.ToString( "x8" )

		End Select


		Return strMsg

	End Function



End Module
