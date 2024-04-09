
'*****
'	パラメータ設定クラス
'*****
Public Class DHDParmSet


	' 試験①・大気圧測定
	Dim dtTaiki		As DataTable

	' 試験②・真空測定１
	Dim dtVcmLow		As DataTable

	' 試験③・真空測定２
	Dim dtVcmHi		As DataTable

	' 試験④・真空測定３
	Dim dtVcmHi2		As DataTable



	'*****
	'	パラメータ設定フォームロードイベント
	'*****
	Private Sub DHDParmSet_Load				_
	(							_
		sender			As Object,		_
		e			As System.EventArgs	_
	)	Handles Me.Load


		WriteLog( "", "LG", "試験パラメータ設定 DHDParmSet_Load" )

		btnSansyo.Enabled	= False

		' DHDParamSet [保存]ボタン
		btnSave.Enabled		= False

		btnCancel.Enabled	= False


		'
		'	試験１・サーモチラーCH1使用フラグ表示用コンボボックス初期化
		'
		With cboTaikiTmpS1

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With


		'
		'	試験１・サーモチラーCH2使用フラグ表示用コンボボックス初期化
		'
		With cboTaikiTmpS2

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With


		'
		'	試験１・配管接続変更有無フラグ表示用コンボボックス初期化
		'
		With cboTaikiTmpStop

			.Items.Clear()

			.Items.Add( DefStrTmpNoChg )

			.Items.Add( DefStrTmpChg )

			.SelectedIndex		= 0

		End With



		'
		'	試験２・サーモチラーCH1使用フラグ表示用コンボボックス初期化
		'
		With cboVcmLowTmpS1

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With



		'
		'	試験２・サーモチラーCH2使用フラグ表示用コンボボックス初期化
		'
		With cboVcmLowTmpS2

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With


		'
		'	試験２・配管接続変更有無フラグ表示用コンボボックス初期化
		'
		With cboVcmLowTmpStop

			.Items.Clear()

			.Items.Add( DefStrTmpNoChg )

			.Items.Add( DefStrTmpChg )

			.SelectedIndex		= 0

		End With



		'
		'	試験３・サーモチラーCH1使用フラグ表示用コンボボックス初期化
		'
		With cboVcmHiTmpS1

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With


		'
		'	試験３・サーモチラーCH2使用フラグ表示用コンボボックス初期化
		'
		With cboVcmHiTmpS2

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With



		'
		'	試験３・配管接続変更有無フラグ表示用コンボボックス初期化
		'
		With cboVcmHiTmpStop

			.Items.Clear()

			.Items.Add( DefStrTmpNoChg )

			.Items.Add( DefStrTmpChg )

			.SelectedIndex		= 0

		End With



		'
		'	試験４・サーモチラーCH1使用フラグ表示用コンボボックス初期化
		'
		With cboVcmHi2TmpS1

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With


		'
		'	試験４・サーモチラーCH2使用フラグ表示用コンボボックス初期化
		'
		With cboVcmHi2TmpS2

			.Items.Clear()

			.Items.Add( DefStrNotUse )

			.Items.Add( DefStrUse )

			.SelectedIndex		= 0

		End With


		'
		'	試験４・配管接続変更有無フラグ表示用コンボボックス初期化
		'
		With cboVcmHi2TmpStop

			.Items.Clear()

			.Items.Add( DefStrTmpNoChg )

			.Items.Add( DefStrTmpChg )

			.SelectedIndex		= 0

		End With



		'
		'	パラメータ初期化
		'
		ClearDispParmData()

		SetBtnEnabled( 0 )


		'
		'	データテーブル作成
		'
		dtTaiki			= New DataTable

		createTblTest( dtTaiki )

		dtVcmLow		= New DataTable

		createTblTest( dtVcmLow )

		dtVcmHi			= New DataTable

		createTblTest( dtVcmHi )

		dtVcmHi2		= New DataTable

		createTblTest( dtVcmHi2 )



		'
		'	テーブルの初期化
		'
		ClearDispTaikiData()

		ClearDispVcmLowData()

		ClearDispVcmHiData()

		ClearDispVcmHi2Data()


		'	20201102 s.harada	トーカロ対応で削除

		'	'
		'	'	試験パラメータ表示用 DataGridView をソート禁止にする
		'	'

		'	' 大気圧・[電圧印加箇所] ソート禁止
		'	dgvTaiki.Columns( "Column1" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 大気圧・[印加電圧] ソート禁止
		'	dgvTaiki.Columns( "Column4" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 大気圧・[吸着力測定印加電圧１] ソート禁止
		'	dgvTaiki.Columns( "Column6" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 大気圧・[吸着力測定印加電圧２] ソート禁止
		'	dgvTaiki.Columns( "Column7" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 大気圧・[吸着力測定判定基準] ソート禁止
		'	dgvTaiki.Columns( "Column8" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 大気圧・[Heﾘｰｸ量測定印加電圧１] ソート禁止
		'	dgvTaiki.Columns( "Column9" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 大気圧・[Heﾘｰｸ量測定印加電圧２] ソート禁止
		'	dgvTaiki.Columns( "Column10" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 大気圧・[Heﾘｰｸ量測定判定基準] ソート禁止
		'	dgvTaiki.Columns( "Column2" ).SortMode			= DataGridViewColumnSortMode.NotSortable





		'	' 真空１・[電圧印加箇所] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn1" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空１・[印加電圧] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn2" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空１・[吸着力測定印加電圧１] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn3" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空１・[吸着力測定印加電圧２] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn4" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空１・[吸着力測定判定基準] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn5" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空１・[Heﾘｰｸ量測定印加電圧１] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn6" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空１・[Heﾘｰｸ量測定印加電圧２] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn7" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空１・[Heﾘｰｸ量測定判定基準] ソート禁止
		'	dgvVcmLow.Columns( "DataGridViewTextBoxColumn8" ).SortMode	= DataGridViewColumnSortMode.NotSortable



		'	' 真空２・[電圧印加箇所] ソート禁止
		'	dgvVcmHi.Columns("DataGridViewTextBoxColumn9").SortMode = DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[電圧印加箇所] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn9" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[印加電圧] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn10" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[吸着力測定印加電圧１] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn11" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[吸着力測定印加電圧２] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn12" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[吸着力測定判定基準] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn13" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[Heﾘｰｸ量測定印加電圧１] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn14" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[Heﾘｰｸ量測定印加電圧２] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn15" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空２・[Heﾘｰｸ量測定判定基準] ソート禁止
		'	dgvVcmHi.Columns( "DataGridViewTextBoxColumn16" ).SortMode	= DataGridViewColumnSortMode.NotSortable



		'	' 真空３・[電圧印加箇所] ソート禁止
		'	dgvVcmHi2.Columns( "DataGridViewTextBoxColumn17" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空３・[印加電圧] ソート禁止
		'	dgvVcmHi2.Columns( "DataGridViewTextBoxColumn18" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空３・[吸着力測定印加電圧１] ソート禁止
		'	dgvVcmHi2.Columns( "DataGridViewTextBoxColumn19" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空３・[吸着力測定印加電圧２] ソート禁止
		'	dgvVcmHi2.Columns( "DataGridViewTextBoxColumn20" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空３・[吸着力測定判定基準] ソート禁止
		'	dgvVcmHi2.Columns( "DataGridViewTextBoxColumn21" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空３・[Heﾘｰｸ量測定印加電圧１] ソート禁止
		'	dgvVcmHi2.Columns( "DataGridViewTextBoxColumn22" ).SortMode	= DataGridViewColumnSortMode.NotSortable

		'	' 真空３・[Heﾘｰｸ量測定印加電圧２] ソート禁止
		'	dgvVcmHi2.Columns( "LEK_VOLT2" ).SortMode			= DataGridViewColumnSortMode.NotSortable

		'	' 真空３・[Heﾘｰｸ量測定判定基準] ソート禁止
		'	dgvVcmHi2.Columns( "DataGridViewTextBoxColumn24" ).SortMode	= DataGridViewColumnSortMode.NotSortable




	End Sub



