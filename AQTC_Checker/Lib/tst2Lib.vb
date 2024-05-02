Module tst2Lib

	' 20201102 AQTC対応で変更

	' バラトロン真空計デ－タの移動平均回数 （未使用）
	Const nMVAVG		= 5

	' ウエハ剥離判断
	' バラトロン真空計の値が今までの最大値より低い状態が
	' 以下に設定されたクロック程続いたら剥離と判断する
	Const RMVclk		= 10



	'*****
	'	ウエハ吸着力測定
	'
	'	<return>
	'	0	正常終了
	'	-1	試験中止
	'*****
	'
	'	20201102 s.harada
	'	測定時間上限を追加
	'
	Public Function					_
	tst2						_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByVal pol		As Integer,	_
		ByVal tprs		As Double,	_
		ByVal bprs		As Double,	_
		ByVal bakp		As Double,	_
		ByVal maxtm		As Double	_
	)
	'tst2						_
	'(						_
	'	ByVal vac		As Integer,	_
	'	ByRef dt		As DTREC,	_
	'	ByVal pol		As Integer,	_
	'	ByVal tprs		As Double,	_
	'	ByVal bprs		As Double,	_
	'	ByVal bakp		As Double	_
	')

		' pol;		0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ
		' schuse();	サ－モチラ－使用　0=CH1　1=CH2　2=CH1,2
		' tmp()		温度設定　(0)= CH1 (1)=CH2
		' dat;		データ格納エリア
		' tmax;		試験回数
		' vac;		真空状態フラグ	0=大気圧 1=真空圧
		' tprs;		真空圧試験条件
		' bprs;		ｳｴﾊ裏面圧力ﾘﾐｯﾄ値
		'		この圧まで検査を行う
		' bakp;		吸着停止する際の裏面圧
		' maxtm;	測定時間の上限

		Dim ntst		As Integer
		Dim sts			As Integer

		FrmLog.LogDspAdd( "", "tst2() Start", Color.Empty )

		DHDTest.StatusDisp( 3, 1, "    ウエハ吸着力測定    " )

		FrmLog.LogDspAdd( "", "ウエハ吸着力測定シーケンス開始", Color.Empty )




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
				convVacumStr(vac) +					_
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
		'	マスフローコントローラ２
		'	※2014-01-09 現時点でMFC2は未使用(未接続)
		'
		AoPutV( MFCaoSETPT2, 0.0 )


		'
		'	電極ヘッドとＥＳＣ電源を接続
		'	高圧リレー接続
		'
		RyHvMode( pol, MES_ESC )

		RyHvPos( pol, MES_ESC, dt.t2.posv )

		WaitTim( 30 )


		'
		'	測定ル－プ
		'	ＥＳＣ電源への印加電圧ステップ数分繰り返す
		'
		ntst		= 0

		Do While ( ntst < dt.t2.dsiz )

			FrmLog.LogDspAdd								_
			(										_
				""									_
				,									_
				"tst2:ntst=" +								_
				ntst.ToString() +							_
				" CH1 " +								_
				" CH1 " +								_
				dt.t2.d( ntst ).volt1.ToString( "0.0 [V]" ).PadLeft( 11 ) +		_
				" CH2: " +								_
				dt.t2.d( ntst ).volt2.ToString( "0.0 [V]" ).PadLeft( 11 )		_
				,									_
				Color.Empty								_
			)

			DHDTest.StatusDisp( 29, 1, ( ntst + 1 ).ToString + "/" + dt.t2.dsiz.ToString )

			' 20200716 s.harada
			FrmLog.LogDspAdd("", "tst2 開始 " + (ntst + 1).ToString + "/" + dt.t4.dsiz.ToString, Color.Empty)

			If pol = POL_MON Then

				' モノポールの時の電圧印加表示
				DHDTest.StatusDisp							_
				(									_
					9, 5,								_
					"印加位置：" +							_
					convVoltInPosToStr( dt.t2.posv ) +				_
					"　　印加電圧CH1: " +						_
					dt.t2.d( ntst ).volt1.ToString( "0.0 [V]" ).PadLeft( 11 )	_
				)

			Else

				' ダイポールの時の電圧印加表示
				DHDTest.StatusDisp							_
				(									_
					9, 5,								_
					"印加位置：" +							_
					convVoltInPosToStr( dt.t2.posv ) +				_
					"　　印加電圧CH1: " +						_
					dt.t2.d( ntst ).volt1.ToString( "0.0 [V]" ).PadLeft( 11 ) +	_
					"　　印加電圧CH2: " +						_
					dt.t2.d( ntst ).volt2.ToString( "0.0 [V]" ).PadLeft( 11 )	_
				)

			End If


			'
			'	吸着力測定実施
			'
			'
			'	20200219 y.goto トーカロ様向け吸着力測定対応
			'	測定ログデータを各検査毎に違うファイル名で保存するように変更
			'	※本機能は下記条件文を入れ替えることで有効になる
			'
			'	20201102 s.harada 測定上源時間を追加
			sts			= tst2_prc( vac, dt, dt.t2.d( ntst ), tprs, bprs, bakp, ntst, maxtm )
			'sts			= tst2_prc( vac, dt, dt.t2.d( ntst ), tprs, bprs, bakp, ntst )
		'	sts			= tst2_prc( vac, dt, dt.t2.d( ntst ), tprs, bprs, bakp )

			If ( sts < 0 ) Then

				'   20200716 s.harada
				FrmLog.LogDspAdd( "", "tst2 途中終了：tst2_prc sts=" + sts.ToString, Color.Empty )
 
				Exit Do

			End If


			'
			'	測定値を画面表示
			'
			DHDTest.setMesKyucyaku( ntst )

			'   20200716 s.harada
			FrmLog.LogDspAdd( "", "tst2 終了 " + (ntst + 1).ToString + "/" + dt.t4.dsiz.ToString, Color.Empty )

			ntst		+= 1

		Loop


		'	※ 20200901 本バルブはトーカロ様では未装着
		'	ウエハ裏面圧開放バルブON
		'
		ExDio_Output( MAEdoPRG, DIO_ON )

		'
		'	20200220 y.goto トーカロ様対応
		'	ウエハ裏面圧開放バルブ SV3(G4) ON
		'
	'
	'	20200901 y.goto 配管真空引き処理で実施する
	'	ExDio_Output( EXSdoRYE3, DIO_ON )
	'
	'	WaitTim( 1000 )


		'
		'	電極ヘッドの除電を行う
		'
		ESCstop( 2000, 7 )


		'
		'	高圧リレー接続ＯＦＦ
		'
		RyHvPos( pol, MES_ESC, CON_OFF )

		WaitTim( 30 )


		' clrln( 3, 3 );
		DHDTest.StatusClear( 1, 5 )

		FrmLog.LogDspAdd( "", "ウエハ吸着力測定シーケンス終了", Color.Empty )



		FrmLog.LogDspAdd( "", "tst2() End", Color.Empty )

		' return( ntst < 3 ? -1 : 0 )
		If sts < 0 Then

			Return -1

		Else

			Return 0

		End If


	End Function



	'*****
	'	吸着力測定を行う
	'
	'	<return>
	'	0 <	正常終了
	'	0	リトライアウト、ｵﾍﾟﾚ-ﾀによる強制終了
	'	0 >	異常終了
	'*****
	'
	'	20201102 S_Harada AQTC対応
	'	測定時間上限を追加
	'
	Private Function tst2_prc			_
	(						_
		ByVal vac		As Integer,	_
		ByRef dt		As DTREC,	_
		ByRef dat		As DTI2,	_
		ByVal tprs		As Double,	_
		ByVal bprs		As Double,	_
		ByVal bakp		As Double,	_
		ByVal ntst		As Integer,	_
		ByVal maxtm		As Double	_
	)	As Integer
	'Private Function tst2_prc			_
	'(						_
	'	ByVal vac		As Integer,	_
	'	ByRef dt		As DTREC,	_
	'	ByRef dat		As DTI2,	_
	'	ByVal tprs		As Double,	_
	'	ByVal bprs		As Double,	_
	'	ByVal bakp		As Double,	_
	'	ByVal ntst		As Integer	_
	')	As Integer
