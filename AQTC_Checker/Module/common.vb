Module common

	'
	'	デバックモード動作
	'
	'	プログラム起動時にコマンドライン引数で指定する
	'	unittest の指定があればデバックモードで動作する
	'
	Public DBGmode		As Integer	= 0


	'*****
	'	コマンドライン引数をチェックして動作モードを設定する
	'*****
	Public Sub chkcmdline()

		'コマンドライン引数を配列で取得する
		Dim cmds		As String() = System.Environment.GetCommandLineArgs()
		Dim prm			As String

		'コマンドライン引数をチェックする
		For Each prm In cmds

			FrmLog.LogDspAdd( "", "コマンドライン引数 " + prm, Color.Empty )

			If prm = "unittest" Then

				' デバックモードで動作する
				DBGmode		= 1

				FrmLog.LogDspAdd( "", "デバックモード動作", Color.Red )

			End If

		Next

	End Sub

End Module
