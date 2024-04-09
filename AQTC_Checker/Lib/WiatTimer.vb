

Module WiatTimer


	'
	'	ダウンカウントタイマー
	'
	'	20210201 y.goto
	'	タイマー件数を５から１０に変更
	'	タイマー６はHe導入開始タイマー専用とする
	'
	Public TimerDCnt( 10 )		As Double

	Dim dt( 10 )			As DateTime

	Dim ms( 10 )			As Double

	Dim dtnow			As DateTime



	Public Sub WaitTim				_
	(						_
		ByVal tim		As Double	_
	)


		' TimerDCnt(2) = tim

		SetTimDCnt( 2, tim )

		Do While ( TimerDCnt( 2 ) > 0 )

			Application.DoEvents()

		Loop

	End Sub



	'Public Sub WaitTim(ByVal tim As Double)
	'    Dim dt1 As DateTime = DateTime.Now
	'    Dim dt As DateTime
	'    Dim ts As TimeSpan
	'
	'    Do
	'        dt = DateTime.Now
	'        ts = dt.Subtract(dt1)
	'
	'        If ts.TotalMilliseconds > tim Then
	'            Exit Do
	'        End If
	'
	'        Application.DoEvents()
	'    Loop
	'End Sub



	Public Function WaitTimKey			_
	(						_
		ByVal tim		As Double	_
	)	As Integer

		Dim key			As Integer = 0



		' TimerDCnt(2) = tim

		SetTimDCnt( 2, tim )

		Do While ( TimerDCnt( 2 ) > 0 )

			If StopFlag = True Then

				If flipbz1r( OPIPco, "処理を中止しますか？" ) = DialogResult.Cancel Then

					key			= Keys.Escape

					StopFlag		= False

					Exit Do

				Else

					StopFlag		= False

				End If

			End If

			Application.DoEvents()

		Loop


		Return key

	End Function



	'Public Function WaitTimKey(ByVal tim As Double) As Integer
	'    Dim dt1 As DateTime = DateTime.Now
	'    Dim dt As DateTime
	'    Dim ts As TimeSpan
	'    Dim key As Integer = 0
	'
	'    Do
	'        dt = DateTime.Now
	'        ts = dt.Subtract(dt1)
	'
	'        If ts.TotalMilliseconds > tim Then
	'            Exit Do
	'        End If
	'
	'        If StopFlag = True Then
	'            If flip(OPIPok, "") = DialogResult.OK Then
	'                key = Keys.Escape
	'                StopFlag = False
	'                Exit Do
	'            Else
	'                StopFlag = False
	'            End If
	'        End If
	'
	'        Application.DoEvents()
	'    Loop
	'
	'    Return key
	'End Function




	Public Sub SetTimDCnt				_
	(						_
		ByVal ch		As Integer,	_
		ByVal cnt		As Double	_
	)


		TimerDCnt( ch )		= cnt

		ms( ch )		= cnt * 10

		dt( ch )		= DateTime.Now


	End Sub




	'*****
	'	割り込みから呼び出すダウンカウンタ
	'*****
	Public Sub timDownCount()

		Dim i			As Integer
		Dim ts			As TimeSpan

		dtnow			= DateTime.Now

		For i = 0 To TimerDCnt.Length - 1

			If TimerDCnt( i ) > 0 Then

				ts			= dtnow.Subtract( dt( i ) )

				TimerDCnt( i )		= ( ms( i ) - ts.TotalMilliseconds ) \ 10

				If 0 > TimerDCnt( i ) Then

					TimerDCnt( i )	= 0

				End If

			End If

		Next

	End Sub

End Module
