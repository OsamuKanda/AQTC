<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDBG
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
        Me.TrackBar1 = New System.Windows.Forms.TrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TrackBar2 = New System.Windows.Forms.TrackBar()
        Me.TrackBar3 = New System.Windows.Forms.TrackBar()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TrackBar4 = New System.Windows.Forms.TrackBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TrackBar5 = New System.Windows.Forms.TrackBar()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TrackBar6 = New System.Windows.Forms.TrackBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TrackBar7 = New System.Windows.Forms.TrackBar()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TrackBar7, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TrackBar1
        '
        Me.TrackBar1.Location = New System.Drawing.Point(121, 9)
        Me.TrackBar1.Maximum = 65535
        Me.TrackBar1.Minimum = 32768
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(400, 45)
        Me.TrackBar1.TabIndex = 0
        Me.TrackBar1.Value = 47350
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "バラトロン (裏面圧)"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 12)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "ピラニ (チャンバ)"
        '
        'TrackBar2
        '
        Me.TrackBar2.Location = New System.Drawing.Point(121, 45)
        Me.TrackBar2.Maximum = 55800
        Me.TrackBar2.Minimum = 35000
        Me.TrackBar2.Name = "TrackBar2"
        Me.TrackBar2.Size = New System.Drawing.Size(400, 45)
        Me.TrackBar2.TabIndex = 4
        Me.TrackBar2.Value = 55800
        '
        'TrackBar3
        '
        Me.TrackBar3.Location = New System.Drawing.Point(121, 84)
        Me.TrackBar3.Maximum = 50000
        Me.TrackBar3.Minimum = 32768
        Me.TrackBar3.Name = "TrackBar3"
        Me.TrackBar3.Size = New System.Drawing.Size(400, 45)
        Me.TrackBar3.TabIndex = 6
        Me.TrackBar3.Value = 32768
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 84)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(85, 12)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "MFC1流量モニタ"
        '
        'TrackBar4
        '
        Me.TrackBar4.Location = New System.Drawing.Point(121, 123)
        Me.TrackBar4.Maximum = 65535
        Me.TrackBar4.Name = "TrackBar4"
        Me.TrackBar4.Size = New System.Drawing.Size(400, 45)
        Me.TrackBar4.TabIndex = 8
        Me.TrackBar4.Value = 32768
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 123)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 12)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "SDC CH1"
        '
        'TrackBar5
        '
        Me.TrackBar5.Location = New System.Drawing.Point(121, 166)
        Me.TrackBar5.Maximum = 65535
        Me.TrackBar5.Name = "TrackBar5"
        Me.TrackBar5.Size = New System.Drawing.Size(400, 45)
        Me.TrackBar5.TabIndex = 10
        Me.TrackBar5.Value = 32768
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 166)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 12)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "SDC CH2"
        '
        'TrackBar6
        '
        Me.TrackBar6.Location = New System.Drawing.Point(121, 209)
        Me.TrackBar6.Maximum = 65535
        Me.TrackBar6.Name = "TrackBar6"
        Me.TrackBar6.Size = New System.Drawing.Size(400, 45)
        Me.TrackBar6.TabIndex = 12
        Me.TrackBar6.Value = 40959
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 209)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(58, 12)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "チラー CH1"
        '
        'TrackBar7
        '
        Me.TrackBar7.Location = New System.Drawing.Point(121, 254)
        Me.TrackBar7.Maximum = 65535
        Me.TrackBar7.Name = "TrackBar7"
        Me.TrackBar7.Size = New System.Drawing.Size(400, 45)
        Me.TrackBar7.TabIndex = 14
        Me.TrackBar7.Value = 40959
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 254)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(58, 12)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "チラー CH2"
        '
        'FrmDBG
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(540, 303)
        Me.Controls.Add(Me.TrackBar7)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TrackBar6)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TrackBar5)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TrackBar4)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TrackBar3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TrackBar2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TrackBar1)
        Me.Name = "FrmDBG"
        Me.Text = "FrmDBG"
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TrackBar7, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TrackBar1 As TrackBar
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TrackBar2 As TrackBar
    Friend WithEvents TrackBar3 As TrackBar
    Friend WithEvents Label3 As Label
    Friend WithEvents TrackBar4 As TrackBar
    Friend WithEvents Label4 As Label
    Friend WithEvents TrackBar5 As TrackBar
    Friend WithEvents Label5 As Label
    Friend WithEvents TrackBar6 As TrackBar
    Friend WithEvents Label6 As Label
    Friend WithEvents TrackBar7 As TrackBar
    Friend WithEvents Label7 As Label
End Class
