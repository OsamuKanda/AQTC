

Imports InterfaceCorpDllWrap
Imports System.Text
Imports System.IO



'*****
'	検査実行処理クラス
'*****
Public Class DHDTest

	Public Enum TestType
		None
		Resistance
		HeLeak
		Adsorption
	End Enum

	Dim dtZetsuen		As DataTable
	Dim dtKyucyaku		As DataTable
	Dim dtHeGas		As DataTable

	'   20201102 s.harada
	'  AQTC対応で残留吸着追加
	Dim dtZanryu		As DataTable


	Dim dtZetsu1		As DataTable
	Dim dtZetsu2		As DataTable
	Dim dtZetsu3		As DataTable
	Dim dtZetsu4		As DataTable
	Dim dtKyucy1		As DataTable
	Dim dtKyucy2		As DataTable
	Dim dtKyucy3		As DataTable
	Dim dtKyucy4		As DataTable
	Dim dtHeGas1		As DataTable
	Dim dtHeGas2		As DataTable
	Dim dtHeGas3		As DataTable
	Dim dtHeGas4		As DataTable

	'   20201102 s.harada
	'  AQTC対応で残留吸着追加
	Dim dtZanry1		As DataTable
	Dim dtZanry2		As DataTable
	Dim dtZanry3		As DataTable
	Dim dtZanry4		As DataTable

	Dim mTabPageNo		As Integer
	Dim selTabNo		As Integer

	Public tstNo		As Integer

	'   20201102 s.harada
	'  AQTC対応で14->16に変更
	Dim msgary( 16 )	As String

	Dim ADptrBak		As Integer




#Region "試験状態表示関係"



	'*****
	'	状態表示クリア
	'*****
	Public Sub StatusClear()

		Dim i			As Integer



		For i = 0 To msgary.Length - 1

			msgary( i )		= ""

		Next


		Dim dsp			As String = msgary( 0 )


		For i = 1 To msgary.Length - 1

			dsp			+= vbCrLf + msgary( i )

		Next

		lblStatus.Text		= dsp


	End Sub



	Public Sub StatusClear				_
	(						_
		ByVal lno		As Integer,	_
		ByVal lsiz		As Integer	_
	)

		Dim i			As Integer
		Dim max			As Integer



		If lsiz + lno > msgary.Length Then

			max			= msgary.Length

		Else

			max			= lno + lsiz

		End If


		For i = lno To max - 1

			msgary( i )		= ""

		Next


		Dim dsp			As String = msgary( 0 )


		For i = 1 To msgary.Length - 1

			dsp			+= vbCrLf + msgary( i )

		Next

		lblStatus.Text		= dsp


	End Sub



	Public Sub StatusDisp				_
	(						_
		ByVal x			As Integer,	_
		ByVal y			As Integer,	_
		ByVal msg		As String	_
	)

		Dim i			As Integer
		Dim bBuf()		As Byte
		Dim bMsg()		As Byte
		Dim siz 		As Integer
		Dim msiz		As Integer



		If y > msgary.Length - 1 Then

			Exit Sub

		End If


		siz			= Encoding.GetEncoding( "shift_jis" ).GetByteCount( msgary( y ) )

		If siz < x Then

			msgary( y )		+= Space( x - siz )

			msgary( y )		+= msg

		Else

			msiz			= Encoding.GetEncoding( "shift_jis" ).GetByteCount( msg )

			If siz < x + msiz Then

				msgary( y )		+= Space( x + msiz - siz )

			End If

			bBuf			= Encoding.GetEncoding( "shift_jis" ).GetBytes( msgary( y ) )

			bMsg			= Encoding.GetEncoding( "shift_jis" ).GetBytes( msg )


			For i = x To x + msiz - 1

				bBuf( i )		= bMsg( i - x )

			Next


			msgary( y )		= Encoding.GetEncoding( "shift_jis" ).GetString( bBuf )

		End If


		'msgary(y) = Space(x) + msg

		Dim dsp			As String = msgary( 0 )


		For i = 1 To msgary.Length - 1

			dsp			+= vbCrLf + msgary( i )

		Next


		lblStatus.Text		= dsp



	End Sub


#End Region



	'*****
	'	絶縁抵抗測定値を画面(DataGridView)表示
	'*****
	Public Sub setMesTaikiData			_
	(						_
		ByVal pos		As Integer,	_
		ByVal dno		As Integer	_
	)

		Dim dgv			As DataGridView
		Dim dt			As DataTable



		Select Case	tstNo

		Case	0

			dgv			= dgvZetsuen1

			dt			= dtZetsu1

		Case	1

			dgv			= dgvZetsuen2

			dt			= dtZetsu2

		Case	2

			dgv			= dgvZetsuen3

			dt			= dtZetsu3

		Case	3

			dgv			= dgvZetsuen4

			dt			= dtZetsu4

		Case Else

			Exit Sub

		End Select



		Dim mdt			As DTI1 = MESrec.dt( tstNo ).t1( pos ).d( dno )
		Dim rno			As Integer = 0
		Dim i			As Integer



		For i = 0 To pos - 1

			rno			+= MESrec.dt( tstNo ).t1( i ).dsiz

		Next

		rno			+= dno

		' 20140204 y.goto
		' 電流値はμA固定とする
		dt.Rows( rno ).Item( 2 )	= convAmp2uAmp( mdt.amp ) + " μA"

		' 20140204 y.goto
		' 抵抗値は MΩ 固定とする
		dt.Rows( rno ).Item( 3 )	= convOm2MOm( mdt.om ) + " MΩ"

		dt.Rows( rno ).Item( 6 )	= mdt.vq.ToString( "0.000 Pa" )




		'
		'	20140122 y.goto
		'	DataGridView 入力されたデータ位置へ選択行を移動させる
		'

		' タブを現在の試験へ切替える
		tabTest.SelectedIndex = tstNo

		' 測定実施した行へ移動させる
		Try

			dgv.CurrentCell			= dgv			_
			(							_
				1,						_
				rno						_
			)

		Catch

		End Try


		dgv.Refresh()



	End Sub



	Public Sub setMesKyucyaku			_
	(						_
		ByVal dno		As Integer	_
	)

		Dim dgv			As DataGridView
		Dim dt			As DataTable



		Select Case	tstNo

		Case	0

			dgv			= dgvKyuucyaku1

			dt			= dtKyucy1

		Case	1

			dgv			= dgvKyuucyaku2

			dt			= dtKyucy2

		Case	2

			dgv			= dgvKyuucyaku3

			dt			= dtKyucy3

		Case	3

			dgv			= dgvKyuucyaku4

			dt			= dtKyucy4

		Case Else

			Exit Sub

		End Select



		Dim mdt			As DTI2 = MESrec.dt(tstNo).t2.d(dno)
		Dim rno			As Integer = 0


		rno			+= dno

		' 20201102 S_Harada AQTC対応用に変更
		'dt.Rows( rno ).Item( 2 )	= mdt.pa( 0 ).ToString( "0.00" )

		'dt.Rows( rno ).Item( 3 )	= mdt.pa( 1 ).ToString( "0.00" )

		'dt.Rows( rno ).Item( 4 )	= mdt.pa( 2 ).ToString( "0.00" )

		'dt.Rows( rno ).Item( 6 )	= mdt.tmr.ToString( "0.0" )

		'If mdt.okng = 0 Or mdt.okng = 2 Then

		'	dt.Rows( rno ).Item( 7 )	= "合"

		'Else

		'	dt.Rows( rno ).Item( 7 )	= "否"

		'End If
		dt.Rows( rno ).Item( 3 )	= mdt.tmr( 0 ).ToString( "0." )

		dt.Rows( rno ).Item( 4 )	= mdt.tmr( 1 ).ToString( "0." )

		dt.Rows( rno ).Item( 5 )	= mdt.tmr( 2 ).ToString( "0." )

		dt.Rows( rno ).Item( 6 )	= mdt.tmr( 3 ).ToString( "0." )

		dt.Rows( rno ).Item( 7 )	= mdt.tmr( 4 ).ToString( "0." )

		If mdt.okng = 0 Then

			dt.Rows( rno ).Item( 9 )	= "合"

		ElseIf mdt.okng = 1  Then

			dt.Rows( rno ).Item( 9 )	= "否"

		Else

			dt.Rows( rno ).Item( 9 )	= "-"

		End If



		'
		'	20140131 y.goto
		'	DataGridView 入力されたデータ位置へ選択行を移動させる
		'

		' タブを現在の試験へ切替える
		tabTest.SelectedIndex = tstNo

		' 測定実施した行へ移動させる
		Try

			dgv.CurrentCell			= dgv			_
			(							_
				1,						_
				rno						_
			)

		Catch

		End Try


		dgv.Refresh()


	End Sub



	Public Sub setMesHeGas _
	(
		ByVal dno As Integer
	)

		Dim dgv As DataGridView
		Dim dt As DataTable



		Select Case tstNo

			Case 0

				dgv = dgvHeGas1

				dt = dtHeGas1

			Case 1

				dgv = dgvHeGas2

				dt = dtHeGas2

			Case 2

				dgv = dgvHeGas3

				dt = dtHeGas3

			Case 3

				dgv = dgvHeGas4

				dt = dtHeGas4

			Case Else

				Exit Sub

		End Select


		Dim mdt As DTI3 = MESrec.dt(tstNo).t3.d(dno)
		Dim rno As Integer = 0



		rno += dno

		' 20140107 小数点以下桁数変更
		' dt.Rows(rno).Item(2) = mdt.mfcvolt.ToString("0.00000")

		'	20201102 s.harada	AQTC対応で削除
		'dt.Rows( rno ).Item( 2 )	= mdt.mfcvolt.ToString( "0.00" )


		' 20201102 S_Harada 測定方法変更
		' 20140107 小数点以下桁数変更
		'▼2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
		'dt.Rows(rno).Item(2) = mdt.cm(0).ToString("0.00")
		If mdt.ptn.Contains("1") Then
			dt.Rows(rno).Item(2) = mdt.cm(0).ToString("0.00")
		Else
			dt.Rows(rno).Item(2) = "-"
		End If
		'▲2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

		If mdt.cm(1) > 0 Then

			'▼2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
			'dt.Rows(rno).Item(3) = mdt.cm(1).ToString("0.00")
			If mdt.ptn.Contains("2") Then
				dt.Rows(rno).Item(3) = mdt.cm(1).ToString("0.00")
			Else
				dt.Rows(rno).Item(3) = "-"
			End If
			'▲2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

		End If

		If mdt.cm(2) > 0 Then

			'▼2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
			'dt.Rows(rno).Item(4) = mdt.cm(2).ToString("0.00")
			If mdt.ptn.Contains("3") Then
				dt.Rows(rno).Item(4) = mdt.cm(2).ToString("0.00")
			Else
				dt.Rows(rno).Item(4) = "-"
			End If
			'▲2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

		End If

		If mdt.cm(3) > 0 Then

			'▼2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
			'dt.Rows(rno).Item(5) = mdt.cm(3).ToString("0.00")
			If mdt.ptn.Contains("4") Then
				dt.Rows(rno).Item(5) = mdt.cm(3).ToString("0.00")
			Else
				dt.Rows(rno).Item(5) = "-"
			End If
			'▲2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

		End If

		If mdt.cm(4) > 0 Then

			'▼2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
			'dt.Rows(rno).Item(6) = mdt.cm(4).ToString("0.00")
			If mdt.ptn.Contains("6") Then
				dt.Rows(rno).Item(6) = mdt.cm(4).ToString("0.00")
			Else
				dt.Rows(rno).Item(6) = "-"
			End If
			'▲2024.05.14 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

		End If



		' 20201102 S_Harada 判定無を追加、1kPaと2kPaを判定
		'If mdt.okng = 0 Then

		'	dt.Rows( rno ).Item( 5 )	= "合"

		'Else

		'	dt.Rows( rno ).Item( 5 )	= "否"

		'End If

		If mdt.okng = 0 Then

			dt.Rows(rno).Item(9) = "合"

		ElseIf mdt.okng = 1 Then

			dt.Rows(rno).Item(9) = "否"

		Else

			dt.Rows(rno).Item(9) = "-"

		End If

		If mdt.okng2 = 0 Then

			dt.Rows(rno).Item(10) = "合"

		ElseIf mdt.okng2 = 1 Then

			dt.Rows(rno).Item(10) = "否"

		Else

			dt.Rows(rno).Item(10) = "-"

		End If


		'
		'	20140122 y.goto
		'	DataGridView 入力されたデータ位置へ選択行を移動させる
		'

		' タブを現在の試験へ切替える
		tabTest.SelectedIndex = tstNo

		' 測定実施した行へ移動させる
		Try

			dgv.CurrentCell = dgv _
			(
				1,
				rno
			)

		Catch

		End Try


		dgv.Refresh()


		'dgv.Refresh()


	End Sub


	' 20200716 s.harada
	' トーカロ選択対応で新規作成
	Public Sub setMesZanryu				_
	(						_
		ByVal dno		As Integer	_
	)

		Dim dgv			As DataGridView
		Dim dt			As DataTable



		Select Case	tstNo

		Case	0

			dgv			= dgvZanryu1

			dt			= dtZanry1

		Case	1

			dgv			= dgvZanryu2

			dt			= dtZanry2

		Case	2

			dgv			= dgvZanryu3

			dt			= dtZanry3

		Case	3

			dgv			= dgvZanryu4

			dt			= dtZanry4

		Case Else

			Exit Sub

		End Select



		Dim mdt			As DTI4 = MESrec.dt( tstNo ).t4.d( dno )
		Dim rno			As Integer = 0


		rno			+= dno

		dt.Rows( rno ).Item( 2 )	= mdt.pc.ToString( "0.00" )

		dt.Rows( rno ).Item( 3 )	= mdt.pd.ToString( "0.00" )

		dt.Rows( rno ).Item( 4 )	= mdt.pdc.ToString( "0.00" )



		' 20201102 S_Harada 判定無を追加
		'If mdt.okng = 0 Or mdt.okng = 2 Then

		'	dt.Rows( rno ).Item( 6 )	= "合"

		'Else

		'	dt.Rows( rno ).Item( 6 )	= "否"

		'End If
		If mdt.okng = 0  Then

			dt.Rows( rno ).Item( 6 )	= "合"

		ElseIf mdt.okng = 1  Then

			dt.Rows( rno ).Item( 6 )	= "否"

		Else

			dt.Rows( rno ).Item( 6 )	= "-"

		End If


		' タブを現在の試験へ切替える
		tabTest.SelectedIndex = tstNo

		' 測定実施した行へ移動させる
		Try

			dgv.CurrentCell			= dgv			_
			(							_
				1,						_
				rno						_
			)

		Catch

		End Try


		dgv.Refresh()


	End Sub



