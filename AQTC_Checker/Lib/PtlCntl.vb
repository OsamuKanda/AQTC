
Module PtlCntl


	'
	'	パトライト点灯、点滅指示フラグ
	'
	'	bit0	赤・点灯指示フラグ
	'	bit1	赤・点滅指示フラグ
	'	bit2	黄・点灯指示フラグ
	'	bit3	黄・点滅指示フラグ
	'	bit4	緑・点灯指示フラグ
	'	bit5	緑・点滅指示フラグ
	'
	Public PTLctl			As Integer = 0

	'
	'	パトライト点滅カウンタ
	'
	Public PTLstsRED		As Integer
	Public PTLcntRED		As Integer
	Public PTLstsYEL		As Integer
	Public PTLcntYEL		As Integer
	Public PTLstsGRN		As Integer
	Public PTLcntGRN		As Integer



	'*****
	'	ブザー停止
	'*****
	Public Sub PTLBzStop()


		' OUT1	ｼﾝｸﾞﾅﾙﾀﾜｰBUZZER1を停止
		ExDio_Output( MAEdoBZ1, DIO_OFF )

		' 20200205 y.goto
		' １号機では２種類ブザーが有ったが２号機では１種類


	End Sub



	'*****
	'	ブザー１吹鳴
	'*****
	Public Sub PTLBz1ON()


		' OUT1	ｼﾝｸﾞﾅﾙﾀﾜｰBUZZER1を鳴らす
		ExDio_Output( MAEdoBZ1, DIO_ON )


	End Sub



	'*****
	'	ブザー２吹鳴
	'*****
	Public Sub PTLBz2ON()


		' 20200205 y.goto
		' １号機では２種類ブザーが有ったが２号機では１種類

		' OUT1	ｼﾝｸﾞﾅﾙﾀﾜｰBUZZER1を鳴らす
		ExDio_Output( MAEdoBZ1, DIO_ON )


	End Sub



	'*****
	'	パトライト・オペレータ確認要求表示Ａ
	'*****
	Public Sub PTLOpeReqTypeAon()


		'
		'	パトライト赤点滅
		'
		PTLctl			= PTLctl Or PTLctlREDfl

		' ブザー２吹鳴
		PTLBz2ON()

	End Sub



	'*****
	'	パトライト・オペレータ確認要求表示Ａ終了
	'*****
	Public Sub PTLOpeReqTypeAoff()


		'
		'	パトライト赤点滅終了
		'
		PTLctl			= PTLctl And( Not  PTLctlREDfl )

		' ブザー停止
		PTLBzStop()

	End Sub



	'*****
	'	パトライト点灯、点滅処理
	'*****
	Public Sub PTLControl()



		'
		'	パトライト赤
		'
		If PTLctl And PTLctlREDon Then

			ExDio_Output( MAEdoLEDR, DIO_ON )

		ElseIf PTLctl And PTLctlREDfl Then

			If PTLcntRED Then

				PTLcntRED		-= 1

			Else

				If PTLstsRED Then

					ExDio_Output( MAEdoLEDR, DIO_OFF )

					PTLstsRED		= 0

					PTLcntRED		= PTLoffCLK

				Else

					ExDio_Output( MAEdoLEDR, DIO_ON )

					PTLstsRED		= 1

					PTLcntRED		= PTLonCLK

				End If

			End If

		Else

			ExDio_Output( MAEdoLEDR, DIO_OFF )

		End If



		'
		'	パトライト黄
		'
		If PTLctl And PTLctlYELon Then

			ExDio_Output( MAEdoLEDY, DIO_ON )

		ElseIf PTLctl And PTLctlYELfl Then

			If PTLcntYEL Then

				PTLcntYEL		-= 1

			Else

				If PTLstsYEL Then

					ExDio_Output( MAEdoLEDY, DIO_OFF )

					PTLstsYEL		= 0

					PTLcntYEL		= PTLoffCLK

				Else

					ExDio_Output( MAEdoLEDY, DIO_ON )

					PTLstsYEL		= 1

					PTLcntYEL		= PTLonCLK

				End If

			End If

		Else

			ExDio_Output( MAEdoLEDY, DIO_OFF )

		End If



		'
		'	パトライト緑
		'
		If PTLctl And PTLctlGRNon Then

			ExDio_Output( MAEdoLEDG, DIO_ON )

		ElseIf ( PTLctl And PTLctlGRNfl ) Then

			If ( PTLcntGRN ) Then

				PTLcntGRN		-= 1

			Else

				If ( PTLstsGRN ) Then

					ExDio_Output( MAEdoLEDG, DIO_OFF )

					PTLstsGRN		= 0

					PTLcntGRN		= PTLoffCLK

				Else

					ExDio_Output( MAEdoLEDG, DIO_ON )

					PTLstsGRN		= 1

					PTLcntGRN		= PTLonCLK

				End If

			End If

		Else

			ExDio_Output( MAEdoLEDG, DIO_OFF )

		End If


	End Sub


End Module
