<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmWaitCdt
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmWaitCdt))
		Me.lblChl1 = New System.Windows.Forms.Label()
		Me.lblTitle = New System.Windows.Forms.Label()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.lblAimChl1 = New System.Windows.Forms.Label()
		Me.Label6 = New System.Windows.Forms.Label()
		Me.lblNowChl1 = New System.Windows.Forms.Label()
		Me.Label8 = New System.Windows.Forms.Label()
		Me.lblStsChl1 = New System.Windows.Forms.Label()
		Me.Label10 = New System.Windows.Forms.Label()
		Me.lblCntSyn = New System.Windows.Forms.Label()
		Me.Label12 = New System.Windows.Forms.Label()
		Me.lblCntChl1 = New System.Windows.Forms.Label()
		Me.lblCntChl2 = New System.Windows.Forms.Label()
		Me.lblStsChl2 = New System.Windows.Forms.Label()
		Me.lblNowChl2 = New System.Windows.Forms.Label()
		Me.lblAimChl2 = New System.Windows.Forms.Label()
		Me.lblChl2 = New System.Windows.Forms.Label()
		Me.lblCntHed = New System.Windows.Forms.Label()
		Me.lblStsHed = New System.Windows.Forms.Label()
		Me.lblNowHed = New System.Windows.Forms.Label()
		Me.lblAimHed = New System.Windows.Forms.Label()
		Me.lblHed = New System.Windows.Forms.Label()
		Me.lblCntCmb = New System.Windows.Forms.Label()
		Me.lblStsCmb = New System.Windows.Forms.Label()
		Me.lblNowCmb = New System.Windows.Forms.Label()
		Me.lblAimCmb = New System.Windows.Forms.Label()
		Me.lblCmb = New System.Windows.Forms.Label()
		Me.lblCntWbp = New System.Windows.Forms.Label()
		Me.lblStsWbp = New System.Windows.Forms.Label()
		Me.lblNowWbp = New System.Windows.Forms.Label()
		Me.lblAimWbp = New System.Windows.Forms.Label()
		Me.lblWbp = New System.Windows.Forms.Label()
		Me.BtnStop = New System.Windows.Forms.Button()
		Me.lblChl1Unit1 = New System.Windows.Forms.Label()
		Me.lblCmbUnit1 = New System.Windows.Forms.Label()
		Me.lblChl2Unit1 = New System.Windows.Forms.Label()
		Me.lblChl1Unit2 = New System.Windows.Forms.Label()
		Me.lblChl2Unit2 = New System.Windows.Forms.Label()
		Me.lblHedUnit1 = New System.Windows.Forms.Label()
		Me.lblHedUnit2 = New System.Windows.Forms.Label()
		Me.lblCmbUnit2 = New System.Windows.Forms.Label()
		Me.lblWbpUnit1 = New System.Windows.Forms.Label()
		Me.lblWbpUnit2 = New System.Windows.Forms.Label()
		Me.TimItv = New System.Windows.Forms.Timer(Me.components)
		Me.Label1 = New System.Windows.Forms.Label()
		Me.lblCntAllOK = New System.Windows.Forms.Label()
		Me.lblStsAllOK = New System.Windows.Forms.Label()
		Me.lblAimAllOK = New System.Windows.Forms.Label()
		Me.lblNowAllOK = New System.Windows.Forms.Label()
		Me.lblAllOKUnit2 = New System.Windows.Forms.Label()
		Me.lblAllOKUnit1 = New System.Windows.Forms.Label()
		Me.BtnSkip = New System.Windows.Forms.Button()
		Me.SuspendLayout
		'
		'lblChl1
		'
		Me.lblChl1.AutoSize = true
		Me.lblChl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblChl1.ForeColor = System.Drawing.Color.Gray
		Me.lblChl1.Location = New System.Drawing.Point(41, 126)
		Me.lblChl1.Name = "lblChl1"
		Me.lblChl1.Size = New System.Drawing.Size(276, 26)
		Me.lblChl1.TabIndex = 0
		Me.lblChl1.Text = "サーモチラーＣＨ１温度"
		'
		'lblTitle
		'
		Me.lblTitle.BackColor = System.Drawing.SystemColors.ActiveCaptionText
		Me.lblTitle.Font = New System.Drawing.Font("ＭＳ ゴシック", 19.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255,Byte),Integer), CType(CType(128,Byte),Integer), CType(CType(0,Byte),Integer))
		Me.lblTitle.Location = New System.Drawing.Point(43, 14)
		Me.lblTitle.Name = "lblTitle"
		Me.lblTitle.Size = New System.Drawing.Size(458, 47)
		Me.lblTitle.TabIndex = 1
		Me.lblTitle.Text = "条件成立待ち"
		Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Label3
		'
		Me.Label3.AutoSize = true
		Me.Label3.ForeColor = System.Drawing.Color.Gray
		Me.Label3.Location = New System.Drawing.Point(406, 77)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(58, 24)
		Me.Label3.TabIndex = 2
		Me.Label3.Text = "目標"
		'
		'Label4
		'
		Me.Label4.AutoSize = true
		Me.Label4.ForeColor = System.Drawing.Color.Gray
		Me.Label4.Location = New System.Drawing.Point(36, 77)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(58, 24)
		Me.Label4.TabIndex = 3
		Me.Label4.Text = "項目"
		'
		'lblAimChl1
		'
		Me.lblAimChl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAimChl1.ForeColor = System.Drawing.SystemColors.ControlText
		Me.lblAimChl1.Location = New System.Drawing.Point(411, 126)
		Me.lblAimChl1.Name = "lblAimChl1"
		Me.lblAimChl1.Size = New System.Drawing.Size(90, 32)
		Me.lblAimChl1.TabIndex = 4
		Me.lblAimChl1.Text = "-100"
		Me.lblAimChl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label6
		'
		Me.Label6.AutoSize = true
		Me.Label6.ForeColor = System.Drawing.Color.Gray
		Me.Label6.Location = New System.Drawing.Point(581, 77)
		Me.Label6.Name = "Label6"
		Me.Label6.Size = New System.Drawing.Size(58, 24)
		Me.Label6.TabIndex = 5
		Me.Label6.Text = "現在"
		'
		'lblNowChl1
		'
		Me.lblNowChl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblNowChl1.Location = New System.Drawing.Point(577, 123)
		Me.lblNowChl1.Name = "lblNowChl1"
		Me.lblNowChl1.Size = New System.Drawing.Size(90, 32)
		Me.lblNowChl1.TabIndex = 6
		Me.lblNowChl1.Text = "-100"
		Me.lblNowChl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'Label8
		'
		Me.Label8.AutoSize = true
		Me.Label8.ForeColor = System.Drawing.Color.Gray
		Me.Label8.Location = New System.Drawing.Point(838, 77)
		Me.Label8.Name = "Label8"
		Me.Label8.Size = New System.Drawing.Size(58, 24)
		Me.Label8.TabIndex = 7
		Me.Label8.Text = "判定"
		'
		'lblStsChl1
		'
		Me.lblStsChl1.BackColor = System.Drawing.SystemColors.Control
		Me.lblStsChl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblStsChl1.Location = New System.Drawing.Point(843, 126)
		Me.lblStsChl1.Name = "lblStsChl1"
		Me.lblStsChl1.Size = New System.Drawing.Size(135, 32)
		Me.lblStsChl1.TabIndex = 8
		Me.lblStsChl1.Text = "成立待ち"
		Me.lblStsChl1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'Label10
		'
		Me.Label10.AutoSize = true
		Me.Label10.ForeColor = System.Drawing.Color.Gray
		Me.Label10.Location = New System.Drawing.Point(653, 23)
		Me.Label10.Name = "Label10"
		Me.Label10.Size = New System.Drawing.Size(154, 24)
		Me.Label10.TabIndex = 9
		Me.Label10.Text = "待ち時間残り"
		'
		'lblCntSyn
		'
		Me.lblCntSyn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCntSyn.Location = New System.Drawing.Point(852, 21)
		Me.lblCntSyn.Name = "lblCntSyn"
		Me.lblCntSyn.Size = New System.Drawing.Size(135, 32)
		Me.lblCntSyn.TabIndex = 10
		Me.lblCntSyn.Text = "99:99:99"
		Me.lblCntSyn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
		'
		'Label12
		'
		Me.Label12.AutoSize = true
		Me.Label12.ForeColor = System.Drawing.Color.Gray
		Me.Label12.Location = New System.Drawing.Point(735, 77)
		Me.Label12.Name = "Label12"
		Me.Label12.Size = New System.Drawing.Size(58, 24)
		Me.Label12.TabIndex = 11
		Me.Label12.Text = "ｶｳﾝﾄ"
		'
		'lblCntChl1
		'
		Me.lblCntChl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCntChl1.Location = New System.Drawing.Point(740, 126)
		Me.lblCntChl1.Name = "lblCntChl1"
		Me.lblCntChl1.Size = New System.Drawing.Size(90, 32)
		Me.lblCntChl1.TabIndex = 12
		Me.lblCntChl1.Text = "99999"
		Me.lblCntChl1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCntChl2
		'
		Me.lblCntChl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCntChl2.Location = New System.Drawing.Point(740, 168)
		Me.lblCntChl2.Name = "lblCntChl2"
		Me.lblCntChl2.Size = New System.Drawing.Size(90, 32)
		Me.lblCntChl2.TabIndex = 17
		Me.lblCntChl2.Text = "99999"
		Me.lblCntChl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblStsChl2
		'
		Me.lblStsChl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblStsChl2.Location = New System.Drawing.Point(843, 168)
		Me.lblStsChl2.Name = "lblStsChl2"
		Me.lblStsChl2.Size = New System.Drawing.Size(135, 32)
		Me.lblStsChl2.TabIndex = 16
		Me.lblStsChl2.Text = "成立待ち"
		Me.lblStsChl2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblNowChl2
		'
		Me.lblNowChl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblNowChl2.Location = New System.Drawing.Point(577, 168)
		Me.lblNowChl2.Name = "lblNowChl2"
		Me.lblNowChl2.Size = New System.Drawing.Size(90, 32)
		Me.lblNowChl2.TabIndex = 15
		Me.lblNowChl2.Text = "-100"
		Me.lblNowChl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAimChl2
		'
		Me.lblAimChl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAimChl2.Location = New System.Drawing.Point(411, 168)
		Me.lblAimChl2.Name = "lblAimChl2"
		Me.lblAimChl2.Size = New System.Drawing.Size(90, 32)
		Me.lblAimChl2.TabIndex = 14
		Me.lblAimChl2.Text = "-100"
		Me.lblAimChl2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblChl2
		'
		Me.lblChl2.AutoSize = true
		Me.lblChl2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblChl2.ForeColor = System.Drawing.Color.Gray
		Me.lblChl2.Location = New System.Drawing.Point(41, 168)
		Me.lblChl2.Name = "lblChl2"
		Me.lblChl2.Size = New System.Drawing.Size(276, 26)
		Me.lblChl2.TabIndex = 13
		Me.lblChl2.Text = "サーモチラーＣＨ２温度"
		'
		'lblCntHed
		'
		Me.lblCntHed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCntHed.Location = New System.Drawing.Point(740, 209)
		Me.lblCntHed.Name = "lblCntHed"
		Me.lblCntHed.Size = New System.Drawing.Size(90, 32)
		Me.lblCntHed.TabIndex = 22
		Me.lblCntHed.Text = "99999"
		Me.lblCntHed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblStsHed
		'
		Me.lblStsHed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblStsHed.Location = New System.Drawing.Point(843, 209)
		Me.lblStsHed.Name = "lblStsHed"
		Me.lblStsHed.Size = New System.Drawing.Size(135, 32)
		Me.lblStsHed.TabIndex = 21
		Me.lblStsHed.Text = "成立待ち"
		Me.lblStsHed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblNowHed
		'
		Me.lblNowHed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblNowHed.Location = New System.Drawing.Point(577, 209)
		Me.lblNowHed.Name = "lblNowHed"
		Me.lblNowHed.Size = New System.Drawing.Size(90, 32)
		Me.lblNowHed.TabIndex = 20
		Me.lblNowHed.Text = "200"
		Me.lblNowHed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAimHed
		'
		Me.lblAimHed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAimHed.Location = New System.Drawing.Point(411, 209)
		Me.lblAimHed.Name = "lblAimHed"
		Me.lblAimHed.Size = New System.Drawing.Size(90, 32)
		Me.lblAimHed.TabIndex = 19
		Me.lblAimHed.Text = "20"
		Me.lblAimHed.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblHed
		'
		Me.lblHed.AutoSize = true
		Me.lblHed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblHed.ForeColor = System.Drawing.Color.Gray
		Me.lblHed.Location = New System.Drawing.Point(41, 209)
		Me.lblHed.Name = "lblHed"
		Me.lblHed.Size = New System.Drawing.Size(228, 26)
		Me.lblHed.TabIndex = 18
		Me.lblHed.Text = "電極ヘッド温度安定"
		'
		'lblCntCmb
		'
		Me.lblCntCmb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCntCmb.Location = New System.Drawing.Point(740, 250)
		Me.lblCntCmb.Name = "lblCntCmb"
		Me.lblCntCmb.Size = New System.Drawing.Size(90, 32)
		Me.lblCntCmb.TabIndex = 27
		Me.lblCntCmb.Text = "99999"
		Me.lblCntCmb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblStsCmb
		'
		Me.lblStsCmb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblStsCmb.Location = New System.Drawing.Point(843, 250)
		Me.lblStsCmb.Name = "lblStsCmb"
		Me.lblStsCmb.Size = New System.Drawing.Size(135, 32)
		Me.lblStsCmb.TabIndex = 26
		Me.lblStsCmb.Text = "成立待ち"
		Me.lblStsCmb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblNowCmb
		'
		Me.lblNowCmb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblNowCmb.Location = New System.Drawing.Point(577, 250)
		Me.lblNowCmb.Name = "lblNowCmb"
		Me.lblNowCmb.Size = New System.Drawing.Size(90, 32)
		Me.lblNowCmb.TabIndex = 25
		Me.lblNowCmb.Text = "10000"
		Me.lblNowCmb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAimCmb
		'
		Me.lblAimCmb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAimCmb.Location = New System.Drawing.Point(411, 250)
		Me.lblAimCmb.Name = "lblAimCmb"
		Me.lblAimCmb.Size = New System.Drawing.Size(90, 32)
		Me.lblAimCmb.TabIndex = 24
		Me.lblAimCmb.Text = "1000"
		Me.lblAimCmb.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblCmb
		'
		Me.lblCmb.AutoSize = true
		Me.lblCmb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCmb.ForeColor = System.Drawing.Color.Gray
		Me.lblCmb.Location = New System.Drawing.Point(41, 250)
		Me.lblCmb.Name = "lblCmb"
		Me.lblCmb.Size = New System.Drawing.Size(180, 26)
		Me.lblCmb.TabIndex = 23
		Me.lblCmb.Text = "チャンバ内圧力"
		'
		'lblCntWbp
		'
		Me.lblCntWbp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCntWbp.Location = New System.Drawing.Point(740, 293)
		Me.lblCntWbp.Name = "lblCntWbp"
		Me.lblCntWbp.Size = New System.Drawing.Size(90, 32)
		Me.lblCntWbp.TabIndex = 32
		Me.lblCntWbp.Text = "99999"
		Me.lblCntWbp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblStsWbp
		'
		Me.lblStsWbp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblStsWbp.Location = New System.Drawing.Point(843, 293)
		Me.lblStsWbp.Name = "lblStsWbp"
		Me.lblStsWbp.Size = New System.Drawing.Size(135, 32)
		Me.lblStsWbp.TabIndex = 31
		Me.lblStsWbp.Text = "成立待ち"
		Me.lblStsWbp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblNowWbp
		'
		Me.lblNowWbp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblNowWbp.Location = New System.Drawing.Point(577, 293)
		Me.lblNowWbp.Name = "lblNowWbp"
		Me.lblNowWbp.Size = New System.Drawing.Size(90, 32)
		Me.lblNowWbp.TabIndex = 30
		Me.lblNowWbp.Text = "10000"
		Me.lblNowWbp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAimWbp
		'
		Me.lblAimWbp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAimWbp.Location = New System.Drawing.Point(411, 293)
		Me.lblAimWbp.Name = "lblAimWbp"
		Me.lblAimWbp.Size = New System.Drawing.Size(90, 32)
		Me.lblAimWbp.TabIndex = 29
		Me.lblAimWbp.Text = "1000"
		Me.lblAimWbp.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblWbp
		'
		Me.lblWbp.AutoSize = true
		Me.lblWbp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblWbp.ForeColor = System.Drawing.Color.Gray
		Me.lblWbp.Location = New System.Drawing.Point(41, 293)
		Me.lblWbp.Name = "lblWbp"
		Me.lblWbp.Size = New System.Drawing.Size(252, 26)
		Me.lblWbp.TabIndex = 28
		Me.lblWbp.Text = "配管・ウエハ裏面圧力"
		'
		'BtnStop
		'
		Me.BtnStop.Location = New System.Drawing.Point(826, 385)
		Me.BtnStop.Name = "BtnStop"
		Me.BtnStop.Size = New System.Drawing.Size(181, 49)
		Me.BtnStop.TabIndex = 33
		Me.BtnStop.Text = "試験中止"
		Me.BtnStop.UseVisualStyleBackColor = true
		'
		'lblChl1Unit1
		'
		Me.lblChl1Unit1.AutoSize = true
		Me.lblChl1Unit1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblChl1Unit1.ForeColor = System.Drawing.Color.Gray
		Me.lblChl1Unit1.Location = New System.Drawing.Point(507, 135)
		Me.lblChl1Unit1.Name = "lblChl1Unit1"
		Me.lblChl1Unit1.Size = New System.Drawing.Size(43, 16)
		Me.lblChl1Unit1.TabIndex = 34
		Me.lblChl1Unit1.Text = "[℃]"
		'
		'lblCmbUnit1
		'
		Me.lblCmbUnit1.AutoSize = true
		Me.lblCmbUnit1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblCmbUnit1.ForeColor = System.Drawing.Color.Gray
		Me.lblCmbUnit1.Location = New System.Drawing.Point(507, 259)
		Me.lblCmbUnit1.Name = "lblCmbUnit1"
		Me.lblCmbUnit1.Size = New System.Drawing.Size(44, 16)
		Me.lblCmbUnit1.TabIndex = 35
		Me.lblCmbUnit1.Text = "[Pa]"
		'
		'lblChl2Unit1
		'
		Me.lblChl2Unit1.AutoSize = true
		Me.lblChl2Unit1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblChl2Unit1.ForeColor = System.Drawing.Color.Gray
		Me.lblChl2Unit1.Location = New System.Drawing.Point(507, 177)
		Me.lblChl2Unit1.Name = "lblChl2Unit1"
		Me.lblChl2Unit1.Size = New System.Drawing.Size(43, 16)
		Me.lblChl2Unit1.TabIndex = 36
		Me.lblChl2Unit1.Text = "[℃]"
		'
		'lblChl1Unit2
		'
		Me.lblChl1Unit2.AutoSize = true
		Me.lblChl1Unit2.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblChl1Unit2.ForeColor = System.Drawing.Color.Gray
		Me.lblChl1Unit2.Location = New System.Drawing.Point(673, 135)
		Me.lblChl1Unit2.Name = "lblChl1Unit2"
		Me.lblChl1Unit2.Size = New System.Drawing.Size(43, 16)
		Me.lblChl1Unit2.TabIndex = 37
		Me.lblChl1Unit2.Text = "[℃]"
		'
		'lblChl2Unit2
		'
		Me.lblChl2Unit2.AutoSize = true
		Me.lblChl2Unit2.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblChl2Unit2.ForeColor = System.Drawing.Color.Gray
		Me.lblChl2Unit2.Location = New System.Drawing.Point(673, 177)
		Me.lblChl2Unit2.Name = "lblChl2Unit2"
		Me.lblChl2Unit2.Size = New System.Drawing.Size(43, 16)
		Me.lblChl2Unit2.TabIndex = 38
		Me.lblChl2Unit2.Text = "[℃]"
		'
		'lblHedUnit1
		'
		Me.lblHedUnit1.AutoSize = true
		Me.lblHedUnit1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblHedUnit1.ForeColor = System.Drawing.Color.Gray
		Me.lblHedUnit1.Location = New System.Drawing.Point(507, 219)
		Me.lblHedUnit1.Name = "lblHedUnit1"
		Me.lblHedUnit1.Size = New System.Drawing.Size(43, 16)
		Me.lblHedUnit1.TabIndex = 39
		Me.lblHedUnit1.Text = "[秒]"
		'
		'lblHedUnit2
		'
		Me.lblHedUnit2.AutoSize = true
		Me.lblHedUnit2.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblHedUnit2.ForeColor = System.Drawing.Color.Gray
		Me.lblHedUnit2.Location = New System.Drawing.Point(673, 221)
		Me.lblHedUnit2.Name = "lblHedUnit2"
		Me.lblHedUnit2.Size = New System.Drawing.Size(43, 16)
		Me.lblHedUnit2.TabIndex = 40
		Me.lblHedUnit2.Text = "[秒]"
		'
		'lblCmbUnit2
		'
		Me.lblCmbUnit2.AutoSize = true
		Me.lblCmbUnit2.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblCmbUnit2.ForeColor = System.Drawing.Color.Gray
		Me.lblCmbUnit2.Location = New System.Drawing.Point(673, 262)
		Me.lblCmbUnit2.Name = "lblCmbUnit2"
		Me.lblCmbUnit2.Size = New System.Drawing.Size(44, 16)
		Me.lblCmbUnit2.TabIndex = 41
		Me.lblCmbUnit2.Text = "[Pa]"
		'
		'lblWbpUnit1
		'
		Me.lblWbpUnit1.AutoSize = true
		Me.lblWbpUnit1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblWbpUnit1.ForeColor = System.Drawing.Color.Gray
		Me.lblWbpUnit1.Location = New System.Drawing.Point(507, 302)
		Me.lblWbpUnit1.Name = "lblWbpUnit1"
		Me.lblWbpUnit1.Size = New System.Drawing.Size(44, 16)
		Me.lblWbpUnit1.TabIndex = 42
		Me.lblWbpUnit1.Text = "[Pa]"
		'
		'lblWbpUnit2
		'
		Me.lblWbpUnit2.AutoSize = true
		Me.lblWbpUnit2.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblWbpUnit2.ForeColor = System.Drawing.Color.Gray
		Me.lblWbpUnit2.Location = New System.Drawing.Point(673, 302)
		Me.lblWbpUnit2.Name = "lblWbpUnit2"
		Me.lblWbpUnit2.Size = New System.Drawing.Size(44, 16)
		Me.lblWbpUnit2.TabIndex = 43
		Me.lblWbpUnit2.Text = "[Pa]"
		'
		'TimItv
		'
		'
		'Label1
		'
		Me.Label1.AutoSize = true
		Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.Label1.ForeColor = System.Drawing.Color.Gray
		Me.Label1.Location = New System.Drawing.Point(41, 336)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(132, 26)
		Me.Label1.TabIndex = 44
		Me.Label1.Text = "全条件成立"
		'
		'lblCntAllOK
		'
		Me.lblCntAllOK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblCntAllOK.Location = New System.Drawing.Point(740, 335)
		Me.lblCntAllOK.Name = "lblCntAllOK"
		Me.lblCntAllOK.Size = New System.Drawing.Size(90, 32)
		Me.lblCntAllOK.TabIndex = 45
		Me.lblCntAllOK.Text = "99999"
		Me.lblCntAllOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblStsAllOK
		'
		Me.lblStsAllOK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblStsAllOK.Location = New System.Drawing.Point(843, 336)
		Me.lblStsAllOK.Name = "lblStsAllOK"
		Me.lblStsAllOK.Size = New System.Drawing.Size(135, 32)
		Me.lblStsAllOK.TabIndex = 47
		Me.lblStsAllOK.Text = "成立待ち"
		Me.lblStsAllOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
		'
		'lblAimAllOK
		'
		Me.lblAimAllOK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblAimAllOK.Location = New System.Drawing.Point(411, 333)
		Me.lblAimAllOK.Name = "lblAimAllOK"
		Me.lblAimAllOK.Size = New System.Drawing.Size(90, 32)
		Me.lblAimAllOK.TabIndex = 48
		Me.lblAimAllOK.Text = "1000"
		Me.lblAimAllOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblNowAllOK
		'
		Me.lblNowAllOK.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
		Me.lblNowAllOK.Location = New System.Drawing.Point(577, 336)
		Me.lblNowAllOK.Name = "lblNowAllOK"
		Me.lblNowAllOK.Size = New System.Drawing.Size(90, 32)
		Me.lblNowAllOK.TabIndex = 49
		Me.lblNowAllOK.Text = "10000"
		Me.lblNowAllOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
		'
		'lblAllOKUnit2
		'
		Me.lblAllOKUnit2.AutoSize = true
		Me.lblAllOKUnit2.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblAllOKUnit2.ForeColor = System.Drawing.Color.Gray
		Me.lblAllOKUnit2.Location = New System.Drawing.Point(673, 345)
		Me.lblAllOKUnit2.Name = "lblAllOKUnit2"
		Me.lblAllOKUnit2.Size = New System.Drawing.Size(43, 16)
		Me.lblAllOKUnit2.TabIndex = 51
		Me.lblAllOKUnit2.Text = "[秒]"
		'
		'lblAllOKUnit1
		'
		Me.lblAllOKUnit1.AutoSize = true
		Me.lblAllOKUnit1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.lblAllOKUnit1.ForeColor = System.Drawing.Color.Gray
		Me.lblAllOKUnit1.Location = New System.Drawing.Point(507, 343)
		Me.lblAllOKUnit1.Name = "lblAllOKUnit1"
		Me.lblAllOKUnit1.Size = New System.Drawing.Size(43, 16)
		Me.lblAllOKUnit1.TabIndex = 50
		Me.lblAllOKUnit1.Text = "[秒]"
		'
		'BtnSkip
		'
		Me.BtnSkip.Location = New System.Drawing.Point(616, 385)
		Me.BtnSkip.Name = "BtnSkip"
		Me.BtnSkip.Size = New System.Drawing.Size(181, 49)
		Me.BtnSkip.TabIndex = 52
		Me.BtnSkip.Text = "スキップ"
		Me.BtnSkip.UseVisualStyleBackColor = true
		'
		'FrmWaitCdt
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(12!, 24!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(1019, 457)
		Me.Controls.Add(Me.BtnSkip)
		Me.Controls.Add(Me.lblAllOKUnit2)
		Me.Controls.Add(Me.lblAllOKUnit1)
		Me.Controls.Add(Me.lblNowAllOK)
		Me.Controls.Add(Me.lblAimAllOK)
		Me.Controls.Add(Me.lblStsAllOK)
		Me.Controls.Add(Me.lblCntAllOK)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.lblWbpUnit2)
		Me.Controls.Add(Me.lblWbpUnit1)
		Me.Controls.Add(Me.lblCmbUnit2)
		Me.Controls.Add(Me.lblHedUnit2)
		Me.Controls.Add(Me.lblHedUnit1)
		Me.Controls.Add(Me.lblChl2Unit2)
		Me.Controls.Add(Me.lblChl1Unit2)
		Me.Controls.Add(Me.lblChl2Unit1)
		Me.Controls.Add(Me.lblCmbUnit1)
		Me.Controls.Add(Me.lblChl1Unit1)
		Me.Controls.Add(Me.BtnStop)
		Me.Controls.Add(Me.lblCntWbp)
		Me.Controls.Add(Me.lblStsWbp)
		Me.Controls.Add(Me.lblNowWbp)
		Me.Controls.Add(Me.lblAimWbp)
		Me.Controls.Add(Me.lblWbp)
		Me.Controls.Add(Me.lblCntCmb)
		Me.Controls.Add(Me.lblStsCmb)
		Me.Controls.Add(Me.lblNowCmb)
		Me.Controls.Add(Me.lblAimCmb)
		Me.Controls.Add(Me.lblCmb)
		Me.Controls.Add(Me.lblCntHed)
		Me.Controls.Add(Me.lblStsHed)
		Me.Controls.Add(Me.lblNowHed)
		Me.Controls.Add(Me.lblAimHed)
		Me.Controls.Add(Me.lblHed)
		Me.Controls.Add(Me.lblCntChl2)
		Me.Controls.Add(Me.lblStsChl2)
		Me.Controls.Add(Me.lblNowChl2)
		Me.Controls.Add(Me.lblAimChl2)
		Me.Controls.Add(Me.lblChl2)
		Me.Controls.Add(Me.lblCntChl1)
		Me.Controls.Add(Me.Label12)
		Me.Controls.Add(Me.lblCntSyn)
		Me.Controls.Add(Me.Label10)
		Me.Controls.Add(Me.lblStsChl1)
		Me.Controls.Add(Me.Label8)
		Me.Controls.Add(Me.lblNowChl1)
		Me.Controls.Add(Me.Label6)
		Me.Controls.Add(Me.lblAimChl1)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.lblTitle)
		Me.Controls.Add(Me.lblChl1)
		Me.Font = New System.Drawing.Font("ＭＳ ゴシック", 18!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128,Byte))
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Margin = New System.Windows.Forms.Padding(6)
		Me.MaximizeBox = false
		Me.MinimizeBox = false
		Me.Name = "FrmWaitCdt"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "条件待ち"
		Me.ResumeLayout(false)
		Me.PerformLayout

