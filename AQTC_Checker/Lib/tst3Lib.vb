'*****
'	Ｈｅリーク量測定処理
'
'	＜概要＞
'	ウエハ裏面圧が指定圧になるようにＰＩＤ調節計でＭＦＣ１を
'	制御させる。Ｈｅ流量が安定するのを待って安定した時の
'	Ｈｅ流量をＨｅリーク量とする
'*****

'	20201102 S_Harada
'	電源印加後1Kpa,2kPa,3kPa,4kPa,6kPaを連続で測定し
'	判定でリトライするためtst3_procを追加し、tst3検査を分離移動する


Module tst3Lib


	'
	'	ＡＩデ－タの移動平均回数
	'
	Public Const DefFlowNAvg	= 120


	'
	'	安定判断条件－１
	'	前回の移動平均と現在の移動平均の差が以下の範囲で有れば安定中と判断する
	'
	Const ANTupr		= 0.003
	Const ANTlwr		= -0.003

' 20140207 y.goto
'	Const nSA		= 30
	Const nSA		= 170


	'
	'	安定判断条件－２
	'	条件－１が以下に設定された時間続けば安定したと判断する
	'
'	Const ANTtim		= 60
' 20140207
	Const ANTtim		= 60


	'
	'	安定待ちタイムアウト時間
	'
	Const ANTtout		= 60000.0


	'
	'	ＰＩＤスタート後の測定開始時間
	'
	Const PIDWaitTim	= 3 * 60 * 100.0



	'*****
	'	Ｈｅリーク量測定
	'
	'	<return>
	'	0	正常終了
	'	-1	試験中止
	'*****
	'
	Public Function tst3				_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByVal pol		As Integer,	_
		ByVal tprs		As Double,	_
		ByVal bakp		As Double	_
	)	As Integer

		' pol;		0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ
		' schuse();	サ－モチラ－使用　0=CH1　1=CH2　2=CH1,2
		' tmp()		温度設定　(0)= CH1 (1)=CH2
		' dat;		Heｶﾞｽﾘｰｸ量格納ｴﾘｱ
		' vac;		真空状態フラグ	0 = 大気圧	1 = 真空圧
		' tprs;		真空圧試験条件
		' bakp;		吸着停止する際の裏面圧

		Dim ntst		As Integer
		Dim sts			As Integer
		Dim sdcv1		As Double
		Dim sdcv2		As Double
		Dim rtn			As Integer


		FrmLog.LogDspAdd( "", "tst3() Start", Color.Empty )

		' 戻り値初期値セット
		rtn			= 0


		DHDTest.StatusDisp( 3, 1, "    Ｈｅリ－ク量測定     " )

		FrmLog.LogDspAdd( "", "Ｈｅリーク量測定シーケンス開始", Color.Empty )


		If dt.schuse( 0 ) <> 0 And dt.schuse( 1 ) = 0 Then

			' サーモチラCH1使用
			DHDTest.StatusDisp							_
			(									_
				3, 3,								_
				convVacumStr( vac ) +						_
				"圧  ｻｰﾓﾁﾗｰ温度 CH1: " +					_
				dt.tmp( 0 ).ToString( "0.0 [℃]" ).PadLeft( 9 )			_
			)

		ElseIf dt.schuse( 0 ) = 0 And dt.schuse( 1 ) <> 0 Then

			' サーモチラCH2使用
			DHDTest.StatusDisp							_
			(									_
				3, 3,								_
				convVacumStr( vac ) +						_
				"圧  ｻｰﾓﾁﾗｰ温度 CH2: " +					_
				dt.tmp( 1 ).ToString( "0.0 [℃]" ).PadLeft( 9 )			_
			)

		Else

			' サーモチラCH1,CH2使用
			DHDTest.StatusDisp							_
			(									_
				3, 3,								_
				convVacumStr( vac ) +						_
				"圧  ｻｰﾓﾁﾗｰ温度 CH1: " +					_
				dt.tmp( 0 ).ToString( "0.0 [℃]" ).PadLeft( 9 ) +		_
				" CH2: " +							_
				dt.tmp( 1 ).ToString( "0.0 [℃]" ).PadLeft( 9 )			_
			)

		End If



		'
		'	PID運転停止
		'
		ExDio_Output( MAEdoPIDSTART, DIO_OFF )
		WaitTim( 10 )


		'
		'	MFC 強制OPEN を解除
		'
		ExDio_Output( MAEdoFOPN, DIO_OFF )
		WaitTim( 10 )


		'
		'	MFC 強制CLOSE を解除
		'
		ExDio_Output( MAEdoFCLS, DIO_OFF )
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
		'	高圧リレー接続
		'	電極ヘッドとＥＳＣ電源を接続
		'
		RyHvMode( pol, MES_ESC )
		RyHvPos( pol, MES_ESC, dt.t3.posv )
		WaitTim( 30 )

		sts			= 0

		ntst			= 0

		'
		'	ＳＤＣ電源電圧ステップ数分のル－プ
		'
		Do While dt.t3.dsiz > ntst

			'   20200716 s.harada
			FrmLog.LogDspAdd( "", "tst3 開始 " + ( ntst + 1 ).ToString + "/" + dt.t4.dsiz.ToString, Color.Empty )


			' ＳＤＣ電源ＣＨ１印加電圧
			sdcv1			= dt.t3.d( ntst ).volt1

			' ＳＤＣ電源ＣＨ２印加電圧
			sdcv2			= dt.t3.d( ntst ).volt2

			FrmLog.LogDspAdd				_
			(						_
				""					_
				,					_
				"tst3:ntst=" +				_
				ntst.ToString() +			_
				" CH1 " +				_
				sdcv1.ToString() +			_
				" CH2 " +				_
				sdcv2.ToString()			_
				,					_
				Color.Empty				_
			)

			If pol = POL_MON Then

				'
				'	モノポールの時の電圧印加表示
				'
				DHDTest.StatusDisp							_
				(									_
					9, 5,								_
					"印加位置：" +							_
					convVoltInPosToStr( dt.t3.posv ) +				_
					"　　印加電圧CH1: " +						_
					sdcv1.ToString( "0.0 [V]" ).PadLeft( 11 )	_
				)

			Else

				'
				'	ダイポールの時の電圧印加表示
				'
				DHDTest.StatusDisp							_
				(									_
					9, 5,								_
					"印加位置：" +							_
					convVoltInPosToStr( dt.t3.posv ) +				_
					"　　印加電圧CH1: " +						_
					sdcv1.ToString( "0.0 [V]" ).PadLeft( 11 ) +			_
					"　　印加電圧CH2: " +						_
					sdcv2.ToString( "0.0 [V]" ).PadLeft( 11 )			_
				)

			End If

			'
			'	チャンバ内圧力、ウエハ裏面圧が下がるのを待つ
			'
			If						_
				DHDTest.waittstcond3			_
				(					_
					"ウエハ裏面圧が下がるのを待つ",	_
					vac,				_
					dt.schuse,			_
					dt.tmp,				_
					tprs,				_
					bakp				_
				)					_
			Then

				'   20200716 s.harada
				FrmLog.LogDspAdd( "", "tst3 途中終了：DHDTest.waittstcond3 sts=" + sts.ToString, Color.Empty )

				' 中止指示
				rtn			= -1

				Exit Do

			End If

			'
			'	電極ヘッドに吸着電圧印加し目標に到達するまで待つ
			'
			If ( ESCproc( sdcv1, sdcv2, 30000.0, 7 ) ) Then

				'   20200716 s.harada
				FrmLog.LogDspAdd( "", "tst3 途中終了：ESCproc", Color.Empty )

				' 中止指示
				rtn			= -1

				Exit Do

			End If

			'	20201102 S_Harada
			'	Ｈｅリーク量測定実施
			'
			sts			= tst3_proc( vac, dt, dt.t3.d( ntst ), tprs, bakp, ntst, sdcv1, sdcv2 )

			'
			'	測定値を画面表示
			'
			DHDTest.setMesHeGas( ntst )

			'
			'	電極ヘッドの除電を行う
			'	ＳＤＣ出力電圧を徐々に下げて、ウエハ飛び上がりを小さくする
			'
			ESCstop2			_
			(				_
				sdcv1,			_
				sdcv2,			_
				3000.0,			_
				7			_
			)

			WaitTim( 300.0 )

			'	20201102 S_Harada
			If ( sts < 0 ) Then

				FrmLog.LogDspAdd( "", "tst3 途中終了：tst3_prc sts=" + sts.ToString, Color.Empty )
 
				rtn			= -1

				Exit Do


			End If

			'   20200716 s.harada
			FrmLog.LogDspAdd( "", "tst3 終了 " + (ntst + 1).ToString + "/" + dt.t4.dsiz.ToString, Color.Empty )

			ntst			+= 1

		Loop

		'
		'	20200310 y.goto トーカロ様対応
		' DOX34 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ閉
		ExDio_Output( EXSdoRYE2, DIO_OFF )
		WaitTim( 1000 )

		'
		'	高圧リレー接続ＯＦＦ
		'
		RyHvPos( pol, MES_ESC, CON_OFF )
		WaitTim( 30 )

		' clrln( 3, 3 );
		DHDTest.StatusClear( 1, 5 )

		FrmLog.LogDspAdd( "", "Ｈｅリーク量測定シーケンス終了", Color.Empty )

		FrmLog.LogDspAdd( "", "tst3() End", Color.Empty )


		Return rtn

	End Function

	'*****
	'	20201102 S_Harada
	'	Ｈｅリーク量測定を行う
	'		裏面圧を1,2,3,4,6kPaまで連続測定に変更
	'
	'	<return>
	'	0	正常終了
	'	-1	試験中止
	'*****
	'
	Public Function tst3_proc			_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByRef dat		As DTI3,	_
		ByVal tprs		As Double,	_
		ByVal bakp		As Double,	_
		ByVal ntst		As Integer,	_
		ByVal sdcv1		As Double,	_
		ByVal sdcv2		As Double	_
	)	As Integer


		Dim sts As Integer
		Dim rtn As Integer
		Dim nrty As Integer     ' リトライ
		Dim bpsel As Integer    ' 試験裏面圧選択
		Dim jdgTm1 As Double    ' 判定値１
		Dim jdgTm2 As Double    ' 判定値２
		Dim vacs As Integer
		''▼2024.05.10 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
		'Dim tmp50 As Boolean = False       '50度の測定になっているか？
		''▲2024.05.10 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）


		'判定値パラメータの設定で判定する
		jdgTm1			= dat.bs
		jdgTm2			= dat.bs2

		' 験結果初期値は判定なし
		dat.okng		= -1
		dat.okng2		= -1

		' 設定圧力 0=1.0[KPa], 1=2.0[KPa], 2=3.0[KPa], 3=4.0[KPa], 4=6.0[KPa]
		bpsel			= 0

		'
		'	裏面圧を1,2,3,4,6kPaまで連続測定に変更
		'
		Do While True

			Dim setpa As Double

			'
			'	20201124 追加 y.goto
			'	VACBproc()で　MAEdoRYFC を OFF にしていたため、2回目以降の検査でPIDの制御がMFCに伝わっていなかった
			'
			'	ＰＩＤによりＭＦＣ１を制御する
			'

			' MFC の SET PT入力を PIDの制御出力と接続
			ExDio_Output(MAEdoRYFC, DIO_ON)
			WaitTim(10)

			''▼2024.05.10 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
			''どちらかが50℃の場合にスキップを有効にする
			'If dt.schuse(0) = 1 AndAlso dt.tmp(0) = 50.0 Then
			'	tmp50 = True
			'End If
			'If dt.schuse(1) = 1 AndAlso dt.tmp(1) = 50.0 Then
			'	tmp50 = True
			'End If
			''▲2024.05.10 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
			'
			'	設定圧力 0=1.0[KPa], 1=2.0[KPa], 2=3.0[KPa], 3=4.0[KPa], 4=6.0[KPa]
			'
			Select Case bpsel
				Case 0 And dat.ptn.Contains("1")
					'1kPa
					'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
					'setpa = 1000.0
					'If dat.ptn.Contains("1") Or (Not tmp50) Then    '1kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
					If dat.ptn.Contains("1") Then    '1kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
						setpa = 1000.0
					Else
						setpa = 0.0
					End If
					'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

				Case 1
					'2kPa
					'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
					'setpa = 2000.0
					'If dat.ptn.Contains("2") Or (Not tmp50) Then    '2kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
					If dat.ptn.Contains("2") Then    '2kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
						setpa = 2000.0
					Else
						setpa = 0.0
					End If
					'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

				Case 2
					'3kPa
					'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
					'setpa = 3000.0
					'If dat.ptn.Contains("3") Or (Not tmp50) Then    '3kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
					If dat.ptn.Contains("3") Then    '3kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
						setpa = 3000.0
					Else
						setpa = 0.0
					End If
					'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

				Case 3
					'4kPa
					'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
					'setpa = 4000.0
					'If dat.ptn.Contains("4") Or (Not tmp50) Then    '4kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
					If dat.ptn.Contains("4") Then    '4kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
						setpa = 4000.0
					Else
						setpa = 0.0
					End If
					'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

				Case 4
					'6kPa
					'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
					'setpa = 6000.0
					'If dat.ptn.Contains("6") Or (Not tmp50) Then    '6kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
					If dat.ptn.Contains("6") Then    '6kPaが有効またはサーモチラーの設定が50℃でない場合は測定実施
						setpa = 6000.0
					Else
						setpa = 0.0
					End If
					'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

			End Select

			'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

			If setpa > 0.0 Then
				'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

				' PIDへ目標裏面圧力 DA値 を出力
				ExDa_Output(PIDaoRSP, cvtp2PIDset(setpa))
				WaitTim(10)

				'
				'	試験ループ (NGの場合は３回までﾘﾄﾗｲ)
				'
				nrty = 0

				Do While (3 > nrty)

					'
					'	20230110 y.goto
					'
					'	本ループ内の tst3_pid() の処理後、VACBproc()が処理される。
					'	VACBproc()の処理で MAEdoRYFC は OFF される。
					'	tst3_pid()の処理結果が判定NGの場合 本Loop先頭から処理継続するが、この時 MAEdoRYFC はOFFのまま
					'	処理してしまうBug有り。
					'	下記処理を追加する。
					'
					' MFC の SET PT入力を PIDの制御出力と接続
					ExDio_Output(MAEdoRYFC, DIO_ON)
					WaitTim(10)

					' 20201102 S_Harada リトライ回数追加
					DHDTest.StatusDisp(9, 7, "設定圧力 : " + (setpa / 1000).ToString("0.0kPa    トライ : ") + (nrty + 1).ToString + "回目")

					' 測定ループ終了フラグ・初期値セット
					rtn = -1

					'
					'	20200220 y.goto トーカロ様対応
					'	ウエハ裏面圧開放バルブ SV3(G4) OFF
					'
					ExDio_Output(EXSdoRYE3, DIO_OFF)
					WaitTim(100)

					'
					'	20201104 y.goto リトライしたとき、SV2 が閉じていたのでここで開くようにした
					'	DOX34 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ開
					'
					ExDio_Output(EXSdoRYE2, DIO_ON)
					WaitTim(100)

					'
					'	チャンバ内圧力、ウエハ裏面圧が下がるのを待つ
					'	リトライした時に圧が下がるまで待つ
					'
					If _
						DHDTest.waittstcond3 _
						(
							"ウエハ裏面圧が下がるのを待つ",
							vac,
							dt.schuse,
							dt.tmp,
							tprs,
							bakp
						) _
					Then

						'   20200716 s.harada
						FrmLog.LogDspAdd("", "tst3_proc 途中終了", Color.Empty)

						' 中止指示
						rtn = -1

						Exit Do

					End If

					'
					'	ｳｴﾊ裏面圧が一定値になる様にPID調節計を使用しHeを制御（安定で測定）
					'
					'	20201102 S_Harada AQTC対応で引数変更
					'sts			= tst3_pid( dt.t3.d( ntst ), ANTtout, 7 )
					sts = tst3_pid(dat, ANTtout, 9, bpsel)


					'
					'	20200901 追加 y.goto
					'	配管真空引き
					'
					'	20230110 y.goto この関数内で MAEdoRYFC を OFFしている!
					'
					vacs = DHDTest.VACBproc(DHDTest.TestType.HeLeak, sdcv1, sdcv2)
					If vacs <> 0 Then

						' 試験中止
						rtn = -1
						Exit Do

					End If

					'
					'	判定
					'
					'	bpsel		設定圧力 0=1.0[KPa], 1=2.0[KPa], 2=3.0[KPa], 3=4.0[KPa], 4=6.0[KPa]
					'	dat.bs		裏面圧力が1.0[KPa]の時のHe流量基準値 (0 < 設定値有りの場合)
					'	dat.bs2		裏面圧力が2.0[KPa]の時のHe流量基準値 (0 < 設定値有りの場合)
					'
					'
					If sts = 1 Then

						'
						'	Ｈｅ流量安定した
						'

						' 裏面圧力が 1.0[KPa] で、He流量の基準値が設定してる時
						If bpsel = 0 And dat.bs > 0 Then

							' He流量が基準値以下か？
							If dat.bs >= dat.cm(bpsel) Then

								' 判定ＯＫ
								dat.okng = 0

								rtn = 1

								Exit Do

							Else

								' 判定ＮＧ
								dat.okng = 1

								' 判定NG　リトライ判断
								'
								nrty += 1

								If (3 <= nrty) Then

									' ﾘﾄﾗｲｱｳﾄ
									rtn = 0 '測定終了なら-1

									Exit Do

								End If

								' 20230110 y.goto
								'
								'	判定NGの場合 Loop先頭から処理するが、この時 MAEdoRYFC はOFFのまま
								'	処理してしまうBug有り①。
								'
							End If

							' 裏面圧力が 2.0[KPa] で、He流量の基準値が設定してる時
						ElseIf bpsel = 1 And dat.bs2 > 0 Then

							If dat.bs2 >= dat.cm(bpsel) Then

								'	判定ＯＫ
								dat.okng2 = 0

								rtn = 1

								Exit Do

							Else

								'	判定ＮＧ
								dat.okng2 = 1

								' 判定NG　リトライ判断
								'
								nrty += 1

								If (3 <= nrty) Then

									' ﾘﾄﾗｲｱｳﾄ
									rtn = 0 '測定終了なら-1

									Exit Do

								End If

								' 20230110 y.goto
								'
								'	判定NGの場合 Loop先頭から処理するが、この時 MAEdoRYFC はOFFのまま
								'	処理してしまうBug有り②。
								'
							End If

						Else

							'判定なし
							rtn = 1     '測定継続

							Exit Do

						End If

					ElseIf sts = -1 Then

						'
						'	Ｈｅ流量安定しなかった
						'

						' 裏面圧力が 1.0[KPa] で、He流量基準値が設定してある時又は、
						' 裏面圧力が 2.0[KPa] で、He流量基準値が設定してある時
						If (bpsel = 0 And dat.bs > 0) Or (bpsel = 1 And dat.bs2 > 0) Then

							' リトライ判断
							'
							nrty += 1

							If (3 <= nrty) Then

								' ﾘﾄﾗｲｱｳﾄ
								rtn = 0 '測定終了なら-1

								Exit Do

							End If

							' 20230110 y.goto
							'
							'	判定NGの場合 Loop先頭から処理するが、この時 MAEdoRYFC はOFFのまま
							'	処理してしまうBug有り③。
							'
						Else

							'判定なし
							rtn = 1     '測定継続

							Exit Do

						End If

					Else

						' 中止
						rtn = 0 '測定終了なら-1

						Exit Do

					End If

				Loop

				'
				'	測定値を画面表示
				'
				DHDTest.setMesHeGas(ntst)

				If rtn <= 0 Then

					' 測定終了
					Exit Do

				End If

				'
				'	ウエハ裏面圧条件待ち処理
				'
				If DHDTest.waitwbakp _
					(
						"ウエハ裏面圧が下がるのを待つ",
						vac,
						dt.schuse,
						dt.tmp,
						tprs,
						bakp
					) Then

					'   20200716 s.harada
					FrmLog.LogDspAdd("", "tst3 途中終了：waitwbakp", Color.Empty)

					' 中止指示
					rtn = -1        '測定継続なら0

					Exit Do

				End If

				'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
			End If
			'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

			' 設定圧力 0=1.0[KPa], 1=2.0[KPa], 2=3.0[KPa], 3=4.0[KPa], 4=6.0[KPa]
			bpsel += 1

			If bpsel > 4 Then

				' 測定終了
				rtn		= 0

				Exit Do

			End If

		Loop

	'	20201102 S_Harada 追加
		DHDTest.StatusClear( 7, 1 )


		Return rtn

	End Function



	'*****
	'	ＰＩＤ運転をスタ－トさせ，Ｈｅリ－ク量が安定するまで待つ
	'			
	'	ウエハの吸着は完了している
	'
	'	<return>
	'	0 <	正常終了
	'	0	ｵﾍﾟﾚ-ﾀによる強制終了
	'	0 >	測定時間オーバー
	'*****
	'	20201102 S_Harada
	'		AQTC対応で裏面圧の設定番号を追加
	Private Function tst3_pid			_
	(						_
		ByRef dat		As DTI3,	_
		ByVal tmr		As Double,	_
		ByVal y			As Double,	_
		ByVal bpsel		As Integer	_
	)
	'Private Function tst3_pid			_
	'(						_
	'	ByRef dat		As DTI3,	_
	'	ByVal tmr		As Double,	_
	'	ByVal y			As Integer	_
	')

		' dat;
		' tmr;
		' y;

		Dim ptr			As Integer
		'Dim bptr		As Integer
		Dim rtn			As Integer
		'Dim tout		As Integer
		Dim anttm		As Integer
		Dim cnt			As Integer
		Dim raw			As UShort
		Dim flow		As Double
		Dim volt		As Double
		Dim vct			As Double
		Dim v1			As Double
		Dim v2			As Double
		Dim gmt			As Double
		Dim gpt			As Double
		Dim sa( nSA )		As Single
		Dim sdcerf		As Integer


		FrmLog.LogDspAdd( "", "tst3_pid() Start", Color.Empty )

		'
		'	SDC電源エラー発生検知フラグ・クリア
		'	SDC電源のエラー信号を検知すると1になる
		'
		sdcerf			= 0

		' 戻り値初期値セット
		rtn			= -1

		If y <> 0 Then

			' 20201102 s.harada	列を12から10に変更
			DHDTest.StatusDisp( 10, y, "＊＊＊  ＰＩＤ・Ｈｅリ－ク量制御中  ＊＊＊" )

			DHDTest.StatusDisp( 10, y + 2, "ESC･CH1電圧    :           [V]   CH2電圧    :           [V]" )

			DHDTest.StatusDisp( 10, y + 3, "真空圧(ﾊﾞﾗﾄﾛﾝ) :           [Pa]" )

			DHDTest.StatusDisp( 10, y + 4, "真空圧(ﾋﾟﾗﾆｰ)  :           [Pa]" )

			DHDTest.StatusDisp( 10, y + 5, "He流量         :           [SCCM]" )

			DHDTest.StatusDisp( 10, y + 7, "安定度 :           安定ｶｳﾝﾄ :         残り時間 :      秒" )

		End If

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

		' タイムアウト時間を設定
		SetTimDCnt( 0, tmr )

		anttm			= 0

		ptr			= ADptrR

		cnt			= 0

		Dim i			As Integer

		For i = 0 To sa.Length - 1

			sa( i )			= 0.0

		Next

		'	20201102 s.harada リトライ判断を移動
		'
		'	安定チェックル－プ(1ｸﾛｯｸは１回分のAD変換終了)
		'
		Do While ( TimerDCnt( 0 ) > 0 )

			'
			'	AD変換1ﾃﾞ-ﾀ確定するまで待つ (500msec)
			'
			If adwaitmsg( ptr ) = Keys.Escape Then

				'   20200716 s.harada
				FrmLog.LogDspAdd( "", "tst3_pid 途中終了：adwaitmsg", Color.Empty )

				rtn			= 0

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
			'	バラトロン真空計の値を取得
			'
			' 20140107 移動平均回数変更
			' raw			= aiget( GMaiPRS, nMVAVG )
			raw			= aiget( GMaiPRS, 1 )

			' ＲＡＷデ－タからＰａへ換算
			gmt			= cvtr2GM_Pa( raw )

			'
			'	ピラニー真空計の値を取得
			'

			' 20140107 移動平均回数変更
			' raw			= aiget( GPaiPRS, nMVAVG )
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
			'	MFCの流量ﾓﾆﾀ信号から、He流量の傾向を求める
			'
			vct			= calvct( cnt, flow, sa, nSA )

			' 状態表示
			If y > 0 Then

				'
				'	ＥＳＣ電源ＣＨ１電圧モニタ信号測定値を表示
				'
				DHDTest.StatusDisp( 27, y + 2, v1.ToString( "0.0" ).PadLeft( 8 ) )


				'
				'	ＥＳＣ電源ＣＨ２電圧モニタ信号測定値を表示
				'
				DHDTest.StatusDisp( 56, y + 2, v2.ToString( "0.0" ).PadLeft( 8 ) )


				'
				'	バラトロン真空計測定値を表示
				'
				DHDTest.StatusDisp( 27, y + 3, gmt.ToString( "0.0" ).PadLeft( 8 ) )


				'
				'	ピラニー真空計測定値を表示
				'
				DHDTest.StatusDisp( 27, y + 4, gpt.ToString( "0.0" ).PadLeft( 8 ) )


				'
				' ＭＦＣ１流量モニタ信号測定値を表示
				'
				DHDTest.StatusDisp( 31, y + 5, flow.ToString( "0.00" ).PadLeft( 5 ) )


				'
				'	流量変化の傾向を表示
				'
				'   20201102 s.harada  19->18に変更
				DHDTest.StatusDisp( 18, y + 7, vct.ToString( "0.000" ).PadLeft( 8 ) )


				'
				'	安定期間のカウント値を表示
				'
				DHDTest.StatusDisp( 40, y + 7, anttm.ToString( "0000" ) )


				'
				'	安定待ちの残り時間を表示
				'
				DHDTest.StatusDisp( 59, y + 7, ( TimerDCnt( 0 ) / 100.0 ).ToString( "0000" ) )


			End If

			'
			'	He流量の安定判断
			'
			If					_
			(					_
				nSA < cnt And			_
				ANTlwr <= vct And		_
				vct <= ANTupr			_
			)					_
			Then

				'
				'	He流量安定と判断
				'
				If anttm = 0 Then

					'
					'	安定期間のカウントを開始
					'
					anttm			= ANTtim

				Else

					'
					'	安定期間をカウント中
					'
					anttm			-= 1

					' 安定期間ほど経過した？
					If anttm = 0 Then

						'
						'	Yes:He流量安定と判断
						'
						rtn			= 1

						Exit Do

					End If

				End If

			Else

				'
				'	He流量は安定していない
				'
				anttm			= 0

			End If

			cnt			+= 1

		Loop

		'
		'	波形サンプリング停止
		'
		WaveSmpStop()

		' 波形サンプリングデータを保存
		'▼2024.04.19 TC Kanda （２．測定中のHe流量及びウエハ裏面圧力のログ出力追加／HEリーク量測定ファイル名が残留吸着力測定印加中とファイル名が被っているため修正）
		'SaveWaveData _
		'(
		'	DHDTest.tstNo,
		'	"C",
		'	dat.volt1,
		'	dat.volt2
		')
		SaveWaveData _
		(
			DHDTest.tstNo,
			"A" + bpsel.ToString,
			dat.volt1,
			dat.volt2
		)
		'▲2024.04.19 TC Kanda （２．測定中のHe流量及びウエハ裏面圧力のログ出力追加／HEリーク量測定ファイル名が残留吸着力測定印加中とファイル名が被っているため修正）

		'
		'	ＭＦＣ電圧とＮ２流量デ－タをセット
		'
		dat.mfcvolt( bpsel )		= volt

		'	20201102 s.harada	測定方法変更
		'dat.cm			= flow
		dat.cm( bpsel )			= flow

		'
		'	ＰＩＤ運転停止 (Ｈｅの停止)
		'
		ExDio_Output( MAEdoPIDSTART, DIO_OFF )
		WaitTim( 10 )

		If y > 0 Then

			DHDTest.StatusClear( y, 8 )

		End If

		FrmLog.LogDspAdd( "", "tst3_pid() End", Color.Empty )


		Return ( rtn )

	End Function

End Module
