<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MessageDlg
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnNo1 = New System.Windows.Forms.Button()
        Me.btnNo2 = New System.Windows.Forms.Button()
        Me.btnNo3 = New System.Windows.Forms.Button()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnNo1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnNo2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnNo3, 2, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(74, 71)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(212, 36)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btnNo1
        '
        Me.btnNo1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnNo1.DialogResult = System.Windows.Forms.DialogResult.Abort
        Me.btnNo1.Location = New System.Drawing.Point(5, 4)
        Me.btnNo1.Margin = New System.Windows.Forms.Padding(4)
        Me.btnNo1.Name = "btnNo1"
        Me.btnNo1.Size = New System.Drawing.Size(60, 28)
        Me.btnNo1.TabIndex = 1
        Me.btnNo1.Text = "中止"
        '
        'btnNo2
        '
        Me.btnNo2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnNo2.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnNo2.Location = New System.Drawing.Point(75, 4)
        Me.btnNo2.Margin = New System.Windows.Forms.Padding(4)
        Me.btnNo2.Name = "btnNo2"
        Me.btnNo2.Size = New System.Drawing.Size(60, 28)
        Me.btnNo2.TabIndex = 0
        Me.btnNo2.Text = "OK"
        '
        'btnNo3
        '
        Me.btnNo3.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnNo3.DialogResult = System.Windows.Forms.DialogResult.Ignore
        Me.btnNo3.Location = New System.Drawing.Point(146, 4)
        Me.btnNo3.Margin = New System.Windows.Forms.Padding(4)
        Me.btnNo3.Name = "btnNo3"
        Me.btnNo3.Size = New System.Drawing.Size(60, 28)
        Me.btnNo3.TabIndex = 2
        Me.btnNo3.Text = "ｽｷｯﾌﾟ"
        '
        'lblMsg
        '
        Me.lblMsg.AutoSize = True
        Me.lblMsg.Location = New System.Drawing.Point(10, 28)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(51, 16)
        Me.lblMsg.TabIndex = 1
        Me.lblMsg.Text = "Label1"
        '
        'MessageDlg
        '
        Me.AcceptButton = Me.btnNo2
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.btnNo1
        Me.ClientSize = New System.Drawing.Size(300, 117)
        Me.Controls.Add(Me.lblMsg)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MessageDlg"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "確認"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnNo2 As System.Windows.Forms.Button
    Friend WithEvents btnNo1 As System.Windows.Forms.Button
    Friend WithEvents btnNo3 As System.Windows.Forms.Button
    Friend WithEvents lblMsg As System.Windows.Forms.Label

End Class