#Region "ボタン処理"



	'*****
	'	新規ボタンクリック
	'*****
	Private Sub btnNew_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnNew.Click


		WriteLog("", "LG", "試験パラメータ設定 btnNew_Click")

		If txtPoleFName.Text = "" Then

			MessageBox.Show("電極ヘッド種別を入力してください。")

			Exit Sub

		End If


		'
		'	ファイルがないか確認
		'
		If CheckTestFile(txtPoleFName.Text) = True Then


			WriteLog( "", "LG", "試験パラメータ設定 btnNew_Click ファイルあり　FILE=" + txtPoleFName.Text )

			If flipprc( OPIPyn, "ファイルが存在します。変更モードで読み込みますか？" ) <> DialogResult.Yes Then

				Exit Sub

			End If


			'
			'	データの読み込み
			'
			If GetTestData(txtPoleFName.Text, 0) < 0 Then

				WriteLog( "", "LG", "試験パラメータ設定 btnNew_Click　読み込み不可　FILE=" + txtPoleFName.Text )

				MessageBox.Show("テストデータが読み込めません", "確認", MessageBoxButtons.OK)

				Exit Sub

			End If


			WriteLog( "", "LG", "試験パラメータ設定 btnNew_Click　変更モード" )


			'
			'	DHDParamSetフォームの画面データ表示
			'

			'
			'	試験基本パラメータの画面表示
			'
			DispParmData()

			'
			'	大気圧・試験パラメータ・データ表示
			'
			DispTaikiData()

			'
			'	真空１・試験パラメータ・データ表示
			'
			DispVcmLowData()

			'
			'	真空２・試験パラメータ・データ表示
			'
			DispVcmHiData()

			'
			'	真空３・試験パラメータ・データ表示
			'
			DispVcmHi2Data()



			btnNew.Enabled		= False

			btnEdit.Enabled		= False

			btnSansyo.Enabled	= False

			btnCancel.Enabled	= True


			SetBtnEnabled( 2 )


			txtPoleFName.ReadOnly	= True

		Else

			WriteLog( "", "LG", "試験パラメータ設定 btnNew_Click　新規" )

			btnNew.Enabled		= False

			btnEdit.Enabled		= False

			btnSansyo.Enabled	= True

			btnCancel.Enabled	= True


			SetBtnEnabled( 1 )

			txtPoleFName.ReadOnly	= True

			tabTest.SelectedIndex	= 0



			' サーモチラーCH1使用フラグ表示用コンボボックス初期化
			cboTaikiTmpS1.SelectedIndex		= 0

			' サーモチラーCH2使用フラグ表示用コンボボックス初期化
			cboTaikiTmpS2.SelectedIndex		= 0

			' 配管接続変更有無フラグ表示用コンボボックス初期化
			cboTaikiTmpStop.SelectedIndex		= 0



			' サーモチラーCH1使用フラグ表示用コンボボックス初期化
			cboVcmLowTmpS1.SelectedIndex		= 0

			' サーモチラーCH2使用フラグ表示用コンボボックス初期化
			cboVcmLowTmpS2.SelectedIndex		= 0

			' 配管接続変更有無フラグ表示用コンボボックス初期化
			cboVcmLowTmpStop.SelectedIndex		= 0



			' サーモチラーCH1使用フラグ表示用コンボボックス初期化
			cboVcmHiTmpS1.SelectedIndex		= 0

			' サーモチラーCH2使用フラグ表示用コンボボックス初期化
			cboVcmHiTmpS2.SelectedIndex		= 0

			' 配管接続変更有無フラグ表示用コンボボックス初期化
			cboVcmHiTmpStop.SelectedIndex		= 0



			' サーモチラーCH1使用フラグ表示用コンボボックス初期化
			cboVcmHi2TmpS1.SelectedIndex		= 0

			' サーモチラーCH2使用フラグ表示用コンボボックス初期化
			cboVcmHi2TmpS2.SelectedIndex		= 0

			' 配管接続変更有無フラグ表示用コンボボックス初期化
			cboVcmHi2TmpStop.SelectedIndex		= 0



		End If


	End Sub



	'*****
	'	変更ボタンクリック
	'*****
	Private Sub btnEdit_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnEdit.Click

		Dim dlg			As New TestDataSelectDlg
		Dim ret			As DialogResult



		WriteLog( "", "LG", "試験パラメータ設定 btnEdit_Click" )

		ret			= dlg.ShowDialog

		If ret = Windows.Forms.DialogResult.OK Then

			txtPoleFName.Text	= dlg.cboDHead.Text


			' データの読み込み
			If GetTestData(txtPoleFName.Text, 0) < 0 Then

				WriteLog( "", "LG", "試験パラメータ設定 btnEdit_Click　読み込み不可　FILE=" + txtPoleFName.Text )

				MessageBox.Show( "テストデータが読み込めません", "確認", MessageBoxButtons.OK )

				Exit Sub

			End If


			'
			'	DHDParamSetフォームの画面データ表示
			'

			'
			'	試験基本パラメータの画面表示
			'
			DispParmData()

			'
			'	大気圧・試験パラメータ・データ表示
			'
			DispTaikiData()

			'
			'	真空１・試験パラメータ・データ表示
			'
			DispVcmLowData()

			'
			'	真空２・試験パラメータ・データ表示
			'
			DispVcmHiData()

			'
			'	真空３・試験パラメータ・データ表示
			'
			DispVcmHi2Data()



			btnNew.Enabled		= False

			btnEdit.Enabled		= False

			btnSansyo.Enabled	= False

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= True

			btnCancel.Enabled	= True


			txtPoleFName.ReadOnly	= True

			SetBtnEnabled( 2 )


			If dtTaiki.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 0

			ElseIf dtVcmLow.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 1

			ElseIf dtVcmHi.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 2

			ElseIf dtVcmHi2.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 3

			Else

				tabTest.SelectedIndex	= 0

			End If

			WriteLog( "", "LG", "試験パラメータ設定 btnEdit_Click 読み込み完了 ")

		Else

			WriteLog( "", "LG", "試験パラメータ設定 btnEdit_Click　中止" )

			MessageBox.Show( "中止しました。" )

		End If


	End Sub



	'*****
	'	参照ボタンクリック
	'*****
	Private Sub btnSansyo_Click			_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnSansyo.Click

		Dim dlg			As New TestDataSelectDlg
		Dim ret			As DialogResult



		WriteLog( "", "LG", "試験パラメータ設定 btnSansyo_Click" )

		ret			= dlg.ShowDialog

		If ret = Windows.Forms.DialogResult.OK Then

			'
			'	データの読み込み
			'
			If GetTestData(dlg.cboDHead.Text, 0) < 0 Then

				WriteLog								_
				(									_
					"",								_
					"LG",								_
					"試験パラメータ設定 btnSansyo_Click　読み込み不可　FILE=" +	_
					dlg.cboDHead.Text						_
				)

				MessageBox.Show( "テストデータが読み込めません", "確認", MessageBoxButtons.OK )

				Exit Sub

			End If


			'
			'	DHDParamSetフォームの画面データ表示
			'


			'
			'	試験基本パラメータの画面表示
			'
			DispParmData()

			'
			'	大気圧・試験パラメータ・データ表示
			'
			DispTaikiData()

			'
			'	真空１・試験パラメータ・データ表示
			'
			DispVcmLowData()

			'
			'	真空２・試験パラメータ・データ表示
			'
			DispVcmHiData()

			'
			'	真空３・試験パラメータ・データ表示
			'
			DispVcmHi2Data()


			btnNew.Enabled		= False

			btnEdit.Enabled		= False

			btnSansyo.Enabled	= False

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= True

			btnCancel.Enabled	= True


			SetBtnEnabled( 2 )


			If dtTaiki.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 0

			ElseIf dtVcmLow.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 1

			ElseIf dtVcmHi.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 2

			ElseIf dtVcmHi2.Rows.Count > 0 Then

				tabTest.SelectedIndex	= 3

			Else

				tabTest.SelectedIndex	= 0

			End If

			WriteLog( "", "LG", "試験パラメータ設定 btnSansyo_Click　読み込み完了　FILE=" + dlg.cboDHead.Text )

		Else

			WriteLog( "", "LG", "試験パラメータ設定 btnSansyo_Click　中止" )

			MessageBox.Show("中止しました。")

		End If


	End Sub



	'*****
	'	保存ボタンクリック
	'*****
	Private Sub btnSave_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnSave.Click



		WriteLog( "", "LG", "試験パラメータ設定 btnSave_Click" )

		If					_
			dtTaiki.Rows.Count = 0 And	_
			dtVcmLow.Rows.Count = 0 And	_
			dtVcmHi.Rows.Count = 0 And	_
			dtVcmHi2.Rows.Count = 0		_
		Then

			WriteLog( "", "LG", "試験パラメータ設定 btnSave_Click　データなし" )

			MessageBox.Show("試験項目データがありません。")

			Exit Sub

		End If


		If					_
			dtTaiki.Rows.Count > 0 And	_
			TempInputCheck			_
			(				_
				txtScrUse.Text,		_
				txtTaikiTmp1.Text,	_
				txtTaikiTmp2.Text
			) = False			_
		Then

			WriteLog( "", "LG", "試験パラメータ設定 btnSave_Click　大気圧温度入力なし" )

			MessageBox.Show("大気圧温度入力がありません。")

			Exit Sub

		End If


		If					_
			dtVcmLow.Rows.Count > 0 And	_
			TempInputCheck			_
			(				_
				txtScrUse.Text,		_
				txtVcmLowTmp1.Text,	_
				txtVcmLowTmp2.Text	_
			) = False			_
		Then

			WriteLog( "", "LG", "試験パラメータ設定 btnSave_Click　真空低温温度入力なし" )

			MessageBox.Show( "真空低温温度入力がありません。" )

			Exit Sub

		End If


		If					_
			dtVcmHi.Rows.Count > 0 And	_
			TempInputCheck			_
			(				_
				txtScrUse.Text,		_
				txtVcmHiTmp1.Text,	_
				txtVcmHiTmp2.Text	_
			) = False			_
		Then

			WriteLog( "", "LG", "試験パラメータ設定 btnSave_Click　真空高温温度入力なし" )

			MessageBox.Show( "真空高温温度入力がありません。" )

			Exit Sub

		End If


		If					_
			dtVcmHi2.Rows.Count > 0 And	_
			TempInputCheck			_
			(				_
				txtScrUse.Text,		_
				txtVcmHi2Tmp1.Text,	_
				txtVcmHi2Tmp2.Text	_
			) = False			_
		Then

			WriteLog( "", "LG", "試験パラメータ設定 btnSave_Click　真空高温２温度入力なし" )

			MessageBox.Show( "真空高温２温度入力がありません。" )

			Exit Sub

		End If


		'
		'	パラメータの保存
		'
		ESCmd			= CInt( txtEscMd.Text.Substring( 0, 1 ) )

		' ウエハ吸着力測定リミット値
		' 20201102 s.harada	トーカロ専用に変更で削除
		'BPRS			= CDbl( txtBPrs.Text ) * 1000.0

		'HLPRS			= CDbl( txtHlPres.Text ) * 1000.0

		SCRUSE			= CInt( txtScrUse.Text.Substring( 0, 1 ) )

		TPRS1			= CDbl( txtTPrs1.Text )

		TPRS2			= CDbl( txtTPrs2.Text )

		BakPres			= CDbl( txtBakPres.Text )

		' 電極ヘッド温度安定待ち時間 (分)
		PrmTmpStbW		= CDbl( txtTmpStbW.Text )


		'  20200716 s.harada
		' Ｈｅ流量(ccm)
		PrmHeFlow		= CDbl(txtHeFlow.Text)

		'  20200716 s.harada
		' 電圧印加時間(秒)
		PrmVoltImp		= CDbl(txtVoltImp.Text)

		'  20200716 s.harada
		' 電圧印加停止後Ｈｅ流すまでの待ち時間(秒)
		PrmHeWait		= CDbl(txtHeWait.Text)

		' 20201102 s.harada
		' 吸着力測定時間上限
		PrmMaxTim		= CDbl( txtKyuMaxTm.Text )

		' 20201102 s.harada
		' 残留吸着安定判断傾き
		PrmStabVct		= CDbl( txtStabVct.Text )

		' 20201102 s.harada
		' 残留吸着安定判断時間（秒）
		PrmStabTim		= CDbl( txtStabTim.Text )

		' 20201124 y.goto
		' 残留吸着電圧印可時裏面圧力 (Pa)
		PrmBakPrs		= CDbl( txtBakPrs.Text )

		'	20201102 s.harada	AQTC専用に変更
		If SaveParameter(txtPoleFName.Text) <> 0 Then

			WriteLog								_
			(									_
				"",								_
				"LG",								_
				"試験パラメータ設定 btnSave_Click　パラメータ保存不可　FILE=" +	_
				txtPoleFName.Text						_
			)

			MessageBox.Show( "保存に失敗しました。" )

			Exit Sub

		End If


		'
		'	大気圧
		'
		If SaveTestDtView _
		(
			txtPoleFName.Text,
			cboTaikiTmpS1.SelectedIndex,
			txtTaikiTmp1.Text,
			cboTaikiTmpS2.SelectedIndex,
			txtTaikiTmp2.Text,
			cboTaikiTmpStop.SelectedIndex,
			dtTaiki,
			0
		) <> 0 _
		Then

			WriteLog									_
			(										_
				"",									_
				"LG",									_
				"試験パラメータ設定 btnSave_Click　大気圧データ保存不可　FILE=" +	_
				txtPoleFName.Text							_
			)

			MessageBox.Show( "保存に失敗しました。" )

			Exit Sub

		End If



		'
		'	真空低温
		'
		If SaveTestDtView _
		(
			txtPoleFName.Text,
			cboVcmLowTmpS1.SelectedIndex,
			txtVcmLowTmp1.Text,
			cboVcmLowTmpS2.SelectedIndex,
			txtVcmLowTmp2.Text,
			cboVcmLowTmpStop.SelectedIndex,
			dtVcmLow,
			1
		) <> 0 _
		Then

			WriteLog									_
			(										_
				"",									_
				"LG",									_
				"試験パラメータ設定 btnSave_Click　真空低温データ保存不可　FILE=" +	_
				txtPoleFName.Text							_
			)

			MessageBox.Show( "保存に失敗しました。" )

			Exit Sub

		End If

		' DHDParamSet [保存]ボタン
		btnSave.Enabled		= False


		'
		'	真空高温
		'
		If SaveTestDtView _
		(
			txtPoleFName.Text,
			cboVcmHiTmpS1.SelectedIndex,
			txtVcmHiTmp1.Text,
			cboVcmHiTmpS2.SelectedIndex,
			txtVcmHiTmp2.Text,
			cboVcmHiTmpStop.SelectedIndex,
			dtVcmHi,
			2
		) <> 0 _
		Then

			WriteLog										_
			(											_
				"",										_
				"LG",										_
				"試験パラメータ設定 btnSave_Click　真空高温データ保存不可　FILE=" +		_
				txtPoleFName.Text								_
			)

			MessageBox.Show( "保存に失敗しました。" )

			Exit Sub

		End If


		'
		'	真空高温２
		'
		If SaveTestDtView _
		(
			txtPoleFName.Text,
			cboVcmHi2TmpS1.SelectedIndex,
			txtVcmHi2Tmp1.Text,
			cboVcmHi2TmpS2.SelectedIndex,
			txtVcmHi2Tmp2.Text,
			cboVcmHi2TmpStop.SelectedIndex,
			dtVcmHi2,
			3
		) <> 0 _
		Then

			WriteLog										_
			(											_
				"",										_
				"LG",										_
				"試験パラメータ設定 btnSave_Click　真空高温２データ保存不可　FILE=" +		_
				txtPoleFName.Text								_
			)

			MessageBox.Show( "保存に失敗しました。" )

			Exit Sub

		End If

		WriteLog										_
		(											_
			"",										_
			"LG",										_
			"試験パラメータ設定 btnSave_Click　保存完了FILE=" +				_
			txtPoleFName.Text								_
		)



	End Sub



	'*****
	'	キャンセルボタンクリック
	'*****
	Private Sub btnCancel_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnCancel.Click


		WriteLog( "", "LG", "試験パラメータ設定 btnCancel_Click" )


		'
		'	表示クリア
		'
		ClearDispParmData()

		ClearDispTaikiData()

		ClearDispVcmLowData()

		ClearDispVcmHiData()

		ClearDispVcmHi2Data()


		btnNew.Enabled		 = True

		btnEdit.Enabled		= True

		btnSansyo.Enabled	= False

		' DHDParamSet [保存]ボタン
		btnSave.Enabled		= False

		btnCancel.Enabled	= False


		SetBtnEnabled( 0 )

		txtPoleFName.ReadOnly	= False


		WriteLog( "", "LG", "試験パラメータ設定 btnCancel_Click　表示初期化完了" )


	End Sub



	'*****
	'	メニューボタンクリック
	'*****
	Private Sub btnEnd_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnEnd.Click


		WriteLog( "", "LG", "試験パラメータ設定 btnEnd_Click" )

		Me.Close()


	End Sub



	'*****
	'	パラメータ入力ボタンクリック
	'*****
	Private Sub btnParm_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnParm.Click

		Dim dlg			As New TestParmDlg



		WriteLog( "", "LG", "試験パラメータ設定 btnParm_Click" )

		Try

			dlg.pEscMd		= txtEscMd.Text.Substring( 0, 1 )

			' ウエハ吸着力測定リミット値
			' 20201102 s.harada	トーカロ専用に変更で削除
			'dlg.pBprs		= CDbl( txtBPrs.Text ) * 1000.0

			'dlg.pHlPres		= CDbl( txtHlPres.Text ) * 1000.0

			dlg.pScrUse		= txtScrUse.Text.Substring( 0, 1 )

			dlg.pTprs1		= CDbl( txtTPrs1.Text )

			dlg.pTprs2		= CDbl( txtTPrs2.Text )

			dlg.pBakPres		= CDbl( txtBakPres.Text )

			' 電極ヘッド温度安定待ち時間
			dlg.pTmpStbW		= CDbl(txtTmpStbW.Text)

			'	20201102 s.harada	AQTC専用に変更
			'残留吸着項目設定

			dlg.pHeFlow		= CDbl(txtHeFlow.Text)

			dlg.pVoltImp		= CDbl(txtVoltImp.Text)

			dlg.pHeWait		= CDbl(txtHeWait.Text)


			' 20201102 s.harada	追加
			dlg.pMaxTim		= CDbl(txtKyuMaxTm.Text)

			dlg.pAntCvt		= CDbl(txtStabVct.Text)

			dlg.pAntTim		= CDbl(txtStabTim.Text)

			dlg.pBakPrs		= CDbl( txtBakPrs.Text )

		Catch ex As Exception

			dlg.pEscMd		= 0

			' 20201102 s.harada	トーカロ専用に変更で削除
			'dlg.pBprs		= 1.0 * 1000

			'dlg.pHlPres		= 1.0 * 1000

			dlg.pScrUse		= 0

			dlg.pTprs1		= 5.0

			dlg.pTprs2		= 5.0

			dlg.pBakPres		= 50

			dlg.pTmpStbW		= 0.0

			' 20201102 s.harada	AQTC専用に変更
			'残留吸着項目設定
			dlg.pHeFlow		= 5.0

			dlg.pVoltImp		= 300.0

			dlg.pHeWait		= 0.0


			' 20201102 s.harada	追加
			dlg.pMaxTim		= 600

			dlg.pAntCvt		= 1.00

			dlg.pAntTim		= 60

			dlg.pBakPrs		= 1500.0

		End Try



		If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then

			txtEscMd.Text		= dlg.pEscMd.ToString + ":" + convEscMdToStr( dlg.pEscMd )

			' ウエハ吸着力測定リミット値
			' 20201102 s.harada	トーカロ専用に変更で削除
			'txtBPrs.Text		= ( dlg.pBprs / 1000.0 ).ToString( "0.00" )

			'txtHlPres.Text		= ( dlg.pHlPres / 1000.0 ).ToString( "0.0" )

			txtScrUse.Text		= dlg.pScrUse.ToString + ":" + convScrUseToStr( dlg.pScrUse )

			txtTPrs1.Text		= dlg.pTprs1.ToString( "0.00" )

			txtTPrs2.Text		= dlg.pTprs2.ToString( "0.00" )

			txtBakPres.Text		= dlg.pBakPres.ToString( "0.0" )

			' 電極ヘッド温度安定待ち時間
			txtTmpStbW.Text		= dlg.pTmpStbW.ToString( "0." )


			'	20201102 s.harada	AQTC専用に変更
				'残留吸着項目設定
			txtHeFlow.Text		= dlg.pHeFlow.ToString("0.0")

			txtVoltImp.Text		= dlg.pVoltImp.ToString("0.")

			txtHeWait.Text		= dlg.pHeWait.ToString("0.")


			' 20201102 s.harada	追加
			txtKyuMaxTm.Text	= dlg.pMaxTim.ToString("0.")

			txtStabVct.Text		= dlg.pAntCvt.ToString("0.0")

			txtStabTim.Text		= dlg.pAntTim.ToString("0.")

			txtBakPrs.Text		= dlg.pBakPrs.ToString( "0." )

			SetBtnEnabled( 2 )

			WriteLog( "", "LG", "試験パラメータ設定 btnParm_Click　設定完了" )

		Else

			WriteLog( "", "LG", "試験パラメータ設定 btnParm_Click　設定中止" )

		End If


		dlg.Dispose()


	End Sub



	'*****
	'	試験温度設定ボタンクリック
	'*****
	Private Sub btnTempSet_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles						_
		btnTaikiTmp.Click,				_
		btnVcmLowTmp.Click,				_
		btnVcmHiTmp.Click,				_
		btnVcmHi2Tmp.Click


		Dim dlg			As New TestTmpDlg

		'
		'	試験パラメータのサーモチラー使用チャネルコード
		'	0	CH1
		'	1	CH2
		'	2	CH1, CH2
		'
		Dim Use			As Integer = CInt( txtScrUse.Text.Substring( 0, 1 ) )
		Dim btn			As Button = sender
		Dim txt1		As TextBox
		Dim txt2		As TextBox
		Dim ch1use		As ComboBox
		Dim ch2use		As ComboBox
		Dim chgtmp		As ComboBox



		WriteLog( "", "LG", "試験パラメータ設定 btnTempSet_Click" )

		Select Case	btn.Name

		Case	"btnTaikiTmp"

			' 大気圧

			' サ－モチラ－CH1設定温度
			txt1			= txtTaikiTmp1

			' サ－モチラ－CH2設定温度
			txt2			= txtTaikiTmp2

			' サ－モチラ－CH1使用フラグ
			ch1use			= cboTaikiTmpS1

			' サ－モチラ－CH2使用フラグ
			ch2use			= cboTaikiTmpS2

			' サ－モチラ－配管接続変更有無フラグ
			chgtmp			= cboTaikiTmpStop


		Case	"btnVcmLowTmp"

			' 真空低温

			' サ－モチラ－CH1設定温度
			txt1			= txtVcmLowTmp1

			' サ－モチラ－CH2設定温度
			txt2			= txtVcmLowTmp2

			' サ－モチラ－CH1使用フラグ
			ch1use			= cboVcmLowTmpS1

			' サ－モチラ－CH2使用フラグ
			ch2use			= cboVcmLowTmpS2

			' サ－モチラ－配管接続変更有無フラグ
			chgtmp			= cboVcmLowTmpStop


		Case	"btnVcmHiTmp"

			' 真空高温

			' サ－モチラ－CH1設定温度
			txt1			= txtVcmHiTmp1

			' サ－モチラ－CH2設定温度
			txt2			= txtVcmHiTmp2

			' サ－モチラ－CH1使用フラグ
			ch1use			= cboVcmHiTmpS1

			' サ－モチラ－CH2使用フラグ
			ch2use			= cboVcmHiTmpS2

			' サ－モチラ－配管接続変更有無フラグ
			chgtmp			= cboVcmHiTmpStop


		Case	"btnVcmHi2Tmp"

			' 真空高温２

			' サ－モチラ－CH1設定温度
			txt1			= txtVcmHi2Tmp1

			' サ－モチラ－CH2設定温度
			txt2			= txtVcmHi2Tmp2

			' サ－モチラ－CH1使用フラグ
			ch1use			= cboVcmHi2TmpS1

			' サ－モチラ－CH2使用フラグ
			ch2use			= cboVcmHi2TmpS2

			' サ－モチラ－配管接続変更有無フラグ
			chgtmp			= cboVcmHi2TmpStop


		Case Else

			'該当なし
			WriteLog( "", "LG", "試験パラメータ設定 btnTempSet_Click　該当なし　btn=" + btn.Name )

			Exit Sub

		End Select


		'
		'	サ－モチラ－温度設定ダイアログの設定
		'

		' CH1使用フラグ
		dlg.pTmpUse1		= ch1use.SelectedIndex

		' CH2使用フラグ
		dlg.pTmpUse2		= ch2use.SelectedIndex

		' 配管接続変更有無フラグ
		dlg.pTmpStop		= chgtmp.SelectedIndex


		' CH1設定温度
		dlg.pTemp1		= txt1.Text

		' CH2設定温度
		dlg.pTemp2		= txt2.Text



		'
		'	サ－モチラ－温度設定ダイアログの表示
		'
		If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

			'
			'	入力された温度を反映する
			'

			' CH1使用フラグ
			ch1use.SelectedIndex	= dlg.pTmpUse1

			' CH2使用フラグ
			ch2use.SelectedIndex	= dlg.pTmpUse2

			' 配管接続変更有無フラグ
			chgtmp.SelectedIndex	= dlg.pTmpStop

			' サ－モチラ－CH1設定温度
			txt1.Text		= dlg.pTemp1

			' サ－モチラ－CH1設定温度
			txt2.Text		= dlg.pTemp2



			WriteLog( "", "LG", "試験パラメータ設定 btnTempSet_Click　設定完了" )

			'
			'	2014-01-21
			'	温度パラメータを変更しても保存ボタンが無効のままになっていた
			'

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= True

		Else

			WriteLog( "", "LG", "試験パラメータ設定 btnTempSet_Click　設定中止" )

		End If

		dlg.Dispose()


	End Sub



	'****
	'	試験項目追加ボタンクリック
	'*****
	Private Sub btnTestAdd_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles						_
		btnVcmLowAdd.Click,				_
		btnVcmHiAdd.Click,				_
		btnVcmHi2Add.Click,				_
		btnTaikiAdd.Click


		' [測定項目]入力用ダイアログのインスタンス作成
		Dim dlg			As New TestItemDlg
		Dim btn			As Button = sender
		Dim dt			As DataTable
		Dim dgv			As DataGridView



		WriteLog( "", "LG", "試験パラメータ設定 btnTestAdd_Click" )

		Select Case	btn.Name

		Case	"btnTaikiAdd"

			' 大気圧
			dt			= dtTaiki

			' DHDParamSetの大気圧・DataGridView
			dgv			= dgvTaiki

			dlg.pSel		= 0

		Case	"btnVcmLowAdd"

			' 真空低温
			dt			= dtVcmLow

			' DHDParamSetの真空(低温)・DataGridView
			dgv			= dgvVcmLow

			dlg.pSel		= 1

		Case	"btnVcmHiAdd"

			' 真空高温
			dt			= dtVcmHi

			' DHDParamSetの真空(高温)・DataGridView
			dgv			= dgvVcmHi

			dlg.pSel		= 2

		Case	"btnVcmHi2Add"

			' 真空高温２
			dt			= dtVcmHi2

			' DHDParamSetの・真空(高温)２・DataGridView
			dgv			= dgvVcmHi2

			dlg.pSel		= 3

		Case Else

			' 該当なし
			WriteLog( "", "LG", "試験パラメータ設定 btnTestAdd_Click　該当なし　btn=" + btn.Name )

			Exit Sub

		End Select



		'
		'	[測定項目]ダイアログを呼び出すための初期処理
		'

		' TestItemDlg・追加、変更処理フラグに[追加]をセット
		dlg.pMode		= 0

		' TestItemDlg・電極ヘッドの種別をセット
		dlg.pESCmd		= CInt( txtEscMd.Text.Substring( 0, 1 ) )

		' 試験パラメータ
		dlg.dt			= dt

		dlg.pDtRow		= dt.NewRow


		'
		'	[測定項目]ダイアログを表示させ、
		'	測定パラメータ入力を行う
		'
		If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

			If IsDBNull( dlg.pDtRow( "PV" ) ) Then

				WriteLog								_
				(									_
					"",								_
					"LG",								_
					"試験パラメータ設定 btnTestAdd_Click　PV設定なし　SEL=" +	_
					dlg.pSel.ToString						_
				)

				Exit Sub

			End If

			dt.Rows.Add( dlg.pDtRow )

			ReIdxDataTbl( dt )

			'
			'	2014-01-21
			'	ソートすると入力した順番と変わってしまう場合があるので
			'	ソートは行わないようにする
			'	
			'	SortDataTbl( dt )


			'
			'	DataGridView 入力されたデータ位置へ選択行を移動させる
			'
			Try

				dgv.CurrentCell			= dgv			_
				(							_
					4,						_
					dgv.Rows.Count - 1				_
				)

			Catch

			End Try

			dgv.Refresh()


			'
			'	2014-01-21
			'	試験パラメータを変更しても保存ボタンが無効のままになっていた
			'

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= True


			WriteLog( "", "LG", "試験パラメータ設定 btnTestAdd_Click　設定完了　SEL=" + dlg.pSel.ToString )

		Else

			WriteLog( "", "LG", "試験パラメータ設定 btnTestAdd_Click　設定中止　SEL=" + dlg.pSel.ToString )

		End If

		dlg.Dispose()


	End Sub



	'*****
	'	試験項目変更ボタンクリック
	'*****
	Private Sub btnTestEdit_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles						_
		btnVcmHi2Edit.Click,				_
		btnVcmLowEdit.Click,				_
		btnVcmHiEdit.Click,				_
		btnTaikiEdit.Click


		Dim dlg			As New TestItemDlg
		Dim btn			As Button = sender
		Dim dt			As DataTable
		Dim dgv			As DataGridView
		Dim idx			As Integer



		WriteLog( "", "LG", "試験パラメータ設定 btnTestEdit_Click" )


		Select Case btn.Name

		Case	"btnTaikiEdit"

			' 大気圧
			dt			= dtTaiki

			' DHDParamSetの大気圧・DataGridView
			dgv			= dgvTaiki

			dlg.pSel		= 0

		Case	"btnVcmLowEdit"

			' 真空低温
			dt			= dtVcmLow

			' DHDParamSetの真空(低温)・DataGridView
			dgv			= dgvVcmLow

			dlg.pSel		= 1

		Case	"btnVcmHiEdit"

			' 真空高温
			dt			= dtVcmHi

			' DHDParamSetの真空(高温)・DataGridView
			dgv			= dgvVcmHi

			dlg.pSel		= 2

		Case	"btnVcmHi2Edit"

			' 真空高温２
			dt			= dtVcmHi2

			' DHDParamSetの・真空(高温)２・DataGridView
			dgv			= dgvVcmHi2

			dlg.pSel		= 3

		Case Else

			' 該当なし
			WriteLog( "", "LG", "試験パラメータ設定 btnTestEdit_Click　該当なし　btn=" + btn.Name )

			Exit Sub

		End Select


		If dt.Rows.Count = 0 Then

			WriteLog( "", "LG", "試験パラメータ設定 btnTestEdit_Click　変更データなし　SEL=" + dlg.pSel.ToString )

			Exit Sub

		End If


		Select Case	dlg.pSel

		Case	0

			' 大気圧
			idx			= dgv.CurrentRow.Cells( "TIDX" ).Value

		Case	1

			' 真空低温
			idx			= dgv.CurrentRow.Cells( "LIDX" ).Value

		Case	2

			' 真空高温
			idx			= dgv.CurrentRow.Cells( "HIDX" ).Value

		Case	3

			' 真空高温２
			idx			= dgv.CurrentRow.Cells( "H2IDX" ).Value

		Case Else

			' 該当なし
			Exit Sub

		End Select



		'
		'	[測定項目]ダイアログを呼び出すための初期処理
		'

		' TestItemDlg・追加、変更処理フラグに[変更]をセット
		dlg.pMode		= 1

		' TestItemDlg・電極ヘッドの種別をセット
		dlg.pESCmd		= CInt( txtEscMd.Text.Substring( 0, 1 ) )

		dlg.dt			= dt

		dlg.pDtRow		= dt.Rows( idx )


		'
		'	[測定項目]ダイアログを表示させ、
		'	測定パラメータ入力を行う
		'
		If dlg.ShowDialog = Windows.Forms.DialogResult.OK Then

			If dlg.pDtRow( "PV" ) < 0 Then

				dt.Rows(idx).Delete()

				ReIdxDataTbl( dt )

				WriteLog( "", "LG", "試験パラメータ設定 btnTestEdit_Click　削除　SEL=" + dlg.pSel.ToString )

			Else

				WriteLog( "", "LG", "試験パラメータ設定 btnTestEdit_Click　変更　SEL=" + dlg.pSel.ToString )

			End If

			'
			'	2014-01-21
			'	試験パラメータを変更しても保存ボタンが無効のままになっていた
			'

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= True

		Else

			WriteLog( "", "LG", "試験パラメータ設定 btnTestEdit_Click　中止　SEL=" + dlg.pSel.ToString )

		End If


		dlg.Dispose()



	End Sub



	'*****
	'	測定選択ボタンの操作許可表示
	'*****
	Private Sub SetBtnEnabled(ByVal sel As Integer)


		Select Case	sel

		Case	0

			btnParm.Enabled		= False

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= False

			tabTest.Enabled		= False

		Case	1

			btnParm.Enabled		= True

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= False

			tabTest.Enabled		= False

		Case	2

			btnParm.Enabled		= True

			' DHDParamSet [保存]ボタン
			btnSave.Enabled		= True

			tabTest.Enabled		= True

		End Select


	End Sub



	'*****
	'	温度入力のチェック
	'*****
	Private Function TempInputCheck				_
	(							_
		ByVal Use		As String,		_
		ByVal tmp1		As String,		_
		ByVal tmp2		As String		_
	)	As Boolean

		Dim ret			As Boolean = False


		Select Case CInt( Use.Substring( 0, 1 ) )

		Case	0

			If tmp1 <> "" And IsNumeric( tmp1 ) = True Then

				ret			= True

			End If

		Case	1

			If tmp2 <> "" And IsNumeric( tmp2 ) = True Then

				ret			= True

			End If

		Case	2

			If						_
				tmp1 <> "" And				_
				IsNumeric( tmp1 ) = True And		_
				tmp2 <> "" And				_
				IsNumeric( tmp2 ) = True		_
			Then

				ret			= True

			End If

		End Select


		Return ret


	End Function



