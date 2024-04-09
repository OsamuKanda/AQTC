Imports System.Configuration



Module CfgGpib


	' ＧＰＩＢ・ボード番号
	Public GPIBNo			As Integer

	' ＧＰＩＢ・マイアドレス
	Public GPIBadMY			As Integer

	' ＧＰＩＢ・Ｒ３８４０Ａアドレス
	Public GPIBadR3840A		As Integer



	Public Sub cfgGpibIni()

		Dim no			As String = ConfigurationManager.AppSettings.Item( "GPIBNo" )



		If no <> "" And IsNumeric( no ) Then

			GPIBNo			= CInt( no )

		Else

			GPIBNo			= 0

		End If



		Dim adr			As String = ConfigurationManager.AppSettings.Item( "GPIBadMY" )


		If adr <> "" And IsNumeric( adr ) Then

			GPIBadMY		= CInt( adr )

		Else

			GPIBadMY		= 1

		End If



		adr			= ConfigurationManager.AppSettings.Item( "GPIBadR3840A" )

		If adr <> "" And IsNumeric( adr ) Then

			GPIBadR3840A		= CInt( adr )

		Else

			GPIBadR3840A		= 2

		End If



	End Sub


End Module
