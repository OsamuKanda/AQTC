<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestParmDlg
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
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboEscMd = New System.Windows.Forms.ComboBox()
        Me.cboScrUse = New System.Windows.Forms.ComboBox()
        Me.txtTRPS1 = New System.Windows.Forms.TextBox()
        Me.txtTRPS2 = New System.Windows.Forms.TextBox()
        Me.txtBakPres = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtTmpStbW = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.grpZanKyu = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtBakPrs = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtAntTim = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtAntVct = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtHeWait = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtVoltImp = New System.Windows.Forms.TextBox()
        Me.txtHeFlow = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label51 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtKyuMaxTm = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grpZanKyu.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSet
        '
        Me.btnSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSet.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSet.Location = New System.Drawing.Point(264, 465)
        Me.btnSet.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(79, 36)
        Me.btnSet.TabIndex = 0
        Me.btnSet.Text = "設定"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(369, 465)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(79, 36)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "戻る"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 138)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(119, 16)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "吸着停止裏面圧"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 108)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(103, 16)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "高温時真空圧"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 78)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 16)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "低温時真空圧"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(135, 16)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "サーモチラー使用CH"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 16)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "SDC電源動作種別"
        '
        'cboEscMd
        '
        Me.cboEscMd.FormattingEnabled = True
        Me.cboEscMd.Location = New System.Drawing.Point(304, 15)
        Me.cboEscMd.Name = "cboEscMd"
        Me.cboEscMd.Size = New System.Drawing.Size(130, 24)
        Me.cboEscMd.TabIndex = 17
        '
        'cboScrUse
        '
        Me.cboScrUse.FormattingEnabled = True
        Me.cboScrUse.Location = New System.Drawing.Point(304, 45)
        Me.cboScrUse.Name = "cboScrUse"
        Me.cboScrUse.Size = New System.Drawing.Size(107, 24)
        Me.cboScrUse.TabIndex = 20
        '
        'txtTRPS1
        '
        Me.txtTRPS1.Location = New System.Drawing.Point(304, 75)
        Me.txtTRPS1.Name = "txtTRPS1"
        Me.txtTRPS1.Size = New System.Drawing.Size(88, 23)
        Me.txtTRPS1.TabIndex = 23
        Me.txtTRPS1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtTRPS2
        '
        Me.txtTRPS2.Location = New System.Drawing.Point(304, 105)
        Me.txtTRPS2.Name = "txtTRPS2"
        Me.txtTRPS2.Size = New System.Drawing.Size(88, 23)
        Me.txtTRPS2.TabIndex = 24
        Me.txtTRPS2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtBakPres
        '
        Me.txtBakPres.Location = New System.Drawing.Point(304, 135)
        Me.txtBakPres.Name = "txtBakPres"
        Me.txtBakPres.Size = New System.Drawing.Size(88, 23)
        Me.txtBakPres.TabIndex = 25
        Me.txtBakPres.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(414, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(26, 16)
        Me.Label10.TabIndex = 26
        Me.Label10.Text = "Pａ"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(414, 110)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(26, 16)
        Me.Label11.TabIndex = 27
        Me.Label11.Text = "Pａ"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(414, 140)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(26, 16)
        Me.Label12.TabIndex = 28
        Me.Label12.Text = "Pａ"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(414, 170)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(23, 16)
        Me.Label13.TabIndex = 31
        Me.Label13.Text = "分"
        '
        'txtTmpStbW
        '
        Me.txtTmpStbW.Location = New System.Drawing.Point(304, 165)
        Me.txtTmpStbW.Name = "txtTmpStbW"
        Me.txtTmpStbW.Size = New System.Drawing.Size(88, 23)
        Me.txtTmpStbW.TabIndex = 30
        Me.txtTmpStbW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(12, 168)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(197, 16)
        Me.Label14.TabIndex = 29
        Me.Label14.Text = "電極ヘッド温度安定待ち時間"
        '
        'grpZanKyu
        '
        Me.grpZanKyu.BackColor = System.Drawing.SystemColors.Control
        Me.grpZanKyu.Controls.Add(Me.Label4)
        Me.grpZanKyu.Controls.Add(Me.txtBakPrs)
        Me.grpZanKyu.Controls.Add(Me.Label5)
        Me.grpZanKyu.Controls.Add(Me.Label20)
        Me.grpZanKyu.Controls.Add(Me.txtAntTim)
        Me.grpZanKyu.Controls.Add(Me.Label22)
        Me.grpZanKyu.Controls.Add(Me.txtAntVct)
        Me.grpZanKyu.Controls.Add(Me.Label23)
        Me.grpZanKyu.Controls.Add(Me.Label17)
        Me.grpZanKyu.Controls.Add(Me.Label16)
        Me.grpZanKyu.Controls.Add(Me.Label15)
        Me.grpZanKyu.Controls.Add(Me.txtHeWait)
        Me.grpZanKyu.Controls.Add(Me.Label18)
        Me.grpZanKyu.Controls.Add(Me.txtVoltImp)
        Me.grpZanKyu.Controls.Add(Me.txtHeFlow)
        Me.grpZanKyu.Controls.Add(Me.Label19)
        Me.grpZanKyu.Controls.Add(Me.Label51)
        Me.grpZanKyu.Location = New System.Drawing.Point(8, 230)
        Me.grpZanKyu.Name = "grpZanKyu"
        Me.grpZanKyu.Size = New System.Drawing.Size(457, 209)
        Me.grpZanKyu.TabIndex = 34
        Me.grpZanKyu.TabStop = False
        Me.grpZanKyu.Text = "残留吸着試験用"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(406, 181)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(25, 16)
        Me.Label4.TabIndex = 58
        Me.Label4.Text = "Pa"
        '
        'txtBakPrs
        '
        Me.txtBakPrs.BackColor = System.Drawing.SystemColors.Window
        Me.txtBakPrs.Location = New System.Drawing.Point(296, 176)
        Me.txtBakPrs.Name = "txtBakPrs"
        Me.txtBakPrs.Size = New System.Drawing.Size(88, 23)
        Me.txtBakPrs.TabIndex = 57
        Me.txtBakPrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 179)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(215, 16)
        Me.Label5.TabIndex = 56
        Me.Label5.Text = "残留吸着電圧印可時裏面圧力"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(406, 150)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(23, 16)
        Me.Label20.TabIndex = 55
        Me.Label20.Text = "秒"
        '
        'txtAntTim
        '
        Me.txtAntTim.BackColor = System.Drawing.SystemColors.Window
        Me.txtAntTim.Location = New System.Drawing.Point(296, 145)
        Me.txtAntTim.Name = "txtAntTim"
        Me.txtAntTim.Size = New System.Drawing.Size(88, 23)
        Me.txtAntTim.TabIndex = 53
        Me.txtAntTim.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 148)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(103, 16)
        Me.Label22.TabIndex = 52
        Me.Label22.Text = "安定判断時間"
        '
        'txtAntVct
        '
        Me.txtAntVct.BackColor = System.Drawing.SystemColors.Window
        Me.txtAntVct.Location = New System.Drawing.Point(296, 115)
        Me.txtAntVct.Name = "txtAntVct"
        Me.txtAntVct.Size = New System.Drawing.Size(88, 23)
        Me.txtAntVct.TabIndex = 51
        Me.txtAntVct.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 118)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(99, 16)
        Me.Label23.TabIndex = 50
        Me.Label23.Text = "安定判断傾き"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(406, 90)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(23, 16)
        Me.Label17.TabIndex = 49
        Me.Label17.Text = "秒"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(406, 60)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(23, 16)
        Me.Label16.TabIndex = 48
        Me.Label16.Text = "秒"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(406, 30)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(42, 16)
        Me.Label15.TabIndex = 47
        Me.Label15.Text = "sccm"
        '
        'txtHeWait
        '
        Me.txtHeWait.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeWait.Location = New System.Drawing.Point(296, 85)
        Me.txtHeWait.Name = "txtHeWait"
        Me.txtHeWait.Size = New System.Drawing.Size(88, 23)
        Me.txtHeWait.TabIndex = 46
        Me.txtHeWait.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 88)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(265, 16)
        Me.Label18.TabIndex = 45
        Me.Label18.Text = "電圧印加停止後He流すまでの待ち時間"
        '
        'txtVoltImp
        '
        Me.txtVoltImp.BackColor = System.Drawing.SystemColors.Window
        Me.txtVoltImp.Location = New System.Drawing.Point(296, 55)
        Me.txtVoltImp.Name = "txtVoltImp"
        Me.txtVoltImp.Size = New System.Drawing.Size(88, 23)
        Me.txtVoltImp.TabIndex = 44
        Me.txtVoltImp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtHeFlow
        '
        Me.txtHeFlow.BackColor = System.Drawing.SystemColors.Window
        Me.txtHeFlow.Location = New System.Drawing.Point(296, 25)
        Me.txtHeFlow.Name = "txtHeFlow"
        Me.txtHeFlow.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtHeFlow.Size = New System.Drawing.Size(88, 23)
        Me.txtHeFlow.TabIndex = 43
        Me.txtHeFlow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 28)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(60, 16)
        Me.Label19.TabIndex = 42
        Me.Label19.Text = "Ｈｅ流量"
        '
        'Label51
        '
        Me.Label51.AutoSize = True
        Me.Label51.Location = New System.Drawing.Point(6, 58)
        Me.Label51.Name = "Label51"
        Me.Label51.Size = New System.Drawing.Size(103, 16)
        Me.Label51.TabIndex = 41
        Me.Label51.Text = "電圧印加時間"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(414, 200)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(23, 16)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "秒"
        '
        'txtKyuMaxTm
        '
        Me.txtKyuMaxTm.BackColor = System.Drawing.SystemColors.Window
        Me.txtKyuMaxTm.Location = New System.Drawing.Point(304, 195)
        Me.txtKyuMaxTm.Name = "txtKyuMaxTm"
        Me.txtKyuMaxTm.Size = New System.Drawing.Size(88, 23)
        Me.txtKyuMaxTm.TabIndex = 50
        Me.txtKyuMaxTm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 198)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 16)
        Me.Label2.TabIndex = 49
        Me.Label2.Text = "吸着力測定時間上限"
        '
        'TestParmDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(477, 522)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtKyuMaxTm)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grpZanKyu)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtTmpStbW)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtBakPres)
        Me.Controls.Add(Me.txtTRPS2)
        Me.Controls.Add(Me.txtTRPS1)
        Me.Controls.Add(Me.cboScrUse)
        Me.Controls.Add(Me.cboEscMd)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSet)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "TestParmDlg"
        Me.Text = "試験パラメータ"
        Me.grpZanKyu.ResumeLayout(False)
        Me.grpZanKyu.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSet As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboEscMd As System.Windows.Forms.ComboBox
    Friend WithEvents cboScrUse As System.Windows.Forms.ComboBox
    Friend WithEvents txtTRPS1 As System.Windows.Forms.TextBox
    Friend WithEvents txtTRPS2 As System.Windows.Forms.TextBox
    Friend WithEvents txtBakPres As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtTmpStbW As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents grpZanKyu As GroupBox
    Friend WithEvents Label17 As Label
    Friend WithEvents Label16 As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents txtHeWait As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents txtVoltImp As TextBox
    Friend WithEvents txtHeFlow As TextBox
    Friend WithEvents Label19 As Label
    Friend WithEvents Label51 As Label
	Friend WithEvents Label20 As Label
	Friend WithEvents txtAntTim As TextBox
	Friend WithEvents Label22 As Label
	Friend WithEvents txtAntVct As TextBox
	Friend WithEvents Label23 As Label
	Friend WithEvents Label1 As Label
	Friend WithEvents txtKyuMaxTm As TextBox
	Friend WithEvents Label2 As Label
	Friend WithEvents Label4 As Label
	Friend WithEvents txtBakPrs As TextBox
	Friend WithEvents Label5 As Label
End Class
