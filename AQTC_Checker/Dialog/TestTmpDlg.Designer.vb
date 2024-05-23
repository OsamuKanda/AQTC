<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestTmpDlg
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
        Me.btnSet = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtTmp1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtTmp2 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboTmp1 = New System.Windows.Forms.ComboBox()
        Me.cboTmp2 = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboTmpStop = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'btnSet
        '
        Me.btnSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSet.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSet.Location = New System.Drawing.Point(270, 219)
        Me.btnSet.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(79, 36)
        Me.btnSet.TabIndex = 1
        Me.btnSet.Text = "設定"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(360, 219)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(79, 36)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "戻る"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtTmp1
        '
        Me.txtTmp1.Location = New System.Drawing.Point(353, 27)
        Me.txtTmp1.Name = "txtTmp1"
        Me.txtTmp1.Size = New System.Drawing.Size(60, 23)
        Me.txtTmp1.TabIndex = 23
        Me.txtTmp1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(36, 16)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "CH1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(419, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 16)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "℃"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(419, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(23, 16)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "℃"
        '
        'txtTmp2
        '
        Me.txtTmp2.Location = New System.Drawing.Point(353, 75)
        Me.txtTmp2.Name = "txtTmp2"
        Me.txtTmp2.Size = New System.Drawing.Size(60, 23)
        Me.txtTmp2.TabIndex = 26
        Me.txtTmp2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(21, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(39, 16)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "CH２"
        '
        'cboTmp1
        '
        Me.cboTmp1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTmp1.FormattingEnabled = True
        Me.cboTmp1.Location = New System.Drawing.Point(78, 26)
        Me.cboTmp1.Name = "cboTmp1"
        Me.cboTmp1.Size = New System.Drawing.Size(121, 24)
        Me.cboTmp1.TabIndex = 28
        '
        'cboTmp2
        '
        Me.cboTmp2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTmp2.FormattingEnabled = True
        Me.cboTmp2.Location = New System.Drawing.Point(78, 74)
        Me.cboTmp2.Name = "cboTmp2"
        Me.cboTmp2.Size = New System.Drawing.Size(121, 24)
        Me.cboTmp2.TabIndex = 29
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(247, 30)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 16)
        Me.Label5.TabIndex = 30
        Me.Label5.Text = "設定温度"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(247, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 16)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "設定温度"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(21, 134)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(295, 16)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "配管接続変更のための検査中断有無を設定"
        '
        'cboTmpStop
        '
        Me.cboTmpStop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTmpStop.FormattingEnabled = True
        Me.cboTmpStop.Location = New System.Drawing.Point(25, 167)
        Me.cboTmpStop.Name = "cboTmpStop"
        Me.cboTmpStop.Size = New System.Drawing.Size(188, 24)
        Me.cboTmpStop.TabIndex = 33
        '
        'TestTmpDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(466, 270)
        Me.Controls.Add(Me.cboTmpStop)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboTmp2)
        Me.Controls.Add(Me.cboTmp1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtTmp2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTmp1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSet)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "TestTmpDlg"
        Me.Text = "サーモチラー温度設定"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSet As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtTmp1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtTmp2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboTmp1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboTmp2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboTmpStop As System.Windows.Forms.ComboBox
End Class