End Sub
    Friend WithEvents lblChl1 As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblAimChl1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblNowChl1 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblStsChl1 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblCntSyn As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblCntChl1 As System.Windows.Forms.Label
    Friend WithEvents lblCntChl2 As System.Windows.Forms.Label
    Friend WithEvents lblStsChl2 As System.Windows.Forms.Label
    Friend WithEvents lblNowChl2 As System.Windows.Forms.Label
    Friend WithEvents lblAimChl2 As System.Windows.Forms.Label
    Friend WithEvents lblChl2 As System.Windows.Forms.Label
    Friend WithEvents lblCntHed As System.Windows.Forms.Label
    Friend WithEvents lblStsHed As System.Windows.Forms.Label
    Friend WithEvents lblNowHed As System.Windows.Forms.Label
    Friend WithEvents lblAimHed As System.Windows.Forms.Label
    Friend WithEvents lblHed As System.Windows.Forms.Label
    Friend WithEvents lblCntCmb As System.Windows.Forms.Label
    Friend WithEvents lblStsCmb As System.Windows.Forms.Label
    Friend WithEvents lblNowCmb As System.Windows.Forms.Label
    Friend WithEvents lblAimCmb As System.Windows.Forms.Label
    Friend WithEvents lblCmb As System.Windows.Forms.Label
    Friend WithEvents lblCntWbp As System.Windows.Forms.Label
    Friend WithEvents lblStsWbp As System.Windows.Forms.Label
    Friend WithEvents lblNowWbp As System.Windows.Forms.Label
    Friend WithEvents lblAimWbp As System.Windows.Forms.Label
    Friend WithEvents lblWbp As System.Windows.Forms.Label
    Friend WithEvents BtnStop As System.Windows.Forms.Button
    Friend WithEvents lblChl1Unit1 As System.Windows.Forms.Label
    Friend WithEvents lblCmbUnit1 As System.Windows.Forms.Label
    Friend WithEvents lblChl2Unit1 As System.Windows.Forms.Label
    Friend WithEvents lblChl1Unit2 As System.Windows.Forms.Label
    Friend WithEvents lblChl2Unit2 As System.Windows.Forms.Label
    Friend WithEvents lblHedUnit1 As System.Windows.Forms.Label
    Friend WithEvents lblHedUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblCmbUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblWbpUnit1 As System.Windows.Forms.Label
    Friend WithEvents lblWbpUnit2 As System.Windows.Forms.Label
    Friend WithEvents TimItv As System.Windows.Forms.Timer
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblCntAllOK As System.Windows.Forms.Label
    Friend WithEvents lblStsAllOK As System.Windows.Forms.Label
    Friend WithEvents lblAimAllOK As System.Windows.Forms.Label
    Friend WithEvents lblNowAllOK As System.Windows.Forms.Label
    Friend WithEvents lblAllOKUnit2 As System.Windows.Forms.Label
    Friend WithEvents lblAllOKUnit1 As System.Windows.Forms.Label
    Friend WithEvents BtnSkip As System.Windows.Forms.Button
End Class
