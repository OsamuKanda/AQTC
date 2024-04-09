Public Class DHDtstAio

    Private Sub DHDtstAio_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        WriteLog("", "LG", "DHDtstAio_FormClosed メンテ終了")

    End Sub

    Private Sub DHDtstAio_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        WriteLog("", "LG", "DHDtstAio_Load メンテ開始")

        lblAdV.Text = ""
        lblCnv1.Text = ""
        lblCnv2.Text = ""
        lblCnv3.Text = ""
        lblCnv4.Text = ""
        lblCnv5.Text = ""
        lblCnv6.Text = ""
        lblCnv7.Text = ""
        lblCnv8.Text = ""
        lblDaHex.Text = ""
    End Sub

    Private Sub btnVolt_Click(sender As System.Object, e As System.EventArgs) Handles btnVolt.Click
        Dim raw As UShort
        Dim volt As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        volt = anar2v(raw)

        lblAdV.Text = volt.ToString("0.00")
    End Sub

    Private Sub btnCnv1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv1.Click
        Dim raw As UShort
        Dim ccm As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        ccm = cvtr2MFCop(raw)
        lblCnv1.Text = ccm.ToString("0.00")
    End Sub

    Private Sub btnCnv1_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv1_1.Click
        Dim raw As UShort
        Dim volt As Double
        Dim ccm As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        ccm = cvtr2MFCop(raw, volt)
        lblAdV.Text = volt.ToString("0.00")
        lblCnv1.Text = ccm.ToString("0.00")
    End Sub

    Private Sub btnCnv2_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv2.Click
        Dim volt As Double
        Dim ccm As Double

        volt = CDbl(txtCnv2.Text)

        ccm = cvtv2Ccm(volt)
        lblCnv2.Text = ccm.ToString("0.00")

    End Sub

    Private Sub btnCnv3_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv3.Click
        Dim raw As UShort
        Dim torr As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        torr = cvtr2GM_Torr(raw)
        lblCnv3.Text = torr.ToString("0.00E+00")

    End Sub

    Private Sub btnCnv3_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv3_1.Click
        Dim raw As UShort
        Dim volt As Double
        Dim torr As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        torr = cvtr2GM_Torr(raw, volt)
        lblAdV.Text = volt.ToString("0.00")
        lblCnv3.Text = torr.ToString("0.00E+00")

    End Sub

    Private Sub btnCnv4_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv4.Click
        Dim raw As UShort
        Dim pa As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        pa = cvtr2GM_Pa(raw)
        lblCnv4.Text = pa.ToString("0.00E+00")
    End Sub

    Private Sub btnCnv4_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv4_1.Click
        Dim raw As UShort
        Dim volt As Double
        Dim pa As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        pa = cvtr2GM_Pa(raw, volt)
        lblAdV.Text = volt.ToString("0.00")
        lblCnv4.Text = pa.ToString("0.00E+00")
    End Sub

    Private Sub btnCnv5_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv5.Click
        Dim raw As UShort
        Dim torr As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        torr = cvtr2GP_Torr(raw)
        lblCnv5.Text = torr.ToString("0.00E+00")
    End Sub

    Private Sub btnCnv5_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv5_1.Click
        Dim raw As UShort
        Dim volt As Double
        Dim torr As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        torr = cvtr2GP_Torr(raw, volt)
        lblAdV.Text = volt.ToString("0.00")
        lblCnv5.Text = torr.ToString("0.00E+00")
    End Sub

    Private Sub btnCnv6_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv6.Click
        Dim raw As UShort
        Dim pa As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        pa = cvtr2GP_Pa(raw)
        lblCnv6.Text = pa.ToString("0.00E+00")
    End Sub

    Private Sub btnCnv6_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv6_1.Click
        Dim raw As UShort
        Dim volt As Double
        Dim pa As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        pa = cvtr2GP_Pa(raw, volt)
        lblAdV.Text = volt.ToString("0.00")
        lblCnv6.Text = pa.ToString("0.00E+00")
    End Sub

    Private Sub btnCnv7_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv7.Click
        Dim raw As UShort
        Dim tmp As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        tmp = cvtr2SCR(raw)
        lblCnv7.Text = tmp.ToString("0.00")
    End Sub

    Private Sub btnCnv7_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv7_1.Click
        Dim raw As UShort
        Dim volt As Double
        Dim tmp As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        tmp = cvtr2SCR(raw, volt)
        lblAdV.Text = volt.ToString("0.00")
        lblCnv7.Text = tmp.ToString("0.00")
    End Sub

    Private Sub btnCnv8_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv8.Click
        Dim raw As UShort
        Dim hv As Double

        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        hv = cvtr2ESC(raw)
        lblCnv8.Text = hv.ToString("0.00")
    End Sub

    Private Sub btnCnv8_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv8_1.Click
        Dim raw As UShort
        Dim volt As Double
        Dim hv As Double
        Try
            raw = CUShort("&h" + txtAdbuf.Text)
        Catch ex As Exception
            MessageBox.Show("入力はＨＥＸ４桁です。")
            Exit Sub
        End Try

        hv = cvtr2ESC(raw, volt)
        lblAdV.Text = volt.ToString("0.00")
        lblCnv8.Text = hv.ToString("0.00")
    End Sub

    Private Sub btnCnv11_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv11.Click
        Dim volt As Double
        Dim raw As Short

        volt = CDbl(txtDAV.Text)

        raw = anav2r(0, volt)

        lblDaHex.Text = raw.ToString("X")
    End Sub

    Private Sub btnCnv12_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv12.Click
        Dim volt As Double
        Dim raw As Short

        volt = CDbl(txtDAV.Text)

        raw = anav2r(1, volt)

        lblDaHex.Text = raw.ToString("X")
    End Sub

    Private Sub btnCnv13_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv13.Click
        Dim volt As Double
        Dim raw As Short

        volt = CDbl(txtDAV.Text)

        raw = anav2r(2, volt)

        lblDaHex.Text = raw.ToString("X")
    End Sub

    Private Sub btnCnv21_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv21.Click
        Dim flow As Double
        Dim raw As Short

        flow = CDbl(txtCnv21.Text)

        raw = cvtf2MFCset(flow)

        lblDaHex.Text = raw.ToString("X")
    End Sub

    Private Sub btnCnv21_1_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv21_1.Click
        Dim flow As Double
        Dim volt As Double
        Dim raw As Short

        flow = CDbl(txtCnv21.Text)

        raw = cvtf2MFCset(flow, volt)

        txtDAV.Text = volt.ToString("0.00")
        lblDaHex.Text = raw.ToString("X")
    End Sub

    Private Sub btnCnv22_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv22.Click
        Dim pa As Double
        Dim volt As Double
        Dim raw As Short

        pa = CDbl(txtCnv22.Text)

        raw = cvtp2PIDset(pa, volt)

        txtDAV.Text = volt.ToString("0.00")
        lblDaHex.Text = raw.ToString("X")
    End Sub

    Private Sub btnCnv23_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv23.Click
        Dim temp As Double
        Dim volt As Double
        Dim raw As Short

        temp = CDbl(txtCnv23.Text)

        raw = cvtt2SCR(temp, volt)

        txtDAV.Text = volt.ToString("0.00")
        lblDaHex.Text = raw.ToString("X")
    End Sub

    Private Sub btnCnv24_Click(sender As System.Object, e As System.EventArgs) Handles btnCnv24.Click
        Dim v As Double
        Dim volt As Double
        Dim raw As Short

        v = CDbl(txtCnv24.Text)

        raw = cvtv2ESC(v, volt)

        txtDAV.Text = volt.ToString("0.00")
        lblDaHex.Text = raw.ToString("x")
    End Sub
End Class