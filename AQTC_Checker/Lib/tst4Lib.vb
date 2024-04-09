'*****
' 20200716 s.harada
' トーカロ選択対応で新規作成
'*****


Imports System.IO



Module tst4Lib

	'
	'	裏面圧測定安定判断条件－１
	'	前回の移動平均と現在の移動平均の差が以下の範囲で有れば安定中と判断する
	'
	dim STABupr As Double

	dim STABlwr As Double

	Const nMS		= 100
	'
	'	裏面圧測定安定判断条件－２
	'	条件－１が以下に設定された時間続けば安定したと判断する
	'
	dim STABtim As Double


	'	電圧印加中安定判断条件
	Const nSA		= 170

	Dim Imptim As Double


	'*****
	'	残留吸着力測定
	'
	'	<return>
	'	0	正常終了
	'	-1	試験中止
	'*****
	'
	Public Function	tst4				_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByVal pol		As Integer,	_
		ByVal tprs		As Double,	_
		ByVal bakp		As Double	_
	)

		' vac;		真空状態フラグ	0=大気圧 1=真空圧
		' dt;		データ格納エリア
		' pol;		0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ
		' tprs;		真空圧試験条件
		' bakp;		吸着停止する際の裏面圧


		Dim ntst		As Integer
		Dim sts			As Integer


		DHDTest.StatusDisp( 3, 1, "    残留吸着力測定    " )

		FrmLog.LogDspAdd( "", "残留吸着力測定シーケンス開始", Color.Empty )



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
		'	MFC 強制CLOSE を解除
		'
		ExDio_Output( MAEdoFCLS, DIO_OFF )

		'
		'	MFC の SET PT入力を パソコンＡＯと接続
		'
		ExDio_Output( MAEdoRYFC, DIO_OFF )

		'
		'	測定ル－プ
		'	ＥＳＣ電源への印加電圧ステップ数分繰り返す
		'
		ntst			= 0

		'
		'	測定裏面圧安定係数
		'
		STABupr			= PrmStabVct

		STABlwr			= PrmStabVct * -1

		STABtim			= PrmStabTim

		'
		'	電圧印加時間
		'
		Imptim			= PrmVoltImp

		'
		'	登録してあるテスト項目数分のループ
		'
		Do While ntst < dt.t4.dsiz

			DHDTest.StatusDisp( 29, 1, ( ntst + 1 ).ToString + "/" + dt.t4.dsiz.ToString )

			FrmLog.LogDspAdd( "", "tst4 開始 " + ( ntst + 1 ).ToString + "/" + dt.t4.dsiz.ToString, Color.Empty )

			'
			'	残留吸着力測定実施
			'
			sts			= tst4_prc( vac, dt, dt.t4.d( ntst ), pol, tprs, bakp, ntst )
			If sts = 0 Then

				' オペレータによる中断
				Exit Do

			End If

			'
			'	測定値を画面表示
			'
			DHDTest.setMesZanryu( ntst )

			FrmLog.LogDspAdd( "", "tst4 終了 " + ( ntst + 1 ).ToString + "/" + dt.t4.dsiz.ToString, Color.Empty )

			ntst			+= 1

		Loop


		'
		'	ウエハ裏面圧開放バルブON
		'
		ExDio_Output( MAEdoPRG, DIO_ON )

		'
		'	ウエハ裏面圧開放バルブ SV3(G4) ON
		'
		ExDio_Output( EXSdoRYE3, DIO_ON )

		WaitTim( 1000 )


		'
		'	電極ヘッドの除電を行う
		'
		ESCstop( 2000, 7 )


		'
		'	高圧リレー接続ＯＦＦ
		'
		RyHvPos( pol, MES_ESC, CON_OFF )

		WaitTim( 30 )


		DHDTest.StatusClear( 1, 5 )

		FrmLog.LogDspAdd( "", "残留吸着力測定シーケンス終了", Color.Empty )



		If sts < 0 Then

			Return -1

		Else

			Return 0

		End If


	End Function



	'*****
	'	残留吸着力測定を行う
	'
	'	<return>
	'	0 <	正常終了
	'	0	ｵﾍﾟﾚ-ﾀによる強制終了
	'	0 >	異常終了
	'*****
	'
	Private Function tst4_prc			_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByRef dat		As DTI4,	_
		ByVal pol		As Integer,	_
		ByVal tprs		As Double,	_
		ByVal bakp		As Double,	_
		ByVal ntst		As Integer	_
	) As Integer

		Dim rtn			As Integer
		Dim nrty		As Integer		' リトライ
		Dim jprs		As Double
		Dim sts			As Integer


		' 判定基準
		jprs			= dat.bs

		' 試験結果初期値を設定
		If jprs > 0 Then

			' 試験結果初期値はＮＧ
			dat.okng		= 1

		Else

			' 試験結果判定なし
			dat.okng		= -1

		End If


		' 測定ループ終了フラグ・初期値セット
		rtn			= 0


		'
		'	試験ループ (NGの場合は３回までﾘﾄﾗｲ)
		'
		nrty			= 0

		Do While ( 3 > nrty )

			'
			'	TMR_A : タイマセット 残留吸着測定処理全体のタイムアウト時間を設定
			'
			SetTimDCnt( 3, 900 * 100L )

			'
			'	20210308 y.goto 残留吸着測定中は RYHV1, RYHV3, RYHV15, RYHV16 は ON のまま
			'	電極ヘッドとＥＳＣ電源を接続
			'	高圧リレー接続
			'
			RyHvMode( pol, MES_ESC )
			RyHvPos( pol, MES_ESC, dt.t4.posv )
			WaitTim( 30 )

			'
			'	吸着力測定ループ
			'
			Do While True

				If pol = POL_MON Then

					' モノポールの時の電圧印加表示
					DHDTest.StatusDisp							_
					(									_
						9, 5,								_
						"印加位置：" +							_
						convVoltInPosToStr( dt.t4.posv ) +				_
						"　　印加電圧CH1: " +						_
						0.ToString( "0.0 [V]" ).PadLeft( 11 )				_
					)

				Else

					' ダイポールの時の電圧印加表示
					DHDTest.StatusDisp							_
					(									_
						9, 5,								_
						"印加位置：" +							_
						convVoltInPosToStr( dt.t4.posv ) +				_
						"　　印加電圧CH1: " +						_
						0.ToString( "0.0 [V]" ).PadLeft( 11 ) +				_
						"　　印加電圧CH2: " +						_
						0.ToString( "0.0 [V]" ).PadLeft( 11 )				_
					)

				End If

				'
				'	試験開始条件待ち３
				'	真空条件の時に、チャンバ内圧力と、ウエハ裏面圧力をチェックする
				'
				If								_
					 DHDTest.waittstcond3					_
					(							_
						"測定開始条件待ち",				_
						vac,						_
						dt.schuse,					_
						dt.tmp,						_
						tprs,						_
						bakp						_
					)							_
				Then

					' オペレータによる中断指示
					rtn			= 0

					FrmLog.LogDspAdd( "", "tst4 途中終了：印加前 DHDTest.waittstcond3 sts=" + sts.ToString, Color.Empty )


					Exit Do

				End If


				'	リトライ回数
				DHDTest.StatusDisp( 10, 8, ( nrty + 1 ).ToString + "回目" )

				'
				'	＜測定１＞　電圧印可前の残留吸着力測定実施
				'
				sts			= tst4_meas( vac, dt, dt.t4.d( ntst ), tprs, bakp, 0, ntst )

				If ( sts <= 0 ) Then

					FrmLog.LogDspAdd( "", "tst4 途中終了：印加前 tst4_meas sts=" + sts.ToString, Color.Empty )

					rtn			= sts

					Exit Do

				End If

				'
				'	電圧印加
				'
				If pol = POL_MON Then

					' モノポールの時の電圧印加表示
					DHDTest.StatusDisp							_
					(									_
						9, 5,								_
						"印加位置：" +							_
						convVoltInPosToStr( dt.t4.posv ) +				_
						"　　印加電圧CH1: " +						_
						dt.t4.d( ntst ).volt1.ToString( "0.0 [V]" ).PadLeft( 11 )	_
					)

				Else

					' ダイポールの時の電圧印加表示
					DHDTest.StatusDisp							_
					(									_
						9, 5,								_
						"印加位置：" +							_
						convVoltInPosToStr( dt.t4.posv ) +				_
						"　　印加電圧CH1: " +						_
						dt.t4.d( ntst ).volt1.ToString( "0.0 [V]" ).PadLeft( 11 ) +	_
						"　　印加電圧CH2: " +						_
						dt.t4.d( ntst ).volt2.ToString( "0.0 [V]" ).PadLeft( 11 )	_
					)

				End If

				'
				'	電極ヘッドとＥＳＣ電源を接続
				'	高圧リレー接続
				'
			'	20210308 y.goto 残留吸着測定中は RYHV1, RYHV3, RYHV15, RYHV16 は ON のまま
			'	RyHvMode( pol, MES_ESC )
			'	RyHvPos( pol, MES_ESC, dt.t4.posv )
			'	WaitTim( 30 )

				'
				'	試験開始条件待ち３
				'	真空条件の時に、チャンバ内圧力と、ウエハ裏面圧力をチェックする
				'
				If									_
					DHDTest.waittstcond3						_
					(								_
						"測定開始条件待ち",					_
						vac,							_
						dt.schuse,						_
						dt.tmp,							_
						tprs,							_
						bakp							_
					)								_
				Then

					' オペレータによる中断指示
					rtn			= 0

					FrmLog.LogDspAdd( "", "tst4 途中終了：印加後 DHDTest.waittstcond3 sts=" + sts.ToString, Color.Empty )

					Exit Do

				End If

				'
				'	電極ヘッドに吸着電圧印加し目標に到達するまで待つ
				'
				If ESCproc( dt.t4.d( ntst ).volt1, dt.t4.d( ntst ).volt2, 30000L, 7 ) Then

					' オペレータによる中断指示
					rtn			= 0

					FrmLog.LogDspAdd( "", "tst4 途中終了：印加後 ESCproc sts=" + sts.ToString, Color.Empty )

					Exit Do

				End If

				'
				'	PID運転停止
				'
				ExDio_Output( MAEdoPIDSTART, DIO_OFF )

				'
				'	MFC 強制OPEN を解除
				'
				ExDio_Output( MAEdoFOPN, DIO_OFF )

				'
				'	MFC 強制CLOSE を解除
				'
				ExDio_Output( MAEdoFCLS, DIO_OFF )

				' DOX34 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ開
				ExDio_Output( EXSdoRYE2, DIO_ON )
				WaitTim( 10 )

				'
				'	ＰＩＤによりＭＦＣ１を制御する
				'
				' MFC の SET PT入力を PIDの制御出力と接続
				ExDio_Output( MAEdoRYFC, DIO_ON )
				WaitTim( 10 )
 
				'
				'	RSP（外部入力）
				'	PIDのセットポイントはPCのアナログ出力で設定する
				'
				' PID調節計のSPをアナログ入力に切替える
				ExDio_Output( MAEdoPIDRSP, DIO_ON )
				WaitTim( 10 )

				'
				'	PID調節計のRSPの設定値をアナログ出力する
				'
				'	20201124 y.goto 裏面圧力をパラメータ化
				'
				ExDa_Output( PIDaoRSP, cvtp2PIDset( PrmBakPrs ) )
				WaitTim( 10 )

				'	リトライ回数
				DHDTest.StatusDisp( 10, 8, ( nrty + 1 ).ToString + "回目" )

				'
				'	＜測定２＞　PIDコントローラでウエハ裏面圧力を1.5[KPa]に制御して裏面圧力安定するのを待って、その時の圧力を印加中裏面圧力とする
				'
				sts			= tst4_pid( vac, dt, dt.t4.d( ntst ), tprs, bakp )

				If (sts <= 0) Then

					FrmLog.LogDspAdd( "", "tst4 途中終了：印加中  sts=" + sts.ToString, Color.Empty )

					Exit Do

				End If

				'
				'	20210118 y.goto この時点では既に吸着停止している
				'
				'	吸着を停止する
				'
				ESCstop( 2000.0, 7 )
				WaitTim( 10 )

				'
				'	PIDのRSP設定
				'	RSP設定電圧を０
				'
				AoPutV( PIDaoRSP, 0.0 )
				WaitTim( 10 )

				'
				'	ＰＩＤ・ＲＳＰ選択信号ＯＦＦ
				'	LSPを選択
				'
				ExDio_Output( MAEdoPIDRSP, DIO_OFF )
				WaitTim( 10 )

				'
				'	マスフローコントローラ１
				'

				' 流量設定信号電圧を０
				AoPutV( MFCaoSETPT1, 0.0 )
				WaitTim( 10 )

				' SET PT入力を パソコンＡＯと接続
				ExDio_Output( MAEdoRYFC, DIO_OFF )
				WaitTim( 10 )

				'
				'	高圧リレー接続ＯＦＦ
				'
			'	20210308 y.goto 残留吸着測定中は RYHV1, RYHV3, RYHV15, RYHV16 は ON のまま
			'	RyHvPos( pol, MES_ESC, CON_OFF )
			'	WaitTim( 30 )

				'
				'	＜測定３＞　印加後吸着力測定
				'

				'
				'	Ｈｅ流すまでの時間待ち
				'
				If PrmHeWait > 0 Then

					HEwaitDisp( PrmHeWait * 100L, 7 )

				End If

				'	リトライ回数
				DHDTest.StatusDisp( 10, 8, ( nrty + 1 ).ToString + "回目" )

				'
				'	＜測定３＞　電圧印加停止後の残留吸着力測定実施
				'
				sts			= tst4_meas( vac, dt, dt.t4.d( ntst ), tprs, bakp, 1, ntst )

				rtn			= sts

				Exit Do

			Loop

			' 判定有
			If 0 < rtn Then

				'
				'	正常終了：裏面圧力安定して終了した
				'

				' 基準値が登録されているか
				If 0 < jprs Then

					' 結果の判定
					If dat.pdc <= jprs Then

						'
						'	判定ＯＫ
						'
						dat.okng			= 0

						Exit Do

					End If

				Else

					' 基準値は登録されていない
					Exit Do

				End If

			ElseIf rtn = 0 Then

				' オペレータによる中断指示

				' 試験結果は ＮＧ
				dat.okng		= 1

				Exit Do

			End If

			'
			'	リトライ実施
			'
			nrty			+= 1

			If ( 3 <= nrty ) Then

				' ﾘﾄﾗｲｱｳﾄ
				rtn			= -1

				'試験結果は ＮＧ
				dat.okng		= 1

				Exit Do

			End If

		Loop 

		'
		'	20210308 y.goto
		'	残留吸着測定中は RYHV1, RYHV3, RYHV15, RYHV16 は ON のまま
		'	ここで OFF する
		'
		RyHvPos( pol, MES_ESC, CON_OFF )
		WaitTim( 10 )


	'	20201102 S_Harada 追加
		DHDTest.StatusClear( 8, 1 )

		Return rtn

	End Function


	'*****
	'	残留吸着力測定を行う
	'
	'	<return>
	'	0 <	正常終了
	'	0	ｵﾍﾟﾚ-ﾀによる強制終了
	'	0 >	異常終了
	'*****
	'
	Private Function tst4_meas			_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByRef dat		As DTI4,	_
		ByVal tprs		As Double,	_
		ByVal bakp		As Double,	_
		ByVal tsel		As Double,	_
		ByVal ntst		As Integer	_
	) As Integer

		' dat;		測定ﾃﾞｰﾀ格納ｴﾘｱ
		' tprs;		真空圧試験条件
		' tsel;		0:印加前 1:印加後

		Dim rtn			As Integer
		Dim ptr			As Integer
		Dim cnt			As Integer
		Dim raw			As UShort
		Dim vct			As Double
		Dim sa( nMS )		As Single
		Dim pa			As Double
		Dim jprs		As Double
		Dim v1			As Double
		Dim v2			As Double
		Dim flw			As Double
		Dim sts			As Integer
		Dim vacs		As Integer
		Dim nowdt		As DateTime
		Dim olddt		As DateTime

		' 判定基準
		jprs			= dat.bs

		'
		'	波形サンプリング開始
		'
		WaveSmpGo()

		rtn			= -1

		'
		'	ウエハ裏面圧開放バルブ SV3(G4) OFF
		'
		ExDio_Output( EXSdoRYE3, DIO_OFF )

		'
		' DOX34 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ開
		ExDio_Output( EXSdoRYE2, DIO_ON )
		WaitTim( 100 )

		'
		'	Ｈｅを設定値流す
		'
		ExDa_Output( MFCaoSETPT1, cvtf2MFCset( PrmHeFlow ) )
		WaitTim( 10 )

		If tsel = 0 Then

			DHDTest.StatusDisp( 10, 9, "＊＊＊  電圧印加前裏面圧測定中  ＊＊＊" )

		Else

			DHDTest.StatusDisp( 10, 9, "＊＊＊  電圧印加後裏面圧測定中  ＊＊＊" )

		End If

		DHDTest.StatusDisp( 10, 10, "ESC･CH1電圧    :           [V]   CH2電圧    :           [V]" )

		DHDTest.StatusDisp( 10, 11, "He流量         :           [CCM]" )

		DHDTest.StatusDisp( 10, 12, "真空圧(ﾊﾞﾗﾄﾛﾝ) :           [Pa]" )

		DHDTest.StatusDisp( 10, 13, "安定度 :           安定時間 :      秒    残り時間 :      秒" )

		' 安定期間のタイムアウト時間を表示
		DHDTest.StatusDisp( 40, 13, ( STABtim ).ToString( "0000" ) )

		ptr			= ADptrR

		cnt			= 0

		Dim i As Integer

		For i = 0 To sa.Length - 1

			sa( i )			= 0.0

		Next

		'
		'	裏面圧画安定するまで待つループ
		'

		' TMR_A タイムアウトチェック
		Do While TimerDCnt( 3 ) > 0

			'AD変換1ﾃﾞ-ﾀ確定するまで待つ (500msec)
			If adwaitmsg( ptr ) = Keys.Escape Then

				' オペレータによる中断
				rtn			= 0

				FrmLog.LogDspAdd( "", "tst4_prc adwaitmsg Keys=Escape", Color.Empty )

				Exit Do

			End If

			'
			'	バラトロン真空計の値を取得（ウエハ裏面圧）
			'
			raw			= aiget( GMaiPRS, 1 )

			' ＲＡＷデ－タからＰａへ換算
			pa			= cvtr2GM_Pa( raw )

			'
			'	ＥＳＣ・ＣＨ１モニタ電圧を取得
			'
			raw			= aiget( ESCaiMON1, 1 )

			' ＲＡＷデ－タから電圧へ換算
			v1			= cvtr2ESC( raw )

			'
			'	ＥＳＣ・ＣＨ２モニタ電圧を取得
			'
			raw			= aiget( ESCaiMON2, 1 )

			' ＲＡＷデ－タから電圧へ換算
			v2			= cvtr2ESC( raw )

			'
			'	ＭＦＣ流量モニタ電圧を取得
			'
			raw			= aiget( MFCaiFLW, 1 )

			' ＲＡＷデ－タから流量へ換算
			flw			= cvtr2MFCop( raw )

			'
			'	ウエハ裏面圧の傾向を求める
			'
			vct			= calvct( cnt, pa, sa, nMS )

			'
			'	ＥＳＣ電源ＣＨ１モニタ電圧の測定値を表示
			'
			DHDTest.StatusDisp( 27, 10, v1.ToString( "0.0" ).PadLeft( 8 ) )

			'
			'	ＥＳＣ電源ＣＨ２モニタ電圧の測定値を表示
			'
			DHDTest.StatusDisp( 56, 10, v2.ToString( "0.0" ).PadLeft( 8 ) )

			'
			'	ＭＦＣ１流量モニタ信号の測定値を表示
			'
			DHDTest.StatusDisp( 27, 11, flw.ToString( "0.0" ).PadLeft( 8 ) )

			'
			'	バラトロン真空計（ウエハ裏面圧）の測定値を表示
			'
			DHDTest.StatusDisp( 27, 12, pa.ToString( "0.0" ).PadLeft( 8 ) )

			'
			'	ウエハ裏面圧変化の傾向を表示
			'
			DHDTest.StatusDisp( 18, 13, vct.ToString( "0.000" ).PadLeft( 8 ) )

			'
			'	安定待ちの残り時間を表示
			'
			DHDTest.StatusDisp( 62, 13, ( TimerDCnt( 4 ) / 100.0 ).ToString( "0000" ) )

			'
			'	１秒毎に安定度をログに記録
			'
			nowdt		= DateTime.Now
			If olddt.ToString( "HHmmss" ) <> nowdt.ToString( "HHmmss" ) Then

				FrmLog.LogDspAdd( "", "tst4_meas tsel=" + tsel.ToString() + " 裏面圧=" + pa.ToString( "0.000" ) + " 安定度=" + vct.ToString( "0.000" ) + " 残り=" + ( TimerDCnt( 4 ) / 100.0 ).ToString( "0000" ), Color.Empty )

				olddt		= nowdt

			End If

			'
			'	ウエハ裏面圧が安定したら測定終了
			'
			If					_
			(					_
				nMS < cnt And			_
				STABlwr <= vct And		_
				vct <= STABupr			_
			) _
			Then

				'
				'	ウエハ裏面圧安定と判断
				'

				' TMR_B : 安定の状態が一定期間継続したかチェック
				If 0 >= TimerDCnt( 4 ) Then

					'
					'	Yes:ウエハ裏面圧安定と判断
					'
					rtn			= 1

					Exit Do

				End If

			Else

				'
				'	ウエハ裏面圧は安定していない
				'

				' TMR_B : 安定中期間タイマーリロード
				SetTimDCnt( 4, STABtim * 100L )

			End If

			cnt			+= 1

		Loop

		' 正常終了
		If 1 = rtn Then

			If tsel = 0 Then

				'
				'	＜測定データ１＞　電圧印可前のウエハ裏面圧データ保存
				'
				dat.pc			= pa

			Else

				'
				'	＜測定データ３＞　電圧印可後のウエハ裏面圧データ保存
				'
				dat.pd			= pa

				' 圧力差計算
				If dat.pd > dat.pc Then
					dat.pdc			= dat.pd - dat.pc
				Else
					dat.pdc			= dat.pc - dat.pd
				End If

			End If

		End If

		'
		'	Ｈｅを止める
		'
		ExDa_Output( MFCaoSETPT1, cvtf2MFCset( 0.0 ) )
		WaitTim( 100 )

		'
		'	20200310 y.goto トーカロ様対応
		' DOX34 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ閉
		ExDio_Output( EXSdoRYE2, DIO_OFF )
		WaitTim( 100 )

		DHDTest.StatusClear( 10, 4 )

		'
		'	波形サンプリング停止
		'
		WaveSmpStop()

		'
		'	しばらく待つ
		'
		WaitTim( 200 )

		' 波形サンプリングデータを保存
		SaveWaveData						_
		(							_
			DHDTest.tstNo,					_
			"D" + ntst.ToString(),				_
			dat.volt1,					_
			dat.volt2					_
		)

		DHDTest.StatusClear( 9, 6 )

		'
		'	配管真空引き
		'
		vacs			= DHDTest.VACBproc()
		If vacs <> 0 Then

			' オペレータによる中断
			rtn		= 0

		End If

		'
		'	ウエハ裏面圧条件待ち処理
		'
		If						_
		DHDTest.waitwbakp				_
		(						_
			"ウエハ裏面圧が下がるのを待つ",		_
			vac,					_
			dt.schuse,				_
			dt.tmp,					_
			tprs,					_
			bakp					_
		)						_
		Then
			' オペレータによる中断
			rtn		= 0
		End If

		Return rtn

	End Function

	'*****
	'	ＰＩＤ運転をスタ－トさせ，設定時間待つ
	'			
	'	<return>
	'	0 <	正常終了
	'	0	ｵﾍﾟﾚ-ﾀによる強制終了
	'	0 >	測定時間オーバー
	'*****
	Private Function tst4_pid			_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByRef dat		As DTI4,	_
		ByVal tprs		As Double,	_
		ByVal bakp		As Double	_
	)

		' dat;
		' tmr;
		' y;

		Dim ptr			As Integer
		Dim rtn			As Integer
		Dim cnt			As Integer
		Dim raw			As UShort
		Dim flow		As Double
		Dim volt		As Double
		Dim vct			As Double
		Dim v1			As Double
		Dim v2			As Double
		Dim gmt			As Double
		Dim gpt			As Double
		Dim sa( nMS )		As Single
		Dim sdcerf		As Integer
		Dim tm			As Double
		Dim nowdt		As DateTime
		Dim olddt		As DateTime

		'
		'	SDC電源エラー発生検知フラグ・クリア
		'	SDC電源のエラー信号を検知すると1になる
		'
		sdcerf			= 0

		' 戻り値初期値セット
		rtn			= 1

		DHDTest.StatusDisp( 10, 9, "＊＊＊  電圧印加中  ＊＊＊" )

		DHDTest.StatusDisp( 10, 10, "ESC･CH1電圧    :           [V]   CH2電圧    :           [V]" )

		DHDTest.StatusDisp( 10, 11, "He流量         :           [SCCM]" )

		DHDTest.StatusDisp( 10, 12, "真空圧(ﾊﾞﾗﾄﾛﾝ) :           [Pa]" )

		DHDTest.StatusDisp( 10, 13, "真空圧(ﾋﾟﾗﾆｰ)  :           [Pa]" )

		DHDTest.StatusDisp( 10, 14, "安定度 :           安定時間 :      秒    残り時間 :      秒" )

		' 安定期間のタイムアウト時間を表示
		DHDTest.StatusDisp( 40, 14, ( Imptim ).ToString( "0000" ) )

		'
		'	波形サンプリング開始
		'
		WaveSmpGo()

		WaitTim( 100 )

		'
		'	ＰＩＤ運転スタ－ト
		'
		ExDio_Output( MAEdoPIDSTART, DIO_ON )
		WaitTim( 10 )

		ptr			= ADptrR

		cnt			= 0

		Dim i			As Integer

		For i = 0 To sa.Length - 1

			sa( i )			= 0.0

		Next

		' TMR_B : 安定期間タイマーリロード
		SetTimDCnt( 4, Imptim * 100L )

		'
		'	安定チェックル－プ(1ｸﾛｯｸは１回分のAD変換終了)
		'

		' TMR_A : タイムアウトチェック
		Do While ( TimerDCnt( 3 ) > 0 )

			'
			'	AD変換1ﾃﾞ-ﾀ確定するまで待つ (500msec)
			'
			If adwaitmsg( ptr ) = Keys.Escape Then

				' オペレータによる中断指示
				rtn			= 0

				'   20200716 s.harada
				FrmLog.LogDspAdd( "", "tst3_pid 途中終了：adwaitmsg", Color.Empty )

				Exit Do

			End If

			' ESC電源ステータス信号チェック
			If sdcerf = 0 Then

				If ESCchkSts() <> 0 Then

					' SDC電源のエラーを検知した
					sdcerf				= 1

				End If

			End If

			'
			'	ＥＳＣ・ＣＨ１モニタ電圧を取得
			'
			raw			= aiget( ESCaiMON1, 1 )

			' ＲＡＷデ－タから電圧へ換算
			v1			= cvtr2ESC( raw )

			'
			'	ＥＳＣ・ＣＨ２モニタ電圧を取得
			'
			raw			= aiget( ESCaiMON2, 1 )

			' ＲＡＷデ－タから電圧へ換算
			v2			= cvtr2ESC( raw )

			'
			'	バラトロン真空計の値を取得（ウエハ裏面圧）
			'
			raw			= aiget( GMaiPRS, 1 )

			' ＲＡＷデ－タからＰａへ換算
			gmt			= cvtr2GM_Pa( raw )

			'
			'	ピラニー真空計の値を取得（チャンバ圧）
			'
			raw			= aiget( GPaiPRS, 1 )

			' ＲＡＷデ－タからＰａへ換算
			gpt			= cvtr2GP_Pa( raw )

			'
			'	現在のＭＦＣの流量デ－タを取得
			'
			raw			= aiget( MFCaiFLW, DefFlowNAvg )

			' ＲＡＷデ－タから電圧へ換算
			volt			= anar2v( raw )

			' ＲＡＷデ－タからN2流量へ換算
			flow			= cvtr2MFCop( raw )

			'
			'	ウエハ裏面圧の傾向を求める
			'
			vct			= calvct( cnt, gmt, sa, nMS )

			'
			'	ＥＳＣ電源ＣＨ１電圧モニタ信号測定値を表示
			'
			DHDTest.StatusDisp( 25, 10, v1.ToString( "0.0" ).PadLeft( 8 ) )

			'
			'	ＥＳＣ電源ＣＨ２電圧モニタ信号測定値を表示
			'
			DHDTest.StatusDisp( 54, 10, v2.ToString( "0.0" ).PadLeft( 8 ) )

			'
			' ＭＦＣ１流量モニタ信号測定値を表示
			'
			DHDTest.StatusDisp( 29, 11, flow.ToString( "0.00" ).PadLeft( 5 ) )

			'
			'	バラトロン真空計測定値を表示
			'
			DHDTest.StatusDisp( 25, 12, gmt.ToString( "0.0" ).PadLeft( 8 ) )

			'
			'	ピラニー真空計測定値を表示
			'
			DHDTest.StatusDisp( 25, 13, gpt.ToString( "0.0" ).PadLeft( 8 ) )

			'
			'	流量変化の傾向を表示
			'
			DHDTest.StatusDisp( 17, 14, vct.ToString( "0.000" ).PadLeft( 8 ) )

			'
			'	安定待ちの残り時間を表示
			'
			DHDTest.StatusDisp( 62, 14, ( TimerDCnt( 4 ) / 100.0 ).ToString( "0000" ) )

			'
			'	１秒毎に安定度をログに記録
			'
			nowdt		= DateTime.Now
			If olddt.ToString( "HHmmss" ) <> nowdt.ToString( "HHmmss" ) Then

				FrmLog.LogDspAdd( "", "tst4_pid 裏面圧=" + gmt.ToString( "0.000" ) + " 安定度=" + vct.ToString( "0.000" ) + " 残り=" + ( TimerDCnt( 4 ) / 100.0 ).ToString( "0000" ), Color.Empty )

				olddt		= nowdt

			End If

		'	20201124 y.goto 20201120木村さんメールの指示により安定は見ない
		'
		'	'
		'	'	ウエハ裏面圧が安定したら測定終了
		'	'
		'	If					_
		'	(					_
		'		nMS < cnt And			_
		'		STABlwr <= vct And		_
		'		vct <= STABupr			_
		'	) _
		'	Then
		'
		'		'
		'		'	ウエハ裏面圧安定と判断
		'		'
		'
		'		' TMR_B : 安定期間ほど経過した？
		'		If 0 >= TimerDCnt( 4 ) Then
		'
		'			'
		'			'	Yes:ウエハ裏面圧安定と判断
		'			'
		'			rtn			= 1
		'
		'			Exit Do
		'
		'		End If
		'
		'	Else
		'
		'		'
		'		'	ウエハ裏面圧は安定していない
		'		'
		'
		'		' TMR_B : 安定期間タイマーリロード
		'		SetTimDCnt( 4, Imptim * 100L )
		'
		'	End If

			' TMR_B : 電圧印加期間ほど経過した？
			If 0 >= TimerDCnt( 4 ) Then

				'
				'	Yes:電圧印加終了
				'
				rtn			= 1
				Exit Do

			End If

			cnt			+= 1

		Loop

		'
		'	＜測定データ２＞　電圧印可中のウエハ裏面圧データ保存
		'
		dat.pe			= gmt

		'
		'	波形サンプリング停止
		'
		WaveSmpStop()

		' 波形サンプリングデータを保存
		SaveWaveData					_
		(						_
			DHDtest.tstNo,				_
			"C",					_
			dat.volt1,				_
			dat.volt2				_
		)

		'
		'	ＰＩＤ運転停止 (Ｈｅの停止)
		'
		ExDio_Output( MAEdoPIDSTART, DIO_OFF )

		DHDTest.StatusClear( 9, 6 )

		'
		'	20210118 y.goto
		'		木村さん20210115メールにより下記処理の中で裏面圧が
		'		30Pa以下になるとESC電源を停止させる処理を追加した。
		'
		'	20201207 y.goto 残留吸着試験の時の配管真空引きは専用処理に変更
		'	配管真空引き
		'
		If DHDTest.VACBT4proc() <> 0 Then

			' オペレータによる中断指示
			rtn		= 0

		End If

		'
		'	20201207 y.goto
		'	残留吸着試験では配管圧が指定圧未満なら下記処理はスキップする
		'
		If bakp < FrmGraph.MesBkp Then

			'
			'	ウエハ裏面圧条件待ち処理
			'
			If						_
			DHDTest.waitwbakp				_
			(						_
				"ウエハ裏面圧が下がるのを待つ",		_
				vac,					_
				dt.schuse,				_
				dt.tmp,					_
				tprs,					_
				bakp					_
			)						_
			Then
				' オペレータによる中断指示
				rtn		= 0
			End If

		Else

			FrmLog.LogDspAdd( "", "tst4_pid skip waitwbakp() 裏面圧指定値 " + bakp.ToString() + " : 裏面圧現在値 " + FrmGraph.MesBkp.ToString(), Color.Empty )

		End If

		Return ( rtn )

	End Function


	'/*****
	'	ヘリウム流入までの待ち表示
	'	<return>
	'	0	正常終了
	'*****/
	Private Function HEwaitDisp			_
	(						_
		ByVal tmr		As Double,	_
		ByVal y			As Integer	_
	) As Integer

		' tmr;		待ち時間
		' y;		状態表示開始行
		'		0	表示しない

		Dim okf			As Integer



		If y <> 0 Then

			DHDTest.StatusDisp( 12, y, "＊＊＊  Ｈｅ流入開始待ち  ＊＊＊" )

		End If


		'　待ちループ
	'
	'	20210201 y.goto
	'	電圧印可停止時にスタートさせる
	'	タイマー数を 5-> 10 に増やし タイマー６をこれ専用とする
	'	※タイマー６はESC電圧印可時に既にスタートしている
	'
	'	SetTimDCnt( 6, tmr )

		okf			= 0



		Do While TimerDCnt( 6 ) > 0

			'
			'	しばらく待つ
			'
			WaitTim( 50 )


			'
			'	状態表示
			'
			If y <> 0 Then


				DHDTest.StatusDisp							_
				(									_
					8, y + 2,							_
					"残り時間:" +							_
					( TimerDCnt( 6 ) / 100.0 ).ToString( "0秒" ).PadLeft( 5 )	_
				)


			End If



			Application.DoEvents()

		Loop


		If y <> 0 Then

			DHDTest.StatusClear( y, 1 )

			DHDTest.StatusClear( y + 2, 1 )

		End If



		Return okf

	End Function


End Module
