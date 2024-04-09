
Imports System.Configuration


'*****
'	メニューフォームクラス
'*****
Public Class DHDMenu


	Dim BefMilSec		As Integer



	Private Sub Menu_FormClosed							_
	(										_
		sender			As Object,					_
		e			As System.Windows.Forms.FormClosedEventArgs	_
	)	Handles Me.FormClosed



		WriteLog( "", "LG", "AQTC Menu_FormClosed" )

		' ボードの終了
		Board_Close()


	End Sub



	Private Sub Menu_Load					_
	(							_
		sender			As Object,		_
		e			As System.EventArgs	_
	)	Handles Me.Load


		' 20200324 y.goto
		' コマンドライン引数をチェックして動作モードを設定する
		chkcmdline()

		' ログ表示初期処理
		FrmLog.Show
		FrmLog.LogDspInit()
	
		WriteLog( "", "LG", "AQTC Menu_Load" )

		FrmLog.LogDspAdd( "", "AQTC検査プログラム起動", Color.Empty )

		FrmLog.LogDspAdd( "", "Ver." + My.Application.Info.Version.ToString, Color.Empty )

		'******************* ２重起動 ********************

		' プロセスインスタンス
		Dim insProcess			As Process()

		' プロセス名
		Dim strProcessName		As String

		' プロセス名取得
		strProcessName			= Process.GetCurrentProcess.ProcessName

		' プロセスインスタンス取得
		insProcess			= Process.GetProcessesByName( strProcessName )

		' プロセスインスタンスが1以上の場合
		If UBound( insProcess ) > 0 Then

			WriteLog( "", "LG", "AQTC 二重起動で終了" )

			MessageBox.Show					_
			(						_
				"すでに起動しています",			_
				"確認してください",			_
				MessageBoxButtons.OK,			_
				MessageBoxIcon.Error			_
			)


			' 処理終了
			End

		End If

		'******************* ２重起動 ********************

		PROGID			= "DHDMenu"

		lblVer.Text		= "Ver." + My.Application.Info.Version.ToString

		'
		'	ログを設定期間で削除
		'
		Dim DayLimit		As Integer = CInt( My.Settings.LogDaylimit )


		If DayLimit <= 0 Then

			DayLimit		= 30

		End If

		DeleteLogFile( "", "LG", DayLimit )

		'
		cfgGpibIni()

		'ボードの初期化
		If Board_Open() <> 0 Then

			'ボード初期化エラー発生のため終了
			' 20140120 拡張基板が無くても動作するようにする
			' ここで終了することは無くなった
			End

		End If

		'
		'	ＡＤリングバッファの初期化
		'
		intai_ini()

		TimINP.Enabled		= True


		'
		'	ＤＯ出力クリア
		'
		ExDio_Output_AllClear()


		'
		'	ＤＡボードのレンジ設定
		'
		Da_Borad_Init()


		'
		'	ＤＡボード出力を０Ｖ
		'
		aoputv( PIDaoRSP, 0.0 )

		aoputv( MFCaoSETPT1, 0.0 )

		aoputv( ESCaoVOLT1, 0.0 )

		aoputv( ESCaoVOLT2, 0.0 )

		aoputv( SCRaoREMOTE1, 0.0 )

		aoputv( SCRaoREMOTE2, 0.0 )


		' ※2014-01-09 現時点でMFC2は未使用(未接続)
		aoputv( MFCaoSETPT2, 0.0 )

		FrmGraph.Show

		'
		'	20200324 y.goto
		'	デバックモード動作時はデバックウインドウを表示する
		'
		If DBGmode = 1 Then

			' デバックウインドウ表示
			FrmDBG.Show()

		End If

	End Sub



	Private Sub btnEnd_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnEnd.Click


		WriteLog( "", "LG", "AQTC btnEnd_Click" )

		Me.Close()


	End Sub



	Private Sub btnMente_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnMente.Click



		WriteLog("", "LG", "AQTC btnMente_Click")

		'Dim dlg As DHDMente
		'dlg = New DHDMente
		'dlg.ShowDialog()
		'dlg.Dispose()

		'
		'	20200909 y.goto
		'	デバックモード動作時はモードレス表示にする
		'
		If DBGmode = 1 Then

			DHDMente.Show()

		Else

			DHDMente.ShowDialog()

		End If

	End Sub



	Private Sub btnTst_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnTst.Click


		WriteLog( "", "LG", "AQTC btnTst_Click" )

		Dim dlg			As DHDtstAio


		dlg			= New DHDtstAio

		dlg.ShowDialog()

		dlg.Dispose()


	End Sub



	Private Sub btnAuto_Click				_
	(							_
		sender			As System.Object,	_
		e As			System.EventArgs	_
	)	Handles btnAuto.Click


		WriteLog( "", "LG", "AQTC btnAuto_Click" )

		DHDmd			= TST_AUTO

	'	DHDTest.ShowDialog()
		DHDTest.Show()

	End Sub



	Private Sub btnZetsuen_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnZetsuen.Click



		WriteLog( "", "LG", "AQTC btnZetsuen_Click" )

		DHDmd			= TST_ZETS

	'	DHDTest.ShowDialog()
		DHDTest.Show()



	End Sub



	Private Sub btnKyucyaku_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnKyucyaku.Click


		WriteLog("", "LG", "AQTC btnKyucyaku_Click")

		DHDmd			= TST_KYUC

	'	DHDTest.ShowDialog()
		DHDTest.Show()


	End Sub



	Private Sub btnHeGas_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnHeGas.Click


		WriteLog( "", "LG", "AQTC btnHeGas_Click" )

		DHDmd			= TST_HGAS

	'	DHDTest.ShowDialog()
		DHDTest.Show()


	End Sub




	'   20201102 s.harada
	'   AQTC対応
	Private Sub btnZanryu_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnZanryu.Click

		WriteLog("", "LG", "AQTC btnZanryu_Click")

		DHDmd			= TST_ZKYU

		DHDTest.Show()


	End Sub



	Private Sub btnParmSet_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnParmSet.Click


		WriteLog( "", "LG", "AQTC btnParmSet_Click" )

		DHDParmSet.ShowDialog()

		DHDParmSet.Dispose()


	End Sub



	Private Sub TimINP_Tick					_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles TimINP.Tick

		Dim frm			As Form
		Dim dtnow		As DateTime = DateTime.Now
		Dim itv			As Integer



		frm			= FrmGraph

		' ＡＤ入力
		If intad() Then

			'
			'	サンプリングデータ確定のタイミングでグラフも更新する
			'
			
			' 前回処理からの経過時間を計算
			If BefMilSec > dtnow.Millisecond Then

				itv			= 1000 - BefMilSec + dtnow.Millisecond

			Else

				itv			= dtnow.Millisecond - BefMilSec

			End If

'	debug.print( itv.tostring() + "msec" )


			BefMilSec		= dtnow.Millisecond


			If frm.Visible = True Then

				' グラフ表示更新処理
				FrmGraph.RefsGraph()

			End If

		End If

		' 20200908 追加
		' DOインターロックシーケンス
		DOILCseq()

	End Sub


	'*****
	'	ブザー停止ボタン・クリックイベント
	'*****
	Private Sub btnBzStop_Click				_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles btnBzStop.Click



		' ブザー停止
		PTLBzStop()


	End Sub




End Class