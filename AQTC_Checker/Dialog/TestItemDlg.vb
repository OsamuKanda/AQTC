﻿
'*****
'	［測定項目］ダイアログ・クラス
'
'	絶縁耐圧、ウエハ吸着力、Ｈｅリーク量の
'	各測定を行うときの条件を入力する
'
'	電圧印加箇所選択
'	測定電圧
'	判定値
'
'	DHDParmSetフォームからこのダイアログが
'	作成されて、測定項目入力を行う
'
'*****
Public Class TestItemDlg


	'
	'	追加、変更処理フラグ
	'
	'	0	追加
	'	1	変更
	'
	Public pMode		As Integer

	'
	'	測定環境
	'
	'	0	大気圧
	'	1	真空低温
	'	2	真空高温
	'	3	真空高温２
	'
	Public pSel		As Integer

	'
	'	電極ヘッドの種別
	'
	'	0	モノポール電極
	'	1	ダイポール電極
	'
	Public pESCmd		As Integer

	'
	'	データテーブル
	'
	'	DataTableから特定の値を抽出するためにはDataTable内の行と列を指定します。
	'	これらもクラス化されていて行を表しているのはDataRowクラス、
	'	列を表しているのはDataColumnクラスです。
	'	特に重要なのは行をあらわすDataRowクラスです。
	'	DataTableの 実体は複数のDataRowで構成されていると考えることもできます。
	'	DataColumnクラスの方はそれほど出番は多くありません。
	'
	'	個々のDataRowにアクセスするにはDataTableのRowsプロパティを使用します。
	'	RowsプロパティはDataRowのコレクションになっていて任意のDataRowに
	'	アクセスすることができます。
	'	たとえば、３番目のDataRowを取得するには次のように書きます。
	'
	'		Dim Row As DataRow
	'
	'		Row = Table.Rows(2)
	'
	'	行の数はDataTable.Rows.Countで取得できます。
	'	列の数はDataTalbe.Column.Countで取得できます。
	'
	'
	'
	Public dt		As DataTable

	' データテーブルの行データ
	Public pDtRow		As DataRow



	Private Sub TestItemDlg_Load			_
	(						_
		sender		As System.Object,	_
		e		As System.EventArgs	_
	)	Handles MyBase.Load

		Dim i		As Integer



		'
		'	表示クリア
		'

		' 絶縁耐圧試験印加電圧
		txtIsoV.Text		= ""

		' 吸着力測定・ＳＤＣ電源印加電圧１
		txtKyuV1.Text		= ""

		' 吸着力測定・ＳＤＣ電源印加電圧２
		txtKyuV2.Text		= ""

		' 吸着力測定・判定値
		txtKyuBase.Text		= ""

		' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
		txtLekV1.Text		= ""

		' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
		txtLekV2.Text		= ""

		' Ｈｅリーク測定・判定値
		txtLekBase.Text		= ""

		'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
		txtLekBase2.Text		= ""


		'	20201102 s.harada	AQTC対応追加
		' 吸着力測定・Ｈｅ流量
		TxtKyuHe.Text		= ""

		' 残留吸着力測定・ＳＤＣ電源印加電圧１
		txtZKyuV1.Text		= ""

		' 残留吸着力測定・ＳＤＣ電源印加電圧２
		txtZKyuV2.Text		= ""

		' 残留吸着力測定・判定値
		txtZKyuBase.Text	= ""


		'
		'	電圧印加箇所コンボボックスに登録
		'
		For i = 0 To CON_POLE_MAX

			'
			'	電圧印加箇所番号から電圧印加場所文字列へ変換し、
			'	コンボボックスに登録する
			'
			cboVinPos.Items.Add( convVoltInPosToStr( i ) )

		Next


		' 追加
		If pMode = 0 Then

			' 絶縁耐圧測定 GroupBox
			grpZetsuen.Enabled	= False

			' ウエハ吸着力測定 GroupBox
			grpKyucyaku.Enabled	= False

			' Ｈｅリーク量測定 GroupBox
			grpHeGas.Enabled	= False

			'	20201102 s.harada	AQTC対応追加
			' 残留吸着力測定 GroupBox
			grpZanKyucyaku.Enabled	= False


			' 電圧印加箇所選択用のコンボボックス
			cboVinPos.Enabled	= True

			' [変更]ボタン
			btnSet.Visible		= False

			' [追加]ボタン		[変更]ボタン左位置
			btnAdd.Left		= btnSet.Left

			' [全削除]ボタン
			btnDelete.Visible	= False


			' モノポール
			If pESCmd = 0 Then

				' 吸着力測定ＳＤＣ電源印加電圧２
				txtKyuV2.Enabled	= False

				' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
				txtLekV2.Enabled	= False

			End If


			Exit Sub

		End If


		' [追加]ボタン
		btnAdd.Visible		= False


		' 電圧印加箇所選択用のコンボボックス
		cboVinPos.Enabled	= False

		cboVinPos.SelectedIndex	= pDtRow( "PV" )

		' 絶縁耐圧試験印加電圧
		txtIsoV.Text		= pDtRow( "ISO_VOLT" )

		' 吸着力測定・ＳＤＣ電源印加電圧１
		txtKyuV1.Text		= pDtRow( "KYU_VOLT1" )

		' 吸着力測定・ＳＤＣ電源印加電圧２
		txtKyuV2.Text		= pDtRow( "KYU_VOLT2" )

		' 吸着力測定・判定値
		txtKyuBase.Text		= pDtRow( "KYU_BASE" )


		If pESCmd = 0 Then

			' 吸着力測定・ＳＤＣ電源印加電圧２
			txtKyuV2.Enabled	= False

		End If

		'	20201102 s.harada	AQTC対応追加
		' 吸着力測定・Ｈｅ流量
		TxtKyuHe.Text	= pDtRow("KYU_HE")



		' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
		txtLekV1.Text		= pDtRow( "LEK_VOLT1" )

		' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
		txtLekV2.Text		= pDtRow( "LEK_VOLT2" )

		'▼ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）
		If pDtRow("LEK_PTN").ToString.Trim("-") = "" Then
			ChkPa1k.Checked = True
			ChkPa2k.Checked = True
			ChkPa3k.Checked = True
			ChkPa4k.Checked = True
			ChkPa6k.Checked = True
		Else
			ChkPa1k.Checked = False
			ChkPa2k.Checked = False
			ChkPa3k.Checked = False
			ChkPa4k.Checked = False
			ChkPa6k.Checked = False
			For Each pa In pDtRow("LEK_PTN").ToString.Split(",")
				Select Case pa
					Case "1"
						ChkPa1k.Checked = True
					Case "2"
						ChkPa2k.Checked = True
					Case "3"
						ChkPa3k.Checked = True
					Case "4"
						ChkPa4k.Checked = True
					Case "6"
						ChkPa6k.Checked = True
				End Select
			Next

		End If
		'▲ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）

		' Ｈｅリーク測定・判定値
		txtLekBase.Text		= pDtRow( "LEK_BASE" )

		'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
		txtLekBase2.Text	= pDtRow( "LEK_BASE2" )


		If pESCmd = 0 Then

			' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
			txtLekV2.Enabled	= False

		End If



		'	20201102 s.harada	AQTC対応追加
		' 残留吸着力測定・ＳＤＣ電源印加電圧１
		txtZKyuV1.Text		= pDtRow( "ZKU_VOLT1" )

		' 残留吸着力測定・ＳＤＣ電源印加電圧２
		txtZKyuV2.Text		= pDtRow( "ZKU_VOLT2" )

		' 残留吸着力測定・判定値
		txtZKyuBase.Text	= pDtRow( "ZKU_BASE" )

		If pESCmd = 0 Then

			' 残留吸着力測定・ＳＤＣ電源印加電圧２
			txtZKyuV2.Enabled	= False

		End If



	End Sub



	'*****
	'	[測定項目]ダイアログ
	'	[追加]ボタンクリック処理
	'*****
	Private Sub btnAdd_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnAdd.Click


		'
		'	cboVinPos	電圧印加箇所
		'	txtIsoV		絶縁耐圧試験印加電圧
		'	txtKyu1		吸着力測定ＳＤＣ電源印加電圧１
		'	txtLekV1	Ｈｅリーク測定・ＳＤＣ電源印加電圧１
		'	txtZKyuV1	残留吸着力測定・ＳＤＣ電源印加電圧１　20201102 追加
		'
		'If						_
		'	cboVinPos.Text = "" Or			_
		'	(					_
		'		txtIsoV.Text = "" And		_
		'		txtKyuV1.Text = "" And		_
		'		txtLekV1.Text = ""		_
		'	)					_
		If						_
			cboVinPos.Text = "" Or			_
			(					_
				txtIsoV.Text = "" And		_
				txtKyuV1.Text = "" And		_
				txtLekV1.Text = "" And		_
				txtZKyuV1.Text = ""		_
			)					_
		Then

			Exit Sub

		End If


		' 電圧印加箇所の選択値を取り出す
		pDtRow( "PV" )			= cboVinPos.SelectedIndex

		' 電圧印加場所文字列
		pDtRow( "VIN_POS" )		= convVoltInPosToStr( cboVinPos.SelectedIndex )


		'
		'	絶縁耐圧試験印加電圧
		'
		If txtIsoV.Text = "" Or IsNumeric( txtIsoV.Text ) = False Then

			' 入力値異常
			pDtRow( "IV" )			= 0

			pDtRow( "ISO_VOLT" )		= "-"

		Else

			' 入力値正常
			pDtRow( "IV" )			= Math.Abs( CDbl( txtIsoV.Text ) )

			pDtRow( "ISO_VOLT" )		= txtIsoV.Text

		End If


		'
		'	吸着力測定ＳＤＣ電源印加電圧１
		'
		If txtKyuV1.Text = "" Or IsNumeric( txtKyuV1.Text ) = False Then

			pDtRow( "KV" )			= 0

			pDtRow( "KYU_VOLT1" )		= "-"

			pDtRow( "KYU_VOLT2" )		= "-"

			pDtRow( "KYU_BASE" )		= "-"


			' 20200716 s.harada
			' AQTC対応追加
			pDtRow( "KYU_HE" )		= "-"

		Else

			' 吸着力測定ＳＤＣ電源印加電圧１
			pDtRow( "KV" )			= Math.Abs( CDbl( txtKyuV1.Text ) )

			' 吸着力測定ＳＤＣ電源印加電圧１
			pDtRow( "KYU_VOLT1" )		= txtKyuV1.Text


			' 吸着力測定ＳＤＣ電源印加電圧２
			If txtKyuV2.Text = "" Or IsNumeric( txtKyuV2.Text ) = False Then

				pDtRow( "KYU_VOLT2" )		= "0"
			Else

				' 吸着力測定ＳＤＣ電源印加電圧２
				pDtRow( "KYU_VOLT2" )		= txtKyuV2.Text

			End If

			' 吸着力測定・判定値
			pDtRow( "KYU_BASE" )		= txtKyuBase.Text


			' 20200716 s.harada
			' AQTC対応追加
			' 吸着力測定・He流量
			pDtRow( "KYU_HE" )		= TxtKyuHe.Text

		End If


		' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
		If txtLekV1.Text = "" Or IsNumeric( txtLekV1.Text ) = False Then

			pDtRow( "LV" )			= 0

			pDtRow( "LEK_VOLT1" )		= "-"

			pDtRow( "LEK_VOLT2" )		= "-"

			pDtRow( "LEK_BASE" )		= "-"

			'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
			pDtRow( "LEK_BASE2" )		= "-"


		Else

			' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
			pDtRow( "LV" )			= Math.Abs( CDbl( txtLekV1.Text ) )

			' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
			pDtRow( "LEK_VOLT1" )		= txtLekV1.Text

			' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
			If txtLekV2.Text = "" Or IsNumeric( txtLekV2.Text ) = False Then

				pDtRow( "LEK_VOLT2" )		= "0"

			Else

				' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
				pDtRow( "LEK_VOLT2" )		= txtLekV2.Text

			End If

			' Ｈｅリーク測定・判定値
			pDtRow( "LEK_BASE" )		= txtLekBase.Text

			'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
			pDtRow( "LEK_BASE2" )		= txtLekBase2.Text


			'▼ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）
			Dim pa As New List(Of String)

			If ChkPa1k.Checked Then
				pa.Add(1)
			End If
			If ChkPa2k.Checked Then
				pa.Add(2)
			End If
			If ChkPa3k.Checked Then
				pa.Add(3)
			End If
			If ChkPa4k.Checked Then
				pa.Add(4)
			End If
			If ChkPa6k.Checked Then
				pa.Add(6)
			End If
			pDtRow("LEK_PTN") = String.Join(",", pa)
			'▲ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）
		End If

		'	20201102 s.harada	AQTC対応追加

		'
		'	残留吸着力測定ＳＤＣ電源印加電圧１
		'
		If txtZKyuV1.Text = "" Or IsNumeric(txtZKyuV1.Text) = False Then

			pDtRow( "ZV" )			= 0

			pDtRow( "ZKU_VOLT1" )		= "-"

			pDtRow( "ZKU_VOLT2" )		= "-"

			pDtRow( "ZKU_BASE" )		= "-"


		Else

			' 吸着力測定ＳＤＣ電源印加電圧１
			pDtRow( "ZV" )		= Math.Abs(CDbl(txtZKyuV1.Text))

			' 吸着力測定ＳＤＣ電源印加電圧１
			pDtRow( "ZKU_VOLT1" )		= txtZKyuV1.Text


			' 吸着力測定ＳＤＣ電源印加電圧２
			If txtZKyuV2.Text = "" Or IsNumeric( txtZKyuV2.Text ) = False Then

				pDtRow( "ZKU_VOLT2" )		= "0"

			Else

				' 吸着力測定ＳＤＣ電源印加電圧２
				pDtRow( "ZKU_VOLT2" )		= txtZKyuV2.Text

			End If

			' 吸着力測定・判定値
			pDtRow( "ZKU_BASE" )		= txtZKyuBase.Text

		End If




	End Sub


	'*****
	'	[変更]ボタンクリックイベント
	'*****
	Private Sub btnSet_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnSet.Click



		'
		'	絶縁耐圧試験印加電圧
		'
		If IsNumeric( txtIsoV.Text ) Then

			pDtRow( "IV" )			= Math.Abs( CDbl( txtIsoV.Text ) )

			pDtRow( "ISO_VOLT" )		= txtIsoV.Text

		Else

			pDtRow( "IV" )			= 0

			pDtRow( "ISO_VOLT" )		= "-"

		End If


		'
		'	吸着力変更
		'

		' 吸着力測定・ＳＤＣ電源印加電圧１
		If IsNumeric( txtKyuV1.Text ) Then

			' 吸着力測定・ＳＤＣ電源印加電圧１
			pDtRow( "KV" )			= Math.Abs( CDbl( txtKyuV1.Text ) )

			' ' 吸着力測定・ＳＤＣ電源印加電圧１
			pDtRow( "KYU_VOLT1" )		= txtKyuV1.Text

			' ' 吸着力測定・ＳＤＣ電源印加電圧２
			If txtKyuV2.Text = "" Or IsNumeric( txtKyuV2.Text ) = False Then

				pDtRow("KYU_VOLT2")		= "0"

			Else

				' 吸着力測定・ＳＤＣ電源印加電圧２
				pDtRow( "KYU_VOLT2" )		= txtKyuV2.Text

			End If

			' 吸着力測定・判定値
			'	20201102 s.harada	０設定はスペースに変更する
			'pDtRow("KYU_BASE")		= txtKyuBase.Text
			If IsNumeric( txtKyuBase.Text ) AndAlso CInt( txtKyuBase.Text ) > 0 Then

				pDtRow("KYU_BASE")		= CInt( txtKyuBase.Text ).ToString

			Else 

				pDtRow("KYU_BASE")		= " "
 
			End If

			'	20201102 s.harada	AQTC対応追加
			' 吸着力測定・He流量
			pDtRow( "KYU_HE" )		= TxtKyuHe.Text


		Else

			pDtRow( "KV" )			= 0

			pDtRow( "KYU_VOLT1" )		= "-"

			pDtRow( "KYU_VOLT2" )		= "-"

			pDtRow( "KYU_BASE" )		= "-"

			'	20201102 s.harada	AQTC対応追加

			pDtRow( "KYU_HE" )		= "-"


		End If


		'
		'	Ｈｅリーク量変更
		'

		' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
		If IsNumeric( txtLekV1.Text ) Then

			' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
			pDtRow( "LV" )		= Math.Abs( CDbl( txtLekV1.Text ) )

			' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
			pDtRow( "LEK_VOLT1" )	= txtLekV1.Text

			' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
			If txtLekV2.Text = "" Or IsNumeric( txtLekV2.Text ) = False Then

				pDtRow( "LEK_VOLT2" )	= "0"

			Else

				' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
				pDtRow( "LEK_VOLT2" )	= txtLekV2.Text

			End If

			'▼ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）
			Dim pa As New List(Of String)

			If ChkPa1k.Checked Then
				pa.Add(1)
			End If
			If ChkPa2k.Checked Then
				pa.Add(2)
			End If
			If ChkPa3k.Checked Then
				pa.Add(3)
			End If
			If ChkPa4k.Checked Then
				pa.Add(4)
			End If
			If ChkPa6k.Checked Then
				pa.Add(6)
			End If
			pDtRow("LEK_PTN") = String.Join(",", pa)
			'▲ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）

			' Ｈｅリーク測定・判定値
			'	20201102 s.harada	０設定はスペースに変更する
			'pDtRow( "LEK_BASE" )	= txtLekBase.Text
			If IsNumeric( txtLekBase.Text ) AndAlso CDbl( txtLekBase.Text ) > 0 Then

				pDtRow("LEK_BASE")		= CDbl( txtLekBase.Text ).ToString( "F1" )

			Else 

				pDtRow("LEK_BASE")		= " "
 
			End If

			'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
			If IsNumeric( txtLekBase2.Text ) AndAlso CDbl( txtLekBase2.Text ) > 0 Then

				pDtRow("LEK_BASE2")		= CDbl( txtLekBase2.Text ).ToString( "F1" )

			Else 

				pDtRow("LEK_BASE2")		= " "
 
			End If


		Else

			pDtRow( "LV" )		= 0

			pDtRow( "LEK_VOLT1" )	= "-"

			pDtRow( "LEK_VOLT2" )	= "-"

			pDtRow( "LEK_BASE" )	= "-"

			'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
			pDtRow( "LEK_BASE2" )	= "-"


		End If


		'	20201102 s.harada	AQTC対応追加
		'
		'	残留吸着力測定ＳＤＣ電源印加電圧１
		'
		If IsNumeric(txtZKyuV1.Text) = True Then

			' 吸着力測定ＳＤＣ電源印加電圧１
			pDtRow("ZV") = Math.Abs(CDbl(txtZKyuV1.Text))

			' 吸着力測定ＳＤＣ電源印加電圧１
			pDtRow("ZKU_VOLT1") = txtZKyuV1.Text


			' 吸着力測定ＳＤＣ電源印加電圧２
			If txtZKyuV2.Text = "" Or IsNumeric(txtZKyuV2.Text) = False Then

				pDtRow("ZKU_VOLT2") = "0"

			Else

				' 吸着力測定ＳＤＣ電源印加電圧２
				pDtRow("ZKU_VOLT2") = txtZKyuV2.Text

			End If

			' 吸着力測定・判定値	０設定はスペースに変更する
			If IsNumeric( txtZKyuBase.Text ) AndAlso CInt( txtZKyuBase.Text ) > 0 Then

				pDtRow("ZKU_BASE")		= CInt( txtZKyuBase.Text ).ToString

			Else 

				pDtRow("ZKU_BASE")		= " "
 
			End If


		Else

			pDtRow("ZV") = 0

			pDtRow("ZKU_VOLT1") = "-"

			pDtRow("ZKU_VOLT2") = "-"

			pDtRow("ZKU_BASE") = "-"

		End If




		'	20201102 s.harada	AQTC対応で変更
		'If				_
		'	pDtRow( "IV" ) = 0 And		_
		'	pDtRow( "KV" ) = 0 And		_
		'	pDtRow( "LV" ) = 0		_
		'Then

		'	pDtRow( "PV" )		= -1

		'	pDtRow( "VIN_POS" )	= ""

		'End If
		If				_
			pDtRow( "IV" ) = 0 And		_
			pDtRow( "KV" ) = 0 And		_
			pDtRow( "LV" ) = 0 And		_
			pDtRow( "ZV" ) = 0		_
		Then

			pDtRow( "PV" )			= -1

			pDtRow( "VIN_POS" )		= ""

		End If



	End Sub



	'*****
	'	[全削除]ボタンクリックイベント
	'*****
	Private Sub btnDelete_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnDelete.Click


		pDtRow( "PV" )		= -1

		pDtRow( "VIN_POS" )	= ""


	End Sub



	'*****
	'	絶縁耐圧削除
	'*****
	Private Sub btnIsoDel_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnIsoDel.Click


		' 絶縁耐圧試験印加電圧
		txtIsoV.Text		= ""


	End Sub



	'*****
	'	吸着力削除
	'*****
	Private Sub btnKyuDel_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnKyuDel.Click


		' 吸着力測定・ＳＤＣ電源印加電圧１
		txtKyuV1.Text		= ""

		' 吸着力測定・ＳＤＣ電源印加電圧２
		txtKyuV2.Text		= ""

		' 吸着力測定・判定値
		txtKyuBase.Text		= ""

		'	20201102 s.harada	AQTC対応追加
		' 吸着力測定・He流量
		TxtKyuHe.Text		= ""


	End Sub



	'*****
	'	Ｈｅリーク量削除
	'*****
	Private Sub btnLekDel_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnLekDel.Click


		' Ｈｅリーク測定・ＳＤＣ電源印加電圧１
		txtLekV1.Text		= ""

		' Ｈｅリーク測定・ＳＤＣ電源印加電圧２
		txtLekV2.Text		= ""

		' Ｈｅリーク測定・判定値
		txtLekBase.Text		= ""

		'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
		txtLekBase2.Text		= ""



	End Sub



	'*****
	'	20200716 s.harada
	'	残留吸着力削除
	'*****
	Private Sub btnZKyuDel_Click				_
	(							_
		sender		As System.Object,		_
		e		As System.EventArgs		_
	) Handles btnZKyuDel.Click


		' 残留吸着力測定・ＳＤＣ電源印加電圧１
		txtZKyuV1.Text			= ""

		' 残留吸着力測定・ＳＤＣ電源印加電圧２
		txtZKyuV2.Text			= ""

		' 残留吸着力測定・判定値
		txtZKyuBase.Text		= ""

	End Sub


	'*****
	'	電圧印加位置変更
	'*****
	Private Sub cboVinPos_TextChanged _
	(
		sender As Object,
		e As System.EventArgs
	)	Handles cboVinPos.TextChanged


		'
		'	絶縁耐圧は入力
		'

		' 絶縁耐圧測定 GroupBox
		grpZetsuen.Enabled	= True


		' データなし
		If dt.Rows.Count = 0 Then

			' ウエハ吸着力測定 GroupBox
			grpKyucyaku.Enabled	= True

			' Ｈｅリーク量測定 GroupBox
			grpHeGas.Enabled	= True

			'	20201102 s.harada	AQTC対応追加

			' 残留吸着力測定 GroupBox
			grpZanKyucyaku.Enabled		= True


			Exit Sub

		End If


		' 吸着力は他電極で設定済みなら入力なし
		Dim i			As Integer


		For i = 0 To dt.Rows.Count - 1

			If dt.Rows(i)("KV") > 0 Then

				' 電圧印加箇所
				If dt.Rows( i )( "VIN_POS" ) = cboVinPos.Text Then

					' ウエハ吸着力測定 GroupBox
					grpKyucyaku.Enabled	= True

				Else

					' ウエハ吸着力測定 GroupBox
					grpKyucyaku.Enabled	= False

				End If

				Exit For

			End If

		Next

		If i = dt.Rows.Count Then

			' ウエハ吸着力測定 GroupBox
			grpKyucyaku.Enabled	= True

		End If


		' リーク量は他電極で設定済みなら入力なし
		For i = 0 To dt.Rows.Count - 1

			If dt.Rows( i )( "LV" ) > 0 Then

				' 電圧印加箇所
				If dt.Rows( i )( "VIN_POS" ) = cboVinPos.Text Then

					' Ｈｅリーク量測定 GroupBox
					grpHeGas.Enabled	= True

				Else

					' Ｈｅリーク量測定 GroupBox
					grpHeGas.Enabled	= False

				End If

				Exit For

			End If

		Next

		If i = dt.Rows.Count Then

			' Ｈｅリーク量測定 GroupBox
			grpHeGas.Enabled	= True

		End If


		'	20201102 s.harada	AQTC対応追加
		' 残留吸着力は他電極で設定済みなら入力なし
		For i = 0 To dt.Rows.Count - 1

			If dt.Rows( i )( "ZV" ) > 0 Then

				' 電圧印加箇所
				If dt.Rows( i )( "VIN_POS" ) = cboVinPos.Text Then

					' 残留吸着力測定 GroupBox
					grpZanKyucyaku.Enabled	= True

				Else

					' 残留吸着力測定 GroupBox
					grpZanKyucyaku.Enabled = False

				End If

				Exit For

			End If

		Next

		If i = dt.Rows.Count Then

			' 残留吸着力測定 GroupBox
			grpZanKyucyaku.Enabled		= True

		End If



		' 大気圧、真空高温２は吸着力、リーク量設定なし
		If pSel = 0 Or pSel = 3 Then

			' ウエハ吸着力測定 GroupBox
			grpKyucyaku.Enabled	= False

			' Ｈｅリーク量測定 GroupBox
			grpHeGas.Enabled	= False

			'	20201102 s.harada	AQTC対応追加

			' 残留吸着力測定 GroupBox
			grpZanKyucyaku.Enabled		= False


		End If

	End Sub


End Class