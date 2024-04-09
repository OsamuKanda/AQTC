

Module SdcLib


	'
	'	ESC電源･CH1設定電圧変換係数初期値
	'
	Const ESCFACT			= ( +10.0 - -10.0 ) / ( +1000.0 - -1000.0 )


	' ESC電源･CH1設定電圧変換係数 20131218 削除
	' Dim ESCfac1 As Double

	' ESC電源･CH2設定電圧変換係数 20131218 削除
	' Dim ESCfac2 As Double



	'/*****
	'	ＥＳＣ電源の終了シーケンス・モノポール動作
	'
	'	<return>
	'	0	正常終了
	'	-1	除電失敗
	'*****/
	Private Function ESCstopS			_
	(						_
		ByVal tmr		As Double,	_
		ByVal y			As Integer	_
	)	As Integer

		Dim sts			As Integer
		Dim raw			As UShort
		Dim ptr			As Integer
		Dim volt		As Double
		Dim rslt		As DialogResult



		If y <> 0 Then

			' mvatr( 12, y, ATR_C7|ATR_RVS|ATR_BLK );
			DHDTest.StatusDisp( 12, y, "＊＊＊  ＥＳＣ電源ＣＨ１・除電中  ＊＊＊" )

		End If


		'
		'	ESC･V1外部設定信号出力
		'
		AoPutV( ESCaoVOLT1, 0.0 )


		'
		'	リトライループ
		'
		Do

			' ESC･外部起動信号 OFF
			ExDio_Output( ESCdoSTART1, DIO_OFF )

			' 除電完了するまで待つ
			SetTimDCnt( 0, tmr )

			ptr			= ADptrR

			Do

				'
				'	ＡＤ変換デ－タが確定するまで待つ
				'
				If adwaitmsg( ptr ) = Keys.Escape Then

					Exit Do

				End If


				'
				'	ＥＳＣ・ＣＨ１モニタ電圧を取得
				'
				raw			= aiget( ESCaiMON1, 1 )

				' ＲＡＷデ－タからＶ１電圧へ換算
				volt			= cvtr2ESC( raw )


				'
				'	ＥＳＣ・ＣＨ１ステ－タス信号状態取得
				'
				sts			= InBuf( ESCdiSTAT1 )



				'
				'	状態表示
				'
				If y <> 0 Then

					DHDTest.StatusDisp						_
					(								_
						8, y + 2,						_
						"残り時間:" +						_
						( TimerDCnt( 0 ) / 100 ).ToString( "0秒" ).PadLeft( 5 )	_
					)

					DHDTest.StatusDisp						_
					(								_
						8, y + 3,						_
						"CH1:出力電圧:" +					_
						volt.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  ステ－タス信号:" +					_
						convOnOffToStr( sts )					_
					)

				End If


				Application.DoEvents()


			Loop While ( TimerDCnt( 0 ) > 0 And sts = DIO_ON )


			'
			'	除電完了かチェック
			'
			If sts = DIO_ON Then

				rslt				= flipbz1r			_
				(								_
					OPIPyn,							_
					"ＥＳＣ・ＣＨ１の除電に失敗しました。ﾘﾄﾗｲしますか？"	_
				)

				If rslt <> DialogResult.Yes Then

					sts			= -1

					Exit Do

				End If

			Else

				Exit Do

			End If

		Loop


		If y <> 0 Then

			DHDTest.StatusClear( y, 5 )

		End If



		Return ( sts )


	End Function



	'/*****
	'	ＥＳＣ電源の終了シーケンス・ダイポール動作２
	'
	'	<return>
	'	0	正常終了
	'	-1	除電失敗
	'
	'	<update>
	'	2014-01-17 y.goto
	'		Heリーク量測定終了時、ESC電源をOFFすると
	'		ウエハが飛び上がってしまう現象が確認されたので、
	'		ESC電源の電圧を徐々に下げるように改造した。
	'*****/
	Private Function ESCstopD2			_
	(						_
		ByVal v1		As Double,	_
		ByVal v2		As Double,	_
		ByVal tmr		As Double,	_
		ByVal y			As Integer	_
	)	As Integer

		Dim sts1		As Integer
		Dim sts2		As Integer
		Dim raw1		As UShort
		Dim raw2		As UShort
		Dim volt1		As Double
		Dim volt2		As Double
		Dim ptr			As Integer
		Dim vstp1		As Double
		Dim vstp2		As Double
		Dim vout1		As Double
		Dim vout2		As Double
		dim dmy			as double

		' Dim dt		As DateTime
		' Dim dt1		As DateTime
		' Dim ts		As TimeSpan
		' Dim tim		As Double



		If y <> 0 Then

			' mvatr( 12, y, ATR_C7|ATR_RVS|ATR_BLK );

			DHDTest.StatusDisp( 12, y, "＊＊＊  ＥＳＣ電源ＣＨ１、ＣＨ２・除電中  ＊＊＊" )

		End If


		'
		'	電圧の変化ステップ電圧計算
		'
		If 0.0 < v1 Then

			vstp1			= -( v1 * 0.02 )

		Else

			vstp1			= +( ( v1 * -1.0 ) * 0.02 )

		End If


		If 0.0 < v2 Then

			vstp2			= -( v2 * 0.02 )

		Else

			vstp2			= +( ( v2 * -1.0 ) * 0.02 )

		End If

		vout1			= v1

		vout2			= v2


		'
		'	電圧を徐々に0.0Vに戻すループ
		'
		Do

			vout1			+= vstp1

			vout2			+= vstp2


			If 0.0 > vstp1 then

				If 0.0 > vout1 Then

					vout1		= 0.0

				End If

			Else

				If 0.0 < vout1 then

					vout1		= 0.0

				End If

			End If
 

			If 0.0 > vstp2 then

				If 0.0 > vout2 Then

					vout2		= 0.0

				End If

			Else

				If 0.0 < vout2 then

					vout2		= 0.0

				End If

			End If
 

			' ESC･CH1,CH2外部設定信号出力 (AO)
			ExDa_Output( ESCaoVOLT1, cvtv2ESC( vout1, dmy ) )

			ExDa_Output( ESCaoVOLT2, cvtv2ESC( vout2, dmy ) )


			WaitTim( 50.0 )


			If vout1 = 0.0 And vout2 = 0.0 Then

				Exit Do

			End If

		Loop


		'
		'	ESC･V1外部設定信号出力０V設定
		'
		AoPutV( ESCaoVOLT1, 0.0 )

		AoPutV( ESCaoVOLT2, 0.0 )



		'
		'	リトライループ
		'
		Do


			'
			'	ESC･外部起動信号 OFF
			'
			ExDio_Output( ESCdoSTART1, DIO_OFF )
			ExDio_Output( ESCdoSTART2, DIO_OFF )


			' 除電完了するまで待つ
			 'TimerDCnt(0) = tmr
			SetTimDCnt( 0, tmr )

			ptr			= ADptrR

			Do

				'
				'	ＡＤ変換デ－タが確定するまで待つ
				'
				If adwaitmsg( ptr ) = Keys.Escape Then

					Exit Do

				End If


				'
				'	ＥＳＣ・ＣＨ１モニタ電圧を取得
				'
				raw1			= aiget( ESCaiMON1, 1 )

				' ＲＡＷデ－タからＶ１電圧へ換算
				volt1			= cvtr2ESC( raw1 )


				'
				'	ＥＳＣ・ＣＨ１ステ－タス信号状態取得
				'
				sts1			= InBuf( ESCdiSTAT1 )



				'
				'	ＥＳＣ・ＣＨ２モニタ電圧を取得
				'
				raw2			= aiget( ESCaiMON2, 1 )

				' ＲＡＷデ－タからＶ１電圧へ換算
				volt2			= cvtr2ESC( raw2 )


				'
				'	ＥＳＣ・ＣＨ２ステ－タス信号状態取得
				'
				sts2			= InBuf( ESCdiSTAT2 )


				'
				'	状態表示
				'
				If y <> 0 Then

					' mvatr( 8, y + 2, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 2,						_
						"残り時間:" +						_
						( TimerDCnt( 0 ) / 100 ).ToString( "0秒" ).PadLeft( 5 )	_
					)

					' mvatr( 8, y + 3, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 3,						_
						"ＣＨ１:出力電圧:" +					_
						volt1.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  ステ－タス信号:" +					_
						convOnOffToStr( sts1 )					_
					)

					' mvatr( 8, y + 4, ATR_C8 );
					DHDTest.StatusDisp						_
					(								_
						8, y + 4,						_
						"ＣＨ２:出力電圧:" +					_
						volt2.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  ステ－タス信号:" +					_
						convOnOffToStr( sts2 )					_
					)

				End If

				Application.DoEvents()

			Loop While ( TimerDCnt( 0 ) > 0 And sts1 = DIO_ON And sts2 = DIO_ON )


			'
			'	除電完了かチェック
			'
			If sts1 = DIO_ON Or sts2 = DIO_ON Then

				If flipbz1r( OPIPyn, "ＥＳＣの除電に失敗しました。ﾘﾄﾗｲしますか？" ) <> DialogResult.Yes Then

					sts1			= -1

					Exit Do

				End If

			Else

				Exit Do

			End If

		Loop


		If y <> 0 Then

			DHDTest.StatusClear( y, 5 )

		End If


		Return ( sts1 Or sts2 )

	End Function




	'/*****
	'	ＥＳＣ電源の終了シーケンス・ダイポール動作
	'
	'	<return>
	'	0	正常終了
	'	-1	除電失敗
	'*****/
	Private Function ESCstopD			_
	(						_
		ByVal tmr		As Double,	_
		ByVal y			As Integer	_
	)	As Integer

		Dim sts1		As Integer
		Dim sts2		As Integer
		Dim raw1		As UShort
		Dim raw2		As UShort
		Dim volt1		As Double
		Dim volt2		As Double
		Dim ptr			As Integer

		' Dim dt		As DateTime
		' Dim dt1		As DateTime
		' Dim ts		As TimeSpan
		' Dim tim		As Double



		If y <> 0 Then

			' mvatr( 12, y, ATR_C7|ATR_RVS|ATR_BLK );

			DHDTest.StatusDisp( 12, y, "＊＊＊  ＥＳＣ電源ＣＨ１、ＣＨ２・除電中  ＊＊＊" )

		End If


		'
		'	ESC･V1外部設定信号出力０V設定
		'
		AoPutV( ESCaoVOLT1, 0.0 )

		AoPutV( ESCaoVOLT2, 0.0 )



		'
		'	リトライループ
		'
		Do


			'
			'	ESC･外部起動信号 OFF
			'
			ExDio_Output( ESCdoSTART1, DIO_OFF )
			ExDio_Output( ESCdoSTART2, DIO_OFF )


			' 除電完了するまで待つ
			 'TimerDCnt(0) = tmr
			SetTimDCnt( 0, tmr )

			ptr			= ADptrR

			Do

				'
				'	ＡＤ変換デ－タが確定するまで待つ
				'
				If adwaitmsg( ptr ) = Keys.Escape Then

					Exit Do

				End If


				'
				'	ＥＳＣ・ＣＨ１モニタ電圧を取得
				'
				raw1			= aiget( ESCaiMON1, 1 )

				' ＲＡＷデ－タからＶ１電圧へ換算
				volt1			= cvtr2ESC( raw1 )


				'
				'	ＥＳＣ・ＣＨ１ステ－タス信号状態取得
				'
				sts1			= InBuf( ESCdiSTAT1 )



				'
				'	ＥＳＣ・ＣＨ２モニタ電圧を取得
				'
				raw2			= aiget( ESCaiMON2, 1 )

				' ＲＡＷデ－タからＶ１電圧へ換算
				volt2			= cvtr2ESC( raw2 )


				'
				'	ＥＳＣ・ＣＨ２ステ－タス信号状態取得
				'
				sts2			= InBuf( ESCdiSTAT2 )


				'
				'	状態表示
				'
				If y <> 0 Then

					' mvatr( 8, y + 2, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 2,						_
						"残り時間:" +						_
						( TimerDCnt( 0 ) / 100 ).ToString( "0秒" ).PadLeft( 5 )	_
					)

					' mvatr( 8, y + 3, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 3,						_
						"ＣＨ１:出力電圧:" +					_
						volt1.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  ステ－タス信号:" +					_
						convOnOffToStr( sts1 )					_
					)

					' mvatr( 8, y + 4, ATR_C8 );
					DHDTest.StatusDisp						_
					(								_
						8, y + 4,						_
						"ＣＨ２:出力電圧:" +					_
						volt2.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  ステ－タス信号:" +					_
						convOnOffToStr( sts2 )					_
					)

				End If

				Application.DoEvents()

			Loop While ( TimerDCnt( 0 ) > 0 And sts1 = DIO_ON And sts2 = DIO_ON )


			'
			'	除電完了かチェック
			'
			If sts1 = DIO_ON Or sts2 = DIO_ON Then

				If flipbz1r( OPIPyn, "ＥＳＣの除電に失敗しました。ﾘﾄﾗｲしますか？" ) <> DialogResult.Yes Then

					sts1			= -1

					Exit Do

				End If

			Else

				Exit Do

			End If

		Loop


		If y <> 0 Then

			DHDTest.StatusClear( y, 5 )

		End If


		Return ( sts1 Or sts2 )

	End Function



	'/*****
	'	ＥＳＣ電源の終了シーケンス
	'	<return>
	'	0	正常終了
	'	-1	除電失敗
	'*****/
	Public Function ESCstop					_
	(							_
		ByVal tmr			As Double,	_
		ByVal y				As Integer	_
	)

		Dim sts				As Integer



		' 電極ヘッド種別により振り分け
		If ESCmd = POL_DIE Then

			' ＥＳＣ－２０００はダイポール動作
			sts			= ESCstopD( tmr, y )

		Else

			' ＥＳＣ－２０００はモノポール動作
			sts			= ESCstopS( tmr, y )

		End If



		Return sts

	End Function



	'/*****
	'	ＥＳＣ電源の終了シーケンス２
	'	<return>
	'	0	正常終了
	'	-1	除電失敗
	'*****/
	Public Function ESCstop2				_
	(							_
		ByVal volt1			As Double,	_
		ByVal volt2			As Double,	_
		ByVal tmr			As Double,	_
		ByVal y				As Integer	_
	)

		Dim sts				As Integer



		' 電極ヘッド種別により振り分け
		If ESCmd = POL_DIE Then

			' ＥＳＣ－２０００はダイポール動作
			sts			= ESCstopD2( volt1, volt2, tmr, y )

		Else

			' ＥＳＣ－２０００はモノポール動作
			sts			= ESCstopS( tmr, y )

		End If



		Return sts

	End Function




	Const nSA		= 6



	'/*****
	'	ＥＳＣ電源の電圧印加処理・モノポール動作
	'	<return>
	'	0	正常終了
	'	-1	ESCは使用出来る状態になっていない
	'	-2	電圧が出力されない
	'*****/
	Private Function ESCprocS				_
	(							_
		ByVal vset			As Double,	_
		ByVal tmr			As Double,	_
		ByVal y				As Integer	_
	)	As Integer

		' vset;		ESCの目標電圧
		' clk;		待ち時間
		' y;		状態表示開始行
		'		0	表示しない
		' Dim aov1 As Double 20131218 削除

		Dim vct1			As Double
		Dim cv1				As Double
		Dim rbf1( nSA )			As Single
		Dim raw1			As UShort
		Dim ptr				As Integer
		Dim okf				As Integer
		Dim cnt				As Integer
		Dim rty				As Integer
		Dim vrng			As Double



		' 目標到達判断の範囲 (±0.5%以内)
		vrng			= vset * 0.005

		If vrng < 0.0 Then

			vrng			*= -1.0

		End If


		If y <> 0 Then

			' mvatr( 12, y, ATR_C7|ATR_RVS|ATR_BLK );

			DHDTest.StatusDisp( 12, y, "＊＊＊  ＥＳＣ電源・ＣＨ１電圧安定待ち中  ＊＊＊" )

		End If


		'
		'	モノポール電極ヘッドとＥＳＣ－２０００・ＣＨ１を接続
		'
		RyHvMode( POL_MON, MES_ESC )


		'
		'	ｲﾝﾀｰﾛｯｸ信号 CH1=ON CH2=OFF
		'
		ExDio_Output( ESCdoITL1, DIO_ON )
		ExDio_Output( ESCdoITL2, DIO_OFF )


		WaitTim( 50 )


		'
		'	ESC･CH1外部起動信号 ON
		'
		ExDio_Output( ESCdoSTART1, DIO_ON )

		'
		'	ESC･CH2外部起動信号 OFF (ﾓﾉﾎﾟｰﾙ動作のときはCH2は使用しない)
		'
		ExDio_Output( ESCdoSTART2, DIO_OFF )



		''ESC電源の V1 と制御電圧の変換比率初期値 (+/-10V = +/-1000V) 20131218 削除
		'cv1 = vset
		'aov1 = vset * ESCFACT

		'ESC･V1外部設定信号出力 (AO) 20131218 DoLoop内から移動
		ExDa_Output( ESCaoVOLT1, cvtv2ESC( vset, cv1 ) )


		rty			= 0

		Do

			''変換係数を再計算  20131218 削除	
			'ESCfac1 = aov1 / cv1

			''ＥＳＣ制御電圧出力値(AO)を計算
			'aov1 = vset * ESCfac1

			''ESC･V1外部設定信号出力 (AO)
			'ExDa_Output(ESCaoVOLT1, cvtv2ESC(vset, cv1))

			'待ちループ
			'TimerDCnt(0) = tmr

			SetTimDCnt( 0, tmr )

			ptr			= ADptrR

			okf			= 1

			cnt			= 0


			For i = 0 To nSA - 1

				rbf1( i )		= 0

			Next



			Do While ( TimerDCnt( 0 ) > 0 )

				'
				'	ＡＤ変換デ－タが確定するまで待つ
				'
				If adwaitmsg( ptr ) = Keys.Escape Then

					okf			= -1

					Exit Do

				End If


				'
				'	ESC･CH1モニタ電圧を取得
				'
				raw1			= aiget( ESCaiMON1, 20 )

				' ＲＡＷデ－タからＶ１電圧へ換算
				cv1			= cvtr2ESC( raw1 )



				'
				'	傾向を求める
				'
				vct1			= calvct( cnt, cv1, rbf1, nSA )



				'
				'	状態表示
				'
				If y <> 0 Then

					' mvatr( 8, y + 2, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 2,						_
						"残り時間:" +						_
						( TimerDCnt( 0 ) / 100 ).ToString( "0秒" ).PadLeft( 5 )	_
					)

					' mvatr( 8, y + 3, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 3,						_
						"CH1:目標:" +						_
						vset.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  現在:" +						_
						cv1.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"   傾向:" +						_
						vct1.ToString( "0.0000" ).PadLeft( 8 )			_
					)

				End If


				'
				'	安定したかチェック
				'
				If				_
				(				_
					nSA < cnt And		_
					-0.05 <= vct1 And	_
					vct1 <= 0.05		_
				)				_
				Then

					Exit Do

				End If


				'
				'	ESC･ｽﾃｰﾀｽ信号ﾁｪｯｸ
				'
				If ( InBuf( ESCdiSTAT1 ) = 0 ) Then

					' ESC･CH1外部起動信号 OFF
					ExDio_Output( ESCdoSTART1, DIO_OFF )

					syserr( "ESC･CH1のｽﾃｰﾀｽ信号が落ちました" )

					okf			= -1

					Exit Do

				End If

				cnt			+= 1

				Application.DoEvents()

			Loop



			' 異常発生した場合は中止
			If okf < 0 Then

				Exit Do

			End If



			'
			'	目標到達判断
			'
			If				_
			(				_
				vset - vrng <= cv1 And	_
				cv1 <= vset + vrng	_
			)				_
			Then

				okf			= 0

				Exit Do

			End If


			'
			'	リトライする
			'
			rty			+= 1

			If rty >= 10 Then

				If flipbz1r( OPIPyn, "目標電圧に到達しません。処理を中止しますか？" ) = DialogResult.Yes Then

					okf			= -1

					Exit Do

				End If

				rty			= 0

			End If

		Loop While True


		If y <> 0 Then

			DHDTest.StatusClear( y, 1 )

			DHDTest.StatusClear( y + 2, 2 )

		End If



		Return okf

	End Function



	'/*****
	'	ＥＳＣ電源の電圧印加処理・ダイポール動作
	'	<return>
	'	0	正常終了
	'	-1	ESCは使用出来る状態になっていない
	'	-2	電圧が出力されない
	'*****/
	Private Function ESCprocD			_
	(						_
		ByVal vset1		As Double,	_
		ByVal vset2		As Double,	_
		ByVal tmr		As Double,	_
		ByVal y			As Integer	_
	)	As Integer

		' vset1;	ESC CH1の目標電圧
		' vset2;	ESC CH2の目標電圧
		' clk;		待ち時間	
		' y;		状態表示開始行
		'		0	表示しない
		' Dim aov1 As Double  20131218 削除
		' Dim aov2 As Double

		Dim vct1		As Double
		Dim vct2		As Double
		Dim cv1			As Double
		Dim cv2			As Double
		Dim rbf1( nSA )		As Single
		Dim rbf2( nSA )		As Single
		Dim raw1		As UShort
		Dim raw2		As UShort
		Dim ptr			As Integer
		Dim okf			As Integer
		Dim cnt			As Integer
		Dim rty			As Integer
		Dim vrng1		As Double
		Dim vrng2		As Double



		' 目標到達判断の範囲 (±0.5%以内)
		vrng1			= vset1 * 0.005

		If vrng1 < 0.0 Then

			vrng1			*= -1.0

		End If


		vrng2			= vset2 * 0.005

		If vrng2 < 0.0 Then

			vrng2			*= -1.0

		End If


		If y <> 0 Then

			DHDTest.StatusDisp( 12, y, "＊＊＊  ＥＳＣ電源・ＣＨ１、ＣＨ２電圧安定待ち中  ＊＊＊" )

		End If


		'
		'	ダイポール電極ヘッドとＥＳＣ－２０００・ＣＨ１、２を接続
		'
		RyHvMode( POL_DIE, MES_ESC )


		'
		'	ｲﾝﾀｰﾛｯｸ信号 ON
		'
		ExDio_Output( ESCdoITL1, DIO_ON )

		ExDio_Output( ESCdoITL2, DIO_ON )


		WaitTim( 50 )


		' ESC･CH1外部起動信号 ON
		ExDio_Output( ESCdoSTART1, DIO_ON )

		' ESC･CH2外部起動信号 ON
		ExDio_Output( ESCdoSTART2, DIO_ON )


		' 'ESC電源の V1 と制御電圧の変換比率初期値 (+/-10V = +/-1000V)	20131218 削除
		' cv1 = vset1
		' cv2 = vset2
		' aov1 = vset1 * ESCFACT
		' aov2 = vset2 * ESCFACT
            


		' ESC･CH1,CH2外部設定信号出力 (AO)  20131218 DoLoop内から移動
		ExDa_Output( ESCaoVOLT1, cvtv2ESC( vset1, cv1 ) )

		ExDa_Output( ESCaoVOLT2, cvtv2ESC( vset2, cv2 ) )

		rty			= 0


		Do

			''変換係数を再計算	20131218 削除
			'ESCfac1 = aov1 / cv1
			'ESCfac2 = aov2 / cv2

			'ＥＳＣ制御電圧出力値(AO)を計算
			'aov1 = vset1 * ESCfac1
			'aov2 = vset2 * ESCfac2

			'ESC･CH1,CH2外部設定信号出力 (AO)	
			'ExDa_Output(ESCaoVOLT1, anav2r1(aov1))
			'ExDa_Output(ESCaoVOLT2, anav2r1(aov2))

			'待ちループ
			'TimerDCnt(0) = tmr

			SetTimDCnt( 0, tmr )

			ptr			= ADptrR

			okf			= 1

			cnt			= 0

			For i = 0 To nSA - 1

				rbf1( i )		= 0

				rbf2( i )		= 0

			Next



			Do While ( TimerDCnt( 0 ) > 0 )


				'
				'	ＡＤ変換デ－タが確定するまで待つ
				'
				If adwaitmsg( ptr ) = Keys.Escape Then

					okf			= -1

					Exit Do

				End If


				'
				'	ESC･CH1,CH2モニタ電圧を取得
				'
				raw1			= aiget( ESCaiMON1, 1 )

				raw2			= aiget( ESCaiMON2, 1 )

				' ＲＡＷデ－タからＶ１電圧へ換算
				cv1			= cvtr2ESC( raw1 )

				cv2			= cvtr2ESC( raw2 )


				'
				'	傾向を求める
				'
				vct1			= calvct( cnt, cv1, rbf1, nSA )

				vct2			= calvct( cnt, cv2, rbf2, nSA )


				'
				'	状態表示
				'
				If y <> 0 Then

					' mvatr( 8, y + 2, ATR_C8 );

					DHDTest.StatusDisp							_
					(									_
						8, y + 2,							_
						"残り時間:" +							_
						( TimerDCnt( 0 ) / 1000 ).ToString( "0秒" ).PadLeft( 5 )	_
					)

					' mvatr( 8, y + 3, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 3,						_
						"CH1:目標:" +						_
						vset1.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  現在:" +						_
						cv1.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"   傾向:" +						_
						vct1.ToString( "0.0000" ).PadLeft( 8 )			_
					)

					' mvatr( 8, y + 4, ATR_C8 );

					DHDTest.StatusDisp						_
					(								_
						8, y + 4,						_
						"CH1:目標:" +						_
						vset2.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"  現在:" +						_
						cv2.ToString( "0.0V" ).PadLeft( 8 ) +			_
						"   傾向:" +						_
						vct2.ToString( "0.0000" ).PadLeft( 8 )			_
					)

				End If


				'
				'	安定したかチェック
				'
				If							_
					( nSA < cnt ) And				_
					( -0.05 <= vct1 And vct1 <= 0.05 ) And		_
					( -0.05 <= vct2 And vct2 <= 0.05 )		_
				Then

					Exit Do

				End If


				'
				'	ESC･ｽﾃｰﾀｽ信号ﾁｪｯｸ
				'
				If ( InBuf( ESCdiSTAT1 ) = 0 Or InBuf( ESCdiSTAT2 ) = 0 ) Then

					' ESC･CH1外部起動信号 OFF
					ExDio_Output( ESCdoSTART1, DIO_OFF )

					' ESC･CH2外部起動信号 OFF
					ExDio_Output( ESCdoSTART2, DIO_OFF )

					syserr( "ESCのｽﾃｰﾀｽ信号が落ちました" )

					okf				= -1

					Exit Do

				End If

				cnt			+= 1

				Application.DoEvents()

			Loop


			' 異常発生した場合は中止
			If okf < 0 Then

				Exit Do

			End If



			'
			'	目標到達判断
			'
			If						_
				( Math.Abs( vset1 ) - vrng1 <= Math.Abs( cv1 ) ) And		_
				( Math.Abs( cv1 ) <= Math.Abs( vset1 ) + vrng1 ) And		_
				( Math.Abs( vset2 ) - vrng2 <= Math.Abs( cv2 ) ) And		_
				( Math.Abs( cv2 ) <= Math.Abs( vset2 ) + vrng2 )		_
			Then

				okf			= 0

				Exit Do

			End If



			'
			'	リトライする
			'
			rty			+= 1

			If rty >= 10 Then

				If flipbz1r( OPIPyn, "目標電圧に到達しません。処理を中止しますか？" ) = DialogResult.Yes Then

					okf			= -1

					Exit Do

				End If

				rty			= 0

			End If

		Loop While True



		If y <> 0 Then

			DHDTest.StatusClear( y, 1 )

			DHDTest.StatusClear( y + 2, 3 )

		   End If



		Return okf

	End Function



	'/*****
	'	ＥＳＣ電源の電圧印加処理
	'	<return>
	'	0	正常終了
	'	-1	ESCは使用出来る状態になっていない
	'	-2	電圧が出力されない
	'*****/
	Public Function ESCproc				_
	(						_
		ByVal vset1		As Double,	_
		ByVal vset2		As Double,	_
		ByVal tmr		As Double,	_
		ByVal y			As Integer	_
	)	As Integer

		'vset1;	ESC CH1の目標電圧
		'vset2;	ESC CH2の目標電圧（モノポール時は未使用）
		'clk;	待ち時間
		'y;	    状態表示開始行
		'		0	表示しない

		Dim sts			As Integer



		'
		'	電極ヘッド種別により振り分け
		'
		If ESCmd = POL_DIE Then

			' ＥＳＣ－２０００はダイポール動作
			sts			= ESCprocD( vset1, vset2, tmr, y )

		Else

			' ＥＳＣ－２０００はモノポール動作
			sts			= ESCprocS( vset1, tmr, y )

		End If



		Return sts

	End Function



	'*****
	'	ESC電源ステータス信号チェック
	'*****
	Public Function ESCchkSts() As Integer

		Dim rtn				As Integer



		rtn			= 0

		
		'
		'	電極ヘッド種別により振り分け
		'
		If ESCmd = POL_DIE Then

			'
			'	ダイポール動作
			'

			' ESC･ｽﾃｰﾀｽ信号ﾁｪｯｸ
			If ( InBuf( ESCdiSTAT1 ) = 0 Or InBuf( ESCdiSTAT2 ) = 0 ) Then

				syserr( "ESCのｽﾃｰﾀｽ信号が落ちました" )

				rtn			= -1

			End If

		Else

			'
			'	モノポール動作
			'

			' ESC･ｽﾃｰﾀｽ信号ﾁｪｯｸ
			If ( InBuf( ESCdiSTAT1 ) = 0 ) Then

				syserr( "ESCのｽﾃｰﾀｽ信号が落ちました" )

				rtn			= -1

			End If

		End If



		return( rtn )

	End Function


End Module
