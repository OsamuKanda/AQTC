<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestDataSelectDlg
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
Me.Label1 = New System.Windows.Forms.Label()
Me.cboDHead = New System.Windows.Forms.ComboBox()
Me.btnOk = New System.Windows.Forms.Button()
Me.btnCancel = New System.Windows.Forms.Button()
Me.SuspendLayout()
'
'Label1
'
Me.Label1.AutoSize = True
Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
Me.Label1.Location = New System.Drawing.Point(13, 32)
Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
Me.Label1.Name = "Label1"
Me.Label1.Size = New System.Drawing.Size(337, 20)
Me.Label1.TabIndex = 0
Me.Label1.Text = "変更するデータファイルを選択してください。"
'
'cboDHead
'
Me.cboDHead.FormattingEnabled = True
Me.cboDHead.Location = New System.Drawing.Point(17, 76)
Me.cboDHead.Margin = New System.Windows.Forms.Padding(4)
Me.cboDHead.Name = "cboDHead"
Me.cboDHead.Size = New System.Drawing.Size(536, 28)
Me.cboDHead.TabIndex = 5
'
'btnOk
'
Me.btnOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK
Me.btnOk.Location = New System.Drawing.Point(342, 130)
Me.btnOk.Margin = New System.Windows.Forms.Padding(4)
Me.btnOk.Name = "btnOk"
Me.btnOk.Size = New System.Drawing.Size(94, 32)
Me.btnOk.TabIndex = 6
Me.btnOk.Text = "ＯＫ"
Me.btnOk.UseVisualStyleBackColor = True
'
'btnCancel
'
Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
Me.btnCancel.Location = New System.Drawing.Point(459, 130)
Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
Me.btnCancel.Name = "btnCancel"
Me.btnCancel.Size = New System.Drawing.Size(94, 30)
Me.btnCancel.TabIndex = 7
Me.btnCancel.Text = "戻る"
Me.btnCancel.UseVisualStyleBackColor = True
'
'TestDataSelectDlg
'
Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 20.0!)
Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
Me.ClientSize = New System.Drawing.Size(568, 182)
Me.Controls.Add(Me.btnCancel)
Me.Controls.Add(Me.btnOk)
Me.Controls.Add(Me.cboDHead)
Me.Controls.Add(Me.Label1)
Me.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
Me.Margin = New System.Windows.Forms.Padding(4)
Me.Name = "TestDataSelectDlg"
Me.Text = "データファイルの選択"
Me.ResumeLayout(False)
Me.PerformLayout()

End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDHead As System.Windows.Forms.ComboBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class