#Region "フォーム"


	Private Sub DHDTest_FormClosed							_
	(										_
		sender			As Object,					_
		e			As System.Windows.Forms.FormClosedEventArgs	_
	)	Handles Me.FormClosed


		WriteLog( "", "LG", "DHDTest_FormClosed テスト終了" )

		SigEnd()

		'メニューに戻る
		PROGID			= "DHDMenu"


	End Sub



	Private Sub DHDTest_Load				_
	(							_
		sender			As Object,		_
		e			As System.EventArgs	_
	)	Handles Me.Load


		PROGID			= "DHDTest"


		' 測定モードを表示
		Dim tMod		As String = "範囲外"


		Select Case	DHDmd

		Case	TST_AUTO

			tMod			= "自動検査"

		Case	TST_ZETS

			tMod			= "絶縁抵抗測定"

		Case	TST_KYUC

			tMod			= "ウエハ吸着測定"

		Case	TST_HGAS

			tMod			= "Ｈｅリーク量測定"


		'   20201102 s.harada
		'   AQTC対応
		'
		Case	TST_ZKYU


			tMod			= "残留吸着力測定"


		End Select


		Me.Text			= Me.Text + " " + tMod


		WriteLog( "", "LG", "DHDTest_Load テスト開始 " + tMod )



		'
		'	画面サイズを最大化
		'
		If Me.WindowState <> FormWindowState.Maximized Then

			Me.WindowState		= FormWindowState.Maximized

		End If


		If Me.Width < 1500 Then

			panMoni.Width		= 300

		Else

			panMoni.Width		= 755

		End If


		panMoni.Left		= Me.Width - panMoni.Width - 20

		tabTest.Width		= panMoni.Left - 40

		grpParm.Enabled		= true

		pnlStatus.Visible	= False



		Dim dt			As DateTime = DateTime.Now


		txtYear.Text		= dt.Year

		txtMonth.Text		= dt.Month

		txtDay.Text		= dt.Day

		'
		'	20140123
		'	試験開始時間の項目を追加
		'
		txtStTime.Text		= dt.ToString( "HH:mm:ss" )

		txtFileName.Text	= ""

		txtTno.Text		= ""

		txtMNo.Text		= ""

		txtSNo.Text		= ""

		txtDSiz.Text		= ""

		cboDHead.Text		= ""

		cboDType.Text		= ""


		tstDataClear()


		'
		'   20200716 s.harada
		'	トーカロ対応
		'
		' 電極ヘッド種別の取得
		'GetTestFiletoCombbox( "", cboDHead )

		'   20201102 s.harada
		'   AQTC対応専用に変更
		'
		Dim strPath		As String

		strPath			= System.IO.Directory.GetCurrentDirectory() + "\testTcl"

		GetTestFiletoCombbox( strPath, cboDHead )


		' 電極種類の設定
		cboDType.Items.Clear()

		cboDType.Items.Add("Ｖノッチ")

		cboDType.Items.Add("オリフラ")


		btnStart.Enabled		= False

		btnStop.Enabled			= False

		StopFlag			= False



		' ＤＯ出力クリア
		ExDio_Output_AllClear()

		' ＤＩ入力
		ExDio_Input()

		' 機器の初期化
		SigIni()

		' 表示の初期設定
		' PIDの設定
		AOX01.Text			= "0.0V"

		' マスフローコントローラ
		AOX02.Text			= "0.0V"

		' ESC電源1CH
		AOX03.Text			= "0.0V"

		' ESC電源2CH
		AOX04.Text			= "0.0V"

		' サ－モチラ－CH1
		AOX05.Text			= "0.0"

		' サ－モチラ－CH2
		AOX06.Text			= "0.0"

		' マスフローコントローラ２
		AOX07.Text			= "0.0"


		' ＡＤリングバッファの初期化
		' intai_ini()


		' ダウンカウントタイマー開始
		timCnt.Enabled			= True

		' 入力タイマー開始()
		timINP.Enabled			= True

		' パトライト点滅、モニター表示タイマー開始
		timPtl.Enabled			= True


	End Sub


#End Region


#Region "コントロール"


	'*****
	'	ＡＤ入力、モニタ用タイマー
	'*****
	Private Sub timINP_Tick					_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles timINP.Tick



		' タイマー停止
		timINP.Enabled		= False


		' ＡＤ入力 20131216 メニューでＡＤ入力のためコメント
		' intad()


		' ＡＤ入力データあり
		' 入力ポインタの変化で入力確認
		If ADrbFUL = 1 And ADptrBak <> ADptrR Then

			ADptrBak		= ADptrR

			' ＡＤ入力状態を表示
			Ad_moni()

		End If


		' タイマー開始
		timINP.Enabled		= True


	End Sub



	'*****
	'	パトライト点滅、ＤＩ入力、ＤＯ、ＤＡモニタ用タイマー
	'*****
	Private Sub timPtl_Tick					_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles timPtl.Tick



		timPtl.Enabled		= False


		' パトライト点滅処理
		PTLControl()


		' ＤＩ入力
		ExDio_Input()


		' ＤＩ入力状態を表示
		Di_moni()


		' ＤＯ出力状態を表示
		Do_moni()


		' ＤＡ出力状態を表示
		Da_moni()



		timPtl.Enabled		= True


	End Sub



	'*****
	'	カウントダウンタイマー（１０ｍｓ）
	'*****
	Private Sub timCnt_Tick					_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles timCnt.Tick


		timDownCount()

	End Sub



	'*****
	'	テストデータ選択コンボボックス
	'*****
	Private Sub cboDHead_SelectedIndexChanged		_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles cboDHead.SelectedIndexChanged


		' 表示クリア
		tstDataClear()

		tabPageDisp()


		btnStart.Enabled	= False



		' テストデータのリード
		If GetTestData( cboDHead.Text, DHDmd ) < 0 Then

			MessageBox.Show( "テストデータが読み込めません", "確認", MessageBoxButtons.OK )

			Exit Sub

		End If


		If MESrec.type = "" Then

			MessageBox.Show( "テストデータが読み込めません", "確認", MessageBoxButtons.OK )

			Exit Sub

		End If


		' テスト項目の表示
		tstDataDisp()

		tabPageDisp()


		btnStart.Enabled		= True

	End Sub



	'*****
	'	タブコントロール選択
	'*****
	Private Sub tabTest_SelectedIndexChanged		_
	(							_
		sender			As Object,		_
		e			As System.EventArgs	_
	)	Handles tabTest.SelectedIndexChanged


		' タブ変更時の表示
		Dim tp			As TabPage


		If mTabPageNo = tabTest.SelectedIndex Then

			Exit Sub

		End If


		Select Case	tabTest.SelectedIndex

		Case	0

			tp			= tabPage1

		Case	1

			tp			= tabPage2

		Case	2

			tp			= tabPage3

		Case	3

			tp			= tabPage4

		Case Else

			tp			= tabPage1

		End Select



		' テスト項目があればタブ表示、なければ元のタブを表示
		If tp.Text <> "" Then

			mTabPageNo		= tabTest.SelectedIndex

		Else

			Select Case	mTabPageNo

			Case	0

				tabTest.SelectedTab	= tabPage1

			Case	1

				tabTest.SelectedTab	= tabPage2

			Case	2

				tabTest.SelectedTab	= tabPage3

			Case	3

				tabTest.SelectedTab	= tabPage4

			End Select

		End If


	End Sub



	'*****
	'	開始ボタン処理
	'*****
	Private Sub btnStart_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnStart.Click
		Dim pcdate		As DateTime



		'
		'	検査開始ＰＣ日時
		'
		pcdate			= Now


		'
		'	測定データファイル名（パス、拡張子を含まない）
		'
		TstRstFname		= pcdate.ToString( "yyyyMMdd_HHmmss" ) + "_" + txtFileName.Text

		'
		'	ファイル保存フォルダパス（測定データファイル、波形記録ファイル）
		'
		TstRstFolpth		= MkFldName( TstRstFname )

		'
		'	測定データファイルパス
		'
		TstRstFpath		= MkDatFName( TstRstFname )



		' 保存ファイル名の確認
		If txtFileName.Text = "" Then

			flipprc( OPIPok, "保存ファイル名を入力してください。" )

			Exit Sub

		End If


		If File.Exists( TstRstFpath ) = True Then

			If flipprc( OPIPyn, "ファイルが存在します。上書きしますか？" ) <> DialogResult.Yes Then

				Exit Sub

			End If

		End If


		Try

			'
			'	データ格納フォルダの作成
			'

			If Dir( TstRstFolpth, vbDirectory ) = "" Then

				MkDir( TstRstFolpth )

			End If

		Catch

			'
			'	配管接続変更メッセージを出力してオペレータ入力を待つ
			'
			flipbz1r( OPIPok, "データ格納フォルダが作成出来ませんでした" )

			return

		End Try


		' btnStart.Enabled = False
		' btnStart.Visible = False


		'
		'	パラメータ位置に測定状態を表示
		'
		grpParm.Enabled		= False

		pnlStatus.Top		= grpParm.Top

		pnlStatus.Left		= grpParm.Left

		pnlStatus.Width		= tabTest.Width

		pnlStatus.Height	= grpParm.Height

		pnlStatus.Visible	= True


		lblStatus.Text		= ""


		StatusClear()


		btnStop.Enabled		= True

		btnEnd.Enabled		= False


		' ウエハ吸着力測定リミット値
		' 20201102 s.harada	トーカロ専用に変更で削除
		'BPRS			= CDbl( txtBPRS.Text ) * 1000

		TPRS1			= CDbl( txtTPRS1.Text )

		TPRS2			= CDbl( txtTPRS2.Text )

		BakPres			= CDbl( txtBakPres.Text )


		'
		'	20140127 y.goto
		'	電極ヘッド温度安定待ち時間 (分)
		'
		PrmTmpStbW		= CDbl( txtTmpStbW.Text )


		'   20201102 s.harada
		'   AQTC対応専用に変更
		'
		'   残留吸着 Ｈｅ流量(ccm)
		PrmHeFlow		= CDbl(TxtHeFlow.Text)

		'   残留吸着 電圧印加時間(秒)
		PrmVoltImp		= CDbl(TxtVoltImp.Text)

		'   残留吸着 電圧印加停止後Ｈｅ流すまでの待ち時間(秒)
		PrmHeWait		= CDbl(TxtHeWait.Text)


		' 20201102 s.harada　追加

		' 吸吸着力測定時間上限（秒）
		PrmMaxTim		= CDbl(TxtMaxTime.Text)

		' 残留吸着測定安定判断基準傾き
		PrmStabVct		= CDbl(txtAntVct.Text)

		' 残留吸着測定安定判断基準安定時間（秒）
		PrmStabTim		= CDbl(txtAntTim.Text)

		' 残留吸着電圧印可時裏面圧力 (Pa)
		PrmBakPrs		= CDbl( txtBakPrs.Text )

		' 20210201 追加 y.goto
		' 電圧印可後He導入前裏面圧力 (Pa)
		PrmBak2Prc		= CDbl( txtBakP2.Text )

		MESrec.type		= cboDType.Text

		MESrec.dh.tno		= txtTno.Text

		MESrec.dh.mno		= txtMNo.Text

		MESrec.dh.sno		= txtSNo.Text

		MESrec.dh.inc		= txtDSiz.Text

		MESrec.dh.vo		= cboDHead.Text

		' 20140123
		' 検査開始時間を追加する
		MESrec.dh.sdt		=	_
					txtYear.Text + "/" +		_
					txtMonth.Text + "/" +		_
					txtDay.Text + " " +		_
					txtStTime.Text

		' 20140123 追加
		' 試験開始ＰＣシステムデイト
		MESrec.dh.pcdt		= pcdate.ToString()

		MESrec.dh.okng		= -1


		'
		'	測定メイン
		'
		Dim okng		As Integer

		okng			= ProcMain()


		'
		'	試験結果をファイルに書出す
		'
		SaveResultData()


		'
		'	試験結果総合判定
		'

		'check( okng )

		btnStop.Enabled		= False

		btnEnd.Enabled		= True


		StatusDisp( 20, 10, "「メニューへ」ボタンを押してメニューに戻ります" )


	End Sub



	'*****
	'	中断ボタン処理
	'*****
	Private Sub btnStop_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnStop.Click


		' btnStop.Enabled = False

		StopFlag		= True


	End Sub



	'*****
	'	終了ボタン処理
	'*****
	Private Sub btnEnd_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnEnd.Click


		Me.Close()


	End Sub


#End Region


#Region "モニター関係"



	'*****
	'	デジタル入力状態を表示
	'*****
	Private Sub Di_moni()

		Dim i			As Integer
		Dim lbl			As Label


		For i = 1 To 10

			Select Case	i

			Case	1

				lbl			= DIX01

			Case	2

				lbl			= DIX02

			Case	3

				lbl			= DIX03

			Case	4

				lbl			= DIX04

			Case	5

				lbl			= DIX05

			Case	6

				lbl			= DIX06

			Case	7

				lbl			= DIX07

			Case	8

				lbl			= DIX08

			Case	9

				lbl			= DIX09

			Case	10

				lbl			= DIX10

			Case Else

				lbl			= Nothing

			End Select


			If InBuf( i ) = DIO_ON Then

				lbl.BackColor		= Color.Lime

			Else

				lbl.BackColor		= Color.WhiteSmoke

			End If

		Next

		' 20200210 EXDIO2基板追加対応
		For i = 33 To 41

			Select Case	i

			Case	33

				lbl			= DIX33

			Case	34

				lbl			= DIX34

			Case	35

				lbl			= DIX35

			Case	36

				lbl			= DIX36

			Case	37

				lbl			= DIX37

			Case	38

				lbl			= DIX38

			Case	39

				lbl			= DIX39

			Case	40

				lbl			= DIX40

			Case	41

				lbl			= DIX41

			Case Else

				lbl			= Nothing

			End Select


			If InBuf( i ) = DIO_ON Then

				lbl.BackColor		= Color.Lime

			Else

				lbl.BackColor		= Color.WhiteSmoke

			End If

		Next

	End Sub




	'*****
	'	デジタル出力状態を表示
	'*****
	Private Sub Do_moni()

		Dim i			As Integer
		Dim lbl			As Label

		For i = 1 To 32

			Select Case	i

			Case	1

				lbl			= DOX01

			Case	2

				lbl			= DOX02
			Case	3

				lbl			= DOX03

			Case	4

				lbl			= DOX04

			Case	5

				lbl			= DOX05

			Case	6

				lbl			= DOX06

			Case	7

				lbl			= DOX07

			Case	8

				lbl			= DOX08

			Case	9

				lbl			= DOX09

			Case	10

				lbl			= DOX10

			Case	11

				lbl			= DOX11

			Case	12

				lbl			= DOX12

			Case	13

				lbl			= DOX13

			Case	14

				lbl			= DOX14

			Case	15

				lbl			= DOX15

			Case	16

				lbl			= DOX16

			Case	17

				lbl			= DOX17

			Case	18

				lbl			= DOX18

			Case	19

				lbl			= DOX19

			Case	20

				lbl			= DOX20

			Case	21

				lbl			= DOX21

			Case	22

				lbl			= DOX22

			Case	23

				lbl			= DOX23

			Case	24

				lbl			= DOX24

			Case	25

				lbl			= DOX25

			Case	26

				lbl			= DOX26

			Case	27

				lbl			= DOX27

			Case	28

				lbl			= DOX28

			Case	29

				lbl			= DOX29

			Case	30

				lbl			= DOX30

			Case	31

				lbl			= DOX31

			Case	32

				lbl			= DOX32

			Case Else

				lbl			= Nothing

			End Select


			If OutBuf( i ) = DIO_ON Then

				lbl.BackColor		= Color.Orange

			Else

				lbl.BackColor		= Color.WhiteSmoke

			End If

		Next


		' 20200210 EXDIO2基板追加対応
		For i = 33 To 38

			Select Case	i

			Case	33

				lbl			= DOX33

			Case	34

				lbl			= DOX34

			Case	35

				lbl			= DOX35

			Case	36

				lbl			= DOX36

			Case	37

				lbl			= DOX37

			Case	38

				lbl			= DOX38

			Case Else

				lbl			= Nothing

			End Select

			If OutBuf( i ) = DIO_ON Then

				lbl.BackColor		= Color.Orange

			Else

				lbl.BackColor		= Color.WhiteSmoke

			End If

		Next

	End Sub




	'*****
	'	アナログ入力状態を表示
	'*****
	Private Sub Ad_moni()

		Dim i			As Integer



		For i = 1 To 7

			' リングバッファから移動平均取り出し
			Dim dt			As UShort


			dt			= aiget( i, 1 )


			Dim adv			As Double = anar2v( dt )


			Select Case	i

			Case	1

				' バラトロン真空計
				Dim torr		As Double = Volt2Torr( adv )
				Dim pa			As Double = Torr2Pa( torr )

				AIX01.Text		= mkpastrN3( pa )
			'
			'	20200217 y.goto 指数表示から通常表示へ
			'	AIX01.Text		= pa.ToString( "0.00E+00" ) + "Pa"
			'

			Case	2

				' ピラニ真空計
				Dim pa			As Double = cvtv2p( adv )

				AIX02.Text		= mkpastrN3( pa )
			'
			'	20200217 y.goto 指数表示から通常表示へ
			'	AIX02.Text		= pa.ToString( "0.00E+00" ) + "Pa"
			'

			Case	3

				' ＭＦＣ１流量モニタ信号
				Dim ccm			As Double = cvtv2Ccm( adv )

				AIX03.Text		= ccm.ToString( "0.0" ) + "CCM"


			Case	4

				' ESC-CH1 出力電圧モニタ
				AIX04.Text		= ( adv * 100 ).ToString( "0" ) + "V"


			Case	5 

				' ESC-CH2 出力電圧モニタ
				AIX05.Text		= ( adv * 100 ).ToString( "0" ) + "V"


			Case	6 

				' サ－モチラ－CH1温度モニタ電圧出力
				AIX06.Text		= ( adv * 10 ).ToString( "0.0" ) + "℃"

			Case	7

				' サ－モチラ－CH2温度モニタ電圧出力
				AIX07.Text		= ( adv * 10 ).ToString( "0.0" ) + "℃"

			End Select

		Next

	End Sub



	'*****
	'	アナログ出力状態を表示
	'*****
	Private Sub Da_moni()

		Dim i			As Integer
		Dim dav			As Double



		For i = 1 To 7

			Select Case	i

			Case	PIDaoRSP

				' ＰＩＤ１リモートセットポイント（ＲＳＰ）

				' 0-5Vに変換
				dav			= anar2v( 2, DaBuf( i ) )

				Dim pa			As Double = cvtv2GM_Pa( dav )

				AOX01.Text		= pa.ToString( "0.000E+00" ) + "Pa"


			Case	MFCaoSETPT1

				' ＭＦＣ１流量設定信号

				' 0-5Vに変換
				dav			= anar2v( 2, DaBuf( i ) )

				Dim ccm			As Double = cvtv2Ccm( dav )

				AOX02.Text		= ccm.ToString( "0.0" ) + "CCM"


			Case	ESCaoVOLT1 

				' ＥＳＣ 1ch

				' ±10Vに変換
				dav			= anar2v( 1, DaBuf( i ) )

				AOX03.Text		= ( dav * 100 ).ToString( "0" ) + "V"


			Case	ESCaoVOLT2

				' ＥＳＣ 2ch

				' ±10Vに変換
				dav			= anar2v( 1, DaBuf( i ) )

				AOX04.Text		= ( dav * 100 ).ToString( "0" ) + "V"


			Case	SCRaoREMOTE1

				' サ－モチラ－ch1

				' ±10Vに変換
				dav			= anar2v( 1, DaBuf( i ) )

				AOX05.Text		= ( dav * 10 ).ToString( "0.0" ) + "℃"


			Case	SCRaoREMOTE2

				' サ－モチラ－ch2

				' ±10Vに変換
				dav			= anar2v( 1, DaBuf( i ) )

				AOX06.Text		= ( dav * 10 ).ToString( "0.0" ) + "℃"


			Case	MFCaoSETPT2

				'ＭＦＣ２ ※2014-01-09 現時点でMFC2は未使用(未接続)

				' ±10Vに変換
				dav			= anar2v( 1, DaBuf( i ) )

				Dim ccm			As Double = cvtv2Ccm( dav )

				AOX07.Text		= ccm.ToString( "0.0" ) + "CCM"

			End Select

		Next


	End Sub



