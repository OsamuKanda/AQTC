<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestItemDlg
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
        Me.grpZetsuen = New System.Windows.Forms.GroupBox()
        Me.btnIsoDel = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIsoV = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grpKyucyaku = New System.Windows.Forms.GroupBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.btnKyuDel = New System.Windows.Forms.Button()
        Me.TxtKyuHe = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtKyuBase = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtKyuV2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtKyuV1 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grpHeGas = New System.Windows.Forms.GroupBox()
        Me.ChkPa6k = New System.Windows.Forms.CheckBox()
        Me.ChkPa4k = New System.Windows.Forms.CheckBox()
        Me.ChkPa3k = New System.Windows.Forms.CheckBox()
        Me.ChkPa2k = New System.Windows.Forms.CheckBox()
        Me.ChkPa1k = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtLekBase2 = New System.Windows.Forms.TextBox()
        Me.btnLekDel = New System.Windows.Forms.Button()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtLekBase = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtLekV2 = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtLekV1 = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSet = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboVinPos = New System.Windows.Forms.ComboBox()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.grpZanKyucyaku = New System.Windows.Forms.GroupBox()
        Me.btnZKyuDel = New System.Windows.Forms.Button()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtZKyuBase = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtZKyuV2 = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtZKyuV1 = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.grpZetsuen.SuspendLayout()
        Me.grpKyucyaku.SuspendLayout()
        Me.grpHeGas.SuspendLayout()
        Me.grpZanKyucyaku.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpZetsuen
        '
        Me.grpZetsuen.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpZetsuen.Controls.Add(Me.btnIsoDel)
        Me.grpZetsuen.Controls.Add(Me.Label1)
        Me.grpZetsuen.Controls.Add(Me.txtIsoV)
        Me.grpZetsuen.Controls.Add(Me.Label3)
        Me.grpZetsuen.Location = New System.Drawing.Point(30, 50)
        Me.grpZetsuen.Name = "grpZetsuen"
        Me.grpZetsuen.Size = New System.Drawing.Size(209, 357)
        Me.grpZetsuen.TabIndex = 1
        Me.grpZetsuen.TabStop = False
        Me.grpZetsuen.Text = "絶縁耐圧測定"
        '
        'btnIsoDel
        '
        Me.btnIsoDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnIsoDel.Location = New System.Drawing.Point(90, 327)
        Me.btnIsoDel.Name = "btnIsoDel"
        Me.btnIsoDel.Size = New System.Drawing.Size(57, 26)
        Me.btnIsoDel.TabIndex = 1
        Me.btnIsoDel.Text = "削除"
        Me.btnIsoDel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(178, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(17, 16)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "V"
        '
        'txtIsoV
        '
        Me.txtIsoV.Location = New System.Drawing.Point(115, 37)
        Me.txtIsoV.Name = "txtIsoV"
        Me.txtIsoV.Size = New System.Drawing.Size(60, 23)
        Me.txtIsoV.TabIndex = 0
        Me.txtIsoV.Text = "-1000"
        Me.txtIsoV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 40)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 16)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "印加電圧"
        '
        'grpKyucyaku
        '
        Me.grpKyucyaku.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpKyucyaku.Controls.Add(Me.Label24)
        Me.grpKyucyaku.Controls.Add(Me.btnKyuDel)
        Me.grpKyucyaku.Controls.Add(Me.TxtKyuHe)
        Me.grpKyucyaku.Controls.Add(Me.Label12)
        Me.grpKyucyaku.Controls.Add(Me.Label25)
        Me.grpKyucyaku.Controls.Add(Me.txtKyuBase)
        Me.grpKyucyaku.Controls.Add(Me.Label13)
        Me.grpKyucyaku.Controls.Add(Me.Label5)
        Me.grpKyucyaku.Controls.Add(Me.txtKyuV2)
        Me.grpKyucyaku.Controls.Add(Me.Label6)
        Me.grpKyucyaku.Controls.Add(Me.Label2)
        Me.grpKyucyaku.Controls.Add(Me.txtKyuV1)
        Me.grpKyucyaku.Controls.Add(Me.Label4)
        Me.grpKyucyaku.Location = New System.Drawing.Point(250, 50)
        Me.grpKyucyaku.Name = "grpKyucyaku"
        Me.grpKyucyaku.Size = New System.Drawing.Size(262, 357)
        Me.grpKyucyaku.TabIndex = 2
        Me.grpKyucyaku.TabStop = False
        Me.grpKyucyaku.Text = "吸着力測定"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(182, 120)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(42, 16)
        Me.Label24.TabIndex = 39
        Me.Label24.Text = "sccm"
        '
        'btnKyuDel
        '
        Me.btnKyuDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnKyuDel.Location = New System.Drawing.Point(119, 327)
        Me.btnKyuDel.Name = "btnKyuDel"
        Me.btnKyuDel.Size = New System.Drawing.Size(57, 26)
        Me.btnKyuDel.TabIndex = 4
        Me.btnKyuDel.Text = "削除"
        Me.btnKyuDel.UseVisualStyleBackColor = True
        '
        'TxtKyuHe
        '
        Me.TxtKyuHe.Location = New System.Drawing.Point(119, 117)
        Me.TxtKyuHe.Name = "TxtKyuHe"
        Me.TxtKyuHe.Size = New System.Drawing.Size(60, 23)
        Me.TxtKyuHe.TabIndex = 0
        Me.TxtKyuHe.Text = "-1000"
        Me.TxtKyuHe.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(178, 160)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 16)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "秒以下"
        Me.Label12.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(10, 120)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(57, 16)
        Me.Label25.TabIndex = 37
        Me.Label25.Text = "He流量"
        '
        'txtKyuBase
        '
        Me.txtKyuBase.Location = New System.Drawing.Point(119, 157)
        Me.txtKyuBase.Name = "txtKyuBase"
        Me.txtKyuBase.Size = New System.Drawing.Size(57, 23)
        Me.txtKyuBase.TabIndex = 2
        Me.txtKyuBase.Text = "-1000"
        Me.txtKyuBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtKyuBase.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 160)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(55, 16)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "判定値"
        Me.Label13.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(182, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(17, 16)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "V"
        '
        'txtKyuV2
        '
        Me.txtKyuV2.Location = New System.Drawing.Point(119, 77)
        Me.txtKyuV2.Name = "txtKyuV2"
        Me.txtKyuV2.Size = New System.Drawing.Size(60, 23)
        Me.txtKyuV2.TabIndex = 1
        Me.txtKyuV2.Text = "-1000"
        Me.txtKyuV2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 80)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 16)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "印加電圧２"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(182, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(17, 16)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "V"
        '
        'txtKyuV1
        '
        Me.txtKyuV1.Location = New System.Drawing.Point(119, 37)
        Me.txtKyuV1.Name = "txtKyuV1"
        Me.txtKyuV1.Size = New System.Drawing.Size(60, 23)
        Me.txtKyuV1.TabIndex = 0
        Me.txtKyuV1.Text = "-1000"
        Me.txtKyuV1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 16)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "印加電圧１"
        '
        'grpHeGas
        '
        Me.grpHeGas.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpHeGas.Controls.Add(Me.ChkPa6k)
        Me.grpHeGas.Controls.Add(Me.ChkPa4k)
        Me.grpHeGas.Controls.Add(Me.ChkPa3k)
        Me.grpHeGas.Controls.Add(Me.ChkPa2k)
        Me.grpHeGas.Controls.Add(Me.ChkPa1k)
        Me.grpHeGas.Controls.Add(Me.Label22)
        Me.grpHeGas.Controls.Add(Me.txtLekBase2)
        Me.grpHeGas.Controls.Add(Me.btnLekDel)
        Me.grpHeGas.Controls.Add(Me.Label27)
        Me.grpHeGas.Controls.Add(Me.Label14)
        Me.grpHeGas.Controls.Add(Me.txtLekBase)
        Me.grpHeGas.Controls.Add(Me.Label23)
        Me.grpHeGas.Controls.Add(Me.Label15)
        Me.grpHeGas.Controls.Add(Me.Label10)
        Me.grpHeGas.Controls.Add(Me.txtLekV2)
        Me.grpHeGas.Controls.Add(Me.Label11)
        Me.grpHeGas.Controls.Add(Me.Label8)
        Me.grpHeGas.Controls.Add(Me.txtLekV1)
        Me.grpHeGas.Controls.Add(Me.Label9)
        Me.grpHeGas.Location = New System.Drawing.Point(525, 50)
        Me.grpHeGas.Name = "grpHeGas"
        Me.grpHeGas.Size = New System.Drawing.Size(290, 357)
        Me.grpHeGas.TabIndex = 3
        Me.grpHeGas.TabStop = False
        Me.grpHeGas.Text = "Ｈｅリーク量測定"
        '
        'ChkPa6k
        '
        Me.ChkPa6k.AutoSize = True
        Me.ChkPa6k.Checked = True
        Me.ChkPa6k.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPa6k.Location = New System.Drawing.Point(119, 220)
        Me.ChkPa6k.Name = "ChkPa6k"
        Me.ChkPa6k.Size = New System.Drawing.Size(63, 20)
        Me.ChkPa6k.TabIndex = 45
        Me.ChkPa6k.Text = "6.0Pa"
        Me.ChkPa6k.UseVisualStyleBackColor = True
        '
        'ChkPa4k
        '
        Me.ChkPa4k.AutoSize = True
        Me.ChkPa4k.Checked = True
        Me.ChkPa4k.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPa4k.Location = New System.Drawing.Point(119, 194)
        Me.ChkPa4k.Name = "ChkPa4k"
        Me.ChkPa4k.Size = New System.Drawing.Size(63, 20)
        Me.ChkPa4k.TabIndex = 45
        Me.ChkPa4k.Text = "4.0Pa"
        Me.ChkPa4k.UseVisualStyleBackColor = True
        '
        'ChkPa3k
        '
        Me.ChkPa3k.AutoSize = True
        Me.ChkPa3k.Checked = True
        Me.ChkPa3k.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPa3k.Location = New System.Drawing.Point(119, 168)
        Me.ChkPa3k.Name = "ChkPa3k"
        Me.ChkPa3k.Size = New System.Drawing.Size(63, 20)
        Me.ChkPa3k.TabIndex = 45
        Me.ChkPa3k.Text = "3.0Pa"
        Me.ChkPa3k.UseVisualStyleBackColor = True
        '
        'ChkPa2k
        '
        Me.ChkPa2k.AutoSize = True
        Me.ChkPa2k.Checked = True
        Me.ChkPa2k.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPa2k.Location = New System.Drawing.Point(119, 142)
        Me.ChkPa2k.Name = "ChkPa2k"
        Me.ChkPa2k.Size = New System.Drawing.Size(63, 20)
        Me.ChkPa2k.TabIndex = 45
        Me.ChkPa2k.Text = "2.0Pa"
        Me.ChkPa2k.UseVisualStyleBackColor = True
        '
        'ChkPa1k
        '
        Me.ChkPa1k.AutoSize = True
        Me.ChkPa1k.Checked = True
        Me.ChkPa1k.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ChkPa1k.Location = New System.Drawing.Point(119, 115)
        Me.ChkPa1k.Name = "ChkPa1k"
        Me.ChkPa1k.Size = New System.Drawing.Size(63, 20)
        Me.ChkPa1k.TabIndex = 45
        Me.ChkPa1k.Text = "1.0Pa"
        Me.ChkPa1k.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(187, 256)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(74, 16)
        Me.Label22.TabIndex = 44
        Me.Label22.Text = "sccm以下"
        Me.Label22.Visible = False
        '
        'txtLekBase2
        '
        Me.txtLekBase2.Location = New System.Drawing.Point(123, 292)
        Me.txtLekBase2.Name = "txtLekBase2"
        Me.txtLekBase2.Size = New System.Drawing.Size(57, 23)
        Me.txtLekBase2.TabIndex = 0
        Me.txtLekBase2.Text = "-1000"
        Me.txtLekBase2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtLekBase2.Visible = False
        '
        'btnLekDel
        '
        Me.btnLekDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnLekDel.Location = New System.Drawing.Point(119, 327)
        Me.btnLekDel.Name = "btnLekDel"
        Me.btnLekDel.Size = New System.Drawing.Size(57, 26)
        Me.btnLekDel.TabIndex = 4
        Me.btnLekDel.Text = "削除"
        Me.btnLekDel.UseVisualStyleBackColor = True
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(14, 255)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(98, 16)
        Me.Label27.TabIndex = 43
        Me.Label27.Text = "判定値(1kPa)"
        Me.Label27.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(187, 295)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(74, 16)
        Me.Label14.TabIndex = 39
        Me.Label14.Text = "sccm以下"
        Me.Label14.Visible = False
        '
        'txtLekBase
        '
        Me.txtLekBase.Location = New System.Drawing.Point(123, 252)
        Me.txtLekBase.Name = "txtLekBase"
        Me.txtLekBase.Size = New System.Drawing.Size(57, 23)
        Me.txtLekBase.TabIndex = 2
        Me.txtLekBase.Text = "-1000"
        Me.txtLekBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtLekBase.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(8, 116)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(113, 16)
        Me.Label23.TabIndex = 37
        Me.Label23.Text = "測定圧(50℃時)"
        Me.Label23.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(14, 295)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(98, 16)
        Me.Label15.TabIndex = 37
        Me.Label15.Text = "判定値(2kPa)"
        Me.Label15.Visible = False
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(187, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(17, 16)
        Me.Label10.TabIndex = 33
        Me.Label10.Text = "V"
        '
        'txtLekV2
        '
        Me.txtLekV2.Location = New System.Drawing.Point(119, 77)
        Me.txtLekV2.Name = "txtLekV2"
        Me.txtLekV2.Size = New System.Drawing.Size(60, 23)
        Me.txtLekV2.TabIndex = 1
        Me.txtLekV2.Text = "-1000"
        Me.txtLekV2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(10, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(82, 16)
        Me.Label11.TabIndex = 31
        Me.Label11.Text = "印加電圧２"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(187, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(17, 16)
        Me.Label8.TabIndex = 30
        Me.Label8.Text = "V"
        '
        'txtLekV1
        '
        Me.txtLekV1.Location = New System.Drawing.Point(119, 37)
        Me.txtLekV1.Name = "txtLekV1"
        Me.txtLekV1.Size = New System.Drawing.Size(60, 23)
        Me.txtLekV1.TabIndex = 0
        Me.txtLekV1.Text = "-1000"
        Me.txtLekV1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(10, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(82, 16)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "印加電圧１"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(1019, 415)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(79, 36)
        Me.btnCancel.TabIndex = 6
        Me.btnCancel.Text = "戻る"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSet
        '
        Me.btnSet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSet.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnSet.Location = New System.Drawing.Point(929, 415)
        Me.btnSet.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSet.Name = "btnSet"
        Me.btnSet.Size = New System.Drawing.Size(79, 36)
        Me.btnSet.TabIndex = 5
        Me.btnSet.Text = "変更"
        Me.btnSet.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnAdd.Location = New System.Drawing.Point(745, 415)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(79, 36)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "追加"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(34, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(103, 16)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "電圧印加箇所"
        '
        'cboVinPos
        '
        Me.cboVinPos.FormattingEnabled = True
        Me.cboVinPos.Location = New System.Drawing.Point(170, 15)
        Me.cboVinPos.Margin = New System.Windows.Forms.Padding(4)
        Me.cboVinPos.Name = "cboVinPos"
        Me.cboVinPos.Size = New System.Drawing.Size(341, 24)
        Me.cboVinPos.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnDelete.Location = New System.Drawing.Point(842, 415)
        Me.btnDelete.Margin = New System.Windows.Forms.Padding(4)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(79, 36)
        Me.btnDelete.TabIndex = 8
        Me.btnDelete.Text = "全削除"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'grpZanKyucyaku
        '
        Me.grpZanKyucyaku.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpZanKyucyaku.Controls.Add(Me.btnZKyuDel)
        Me.grpZanKyucyaku.Controls.Add(Me.Label16)
        Me.grpZanKyucyaku.Controls.Add(Me.txtZKyuBase)
        Me.grpZanKyucyaku.Controls.Add(Me.Label17)
        Me.grpZanKyucyaku.Controls.Add(Me.Label18)
        Me.grpZanKyucyaku.Controls.Add(Me.txtZKyuV2)
        Me.grpZanKyucyaku.Controls.Add(Me.Label19)
        Me.grpZanKyucyaku.Controls.Add(Me.Label20)
        Me.grpZanKyucyaku.Controls.Add(Me.txtZKyuV1)
        Me.grpZanKyucyaku.Controls.Add(Me.Label21)
        Me.grpZanKyucyaku.Location = New System.Drawing.Point(830, 51)
        Me.grpZanKyucyaku.Name = "grpZanKyucyaku"
        Me.grpZanKyucyaku.Size = New System.Drawing.Size(262, 357)
        Me.grpZanKyucyaku.TabIndex = 4
        Me.grpZanKyucyaku.TabStop = False
        Me.grpZanKyucyaku.Text = "残留吸着力測定"
        '
        'btnZKyuDel
        '
        Me.btnZKyuDel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnZKyuDel.Location = New System.Drawing.Point(119, 327)
        Me.btnZKyuDel.Name = "btnZKyuDel"
        Me.btnZKyuDel.Size = New System.Drawing.Size(57, 26)
        Me.btnZKyuDel.TabIndex = 3
        Me.btnZKyuDel.Text = "削除"
        Me.btnZKyuDel.UseVisualStyleBackColor = True
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(178, 120)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(57, 16)
        Me.Label16.TabIndex = 36
        Me.Label16.Text = "Pa以下"
        Me.Label16.Visible = False
        '
        'txtZKyuBase
        '
        Me.txtZKyuBase.Location = New System.Drawing.Point(119, 117)
        Me.txtZKyuBase.Name = "txtZKyuBase"
        Me.txtZKyuBase.Size = New System.Drawing.Size(57, 23)
        Me.txtZKyuBase.TabIndex = 2
        Me.txtZKyuBase.Text = "-1000"
        Me.txtZKyuBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtZKyuBase.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(10, 120)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(55, 16)
        Me.Label17.TabIndex = 34
        Me.Label17.Text = "判定値"
        Me.Label17.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(182, 80)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(17, 16)
        Me.Label18.TabIndex = 33
        Me.Label18.Text = "V"
        '
        'txtZKyuV2
        '
        Me.txtZKyuV2.Location = New System.Drawing.Point(119, 77)
        Me.txtZKyuV2.Name = "txtZKyuV2"
        Me.txtZKyuV2.Size = New System.Drawing.Size(60, 23)
        Me.txtZKyuV2.TabIndex = 1
        Me.txtZKyuV2.Text = "-1000"
        Me.txtZKyuV2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 80)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(82, 16)
        Me.Label19.TabIndex = 31
        Me.Label19.Text = "印加電圧２"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(182, 40)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(17, 16)
        Me.Label20.TabIndex = 30
        Me.Label20.Text = "V"
        '
        'txtZKyuV1
        '
        Me.txtZKyuV1.Location = New System.Drawing.Point(119, 37)
        Me.txtZKyuV1.Name = "txtZKyuV1"
        Me.txtZKyuV1.Size = New System.Drawing.Size(60, 23)
        Me.txtZKyuV1.TabIndex = 0
        Me.txtZKyuV1.Text = "-1000"
        Me.txtZKyuV1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(10, 40)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(82, 16)
        Me.Label21.TabIndex = 28
        Me.Label21.Text = "印加電圧１"
        '
        'TestItemDlg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1124, 461)
        Me.Controls.Add(Me.grpZanKyucyaku)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.cboVinPos)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSet)
        Me.Controls.Add(Me.grpHeGas)
        Me.Controls.Add(Me.grpKyucyaku)
        Me.Controls.Add(Me.grpZetsuen)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "TestItemDlg"
        Me.Text = "測定項目"
        Me.grpZetsuen.ResumeLayout(False)
        Me.grpZetsuen.PerformLayout()
        Me.grpKyucyaku.ResumeLayout(False)
        Me.grpKyucyaku.PerformLayout()
        Me.grpHeGas.ResumeLayout(False)
        Me.grpHeGas.PerformLayout()
        Me.grpZanKyucyaku.ResumeLayout(False)
        Me.grpZanKyucyaku.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpZetsuen As System.Windows.Forms.GroupBox
    Friend WithEvents grpKyucyaku As System.Windows.Forms.GroupBox
    Friend WithEvents grpHeGas As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIsoV As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSet As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboVinPos As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtKyuBase As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtKyuV2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtKyuV1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtLekBase As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtLekV2 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLekV1 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnIsoDel As System.Windows.Forms.Button
    Friend WithEvents btnKyuDel As System.Windows.Forms.Button
    Friend WithEvents btnLekDel As System.Windows.Forms.Button
	Friend WithEvents Label24 As Label
	Friend WithEvents TxtKyuHe As TextBox
	Friend WithEvents Label25 As Label
	Friend WithEvents txtLekBase2 As TextBox
	Friend WithEvents Label27 As Label
	Friend WithEvents grpZanKyucyaku As GroupBox
	Friend WithEvents btnZKyuDel As Button
	Friend WithEvents Label16 As Label
	Friend WithEvents txtZKyuBase As TextBox
	Friend WithEvents Label17 As Label
	Friend WithEvents Label18 As Label
	Friend WithEvents txtZKyuV2 As TextBox
	Friend WithEvents Label19 As Label
	Friend WithEvents Label20 As Label
	Friend WithEvents txtZKyuV1 As TextBox
	Friend WithEvents Label21 As Label
	Friend WithEvents Label22 As Label
	Friend WithEvents ChkPa1k As CheckBox
	Friend WithEvents Label23 As Label
    Friend WithEvents ChkPa2k As CheckBox
    Friend WithEvents ChkPa6k As CheckBox
    Friend WithEvents ChkPa4k As CheckBox
    Friend WithEvents ChkPa3k As CheckBox
End Class
