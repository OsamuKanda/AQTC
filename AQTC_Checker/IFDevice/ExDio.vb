

Imports		InterfaceCorpDllWrap

Imports		System.Configuration

'
'	20200205 y.goto
'	EXDIO2 (PCI-2760C)追加に伴う修正実施
'

Module ExDio


	' PCI-2760C

	' InBuf,OutBufの(0), は未使用
	' 20200205 配列サイズを 32 -> 64 に変更し、EXDIO, EXDIO2と共用する
	Public InBuf( 64 )		As Integer

	' 20200205 配列サイズを 32 -> 64 に変更、EXDIO, EXDIO2と共用する
	Public OutBuf( 64 )		As Integer


	Dim hEXDIO			As IntPtr

	' 20200205 EXDIO2用に追加
	Dim h2EXDIO			As IntPtr

	Dim DIO_OPEN			As Boolean

	' 20200205 EXDIO2用に追加
	Dim DIO2_OPEN			As Boolean

	Public OutOldBuf( 64 )		As Integer

	'
	'	20200908 追加
	'	DOインターロックシーケンス
	'

	' 現在の処理ステップ
	Public DOILCstep		As Integer

	' DOインターロックシーケンス終了
	Public Const ILCseqEND		As Integer	= 0

	' DP:OFF->ON シーケンス開始ステップ
	Public Const ILCseqDPON		As Integer	= 510000

	' DP:ON->OFF シーケンス開始ステップ
	Public Const ILCseqDPOFF	As Integer	= 530000

	' MB:OFF->ON シーケンス開始ステップ
	Public Const ILCseqMBON		As Integer	= 550000

	' MB:ON->OFF シーケンス開始ステップ
	Public Const ILCseqMBOFF	As Integer	= 570000

	' G1:CLOSE->OPEN シーケンス開始ステップ
	Public Const ILCseqG1OPEN	As Integer	= 610000

	' G1:OPEN->CLOSE シーケンス開始ステップ
	Public Const ILCseqG1CLOSE	As Integer	= 630000

	' G4:CLOSE->OPEN シーケンス開始ステップ
	Public Const ILCseqG4OPEN	As Integer	= 650000

	' G4:OPEN->CLOSE シーケンス開始ステップ
	Public Const ILCseqG4CLOSE	As Integer	= 670000

	' MV:CLOSE->OPEN シーケンス開始ステップ
	Public Const ILCseqMVOPEN	As Integer	= 710000

	' MV:OPEN->CLOSE シーケンス開始ステップ
	Public Const ILCseqMVCLOSE	As Integer	= 730000

	' LV:CLOSE->OPEN シーケンス開始ステップ
	Public Const ILCseqLVOPEN	As Integer	= 750000

	' LV:OPEN->CLOSE シーケンス開始ステップ
	Public Const ILCseqLVCLOSE	As Integer	= 770000

	'
	'	DOインターロックシーケンス
	'	タイマーダウンカウンタ
	'

	' ダウンカウンタ１
	Dim TmrDCnt1			As Integer

	' ダウンカウンタ２
	Dim TmrDCnt2			As Integer

	'*****
	'	20200902 y.goto
	'	配管制御状態 DP, MB, G1, G4, MV, LVの変化をログ記録
	'*****
	Public Sub SigPlumbPutLog()

		Dim msg			As String
		Dim chgflg		As Integer

		chgflg			= 0

		msg			= "<><><> DP="
		If OutOldBuf( MAEdoPUMP ) <> OutBuf( MAEdoPUMP ) Then

			chgflg			= 1

			If OutBuf( MAEdoPUMP ) Then

				msg			= msg + "U"

			Else

				msg			= msg + "D"

			End If

		Else

			msg			= msg + OutBuf( MAEdoPUMP ).ToString()

		End If

		msg			= msg + " MB="
		If OutOldBuf( EXSdoMBP ) <> OutBuf( EXSdoMBP ) Then

			chgflg			= 1

			If OutBuf( EXSdoMBP ) Then

				msg			= msg + "U"

			Else

				msg			= msg + "D"

			End If

		Else

			msg			= msg + OutBuf( EXSdoMBP ).ToString()

		End If

		msg			= msg + " G4="
		If OutOldBuf( EXSdoRYE3 ) <> OutBuf( EXSdoRYE3 ) Then

			chgflg			= 1

			If OutBuf( EXSdoRYE3 ) Then

				msg			= msg + "U"

			Else

				msg			= msg + "D"

			End If

		Else

			msg			= msg + OutBuf( EXSdoRYE3 ).ToString()

		End If

		msg			= msg + " G1="
		If OutOldBuf( EXSdoRYE2 ) <> OutBuf( EXSdoRYE2 ) Then

			chgflg			= 1

			If OutBuf( EXSdoRYE2 ) Then

				msg			= msg + "U"

			Else

				msg			= msg + "D"

			End If

		Else

			msg			= msg + OutBuf( EXSdoRYE2 ).ToString()

		End If

		msg			= msg + " MV="
		If OutOldBuf( EXSdoRYE1 ) <> OutBuf( EXSdoRYE1 ) Then

			chgflg			= 1

			If OutBuf( EXSdoRYE1 ) Then

				msg			= msg + "U"

			Else

				msg			= msg + "D"

			End If

		Else

			msg			= msg + OutBuf( EXSdoRYE1 ).ToString()

		End If

		msg			= msg + " LV="
		If OutOldBuf( EXSdoRYE4 ) <> OutBuf( EXSdoRYE4 ) Then

			chgflg			= 1

			If OutBuf( EXSdoRYE4 ) Then

				msg			= msg + "U"

			Else

				msg			= msg + "D"

			End If

		Else

			msg			= msg + OutBuf( EXSdoRYE4 ).ToString

		End If

		' 変化が有った時のみログ出力
		If chgflg Then

			FrmLog.LogDspAdd( "", msg, Color.Empty )

		End If

	End Sub

	'*****
	'	20200901 y.goto
	'	信号状態ログ出力
	'*****
	Public Sub SigPutLog()

		Dim msg			As String
		Dim i			As Integer

		msg		= "DOX:"

		For i = 1 To 38


			If OutBuf( i ) <> OutOldBuf( i ) Then

				If OutBuf( i ) Then

					msg		= msg + "U"

				Else

					msg		= msg + "D"

				End If

			Else

				msg		= msg + OutBuf( i ).ToString()

			End If

			If i Mod 5 = 0 Then

				msg		= msg + " "

			End If

		Next i

		msg		= msg + " DIX:"
		For i = 1 To 42

			msg		= msg + InBuf( i ).ToString()

			If i Mod 5 = 0 Then

				msg		= msg + " "

			End If

		Next i

		msg		= msg + " AOX:"
		For i = 1 To 6

			msg		= msg + " " + DaBuf( i ).ToString( "X4" )

		Next i

		msg		= msg + " AIX:"
		For i = 1 To 7

			msg		= msg + " " + AdBuf( i ).ToString( "X4" )

		Next i

		FrmLog.LogDspAdd( "", msg, Color.Empty )

		' 配管制御状態 DP, MB, G1, G4, MV, LVの変化をログ記録
		SigPlumbPutLog()

	End Sub

	Public Function ExDio_Open() As Integer

		'デバイスオープン
		Dim DevName			As String = ConfigurationManager.AppSettings.Item( "DioDeviceName" )
		Dim Dev2Name			As String


		' 初期値セット
		Dev2Name		= "FBIDIO2"
		DIO_OPEN		= False
		DIO2_OPEN		= False

		If DevName = "" Then

			DevName			= "FBIDIO1"

		End If

		' EXDIO (PCI-2760C) のオープン
		Try

			hEXDIO			= IFCDIO_ANY.DioOpen( DevName, IFCDIO_ANY.FBIDIO_FLAG_SHARE )

		Catch ex As Exception

			WriteLog( "", "LG", "ExDio_Open DIO:" + DevName + " ボードなし？" )

			Return -1

		End Try

		' EXDIO2 (PCI-2760C) のオープン
		Try

			h2EXDIO			= IFCDIO_ANY.DioOpen( Dev2Name, IFCDIO_ANY.FBIDIO_FLAG_SHARE )

		Catch ex As Exception

			WriteLog( "", "LG", "ExDio_Open DIO:" + Dev2Name + " ボードなし？" )

			Return -1

		End Try

		' EXDIO がオープン出来たか確認
		If hEXDIO = New IntPtr( -1 ) Then

			WriteLog( "", "LG", "ExDio_Open DIO:" + DevName + " オープンエラー" )

			Return -2

		End If

		' EXDIO オープン完了
		DIO_OPEN		= True

		' EXDIO2 がオープン出来たか確認
		If h2EXDIO = New IntPtr( -1 ) Then

			WriteLog( "", "LG", "ExDio_Open DIO:" + Dev2Name + " オープンエラー" )

			Return -2

		End If

		' EXDIO2 オープン完了
		DIO2_OPEN		= True

		WriteLog( "", "LG", "ExDio_Open DIO:" + DevName + " オープン完了" )

		WriteLog( "", "LG", "ExDio_Open DIO:" + Dev2Name + " オープン完了" )


		Return IFCDIO_ANY.FBIDIO_ERROR_SUCCESS

	End Function



	Public Function ExDio_Close() As Integer

		' デバイスクローズ
		Dim nRet		As Integer


		' EXDIO クローズ
		If DIO_OPEN = True Then

			nRet			= IFCDIO_ANY.DioClose( hEXDIO )

			If nRet = IFCDIO_ANY.FBIDIO_ERROR_SUCCESS Then

				WriteLog( "", "LG", "ExDio_Close EXDIO クローズ完了" )

			Else

				WriteLog( "", "LG", "ExDio_Close EXDIO クローズエラー" )

			End If

		Else

			WriteLog( "", "LG", "ExDio_Close EXDIO 未オープン" )

			nRet			= -3

		End If

		' EXDIO2 クローズ
		If DIO2_OPEN = True Then

			nRet			= IFCDIO_ANY.DioClose( h2EXDIO )

			If nRet = IFCDIO_ANY.FBIDIO_ERROR_SUCCESS Then

				WriteLog( "", "LG", "ExDio_Close EXDIO2 クローズ完了" )

			Else

				WriteLog( "", "LG", "ExDio_Close EXDIO2 クローズエラー" )

			End If

		Else

			WriteLog( "", "LG", "ExDio_Close EXDIO2 未オープン" )

			nRet			= -3

		End If



		Return nRet

	End Function



	Public Function ExDio_Input() As Integer

		' 入力　MAX64点
		Dim nRet		As Integer
		Dim n2Ret		As Integer

		' EXDIO DIX01～DIX10 入力
		'
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			nRet			= 0

		Else

			nRet			= IFCDIO_ANY.DioInputPoint	_
			(							_
				hEXDIO,						_
				InBuf( 1 ),					_
				1,						_
				12						_
			)

		End If

		' EXDIO2 DIX33～DIX44 入力
		'
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			n2Ret			= 0

		Else

			n2Ret			= IFCDIO_ANY.DioInputPoint	_
			(							_
				h2EXDIO,					_
				InBuf( 33 ),					_
				1,						_
				12						_
			)

		End If

		'
		'	20200410 デバックモード時の入力信号操作
		'
		If DBGmode = 1 Then

			' ESCのｽﾃｰﾀｽ信号は起動信号に合わせる
			InBuf( ESCdiSTAT1 )	= OutBuf( ESCdoSTART1 )
			InBuf( ESCdiSTAT2 )	= OutBuf( ESCdoSTART2 )

			' サーモチラーのトラブル信号を強制的に無効
			InBuf( SCRdiTROUBLE1 )	= 1
			InBuf( SCRdiTROUBLE2 )	= 1

		End If

		' EXDIO は正常終了したか確認
		If nRet <> IFCDIO_ANY.FBIDIO_ERROR_SUCCESS Then

			' 異常終了
			Return nRet

		End If

		' EXDIO2 は正常終了したか確認
		If n2Ret <> IFCDIO_ANY.FBIDIO_ERROR_SUCCESS Then

			' 異常終了
			Return nRet

		End If


		' 正常終了
		Return nRet

	End Function

	'*****
	'	20200909 y.goto
	'	シーケンスは本関数を使用すること
	'	インターロックが有るDOはシーケンス完了するまで本関数から戻らない
	'	<注意>
	'	MB に関しては、チャンバ圧力が2400Pa以上だと起動しないで戻るので、
	'	呼び出し側では MB が起動したかをチェックする必要が有る
	'*****
	Public Function ExDio_Output			_
	(						_
		ByVal pNo		As Integer,	_
		ByVal pDat		As Integer	_
	)

		Dim nRet		As Integer

		nRet		= 0

		'
		'	DOインターロックシーケンス
		'
		Select Case	pNo

		Case	MAEdoPUMP

			If OutOldBuf( pNo ) = 0 And pDat Then

				' DP OFF->ON シーケンス開始
				DOILCstep		= ILCseqDPON

			ElseIf OutOldBuf( pNo ) And pDat = 0 Then

				' DP ON->OFF シーケンス開始
				DOILCstep		= ILCseqDPOFF

			End If

		Case	EXSdoMBP

			If OutOldBuf( pNo ) = 0 And pDat Then

				' MB OFF->ON シーケンス開始
				DOILCstep		= ILCseqMBON

			ElseIf OutOldBuf( pNo ) And pDat = 0 Then

				' MB ON->OFF シーケンス開始
				DOILCstep		= ILCseqMBOFF

			End If

		Case	EXSdoRYE2

			If OutOldBuf( pNo ) = 0 And pDat Then

				' G1:CLOSE->OPEN シーケンス開始
				DOILCstep		= ILCseqG1OPEN

			ElseIf OutOldBuf( pNo ) And pDat = 0 Then

				' G1:OPEN->CLOSE シーケンス開始
				DOILCstep		= ILCseqG1CLOSE

			End If

		Case	EXSdoRYE3

			If OutOldBuf( pNo ) = 0 And pDat Then

				' G4:CLOSE->OPEN シーケンス開始
				DOILCstep		= ILCseqG4OPEN

			ElseIf OutOldBuf( pNo ) And pDat = 0 Then

				' G4:OPEN->CLOSE シーケンス開始
				DOILCstep		= ILCseqG4CLOSE

			End If

		Case	EXSdoRYE1

			If OutOldBuf( pNo ) = 0 And pDat Then

				' MV:CLOSE->OPEN シーケンス開始
				DOILCstep		= ILCseqMVOPEN

			ElseIf OutOldBuf( pNo ) And pDat = 0 Then

				' MV:OPEN->CLOSE シーケンス開始
				DOILCstep		= ILCseqMVCLOSE

			End If

		Case	EXSdoRYE4

			If OutOldBuf( pNo ) = 0 And pDat Then

				' LV:CLOSE->OPEN シーケンス開始
				DOILCstep		= ILCseqLVOPEN

			ElseIf OutOldBuf( pNo ) And pDat = 0 Then

				' LV:OPEN->CLOSE シーケンス開始
				DOILCstep		= ILCseqLVCLOSE

			End If

		End Select

		If DOILCstep <> ILCseqEND Then

			' DOインターロックシーケンス終了まで待つ
			Do While DOILCstep <> ILCseqEND

				Application.DoEvents()

			Loop

		Else

			' DIO基板DO出力
			nRet		= ExDoOut( pNo, pDat )

		End If

		Return nRet

	End Function


	'*****
	'	20200909 y.goto ExDio.vb内部専用・外部ソースからの呼び出しは禁止
	'	DIO基板DO出力
	'*****
	Private Function ExDoOut			_
	(						_
		ByVal pNo		As Integer,	_
		ByVal pDat		As Integer	_
	)
		Dim nRet		As Integer

		' 出力　個別指定ポイント　OutBufに保存
		OutBuf( pNo )		= pDat

		'
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			nRet			= 0

		Else

			If 1 <= pNo And 32 >= pNo Then

				' EXDIO が対象 (DOX01～DOX32)
				nRet			= IFCDIO_ANY.DioOutputPoint( hEXDIO, OutBuf( pNo ), pNo, 1 )

			ElseIf 33 <= pNo And 64 >= pNo Then

				' EXDIO2 が対象 (DOX33～DOX64)
				nRet			= IFCDIO_ANY.DioOutputPoint( h2EXDIO, OutBuf( pNo ), pNo - 32, 1 )

			End If

		End If

		' 変化有無判断
		For i = 1 To 64
			If OutBuf( i ) <> OutOldBuf( i ) Then

				' 変更有り

				' 信号状態ログ出力
				SigPutLog()

				Exit For

			End If
		Next

		Array.Copy( OutBuf, OutOldBuf, OutBuf.Length )

		Return nRet

	End Function


	Public Function ExDio_Output_ALL()

		' 出力　OutBufのデータを全出力
		Dim nRet		As Integer


		' EXDIO 全点出力
		nRet			= IFCDIO_ANY.DioOutputPoint( hEXDIO, OutBuf( 1 ), 1, 32 )

		' EXDIO2 全点出力
		nRet			= IFCDIO_ANY.DioOutputPoint( h2EXDIO, OutBuf( 33 ), 1, 32 )


		Return nRet

	End Function



	Public Function ExDio_Output_AllClear()

		' 出力全クリア　32 + 32点
		Dim nRet		As Integer
		Dim i			As Integer



		For i = 1 To 64

			OutBuf( i )		= DIO_OFF

			OutOldBuf( i )		= DIO_OFF
		Next

		'
		'	20140120 拡張基板が無くても動作するようにする
		'	20200508 Visual Studio の Debug でも動作するように対応 DoEventsから戻って来ない対応
		'
		If DBGmode = 1 Then

			nRet			= 0

		Else

			' EXDIO 全点出力
			nRet			= IFCDIO_ANY.DioOutputPoint( hEXDIO, OutBuf( 1 ), 1, 32 )

			' EXDIO2 全点出力
			nRet			= IFCDIO_ANY.DioOutputPoint( h2EXDIO, OutBuf( 33 ), 1, 32 )

		End If



		Return nRet

	End Function



	Public Function ConvErrMessageDio		_
	(						_
		ByVal uErrCode		As Integer	_
	)	As String

		' 戻り値のメッセージ変換
		Dim strMsg		As String


		Select Case	uErrCode

		Case	IFCDIO_ANY.FBIDIO_ERROR_SUCCESS

			strMsg		= "正常終了"

		Case	IFCDIO_ANY.FBIDIO_ERROR_NOT_DEVICE

			strMsg		= "ドライバが呼び出せません"

		Case	IFCDIO_ANY.FBIDIO_ERROR_NOT_OPEN

			strMsg		= "ドライバがオープンできません"

		Case	IFCDIO_ANY.FBIDIO_ERROR_INVALID_HANDLE

			strMsg		= "デバイスハンドルが正しくありません"

		Case	IFCDIO_ANY.FBIDIO_ERROR_ALREADY_OPEN

			strMsg		= "既にオープンしているデバイスです"

		Case	IFCDIO_ANY.FBIDIO_ERROR_INSUFFICIENT_BUFFER

			strMsg		= "システムコールに渡されたデータ領域が小さすぎます"

		Case	IFCDIO_ANY.FBIDIO_ERROR_IO_PENDING

			strMsg		= "非同期I/O操作が進行中です"

		Case	IFCDIO_ANY.FBIDIO_ERROR_NOT_SUPPORTED

			strMsg		= "サポートされていない機能です"

		Case	IFCDIO_ANY.FBIDIO_ERROR_MEMORY_NOTALLOCATED

			strMsg		= "作業用メモリの確保に失敗しました"

		Case	IFCDIO_ANY.FBIDIO_ERROR_NULL_POINTER

			strMsg		= "DLL、ドライバ間でNULL参照渡しが渡されました"

		Case	IFCDIO_ANY.FBIDIO_ERROR_PARAMETER

			strMsg		= "引数パラメーターの値が不正です"

		Case	IFCDIO_ANY.FBIDIO_ERROR_INVALID_CALL

			strMsg		= "不正な呼び出しです"

		Case	IFCDIO_ANY.FBIDIO_ERROR_DRVCAL

			strMsg		= "ドライバをコールできません"

		Case	-1

			strMsg		= "ボードがありません"

		Case	-2

			strMsg		= "デバイスオープンできません"

		Case	-3

			strMsg		= "デバイスが未オープンです"

		Case Else

			strMsg		= "未設定の戻り値です errCode=&H" + uErrCode.ToString( "x8" )

		End Select



		Return strMsg


	End Function

	'*****
	'	20200908 追加
	'	DOインターロックシーケンス
	'
	'	0.1秒インターバルタイマー処理
	'	DHDMenu.vbのインターバルタイマー TimINP より呼び出されて処理を行う
	'*****
	Public Sub DOILCseq()

		Dim prscmb			As Double

		'
		'	タイマーカウンタダウンカウント処理
		'

		' ダウンカウンタ１
		If TmrDCnt1 Then

			TmrDCnt1	-= 1

		End If

		' ダウンカウンタ２
		If TmrDCnt2 Then

			TmrDCnt2	-= 1

		End If


		Select Case	DOILCstep

		'*****
		'	DOインターロックシーケンス終了
		'*****
		Case	ILCseqEND



		'*****
		'	DP OFF->ON シーケンス開始
		'*****
		Case	ILCseqDPON

			DOILCstep	= ILCseqDPON + 1000

		Case	ILCseqDPON + 1000

			' LV:OPEN ?
			If OutBuf( EXSdoRYE4 ) Then

				' Yes LV:OPEN
				DOILCstep	= ILCseqDPON + 2000

			Else

				' Yes LV:CLOSE
				DOILCstep	= ILCseqDPON + 3000

			End If

		'
		'	LV:CLOSE
		'
		Case	ILCseqDPON + 2000

			' LV:CLOSE
			ExDoOut( EXSdoRYE4, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqDPON + 2100

		Case	ILCseqDPON + 2100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqDPON + 3000

			End If

		'
		'	DP:ON
		'
		Case	ILCseqDPON + 3000

			' DP:ON
			ExDoOut( MAEdoPUMP, DIO_ON )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	DP ON->OFF シーケンス開始
		'*****
		Case	ILCseqDPOFF

			DOILCstep	= ILCseqDPOFF + 1000

		Case	ILCseqDPOFF + 1000

			' MV:OPEN ?
			If OutBuf( EXSdoRYE1 ) Then

				' Yes MV:OPEN
				DOILCstep	= ILCseqDPOFF + 2000

			Else

				' No  MV:CLOSE
				DOILCstep	= ILCseqDPOFF + 3000

			End If

		'
		'	MV:CLOSE
		'
		Case	ILCseqDPOFF + 2000

			' MV:CLOSE
			ExDoOut( EXSdoRYE1, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqDPOFF + 2100

		Case	ILCseqDPOFF + 2100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqDPOFF + 3000

			End If

		Case	ILCseqDPOFF + 3000

			' G4:OPEN ?
			If OutBuf( EXSdoRYE3 ) Then

				' Yes G4:OPEN
				DOILCstep	= ILCseqDPOFF + 4000

			Else

				' No  G4:CLOSE
				DOILCstep	= ILCseqDPOFF + 5000

			End If

		'
		'	G4:CLOSE
		'
		Case	ILCseqDPOFF + 4000

			' G4:CLOSE
			ExDoOut( EXSdoRYE3, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqDPOFF + 4100

		Case	ILCseqDPOFF + 4100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqDPOFF + 5000

			End If

		Case	ILCseqDPOFF + 5000

			' MB:ON ?
			If OutBuf( EXSdoMBP ) Then

				' Yes MB ON
				DOILCstep	= ILCseqDPOFF + 6000

			Else

				' Yes MB OFF
				DOILCstep	= ILCseqDPOFF + 7000

			End If

		'
		'	MB:OFF
		'
		Case	ILCseqDPOFF + 6000

			' MB:OFF
			ExDoOut( EXSdoMBP, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqDPOFF + 6100

		Case	ILCseqDPOFF + 6100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqDPOFF + 7000

			End If

		'
		'	DP:OFF
		'
		Case	ILCseqDPOFF + 7000

			' DP:OFF
			ExDoOut( MAEdoPUMP, DIO_OFF )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	MB OFF->ON シーケンス開始
		'*****
		Case	ILCseqMBON

			DOILCstep	= ILCseqMBON + 1000

		Case	ILCseqMBON + 1000

			prscmb		= FrmGraph.MesCbp

			' PIG (ﾁｬﾝﾊﾞ圧)  < 2400Pa ?
			If prscmb < 2400 Then

				' Yes チャンバ圧は 2400Pa 未満
				DOILCstep	= ILCseqMBON + 2000

			Else

				' No  チャンバ圧は 2400Pa 以上、MB起動不可
				FrmLog.LogDspAdd( "", "DOILCseq() PIGが2400Pa以上,MB起動不可 " + DOILCstep.ToString(), Color.Empty )

				DOILCstep	= ILCseqMBON + 5000

			End If

		Case	ILCseqMBON + 2000

			' G4:OPEN ?
			If OutBuf( EXSdoRYE3 ) Then

				' Yes G4:OPEN
				DOILCstep	= ILCseqMBON + 3000

			Else

				' No  G4:CLOSE
				DOILCstep	= ILCseqMBON + 4000

			End If

		'
		'	G4:CLOSE
		'
		Case	ILCseqMBON + 3000

			' G4:CLOSE
			ExDoOut( EXSdoRYE3, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqMBON + 3100

		Case	ILCseqMBON + 3100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqMBON + 4000

			End If

		'
		'	MB:ON
		'
		Case	ILCseqMBON + 4000

			' MB:ON
			ExDoOut( EXSdoMBP, DIO_ON )

			DOILCstep	= ILCseqMBON + 5000

		'
		'	MB OFF->ON シーケンス終了
		'
		Case	ILCseqMBON + 5000

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	MB ON->OFF シーケンス開始
		'*****
		Case	ILCseqMBOFF

			DOILCstep	= ILCseqMBOFF + 1000

		Case	ILCseqMBOFF + 1000

			' MV:OPEN ?
			If OutBuf( EXSdoRYE1 ) Then

				' Yes MV OPEN
				DOILCstep	= ILCseqMBOFF + 2000

			Else

				' Yes MV CLOSE
				DOILCstep	= ILCseqMBOFF + 5000

			End If

		'
		'	MV閉
		'
		Case	ILCseqMBOFF + 2000

			' MV 閉
			ExDoOut( EXSdoRYE1, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqMBOFF + 2100

		Case	ILCseqMBOFF + 2100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqMBOFF + 5000

			End If

		'
		'	MB OFF
		'
		Case	ILCseqMBOFF + 5000

			' MB OFF
			ExDoOut( EXSdoMBP, DIO_OFF )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	G1 CLOSE->OPEN シーケンス開始
		'*****
		Case	ILCseqG1OPEN

			' G1 開
			ExDoOut( EXSdoRYE2, DIO_ON )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	G1 OPEN->CLOSE シーケンス開始
		'*****
		Case	ILCseqG1CLOSE

			' G1 閉
			ExDoOut( EXSdoRYE2, DIO_OFF )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	G4 CLOSE->OPEN シーケンス開始
		'*****
		Case	ILCseqG4OPEN

			DOILCstep	= ILCseqG4OPEN + 1000

		Case	ILCseqG4OPEN + 1000

			' MB:ON ?
			If OutBuf( EXSdoMBP ) Then

				' Yes MB:ON
				DOILCstep	= ILCseqG4OPEN + 2000

			Else

				' Yes MB:OFF
				DOILCstep	= ILCseqG4OPEN + 5000

			End If

		Case	ILCseqG4OPEN + 2000

			' MV:OPEN ?
			If OutBuf( EXSdoRYE1 ) Then

				' Yes MV:OPEN
				DOILCstep	= ILCseqG4OPEN + 3000

			Else

				' Yes MV:CLOSE
				DOILCstep	= ILCseqG4OPEN + 4000

			End If

		'
		'	MV:CLOSE
		'
		Case	ILCseqG4OPEN + 3000

			' MV:CLOSE
			ExDoOut( EXSdoRYE1, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqG4OPEN + 3100

		Case	ILCseqG4OPEN + 3100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqG4OPEN + 4000

			End If

		'
		'	MB:OFF
		'
		Case	ILCseqG4OPEN + 4000

			' MB:OFF
			ExDoOut( EXSdoMBP, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqG4OPEN + 4100

		Case	ILCseqG4OPEN + 4100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqG4OPEN + 5000

			End If

		Case	ILCseqG4OPEN + 5000

			' DP:ON ?
			If OutBuf( MAEdoPUMP ) Then

				' Yes DP:ON
				DOILCstep	= ILCseqG4OPEN + 6000

			Else

				' No  DP:OFF
				DOILCstep	= ILCseqG4OPEN + 8000

			End If

		Case	ILCseqG4OPEN + 6000

			' LV:OPEN ?
			If OutBuf( EXSdoRYE4 ) Then

				' Yes LV:OPEN
				DOILCstep	= ILCseqG4OPEN + 7000

			Else

				' No  LV:CLOSE
				DOILCstep	= ILCseqG4OPEN + 8000

			End If

		'
		'	LV:CLOSE
		'
		Case	ILCseqG4OPEN + 7000

			' LV:CLOSE
			ExDoOut( EXSdoRYE4, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqG4OPEN + 7100

		Case	ILCseqG4OPEN + 7100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqG4OPEN + 8000

			End If

		'
		'	G4:OPEN
		'
		Case	ILCseqG4OPEN + 8000

			' G4:OPEN
			ExDoOut( EXSdoRYE3, DIO_ON )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	G4:OPEN->CLOSE シーケンス開始
		'*****
		Case	ILCseqG4CLOSE

			' G4:CLOSE
			ExDoOut( EXSdoRYE3, DIO_OFF )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	MV CLOSE->OPEN シーケンス開始
		'*****
		Case	ILCseqMVOPEN

			DOILCstep	= ILCseqMVOPEN + 1000

		Case	ILCseqMVOPEN + 1000

			prscmb		= FrmGraph.MesCbp

			' PIG (ﾁｬﾝﾊﾞ圧) >= 2400Pa ?
			If prscmb >= 2400 Then

				' Yes チャンバ圧は2400Pa以上
				DOILCstep	= ILCseqMVOPEN + 2000

			Else

				' No  チャンバ圧は2400Pa未満
				DOILCstep	= ILCseqMVOPEN + 4000

			End If

		Case	ILCseqMVOPEN + 2000

			' MB:ON ?
			If OutBuf( EXSdoMBP ) Then

				' Yes MB:ON
				DOILCstep	= ILCseqMVOPEN + 3000

			Else

				' No  MB:OFF
				DOILCstep	= ILCseqMVOPEN + 4000

			End If

		'
		'	MB:OFF
		'
		Case	ILCseqMVOPEN + 3000

			' MB:OFF
			ExDoOut( EXSdoMBP, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqMVOPEN + 3100

		Case	ILCseqMVOPEN + 3100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqMVOPEN + 4000

			End If

		Case	ILCseqMVOPEN + 4000

			' DP:ON ?
			If OutBuf( MAEdoPUMP ) Then

				' Yes DP:ON
				DOILCstep	= ILCseqMVOPEN + 5000

			Else

				' No  DP:OFF
				DOILCstep	= ILCseqMVOPEN + 7000

			End If

		Case	ILCseqMVOPEN + 5000

			' LV:OPEN ?
			If OutBuf( EXSdoRYE4 ) Then

				' Yes LV:OPEN
				DOILCstep	= ILCseqMVOPEN + 6000

			Else

				' No  LV:CLOSE
				DOILCstep	= ILCseqMVOPEN + 7000

			End If

		'
		'	LV:CLOSE
		'
		Case	ILCseqMVOPEN + 6000

			' LV:CLOSE
			ExDoOut( EXSdoRYE4, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqMVOPEN + 6100

		Case	ILCseqMVOPEN + 6100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqMVOPEN + 7000

			End If

		'
		'	MV:OPEN
		'
		Case	ILCseqMVOPEN + 7000

			' MV:OPEN
			ExDoOut( EXSdoRYE1, DIO_ON )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	MV OPEN->CLOSE シーケンス開始
		'*****
		Case	ILCseqMVCLOSE

			' MV CLOSE
			ExDoOut( EXSdoRYE1, DIO_OFF )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	LV:CLOSE->OPEN シーケンス開始
		'*****
		Case	ILCseqLVOPEN

			DOILCstep	= ILCseqLVOPEN + 1000

		Case	ILCseqLVOPEN + 1000

			' MV:OPEN ?
			If OutBuf( EXSdoRYE1 ) Then

				' Yes MV OPEN
				DOILCstep	= ILCseqLVOPEN + 2000

			Else

				' No  MV CLOSE
				DOILCstep	= ILCseqLVOPEN + 5000

			End If

		Case	ILCseqLVOPEN + 2000

			' DP:ON ?
			If OutBuf( MAEdoPUMP ) Then

				' Yes DP:ON
				DOILCstep	= ILCseqLVOPEN + 3000

			Else

				' No  DP:OFF
				DOILCstep	= ILCseqLVOPEN + 5000

			End If

		'
		'	MV:CLOSE
		'
		Case	ILCseqLVOPEN + 3000

			' MV CLOSE
			ExDoOut( EXSdoRYE1, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqLVOPEN + 3100

		Case	ILCseqLVOPEN + 3100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqLVOPEN + 5000

			End If

		Case	ILCseqLVOPEN + 5000

			' G4:OPEN ?
			If OutBuf( EXSdoRYE3 ) Then

				' Yes G4:OPEN
				DOILCstep	= ILCseqLVOPEN + 6000

			Else

				' No  G4:CLOSE
				DOILCstep	= ILCseqLVOPEN + 8000

			End If

		Case	ILCseqLVOPEN + 6000

			' DP:ON ?
			If OutBuf( MAEdoPUMP ) Then

				' Yes DP ON
				DOILCstep	= ILCseqLVOPEN + 7000

			Else

				' No  DP OFF
				DOILCstep	= ILCseqLVOPEN + 8000

			End If

		'
		'	20201008 MV:CLOSE -> G4:CLOSEに変更
		'	G4:CLOSE
		'
		Case	ILCseqLVOPEN + 7000

			' G4:CLOSE
			ExDoOut( EXSdoRYE3, DIO_OFF )

			' 待機 3sec
			TmrDCnt1	= 30

			DOILCstep	= ILCseqLVOPEN + 7100

		Case	ILCseqLVOPEN + 7100

			If TmrDCnt1 = 0 Then

				DOILCstep	= ILCseqLVOPEN + 8000

			End If

		'
		'	LV:OPEN
		'
		Case	ILCseqLVOPEN + 8000

			' LV:OPEN
			ExDoOut( EXSdoRYE4, DIO_ON )

			' シーケンス終了
			DOILCstep	= ILCseqEND



		'*****
		'	LV OPEN->CLOSE シーケンス開始
		'*****
		Case	ILCseqLVCLOSE

			' LV:CLOSE
			ExDoOut( EXSdoRYE4, DIO_OFF )

			' シーケンス終了
			DOILCstep	= ILCseqEND


		'*****
		'	未定義ステップ
		'*****
		Case Else

			FrmLog.LogDspAdd( "", "DOILCseq() 未定義ステップ " + DOILCstep.ToString(), Color.Empty )

			DOILCstep	= ILCseqEND

		End Select

	End Sub

End Module
