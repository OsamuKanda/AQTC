Public Class FrmDBG

	' デバック動作時のAI信号模擬入力バッファ
	Public DBGAI( ADnCH )	As UShort

	Private Sub TrackBar1_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar1.ValueChanged

		' AIX01 EXAIO AIN1	ﾊﾞﾗﾄﾛﾝ真空計測定値
		DBGAI( GMaiPRS )	= TrackBar1.Value

	End Sub

	Private Sub TrackBar2_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar2.ValueChanged

		' AIX02 EXAIO AIN2	ﾋﾟﾗﾆ真空計測定値
		DBGAI( GPaiPRS )	= TrackBar2.Value

	End Sub

	Private Sub TrackBar3_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar3.ValueChanged

		' AIX03 EXAIO AIN3	MFC1流量モニタ信号
		DBGAI( MFCaiFLW )	= TrackBar3.Value

	End Sub

	Private Sub TrackBar4_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar4.ValueChanged

		' AIX04 EXAIO AIN4	ESC-2000･CH1出力電圧ﾓﾆﾀ信号
		DBGAI( ESCaiMON1 )	= TrackBar4.Value

	End Sub

	Private Sub TrackBar5_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar5.ValueChanged

		' AIX05 EXAIO AIN5	ESC-2000･CH2出力電圧ﾓﾆﾀ信号
		DBGAI( ESCaiMON2 )	= TrackBar5.Value

	End Sub

	Private Sub TrackBar6_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar6.ValueChanged

		' AIX06 EXAIO AIN6	サーモチラー･CH1ﾓﾆﾀ出力
		DBGAI( SCRaiTMP1 )	= TrackBar6.Value

	End Sub

	Private Sub TrackBar7_ValueChanged(sender As Object, e As EventArgs) Handles TrackBar7.ValueChanged

		' AIX07 EXAIO AIN7	サーモチラー･CH2ﾓﾆﾀ出力
		DBGAI( SCRaiTMP2 )	= TrackBar7.Value

	End Sub

	Private Sub FrmDBG_Load(sender As Object, e As EventArgs) Handles Me.Load

		Dim bak		As Long

		' ﾊﾞﾗﾄﾛﾝ真空計測定値
		bak		= TrackBar1.Value
		TrackBar1.Value	= TrackBar1.Minimum
		TrackBar1.Value	= bak

		bak		= TrackBar2.Value
		TrackBar2.Value	= TrackBar2.Minimum
		TrackBar2.Value	= bak

		bak		= TrackBar3.Value
		TrackBar3.Value	= TrackBar3.Maximum
		TrackBar3.Value	= bak

	End Sub

End Class