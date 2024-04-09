Module tst1Lib


	'*****
	'	絶縁抵抗試験処理
	'*****
	Public Function tst1				_
	(						_
		ByRef dt		As DTREC,	_
		ByVal pol		As Integer,	_
		ByVal vac		As Integer,	_
		ByVal tprs		As Double	_
	)

		' pol;		0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ
		' vac;		真空状態フラグ	0=大気圧 1=真空圧
		' tprs		測定条件・チャンバ内圧力

		Dim npos		As Integer
		Dim ntst		As Integer
		Dim sts			As Integer


		FrmLog.LogDspAdd( "", "tst1() Start", Color.Empty )

		' 戻り値初期値セット
		sts			= 0

		'
		'	画面表示
		'
		DHDTest.StatusDisp( 3, 1, "    絶縁・耐圧試験    " )

		FrmLog.LogDspAdd( "", "絶縁抵抗測定シーケンス開始", Color.Empty )


		If dt.schuse( 0 ) <> 0 And dt.schuse( 1 ) = 0 Then

			' サーモチラCH1使用
			DHDTest.StatusDisp						_
			(								_
				3, 3,							_
				convVacumStr( vac ) +					_
				"圧  ｻｰﾓﾁﾗｰ温度 CH1: " +				_
				dt.tmp( 0 ).ToString( "0.0 [℃]" ).PadLeft( 9 )		_
			)

		ElseIf dt.schuse( 0 ) = 0 And dt.schuse( 1 ) <> 0 Then

			' サーモチラCH2使用
			DHDTest.StatusDisp						_
			(								_
				3, 3,							_
				convVacumStr( vac ) +					_
				"圧  ｻｰﾓﾁﾗｰ温度 CH2: " +				_
				dt.tmp( 1 ).ToString( "0.0 [℃]" ).PadLeft( 9 )		_
			)

		Else

			' サーモチラCH1,CH2使用
			DHDTest.StatusDisp						_
			(								_
				3, 3,							_
				convVacumStr( vac ) +					_
				"圧  ｻｰﾓﾁﾗｰ温度 CH1: " +				_
				dt.tmp( 0 ).ToString( "0.0 [℃]" ).PadLeft( 9 ) +	_
				" CH2: " +						_
				dt.tmp( 1 ).ToString( "0.0 [℃]" ).PadLeft( 9 )		_
			)

		End If


		'	※ 20200901 本バルブはトーカロ様では未装着
		'	ウエハ裏面圧開放バルブON
		'
		ExDio_Output( MAEdoPRG, DIO_ON )

		'
		'	20200220 y.goto トーカロ様対応
		'	ウエハ裏面圧開放バルブ SV3(G4) ON
		'
	'
	'	20200901 y.goto
	'	MB起動中は G4 閉
	'	ExDio_Output( EXSdoRYE3, DIO_ON )


		'
		'	ESC･CH1,CH2外部起動信号 OFF
		'
		ExDio_Output( ESCdoSTART1, DIO_OFF )

		ExDio_Output( ESCdoSTART2, DIO_OFF )


		'
		'	PID運転停止
		'
		ExDio_Output( MAEdoPIDSTART, DIO_OFF )


		'
		'	MFC1 SET PTに接続されているﾊﾟｿｺﾝAO出力を0Vに設定
		'
		AoPutV( MFCaoSETPT1, 0.0 )

		WaitTim( 30 )


		'
		'	MFC 強制OPEN を解除
		'
		ExDio_Output( MAEdoFOPN, DIO_OFF )


		'
		'	MFC 強制CLOSE 実行
		'
		ExDio_Output( MAEdoFCLS, DIO_ON )


		'
		'	MFC の SET PT入力を パソコンＡＯと接続
		'
		ExDio_Output( MAEdoRYFC, DIO_OFF )


		'
		'	マスフローコントローラ２
		'	※2014-01-09 現時点でMFC2は未使用(未接続)
		'
		AoPutV( MFCaoSETPT2, 0.0 )


		'
		'	電極ヘッドと絶縁抵抗計を接続
		'	ﾓﾉﾎﾟｰﾙ､ﾀﾞｲﾎﾟｰﾙともに高耐圧ﾘﾚｰRY1,RY2はOFF
		'
		RyHvMode( pol, MES_IOS )


		npos			= 0

		'
		'	各種接続での測定ループ（試験設定による）
		'
		'	1) 電極(内)～(外)での測定
		'	2) ウエハ～電極(内)での測定
		'	3) ウエハ～電極(外)での測定
		'	|		|
		'
		Do While ( npos < dt.t1siz )

			FrmLog.LogDspAdd( "", "tst1:npos=" + npos.ToString(), Color.Empty )

			'
			'	高圧リレー接続
			'
			RyHvPos( pol, MES_IOS, dt.t1( npos ).posv )

			WaitTim( 30 )


			'
			'	測定電圧ステップのループ（試験設定による）
			'
			'	1) 300Vでの絶縁抵抗測定
			'	2) 400Vでの絶縁抵抗測定
			'	3) 500Vでの絶縁抵抗測定
			'	|	|
			'
			ntst			= 0

			Do While ( ntst < dt.t1( npos ).dsiz )

				FrmLog.LogDspAdd									_
				(											_
					"",										_
					"tst1:ntst=" +									_
					ntst.ToString() +								_
					dt.t1( npos ).d( ntst ).volt.ToString( " 0.0 [V]" ).PadLeft( 11 ),		_
					Color.Empty									_
				)

				DHDTest.StatusDisp					_
				(							_
					29, 1,						_
					( npos + 1 ).ToString +				_
					"/" +						_
					dt.t1siz.ToString +				_
					"-" +						_
					( ntst + 1 ).ToString +				_
					"/" +						_
					dt.t1( npos ).dsiz.ToString			_
				)


				'電圧印加位置を接続
				DHDTest.StatusDisp							_
				(									_
					9, 5,								_
					"印加位置：" +							_
					convVoltInPosToStr( dt.t1( npos ).posv ) +			_
					"　　印加電圧: " +						_
					dt.t1( npos ).d( ntst ).volt.ToString( "0.0 [V]" ).PadLeft( 11 )	_
				)


				'
				'	絶縁抵抗測定実施
				'	
				sts			= tst1_prc( dt.t1( npos ).d( ntst ) )

				If sts < 0 Then

					Exit Do

				End If


				'
				'	測定値を画面表示
				'
				DHDTest.setMesTaikiData( npos, ntst )


				ntst			+= 1


			Loop


			' If ntst < dt.t1(npos).dsiz Then
			If sts < 0 Then

				Exit Do

			End If

			npos			+= 1


		Loop


		'
		'	高圧リレー接続ＯＦＦ
		'
		RyHvPos( pol, MES_IOS, CON_OFF )


		'
		'	MFC 強制CLOSE 解除
		'
		ExDio_Output( MAEdoFCLS, DIO_OFF )

		' clrln( 3, 3 );
		DHDTest.StatusClear( 1, 5 )

		FrmLog.LogDspAdd( "", "絶縁抵抗測定シーケンス終了", Color.Empty )


		FrmLog.LogDspAdd( "", "tst1() End", Color.Empty )

		' If ntst < dt.t1siz Then
		'	Return -1
		' Else
		'	Return 0
		' End If



		Return sts

	End Function



	'*****
	'	絶縁抵抗測定実施
	'	Ｒ３８４０Ａの制御（ＧＰＩＢによる）
	'
	'	<return>
	'	0	正常終了
	'	< 0	中止
	'*****
	Private Function tst1_prc			_
	(						_
		ByRef dat		As DTI1		_
	)	As Integer

		'	dat;		測定デ－タ格納エリア

		Dim rtn			As Integer
		Dim bf			As String


		FrmLog.LogDspAdd( "", "tst1_proc() Start", Color.Empty )

		'
		'	リトライループ
		'
		Do

			rtn			= -1

			Do


				'
				'	R3840A･V-SOURCEの設定
				'

				' コマンド文字列作成
				bf		= "PVS" + dat.volt.ToString( "0.00" )

				' コマンドをGPIB送信
				If ( R3840Asd( bf, 100 ) ) < 0 Then

					' 送信失敗
					Exit Do

				End If


				'
				'	R3840A･[AUTO]ｵｰﾄﾚﾝｼﾞ
				'

				' コマンドをGPIB送信
				If ( R3840Asd( "R0", 100 ) ) < 0 Then

					Exit Do

				End If


				'
				'	R3840A･ﾃﾞｰﾀﾍｯﾀﾞOFF
				'

				' コマンドをGPIB送信
				If ( R3840Asd( "OM1", 100 ) ) < 0 Then

					Exit Do

				End If


				'
				'	R3840A･[OPERATE] ｵﾍﾟﾚｰﾄ
				'
				If ( R3840Asd( "OT1", 100 ) ) < 0 Then

					Exit Do

				End If


				'
				'	R3840A･[CHARGE] ﾁｬｰｼﾞ
				'
				If ( R3840Asd( "MD1", 100 ) ) < 0 Then

					Exit Do

				End If


				'
				'	10秒待ち
				'
				WaitTim( 1000 )



				'
				'	R3840A･[MEASURE] 測定
				'
				If ( R3840Asd( "MD0", 200 ) ) < 0 Then

					Exit Do

				End If


				'
				'	R3840A･[RUN] ｻﾝﾌﾟﾘﾝｸﾞ開始
				'
				If ( R3840Asd( "MO0", 200 ) ) < 0 Then

					Exit Do

				End If


				'
				'	データ安定待ち
				'
				If ( R3840Await( 500, 7 ) ) < 0 Then

					Exit Do

				End If


				'
				'	電流値、抵抗値を測定
				'
				If ( R3840Ames( dat, 7, 3 ) ) < 0 Then

					Exit Do

				End If


				rtn			= 0

			Loop While False


			'
			'	R3840A･[OPERATE] ｽﾀﾝﾊﾞｲ
			'
			R3840Asd( "OT0", 200 )


			'
			'	R3840A･[DISCHARGE] ﾃﾞｨｽﾁｬｰｼﾞ
			'
			R3840Asd( "MD2", 200 )


			If rtn = 0 Then

				Exit Do

			End If


			'
			'	リトライ確認
			'
			If ( flipbz1r( OPIPyn, "R3840Aによる測定は失敗しました。ﾘﾄﾗｲしますか？" ) <> DialogResult.Yes ) Then

				Exit Do

			End If


		Loop While True

		FrmLog.LogDspAdd( "", "tst1_proc() End", Color.Empty )


		Return rtn

	End Function



	'*****
	'	Ｒ３８４０Ａによる電流値、抵抗値測定
	'
	'	<return>
	'	0	正常終了
	'	!0	異常終了
	Private Function R3840Await				_
	(							_
		ByVal tmr		As Double,		_
		ByVal y			As Integer		_
	)

		' tmr;
		' y;

		Dim rtn			As Integer



		' mvatr( 12, y, ATR_C7|ATR_RVS|ATR_BLK );
		' printf( "＊＊＊  Ｒ３８４０Ａ測定データ安定待ち  ＊＊＊" );

		DHDTest.StatusDisp( 12, y, "＊＊＊  Ｒ３８４０Ａ測定データ安定待ち  ＊＊＊" )

		' tmcatr( ATR_C7 );


		rtn			= 0


		' TimerDCnt(0) = tmr
		SetTimDCnt( 0, tmr )


		Do While TimerDCnt( 0 ) > 0

			' mvatr( 28, y + 2, ATR_C8 );
			' printf( "残り %04ld 秒", TIMDCNT( 0 ) / 100 )

			DHDTest.StatusDisp						_
			(								_
				28, y + 2,						_
				"残り " +						_
				( TimerDCnt( 0 ) / 100 ).ToString( "0000 秒" )		_
			)



			If StopFlag = True Then

				If ( flipbz1r( OPIPyn, "処理を中止しますか？" ) = DialogResult.Yes ) Then

					rtn			= -1

					Exit Do

				End If

			End If


			Application.DoEvents()


		Loop


		' clrln( y, 3 );
		DHDTest.StatusClear( y, 3 )


		Return ( rtn )

	End Function



	'*****
	'	Ｒ３８４０Ａによる電流値、抵抗値測定
	'
	'	<return>
	'	0	正常終了
	'	!0	異常終了
	'*****
	Private Function R3840Ames			_
	(						_
		ByRef dat		As DTI1,	_
		ByVal y			As Integer,	_
		ByVal navg		As Integer	_
	)	As Integer


		' dat;		測定デ－タ格納エリア
		' y;
		' navg;      平均回数

		Dim n			As Integer
		Dim asum		As Double
		Dim osum		As Double
		Dim vsum		As Double
		Dim nav			As Double



		DHDTest.StatusDisp( 12, y, "＊＊＊  Ｒ３８４０Ａデータ測定中  ＊＊＊" )

		DHDTest.StatusDisp( 20, y + 2, "電流値 : ")

		DHDTest.StatusDisp( 20, y + 3, "抵抗値 :" )

		DHDTest.StatusDisp( 20, y + 4, "真空度 :" )


		vsum			= 0.0

		asum			= 0.0

		osum			= 0.0


		For n = 0 To navg - 1

			DHDTest.StatusDisp				_
			(						_
				55, y,					_
				( n + 1 ).ToString( 0 ) +		_
				"/" +					_
				navg.ToString( "00" )			_
			)


			'
			'	R3840A･電流測定
			'
			If ( R3840Asd( "RI0", 10 ) ) < 0 Then

				Exit For

			End If


			If  ( WaitTimKey( 10 ) = Keys.Escape ) Then

				Exit For

			End If



			'
			'	R3840A･電流値読み取り
			'
			If ( ExGpib_rd( GPIBadR3840A ) ) < 0 Then

				Exit For

			End If


			asum			+= CDbl( GPrs )


			If ( WaitTimKey( 10 ) = Keys.Escape ) Then

				Exit For

			End If


			'
			'	R3840A･抵抗測定
			'
			If ( R3840Asd( "RI1", 10 ) ) < 0 Then

				Exit For

			End If


			If (　WaitTimKey( 10 ) = Keys.Escape ) Then

				Exit For

			End If


			'
			'	R3840A･抵抗値読み取り
			'
			If ( ExGpib_rd( GPIBadR3840A ) ) < 0 Then

				Exit For

			End If


			osum			+= CDbl( GPrs )


			If ( WaitTimKey( 10 ) = Keys.Escape ) Then

				Exit For

			End If


			'
			'	チャンバー内の真空度取得 (Pa)
			'
			vsum			+= cvtr2GP_Pa(aiget( GPaiPRS, 1 ) )



			' 電流値を表示
			DHDTest.StatusDisp							_
			(									_
				29, y + 2,							_
				( asum / ( n + 1 ) ).ToString( "0.0" ).PadLeft( 8 )		_
			)

			' 抵抗値を表示
			DHDTest.StatusDisp							_
			(									_
				29, y + 3,							_
				( osum / ( n + 1 ) ).ToString( "0.0" ).PadLeft( 8 )		_
			)

			' チャンバー内圧力を表示
			DHDTest.StatusDisp							_
			(									_
				29, y + 4,							_
				( vsum / ( n + 1 ) ).ToString( "0.0" ).PadLeft( 8 )		_
			)


		Next


		'
		'	データ記録
		'
		nav			= CDbl( navg )

		dat.amp			= asum / nav

		dat.om			= osum / nav

		dat.vq			= vsum / nav



		' clrln( y, 5 );
		DHDTest.StatusClear( y, 5 )


		If n < navg - 1 Then

			Return -1

		Else

			Return 0

		End If


	End Function



	'*****
	'	Ｒ３８４０Ａに指定電文を送信する
	'
	'	<return>
	'	0	正常終了
	'	!0	異常終了
	'*****
	Private Function R3840Asd			_
	(						_
		ByVal cmd		As String,	_
		ByVal tmr		As Double	_
	)	As Integer

		' char		*cmd;
		' unsigned long	tmr;

		Dim rtn			As Integer



		DHDTest.StatusDisp( 2, MIPy, "GPIB->" + cmd )


		rtn			= -1


		Do

			' 指定コマンドを送信
			If ( ExGpib_sd( GPIBadR3840A, cmd ) ) < 0 Then

				Exit Do

			End If


			' 指定時間待つ
			If ( WaitTimKey( tmr ) = Keys.Escape ) Then

				Exit Do

			End If

			rtn			= 0

		Loop While False


		' clrln( MIPy, 1 );
		DHDTest.StatusClear( MIPy, 1 )



		Return ( rtn )

	End Function


End Module
