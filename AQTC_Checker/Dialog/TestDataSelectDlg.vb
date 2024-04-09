

Public Class TestDataSelectDlg



	Private Sub TestDataSelectDlg_Load			_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles MyBase.Load


		'
		'   20200716 s.harada
		'	AQTC対応
		'
		' 電極ヘッド種別の取得
		Dim strPath		As String

		strPath		= System.IO.Directory.GetCurrentDirectory() + "\testTcl"

		GetTestFiletoCombbox( strPath , cboDHead )



		If cboDHead.Items.Count = 0 Then

			Me.DialogResult		= Windows.Forms.DialogResult.None

			Me.Close()

		Else

			cboDHead.SelectedIndex	= 0

		End If


	End Sub



	Private Sub btnCancel_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnCancel.Click


		Me.Close()


	End Sub



	Private Sub btnOk_Click					_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnOk.Click


		Me.Close()


	End Sub



End Class