'	Private Function tst2_prc			_
'	(						_
'		ByVal vac		As Integer,	_
'		ByRef dt		As DTREC,	_
'		ByRef dat		As DTI2,	_
'		ByVal tprs		As Double,	_
'		ByVal bprs		As Double,	_
'		ByVal bakp		As Double	_
'	)	As Integer


		' dat;		測定ﾃﾞｰﾀ格納ｴﾘｱ
		' tprs;		真空圧試験条件
		' bprs;		ｳｴﾊ裏面圧力ﾘﾐｯﾄ値
		'		この圧まで検査を行う
		' bakp;		吸着停止する際の裏面圧

		' maxtm;	測定時間の上限

		Dim nrty		As Integer
		Dim rtn			As Integer
		Dim ngcnt		As Integer
		Dim ptr			As Integer
		Dim clk			As Double
		Dim tim			As Double
		Dim raw			As UShort
		'Dim torr		As Double
		Dim pa			As Double
		'Dim maxt		As Double
		Dim maxp		As Double
		'Dim hprs		As Double
		Dim v1			As Double
		Dim v2			As Double
		Dim flw			As Double
		Dim tm			As TimeSpan
		Dim sttim		As DateTime
		Dim crtim		As DateTime
		Dim sts			As Integer

		' 20201102 S_Harada　追加
		Dim bpsel		As Integer		' 試験裏面圧選択
		Dim setpa		As Double		' 設定圧力Pa
		Dim jdgTm		As Double		' 判定値


		FrmLog.LogDspAdd( "", "tst2_prc() Start", Color.Empty )

		' 20201102 S_Harada 判定基準（秒）に変更
		' 判定基準
		'hprs			= dat.bs
		jdgTm			= dat.bs	’判定値パラメータの設定で判定する


		' 試験結果初期値はＮＧ
		' 20201102 s.harada	試験結果初期値を判定なしに変更
		'dat.okng		= 1
		dat.okng		= -1


		'
		'	試験ループ (NGの場合は３回までﾘﾄﾗｲ)
		'
		nrty			= 0


		Do While ( 3 > nrty )

			' 測定ループ終了フラグ・初期値中止セット
			rtn			= -1

			' 20201102 S_Harada 試験裏面選択圧初期値セット
			bpsel			= 0


			'
			'	試験開始条件待ち３
			'	真空条件の時に、チャンバ内圧力と、ウエハ裏面圧力をチェックする
			'
			sts			= DHDTest.waittstcond3		_
			(							_
				"測定開始条件待ち",				_
				vac,						_
				dt.schuse,					_
				dt.tmp,						_
				tprs,						_
				bakp						_
			)

			If sts Then

				' 中止指示
				rtn			= -1

				Exit Do

			End If


			'
			'	波形サンプリング開始
			'
			WaveSmpGo()


			'
			'	電極ヘッドに吸着電圧印加し目標に到達するまで待つ
			'
			'▼ 2024.04.19 TC Kanda （ウエハ吸着直測定においてESC電源ONは既にありました）
			If ( ESCproc( dat.volt1, dat.volt2, 30000L, 7 ) ) Then

				Exit Do

			End If
			'▲ 2024.04.19 TC Kanda （ウエハ吸着直測定においてESC電源ONは既にありました）


			'	※ 20200901 本バルブはトーカロ様では未装着
			'	ウエハ裏面圧開放バルブOFF
			'
			ExDio_Output( MAEdoPRG, DIO_OFF )

			'
			'	20200220 y.goto トーカロ様対応
			'	ウエハ裏面圧開放バルブ SV3(G4) OFF
			'
			ExDio_Output( EXSdoRYE3, DIO_OFF )		' 20200901 y.goto MB起動中なので既にOFF

			'
			'	20200310 y.goto トーカロ様対応
			' DOX34 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ開
			ExDio_Output( EXSdoRYE2, DIO_ON )

			WaitTim( 100 )



			'
			'	Ｈｅをパラメータで指定された流量流す
			'	20201124 y.goto
			'
			ExDa_Output( MFCaoSETPT1, cvtf2MFCset( dat.he ) )


			' 指定圧に達するまでの時間測定
			' tim = TIM98CNT
			sttim			= DateTime.Now


			' 最大圧記録用
			maxp			= 0.0

			clk			= 0L

			ngcnt			= 0

			ptr			= ADptrR

			' 20201102 S_Harada 測定裏面圧の選択を追加
			bpsel			= 0

			setpa			= convSetPa( bpsel )

			'	20201102 S_Harada 設定圧力追加
			'DHDTest.StatusDisp( 10, 9, ( nrty + 1 ).ToString )
			DHDTest.StatusDisp( 10, 9, ( nrty + 1 ).ToString + "回目     設定圧力 :       kPa" )

			DHDTest.StatusDisp( 34, 9,　( setpa / 1000 ).ToString( "0.0" ) )


			DHDTest.StatusDisp( 10, 10, "ESC･CH1電圧    :           [V]   CH2電圧    :           [V]" )

			DHDTest.StatusDisp( 10, 11, "He流量         :           [CCM]" )

			DHDTest.StatusDisp(10, 12, "真空圧(ﾊﾞﾗﾄﾛﾝ) :           [Pa]")

			DHDTest.StatusDisp(10, 13, "真空圧最大値   :           [Pa]")

			DHDTest.StatusDisp(10, 14, "到達時間       :           [Sec]")



			'
			'	指定裏面圧に到達するまで待つループ
			'
			Do While True

				'AD変換1ﾃﾞ-ﾀ確定するまで待つ (500msec)
				If ( adwaitmsg( ptr ) = Keys.Escape ) Then

					' Ｈｅを止める
					ExDa_Output( MFCaoSETPT1, cvtf2MFCset( 0.0 ) )

					rtn			= 0

					Exit Do

				End If

				clk			+= 1


				'
				'	バラトロン真空計の値を取得
				'
				'raw	= aiget( GMaiPRS, nMVAVG )
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
				'	今までのウエハ裏面の最大圧を記録する
				'
				If ( maxp < pa ) Then

					maxp			= pa

				End If


				' 経過時間を計算
				crtim			= DateTime.Now

				tm			= crtim.Subtract( sttim )

				'	20201102 s.harada
				'	単位を秒に変更
				'tim			= tm.TotalMilliseconds
				tim			= tm.TotalSeconds


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
				DHDTest.StatusDisp( 27, 11, flw.ToString( "0.0" ).PadLeft( 5 ) )


				'
				'	バラトロン真空計（ウエハ裏面圧）の測定値を表示
				'
				DHDTest.StatusDisp( 27, 12, pa.ToString( "0.0" ).PadLeft( 8 ) )


				'
				'	ウエハ裏面圧最大値を表示
				'
				DHDTest.StatusDisp( 27, 13, maxp.ToString( "0.0" ).PadLeft( 8 ) )


				'	20201102 s.harada
				'	単位を秒に変更
				'DHDTest.StatusDisp( 27, 14, ( tim / 1000.0 ).ToString( "0.0" ).PadLeft( 5 ) )
				DHDTest.StatusDisp( 30, 14, tim.ToString( "0" ).PadLeft( 5 ) )


				'
				'	指定圧力まで上がったなら測定終了
				'
				'	20201102 S_Harada 1,2,3,4,6kPaまで連続測定に変更
				'
				''	20200219 y.goto トーカロ様向け吸着力測定対応
				''	目標圧力は、各試験項目ごとの吸着力判定基準[Pa]に変更する
				''	こうすることで、以前は試験１，２，３，４ごとに固定であった裏面圧力目標値を
				''	各項目ごとに設定した圧力にすることが出来る
				''	※本機能は下記条件文を入れ替えることで有効になる
				'	If dat.bs <= pa Then
				''	If ( bprs <= pa ) Then

				'		' Ｈｅを止める
				'		ExDa_Output( MFCaoSETPT1, cvtf2MFCset( 0.0 ) )

				'		' 到達時間記録
				'		dat.tmr = tim / 1000.0

				'		rtn			= 1

				'		dat.pa( nrty )		= pa


				'		'
				'		'	判定ＯＫ
				'		'
				'		dat.okng		= 0

				'		Exit Do

				'	End If

				If tim < maxTm Then
					' 測定時間内

					If bpsel <= 2 And jdgTm > 0  Then
						'判定有

						If jdgTm < tim then

							'
							'	3kPaのタイムアウト
							'

							rtn			= -1

							Exit Do

						ElseIf setpa <= pa Then

							' 到達時間記録
							'
							dat.tmr( bpsel)	= tim

							If bpsel = 2 then
								'
								'	判定ＯＫ
								'
								dat.okng		= 0

							End If

							bpsel			+= 1

							If bpsel > 4 Then

								' 測定終了
								rtn			= 1

								Exit Do

							End If

							setpa			= convSetPa( bpsel )

							DHDTest.StatusDisp( 34, 9,　( setpa / 1000 ).ToString( "0.0" ) )

						End If


					Else

						’判定なし

						If setpa <= pa Then

							' 到達時間記録
							'
							dat.tmr( bpsel)	= tim

							bpsel			+= 1

							If bpsel > 4 Then

								' 測定終了
								rtn			= 1

								Exit Do

							End If

							setpa			= convSetPa( bpsel )

							DHDTest.StatusDisp( 34, 9,　( setpa / 1000 ).ToString( "0.0" ) )


						End If

					End If

				Else

					If bpsel <= 2 And jdgTm > 0  Then
						'判定有
						'測定時間オーバー リトライへ
						rtn			= -1

						Exit Do

					Else
						’判定なし
						'測定時間オーバー 次の測定へ
						rtn			= 1

						Exit Do

					End If

				End If


			Loop


			'
			'	Ｈｅを止める
			'
			ExDa_Output( MFCaoSETPT1, cvtf2MFCset( 0.0 ) )


			' clrln( 10, 10 );
			DHDTest.StatusClear( 9, 6 )


			WaitTim( 100 )


			'	※ 20200901 本バルブはトーカロ様では未装着
			'	ウエハ裏面圧開放バルブON
			'
			ExDio_Output( MAEdoPRG, DIO_ON )

			'
			'	20200220 y.goto トーカロ様対応
			'	ウエハ裏面圧開放バルブ SV3(G4) ON
			'
			'
			'	20200901 y.goto 配管真空引き VACBproc() で実施する
			'	ExDio_Output( EXSdoRYE3, DIO_ON )
			'

			'
			'	20200901 追加 y.goto
			'	配管真空引き
			'
			sts = DHDTest.VACBproc(DHDTest.TestType.Resistance, 0, 0)
			If sts <> 0 Then

				' 試験中止
				rtn		= -1
				Exit Do

			End If

			'
			'	20201013 y.goto
			'	トーカロ木村さんの指示で、電圧印可停止タイミング変更
			'	ウエハ裏面圧がなかなか下がらないため。
			'
			'	吸着を停止する
			'
			'▼2024.04.19 TC Kanda (ESC電源OFFの処理を チャンバ内圧力を下げる前に異動）
			'ESCstop( 2000.0, 7 )
			'▲2024.04.19 TC Kanda (ESC電源OFFの処理を チャンバ内圧力を下げる前に異動）

			'
			'	しばらく待つ
			'
			WaitTim( 200 )

			'
			'	ウエハ裏面圧条件待ち処理
			'
			'▼2024.04.11 TC Kanda （無限ループすることがあるらしいので削除）
			sts = DHDTest.waitwbakp _
			(
				"ウエハ裏面圧が下がるのを待つ",
				vac,
				dt.schuse,
				dt.tmp,
				tprs,
				bakp
			)
			'▲2024.04.11 TC Kanda （無限ループすることがあるらしいので削除）

			'
			'	波形サンプリング停止
			'
			WaveSmpStop()


			'
			'	しばらく待つ
			'
			WaitTim( 200 )



			' 波形サンプリングデータを保存
			SaveWaveData					_
			(						_
				DHDtest.tstNo,				_
				"B" + ntst.ToString(),			_
				dat.volt1,				_
				dat.volt2				_
			)


			If sts Then

				' 中止指示
				rtn			= -1

				Exit Do

			End If

			'
			'	ループ終了判断
			'
			If ( 0 <= rtn ) Then

				Exit Do

			End If


			'
			'	リトライ対象外
			'
			If bpsel > 2 Or jdgTm = 0  Then

				Exit Do

			End If

			'
			'	リトライ判断
			'
			nrty			+= 1

			If ( 3 <= nrty ) Then

				' ﾘﾄﾗｲｱｳﾄ
				rtn			= 0	'測定終了なら-1

				'試験結果は ＮＧ
				dat.okng		= 1

				Exit Do

			End If

		Loop

		'
		'	20200310 y.goto トーカロ様対応
		' DOX34 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ閉
		ExDio_Output( EXSdoRYE2, DIO_OFF )

		FrmLog.LogDspAdd( "", "tst2_prc() End", Color.Empty )

		Return rtn

	End Function


	Private Function convSetPa( ByVal no As Integer ) As Double

		Dim pa		As Double

		Select Case no
	
		Case 0
			'1kPa
			pa		= 1000

		Case 1
			'2kPa
			pa		= 2000

		Case 2
			'3kPa
			pa		= 3000

		Case 3
			'4kPa
			pa		= 4000

		Case 4
			'6kPa
			pa		= 6000

		End Select

		Return pa

	End Function

End Module