#End Region


#Region "データ表示関係"



	Private Sub tstDataClear()



		' ウエハ吸着力測定リミット値
		' 20201102 s.harada	AQTC対応専用に変更で削除
		'txtBPRS.Text		= ""

		txtTPRS1.Text		= ""

		txtTPRS2.Text		= ""

		txtBakPres.Text		= ""

		'   20200716 s.harada
		'	トーカロ対応
		TxtHeFlow.Text		= ""

		TxtVoltImp.Text		= ""

		TxtHeWait.Text		= ""



		'
		'	データテーブル作成
		'
		dtZetsuen		= New DataTable

		createTblZetsuenn( dtZetsuen )

		dtKyucyaku		= New DataTable

		createTblKyuucyaku( dtKyucyaku )

		dtHeGas			= New DataTable

		createTblHeGas( dtHeGas )

		'	20201102 s.harada	AQTC対応専用で追加
		dtZanryu		= New DataTable

		createTblZanryu( dtZanryu )


	
		'
		'	絶縁テスト項目の設定
		'
		If DHDmd = TST_AUTO Or DHDmd = TST_ZETS Then

			tstZetsuDgvDispClear( 0, dtZetsuen, dgvZetsuen1, grpZetsuen1, True )

			tstZetsuDgvDispClear( 1, dtZetsuen, dgvZetsuen2, grpZetsuen2, True )

			tstZetsuDgvDispClear( 2, dtZetsuen, dgvZetsuen3, grpZetsuen3, True )

			tstZetsuDgvDispClear( 3, dtZetsuen, dgvZetsuen4, grpZetsuen4, True )

		Else

			tstZetsuDgvDispClear( 0, dtZetsuen, dgvZetsuen1, grpZetsuen1, False )

			tstZetsuDgvDispClear( 1, dtZetsuen, dgvZetsuen2, grpZetsuen2, False )

			tstZetsuDgvDispClear( 2, dtZetsuen, dgvZetsuen3, grpZetsuen3, False )

			tstZetsuDgvDispClear( 3, dtZetsuen, dgvZetsuen4, grpZetsuen4, False )

		End If



		'
		'	吸着テスト項目の設定
		'
		If DHDmd = TST_AUTO Or DHDmd = TST_KYUC Then

			tstKyuuDgvDispClear( 1, dtKyucyaku, dgvKyuucyaku2, grpKyuucyaku2, True )

			tstKyuuDgvDispClear( 2, dtKyucyaku, dgvKyuucyaku3, grpKyuucyaku3, True )

		Else

			tstKyuuDgvDispClear( 1, dtKyucyaku, dgvKyuucyaku2, grpKyuucyaku2, False )

			tstKyuuDgvDispClear( 2, dtKyucyaku, dgvKyuucyaku3, grpKyuucyaku3, False )

		End If


		tstKyuuDgvDispClear( 0, dtKyucyaku, dgvKyuucyaku1, grpKyuucyaku1, False )

		tstKyuuDgvDispClear( 3, dtKyucyaku, dgvKyuucyaku4, grpKyuucyaku4, False )



		'
		'	Ｈｅリーク量テスト項目の設定
		'
		If DHDmd = TST_AUTO Or DHDmd = TST_HGAS Then

			tstHeGasDgvDispClear( 1, dtHeGas, dgvHeGas2, grpHeGas2, True )

			tstHeGasDgvDispClear( 2, dtHeGas, dgvHeGas3, grpHeGas3, True )

		Else

			tstHeGasDgvDispClear( 1, dtHeGas, dgvHeGas2, grpHeGas2, False )

			tstHeGasDgvDispClear( 2, dtHeGas, dgvHeGas3, grpHeGas3, False )

		End If


		tstHeGasDgvDispClear( 0, dtHeGas, dgvHeGas1, grpHeGas1, False )

		tstHeGasDgvDispClear( 3, dtHeGas, dgvHeGas4, grpHeGas4, False )


	'	20201102 s.harada
	'	AQTC対応で残留吸着テスト項目追加
		If DHDmd = TST_AUTO Or DHDmd = TST_ZKYU Then

			tstZanryuDgvDispClear( 1, dtZanryu, dgvZanryu2, grpZanryu2, True )

			tstZanryuDgvDispClear( 2, dtZanryu, dgvZanryu3, grpZanryu3, True )

		Else

			tstZanryuDgvDispClear( 1, dtZanryu, dgvZanryu2, grpZanryu2, False )

			tstZanryuDgvDispClear( 2, dtZanryu, dgvZanryu3, grpZanryu3, False )

		End If

		tstZanryuDgvDispClear( 0, dtZanryu, dgvZanryu1, grpZanryu1, False )

		tstZanryuDgvDispClear( 3, dtZanryu, dgvZanryu4, grpZanryu4, False )


		'
		'	テスト項目の表示処理
		'
		Dim zdsp		As Integer
		Dim kdsp		As Integer
		Dim hdsp		As Integer


		If DHDmd = TST_AUTO Or DHDmd = TST_ZETS Then

			zdsp			= 1

		End If


		If DHDmd = TST_AUTO Or DHDmd = TST_KYUC Then

			kdsp			= 1

		End If


		If DHDmd = TST_AUTO Or DHDmd = TST_HGAS Then

			hdsp			= 1

		End If


	'	20201102 s.harada
	'	AQTC対応に変更

		Dim zkdsp As Integer

		If DHDmd = TST_AUTO Or DHDmd = TST_ZKYU Then

			zkdsp		= 1

		End If

		'tabGroupReSize( 0, tabPage1, grpZetsuen1, zdsp, grpKyuucyaku1, 0, grpHeGas1, 0 )

		'tabGroupReSize( 1, tabPage2, grpZetsuen2, zdsp, grpKyuucyaku2, kdsp, grpHeGas2, hdsp )

		'tabGroupReSize( 2, tabPage3, grpZetsuen3, zdsp, grpKyuucyaku3, kdsp, grpHeGas3, hdsp )

		'tabGroupReSize( 3, tabPage4, grpZetsuen4, zdsp, grpKyuucyaku4, 0, grpHeGas4, 0 )

		'▼ 2024.04.11 TC Kanda （４．測定の順番変更）
		'tabGroupReSize(0, tabPage1, grpZetsuen1, zdsp, grpKyuucyaku1, 0, grpHeGas1, 0, grpZanryu1, 0)

		'tabGroupReSize(1, tabPage2, grpZetsuen2, zdsp, grpKyuucyaku2, kdsp, grpHeGas2, hdsp, grpZanryu2, zkdsp)

		'tabGroupReSize(2, tabPage3, grpZetsuen3, zdsp, grpKyuucyaku3, kdsp, grpHeGas3, hdsp, grpZanryu3, zkdsp)

		'tabGroupReSize(3, tabPage4, grpZetsuen4, zdsp, grpKyuucyaku4, 0, grpHeGas4, 0, grpZanryu4, 0)
		tabGroupReSize(0, tabPage1, grpZetsuen1, zdsp, grpHeGas1, 0, grpKyuucyaku1, 0, grpZanryu1, 0)
		tabGroupReSize(1, tabPage2, grpZetsuen2, zdsp, grpHeGas2, hdsp, grpKyuucyaku2, kdsp, grpZanryu2, zkdsp)
		tabGroupReSize(2, tabPage3, grpZetsuen3, zdsp, grpHeGas3, hdsp, grpKyuucyaku3, kdsp, grpZanryu3, zkdsp)
		tabGroupReSize(3, tabPage4, grpZetsuen4, zdsp, grpHeGas4, 0, grpKyuucyaku4, 0, grpZanryu4, 0)
		'▲ 2024.04.11 TC Kanda （４．測定の順番変更）


	End Sub



	Private Sub tstDataDisp()



	'	20201102 s.harada
	’	AQTC対応で削除
	'	ウエハ吸着力測定リミット値 (kPaで表示)
	'	txtBPRS.Text		= ( BPRS / 1000 ).ToString( "0.00" )
	'
	'	20200217 y.goto
	'	ウエハ吸着力測定で指定ウエハ裏面圧が小数点以下が消されていた
	'	txtBPRS.Text		= ( BPRS / 1000 ).ToString( "00" )
	'

		txtTPRS1.Text		= TPRS1.ToString( "0.00" )

		txtTPRS2.Text		= TPRS2.ToString( "0.00" )

		txtBakPres.Text		= BakPres.ToString( "0" )


		'
		'	20140127 y.goto
		'	電極ヘッド温度安定待ち時間 (分)
		'
		txtTmpStbW.Text		= PrmTmpStbW.ToString( "0." )


	'	20201102 s.harada
	'	AQTC対応で追加

		'   残留吸着 Ｈｅ流量(ccm)
		TxtHeFlow.Text		= PrmHeFlow.ToString("0.")

		'   残留吸着 電圧印加時間(秒)
		TxtVoltImp.Text		= PrmVoltImp.ToString("0.")

		'   残留吸着 電圧印加停止後Ｈｅ流すまでの待ち時間(秒)
		TxtHeWait.Text		= PrmHeWait.ToString("0.")

		' 吸吸着力測定時間上限（秒）
		TxtMaxTime.Text		= PrmMaxTim.ToString("0.")

		' 残留吸着測定安定判断基準傾き
		txtAntVct.Text		= PrmStabVct.ToString("0.0")

		' 残留吸着測定安定判断基準安定時間（秒）
		txtAntTim.Text		= PrmStabTim.ToString("0.")

		' 残留吸着電圧印可時裏面圧力 (Pa)
		txtBakPrs.Text		= PrmBakPrs.ToString( "0." )

		' 20210201 追加 y.goto
		' 電圧印可後He導入前裏面圧力 (Pa)
		txtBakP2.Text		= PrmBak2Prc.ToString( "0." )

		'
		'	データテーブル作成
		'
		dtZetsuen		= New DataTable

		createTblZetsuenn( dtZetsuen )


		dtKyucyaku		= New DataTable

		createTblKyuucyaku( dtKyucyaku )


		dtHeGas			= New DataTable

		createTblHeGas( dtHeGas )


	'	20200716 s.harada
	'	トーカロ対応で追加
		dtZanryu		= New DataTable

		createTblZanryu( dtZanryu )


		dtZetsu1		= dtZetsuen.Copy

		dtZetsu2		= dtZetsuen.Copy

		dtZetsu3		= dtZetsuen.Copy

		dtZetsu4		= dtZetsuen.Copy


		dtKyucy1		= dtKyucyaku.Copy

		dtKyucy2		= dtKyucyaku.Copy

		dtKyucy3		= dtKyucyaku.Copy

		dtKyucy4		= dtKyucyaku.Copy


		dtHeGas1		= dtHeGas.Copy

		dtHeGas2		= dtHeGas.Copy

		dtHeGas3		= dtHeGas.Copy

		dtHeGas4		= dtHeGas.Copy

	'	20201102 s.harada
	'	AQTC対応で追加
		'   残留吸着
		dtZanry1		= dtZanryu.Copy

		dtZanry2		= dtZanryu.Copy

		dtZanry3		= dtZanryu.Copy

		dtZanry4		= dtKyucyaku.Copy



		'
		'	大気テスト項目の設定
		'
		tstZetsuDgvDisp( 0, dtZetsu1, MESrec, dgvZetsuen1, grpZetsuen1 )

		tstKyuuDgvDisp( 0, dtKyucy1, MESrec, dgvKyuucyaku1, grpKyuucyaku1, ESCmd )

		tstHeGasDgvDisp( 0, dtHeGas1, MESrec, dgvHeGas1, grpHeGas1, ESCmd )

	'	20201102 s.harada
	'	AQTC対応で追加
		tstZanryuDgvDisp( 0, dtZanry1, MESrec, dgvZanryu1, grpZanryu1, ESCmd )


		'
		'	真空（低温）テスト項目の設定
		'
		tstZetsuDgvDisp( 1, dtZetsu2, MESrec, dgvZetsuen2, grpZetsuen2 )

		tstKyuuDgvDisp( 1, dtKyucy2, MESrec, dgvKyuucyaku2, grpKyuucyaku2, ESCmd )

		tstHeGasDgvDisp( 1, dtHeGas2, MESrec, dgvHeGas2, grpHeGas2, ESCmd )

	'	20201102 s.harada
	'	AQTC対応で追加
		tstZanryuDgvDisp( 1, dtZanry2, MESrec, dgvZanryu2, grpZanryu2, ESCmd )


		'
		'	真空（高温)テスト項目の設定
		'
		tstZetsuDgvDisp( 2, dtZetsu3, MESrec, dgvZetsuen3, grpZetsuen3 )

		tstKyuuDgvDisp( 2, dtKyucy3, MESrec, dgvKyuucyaku3, grpKyuucyaku3, ESCmd )

		tstHeGasDgvDisp( 2, dtHeGas3, MESrec, dgvHeGas3, grpHeGas3, ESCmd )

	'	20201102 s.harada
	'	AQTC対応で追加
		tstZanryuDgvDisp( 2, dtZanry3, MESrec, dgvZanryu3, grpZanryu3, ESCmd )



		'
		'	真空（高温２）テスト項目の設定
		'
		tstZetsuDgvDisp( 3, dtZetsu4, MESrec, dgvZetsuen4, grpZetsuen4 )

		tstKyuuDgvDisp( 3, dtKyucy4, MESrec, dgvKyuucyaku4, grpKyuucyaku4, ESCmd )

		tstHeGasDgvDisp( 3, dtHeGas4, MESrec, dgvHeGas4, grpHeGas4, ESCmd )

	'	20201102 s.harada
	'	AQTC対応で追加
		tstZanryuDgvDisp( 3, dtZanry4, MESrec, dgvZanryu4, grpZanryu4, ESCmd )


		'
		'	テスト項目の表示処理
		'
		'	20201102 s.harada
		'	 AQTC対応で変更
		'tabGroupReSize( 0, tabPage1, grpZetsuen1, grpKyuucyaku1, grpHeGas1 )

		'tabGroupReSize( 1, tabPage2, grpZetsuen2, grpKyuucyaku2, grpHeGas2 )

		'tabGroupReSize( 2, tabPage3, grpZetsuen3, grpKyuucyaku3, grpHeGas3 )

		'tabGroupReSize( 3, tabPage4, grpZetsuen4, grpKyuucyaku4, grpHeGas4 )

		'▼ 2024.04.11 TC Kanda （４．測定の順番変更）
		'tabGroupReSize(0, tabPage1, grpZetsuen1, grpKyuucyaku1, grpHeGas1, grpZanryu1)

		'tabGroupReSize( 1, tabPage2, grpZetsuen2, grpKyuucyaku2, grpHeGas2, grpZanryu2 )

		'tabGroupReSize( 2, tabPage3, grpZetsuen3, grpKyuucyaku3, grpHeGas3, grpZanryu3 )

		'tabGroupReSize( 3, tabPage4, grpZetsuen4, grpKyuucyaku4, grpHeGas4, grpZanryu4 )

		tabGroupReSize(0, tabPage1, grpZetsuen1, grpHeGas1, grpKyuucyaku1, grpZanryu1)

		tabGroupReSize(1, tabPage2, grpZetsuen2, grpHeGas2, grpKyuucyaku2, grpZanryu2)

		tabGroupReSize(2, tabPage3, grpZetsuen3, grpHeGas3, grpKyuucyaku3, grpZanryu3)

		tabGroupReSize(3, tabPage4, grpZetsuen4, grpHeGas4, grpKyuucyaku4, grpZanryu4)
		'▲ 2024.04.11 TC Kanda （４．測定の順番変更）



	End Sub



	'   20201102 s.harada
	'   AQTC対応で残留吸着追加のため未使用
	Private Sub tabGroupReSize				_
	(							_
		ByVal no		As Integer,		_
		ByRef tabPage		As TabPage,		_
		ByRef zgrp		As GroupBox,		_
		ByVal zct		As Integer,		_
		ByRef kgrp		As GroupBox,		_
		ByVal kct		As Integer,		_
		ByRef hgrp		As GroupBox,		_
		ByVal hct		As Integer		_
	)

		'no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		'tabPage:タブページ
		'zgrp:絶縁耐圧表示グループボックス
		'zct:絶縁耐圧　データ数　＞０で表示
		'kgrp:吸着測定表示グループボックス
		'kct:吸着測定　データ数　＞０で表示
		'hgrp:Heリーク量測定表示グループボックス
		'hct:Heリーク量測定　データ数　＞０で表示

		Dim ct			As Integer = 0
		Dim hight		As Integer = tabPage.Height



		If zct > 0 Then

			ct			+= 1

		End If


		If kct > 0 Then

			ct			+= 1

		End If


		If hct > 0 Then

			ct			+= 1

		End If


		If ct = 1 Then

			If zct > 0 Then

				zgrp.Height		= hight - 20

			ElseIf kct > 0 Then

				kgrp.Height		= hight - 20

				kgrp.Top		= zgrp.Top

			ElseIf hct > 0 Then

				hgrp.Height		= hight - 20

				hgrp.Top		= zgrp.Top

			End If

		ElseIf ct = 2 Then

			Dim h			As Integer = ( hight - 35 ) / 2


			If zct > 0 Then

				zgrp.Height		= h

				If kct > 0 Then

					kgrp.Height		= h

					kgrp.Top		= zgrp.Top + h + 15

				Else

					hgrp.Height		= h

					hgrp.Top		= zgrp.Top + h + 15

				End If

			ElseIf kct > 0 Then

				kgrp.Height		= h

				kgrp.Top		= zgrp.Top

				hgrp.Height		= h

				hgrp.Top		= kgrp.Top + h + 15

			End If

		ElseIf ct = 3 Then


			Dim h			As Integer = ( hight - 50 ) / 7

			zgrp.Height		= h * 3

			kgrp.Height		= h * 2

			hgrp.Height		= h * 2

			kgrp.Top		= zgrp.Top + zgrp.Height + 15

			hgrp.Top		= kgrp.Top + kgrp.Height + 15

		End If


		If ct = 0 Then

			tabPage.Text		= ""

		Else

			tabPage.Text		= convFunikiToStr(no)

		End If



	End Sub



	'   20201102 s.harada
	'   AQTC対応で残留吸着追加
	Private Sub tabGroupReSize			_
	(						_
		ByVal no		As Integer,	_
		ByRef tabPage		As TabPage,	_
		ByRef zgrp		As GroupBox,	_
		ByVal zct		As Integer,	_
		ByRef kgrp		As GroupBox,	_
		ByVal kct		As Integer,	_
		ByRef hgrp		As GroupBox,	_
		ByVal hct		As Integer,	_
		ByRef zkgrp		As GroupBox,	_
		ByVal zkct		As Integer	_
	)

		'no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		'tabPage:タブページ
		'zgrp:絶縁耐圧表示グループボックス
		'zct:絶縁耐圧　データ数　＞０で表示
		'kgrp:吸着測定表示グループボックス
		'kct:吸着測定　データ数　＞０で表示
		'hgrp:Heリーク量測定表示グループボックス
		'hct:Heリーク量測定　データ数　＞０で表示
		'zkgrp:残留吸着測定表示グループボックス
		'zkct:残留吸着測定　データ数　＞０で表示

		Dim ct			As Integer = 0
		Dim hight		As Integer = tabPage.Height



		If zct > 0 Then

			ct			+= 1

		End If


		If kct > 0 Then

			ct			+= 1

		End If


		If hct > 0 Then

			ct			+= 1

		End If


		If zkct > 0 Then

			ct			+= 1

		End If


		If ct = 1 Then

			If zct > 0 Then

				zgrp.Height		= hight - 10

			ElseIf kct > 0 Then

				kgrp.Height		= hight - 10

				kgrp.Top		= zgrp.Top

			ElseIf hct > 0 Then

				hgrp.Height		= hight - 10

				hgrp.Top		= zgrp.Top

			Else

				zkgrp.Height		= hight - 10

				zkgrp.Top		= zgrp.Top

			End If

		ElseIf ct = 2 Then

			Dim h			As Integer = (hight - 20) / 2


			If zct > 0 Then

				zgrp.Height		= h

				If kct > 0 Then

					kgrp.Height		= h

					kgrp.Top		= zgrp.Top + h + 10

				ElseIf hct > 0 Then

					hgrp.Height		= h

					hgrp.Top		= zgrp.Top + h + 10

				Else

					zkgrp.Height		= h

					zkgrp.Top		= zgrp.Top + h + 10

				End If

			ElseIf kct > 0 Then

				kgrp.Height		= h

				kgrp.Top		= zgrp.Top

				If hct > 0 Then

					hgrp.Height		= h

					hgrp.Top		= kgrp.Top + h + 10

				Else

					zkgrp.Height		= h

					zkgrp.Top		= kgrp.Top + h + 10

				End If

			Else

				hgrp.Height		= h

				hgrp.Top		= zgrp.Top

				zkgrp.Height		= h

				zkgrp.Top		= hgrp.Top + h + 10

			End If

		ElseIf ct = 3 Then

			Dim h			As Integer = (hight - 30) / 3

			If zct > 0 Then

				zgrp.Height		= h

				If kct > 0 Then

					kgrp.Height		= h

					kgrp.Top		= zgrp.Top + h + 10

					If hct > 0 Then

						hgrp.Height		= h

						hgrp.Top		= kgrp.Top + h + 10

					Else

						zkgrp.Height		= h

						zkgrp.Top		= kgrp.Top + h + 10

					End If

				Else

					hgrp.Height		= h

					hgrp.Top		= zgrp.Top + h + 10

					zkgrp.Height		= h

					zkgrp.Top		= hgrp.Top + h + 10

				End If

			Else

				kgrp.Height		= h

				kgrp.Top		= zgrp.Top

				hgrp.Height		= h

				hgrp.Top		= kgrp.Top + h + 10

				zkgrp.Height		= h

				zkgrp.Top		= hgrp.Top + h + 10

			End If

		ElseIf ct = 4 Then

			Dim h As Integer		= (hight - 40) / 10

			zgrp.Height		= h * 3

			kgrp.Height		= h * 3

			hgrp.Height		= h * 2

			zkgrp.Height		= h * 2

			kgrp.Top		= zgrp.Top + zgrp.Height + 10

			hgrp.Top		= kgrp.Top + kgrp.Height + 10

			zkgrp.Top		= hgrp.Top + hgrp.Height + 10

		End If


		If ct = 0 Then

			tabPage.Text		= ""

		Else

			tabPage.Text		= convFunikiToStr(no)

		End If



	End Sub


	'   20201102 s.harada
	'   AQTC対応で残留吸着追加のため未使用
	Private Sub tabGroupReSize			_
	(						_
		ByVal no		As Integer,	_
		ByRef tabPage		As TabPage,	_
		ByRef zgrp		As GroupBox,	_
		ByRef kgrp		As GroupBox,	_
		ByRef hgrp		As GroupBox	_
	)

		'no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		'tabPage:タブページ
		'zgrp:絶縁耐圧表示グループボックス
		'kgrp:吸着測定表示グループボックス
		'hgrp:Heリーク量測定表示グループボックス

		Dim ct			As Integer = 0
		Dim hight		As Integer = tabPage.Height
		Dim zct			As Integer
		Dim kct			As Integer
		Dim hct			As Integer



		Select Case	no

		Case	0

			zct		= dtZetsu1.Rows.Count

			kct		= dtKyucy1.Rows.Count

			hct		= dtHeGas1.Rows.Count

		Case	1

			zct		= dtZetsu2.Rows.Count

			kct		= dtKyucy2.Rows.Count

			hct		= dtHeGas2.Rows.Count

		Case	2

			zct		= dtZetsu3.Rows.Count

			kct		= dtKyucy3.Rows.Count

			hct		= dtHeGas3.Rows.Count

		Case	3

			zct		= dtZetsu4.Rows.Count

			kct		= dtKyucy4.Rows.Count

			hct		= dtHeGas4.Rows.Count

		End Select


		If zct > 0 Then

			ct		+= 1

		End If


		If kct > 0 Then

			ct		+= 1

		End If


		If hct > 0 Then

			ct		+= 1

		End If


		If ct = 1 Then

			If zct > 0 Then

				zgrp.Height		= hight - 10

			ElseIf kct > 0 Then

				kgrp.Height		= hight - 10

				kgrp.Top		= zgrp.Top

			ElseIf hct > 0 Then

				hgrp.Height		= hight - 10

				hgrp.Top		= zgrp.Top

			End If

		ElseIf ct = 2 Then

			Dim h			As Integer = ( hight - 20 ) / 2

			If zct > 0 Then

				zgrp.Height		= h

				If kct > 0 Then

					kgrp.Height		= h

					kgrp.Top		= zgrp.Top + h + 10

				Else

					hgrp.Height		= h

					hgrp.Top		= zgrp.Top + h + 10

				End If

			ElseIf kct > 0 Then

				kgrp.Height		= h

				kgrp.Top		= zgrp.Top

				hgrp.Height		= h

				hgrp.Top		= kgrp.Top + h + 10

			End If

		ElseIf ct = 3 Then

			Dim h			As Integer = (hight - 30) / 7


			zgrp.Height		= h * 3

			kgrp.Height		= h * 2

			hgrp.Height		= h * 2

			kgrp.Top		= zgrp.Top + zgrp.Height + 10

			hgrp.Top		= kgrp.Top + kgrp.Height + 10

		End If


		If ct = 0 Then

			tabPage.Text		= ""

		Else

			tabPage.Text		= convFunikiToStr( no )

		End If


	End Sub



	'   20201102 s.harada
	'   AQTC対応で残留吸着追加
	'▼ 2024.04.11 TC Kanda （４．測定の順番変更）
	'Private Sub tabGroupReSize _
	'(
	'	ByVal no As Integer,
	'	ByRef tabPage As TabPage,
	'	ByRef zgrp As GroupBox,
	'	ByRef kgrp As GroupBox,
	'	ByRef hgrp As GroupBox,
	'	ByRef zkgrp As GroupBox
	')
	Private Sub tabGroupReSize _
	(
		ByVal no As Integer,
		ByRef tabPage As TabPage,
		ByRef zgrp As GroupBox,
		ByRef hgrp As GroupBox,
		ByRef kgrp As GroupBox,
		ByRef zkgrp As GroupBox
	)
		'▲ 2024.04.11 TC Kanda （４．測定の順番変更）

		'no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		'tabPage:タブページ
		'zgrp:絶縁耐圧表示グループボックス
		'kgrp:吸着測定表示グループボックス
		'hgrp:Heリーク量測定表示グループボックス
		'zkgrp:残留吸着測定表示グループボックス


		Dim ct As Integer = 0
		Dim hight As Integer = tabPage.Height
		Dim zct As Integer
		Dim kct As Integer
		Dim hct As Integer
		Dim zkct As Integer


		Select Case no

			Case 0

				zct = dtZetsu1.Rows.Count

				kct = dtKyucy1.Rows.Count

				hct = dtHeGas1.Rows.Count

				zkct = dtZanry1.Rows.Count

			Case 1

				zct = dtZetsu2.Rows.Count

				kct = dtKyucy2.Rows.Count

				hct = dtHeGas2.Rows.Count

				zkct = dtZanry2.Rows.Count

			Case 2

				zct = dtZetsu3.Rows.Count

				kct = dtKyucy3.Rows.Count

				hct = dtHeGas3.Rows.Count

				zkct = dtZanry3.Rows.Count

			Case 3

				zct = dtZetsu4.Rows.Count

				kct = dtKyucy4.Rows.Count

				hct = dtHeGas4.Rows.Count

				zkct = dtZanry4.Rows.Count

		End Select


		'▼ 2024.04.11 TC Kanda （４．測定の順番変更）
		'tabGroupReSize(no, tabPage, zgrp, zct, kgrp, kct, hgrp, hct, zkgrp, zkct)
		tabGroupReSize(no, tabPage, zgrp, zct, hgrp, hct, kgrp, kct, zkgrp, zkct)
		'▲ 2024.04.11 TC Kanda （４．測定の順番変更）


	End Sub


	Private Sub tabPageDisp()


		' テスト項目があればタブ表示、なければ元のタブを表示
		If tabPage1.Text <> "" Then

			tabTest.SelectedTab	= tabPage1

		ElseIf tabPage2.Text <> "" Then

			tabTest.SelectedTab	= tabPage2

		ElseIf tabPage3.Text <> "" Then

			tabTest.SelectedTab	= tabPage3

		ElseIf tabPage4.Text <> "" Then

			tabTest.SelectedTab	= tabPage4

		Else

			tabTest.SelectedTab	= tabPage1

		End If


		mTabPageNo		= tabTest.SelectedIndex


	End Sub


