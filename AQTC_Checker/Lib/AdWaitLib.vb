

Module AdWaitLib



	'/*****
	'	AD変換1ﾃﾞ-ﾀ確定するまで待つ
	'	AD用ﾘﾝｸﾞﾊﾞｯﾌｧﾎﾟｲﾝﾀが１つ進むのを待つ
	'*****/
	Public Function adwait				_
	(						_
		ByRef ptr		As Integer	_
	)	As Integer

		' ptr;		AD変換用ﾘﾝｸﾞﾊﾞｯﾌｧﾎﾟｲﾝﾀ

		Dim cptr		As Integer
		Dim key			As Integer = 0


		Do

			If StopFlag = True Then

				key			= Keys.Escape

				Exit Do

			End If

			Application.DoEvents()

			cptr			= ADptrR

		Loop While ( ptr = cptr )

		ptr			= cptr


		Return key

	End Function



	'/*****
	'	AD変換1ﾃﾞ-ﾀ確定するまで待つ
	'	AD用ﾘﾝｸﾞﾊﾞｯﾌｧﾎﾟｲﾝﾀが１つ進むのを待つ
	'*****/
	Public Function adwaitmsg			_
	(						_
		ByRef ptr		As Integer	_
	)	As Integer

		' ptr;		AD変換用ﾘﾝｸﾞﾊﾞｯﾌｧﾎﾟｲﾝﾀ

		Dim cptr		As Integer
		Dim key			As Integer = 0



		Do

			If StopFlag = True Then

				If flipr( OPIPyn, "処理を中止しますか？" ) = DialogResult.Yes Then

					key			= Keys.Escape

					StopFlag		= False

					Exit Do

				Else

					StopFlag		= False

				End If

			End If

			Application.DoEvents()

			cptr			= ADptrR

		Loop While ( ptr = cptr )

		ptr			= cptr



		Return key

	End Function

End Module