#End Region


#Region "データ表示"



	'*****
	'	パラメータ表示クリア
	'*****
	Private Sub ClearDispParmData()


		txtPoleFName.Text	= ""

		txtEscMd.Text		= ""

		' ウエハ吸着力測定リミット値
		' 20201102 s.harada	トーカロ専用に変更で削除
		'txtBPrs.Text		= ""

		'txtHlPres.Text		= ""

		txtScrUse.Text		= ""

		txtTPrs1.Text		= ""

		txtTPrs2.Text		= ""

		txtBakPres.Text		= ""

		' 電極ヘッド温度安定待ち時間
		txtTmpStbW.Text		= ""

		' 20200716 s.harada
		' ヘリウム流量
		txtHeFlow.Text		= ""

		' 20200716 s.harada
		' 電圧印加時間
		txtVoltImp.Text		= ""

		' 20200716 s.harada
		' 電圧印加停止後Ｈｅ流すまでの待ち時間
		txtHeWait.Text		= ""

		' 20201102 s.harada
		' 吸着力測定時間上限（秒）
		txtKyuMaxTm.Text	= ""

		' 20201102 s.harada
		' 残留吸着安定判断傾き
		txtStabVct.Text	= ""

		' 20201102 s.harada
		' 残留吸着安定判断時間（秒）
		txtStabTim.Text	= ""

	End Sub


	'*****
	'	大気試験データ表示クリア
	'*****
	Private Sub ClearDispTaikiData()


		' サーモチラーCH1設定温度
		txtTaikiTmp1.Text	= ""

		' サーモチラーCH2設定温度
		txtTaikiTmp2.Text	= ""

		' サーモチラーCH1使用フラグ
		cboTaikiTmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ
		cboTaikiTmpS2.SelectedIndex	= 0

		' サーモチラー配管接続変更有無フラグ
		cboTaikiTmpStop.SelectedIndex	= 0



		dtTaiki.Clear()

		dgvTaiki.DataSource	= dtTaiki

		dgvTaiki.Refresh()


	End Sub



	'*****
	'	真空低温試験データ表示クリア
	'*****
	Private Sub ClearDispVcmLowData()


		' サーモチラーCH1設定温度
		txtVcmLowTmp1.Text	= ""

		' サーモチラーCH2設定温度
		txtVcmLowTmp2.Text	= ""

		' サーモチラーCH1使用フラグ
		cboVcmLowTmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ
		cboVcmLowTmpS2.SelectedIndex	= 0

		' サーモチラー配管接続変更有無フラグ
		cboVcmLowTmpStop.SelectedIndex	= 0


		dtVcmLow.Clear()

		dgvVcmLow.DataSource	= dtVcmLow

		dgvVcmLow.Refresh()


	End Sub



	'*****
	'	真空高温試験データ表示クリア
	'*****
	Private Sub ClearDispVcmHiData()


		' サーモチラーCH1設定温度
		txtVcmHiTmp1.Text	= ""

		' サーモチラーCH2設定温度
		txtVcmHiTmp2.Text	= ""

		' サーモチラーCH1使用フラグ
		cboVcmHiTmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ
		cboVcmHiTmpS2.SelectedIndex	= 0

		' サーモチラー配管接続変更有無フラグ
		cboVcmHiTmpStop.SelectedIndex	= 0



		dtVcmHi.Clear()

		dgvVcmHi.DataSource	= dtVcmHi

		dgvVcmHi.Refresh()


	End Sub



	'*****
	'	真空高温２試験データ表示クリア
	'*****
	Private Sub ClearDispVcmHi2Data()


		' サーモチラーCH1設定温度
		txtVcmHi2Tmp1.Text		= ""

		' サーモチラーCH2設定温度
		txtVcmHi2Tmp2.Text		= ""

		' サーモチラーCH1使用フラグ
		cboVcmHi2TmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ
		cboVcmHi2TmpS2.SelectedIndex	= 0

		' サーモチラー配管接続変更有無フラグ
		cboVcmHi2TmpStop.SelectedIndex	= 0



		dtVcmHi2.Clear()

		dgvVcmHi2.DataSource		= dtVcmHi2

		dgvVcmHi2.Refresh()


	End Sub



	'*****
	'	試験基本パラメータの画面表示
	'*****
	Private Sub DispParmData()


		txtEscMd.Text		= ESCmd.ToString + ":" + convEscMdToStr( ESCmd )

		' ウエハ吸着力測定リミット値
		' 20201102 s.harada	トーカロ専用に変更で削除
		'txtBPrs.Text		= ( BPRS / 1000.0 ).ToString( "0.00 ")

		'txtHlPres.Text		= ( HLPRS / 1000.0 ).ToString( "0.0" )

		txtScrUse.Text		= SCRUSE.ToString + ":" + convScrUseToStr( SCRUSE )

		txtTPrs1.Text		= TPRS1.ToString( "0.00" )

		txtTPrs2.Text		= TPRS2.ToString( "0.00" )

		txtBakPres.Text		= BakPres.ToString( "0.0" )

		' 電極ヘッド温度安定待ち時間 (分)
		txtTmpStbW.Text		= PrmTmpStbW.ToString( "0." )

		'  20200716 s.harada
		' トーカロ対応追加
		'	20201102 s.harada	トーカロ専用に変更
		'残留吸着項目設定

		txtHeFlow.Text		= PrmHeFlow.ToString("0.")

		txtVoltImp.Text		= PrmVoltImp.ToString("0.")

		txtHeWait.Text		= PrmHeWait.ToString("0.")

		' 20201102 s.harada	追加
		txtKyuMaxTm.Text	= PrmMaxTim.ToString("0.")

		txtStabVct.Text		= PrmStabVct.ToString("0.0")

		txtStabTim.Text		= PrmStabTim.ToString("0.")

		txtBakPrs.Text		= PrmBakPrs.ToString( "0." )

	End Sub



	'*****
	'	DHDParmSet : dgvTaiki DataGridView の表示更新
	'	大気圧・試験パラメータ・データ表示
	'*****
	Private Sub DispTaikiData()



		' サーモチラーCH1使用フラグ表示
		cboTaikiTmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ表示
		cboTaikiTmpS2.SelectedIndex	= 0

		' 配管接続変更有無フラグ表示
		cboTaikiTmpStop.SelectedIndex	= 0



		If Not IsNothing( MESrec.dt( 0 ).tmp ) Then


			' 20140123
			' サーモチラーCH1使用フラグ表示
			cboTaikiTmpS1.SelectedIndex	= MESrec.dt( 0 ).schuse( 0 )

			' CH1設定温度
			txtTaikiTmp1.Text		= MESrec.dt( 0 ).tmp( 0 ).ToString( "0.0" )

			' 20140123
			' サーモチラーCH2使用フラグ表示
			cboTaikiTmpS2.SelectedIndex	= MESrec.dt( 0 ).schuse( 1 )

			' CH2設定温度
			txtTaikiTmp2.Text		= MESrec.dt( 0 ).tmp( 1 ).ToString( "0.0" )

			' 配管接続変更有無フラグ表示
			cboTaikiTmpStop.SelectedIndex	= MESrec.dt( 0 ).schchg


		End If


		dtTaiki.Clear()

		setTblTest( dtTaiki, MESrec.dt( 0 ) )

		ReIdxDataTbl( dtTaiki )

		dgvTaiki.DataSource	= dtTaiki

		dgvTaiki.Refresh()


	End Sub



	'*****
	'	DHDParmSet : dgvVcmLow DataGridView の表示更新
	'	真空１・試験パラメータ・データ表示
	'*****
	Private Sub DispVcmLowData()



		' サーモチラーCH1使用フラグ表示
		cboVcmLowTmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ表示
		cboVcmLowTmpS2.SelectedIndex	= 0

		' 配管接続変更有無フラグ表示
		cboVcmLowTmpStop.SelectedIndex	= 0



		If Not IsNothing( MESrec.dt( 1 ).tmp ) Then


			' 20140123
			' サーモチラーCH1使用フラグ表示
			cboVcmLowTmpS1.SelectedIndex	 = MESrec.dt( 1 ).schuse( 0 )

			' CH1設定温度
			txtVcmLowTmp1.Text		= MESrec.dt( 1 ).tmp( 0 ).ToString( "0.0" )

			' 20140123
			' サーモチラーCH2使用フラグ表示
			cboVcmLowTmpS2.SelectedIndex	= MESrec.dt( 1 ).schuse( 1 )

			' CH2設定温度
			txtVcmLowTmp2.Text		= MESrec.dt( 1 ).tmp( 1 ).ToString( "0.0" )

			' 配管接続変更有無フラグ表示
			cboVcmLowTmpStop.SelectedIndex	= MESrec.dt( 1 ).schchg

		End If



		dtVcmLow.Clear()

		setTblTest( dtVcmLow, MESrec.dt( 1 ) )

		ReIdxDataTbl( dtVcmLow )

		dgvVcmLow.DataSource	= dtVcmLow

		dgvVcmLow.Refresh()



	End Sub



	'*****
	'	DHDParmSet : dgvVcmHi DataGridView の表示更新
	'	真空２・試験パラメータ・データ表示
	'*****
	Private Sub DispVcmHiData()



		' サーモチラーCH1使用フラグ表示
		cboVcmHiTmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ表示
		cboVcmHiTmpS2.SelectedIndex	= 0

		' 配管接続変更有無フラグ表示
		cboVcmHiTmpStop.SelectedIndex	= 0



		If Not IsNothing( MESrec.dt( 2 ).tmp ) Then


			' 20140123
			' サーモチラーCH1使用フラグ表示
			cboVcmHiTmpS1.SelectedIndex	= MESrec.dt( 2 ).schuse( 0 )

			' CH1設定温度
			txtVcmHiTmp1.Text		= MESrec.dt( 2 ).tmp( 0 ).ToString( "0.0" )

			' 20140123
			' サーモチラーCH2使用フラグ表示
			cboVcmHiTmpS2.SelectedIndex	= MESrec.dt( 2 ).schuse( 1 )

			' CH2設定温度
			txtVcmHiTmp2.Text		= MESrec.dt( 2 ).tmp( 1 ).ToString( "0.0" )

			' 配管接続変更有無フラグ表示
			cboVcmHiTmpStop.SelectedIndex	= MESrec.dt( 2 ).schchg


		End If



		dtVcmHi.Clear()

		setTblTest( dtVcmHi, MESrec.dt( 2 ) )

		ReIdxDataTbl( dtVcmHi )

		dgvVcmHi.DataSource	= dtVcmHi

		dgvVcmHi.Refresh()


	End Sub



	'*****
	'	DHDParmSet : dgvVcmHi2 DataGridView の表示更新
	'	真空３・試験パラメータ・データ表示
	'*****
	Private Sub DispVcmHi2Data()


		' サーモチラーCH1使用フラグ表示
		cboVcmHi2TmpS1.SelectedIndex	= 0

		' サーモチラーCH2使用フラグ表示
		cboVcmHi2TmpS2.SelectedIndex	= 0

		' 配管接続変更有無フラグ表示
		cboVcmHi2TmpStop.SelectedIndex	= 0


		If Not IsNothing( MESrec.dt( 3 ).tmp ) Then


			' 20140123
			' サーモチラーCH1使用フラグ表示
			cboVcmHi2TmpS1.SelectedIndex	= MESrec.dt( 3 ).schuse( 0 )

			' CH2設定温度
			txtVcmHi2Tmp1.Text		= MESrec.dt( 3 ).tmp( 0 ).ToString( "0.0" )

			' 20140123
			' サーモチラーCH2使用フラグ表示
			cboVcmHi2TmpS2.SelectedIndex	= MESrec.dt( 3 ).schuse( 1 )

			' CH2設定温度
			txtVcmHi2Tmp2.Text		= MESrec.dt( 3 ).tmp( 1 ).ToString( "0.0" )

			' 配管接続変更有無フラグ表示
			cboVcmHi2TmpStop.SelectedIndex	= MESrec.dt( 3 ).schchg


		End If


		dtVcmHi2.Clear()

		setTblTest( dtVcmHi2, MESrec.dt( 3 ) )

		ReIdxDataTbl( dtVcmHi2 )

		dgvVcmHi2.DataSource	= dtVcmHi2

		dgvVcmHi2.Refresh()


	End Sub



	Private Sub SortDataTbl(ByRef dt As DataTable)

		Dim view		As DataView = dt.DefaultView


		view.Sort	= "PV,IV,KV,LV ASC"

		' 20201102 s.harada	ソートするならこちらを有効にすること
		'view.Sort		= "PV,IV,KV,LV,ZV ASC"


	End Sub



	Private Sub ReIdxDataTbl(ByRef dt As DataTable)

		Dim i			As Integer


		For i = 0 To dt.Rows.Count - 1

			dt.Rows( i )( "IDX" )		= i

		Next


	End Sub


#End Region




End Class