#End Region


#Region "測定関係"



	'*****
	'	測定メイン
	'*****
	Private Function ProcMain() As Integer

		Dim ret			As Integer = 0
		Dim sts			As Integer



		' 試験中画面表示
		StatusClear()


		'
		'	サ－モチラ－を遠隔操作モードに設定
		'
		SCRextCTL()

		'	※ 20200901 本バルブはトーカロ様では未装着
		'	ウエハ裏面圧開放バルブON
		'
		ExDio_Output( MAEdoPRG, DIO_ON )

		'
		'	20200220 y.goto トーカロ様対応
		'	ウエハ裏面圧開放バルブ SV3(G4) ON
		'
		ExDio_Output( EXSdoRYE3, DIO_ON )


		'
		'	20200310 y.goto トーカロ様設備対応
		'
		' start

			' MV=開 SV1=開 RYE1=ON DOX33=ON
			ExDio_Output( EXSdoRYE1,	DIO_ON )

			' G1=閉 SV2=閉 RYE2=OFF DOX34=OFF
			ExDio_Output( EXSdoRYE2,	DIO_OFF )

			' G4=開 SV3=閉 RYE3=ON DOX35=ON
			ExDio_Output( EXSdoRYE3,	DIO_ON )

			' LV=閉 SV4=OFF RYE4=OFF DOX36=OFF
			ExDio_Output( EXSdoRYE4,	DIO_OFF )

		' end


		'
		'	パトライト点灯
		'
		PTLctl			= PTLctlYELon



		' ログ表示リストビューに１件追加
		FrmLog.LogDspAdd( "", "検査開始", Color.Blue )


		'
		'	検査処理
		'
		Do


			'
			'	大気圧測定
			'
			tstNo			= 0

			WriteLog( "", "LG", "tst_proc_0" )

			If tst_proc( 0, MESrec.dt( tstNo ), 0, BakPres ) < 0 Then

				ret			= -1

				Exit Do

			End If



			'
			'	真空低温測定
			'
			tstNo			= 1

			WriteLog( "", "LG", "tst_proc_1" )

			If tst_proc( 1, MESrec.dt( tstNo ), TPRS1, BakPres ) < 0 Then

				ret			= -2

				Exit Do

			End If



			'
			'	真空高温測定
			'
			tstNo			= 2

			WriteLog( "", "LG", "tst_proc_2" )

			If tst_proc(1, MESrec.dt(tstNo), TPRS2, BakPres) < 0 Then

				ret = -3

				Exit Do

			End If



			'
			'	真空高温２測定
			'
			tstNo			= 3

			WriteLog( "", "LG", "tst_proc_3" )

			If tst_proc( 1, MESrec.dt( tstNo ), TPRS2, BakPres ) < 0 Then

				ret			= -4

				Exit Do

			End If


		Loop While False

		'   20200716 s.harada
		'	トーカロ対応
		WriteLog( "", "LG", "tst_proc_end" )


		'
		'	ＥＳＣ電源の終了
		'
		ESCstop( 20000, 0 )


		'
		'	ｲﾝﾀｰﾛｯｸ信号 OFF
		'
		ExDio_Output( ESCdoITL1, DIO_OFF )

		ExDio_Output( ESCdoITL2, DIO_OFF )



		'
		'	外部起動信号 OFF
		'
		ExDio_Output( ESCdoSTART1, DIO_OFF )

		ExDio_Output( ESCdoSTART2, DIO_OFF )


		'
		'	PID運転停止
		'
		ExDio_Output( MAEdoPIDSTART, DIO_OFF )


		'
		'	MFC SET PTに接続されているﾊﾟｿｺﾝAO出力を0Vに設定
		'
		AoPutV( MFCaoSETPT1, 0.0 )


		'
		'	強制OPEN を解除
		'
		ExDio_Output( MAEdoFOPN, DIO_OFF )


		'
		'	強制CLOSE を解除
		'
		ExDio_Output( MAEdoFCLS, DIO_OFF )


		'
		'	SET PT入力を パソコンＡＯと接続
		'
		ExDio_Output( MAEdoRYFC, DIO_OFF )


		'
		'	電極をモノポール、電極ヘッドと絶縁抵抗計を接続
		'
		RyHvMode( POL_MON, MES_IOS )

		'	※ 20200901 本バルブはトーカロ様では未装着
		'	ウエハ裏面圧開放バルブON
		'
		ExDio_Output( MAEdoPRG, DIO_ON )

		'
		'	トーカロ様設備対応
		'
		' ベント処理 20201207 y.goto サーモチラー使用CH把握の為、パラメータ追加
		VENTproc( 1, 0 )

		
		'
		'	総合判定
		'
		sts			= judgeOkNg()

		MESrec.dh.okng		= sts



		'
		'	パトライト・オペレータ確認要求表示Ａ
		'
		PTLOpeReqTypeAon()


		If ret = 0 Then

			StatusDisp( 20, 8, "検査終了" )

		Else

			StatusDisp( 20, 8, "中断により終了" )

		End If

		' ログ表示リストビューに１件追加
		FrmLog.LogDspAdd( "", "検査終了", Color.Blue )

		'
		'	20210308 y.goto 試験終了時にサーモチラーを停止させる
		'	サ－モチラ－の運転停止
		'
		ExDio_Output( MAEdoS1RUN, DIO_OFF )
		ExDio_Output( MAEdoS2RUN, DIO_OFF )
		WaitTim( 10 )


		flipbz1r( OPIPyn, "検査終了" )


		'
		'	パトライトＯＦＦ
		'
		PTLctl			= 0


		'
		'	パトライト・オペレータ確認要求表示Ａ終了
		'
		PTLOpeReqTypeAoff()


		' 試験中画面消去
		StatusClear( 2, 21 )



		Return sts

    End Function



	'*****
	'	総合判定
	'*****
	Private Function judgeOkNg() As Integer

		Dim i			As Integer
		Dim j			As Integer
		Dim okng		As Integer



		okng			= 0


		'
		'	吸着力試験結果チェック
		'
		For i = 0 To 3

			For j = 0 To MESrec.dt( i ).t2.dsiz - 1

				If MESrec.dt( i ).t2.d( j ).okng >= 0 Then

					okng			= MESrec.dt( i ).t2.d( j ).okng

					If okng = 1 Then

						Return 1

					End If

				End If

			Next

		Next



		'
		'	Ｈｅリーク量測定結果チェック
		'
		For i = 0 To 3

			For j = 0 To MESrec.dt( i ).t3.dsiz - 1

				If MESrec.dt( i ).t3.d( j ).okng >= 0 Then

					okng			= MESrec.dt( i ).t3.d( j ).okng

					If okng = 1 Then

						Return 1

					End If

				End If

			Next

		Next



		'
		'   20201102 s.harada	AQTC対応専用に変更
		'
		'
		'	残留吸着力測定結果チェック
		'
		For i = 0 To 3

			For j = 0 To MESrec.dt( i ).t4.dsiz - 1

				If MESrec.dt( i ).t4.d( j ).okng >= 0 Then

					okng			= MESrec.dt( i ).t4.d( j ).okng

					If okng = 1 Then

						Return 1

					End If

				End If

			Next

		Next


		Return 0

	End Function



	'*****
	'	出力信号初期化
	'*****
	Private Sub SigIni()


		'
		'	PIDのRSP設定
		'	RSP設定電圧を０
		'
		AoPutV( PIDaoRSP, 0.0 )


		'
		'	ＰＩＤ運転停止
		'
		ExDio_Output( MAEdoPIDSTART, DIO_OFF )


		'
		'	ＰＩＤ・ＲＳＰ選択信号ＯＦＦ
		'	LSPを選択
		'
		ExDio_Output( MAEdoPIDRSP, DIO_OFF )



		'
		'	マスフローコントローラ１
		'


		' 流量設定信号電圧を０
		AoPutV( MFCaoSETPT1, 0.0 )

		' 強制OPEN を解除
		ExDio_Output( MAEdoFOPN, DIO_OFF )

		' 強制CLOSE を解除
		ExDio_Output( MAEdoFCLS, DIO_OFF )

		' SET PT入力を パソコンＡＯと接続
		ExDio_Output( MAEdoRYFC, DIO_OFF )


		'
		'	ESC電源1CH,2CH
		'	出力電圧設定を０
		'
		AoPutV( ESCaoVOLT1, 0.0 )

		AoPutV( ESCaoVOLT2, 0.0 )


		' 外部起動信号 OFF
		ExDio_Output( ESCdoSTART1, DIO_OFF )

		ExDio_Output( ESCdoSTART2, DIO_OFF )

		' ｲﾝﾀｰﾛｯｸ信号 OFF
		ExDio_Output( ESCdoITL1, DIO_OFF )

		ExDio_Output( ESCdoITL2, DIO_OFF )



		Dim wk			As Double	= 0.0

		' サ－モチラ－ＣＨ１外部温度設定
		ExDa_Output( SCRaoREMOTE1, cvtt2SCR( 20.0, wk ) )

		' サ－モチラ－ＣＨ２外部温度設定
		ExDa_Output( SCRaoREMOTE2, cvtt2SCR( 20.0, wk ) )



		'
		'	マスフローコントローラ２
		'	※2014-01-09 現時点でMFC2は未使用(未接続)
		'

		' ＭＦＣ２流量設定信号
		AoPutV( MFCaoSETPT2, 0.0 )



		'
		'	高耐圧リレーＢＯＸ
		'

		'電極をモノポール、電極ヘッドと絶縁抵抗計を接続
		RyHvMode( POL_MON, MES_IOS )

		' 高圧リレーを全てOFF
		RyHvPos( POL_MON, MES_IOS, CON_OFF )


		'	※ 20200901 本バルブはトーカロ様では未装着
		'	ウエハ裏面圧開放バルブOFF
		'
		ExDio_Output( MAEdoPRG, DIO_OFF )

		'
		'	20200220 y.goto トーカロ様対応
		'	ウエハ裏面圧開放バルブ SV3(G4) OFF
		'
		ExDio_Output( EXSdoRYE3, DIO_OFF )


		'
		'	シグナルタワーOFF
		'
		PTLctl			= 0

		ExDio_Output( MAEdoLEDR, DIO_OFF )

		ExDio_Output( MAEdoLEDY, DIO_OFF )

		ExDio_Output( MAEdoLEDG, DIO_OFF )

	End Sub



	'*****
	'	出力信号終了
	'*****
	Private Sub SigEnd()


		'
		'	PIDのRSP設定
		'

		' RSP入力電圧を０
		AoPutV( PIDaoRSP, 0.0 )

		' PID運転停止
		ExDio_Output( MAEdoPIDSTART, DIO_OFF )


		'
		' マスフローコントローラ１
		'

		' MFC1流量設定信号電圧を０
		AoPutV( MFCaoSETPT1, 0.0 )

		' MFC1強制OPEN を解除
		ExDio_Output( MAEdoFOPN, DIO_OFF )

		' MFC1強制CLOSE を解除
		ExDio_Output( MAEdoFCLS, DIO_OFF )


		'
		'	RYFC切り替え
		'

		' SET PT入力を パソコンＡＯと接続
		ExDio_Output( MAEdoRYFC, DIO_OFF )


		'
		'	ESC電源1CH,2CH
		'

		'外部設定電圧を０
		AoPutV( ESCaoVOLT1, 0.0 )

		AoPutV( ESCaoVOLT2, 0.0 )

		' 外部起動信号 OFF
		ExDio_Output( ESCdoSTART1, DIO_OFF )

		ExDio_Output( ESCdoSTART2, DIO_OFF )

		' ｲﾝﾀｰﾛｯｸ信号 OFF
		ExDio_Output( ESCdoITL1, DIO_OFF )

		ExDio_Output( ESCdoITL2, DIO_OFF )


		'
		'	高耐圧リレー切り替え
		'

		' 高圧リレーを全てOFF
		RyHvPos( POL_MON, MES_IOS, CON_OFF )

		' 電極をモノポール、電極ヘッドと絶縁抵抗計を接続
		RyHvMode( POL_MON, MES_IOS )



		'
		'	サ－モチラ－の運転停止
		'
		' ExDio_Output( MAEdoS1RUN, DIO_OFF )

		' ExDio_Output( MAEdoS2RUN, DIO_OFF )


	'	Dim wk			As Double	= 0.0

		' サ－モチラ－ＣＨ１外部温度設定
	'	ExDa_Output( SCRaoREMOTE1, cvtt2SCR( 30.0, wk ) )

		' サ－モチラ－ＣＨ２外部温度設定
	'	ExDa_Output( SCRaoREMOTE2, cvtt2SCR( 30.0, wk ) )


		'
		'	マスフローコントローラ２
		'	※2014-01-09 現時点でMFC2は未使用(未接続)
		'
		AoPutV( MFCaoSETPT2, 0.0 )


		'	※ 20200901 本バルブはトーカロ様では未装着
		'	ウエハ裏面圧開放バルブOFF
		'
		ExDio_Output( MAEdoPRG, DIO_OFF )

		'
		'	20200220 y.goto トーカロ様対応
		'	ウエハ裏面圧開放バルブ SV3(G4) OFF
		'
		ExDio_Output( EXSdoRYE3, DIO_OFF )


		'
		'	シグナルタワーOFF
		'
		PTLctl			= 0

		ExDio_Output( MAEdoLEDR, DIO_OFF )

		ExDio_Output( MAEdoLEDY, DIO_OFF )

		ExDio_Output( MAEdoLEDG, DIO_OFF )


		' 500ms
	'	WaitTim( 50 )


	End Sub



	'*****
	'	試験開始条件待ち１
	'
	'	サ－モチラ－の設定温度が変更になっていることを
	'	想定して、全ての条件をチェックする
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Private Function waittstcond1				_
	(							_
		ByVal vac		As Integer,		_
		ByRef dt		As DTREC,		_
		ByVal tprs		As Double,		_
		ByVal bakp		As Double		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer



		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"測定開始条件成立待ち処理開始",		_
			Color.Empty				_
		)



		' 総合待ち時間 (秒)
		frm.PrmWTim		= 40 * 60

		' 全条件成立確定時間タイマー値（秒）
		'▼ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）
		'frm.PrmTcAllOK = 10
		frm.PrmTcAllOK = 1 + 1
		'▲ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1	= 20

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2	= 20

		' 電極ヘッド温度安定待ち時間（秒）
		frm.PrmTcAimHed		= ( PrmTmpStbW * 60 ).ToString()

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb		= 10

		' ウエハ裏面圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimWbp		= 10



		' サーモチラーＣＨ１設定温度
		If dt.schuse( 0 ) Then

			frm.PrmAimChl1		= dt.tmp( 0 ).ToString( ".0" )

		Else

			frm.PrmAimChl1		= ""

		End If

		' サーモチラーＣＨ２設定温度
		If dt.schuse( 1 ) Then

			frm.PrmAimChl2		= dt.tmp( 1 ).ToString( ".0" )

		Else

			frm.PrmAimChl2		= ""

		End If


		If vac Then

			' チャンバ内圧力
			frm.PrmAimCmb		= tprs.ToString( "0" )

			' ウエハ裏面圧力
			frm.PrmAimWbp		= bakp.ToString( "0" )

		Else

			' チャンバ内圧力
			frm.PrmAimCmb		= ""

			' ウエハ裏面圧力
			frm.PrmAimWbp		= ""

		End If

		' 20200326 追加 y.goto
		' 動作モード
		frm.PrmMode		= 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()


		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If


		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"測定開始条件成立待ち処理終了",		_
			Color.Empty				_
		)


		return rtn


	End Function



	'*****
	'	条件成立待ち２
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Public Function waittstcond2				_
	(							_
		ByVal vac		As Integer,		_
		ByVal schuse()		As Integer,		_
		ByVal tmp()		As Double,		_
		ByVal tprs		As Double,		_
		ByVal bakp		As Double		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer



		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"条件成立待ち処理2開始",		_
			Color.Empty				_
		)


		' 総合待ち時間 (秒)
		frm.PrmWTim		= 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		'▼ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）
		'frm.PrmTcAllOK = 10
		frm.PrmTcAllOK = 1 + 1
		'▲ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1	= 10

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2	= 10

		' 電極ヘッド温度安定待ち時間（秒）
		frm.PrmTcAimHed		= 5

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb		= 10

		' ウエハ裏面圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimWbp		= 10



		' サーモチラーＣＨ１設定温度
		If schuse( 0 ) Then

			frm.PrmAimChl1		= tmp( 0 ).ToString( ".0" )

		Else

			frm.PrmAimChl1		= ""

		End If

		' サーモチラーＣＨ２設定温度
		If schuse( 1 ) Then

			frm.PrmAimChl2		= tmp( 1 ).ToString( ".0" )

		Else

			frm.PrmAimChl2		= ""

		End If


		If vac Then

			' チャンバ内圧力
			frm.PrmAimCmb		= tprs.ToString( "0" )

			' ウエハ裏面圧力
			frm.PrmAimWbp		= bakp.ToString( "0" )

		Else

			' チャンバ内圧力
			frm.PrmAimCmb		= ""

			' ウエハ裏面圧力
			frm.PrmAimWbp		= ""

		End If

		' 20200326 追加 y.goto
		' 動作モード
		frm.PrmMode		= 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()


		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If


		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"条件成立待ち処理2終了",		_
			Color.Empty				_
		)



		return rtn


	End Function



	'*****
	'	条件待ち処理
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Public Function waittstcond3				_
	(							_
		ByVal msg		As String,		_
		ByVal vac		As Integer,		_
		ByVal schuse()		As Integer,		_
		ByVal tmp()		As Double,		_
		ByVal tprs		As Double,		_
		ByVal bakp		As Double		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer



		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"条件成立待ち処理3開始",		_
			Color.Empty				_
		)


		frm.Text		= msg

		' 総合待ち時間 (秒)
		frm.PrmWTim		= 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		'▼ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）
		'frm.PrmTcAllOK = 10
		frm.PrmTcAllOK = 1 + 1
		'▲ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1	= 10

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2	= 10

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb		= 10

		' ウエハ裏面圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimWbp		= 10



		'
		'	サーモチラーの温度はチェックしない
		'

		' サーモチラーＣＨ１設定温度
		frm.PrmAimChl1		= ""

		' サーモチラーＣＨ２設定温度
		frm.PrmAimChl2		= ""

		' 電極ヘッド温度安定待ち
		frm.PrmTcAimHed		= ""


		If vac Then

			' チャンバ内圧力
			frm.PrmAimCmb		= tprs.ToString( "0" )

			' ウエハ裏面圧力
			frm.PrmAimWbp		= bakp.ToString( "0" )

		Else

			' チャンバ内圧力
			frm.PrmAimCmb		= ""

			' ウエハ裏面圧力
			frm.PrmAimWbp		= ""

		End If

		' 20200326 追加 y.goto
		' 動作モード
		frm.PrmMode		= 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()


		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If


		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"条件成立待ち処理3終了",		_
			Color.Empty				_
		)



		return rtn


	End Function



	'*****
	'	ウエハ裏面圧条件待ち処理
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Public Function waitwbakp				_
	(							_
		ByVal msg		As String,		_
		ByVal vac		As Integer,		_
		ByVal schuse()		As Integer,		_
		ByVal tmp()		As Double,		_
		ByVal tprs		As Double,		_
		ByVal bakp		As Double		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer



		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"ウエハ裏面圧下降待ち処理開始",		_
			Color.Empty				_
		)


		frm.Text		= msg

		' 総合待ち時間 (秒)
		frm.PrmWTim		= 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		'▼ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）
		'frm.PrmTcAllOK = 3
		frm.PrmTcAllOK = 1 + 1
		'▲ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1	= 10

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2	= 10

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb		= 10

		' ウエハ裏面圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimWbp		= 10



		'
		'	サーモチラーの温度はチェックしない
		'

		' サーモチラーＣＨ１設定温度
		frm.PrmAimChl1		= ""

		' サーモチラーＣＨ２設定温度
		frm.PrmAimChl2		= ""

		' 電極ヘッド温度安定待ち
		frm.PrmTcAimHed		= ""

		' チャンバ内圧力はチェックしない
		frm.PrmAimCmb		= ""

		If vac Then

			' ウエハ裏面圧力
			frm.PrmAimWbp		= bakp.ToString( "0" )

		Else

			' ウエハ裏面圧力
			frm.PrmAimWbp		= ""

		End If

		' 20200326 追加 y.goto
		' 動作モード
		frm.PrmMode		= 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()


		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If


		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"ウエハ裏面圧下降待ち処理終了",		_
			Color.Empty				_
		)



		return rtn


	End Function

	'*****
	'	20210118 追加 y.goto
	'	残留吸着専用とするため waitwbakp をコピーして処理を変更した
	'
	'	ウエハ裏面圧条件待ち処理
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Public Function waitAQTCwbakp				_
	(							_
		ByVal msg		As String,		_
		ByVal vac		As Integer,		_
		ByVal schuse()		As Integer,		_
		ByVal tmp()		As Double,		_
		ByVal tprs		As Double,		_
		ByVal bakp		As Double		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer
		Dim escrun		As Integer


		escrun		= 1

		FrmLog.LogDspAdd						_
		(								_
			"",							_
			"waitAQTCwbakp ウエハ裏面圧下降待ち処理開始",		_
			Color.Empty						_
		)


		frm.Text		= msg

		' 総合待ち時間 (秒)
		frm.PrmWTim		= 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		'▼ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）
		'frm.PrmTcAllOK = 3
		frm.PrmTcAllOK = 1 + 1
		'▲ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1	= 10

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2	= 10

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb		= 10

		' ウエハ裏面圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimWbp		= 10



		'
		'	サーモチラーの温度はチェックしない
		'

		' サーモチラーＣＨ１設定温度
		frm.PrmAimChl1		= ""

		' サーモチラーＣＨ２設定温度
		frm.PrmAimChl2		= ""

		' 電極ヘッド温度安定待ち
		frm.PrmTcAimHed		= ""

		' チャンバ内圧力はチェックしない
		frm.PrmAimCmb		= ""

		If vac Then

			' ウエハ裏面圧力
			frm.PrmAimWbp		= bakp.ToString( "0" )

		Else

			' ウエハ裏面圧力
			frm.PrmAimWbp		= ""

		End If

		' 20200326 追加 y.goto
		' 動作モード
		frm.PrmMode		= 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()


		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

			'
			'	2021018 下記処理追加 y.goto
			'		トーカロ木村さん依頼により電圧印可後、30Pa以下になったら吸着を停止する
			'
			If bakp >= FrmGraph.MesBkp Then
		' 20210201 裏面圧力を変更出来るように
		'	If 30.0 >= FrmGraph.MesBkp Then

				If escrun Then

					escrun		= 0

					'
					'	吸着を停止する
					'
					ESCstop( 2000.0, 7 )

					'
					'	20210201 y,goto
					'	20210130木村さんメールの対応
					'
					frm.Close()

					' フォームクローズフラグ・強制クリア
					frm.StsFrmWaitCdt	= 0

					'
					'	20210201 y.goto
					'	電圧印可停止とともに、He導入開始待ちタイマーを起動
					'
					If PrmHeWait > 0 Then

						' ※タイマー６はHe導入開始タイマー専用とする
						SetTimDCnt( 6, PrmHeWait * 100L )

					End If

					FrmLog.LogDspAdd( "", "waitAQTCwbakp 裏面圧 " + bakp.ToString( "0." ) + " Pa以下 ESC吸着停止", Color.Empty )
				End If

			End If

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If


		FrmLog.LogDspAdd						_
		(								_
			"",							_
			"waitAQTCwbakp ウエハ裏面圧下降待ち処理終了",		_
			Color.Empty						_
		)

		return rtn

	End Function



	'*****
	'	チャンバ圧条件待ち処理
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Public Function waitwchmp				_
	(							_
		ByVal msg		As String,		_
		ByVal tprs		As Double		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer



		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"チャンバ圧条件成立待ち処理開始",	_
			Color.Empty				_
		)

		frm.Text		= msg

		' サーモチラーＣＨ１設定温度・チェックしない
		frm.PrmAimChl1		= ""

		' サーモチラーＣＨ２設定温度・チェックしない
		frm.PrmAimChl2		= ""

		' 電極ヘッド温度安定待ち・チェックしない
		frm.PrmTcAimHed		= ""

		' 条件成立チャンバ内圧力・チェックする
		frm.PrmAimCmb		= tprs.ToString( "0" )

		' ウエハ裏面圧力・チェックしない
		frm.PrmAimWbp		= ""

		' 総合待ち時間 (秒)
		frm.PrmWTim		= 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		'▼ 2024.04.11 条件成立後の待ち時間を１秒に統一
		'frm.PrmTcAllOK = 3
		frm.PrmTcAllOK = 1 + 1
		'▲ 2024.04.11 条件成立後の待ち時間を１秒に統一

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb		= 10

		' 20200326 追加 y.goto
		' 動作モード
		frm.PrmMode		= 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()

		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If

		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"チャンバ圧条件成立待ち処理終了",	_
			Color.Empty				_
		)

		return rtn

	End Function

	'*****
	'	20200326 追加 y.goto
	'	ベント完了条件待ち処理
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Public Function waittstbent				_
	(							_
		ByVal msg		As String,		_
		ByVal schuse()		As Integer,		_
		ByVal tmp()		As Double,		_
		ByVal tprs		As Double,		_
		ByVal bakp		As Double		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer

		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"ベント待ち開始",			_
			Color.Empty				_
		)

		frm.Text		= msg

		' 総合待ち時間 (秒)
		frm.PrmWTim		= 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		frm.PrmTcAllOK		= 0

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1	= 0

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2	= 0

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb		= 3

		' ウエハ裏面圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimWbp		= 0

		'
		'	条件チェックする、しないの指示をセット　 ( "" を指定するとチェックしない )
		'

		' サーモチラーＣＨ１設定温度	チェックしない
		frm.PrmAimChl1		= ""

		' サーモチラーＣＨ２設定温度	チェックしない
		frm.PrmAimChl2		= ""

		' 電極ヘッド温度安定待ち	チェックしない
		frm.PrmTcAimHed		= ""

		' チャンバ内圧力は指示が有れば(0 > でない)チェックする
		If 0.0 <= tprs Then

			' チェックの指示有り
			frm.PrmAimCmb		= tprs.ToString( "0" )

		Else

			' チェックの指示無し
			frm.PrmAimCmb		= ""

		End If

		' ウエハ裏面圧力は指示が有れば(0 > でない)チェックする
		If 0.0 <= bakp Then

			' チェックの指示有り
			frm.PrmAimWbp		= bakp.ToString( "0" )

		Else

			' チェックの指示無し
			frm.PrmAimWbp		= ""

		End If

		' 20200326 追加 y.goto
		' 動作モード トーカロ仕様　ベント完了待ち
		frm.PrmMode		= 1

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()

		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If

		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"ベント待ち終了",			_
			Color.Empty				_
		)

		return rtn

	End Function

	'*****
	'	20200327 追加 y.goto
	'	指定時間待ち処理
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****


	Public Function waittsttim _
	(
		ByVal msg As String,
		ByVal tmr As Long
	) As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm As New FrmWaitCdt
		Dim rtn As Integer

		FrmLog.LogDspAdd _
		(
			"",
			"待機時間待ち開始",
			Color.Empty
		)

		frm.Text = msg

		' 総合待ち時間 (秒)
		frm.PrmWTim = 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		frm.PrmTcAllOK = tmr

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1 = 0

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2 = 0

		' チャンバ内圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimCmb = 0

		' ウエハ裏面圧力目標到達判定タイマー値（秒）
		frm.PrmTcAimWbp = 0

		'
		'	条件チェックする、しないの指示をセット　 ( "" を指定するとチェックしない )
		'

		' サーモチラーＣＨ１設定温度	チェックしない
		frm.PrmAimChl1 = ""

		' サーモチラーＣＨ２設定温度	チェックしない
		frm.PrmAimChl2 = ""

		' 電極ヘッド温度安定待ち	チェックしない
		frm.PrmTcAimHed = ""

		' チャンバ内圧力		チェックしない
		frm.PrmAimCmb = ""

		' ウエハ裏面圧力		チェックしない
		frm.PrmAimWbp = ""

		' 20200326 追加 y.goto
		frm.PrmMode = 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()

		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.DoEvents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn = -1

		Else

			' 条件成立
			rtn = 0

		End If

		FrmLog.LogDspAdd _
		(
			"",
			"待機時間待ち終了",
			Color.Empty
		)

		Return rtn

	End Function

	'*****
	'	チラー指定温度到達後、５分間待つ
	'
	'	<return>
	'	Integer
	'	0	条件成立
	'	!0	試験中止指示
	'*****
	Private Function waittcrtmp				_
	(							_
		ByVal tcr1		As Double,		_
		ByVal tcr2		As Double,		_
		ByVal schch1		As Integer,		_
		ByVal schch2		As Integer		_
	)	As Integer

		' 試験開始条件待ちフォーム・クラス
		Dim frm			As New FrmWaitCdt
		Dim rtn			As Integer

		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"チラー測定終了温度到達ち開始",		_
			Color.Empty				_
		)

		frm.lblTitle.Text	= "チラー測定終了温度到達待ち"

		' 総合待ち時間 (秒)
		frm.PrmWTim		= 20 * 60

		' 全条件成立確定時間タイマー値（秒）
		'▼ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）
		'frm.PrmTcAllOK = 3
		frm.PrmTcAllOK = 1 + 1
		'▲ 2024.04.11 TC Kanda （５．ポップアップ待機時間削減／条件成立後の待ち時間を１秒に統一）

		' サーモチラーＣＨ１目標到達判定タイマー値（秒）
		frm.PrmTcAimChl1	= 5

		' サーモチラーＣＨ２目標到達判定タイマー値（秒）
		frm.PrmTcAimChl2	= 5

		' 電極ヘッド温度安定待ち時間（秒）
		frm.PrmTcAimHed		= ( PrmTmpStbW * 60 ).ToString()

		' サーモチラーＣＨ１設定温度
		' 20201207 追加 y.goto
		If schch1 Then

			frm.PrmAimChl1		= tcr1.ToString( ".0" )

		Else

			frm.PrmAimChl1		= ""

		End If


		' サーモチラーＣＨ２設定温度
		' 20201207 追加 y.goto
		If schch2 Then

			frm.PrmAimChl2		= tcr2.ToString( ".0" )

		Else

			frm.PrmAimChl2		= ""

		End If

		' チャンバ内圧力
		frm.PrmAimCmb		= ""

		' ウエハ裏面圧力
		frm.PrmAimWbp		= ""

		' 20200326 追加 y.goto
		' 動作モード
		frm.PrmMode		= 0

		'
		'	条件成立待ちウインドウ表示
		'
		frm.Show()

		'
		'	ウインドウが閉じられるまで待つ
		'
		Do While frm.FrmWaitCdtClose = 0

			Application.Doevents()

		Loop

		If frm.StsFrmWaitCdt Then

			' 試験中止が選択された
			rtn			= -1

		Else

			' 条件成立
			rtn			= 0

		End If


		FrmLog.LogDspAdd				_
		(						_
			"",					_
			"チラー測定終了温度到達ち終了",		_
			Color.Empty				_
		)

		return rtn

	End Function

	'*****
	'	20200323追加 トーカロ様設備対応
	'	真空引き処理
	'
	'	<return>
	'	Integer
	'	0	正常終了
	'	!= 0	試験中止
	'*****
	Private Function VACproc				_
	(							_
		ByVal schuse()		As Integer		_
	)	As Integer

		Dim sts			As Integer
		Dim schflg( 2 )		As Integer
		Dim temp( 2 )		As Double
		Dim rtn			As Integer
		Dim retry		As Integer
		Dim msg			As String
		Dim bkp			As Double

		' 戻り値初期値セット
		rtn			= -1

		' リトライカウンタクリア
		retry			= 0

		Do

			schflg( 0 )		= 0
			schflg( 1 )		= 0

			temp( 0 )		= 0.0
			temp( 1 )		= 0.0

			FrmLog.LogDspAdd( "", "VACproc(1) <<<< 真空排気動作開始", Color.Empty )

			'
			'	20200902 y.goto
			'	[測定2] が終了して [測定3] を開始する時に VACproc() が呼び出されるが、その時
			'	G4が開のまま MB が起動してしまうので、ここで必ずMBを停止するようにする
			'
			' MB=OFF R2=OFF DOX38=OFF
			FrmLog.LogDspAdd( "", "VACproc(1-1) MB停止", Color.Empty )
			ExDio_Output( EXSdoMBP,	DIO_OFF )

			'
			'	MFC1の流量設定を０にする
			'

			' MFC1の SET PT入力を パソコンＡＯと接続
			ExDio_Output( MAEdoRYFC, DIO_OFF )

			' MFC1強制OPEN を解除
			ExDio_Output( MAEdoFOPN, DIO_OFF )

			' MFC1強制CLOSE を解除
			ExDio_Output( MAEdoFCLS, DIO_OFF )

			' MFC1流量設定信号電圧を０
			AoPutV( MFCaoSETPT1, 0.0 )

			'
			'	LV閉
			'
			FrmLog.LogDspAdd( "", "VACproc(2) LV閉", Color.Empty )
			' SV4閉 RYE4 OFF DOX36 OFF
			ExDio_Output( EXSdoRYE4, DIO_OFF )

			'
			'	チラー内、外温度を20.0℃で運転開始
			'
			'	20201124 チラー起動関係修正
			'
			'		1) y.goto チラーは使用するCHのみ起動する
			'		2) 起動温度は 30℃ -> 20℃ に変更
			'

			'
			'	サ－モチラ－ＣＨ１の温度設定
			'
			If schuse( 0 ) Then

				SCRset( 0, 20.0 )

			End If

			'
			'	サ－モチラ－ＣＨ２の温度設定
			'
			If schuse( 1 ) Then

				SCRset( 1, 20.0 )

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

				FrmLog.LogDspAdd( "", "VACproc(3-1) TCR ON CH1=20℃", Color.Empty )

			End If

			If schuse( 1 ) Then

				' サーモチラー･CH2 運転開始/停止信号を ON
				ExDio_Output( MAEdoS2RUN, DIO_ON )

				FrmLog.LogDspAdd( "", "VACproc(3-2) TCR ON CH2=20℃", Color.Empty )

			End If

			'
			'	待機 1 sec
			'
			FrmLog.LogDspAdd( "", "VACproc(4) 待機 1 sec", Color.Empty )
			WaitTim( 100 )

			'
			'	真空ポンプの起動 (DP ON)
			'
			FrmLog.LogDspAdd( "", "VACproc(5) DPスタート", Color.Empty )
			ExDio_Output( MAEdoPUMP, DIO_ON )

			'	MV開
			'
			' SV1=開 RYE1=ON DOX33=ON
			FrmLog.LogDspAdd( "", "VACproc(6) MV開", Color.Empty )
			ExDio_Output( EXSdoRYE1, DIO_ON )

			'
			'	G4開
			'
			' ウエハ裏面圧開放バルブ SV3(G4) ON
			FrmLog.LogDspAdd( "", "VACproc(7) G4開", Color.Empty )
			ExDio_Output( EXSdoRYE3, DIO_ON )

			'
			'	G1開
			'
			' SV2=開 RYE2=ON DOX34=ON
			FrmLog.LogDspAdd( "", "VACproc(8) G1開", Color.Empty )
			ExDio_Output( EXSdoRYE2, DIO_ON )

			'
			'	真空引きループ先頭
			'
	VACPROC100:
			' リトライカウンタチェック
			If 3 <= retry Then

				FrmLog.LogDspAdd( "", "VACproc(9) 配管、ウエハ裏面圧真空引き失敗 Retry=" + retry.ToString(), Color.Empty )

				Exit Do

			End If

			'
			'	配管・ウエハ裏面圧 <= 30Pa になるのを待つ
			'	CMの圧力値で判断
			'
			FrmLog.LogDspAdd( "", "VACproc(10) 配管・ウエア裏面圧 <= 30Pa 待ち開始 Retry=" + retry.ToString(), Color.Empty )

			sts			= waitwbakp			_
			(							_
				"真空排気動作・配管・ウエハ裏面圧到達待ち Retry=" + retry.ToString(),		_
				1,						_
				schflg,						_
				temp,						_
				0.0,						_
				30.0						_
			)
			If sts Then

				FrmLog.LogDspAdd( "", "VACproc(11) 試験中止", Color.Empty )

				Exit Do

			End If
			FrmLog.LogDspAdd( "", "VACproc(12) 配管・ウエハ裏面圧 <= 30Pa 待ち終了", Color.Empty )

			'
			'	G1閉
			'
			' SV2=閉 RYE2=OFF DOX34=OFF
			FrmLog.LogDspAdd( "", "VACproc(13) G1閉", Color.Empty )
			ExDio_Output( EXSdoRYE2, DIO_OFF )

			'
			'	G4閉
			'
			' ウエハ裏面圧開放バルブ SV3(G4) OFF
			FrmLog.LogDspAdd( "", "VACproc(14) G4閉", Color.Empty )
			ExDio_Output( EXSdoRYE3, DIO_OFF )

			'
			'	待機 3 sec
			'
			' 待機 3 sec
			FrmLog.LogDspAdd( "", "VACproc(15) 待機 3 sec", Color.Empty )

			' 指定時間待ち処理
			sts		= waittsttim				_
			(							_
				"真空排気動作・待機時間待ち",			_
				3						_
			)
			If sts Then

				FrmLog.LogDspAdd( "", "VACproc(16) 試験中止", Color.Empty )

				Exit Do

			End If

			'
			'	20200909 y.goto
			'	チャンバ圧が2400Pa未満でないと MB 起動しないインターロックが
			'	有るので、MB起動したかチェックする
			'
			sts		= 0
			Do
				'
				'	MB ON
				'
				' MB=ON R2=ON DOX38=ON
				FrmLog.LogDspAdd( "", "VACproc(17-1) MB起動", Color.Empty )
				ExDio_Output( EXSdoMBP,	DIO_ON )

				If OutBuf( EXSdoMBP ) Then

					' MB起動した
					Exit Do

				End If

				FrmLog.LogDspAdd( "", "VACproc(17-2) チャンバ圧 <= 2400.0 [Pa] 待ち開始", Color.Empty )

				' チャンバ圧条件待ち処理
				sts			= waitwchmp			_
				(							_
					"真空排気動作・チャンバ内圧到達待ち",		_
					2400.0						_
				)

				If sts Then

					FrmLog.LogDspAdd( "", "VACproc(17-2) 試験中止", Color.Empty )

					Exit Do

				End If

			Loop

			If sts Then

				' 試験中止
				Exit Do

			End If

			'
			'	PIG < 5.0Pa になるのを待つ
			'
			FrmLog.LogDspAdd( "", "VACproc(18) チャンバ圧 < 5.0Pa 待ち開始", Color.Empty )

			' チャンバ圧条件待ち処理
			sts			= waitwchmp			_
			(							_
				"真空排気動作・チャンバ内圧到達待ち",		_
				5.0						_
			)

			If sts Then

				FrmLog.LogDspAdd( "", "VACproc(19) 試験中止", Color.Empty )

				Exit Do

			End If

			FrmLog.LogDspAdd( "", "VACproc(20) チャンバ圧 < 5.0Pa 待ち終了", Color.Empty )

			'
			'	配管、ウエハ裏面圧 < 50.0[Pa] 判断
			'
			bkp		= FrmGraph.MesBkp
			If FrmGraph.MesBkp >= 50.0 Then

				'
				'	配管、ウエハ裏面圧が 50.0 [Pa]以上なので排気処理実施
				'
				msg		= "VACproc(21) BKP_RTY=" + retry.ToString() + " "
				FrmLog.LogDspAdd( "", msg + "配管、ウエハ裏面圧 => 50.0[Pa] 排気処理開始 " + bkp.ToString + "[Pa]", Color.Empty )

				'
				'	MV閉
				'
				' SV1=閉 RYE1=OFF DOX33=OFF
				FrmLog.LogDspAdd( "", msg + "MV閉", Color.Empty )
				ExDio_Output( EXSdoRYE1, DIO_OFF )

				'
				'	待機: 3sec
				'
				FrmLog.LogDspAdd( "", msg + "待機 3 sec", Color.Empty )

				' 指定時間待ち処理
				sts		= waittsttim				_
				(							_
					"配管、ウエハ裏面圧排気動作・待機時間待ち Retry=" + retry.ToString(),		_
					3						_
				)
				If sts Then

					FrmLog.LogDspAdd( "", msg + "試験中止", Color.Empty )

					Exit Do

				End If

				'
				'	MB OFF
				'
				' MB=OFF R2=OFF DOX38=OFF
				FrmLog.LogDspAdd( "", msg + "MB停止", Color.Empty )
				ExDio_Output( EXSdoMBP,	DIO_OFF )

				'
				'	待機: 3sec
				'
				FrmLog.LogDspAdd( "", msg + "待機 3 sec", Color.Empty )

				' 指定時間待ち処理
				sts		= waittsttim				_
				(							_
					"配管、ウエハ裏面圧排気動作・待機時間待ち Retry=" + retry.ToString(),	_
					3						_
				)
				If sts Then

					FrmLog.LogDspAdd( "", msg + "試験中止", Color.Empty )

					Exit Do

				End If

				'
				'	G4開
				'
				' ウエハ裏面圧開放バルブ SV3(G4) ON
				FrmLog.LogDspAdd( "", msg + "G4開", Color.Empty )
				ExDio_Output( EXSdoRYE3, DIO_ON )

				'
				'	G1開
				'
				' SV2=開 RYE2=ON DOX34=ON
				FrmLog.LogDspAdd( "", msg + "G1開", Color.Empty )
				ExDio_Output( EXSdoRYE2, DIO_ON )

				' リトライカウンタ＋１
				retry			+= 1

				GoTo	VACPROC100

			End If

			'
			'	真空引きループ終端
			'

			'
			'	真空排気完了
			'
			FrmLog.LogDspAdd( "", "VACproc() >>>> 真空排気動作終了", Color.Empty )

			' 正常終了
			rtn		= 0

			Exit Do

		Loop

		Return( rtn )

	End Function

	'*****
	'	20200901追加
	'	測定途中の配管真空引き処理
	'
	'	<return>
	'	Integer
	'	0	正常終了
	'	!= 0	試験中止
	'*****
	'▼2024.04.19 TC Kanda （１．配管真空排気シーケンス修正／真空排気シーケンス内でESC電源OFF）
	'Public Function VACBproc() As Integer
	Public Function VACBproc(testType As TestType, sdcv1 As Double, sdcv2 As Double) As Integer
		'▲2024.04.19 TC Kanda （１．配管真空排気シーケンス修正／真空排気シーケンス内でESC電源OFF）

		Dim sts As Integer
		Dim schflg(2) As Integer
		Dim temp(2) As Double
		Dim rtn As Integer
		Dim msg As String
		Dim bkp As Double

		' 戻り値初期値セット
		rtn = -1

		Do

			schflg(0) = 0
			schflg(1) = 0

			temp(0) = 0.0
			temp(1) = 0.0

			FrmLog.LogDspAdd("", "VACBproc(1) <<<< 配管真空引き動作開始", Color.Empty)

			'
			'	バルブ初期値
			'

			' MV=開 SV1=開 RYE1=ON DOX33=ON
			ExDio_Output(EXSdoRYE1, DIO_ON)

			' G1=閉 SV2=閉 RYE2=OFF DOX34=OFF
			ExDio_Output(EXSdoRYE2, DIO_OFF)

			' G4=開 SV3=閉 RYE3=ON DOX35=ON
			ExDio_Output(EXSdoRYE3, DIO_OFF)

			' LV=閉 SV4=OFF RYE4=OFF DOX36=OFF
			ExDio_Output(EXSdoRYE4, DIO_OFF)

			'
			'	MFC1の流量設定を０にする
			'

			' MFC1の SET PT入力を パソコンＡＯと接続
			ExDio_Output(MAEdoRYFC, DIO_OFF)

			' MFC1強制OPEN を解除
			ExDio_Output(MAEdoFOPN, DIO_OFF)

			' MFC1強制CLOSE を解除
			ExDio_Output(MAEdoFCLS, DIO_OFF)

			' MFC1流量設定信号電圧を０
			AoPutV(MFCaoSETPT1, 0.0)

			'
			'	真空ポンプの起動 (DP ON)
			'
			FrmLog.LogDspAdd("", "VACBproc(2) DPスタート", Color.Empty)
			ExDio_Output(MAEdoPUMP, DIO_ON)

			'
			'	20201008 y.goto ウエハ裏面圧判定値を 20[Pa] -> 50[Pa] に変更
			'	CM <= 50 [Pa] ?
			'	配管、ウエハ裏面圧 <= 50.0 [Pa] 判断
			'
			bkp = FrmGraph.MesBkp
			If FrmGraph.MesBkp <= 50.0 Then

				'
				'	CM (ウエハ裏面圧)は 50[[Pa] 以下
				'
				FrmLog.LogDspAdd("", "VACBproc(3) CM <= 50 [Pa]", Color.Empty)

			Else

				'
				'	CM > 50 [Pa]
				'	ウエハ裏面圧は 50[[Pa] より大きい
				'
				msg = "VACBproc(4) CM > 50[Pa]" + " "
				FrmLog.LogDspAdd("", msg + "1) Start", Color.Empty)

				'
				'	MV閉
				'
				' SV1=閉 RYE1=OFF DOX33=OFF
				FrmLog.LogDspAdd("", msg + "2) MV閉", Color.Empty)
				ExDio_Output(EXSdoRYE1, DIO_OFF)

				'
				'	MB OFF
				'
				' MB=OFF R2=OFF DOX38=OFF
				FrmLog.LogDspAdd("", msg + "3) MB停止", Color.Empty)
				ExDio_Output(EXSdoMBP, DIO_OFF)

				'
				'	待機 5 sec
				'
				' 待機 5 sec
				FrmLog.LogDspAdd("", msg + "4) 待機 5 sec", Color.Empty)

				' 指定時間待ち処理
				sts = waittsttim _
				(
					"真空排気動作・待機時間待ち",
					5
				)
				If sts Then

					FrmLog.LogDspAdd("", msg + " 5) 試験中止", Color.Empty)

					Exit Do

				End If

				'
				'	G4開
				'
				' ウエハ裏面圧開放バルブ SV3(G4) ON
				FrmLog.LogDspAdd("", msg + "6) G4開", Color.Empty)
				ExDio_Output(EXSdoRYE3, DIO_ON)

				'
				'	G1開
				'
				' SV2=開 RYE2=ON DOX34=ON
				FrmLog.LogDspAdd("", msg + "7) G1開", Color.Empty)
				ExDio_Output(EXSdoRYE2, DIO_ON)

				'
				'	20201008 y.goto ウエハ裏面圧判定値を 20[Pa] -> 30[Pa] に変更
				'	CM (配管・ウエハ裏面圧) <= 30Pa になるのを待つ
				'	CMの圧力値で判断
				'
				FrmLog.LogDspAdd("", msg + "8) 配管・ウエハ裏面圧 <= 30 [Pa] 待ち開始", Color.Empty)

				sts = waitwbakp _
				(
					"真空排気動作・配管・ウエハ裏面圧 30 [Pa] 到達待ち",
					1,
					schflg,
					temp,
					0.0,
					30.0
				)
				If sts Then

					FrmLog.LogDspAdd("", msg + " 9) 試験中止", Color.Empty)

					Exit Do

				End If
				FrmLog.LogDspAdd("", msg + "10) 配管・ウエハ裏面圧 CM <= 30 [Pa] 待ち終了", Color.Empty)

				'▼2024.04.19 TC Kanda （１．配管真空排気シーケンス修正／ESC電源OFFの処理を チャンバ内圧力を下げる前に行う）
				Select Case (testType)
					Case TestType.None
					Case TestType.Resistance
						'ウエハ吸着テストの場合
						FrmLog.LogDspAdd("", msg + "★ 真空引き時のESC電源OFF ★", Color.Empty)
						ESCstop(2000.0, 7)
					Case TestType.HeLeak
						'Heリークテストの場合
						FrmLog.LogDspAdd("", msg + "★ 真空引き時のESC電源OFF ★", Color.Empty)
						'▼2024.05.23 TC Kanda （１．配管真空排気シーケンス修正／ESC電源OFFの処理を チャンバ内圧力を下げる前に行う）／現地にて修正
						'ESCstop2(sdcv1, sdcv2, 3000.0, 7)
						ESCstop(2000.0, 7)
						'▲2024.05.23 TC Kanda （１．配管真空排気シーケンス修正／ESC電源OFFの処理を チャンバ内圧力を下げる前に行う）／現地にて修正
					Case TestType.Adsorption
						'残留吸着力測定において印加中の真空引きには独自の排気シーケンスがある
						'印加前、印加後はこの真空引き処理を利用しているが、ESC電源の状態が重要な為、ここでは処理しない
				End Select
				'▲2024.04.19 TC Kanda （１．配管真空排気シーケンス修正／ESC電源OFFの処理を チャンバ内圧力を下げる前に行う）

				'
				'	G1閉
				'
				' SV2=閉 RYE2=OFF DOX34=OFF
				FrmLog.LogDspAdd("", msg + "11) G1閉", Color.Empty)
				ExDio_Output(EXSdoRYE2, DIO_OFF)

				'
				'	G4閉
				'
				' ウエハ裏面圧開放バルブ SV3(G4) OFF
				FrmLog.LogDspAdd("", msg + "12) G4閉", Color.Empty)
				ExDio_Output(EXSdoRYE3, DIO_OFF)

			End If

			'
			'	待機 5 sec
			'
			' 待機 5 sec
			FrmLog.LogDspAdd("", "VACBproc(5) 待機 5 sec", Color.Empty)

			' 指定時間待ち処理
			sts = waittsttim _
			(
				"真空排気動作・待機時間待ち",
				5
			)
			If sts Then

				FrmLog.LogDspAdd("", "VACBproc(6) 試験中止", Color.Empty)

				Exit Do

			End If

			'
			'	MV開
			'
			' SV1=開 RYE1=ON DOX33=ON
			FrmLog.LogDspAdd("", "VACBproc(7) MV開", Color.Empty)
			ExDio_Output(EXSdoRYE1, DIO_ON)

			'
			'	2400[Pa] => PIG になるのを待つ
			'	念のためMB起動条件の下限を設定
			'
			FrmLog.LogDspAdd("", "VACBproc(8) チャンバ圧 <= 2400.0 [Pa] 待ち開始", Color.Empty)

			' チャンバ圧条件待ち処理
			sts = waitwchmp _
			(
				"真空排気動作・チャンバ内圧到達待ち",
				2400.0
			)

			If sts Then

				FrmLog.LogDspAdd("", "VACBproc(9) 試験中止", Color.Empty)

				Exit Do

			End If

			FrmLog.LogDspAdd("", "VACBproc(10) チャンバ圧 <= 2400.0 [Pa] 待ち終了", Color.Empty)

			'
			'	MB ON
			'
			' MB=ON R2=ON DOX38=ON
			FrmLog.LogDspAdd("", "VACBproc(11) MB起動", Color.Empty)
			ExDio_Output(EXSdoMBP, DIO_ON)

			'
			'	真空排気完了
			'
			FrmLog.LogDspAdd("", "VACBproc() >>>> 真空排気動作終了", Color.Empty)

			' 正常終了
			rtn = 0

			Exit Do

		Loop

		Return (rtn)

	End Function

	'*****
	'	20201207追加 y.goto
	'	残留吸着試験専用測定途中の配管真空引き処理
	'
	'	<return>
	'	Integer
	'	0	正常終了
	'	!= 0	試験中止
	'*****
	Public Function VACBT4proc() As Integer

		Dim sts			As Integer
		Dim schflg( 2 )		As Integer
		Dim temp( 2 )		As Double
		Dim rtn			As Integer
		Dim msg			As String
		Dim bkp			As Double

		' 戻り値初期値セット
		rtn			= -1

		Do

			schflg( 0 )		= 0
			schflg( 1 )		= 0

			temp( 0 )		= 0.0
			temp( 1 )		= 0.0

			FrmLog.LogDspAdd( "", "VACBT4proc(1) <<<< 配管真空引き動作開始", Color.Empty )

			'
			'	バルブ初期値
			'

			' MV=開 SV1=開 RYE1=ON DOX33=ON
			ExDio_Output( EXSdoRYE1,	DIO_ON )

			' G1=閉 SV2=閉 RYE2=OFF DOX34=OFF
			ExDio_Output( EXSdoRYE2,	DIO_OFF )

			' G4=開 SV3=閉 RYE3=ON DOX35=ON
			ExDio_Output( EXSdoRYE3,	DIO_OFF )

			' LV=閉 SV4=OFF RYE4=OFF DOX36=OFF
			ExDio_Output( EXSdoRYE4,	DIO_OFF )

			'
			'	MFC1の流量設定を０にする
			'

			' MFC1の SET PT入力を パソコンＡＯと接続
			ExDio_Output( MAEdoRYFC, DIO_OFF )

			' MFC1強制OPEN を解除
			ExDio_Output( MAEdoFOPN, DIO_OFF )

			' MFC1強制CLOSE を解除
			ExDio_Output( MAEdoFCLS, DIO_OFF )

			' MFC1流量設定信号電圧を０
			AoPutV( MFCaoSETPT1, 0.0 )

			'
			'	真空ポンプの起動 (DP ON)
			'
			FrmLog.LogDspAdd( "", "VACBT4proc(2) DPスタート", Color.Empty )
			ExDio_Output( MAEdoPUMP, DIO_ON )

			'
			'	20201008 y.goto ウエハ裏面圧判定値を 20[Pa] -> 50[Pa] に変更
			'	CM <= 50 [Pa] ?
			'	配管、ウエハ裏面圧 <= 50.0 [Pa] 判断
			'
			bkp		= FrmGraph.MesBkp
			If FrmGraph.MesBkp <= 50.0 Then

				'
				'	CM (ウエハ裏面圧)は 50[[Pa] 以下
				'
				FrmLog.LogDspAdd( "", "VACBT4proc(3) CM <= 50 [Pa]", Color.Empty )

			Else

				'
				'	CM > 50 [Pa]
				'	ウエハ裏面圧は 50[[Pa] より大きい
				'
				msg		= "VACBT4proc(4) CM > 50[Pa]" + " "
				FrmLog.LogDspAdd( "", msg + "1) Start", Color.Empty )

				'
				'	MV閉
				'
				' SV1=閉 RYE1=OFF DOX33=OFF
				FrmLog.LogDspAdd( "", msg + "2) MV閉", Color.Empty )
				ExDio_Output( EXSdoRYE1, DIO_OFF )

				'
				'	MB OFF
				'
				' MB=OFF R2=OFF DOX38=OFF
				FrmLog.LogDspAdd( "", msg + "3) MB停止", Color.Empty )
				ExDio_Output( EXSdoMBP,	DIO_OFF )

			'
			'	20201207 y.goto
			'	残留吸着試験では下記処理はスキップする
			'
			'	'
			'	'	待機 5 sec
			'	'
			'	' 待機 5 sec
			'	FrmLog.LogDspAdd( "", msg + "4) 待機 5 sec", Color.Empty )
			'
			'	' 指定時間待ち処理
			'	sts		= waittsttim				_
			'	(							_
			'		"真空排気動作・待機時間待ち",			_
			'		5						_
			'	)
			'	If sts Then
			'
			'		FrmLog.LogDspAdd( "", msg + " 5) 試験中止", Color.Empty )
			'
			'		Exit Do
			'
			'	End If

				'
				'	G4開
				'
				' ウエハ裏面圧開放バルブ SV3(G4) ON
				FrmLog.LogDspAdd( "", msg + "6) G4開", Color.Empty )
				ExDio_Output( EXSdoRYE3, DIO_ON )

				'
				'	G1開
				'
				' SV2=開 RYE2=ON DOX34=ON
				FrmLog.LogDspAdd( "", msg + "7) G1開", Color.Empty )
				ExDio_Output( EXSdoRYE2, DIO_ON )

				'
				'	20201008 y.goto ウエハ裏面圧判定値を 20[Pa] -> 30[Pa] に変更
				'	CM (配管・ウエハ裏面圧) <= 30Pa になるのを待つ
				'	CMの圧力値で判断
				'
				FrmLog.LogDspAdd( "", msg + "8) 配管・ウエハ裏面圧 <= " + PrmBak2Prc.ToString( "0." ) + " [Pa] 待ち開始", Color.Empty )

				sts			= waitAQTCwbakp				_
				(								_
					"真空排気動作・配管・ウエハ裏面圧 " + PrmBak2Prc.ToString( "0." ) + " [Pa] 到達待ち",	_
					1,							_
					schflg,							_
					temp,							_
					0.0,							_
					PrmBak2Prc						_
				)
				If sts Then

					FrmLog.LogDspAdd( "", msg + " 9) 試験中止", Color.Empty )

					Exit Do

				End If
				FrmLog.LogDspAdd( "", msg + "10) 配管・ウエハ裏面圧 CM <= 30 [Pa] 待ち終了", Color.Empty )

				'
				'	G1閉
				'
				' SV2=閉 RYE2=OFF DOX34=OFF
				FrmLog.LogDspAdd( "", msg + "11) G1閉", Color.Empty )
				ExDio_Output( EXSdoRYE2, DIO_OFF )
				'▼
				ESCstop(2000.0, 7)
				'▲

				'
				'	G4閉
				'
				' ウエハ裏面圧開放バルブ SV3(G4) OFF
				FrmLog.LogDspAdd( "", msg + "12) G4閉", Color.Empty )
				ExDio_Output( EXSdoRYE3, DIO_OFF )

			End If

		'
		'	20201207 y.goto
		'	残留吸着試験では下記処理はスキップする
		'
		'	'
		'	'	待機 5 sec
		'	'
		'	' 待機 5 sec
		'	FrmLog.LogDspAdd( "", "VACBT4proc(5) 待機 5 sec", Color.Empty )
		'
		'	' 指定時間待ち処理
		'	sts		= waittsttim				_
		'	(							_
		'		"真空排気動作・待機時間待ち",			_
		'		5						_
		'	)
		'	If sts Then
		'
		'		FrmLog.LogDspAdd( "", "VACBT4proc(6) 試験中止", Color.Empty )
		'
		'		Exit Do
		'
		'	End If

			'
			'	MV開
			'
			' SV1=開 RYE1=ON DOX33=ON
			FrmLog.LogDspAdd( "", "VACBT4proc(7) MV開", Color.Empty )
			ExDio_Output( EXSdoRYE1, DIO_ON )

			'
			'	20201207 y.goto
			'	残留吸着試験では既にチャンバ圧が 2400.0[Pa] 以下の時は下記処理はスキップする
			'
			If 2400.0 < FrmGraph.MesCbp Then

				'
				'	2400[Pa] => PIG になるのを待つ
				'	念のためMB起動条件の下限を設定
				'
				FrmLog.LogDspAdd( "", "VACBT4proc(8) チャンバ圧 <= 2400.0 [Pa] 待ち開始", Color.Empty )

				' チャンバ圧条件待ち処理
				sts			= waitwchmp			_
				(							_
					"真空排気動作・チャンバ内圧到達待ち",		_
					2400.0						_
				)

				If sts Then

					FrmLog.LogDspAdd( "", "VACBT4proc(9) 試験中止", Color.Empty )

					Exit Do

				End If

				FrmLog.LogDspAdd( "", "VACBT4proc(10) チャンバ圧 <= 2400.0 [Pa] 待ち終了", Color.Empty )

			Else

				FrmLog.LogDspAdd( "", "VACBT4proc(10-1) 既に チャンバ圧 <= 2400.0 [Pa] なので 待ち処理は Skip", Color.Empty )

			End If

			'
			'	MB ON
			'
			' MB=ON R2=ON DOX38=ON
			FrmLog.LogDspAdd( "", "VACBT4proc(11) MB起動", Color.Empty )
			ExDio_Output( EXSdoMBP,	DIO_ON )

			'
			'	真空排気完了
			'
			FrmLog.LogDspAdd( "", "VACBT4proc() >>>> 真空排気動作終了", Color.Empty )

			' 正常終了
			rtn		= 0

			Exit Do

		Loop

		Return( rtn )

	End Function

	'*****
	'	20201207 y.goto サーモチラー使用CH把握の為、パラメータ追加
	'	20200323追加 トーカロ様設備対応
	'	ベント処理
	'*****
	Private Sub VENTproc				_
	(						_
		ByVal schch1		As Integer,	_
		ByVal schch2		As Integer	_
	)

		Dim sts			As Integer
		Dim schflg( 2 )		As Integer
		Dim temp( 2 )		As Double

		schflg( 0 )		= 0
		schflg( 1 )		= 0

		temp( 0 )		= 0.0
		temp( 1 )		= 0.0

		FrmLog.LogDspAdd( "", "VENTproc() <<<< ベント開始", Color.Empty )

		'
		'	チラー温度２０℃設定
		'

		' 20201207 追加 y.goto
		If schch1 Then

			' サ－モチラ－ＣＨ１の温度設定
			SCRset( 0, 20.0 )

		End If

		' 20201207 追加 y.goto
		If schch2 Then

			' サ－モチラ－ＣＨ２の温度設定
			SCRset( 1, 20.0 )

		End If

		'
		'	20201207 y.goto サーモチラー使用CH把握の為、パラメータ追加
		'	チラーが20℃になるのを待ち、到達後５分待つ
		'
		sts		= waittcrtmp( 20.0, 20.0, schch1, schch2 )

		'
		'	MV閉
		'
		' SV1=閉 RYE1=OFF DOX33=OFF
		FrmLog.LogDspAdd( "", "VENTproc() MV閉", Color.Empty )
		ExDio_Output( EXSdoRYE1, DIO_OFF )

		'
		'	G1閉
		'
		' SV2=閉 RYE2=OFF DOX34=OFF
		FrmLog.LogDspAdd( "", "VENTproc() G1閉", Color.Empty )
		ExDio_Output( EXSdoRYE2, DIO_OFF )

		'
		'	MB OFF
		'
		' MB=OFF R2=OFF DOX38=OFF
		FrmLog.LogDspAdd( "", "VENTproc() MB停止", Color.Empty )
		ExDio_Output( EXSdoMBP,	DIO_OFF )

		' 待機 3 sec
		FrmLog.LogDspAdd( "", "VENTproc() 待機 3 sec", Color.Empty )

		' 指定時間待ち処理
		sts		= waittsttim				_
		(							_
			"ベント動作・MB停止後待機時間待ち",		_
			3						_
		)

		'
		'	DP OFF
		'
		' 真空ポンプの停止 (DP OFF)
		FrmLog.LogDspAdd( "", "VENTproc() DP停止", Color.Empty )
		ExDio_Output( MAEdoPUMP, DIO_OFF )

		' 待機 3 sec
		FrmLog.LogDspAdd( "", "VENTproc() 待機 3 sec", Color.Empty )
		sts		= waittsttim				_
		(							_
			"ベント動作・DP停止後待機時間待ち",		_
			3						_
		)

		'
		'	LV開
		'
		' SV4開 RYE4 ON  DOX36 ON
		FrmLog.LogDspAdd( "", "VENTproc() LV開", Color.Empty )
		ExDio_Output( EXSdoRYE4, DIO_ON )

		'
		'	チャンバ内大気圧になるまで待つ
		'
		' 20200326 追加 y.goto
		' ベント完了条件待ち処理
		sts		= waittstbent				_
		(							_
			"ベント動作・チャンバ内大気圧解放待ち",		_
			schflg,						_
			temp,						_
			100000,						_
			-1						_
		)

		'
		'	LV閉
		'
		' SV4閉 RYE4 OFF DOX36 OFF
	' 20200323
	'	大気圧になったかという判断をどのくらいの圧力値にするかが分からないため
	'	LVは開けたままにしておく
	'	ExDio_Output( EXSdoRYE4, DIO_OFF )

		'
		'	ベント完了
		'
		FrmLog.LogDspAdd( "", "VENTproc() >>>> ベント終了", Color.Empty )

	End Sub


	'*****
	'	検査実行
	'*****
	Private Function tst_proc _
	(
		ByVal vac As Integer, _ '★真空引きするか？
		ByRef dt As DTREC,      '
		ByVal tprs As Double,
		ByVal bakp As Double
	) As Integer

		'vac;		真空設定
		'		0:      大気圧
		'		1:      真空圧
		'dt;		試験ﾃﾞｰﾀ格納ｴﾘｱ	
		'tprs;		検査条件・真空圧
		'bakp;		吸着停止する際の裏面圧

		Dim rtn As Integer
		Dim sts As Integer



		'
		'	各測定とも測定項目が１つも無い時は何もせずに戻る
		'

		'
		'   20201102 s.harada	AQTC対応専用に変更
		'
		If _
			dt.t1siz <= 0 And
			dt.t2.dsiz <= 0 And
			dt.t3.dsiz <= 0 And
			dt.t4.dsiz <= 0 _
		Then

			Return 0

		End If


		' 戻り値初期値セット
		rtn = -1

		Do

			'
			'	真空設定
			'
			If vac Then

				' 20200323追加
				' 真空引き処理
				If VACproc(dt.schuse) Then

					' 試験中止
					Exit Do

				End If

			End If

			'
			'	サ－モチラ－の温度設定
			'
			SCRSetTmp(dt.schchg, dt.schuse, dt.tmp)

			'
			'	試験開始条件待ち１
			'
			sts = waittstcond1 _
			(
				vac,
				dt,
				tprs,
				bakp
			)

			If sts Then

				' 試験中止
				Exit Do

			End If

			'
			'	絶縁抵抗試験
			'
			If (dt.t1siz > 0) Then

				If _
					tst1 _
					(
						dt,
						ESCmd,
						vac,
						tprs
					) < 0 Then

					Exit Do

				End If

			End If

			'▼2024.05.02 TC Kanda (４．測定順の変更／テスト順を入れ替え）
			''
			''	ｳｴﾊ吸着力測定
			''
			'If (dt.t2.dsiz > 0) Then

			'	'	20201102 s.harada
			'	'	ウエハ裏面圧力リミット値は未使用
			'	'	測定の上限時間を追加
			'	If tst2(vac, dt, ESCmd, tprs, 0, bakp, PrmMaxTim) Then
			'		'If tst2( vac, dt, ESCmd, tprs, BPRS, bakp ) Then

			'		Exit Do

			'	End If

			'End If

			''
			''	Ｈｅリーク量測定
			''	
			'If (dt.t3.dsiz > 0) Then

			'	If tst3(vac, dt, ESCmd, tprs, bakp) Then

			'		Exit Do

			'	End If

			'End If

			'
			'	Ｈｅリーク量測定
			'	
			If (dt.t3.dsiz > 0) Then

				If tst3(vac, dt, ESCmd, tprs, bakp) Then

					Exit Do

				End If

			End If
			'
			'	ｳｴﾊ吸着力測定
			'
			If (dt.t2.dsiz > 0) Then

				'	20201102 s.harada
				'	ウエハ裏面圧力リミット値は未使用
				'	測定の上限時間を追加
				If tst2(vac, dt, ESCmd, tprs, 0, bakp, PrmMaxTim) Then
					'If tst2( vac, dt, ESCmd, tprs, BPRS, bakp ) Then

					Exit Do

				End If

			End If
			'▲2024.05.02 TC Kanda (４．測定順の変更／テスト順を入れ替え）

			' 20200716 s.harada
			'
			'	残留吸着量測定
			'	
			If (dt.t4.dsiz > 0) Then

				If tst4(vac, dt, ESCmd, tprs, bakp) Then

					Exit Do

				End If

			End If

			rtn = 0

			Application.DoEvents()

		Loop While False

		Return rtn

	End Function


#End Region



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