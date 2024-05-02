
Module dbTblSub

#Region "測定データテーブル"



	'*****
	'	絶縁抵抗測定用データテーブル初期化
	'*****
	Public Sub createTblZetsuenn(ByRef dt As DataTable)


		With dt.Columns

			' 電圧印加箇所
			.Add( "VIN_POS", Type.GetType( "System.String") )

			' 印加電圧
			.Add( "VOLT_IN", Type.GetType( "System.String") )

			' 電流値
			.Add( "DENRYU", Type.GetType( "System.String") )

			' 絶縁抵抗
			.Add( "ZETSU_R", Type.GetType( "System.String") )

			' 判定基準
			.Add( "H_BASE", Type.GetType( "System.String") )

			' 判定
			.Add( "HANTEI", Type.GetType( "System.String") )

			' 真空度
			.Add( "SINKUDO", Type.GetType( "System.String") )


		End With


	End Sub



	'*****
	'	ウエハ吸着力測定用データテーブル初期化
	'*****
	Public Sub createTblKyuucyaku			_
	(						_
		ByRef dt		As DataTable	_
	)


		With dt.Columns

			' 電圧印加箇所
			' .Add( "VIN_POS", Type.GetType( "System.String" ) )

			' 印加電圧 CH1
			.Add( "VOLT_IN", Type.GetType( "System.String" ) )

			' 印加電圧 CH2
			.Add( "VOLT_OUT", Type.GetType( "System.String" ) )

			'   20201102 s.harada
			'   トーカロ専用で削除
			'' 裏面圧実測値
			'.Add( "PRESS1", Type.GetType( "System.String" ) )

			'' 裏面圧力実測値２
			'.Add( "PRESS2", Type.GetType( "System.String" ) )

			'' 裏面圧力実測値３
			'.Add( "PRESS3", Type.GetType( "System.String" ) )

			'   20201102 s.harada
			'   トーカロ専用で追加
			' Ｈｅ流量
			.Add( "HE_FLOW", Type.GetType( "System.String" ) )

			' 到達時間１kPa
			.Add( "TIME1", Type.GetType( "System.String" ) )

			' 到達時間２kPa
			.Add( "TIME2", Type.GetType( "System.String" ) )

			' 到達時間３kPa
			.Add( "TIME3", Type.GetType( "System.String" ) )

			' 到達時間４kPa
			.Add( "TIME4", Type.GetType( "System.String" ) )

			' 到達時間６kPa
			.Add( "TIME6", Type.GetType( "System.String" ) )

			' 判定基準
			.Add( "H_BASE", Type.GetType( "System.String" ) )

			'   20201102 s.harada
			'   トーカロ専用で削除
			'' 到達時間
			'.Add( "TIME", Type.GetType( "System.String" ) )

			' 判定so
			.Add( "HANTEI", Type.GetType( "System.String" ) )


		End With



	End Sub


	'*****
	'	Ｈｅリーク量測定用データテーブル初期化
	'*****
	Public Sub createTblHeGas				_
	(							_
		ByRef dt		As DataTable		_
	)


		With dt.Columns


			' 電圧印加箇所
			' .Add( "VIN_POS", Type.GetType( "System.String" ) )

			' 印加電圧 CH1
			.Add( "VOLT_IN", Type.GetType( "System.String" ) )

			' 印加電圧 CH2
			.Add( "VOLT_OUT", Type.GetType( "System.String" ) )

			'	20201102 s.harada	AQTC対応で削除
			' ＭＦＣ電圧
			'.Add( "MFC_VOLT", Type.GetType( "System.String" ) )

			' リーク量
			.Add( "LEAK", Type.GetType( "System.String" ) )


			'	20201102 s.harada	AQTC対応で追加
			' リーク量
			.Add( "LEAK2", Type.GetType( "System.String" ) )

			' リーク量
			.Add( "LEAK3", Type.GetType( "System.String" ) )

			' リーク量
			.Add( "LEAK4", Type.GetType( "System.String" ) )

			' リーク量
			.Add( "LEAK6", Type.GetType( "System.String" ) )


			' 判定基準
			.Add( "H_BASE", Type.GetType( "System.String" ) )


			'	20201102 s.harada	トーカロ専用で追加
			' 判定基準
			.Add( "H_BASE2", Type.GetType( "System.String" ) )


			' 判定
			.Add( "HANTEI", Type.GetType( "System.String" ) )


			'	20201102 s.harada	トーカロ専用で追加
			' 判定
			.Add( "HANTEI2", Type.GetType( "System.String" ) )


		End With



	End Sub


	'   20201102 s.harada
	'  トーカロ対応で残留吸着追加
	'*****
	'	残留吸着力測定用データテーブル初期化
	'*****
	Public Sub createTblZanryu				_
	(							_
		ByRef dt		As DataTable		_
	)


		With dt.Columns

			' 印加電圧 CH1
			.Add( "VOLT_IN", Type.GetType( "System.String" ) )

			' 印加電圧 CH2
			.Add( "VOLT_OUT", Type.GetType( "System.String" ) )

			' 印加前吸着力実測値
			.Add( "PRESS1", Type.GetType( "System.String" ) )

			' 印加後吸着力実測値
			.Add( "PRESS2", Type.GetType( "System.String" ) )

			' 圧力差
			.Add( "PRESS_DIF", Type.GetType( "System.String" ) )

			' 判定基準
			.Add( "H_BASE", Type.GetType( "System.String" ) )

			' 判定
			.Add( "HANTEI", Type.GetType( "System.String" ) )


		End With



	End Sub


	Public Function setTblZetsuen				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef mes		As DHrec		_
	)	As Integer

		' no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		' dt:絶縁耐圧測定データテーブル
		' mes:測定用設定データ

		Dim ct			As Integer = 0
		Dim dtWorkRow		As System.Data.DataRow



		With mes.dt( no )

			If .t1siz <> 0 Then

				For i = 0 To .t1siz - 1

					For j = 0 To .t1( i ).dsiz - 1

						dtWorkRow		= dt.NewRow

						' 電圧印加場所文字列
						dtWorkRow( "VIN_POS" )	= convVoltInPosToStr( .t1( i ).posv )

						dtWorkRow( "VOLT_IN" )	= .t1( i ).d( j ).volt.ToString( "0V" )

						dtWorkRow( "H_BASE" )	= "-"

						dt.Rows.Add( dtWorkRow )

						ct			+= 1

					Next

				Next

			End If

		End With


		Return ct

	End Function



	Public Function setTblKyuucyaku			_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef mes		As DHrec,		_
		ByVal esc		As Integer		_
	) As Integer

		' no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		' dt:吸着力測定データテーブル
		' mes:測定用設定データ
		' esc:電源動作　0:モノポール　1:ダイポール

		Dim ct			As Integer = 0
		Dim dtWorkRow		As System.Data.DataRow



		With mes.dt( no )

			For i = 0 To .t2.dsiz - 1

				dtWorkRow = dt.NewRow

				' dtWorkRow( "VIN_POS" )	= convVoltInPosToStr( .t2.posv )

				dtWorkRow( "VOLT_IN" )	= .t2.d( i ).volt1.ToString( "0V" )


				If esc = 0 Then

					dtWorkRow( "VOLT_OUT" )		= "-"

				Else

					dtWorkRow( "VOLT_OUT" )		= .t2.d( i ).volt2.ToString( "0V" )

				End If

				'   20201102 s.harada	トーカロで追加
				dtWorkRow( "HE_FLOW" )		= .t2.d( i ).he.ToString( "0." )

				If .t2.d(i).bs <= 0 Then

					dtWorkRow("H_BASE") = "-"

				Else

					'   20201102 s.harada	秒表示に変更
					'dtWorkRow( "H_BASE" )		= ( .t2.d( i ).bs / 1000 ).ToString( "0.00 以上" )
					dtWorkRow( "H_BASE" )		= .t2.d( i ).bs.ToString( "0. 以下" )

				End If


				dt.Rows.Add( dtWorkRow )

				ct += 1

			Next

		End With



		Return ct

	End Function


	Public Function setTblHeGas				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef mes		As DHrec,		_
		ByVal esc		As Integer		_
	)	As Integer

		' no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		' dt:Ｈｅリーク量測定データテーブル
		' mes:測定用設定データ
		' esc:電源動作　0:モノポール　1:ダイポール

		Dim ct			As Integer = 0
		Dim dtWorkRow		As System.Data.DataRow


		With mes.dt( no )

			For i = 0 To .t3.dsiz - 1

				dtWorkRow			= dt.NewRow

				' dtWorkRow( "VIN_POS" )	= convVoltInPosToStr( .t3.posv )

				dtWorkRow( "VOLT_IN" )		= .t3.d( i ).volt1.ToString( "0V" )


				If esc = 0 Then

					dtWorkRow( "VOLT_OUT" )		= "-"

				Else

					dtWorkRow( "VOLT_OUT" )		= .t3.d( i ).volt2.ToString( "0V" )

				End If

				''	20201102 s.harada	トーカロ専用に変更
				'dtWorkRow( "PRESS_BK" ) = ( .t3.d( i ).bp / 1000 ).ToString( "0.00" )


				If .t3.d( i ).bs <= 0 Then

					dtWorkRow( "H_BASE" )		= "-"

				Else

					dtWorkRow( "H_BASE" )		= .t3.d( i ).bs.ToString( "0.0 以下" )

				End If

				'	20201102 s.harada	変定期準２を追加（設定圧力2kpa）
				If .t3.d(i).bs2 <= 0 Then

					dtWorkRow("H_BASE2") = "-"

				Else

					dtWorkRow("H_BASE2") = .t3.d(i).bs2.ToString("0.0 以下")

				End If

				'▼ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）
				dtWorkRow("LEAK") = "-"
				dtWorkRow("LEAK2") = "-"
				dtWorkRow("LEAK3") = "-"
				dtWorkRow("LEAK4") = "-"
				dtWorkRow("LEAK6") = "-"
				For Each ptn In .t3.d(i).ptn
					Select Case ptn
						Case "1"
							dtWorkRow("LEAK") = ""
						Case "2"
							dtWorkRow("LEAK2") = ""
						Case "3"
							dtWorkRow("LEAK3") = ""
						Case "4"
							dtWorkRow("LEAK4") = ""
						Case "6"
							dtWorkRow("LEAK6") = ""
					End Select
				Next
				'▲ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）

				'	20201102 s.harada	判定結果２を追加

				dt.Rows.Add( dtWorkRow )

				ct			+= 1

			Next

		End With



		Return ct

	End Function



	'	20201102 s.harada
	'	トーカロ対応で残留空着を追加
	Public Function setTblZanryu				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef mes		As DHrec,		_
		ByVal esc		As Integer		_
	) As Integer

		' no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		' dt:吸着力測定データテーブル
		' mes:測定用設定データ
		' esc:電源動作　0:モノポール　1:ダイポール

		Dim ct			As Integer = 0
		Dim dtWorkRow		As System.Data.DataRow



		With mes.dt( no )

			For i = 0 To .t4.dsiz - 1

				dtWorkRow			= dt.NewRow

				dtWorkRow( "VOLT_IN" )		= .t4.d( i ).volt1.ToString( "0V" )


				If esc = 0 Then

					dtWorkRow( "VOLT_OUT" )		= "-"

				Else

					dtWorkRow( "VOLT_OUT" )		= .t4.d( i ).volt2.ToString( "0V" )

				End If


				If .t4.d( i ).bs <= 0 Then

					dtWorkRow( "H_BASE" )		= "-"

				Else

					dtWorkRow( "H_BASE" )		= .t4.d( i ).bs.ToString( "0. 以下" )

				End If


				dt.Rows.Add( dtWorkRow )

				ct += 1

			Next

		End With



		Return ct

	End Function



	Public Sub getSMCTemp					_
	(							_
		ByVal no		As Integer,		_
		ByRef tmp1		As Double,		_
		ByRef tmp2		As Double,		_
		ByVal mes		As DHrec		_
	)

		' no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		' tmp1:サ－モチラ－CH1温度
		' tmp2:サ－モチラ－CH2温度
		' mes:測定用設定データ


		With mes.dt( no )

			tmp1			= .tmp( 0 )

			tmp2			= .tmp( 1 )

		End With



	End Sub



	Public Sub tstZetsuDgvDisp				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByVal mes		As DHrec,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox		_
	)


		If setTblZetsuen( no, dt, mes ) > 0 Then

			dgv.DataSource		= dt

			dgv.Refresh()

			grp.Visible		= True


			Dim ttl			As String =				_
				"絶縁耐圧測定　サ－モチラ－：" +			_
				convTempInOutToStr					_
				(							_
					mes.dt( no ).tmp( 0 ),				_
					mes.dt( no ).tmp( 1 ),				_
					mes.dt( no ).schuse				_
				)

			grp.Text		= ttl

		Else

			grp.Visible		= False

		End If


	End Sub



	Public Sub tstKyuuDgvDisp				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByVal mes		As DHrec,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox,		_
		ByVal esc		As Integer		_
	)


		If setTblKyuucyaku( no, dt, mes, esc ) > 0 Then

			dgv.DataSource		= dt

			dgv.Refresh()

			grp.Visible		= True


			Dim ttl			As String =			_
				"吸着力測定　" +					_
				convVoltInPosToStr( mes.dt( no ).t2.posv ) +	_
				"　サ－モチラ－：" +				_
				convTempInOutToStr				_
				(						_
					mes.dt( no ).tmp( 0 ),			_
					mes.dt( no ).tmp( 1 ),			_
					mes.dt( no ).schuse			_
				)

			grp.Text		= ttl

		Else

			grp.Visible		= False

		End If


	End Sub



	Public Sub tstHeGasDgvDisp				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByVal mes		As DHrec,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox,		_
		ByVal esc		As Integer		_
	)


		If setTblHeGas( no, dt, mes, esc ) > 0 Then

			dgv.DataSource		= dt

			dgv.Refresh()

			grp.Visible		= True


			Dim ttl			As String =			_
				"Ｈｅリーク量測定　" +				_
				convVoltInPosToStr( mes.dt( no ).t3.posv ) +	_
				"　サ－モチラ－：" +				_
				convTempInOutToStr				_
				(						_
					mes.dt( no ).tmp( 0 ),			_
					mes.dt( no ).tmp( 1 ),			_
					mes.dt( no ).schuse			_
				)

			grp.Text		= ttl

		Else

			grp.Visible		= False

		End If


	End Sub



	'	20201102 s.harada
	'	トーカロ対応で残留空着を追加
	Public Sub tstZanryuDgvDisp				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByVal mes		As DHrec,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox,		_
		ByVal esc		As Integer		_
	)


		If setTblZanryu( no, dt, mes, esc ) > 0 Then

			dgv.DataSource		= dt

			dgv.Refresh()

			grp.Visible		= True


			Dim ttl			As String =			_
				"残留吸着力測定　" +				_
				convVoltInPosToStr( mes.dt( no ).t4.posv ) +	_
				"　サ－モチラ－：" +				_
				convTempInOutToStr				_
				(						_
					mes.dt( no ).tmp( 0 ),			_
					mes.dt( no ).tmp( 1 ),			_
					mes.dt( no ).schuse			_
				)

			grp.Text			= ttl

		Else

			grp.Visible			= False

		End If


	End Sub



	Public Sub tstZetsuDgvDispClear				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox,		_
		ByVal disp		As Boolean		_
	)


		dgv.DataSource		= dt

		dgv.Refresh()

		grp.Visible		= disp

		grp.Text		= "絶縁耐圧測定"


	End Sub



	Public Sub tstKyuuDgvDispClear				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox,		_
		ByVal disp		As Boolean		_
	)


		dgv.DataSource		= dt

		dgv.Refresh()

		grp.Visible		= disp

		grp.Text		= "吸着力測定"


	End Sub



	Public Sub tstHeGasDgvDispClear				_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox,		_
		ByVal disp		As Boolean		_
	)


		dgv.DataSource		= dt

		dgv.Refresh()

		grp.Visible		= disp

		grp.Text		= "Ｈｅリーク量測定"


	End Sub


	'	20201102 s.harada
	'	トーカロ対応で残留空着を追加
	Public Sub tstZanryuDgvDispClear			_
	(							_
		ByVal no		As Integer,		_
		ByRef dt		As DataTable,		_
		ByRef dgv		As DataGridView,	_
		ByRef grp		As GroupBox,		_
		ByVal disp		As Boolean		_
	)


		dgv.DataSource		= dt

		dgv.Refresh()

		grp.Visible		= disp

		grp.Text		= "残留吸着力測定"


	End Sub



