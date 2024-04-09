
Module RyHv


	Public Sub RyHvMode				_
	(						_
		ByVal pol		As Integer,	_
		ByVal mes		As Integer	_
	)

		' 電極測定の選択
		' pol: 0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ
		' mes: 0=絶縁抵抗計 / 1=ＥＳＣ電源



		If pol = POL_MON Then

			If mes = MES_IOS Then

				' モノポール絶縁抵抗計
				ExDio_Output( MAEdoRYHV01, DIO_OFF	)
				ExDio_Output( MAEdoRYHV02, DIO_OFF	)
				ExDio_Output( MAEdoRYHV03, DIO_OFF	)

			Else

				' モノポールＥＳＣ電源
				ExDio_Output( MAEdoRYHV01, DIO_ON	)
				ExDio_Output( MAEdoRYHV02, DIO_ON	)
				ExDio_Output( MAEdoRYHV03, DIO_OFF	)

			End If

		Else

			If mes = MES_IOS Then

				' ダイポール絶縁抵抗計
				ExDio_Output( MAEdoRYHV01, DIO_OFF	)
				ExDio_Output( MAEdoRYHV02, DIO_OFF	)
				ExDio_Output( MAEdoRYHV03, DIO_OFF	)

			Else

				' ダイポールＥＳＣ電源
				ExDio_Output( MAEdoRYHV01, DIO_ON	)
				ExDio_Output( MAEdoRYHV02, DIO_OFF	)
				ExDio_Output( MAEdoRYHV03, DIO_ON	)

			End If

		End If



	End Sub




	Public Sub RyHvPos				_
	(						_
		ByVal pol		As Integer,	_
		ByVal mes		As Integer,	_
		ByVal sel		As Integer	_
	)

		' 電極接続の選択
		' pol: 0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ
		' mes: 0=絶縁抵抗計 / 1=ＥＳＣ電源
		' sel: 0=OFF 1-7=接続

		If pol = POL_MON Then

			'
			'	モノポール電極
			'
			If sel = CON_OFF Then

				' 高圧リレーを全てOFF
				ExDio_Output( MAEdoRYHV10, DIO_OFF	)
				ExDio_Output( MAEdoRYHV11, DIO_OFF	)
				ExDio_Output( MAEdoRYHV12, DIO_OFF	)
				ExDio_Output( MAEdoRYHV13, DIO_OFF	)
				ExDio_Output( MAEdoRYHV14, DIO_OFF	)
				ExDio_Output( MAEdoRYHV15, DIO_OFF	)
				ExDio_Output( MAEdoRYHV16, DIO_OFF	)
				ExDio_Output( MAEdoRYHV17, DIO_OFF	)
				ExDio_Output( MAEdoRYHV18, DIO_OFF	)
				ExDio_Output( MAEdoRYHV19, DIO_OFF	)

			Else

				' 高圧リレーを10,11をON、他はOFF
				ExDio_Output( MAEdoRYHV10, DIO_ON	)
				ExDio_Output( MAEdoRYHV11, DIO_ON	)
				ExDio_Output( MAEdoRYHV12, DIO_OFF	)
				ExDio_Output( MAEdoRYHV13, DIO_OFF	)
				ExDio_Output( MAEdoRYHV14, DIO_OFF	)
				ExDio_Output( MAEdoRYHV15, DIO_OFF	)
				ExDio_Output( MAEdoRYHV16, DIO_OFF	)
				ExDio_Output( MAEdoRYHV17, DIO_OFF	)
				ExDio_Output( MAEdoRYHV18, DIO_OFF	)
				ExDio_Output( MAEdoRYHV19, DIO_OFF	)

			End If

		Else

			'
			'	ダイポール電極
			'
			If mes = MES_ESC Then

				If sel = CON_OFF Then

					'
					'	高圧リレーを全てOFF
					'
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_OFF	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_OFF	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_OFF	)
					ExDio_Output( MAEdoRYHV17, DIO_OFF	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_OFF	)

				Else

					'
					'	接続パターン毎の処理
					'	20140219｢700系ﾀﾞｲﾎﾟｰﾙ(埋込電極1ｹ)｣に対応する処理追加
					'
					Select Case	sel

					Case	CON_IN_BASE

						'
						'	ダイポール電極接続  ｅ・内ー基材
						'

						' 高圧リレーを16,19をON、他はOFF
						ExDio_Output( MAEdoRYHV10, DIO_OFF	)
						ExDio_Output( MAEdoRYHV11, DIO_OFF	)
						ExDio_Output( MAEdoRYHV12, DIO_OFF	)
						ExDio_Output( MAEdoRYHV13, DIO_OFF	)
						ExDio_Output( MAEdoRYHV14, DIO_OFF	)
						ExDio_Output( MAEdoRYHV15, DIO_OFF	)
						ExDio_Output( MAEdoRYHV16, DIO_ON	)
						ExDio_Output( MAEdoRYHV17, DIO_OFF	)
						ExDio_Output( MAEdoRYHV18, DIO_OFF	)
						ExDio_Output( MAEdoRYHV19, DIO_ON	)

					Case Else

						'
						'	ダイポール電極・ＥＳＣ電源と接続
						'	図面 : DHI13907
						'	高圧リレーを15,16をON、他はOFF
						'
						ExDio_Output( MAEdoRYHV10, DIO_OFF	)
						ExDio_Output( MAEdoRYHV11, DIO_OFF	)
						ExDio_Output( MAEdoRYHV12, DIO_OFF	)
						ExDio_Output( MAEdoRYHV13, DIO_OFF	)
						ExDio_Output( MAEdoRYHV14, DIO_OFF	)
						ExDio_Output( MAEdoRYHV15, DIO_ON	)
						ExDio_Output( MAEdoRYHV16, DIO_ON	)
						ExDio_Output( MAEdoRYHV17, DIO_OFF	)
						ExDio_Output( MAEdoRYHV18, DIO_OFF	)
						ExDio_Output( MAEdoRYHV19, DIO_OFF	)

					End Select

				End If

			Else

				'
				'	絶縁抵抗計と接続
				'

				' 接続パターン毎の処理
				Select Case	sel

				Case	CON_IN_OUT

					'
					'	ダイポール電極接続  ａ・内ー外
					'

					' 高圧リレーを14,17をON、他はOFF
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_OFF	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_ON	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_OFF	)
					ExDio_Output( MAEdoRYHV17, DIO_ON	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_OFF	)


				Case	CON_WAF_IN

					'
					'	ダイポール電極接続  ｂ・ウエハー内
					'

					' 高圧リレーを12,17をON、他はOFF
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_ON	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_OFF	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_OFF	)
					ExDio_Output( MAEdoRYHV17, DIO_ON	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_OFF	)


				Case	CON_WAF_OUT

					'
					'	ダイポール電極接続  ｃ・ウエハー外
					'

					' 高圧リレーを12,15をON、他はOFF
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_ON	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_OFF	)
					ExDio_Output( MAEdoRYHV15, DIO_ON	)
					ExDio_Output( MAEdoRYHV16, DIO_OFF	)
					ExDio_Output( MAEdoRYHV17, DIO_OFF	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_OFF	)


				Case	CON_INOUT_BASE

					'
					'	ダイポール電極接続  ｄ・内＋外ー基材
					'

					' 高圧リレーを14,16,19をON、他はOFF
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_OFF	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_ON	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_ON	)
					ExDio_Output( MAEdoRYHV17, DIO_OFF	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_ON	)


				Case	CON_IN_BASE

					'
					'	ダイポール電極接続  ｅ・内ー基材
					'

					' 高圧リレーを16,19をON、他はOFF
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_OFF	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_OFF	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_ON	)
					ExDio_Output( MAEdoRYHV17, DIO_OFF	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_ON	)


				Case	CON_OUT_BASE

					'
					'	ダイポール電極接続  ｆ・外ー基材
					'

					' 高圧リレーを14,19をON、他はOFF
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_OFF	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_ON	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_OFF	)
					ExDio_Output( MAEdoRYHV17, DIO_OFF	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_ON	)


				Case	CON_WAF_BASE

					'
					'	ダイポール電極接続  ｇ・ウエハー基材
					'

					' 高圧リレーを12,19をON、他はOFF
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_ON	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_OFF	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_OFF	)
					ExDio_Output( MAEdoRYHV17, DIO_OFF	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_ON	)


				Case Else

					'
					'	高圧リレーを全てOFF
					'
					ExDio_Output( MAEdoRYHV10, DIO_OFF	)
					ExDio_Output( MAEdoRYHV11, DIO_OFF	)
					ExDio_Output( MAEdoRYHV12, DIO_OFF	)
					ExDio_Output( MAEdoRYHV13, DIO_OFF	)
					ExDio_Output( MAEdoRYHV14, DIO_OFF	)
					ExDio_Output( MAEdoRYHV15, DIO_OFF	)
					ExDio_Output( MAEdoRYHV16, DIO_OFF	)
					ExDio_Output( MAEdoRYHV17, DIO_OFF	)
					ExDio_Output( MAEdoRYHV18, DIO_OFF	)
					ExDio_Output( MAEdoRYHV19, DIO_OFF	)


				End Select

			End If

		End If

	  End Sub



End Module
