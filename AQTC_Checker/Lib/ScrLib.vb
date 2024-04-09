

Module ScrLib



	'*****
	'	サ－モチラ－を遠隔操作モードに設定
	'*****
	Public Sub SCRextCTL()


		'
		'	サーモチラーをﾘﾓｰﾄ運転ﾓｰﾄﾞにする
		'	サーモチラー･ﾘﾓｰﾄ運転ﾓｰﾄﾞ切替信号のパルス信号を作成
		'
		ExDio_Output( MAEdoSRMT, DIO_ON )

		WaitTim( 70 )

		ExDio_Output( MAEdoSRMT, DIO_OFF )



		' しばらく待つ
		WaitTim( 100 )


	End Sub




	'/*****
	'	サ－モチラ－の状態を調べる
	'
	'	ch: 0=CH1 1=CH2
	'	<return>
	'	0	正常
	'	< 0	異常
	'*****/
	Public Function SCRchk				_
	(						_
		ByVal ch		As Integer	_
	)	As Integer
	


		If ch = 0 Then

			' ｻ-ｷｭﾚ-ﾀ･トラブル信号チェック	
			If InBuf( SCRdiTROUBLE1 ) = DIO_OFF Then

				syserr( "CH1ｻｰﾓﾁﾗｰﾄﾗﾌﾞﾙ信号がOFF" )

			' 201420225 トラブル信号がONの時、温度が設定されなかったのでこの信号は無視する
			'	Return -1

			End If

		ElseIf ch = 1 Then

			' ｻ-ｷｭﾚ-ﾀ･トラブル信号チェック
			If InBuf( SCRdiTROUBLE2 ) = DIO_OFF Then

				syserr( "CH2ｻｰﾓﾁﾗｰﾄﾗﾌﾞﾙ信号がOFF" )

			' 201420225 トラブル信号がONの時、温度が設定されなかったのでこの信号は無視する
			'	Return -1

			End If

		End If


		Return 0

	End Function



	'/*****
	'	サ－モチラ－の温度設定処理
	'
	'	ch: 0=CH1 1=CH2
	'	tmp: 設定温度
	'
	'	<return>
	'	サーモチラーの制御方向
	'	+1	温める方向
	'	-1	冷やす方向
	'	0	サーモチラー異常
	'*****/
	Public Function SCRset				_
	(						_
		ByVal ch		As Integer,	_
		ByVal tmp		As Double	_
	)	As Integer

		Dim ctmp		As Double
		Dim dir			As Integer



		' サ－モチラ－の状態を調べる
		If SCRchk( ch ) Then

		' 201420225 トラブル信号がONの時、温度が設定されなかったのでこの信号は無視する
		'	Return 0

		End If


		' サ－モチラ－の現在の温度を調べる
		If ch = 0 Then

			ctmp			= cvtr2SCR( aiget( SCRaiTMP1, 10 ) )

		ElseIf ch = 1 Then

			ctmp			= cvtr2SCR( aiget( SCRaiTMP2, 10 ) )

		End If


		' サーモチラー外部温度設定信号出力
		Dim wk			As Double

		If ch = 0 Then

			ExDa_Output( SCRaoREMOTE1, cvtt2SCR( tmp, wk ) )

		Else

			ExDa_Output( SCRaoREMOTE2, cvtt2SCR( tmp, wk ) )

		End If


		'
		'	サ－モチラ－の温度制御方向を判定
		'
		If ctmp > tmp Then

			dir			= -1

		Else

			dir			= 1

		End If



		Return dir

	End Function



	'*****
	'	サ－モチラ－の温度設定
	'*****
	Public Function SCRSetTmp			_
	(						_
		ByVal schchg		As Integer,	_
		ByVal schuse()		As Integer,	_
		ByVal tmp()		As Double	_
	)	As Integer

		Dim sts			As Integer = 0
	


		' サーモチラー配管接続変更有無チェック
		If schchg Then

			'
			'	サーモチラー配管接続変更有り
			'

			'
			'	サーモチラーＣＨ１，ＣＨ２とも停止させる
			'

			' サーモチラー･CH1 運転開始/停止信号を OFF
			ExDio_Output( MAEdoS1RUN, DIO_OFF )

			' サーモチラー･CH2 運転開始/停止信号を OFF
			ExDio_Output( MAEdoS2RUN, DIO_OFF )

			'
			'	配管接続変更メッセージを出力してオペレータ入力を待つ
			'
			flipbz1r( OPIPok, "サーモチラーの配管接続変更して下さい" )

			'
			'	配管接続変更メッセージを出力してオペレータ入力を待つ
			'
			flipprc( OPIPok, "サーモチラーの運転を開始します" )

		End If

		'
		'	サーモチラーの使用ＣＨの温度設定を実施
		'
		If schuse( 0 ) Then

			'
			'	サーモチラーＣＨ１使用する
			'	サ－モチラ－ＣＨ１の温度設定
			'
			SCRset( 0, tmp( 0 ) )

		End If


		If schuse( 1 ) Then

			'
			'	サーモチラーＣＨ２使用する
			'	サ－モチラ－ＣＨ２の温度設定
			'
			SCRset( 1, tmp( 1 ) )

		End If




		'
		'	サーモチラーが温度設定値を認識するのを待つ
		'
		WaitTim( 100 )



		'
		'	サーモチラー使用ＣＨの運転を開始する
		'
		If schuse( 0 ) Then

			' サーモチラー･CH1 運転開始/停止信号を ON
			ExDio_Output( MAEdoS1RUN, DIO_ON )

		End If

		If schuse( 1 ) Then

			' サーモチラー･CH2 運転開始/停止信号を ON
			ExDio_Output( MAEdoS2RUN, DIO_ON )

		End If



		Return sts

	End Function



End Module