#End Region


#Region "入力データテーブル"



	'*****
	'	試験項目用データテーブルの設定
	'*****
	Public Sub createTblTest				_
	(							_
		ByRef dt		As DataTable		_
	)

		' dt:データテーブル


		' データテーブルに項目を設定する
		With dt.Columns


			' 電圧印加箇所
			.Add( "PV", Type.GetType( "System.Int32" ) )

			' 絶縁耐圧印加電圧
			.Add( "IV", Type.GetType( "System.Double" ) )

			' 吸着印加電圧
			.Add( "KV", Type.GetType( "System.Double" ) )

			' Heリーク印加電圧
			.Add( "LV", Type.GetType( "System.Double" ) )

		'	20200716 s.harada　トーカロ対応で追加
			' 吸着印加電圧
			.Add( "ZV", Type.GetType( "System.Double" ) )

			' 電圧印加箇所
			.Add( "VIN_POS", Type.GetType( "System.String" ) )

			' 絶縁耐圧印加電圧
			.Add( "ISO_VOLT", Type.GetType( "System.String" ) )

			' 吸着印加電圧 CH1
			.Add( "KYU_VOLT1", Type.GetType( "System.String" ) )

			' 吸着印加電圧 CH2
			.Add( "KYU_VOLT2", Type.GetType( "System.String" ) )

		'	20200716 s.harada　トーカロ対応で追加
			' 吸着He流量
			.Add( "KYU_HE", Type.GetType( "System.String" ) )

			' 吸着判定基準
			.Add( "KYU_BASE", Type.GetType( "System.String" ) )

			' Heリーク印加電圧 CH1
			.Add( "LEK_VOLT1", Type.GetType( "System.String" ) )

			' Heリーク印加電圧 CH2
			.Add("LEK_VOLT2", Type.GetType("System.String"))

			'▼ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）
			.Add("LEK_PTN", Type.GetType("System.String"))
			'▲ 2024.05.02 TC Kanda （測定有効無効パラメータ追加）

			'	20201102 s.harada	検査方法変更で削除
			'	20200716 s.harada　トーカロ対応で追加
			' Heリーク裏面圧
			'.Add( "LEK_BP", Type.GetType( "System.String" ) )

			' Heリーク判定基準
			.Add( "LEK_BASE", Type.GetType( "System.String" ) )

			'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
			.Add( "LEK_BASE2", Type.GetType( "System.String" ) )

		'	20200716 s.harada　トーカロ対応で追加
			' 残留吸着印加電圧 CH1
			.Add( "ZKU_VOLT1", Type.GetType( "System.String" ) )

		'	20200716 s.harada　トーカロ対応で追加
			' 残留吸着印加電圧 CH2
			.Add( "ZKU_VOLT2", Type.GetType( "System.String" ) )

		'	20200716 s.harada　トーカロ対応で追加
			' 残留吸着判定基準
			.Add( "ZKU_BASE", Type.GetType( "System.String" ) )

			' インデックス
			.Add( "IDX", Type.GetType( "System.Int32" ) )


		End With



	End Sub

	'*****
	'	測定データを表示データテーブルに設定する
	'*****
	Public Sub setTblTest					_
	(							_
		ByRef dt		As DataTable,		_
		ByVal mes		As DTREC		_
	)

		' dt:データテーブル
		' mes:設定データ

		Dim dtWorkRow		As System.Data.DataRow
		Dim i			As Integer
		Dim j			As Integer
		Dim No			As Integer
		Dim dsiz		As Integer



		With mes

			' 電圧印加位置毎にDB設定
			For No = 0 To CON_POLE_MAX

				' サイズの最大値を取り出す
				dsiz			= 0

				' 絶縁抵抗
				If .t1siz > 0 Then

					For i = 0 To .t1siz - 1

						If .t1( i ).posv = No Then

							dsiz			= .t1( i ).dsiz

							Exit For

						End If

					Next

				End If


				' 吸着
				If .t2.posv = No Then

					If dsiz < .t2.dsiz Then

						dsiz			= .t2.dsiz

					End If

				End If


				' Heリーク量
				If .t3.posv = No Then

					If dsiz < .t3.dsiz Then

						dsiz			= .t3.dsiz

					End If

				End If


				' 20201102 残留吸着力の設定漏れを追加
				If .t4.posv = No Then

					If dsiz < .t4.dsiz Then

						dsiz			= .t4.dsiz

					End If

				End If

				' サイズ分のデータをデータテーブルにセット
				For j = 0 To dsiz - 1

					dtWorkRow		= dt.NewRow

					dtWorkRow( "PV" )	= No

					' 電圧印加場所文字列
					dtWorkRow( "VIN_POS" )	= convVoltInPosToStr( No )


					' 絶縁耐圧
					If i < .t1siz Then

						If j < .t1(i).dsiz Then

							dtWorkRow( "IV" )		= .t1( i ).d( j ).volt

							dtWorkRow( "ISO_VOLT" )		= .t1( i ).d( j ).volt.ToString

						Else

							dtWorkRow( "IV" )		= 0

							dtWorkRow( "ISO_VOLT" )		= "-"

						End If

					Else

						dtWorkRow( "IV" )		= 0

						dtWorkRow( "ISO_VOLT" )		= "-"

					End If


					' 吸着
					If .t2.posv = No Then

						If j < .t2.dsiz Then

							dtWorkRow( "KV" )		= Math.Abs( .t2.d( j ).volt1 )

							dtWorkRow( "KYU_VOLT1" )	= .t2.d( j ).volt1.ToString

							dtWorkRow( "KYU_VOLT2" )	= .t2.d( j ).volt2.ToString

							' ウエハ吸着力測定･判定値(表示はKPa単位)
							' 20201102 s.harada	判定値を時間に変更
							'dtWorkRow( "KYU_BASE" )		= ( .t2.d( j ).bs / 1000.0 ).ToString( "F2" )
							If .t2.d( j ).bs > 0 Then

								dtWorkRow( "KYU_BASE" )		= .t2.d( j ).bs.ToString

							Else

								dtWorkRow( "KYU_BASE" )		= " "

							End If


							' 20201102 s.harada	AQTC対応
							'	ヘリウム流量
							dtWorkRow( "KYU_HE" )		= .t2.d( j ).he.ToString

						Else

							dtWorkRow( "KV" )		= 0

							dtWorkRow( "KYU_VOLT1" )	= "-"

							dtWorkRow( "KYU_VOLT2" )	= "-"

							' ウエハ吸着力測定･判定値(表示はKPa単位)
							dtWorkRow( "KYU_BASE" )		= "-"

							' 20201102 s.harada	AQTC対応
							'	ヘリウム流量
							dtWorkRow( "KYU_HE" )		= "-"

						End If

					Else

						dtWorkRow( "KV" )		= 0

						dtWorkRow( "KYU_VOLT1" )	= "-"

						dtWorkRow( "KYU_VOLT2" )	= "-"

						' ウエハ吸着力測定･判定値(表示はKPa単位)
						dtWorkRow( "KYU_BASE" )		= "-"

						' 20201102 s.harada	AQTC対応
						'	ヘリウム流量
						dtWorkRow( "KYU_HE" )		= "-"


					End If


					' Heリーク量
					If .t3.posv = No Then

						If j < .t3.dsiz Then

							dtWorkRow( "LV" )		= Math.Abs( .t3.d( j ).volt1 )

							dtWorkRow( "LEK_VOLT1" )	= .t3.d( j ).volt1.ToString

							dtWorkRow( "LEK_VOLT2" )	= .t3.d( j ).volt2.ToString

							' 20201102 s.harada	判定なしをスペース表示
							’			判定値２（2kpa)を追加
							dtWorkRow( "LEK_BASE" )		= .t3.d( j ).bs.ToString( "0.0" )
							If .t3.d( j ).bs > 0 Then

								dtWorkRow( "LEK_BASE" )		= .t3.d( j ).bs.ToString( "0.0" )

							Else

								dtWorkRow( "LEK_BASE" )		= " "

							End If
							dtWorkRow( "LEK_BASE2" )		= .t3.d( j ).bs2.ToString( "0.0" )
							If .t3.d(j).bs2 > 0 Then

								dtWorkRow("LEK_BASE2") = .t3.d(j).bs2.ToString("0.0")

							Else

								dtWorkRow("LEK_BASE2") = " "

							End If
							'▼2024.05.02 TC Kanda (測定有効無効パラメータ追加)
							dtWorkRow("LEK_PTN") = String.Join(",", .t3.d(j).ptn)
							If dtWorkRow("LEK_PTN") = "" Then
								dtWorkRow("LEK_PTN") = "-"
							End If
							'▲2024.05.02 TC Kanda (測定有効無効パラメータ追加)

						Else

							dtWorkRow( "LV" )		= 0

							dtWorkRow( "LEK_VOLT1" )	= "-"

							dtWorkRow( "LEK_VOLT2" )	= "-"

							dtWorkRow( "LEK_BASE" )		= "-"

							'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
							dtWorkRow( "LEK_BASE2" )	= "-"

							'▼2024.05.02 TC Kanda (測定有効無効パラメータ追加)
							dtWorkRow("LEK_PTN") = "-"
							'▲2024.05.02 TC Kanda (測定有効無効パラメータ追加)
						End If

					Else

						dtWorkRow( "LV" )		= 0

						dtWorkRow( "LEK_VOLT1" )	= "-"

						dtWorkRow( "LEK_VOLT2" )	= "-"

						dtWorkRow( "LEK_BASE" )		= "-"

						'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
						dtWorkRow( "LEK_BASE2" )	= "-"

						'▼2024.05.02 TC Kanda (測定有効無効パラメータ追加)
						dtWorkRow("LEK_PTN") = "-"
						'▲2024.05.02 TC Kanda (測定有効無効パラメータ追加)
					End If


					' 20201102 s.harada	AQTC対応
					' 残留吸着
					If .t4.posv = No Then

						If j < .t4.dsiz Then

							dtWorkRow( "ZV" )		= Math.Abs (.t4.d( j ).volt1 )

							dtWorkRow( "ZKU_VOLT1" )	= .t4.d( j ).volt1.ToString

							dtWorkRow( "ZKU_VOLT2" )	= .t4.d( j ).volt2.ToString

							'　残留吸着力測定･判定値
							If .t4.d( j ).bs > 0 Then

								dtWorkRow( "ZKU_BASE" )		= .t4.d( j ).bs .ToString

							Else

								dtWorkRow( "ZKU_BASE" )		= " "

							End If

						Else

							dtWorkRow( "ZV" )		= 0

							dtWorkRow( "ZKU_VOLT1" )	= "-"

							dtWorkRow( "ZKU_VOLT2" )	= "-"

							' 残留吸着力測定･判定値
							dtWorkRow( "ZKU_BASE" )		= "-"


						End If

					Else

						dtWorkRow( "ZV" )		= 0

						dtWorkRow( "ZKU_VOLT1" )	= "-"

						dtWorkRow( "ZKU_VOLT2" )	= "-"

						' 残留吸着力測定･判定値
						dtWorkRow( "ZKU_BASE" )		= "-"


					End If



					dt.Rows.Add( dtWorkRow )

				Next

			Next


		End With

	End Sub



#End Region



End Module
