
'*****
'	サ－モチラ－設定温度入力ダイアログクラス
'
'	<update>
'	20140123 y.goto
'		サーモチラー使用設定追加
'*****
Public Class TestTmpDlg


	'
	'	CH1設定温度
	'
	Public pTemp1		As String


	'
	'	CH2設定温度
	'
	Public pTemp2		As String


	'
	'	CH1使用フラグ
	'	0	使用しない
	'	1	使用する
	'
	Public pTmpUse1		As Integer


	'
	'	CH2使用フラグ
	'	0	使用しない
	'	1	使用する
	'
	Public pTmpUse2		As Integer



	'
	'	配管接続変更有無フラグ
	'	0	接続変更無し
	'	1	接続変更有り
	'
	Public pTmpStop		As Integer



	'*****
	'	ＣＨ１設定温度入力テキストボックス入力許可設定
	'*****
	Private Sub EnbSettxtTmp1()



		If pTmpUse1 = 0 Then

			'
			'	CH1使用しない
			'	
			txtTmp1.Enabled		= False

		Else

			'
			'	CH1使用する
			'
			txtTmp1.Enabled		= True

		End If



	End Sub


	
	'*****
	'	ＣＨ２設定温度入力テキストボックス入力許可設定
	'*****
	Private Sub EnbSettxtTmp2()



		If pTmpUse2 = 0 Then

			'
			'	CH2使用しない
			'	
			txtTmp2.Enabled		= False

		Else

			'
			'	CH2使用する
			'
			txtTmp2.Enabled		= True

		End If



	End Sub




	Private Sub TestTmpDlg_Load				_
	(							_
		sender			As Object,		_
		e			As System.EventArgs	_
	)	Handles Me.Load



		'
		'	20140123
		'	サーモチラー使用設定追加
		'

		' コンボボックスをクリア
		cboTmp1.Items.Clear()

		' コンボボックスへ追加
		cboTmp1.Items.Add( DefStrNotUse )

		' コンボボックスへ追加
		cboTmp1.Items.Add( DefStrUse )

		' 初期選択値設定
		cboTmp1.SelectedIndex	= pTmpUse1


		' コンボボックスをクリア
		cboTmp2.Items.Clear()

		' コンボボックスへ追加
		cboTmp2.Items.Add( DefStrNotUse )

		' コンボボックスへ追加
		cboTmp2.Items.Add( DefStrUse )

		' 初期選択値設定
		cboTmp2.SelectedIndex	= pTmpUse2


		'
		'	配管接続変更有無選択コンボボックス
		'
		cboTmpStop.Items.Clear()

		' コンボボックスへ追加
		cboTmpStop.Items.Add( DefStrTmpNoChg )

		' コンボボックスへ追加
		cboTmpStop.Items.Add( DefStrTmpChg )

		' 初期選択値設定
		cboTmpStop.SelectedIndex	= pTmpStop



		'
		'	サーモチラーＣＨ１使用設定
		'

		' 設定温度
		txtTmp1.Text		= pTemp1

		' ＣＨ１設定温度入力テキストボックス入力許可設定
		EnbSettxtTmp1()



		'
		'	サーモチラーＣＨ２使用設定
		'

		' 設定温度
		txtTmp2.Text		= pTemp2

		' ＣＨ２設定温度入力テキストボックス入力許可設定
		EnbSettxtTmp2()



	End Sub



	'*****
	'	オリジナル・未使用
	'*****
	'Private Sub TestTmpDlg_Load_ORG				_
	'(							_
	'	sender			As Object,		_
	'	e			As System.EventArgs	_
	')	Handles Me.Load



	'	Select Case	pTmpUse

	'	Case	0

	'		txtTmp1.Text		= pTemp1

	'		txtTmp2.Text		= "-"

	'		txtTmp1.Enabled		= True

	'		txtTmp2.Enabled		= False

	'	Case	1

	'		txtTmp1.Text		= "-"

	'		txtTmp2.Text		= pTemp2

	'		txtTmp1.Enabled		= False

	'		txtTmp2.Enabled		= True

	'	Case	2

	'		txtTmp1.Text		= pTemp1

	'		txtTmp2.Text		= pTemp2

	'		txtTmp1.Enabled		= True

	'		txtTmp2.Enabled		= True

	'	End Select


	'End Sub




	Private Sub btnSet_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnSet.Click




		If IsNumeric( txtTmp1.Text ) = True Then

			pTemp1			= CDbl( txtTmp1.Text ).ToString( "0.0" )

		Else

			pTemp1			= "20.0"

		End If


		If IsNumeric( txtTmp2.Text ) = True Then

			pTemp2			= CDbl( txtTmp2.Text ).ToString( "0.0" )

		Else

			pTemp2			= "20.0"

		End If



	End Sub



	'*****
	'	CH1使用選択コンボボックス選択変更イベント
	'*****
	Private Sub cboTmp1_SelectedIndexChanged		_
	(							_
		ByVal sender		As Object,		_
		ByVal e			As System.EventArgs	_
	)	Handles cboTmp1.SelectedIndexChanged



		' CH1使用フラグ
		pTmpUse1		= cboTmp1.SelectedIndex

		' ＣＨ１設定温度入力テキストボックス入力許可設定
		EnbSettxtTmp1()
	

	End Sub



	'*****
	'	CH2使用選択コンボボックス選択変更イベント
	'*****
	Private Sub cboTmp2_SelectedIndexChanged		_
	(							_
		ByVal sender		As Object,		_
		ByVal e			As System.EventArgs	_
	)	Handles cboTmp2.SelectedIndexChanged



		' CH2使用フラグ
		pTmpUse2		= cboTmp2.SelectedIndex

		' ＣＨ２設定温度入力テキストボックス入力許可設定
		EnbSettxtTmp2()
	

	End Sub



	'*****
	'	配管接続変更有無選択コンボボックス選択変更イベント
	'*****
	Private Sub cboTmpStop_SelectedIndexChanged		_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles cboTmpStop.SelectedIndexChanged



		' 配管接続変更有無フラグ
		pTmpStop		= cboTmpStop.SelectedIndex


	End Sub




End Class