Imports InterfaceCorpDllWrap

Module Board


	Public Function Board_Open() As Integer


		' ボードの初期化
		Dim nRet		As Integer
		Dim err			As Integer



		err			= 0

		nRet			= ExDio_Open()

		If nRet <> IFCDIO_ANY.FBIDIO_ERROR_SUCCESS Then

			MessageBox.Show( "ＤＩＯボードがオープンできません。" + ConvErrMessageDio( nRet ), "ボードエラー" )

			WriteLog( "", "LG", "Board_Open ＤＩＯオープンエラー：" + ConvErrMessageDio( nRet ) )


' 20140120 拡張基板が無くても動作するようにする
			err			+= 1
'			Return nRet


		End If



		nRet			= ExAd_Open()

		If nRet <> IFCAD_ANY.AD_ERROR_SUCCESS Then

			MessageBox.Show( "ＡＤボードがオープンできません。" + ConvErrMessageAd( nRet ), "ボードエラー" )

			WriteLog( "", "LG", "Board_Open ＡＤオープンエラー：" + ConvErrMessageAd( nRet ) )

' 20140120 拡張基板が無くても動作するようにする
			err			+= 1
'			Return nRet

		End If



		nRet			= ExDa_Open( PCI3310 )

		If nRet <> IFCDA_ANY.DA_ERROR_SUCCESS Then

			MessageBox.Show( "ＤＡボード１がオープンできません。" + ConvErrMessageDa( nRet ), "ボードエラー" )

			WriteLog( "", "LG", "Board_Open ＤＡ１オープンエラー：" + ConvErrMessageDa( nRet ) )


' 20140120 拡張基板が無くても動作するようにする
			err			+= 1
'			Return nRet

		End If



		nRet			= ExDa_Open( LPC340416 )

		If nRet <> IFCDA_ANY.DA_ERROR_SUCCESS Then

			MessageBox.Show( "ＤＡボード２がオープンできません。" + ConvErrMessageDa( nRet ), "ボードエラー" )

			WriteLog( "", "LG", "Board_Open ＤＡ２オープンエラー：" + ConvErrMessageDa( nRet ) )


' 20140120 拡張基板が無くても動作するようにする
			err			+= 1
'			Return nRet

		End If



		nRet			= ExGpib_Open( GPIBNo, GPIBadMY, GPIB1DLM, GPIB1TOUT )

		If nRet <> IFCGPIB_ANY.GPIB_SUCCESS Then

			MessageBox.Show( "ＧＰＩＢボードがオープンできません。" + ConvErrMessageGpib( nRet ), "ボードエラー" )

			WriteLog( "", "LG", "Board_Open ＧＰＩＢオープンエラー：" + ConvErrMessageGpib( nRet ) )


' 20140120 拡張基板が無くても動作するようにする
			err			+= 1
'			Return nRet

		End If



		If err = 0 Then

			WriteLog( "", "LG", "Board_Open ボードオープン完了" )

		End If


		Return 0

	End Function




	Public Sub Board_Close()

		'ボードの終了
		Dim nRet		As Integer



		nRet			= ExDio_Close()

		nRet			= ExAd_Close()

		nRet			= ExDa_Close( PCI3310 )

		nRet			= ExDa_Close( LPC340416 )

		nRet			= ExGpib_End()


		WriteLog( "", "LG", "Board_Close ボードクローズ完了" )

	End Sub



	Public Sub Da_Borad_Init()


		'
		'	ＤＡボードのレンジ設定
		'

		'
		'	AOX01 EXAO1 (PCI-3310 VOUT1)		PID･RSP設定
		'	ユニポーラ 0～+5V
		'
		ExDa_SetRange( PIDaoRSP, IFCDA_ANY.DA_0_5V )

		'
		'	AOX02 EXAO1 (PCI-3310 VOUT2)		MFC･CONTROL1
		'	ユニポーラ 0～+5V
		'
		ExDa_SetRange( MFCaoSETPT1, IFCDA_ANY.DA_0_5V )

		'
		'	AOX03 EXAO1 (PCI-3310 VOUT3)		ESC-2000･CH1出力電圧設定
		'	バイポーラ -10～+10V
		'
		ExDa_SetRange( ESCaoVOLT1, IFCDA_ANY.DA_10V )
 
		'
		'	AOX04 EXAO1 (PCI-3310 VOUT4)		ESC-2000･CH2出力電圧設定
		'	バイポーラ -10～+10V
		'
		ExDa_SetRange( ESCaoVOLT2, IFCDA_ANY.DA_10V )
 
		'
		'	AOX05 EXAO2 (PCI-340416 AOUT1)	サーモチラー･ﾘﾓｰﾄCH1温度設定
		'	バイポーラ -10～+10V
		'
		ExDa_SetRange( SCRaoREMOTE1, IFCDA_ANY.DA_10V )

		'
		'	AOX06 EXAO2 (PCI-340416 AOUT2)	サーモチラー･ﾘﾓｰﾄCH2温度設定
		'	バイポーラ -10～+10V
		'
		ExDa_SetRange( SCRaoREMOTE2, IFCDA_ANY.DA_10V )

		'
		'	AOX07 EXAO2 (PCI-340416 AOUT3)	MFC･CONTROL2
		'	バイポーラ -10～+10V
		'
		'	※2014-01-09 現時点でMFC2は未使用(未接続)
		'
		ExDa_SetRange( MFCaoSETPT2, IFCDA_ANY.DA_10V )



	End Sub



	'/*****
	'	指定ＤＡ・指定電圧を出力
	'*****/
	Public Sub AoPutV				_
	(						_
		ByVal ch		As Integer,	_
		ByVal volt		As Double	_
	)

		Dim raw			As Short
		Dim wkV			As Double



		'
		'	電圧値をＲＡＷデータに変換
		'
		Select Case ch

		Case	PIDaoRSP

			' ＰＩＤ調節計ＲＳＰ
			raw			= cvtp2PIDset( volt, wkV )


		Case	MFCaoSETPT1

			' ＭＦＣ１流量設定信号
			raw			= cvtf2MFCset( volt, wkV )


		Case	ESCaoVOLT1, ESCaoVOLT2 

			' ＥＳＣ 1ch,2ch 出力電圧設定信号
			raw			= cvtv2ESC( volt, wkV )


		Case	SCRaoREMOTE1, SCRaoREMOTE2

			' サ－モチラ－ ch1,ch2 温度設定
			raw			= cvtt2SCR( volt, wkV )


		Case	MFCaoSETPT2

			' ＭＦＣ２流量設定信号

			' ※2014-01-09 現時点でMFC2は未使用(未接続)
			raw			= cvtf2MFCset2( volt, wkV )


		End Select


		'
		'	指定ＡＯ・ＲＡＷデータを出力
		'
		ExDa_Output( ch, raw )


	End Sub


End Module
