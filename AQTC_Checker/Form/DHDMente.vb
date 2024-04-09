

Imports InterfaceCorpDllWrap


'*****
'	メンテナンスフォームクラス
'*****
Public Class DHDMente


	Dim ADptrBak As Integer

	Dim DOXBTN()	As Button

	Private Sub DHDMoni_FormClosed							_
	(										_
		sender			As Object,					_
		e			As System.Windows.Forms.FormClosedEventArgs	_
	)	Handles Me.FormClosed



		WriteLog(　"", "LG", "DHDMoni_FormClosed メンテ終了"　)


		'　メニューに戻る
		PROGID			= "DHDMenu"


    End Sub



	Private Sub DHDMoni_Load				_
	(							_
		sender			As Object,		_
		e			As System.EventArgs	_
	)	Handles Me.Load



		PROGID			= "DHDMente"

		WriteLog( "", "LG", "DHDMoni_Load メンテ開始" )


		' ＤＯ出力クリア
		ExDio_Output_AllClear()


		'
		'	表示の初期設定
		'


		'
		'	PIDのRSP設定
		'
		AoPutV( PIDaoRSP, 0.0 )

		VAOX01.Text		= "0.0V"

		HAOX01.Text		= anav2r2( 0.0 ).ToString( "X" ) + "h"

		AOX01.Text		= "0.0"


		'
		'	ＭＦＣ１
		'
		AoPutV( MFCaoSETPT1, 0.0 )

		VAOX02.Text		= "0.0V"

		HAOX02.Text		= anav2r2( 0.0 ).ToString( "X" ) + "h"

		AOX02.Text		= "0.0"


		'
		'	ESC電源1CH
		'
		AoPutV( ESCaoVOLT1, 0.0 )

		VAOX03.Text		= "0.0V"

		HAOX03.Text		= anav2r1( 0.0 ).ToString( "X" ) + "h"

		AOX03.Text		= "0.0"



		'
		'	ESC電源2CH
		'
		AoPutV( ESCaoVOLT2, 0.0 )

		VAOX04.Text		= "0.0V"

		HAOX04.Text		= anav2r1( 0.0 ).ToString( "X" ) + "h"

		AOX04.Text		= "0.0"



		'
		'	サ－モチラ－CH1
		'
		AoPutV( SCRaoREMOTE1, 0.0 )

		VAOX05.Text		= "0.0V"

		HAOX05.Text		= anav2r1( 0.0 ).ToString( "X" ) + "h"

		AOX05.Text		= "0.0"


		'
		'	サ－モチラ－CH2
		'
		AoPutV( SCRaoREMOTE2, 0.0 )

		VAOX06.Text		= "0.0V"

		HAOX06.Text		= anav2r1( 0.0 ).ToString( "X" ) + "h"

		AOX06.Text		= "0.0"



		'
		'	マスフローコントローラ２
		'	※2014-01-09 現時点でMFC2は未使用(未接続)
		'
		AoPutV( MFCaoSETPT2, 0.0 )

		VAOX07.Text		= "0.0V"

		HAOX07.Text		= anav2r1( 0.0 ).ToString( "X" ) + "h"

		AOX07.Text		= "0.0"


		'
		'	ＡＤリングバッファの初期化
		'
		' intai_ini()


		DOXBTN		=									_
		{											_
			Nothing,									_
			DOX01, DOX02, DOX03, DOX04, DOX05, DOX06, DOX07, DOX08, DOX09, DOX10,		_
			DOX11, DOX12, DOX13, DOX14, DOX15, DOX16, DOX17, DOX18, DOX19, DOX20,		_
			DOX21, DOX22, DOX23, DOX24, DOX25, DOX26, DOX27, DOX28, DOX29, DOX30,		_
			DOX31, DOX32, DOX33, DOX34, DOX35, DOX36, DOX37, DOX38				_
		}

		'
		'	タイマー開始
		'
		TimINP.Enabled		= True

	End Sub


	'*****
	'	20200903 y.goto
	'	DOXとボタンの状態を更新する
	'	MB, G4のインターロック追加の為、ボタンの状態とDOXの状態の食い違いを無くす
	'*****
	Private Sub RefsDOXBtn()

		Dim i			As Integer

		For i = 1 To 38

			With DOXBTN( i )

				If OutBuf( i ) Then

					.BackColor	= Color.Orange
					.Text		= "ON"

				Else

					.BackColor	= Color.WhiteSmoke
					.Text		= "OFF"

				End If
			End With

		Next i

	End Sub

	Private Sub DOX01_Click					_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles						_
		DOX01.Click,					_
		DOX02.Click,					_
		DOX03.Click,					_
		DOX04.Click,					_
		DOX05.Click,					_
		DOX06.Click,					_
		DOX19.Click,					_
		DOX18.Click,					_
		DOX17.Click,					_
		DOX16.Click,					_
		DOX15.Click,					_
		DOX14.Click,					_
		DOX13.Click,					_
		DOX10.Click,					_
		DOX09.Click,					_
		DOX08.Click,					_
		DOX07.Click,					_
		DOX30.Click,					_
		DOX29.Click,					_
		DOX28.Click,					_
		DOX27.Click,					_
		DOX26.Click,					_
		DOX25.Click,					_
		DOX24.Click,					_
		DOX23.Click,					_
		DOX22.Click,					_
		DOX21.Click,					_
		DOX20.Click,					_
		DOX31.Click,					_
		DOX12.Click,					_
		DOX11.Click,					_
		DOX32.Click,					_
		DOX33.Click,					_
		DOX34.Click,					_
		DOX35.Click,					_
		DOX36.Click,					_
		DOX37.Click,					_
		DOX38.Click

		Dim btn			As Button = sender
		Dim No			As Integer
		Dim dlg			As New FrmWait


		dlg.DesktopLocation	= New Point( System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y )

		'設定値ファイル名を選択
		dlg.Show()

		No			= CInt( btn.Name.Substring( 3 ) )

		If OutBuf( No ) = DIO_ON Then

			ExDio_Output( No, DIO_OFF )

			btn.BackColor		= Color.WhiteSmoke

			btn.Text		= "OFF"

		Else

			ExDio_Output( No, DIO_ON )

			btn.BackColor		= Color.Orange

			btn.Text		= "ON"

		End If

		' DOXとボタンの状態を更新する
		RefsDOXBtn()

		dlg.Dispose()

	End Sub



	Private Sub AOX07_TextChanged				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles						_
		AOX07.TextChanged,				_
		AOX06.TextChanged,				_
		AOX05.TextChanged,				_
		AOX04.TextChanged,				_
		AOX03.TextChanged,				_
		AOX02.TextChanged,				_
		AOX01.TextChanged


		Dim txt			As TextBox = sender
		Dim No			As Integer



		No			= CInt( txt.Name.Substring( 3 ) )

		Dim Name		= "BAOX" + No.ToString( "00" )


		For Each grp In Me.Controls

			Dim btn			As Button = GetButtonFromGroupBox( Name, grp )

			If IsNothing( btn ) = False Then

				If btn.Name = Name Then

					If txt.Text = "" Then

						btn.Enabled		= False

					Else

						btn.Enabled		= True

					End If

					Exit Sub

				End If

			End If

		Next


	End Sub



	Private Sub BAOX01_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles						_
		BAOX07.Click,					_
		BAOX06.Click,					_
		BAOX05.Click,					_
		BAOX04.Click,					_
		BAOX03.Click,					_
		BAOX02.Click,					_
		BAOX01.Click


		Dim btn			As Button = sender
		Dim No			As Integer
		Dim dat			As Double



		btn.Enabled		= False

		No			= CInt( btn.Name.Substring( 4 ) )


		Dim grp			As Object
		Dim Name		As String = "AOX" + No.ToString( "00" )


		For Each grp In Me.Controls

			If grp.GetType = GetType( GroupBox ) Then

				Dim txt			As TextBox = GetTextBoxFromGroupBox( Name, grp )

				If IsNothing( txt ) = False Then

					If IsNumeric( txt.Text ) = False Then

						Exit For

					End If

					dat			= CDbl( txt.Text )


					Select Case	No

					Case	PIDaoRSP

						'
						'	ＰＩＤのＲＳＰ設定値
						'
						If dat < 0.0 Or dat > 13332.0 Then

							MessageBox.Show( "入力範囲は0～13332です。" )

							Exit For

						End If


					Case	MFCaoSETPT1

						'
						'	ＭＦＣ１の流量設定値
						'
						If dat < 0.0 Or dat > 50.0 Then

							MessageBox.Show( "入力範囲は0～50です。" )

							Exit For

						End If


					Case	ESCaoVOLT1, ESCaoVOLT2

						'
						'	ＥＳＣ電源ＣＨ１，ＣＨ２出力電圧設定値
						'
						If dat < -1000.0 Or dat > 1000.0 Then

							MessageBox.Show( "入力範囲は-1000～1000です。" )

							Exit For

						End If


					Case	SCRaoREMOTE1, SCRaoREMOTE2

						'
						'	サ－モチラ－ＣＨ１，ＣＨ２温度設定値
						'
						If dat < -100.0 Or dat > 100.0 Then

							MessageBox.Show( "入力範囲は-100～100です。" )

							Exit For

						End If


					Case	MFCaoSETPT2

						'
						'	ＭＦＣ２流量設定信号
						'	※2014-01-09 現時点でMFC2は未使用(未接続)
						'
						If dat < 0 Or dat > 50.0 Then

							MessageBox.Show( "入力範囲は0～50です。" )

							Exit For

						End If

					End Select


					Dim raw			As Short
					Dim volt		As Double


					Select Case	No

					Case	PIDaoRSP

						' ＰＩＤのＲＳＰ設定値をＤＡ出力値へ変換
						raw			= cvtp2PIDset( dat, volt )

					Case	MFCaoSETPT1

						' ＭＦＣ１の流量設定値をＤＡ出力値へ変換
						raw			= cvtf2MFCset( dat, volt )

					Case	ESCaoVOLT1, ESCaoVOLT2

						' ＥＳＣ　ＣＨ１，Ｃｈ２出力電圧指示値をＤＡ出力値へ変換
						raw			= cvtv2ESC( dat, volt )


					Case	SCRaoREMOTE1, SCRaoREMOTE2 

						' サ－モチラ－ＣＨ１，ＣＨ２温度設定値をＤＡ出力値へ変換
						raw			= cvtt2SCR( dat, volt )

					Case	MFCaoSETPT2

						' ＭＦＣ２流量設定値をＤＡ出力値へ変換
						' ※2014-01-09 現時点でMFC2は未使用(未接続)
						raw			= cvtf2MFCset2( dat, volt )

					End Select


					'
					'	ＤＡ出力
					'
					ExDa_Output( No, raw )


					Name			= "VAOX" + No.ToString( "00" )

					Dim lbl			As Label = GetLabelFromGroupBox( Name, grp )


					If IsNothing( lbl ) = False Then

						lbl.Text		= volt.ToString( "0.000" ) + "V"

					End If

					Name			= "HAOX" + No.ToString( "00" )

					lbl			= GetLabelFromGroupBox( Name, grp )

					If IsNothing( lbl ) = False Then

						lbl.Text		= raw.ToString( "X" ) + "h"

					End If

					Exit For

				End If

			End If

		Next


	End Sub



	Private Sub TimINP_Tick					_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles TimINP.Tick

		Dim Name		As String
		Dim lbl			As Label
		Dim grp			As Object



		' タイマー停止
		TimINP.Enabled		= False


		ExDio_Input()

	'	20200206 y.goto EXDIO2 に対応する
	'	For i = 1 To DInCH
		For i = 1 To 64

			Name			= "DIX" + i.ToString( "00" )

			For Each grp In Me.Controls

				If grp.GetType = GetType( GroupBox ) Then

					lbl			= GetLabelFromGroupBox( Name, grp )

					If IsNothing( lbl ) = False Then

						If InBuf( i ) = DIO_ON Then

							lbl.BackColor		= Color.Lime

						Else

							lbl.BackColor		= Color.LightGray

						End If

						Exit For

					End If

				End If

			Next

		Next


		' 20131216 メニューでＡＤ入力のためコメント
		'intad()


		' 入力ポインタの変化で入力確認
		If ADrbFUL = 1 And ADptrBak <> ADptrR Then

			ADptrBak		= ADptrR

			For i = 1 To ADnCH

				Name			= "HAIX" + i.ToString( "00" )

				For Each grp In Me.Controls

					If grp.GetType = GetType( GroupBox ) Then

						lbl			= GetLabelFromGroupBox( Name, grp)

						If IsNothing( lbl ) = False Then


							'
							'	リングバッファから移動平均取り出し
							'
							Dim dt			As UShort


							dt			= aiget( i, 1 )

							lbl.Text		= dt.ToString( "X" ) + "h"


							Dim adv			As Double = anar2v( dt )


							Name			= "VAIX" + i.ToString( "00" )

							lbl			= GetLabelFromGroupBox( Name, grp )

							If IsNothing( lbl ) = False Then

								lbl.Text		= adv.ToString( "0.000" ) + "V"

							End If


							Name			= "AIX" + i.ToString( "00" )

							lbl			= GetLabelFromGroupBox( Name, grp )

							If IsNothing( lbl ) = False Then

								Select Case	i

								Case	1

									' AIX01 バラトロン真空計
									Dim torr			As Double = Volt2Torr( adv )
									Dim pa				As Double = Torr2Pa( torr )

									lbl.Text			= mkpastrN3( pa )
							' 20200217	lbl.Text			= mkpastrN6( pa )
							' 20200207	lbl.Text			= pa.ToString( "0.00E+00" ) + "Pa"


								Case	2

									' AIX02 ピラニ真空計
									Dim pa				As Double = cvtv2p( adv )

									lbl.Text			= mkpastrN3( pa )
							' 20200217	lbl.Text			= mkpastrN6( pa )
							' 20200207	lbl.Text			= pa.ToString( "0.00E+00" ) + "Pa"


								Case	3

									' AIX03 ＭＦＣ１流量モニタ信号
									Dim ccm				As Double = cvtv2Ccm( adv )

									lbl.Text			= ccm.ToString( "0.00" ) + "CCM"


								Case	4, 5

									' ESC電源ＣＨ１ AIX04 ，ＣＨ２ AIX05 出力電圧モニタ
									lbl.Text			= ( adv * 100 ).ToString( "0" ) + "V"


								Case	6, 7

									' サ－モチラ－ＣＨ１，ＣＨ２温度モニタ信号
									lbl.Text			= ( adv * 10 ).ToString( "0.0" ) + "℃"

								End Select

							End If


							Exit For


						End If

					End If

				Next

			Next


			'
			'	流量の100回平均を表示
			'
			Dim dtm			As UShort

			dtm			= aiget( MFCaiFLW, MFCAVG )

			Dim advm		As Double = anar2v( dtm )

			Dim ccmm		As Double = cvtv2Ccm( advm )

			lblMFC.Text		= ccmm.ToString( "0.00" ) + "CCM"

		End If

		'
		'	20200213追加
		'	MBP動作状態表示を更新
		'
		With DOX38

			If OutBuf( EXSdoMBP ) = DIO_ON Then

				.BackColor	= Color.Orange

				.Text		= "ON"

			Else

				.BackColor	= Color.WhiteSmoke

				.Text		= "OFF"

			End If

		End With

		' タイマー開始
		TimINP.Enabled		= True


	End Sub



	'*****
	'	グループボックスの中のコントロールからnameの一致するラベルを取り出す。
	'*****
	Private Function GetLabelFromGroupBox		_
	(						_
		ByVal Name		As String,	_
		grp			As GroupBox	_
	)	As Label

		Dim obj			As Label = Nothing



		For Each lbl In grp.Controls

			If lbl.GetType = GetType( Label ) Then

				If CType( lbl, Label ).Name = Name Then

					obj			= lbl

					Exit For

				End If

			End If

		Next



		Return obj

	End Function



	'*****
	'	グループボックスの中のコントロールからnameの一致するテキストボックスを取り出す。
	'*****
	Private Function GetTextBoxFromGroupBox		_
	(						_
		ByVal Name		As String,	_
		grp			As GroupBox	_
	)	As TextBox

		Dim obj			As TextBox = Nothing


		For Each txt In grp.Controls

			If txt.GetType = GetType( TextBox ) Then

				If CType( txt, TextBox ).Name = Name Then

					obj			= txt

					Exit For

				End If

			End If

		Next


		Return obj

	End Function



	'*****
	'	グループボックスの中のコントロールからnameの一致するボタンを取り出す。
	'*****
	Private Function GetButtonFromGroupBox		_
	(						_
		ByVal Name		As String,	_
		grp			As GroupBox	_
	)	As Button

		Dim obj			As Button = Nothing


		For Each btn In grp.Controls

			If btn.GetType = GetType( Button ) Then

				If CType( btn, Button ).Name = Name Then

					obj			= btn

					Exit For

				End If

			End If

		Next


		Return obj

	End Function



End Class