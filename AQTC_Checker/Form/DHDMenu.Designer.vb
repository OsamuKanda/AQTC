<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DHDMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DHDMenu))
        Me.btnAuto = New System.Windows.Forms.Button()
        Me.btnMente = New System.Windows.Forms.Button()
        Me.btnEnd = New System.Windows.Forms.Button()
        Me.lblVer = New System.Windows.Forms.Label()
        Me.btnTst = New System.Windows.Forms.Button()
        Me.btnZetsuen = New System.Windows.Forms.Button()
        Me.btnKyucyaku = New System.Windows.Forms.Button()
        Me.btnHeGas = New System.Windows.Forms.Button()
        Me.btnParmSet = New System.Windows.Forms.Button()
        Me.TimINP = New System.Windows.Forms.Timer(Me.components)
        Me.btnBzStop = New System.Windows.Forms.Button()
        Me.btnZanryu = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAuto
        '
        Me.btnAuto.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnAuto.Location = New System.Drawing.Point(115, 40)
        Me.btnAuto.Name = "btnAuto"
        Me.btnAuto.Size = New System.Drawing.Size(157, 34)
        Me.btnAuto.TabIndex = 0
        Me.btnAuto.Text = "自動検査"
        Me.btnAuto.UseVisualStyleBackColor = True
        '
        'btnMente
        '
        Me.btnMente.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnMente.Location = New System.Drawing.Point(115, 290)
        Me.btnMente.Name = "btnMente"
        Me.btnMente.Size = New System.Drawing.Size(157, 34)
        Me.btnMente.TabIndex = 2
        Me.btnMente.Text = "メンテナンス"
        Me.btnMente.UseVisualStyleBackColor = True
        '
        'btnEnd
        '
        Me.btnEnd.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEnd.Location = New System.Drawing.Point(115, 390)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(157, 34)
        Me.btnEnd.TabIndex = 3
        Me.btnEnd.Text = "終了"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'lblVer
        '
        Me.lblVer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblVer.AutoSize = True
        Me.lblVer.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblVer.Location = New System.Drawing.Point(301, 9)
        Me.lblVer.Name = "lblVer"
        Me.lblVer.Size = New System.Drawing.Size(43, 13)
        Me.lblVer.TabIndex = 4
        Me.lblVer.Text = "Label1"
        Me.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnTst
        '
        Me.btnTst.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnTst.Location = New System.Drawing.Point(-48, 340)
        Me.btnTst.Name = "btnTst"
        Me.btnTst.Size = New System.Drawing.Size(157, 34)
        Me.btnTst.TabIndex = 5
        Me.btnTst.Text = "Ａｉｏ関数テスト"
        Me.btnTst.UseVisualStyleBackColor = True
        Me.btnTst.Visible = False
        '
        'btnZetsuen
        '
        Me.btnZetsuen.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnZetsuen.Location = New System.Drawing.Point(115, 90)
        Me.btnZetsuen.Name = "btnZetsuen"
        Me.btnZetsuen.Size = New System.Drawing.Size(157, 34)
        Me.btnZetsuen.TabIndex = 6
        Me.btnZetsuen.Text = "絶縁抵抗測定"
        Me.btnZetsuen.UseVisualStyleBackColor = True
        '
        'btnKyucyaku
        '
        Me.btnKyucyaku.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnKyucyaku.Location = New System.Drawing.Point(115, 140)
        Me.btnKyucyaku.Name = "btnKyucyaku"
        Me.btnKyucyaku.Size = New System.Drawing.Size(157, 34)
        Me.btnKyucyaku.TabIndex = 7
        Me.btnKyucyaku.Text = "ウエハ吸着力測定"
        Me.btnKyucyaku.UseVisualStyleBackColor = True
        '
        'btnHeGas
        '
        Me.btnHeGas.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnHeGas.Location = New System.Drawing.Point(115, 190)
        Me.btnHeGas.Name = "btnHeGas"
        Me.btnHeGas.Size = New System.Drawing.Size(157, 34)
        Me.btnHeGas.TabIndex = 8
        Me.btnHeGas.Text = "Ｈｅリーク量測定"
        Me.btnHeGas.UseVisualStyleBackColor = True
        '
        'btnParmSet
        '
        Me.btnParmSet.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnParmSet.Location = New System.Drawing.Point(115, 340)
        Me.btnParmSet.Name = "btnParmSet"
        Me.btnParmSet.Size = New System.Drawing.Size(157, 34)
        Me.btnParmSet.TabIndex = 9
        Me.btnParmSet.Text = "試験パラメータ設定"
        Me.btnParmSet.UseVisualStyleBackColor = True
        '
        'TimINP
        '
        '
        'btnBzStop
        '
        Me.btnBzStop.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnBzStop.Location = New System.Drawing.Point(115, 440)
        Me.btnBzStop.Name = "btnBzStop"
        Me.btnBzStop.Size = New System.Drawing.Size(157, 34)
        Me.btnBzStop.TabIndex = 10
        Me.btnBzStop.Text = "ブザー停止"
        Me.btnBzStop.UseVisualStyleBackColor = True
        '
        'btnZanryu
        '
        Me.btnZanryu.Font = New System.Drawing.Font("MS UI Gothic", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnZanryu.Location = New System.Drawing.Point(115, 240)
        Me.btnZanryu.Name = "btnZanryu"
        Me.btnZanryu.Size = New System.Drawing.Size(157, 34)
        Me.btnZanryu.TabIndex = 12
        Me.btnZanryu.Text = "残留吸着力測定"
        Me.btnZanryu.UseVisualStyleBackColor = True
        '
        'DHDMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Info
        Me.ClientSize = New System.Drawing.Size(374, 495)
        Me.Controls.Add(Me.btnZanryu)
        Me.Controls.Add(Me.btnBzStop)
        Me.Controls.Add(Me.btnParmSet)
        Me.Controls.Add(Me.btnHeGas)
        Me.Controls.Add(Me.btnKyucyaku)
        Me.Controls.Add(Me.btnZetsuen)
        Me.Controls.Add(Me.btnTst)
        Me.Controls.Add(Me.lblVer)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.btnMente)
        Me.Controls.Add(Me.btnAuto)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DHDMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "AQTC検査装置 メニュー"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAuto As System.Windows.Forms.Button
    Friend WithEvents btnMente As System.Windows.Forms.Button
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents lblVer As System.Windows.Forms.Label
    Friend WithEvents btnTst As System.Windows.Forms.Button
    Friend WithEvents btnZetsuen As System.Windows.Forms.Button
    Friend WithEvents btnKyucyaku As System.Windows.Forms.Button
    Friend WithEvents btnHeGas As System.Windows.Forms.Button
    Friend WithEvents btnParmSet As System.Windows.Forms.Button
    Friend WithEvents TimINP As System.Windows.Forms.Timer
    Friend WithEvents btnBzStop As System.Windows.Forms.Button
	Friend WithEvents btnZanryu As Button
End Class
