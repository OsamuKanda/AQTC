

Imports		System.Windows.Forms


Public Class MessageDlg


	Public btn		As Integer

	Public msg		As String



	Private Sub Cancel_Button_Click				_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles btnNo1.Click


		' Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.Close()


	End Sub


	Private Sub OK_Button_Click				_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles btnNo2.Click


		' Me.DialogResult = System.Windows.Forms.DialogResult.OK
		Me.Close()


	End Sub



	Private Sub btnSkip_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnNo3.Click


		Me.Close()


	End Sub



	Private Sub MessageDlg_Load				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles MyBase.Load


		lblMsg.Text		= msg

		If lblMsg.Width > Me.Width - 20 Then

			Me.Width		= lblMsg.Width + 20

		End If


		Select Case	btn

		Case	OPIPok

			btnNo1.Visible		= False

			btnNo2.Visible		= False

			btnNo3.Text		= "確認"

			btnNo3.DialogResult	= Windows.Forms.DialogResult.OK

		Case	OPIPco

			btnNo1.Visible		= False

			btnNo2.Text		= "中止"

			btnNo2.DialogResult	= Windows.Forms.DialogResult.Cancel

			btnNo3.Text		= "続行"

			btnNo3.DialogResult	= Windows.Forms.DialogResult.OK

		Case	OPIPcoi

			btnNo1.Text		= "中止"

			btnNo2.DialogResult	= Windows.Forms.DialogResult.Cancel

			btnNo2.Text		= "続行"

			btnNo2.DialogResult	= Windows.Forms.DialogResult.OK

			btnNo3.Text		= "ｽｷｯﾌﾟ"

			btnNo3.DialogResult	= Windows.Forms.DialogResult.Ignore

		Case	OPIPcr

			btnNo1.Visible		= False

			btnNo2.Text		= "中止"

			btnNo2.DialogResult	= Windows.Forms.DialogResult.Cancel

			btnNo3.Text		= "ﾘﾄﾗｲ"

			btnNo3.DialogResult	= Windows.Forms.DialogResult.Retry

		Case	OPIPyn

			btnNo1.Visible		= False

			btnNo2.Text		= "ＹＥＳ"

			btnNo2.DialogResult	= Windows.Forms.DialogResult.Yes

			btnNo3.Text		= "ＮＯ"

			btnNo3.DialogResult	= Windows.Forms.DialogResult.No

		Case	OPIPcri

			btnNo1.Text		= "中止"

			btnNo2.DialogResult	= Windows.Forms.DialogResult.Cancel

			btnNo2.Text		= "ﾘﾄﾗｲ"

			btnNo2.DialogResult	= Windows.Forms.DialogResult.Retry

			btnNo3.Text		= "ｽｷｯﾌﾟ"

			btnNo3.DialogResult	= Windows.Forms.DialogResult.Ignore

		End Select


	End Sub



End Class
