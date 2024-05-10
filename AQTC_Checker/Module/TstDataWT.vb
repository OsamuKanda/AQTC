
Imports System.IO


Module TstDataWT



	'*****
	'	MESrecのデータを保存
	'*****
	Public Function SaveTestData			_
	(						_
		ByVal agFName		As String	_
	)	As Integer


		Dim ret			As Integer = -1
		Dim TextFile		As IO.StreamWriter
		Dim Buff		As String = ""
		Dim FileName		As String



		FileName =
			Directory.GetCurrentDirectory() +
			"\test\" +
			agFName +
			".dat"

		Try

			' ファイルをオープン
			TextFile		=				_
				New IO.StreamWriter				_
				(						_
					New IO.FileStream			_
					(					_
						FileName, IO.FileMode.Create	_
					),					_
					System.Text.Encoding.Default		_
				)

		Catch ex As Exception

			WriteLog( "", "LG", "SaveTestData File=" + FileName + " " + ex.Message )

			Return ret

		End Try


		Try

			Buff			= "検査パラメータ"

			TextFile.WriteLine( Buff )


			Buff			=					_
				"0,1," +						_
				ESCmd.ToString +					_
				",SDC電源動作種別　0:モノポール　1:ダイポール"

			TextFile.WriteLine( Buff )


			Buff			=					_
				"0,2," +						_
				( BPRS / 1000.0 ).ToString( "0.00" ) +			_
				",ウエハ吸着力リミット値（Ｋｐａ）"

			TextFile.WriteLine( Buff )


			Buff			=					_
				"0,3," +						_
				( HLPRS / 1000.0 ).ToString( "0.00" ) +			_
				",Ｈｅリーク測定時のウエハ裏面圧力（Ｋｐａ）"

			TextFile.WriteLine( Buff )


			Buff			=					_
				"0,4," +						_
				SCRUSE.ToString +					_
				",ＳＭＣサ－モチラ－使用　0:CH1低温用　1:CH2高温用　2:両方"

			TextFile.WriteLine( Buff )


			Buff			=					_
				"0,5," +						_
				TPRS1.ToString( "0.00" ) +				_
				",低温時真空圧（Ｐａ）"

			TextFile.WriteLine( Buff )



			Buff			=				_
				"0,6," +					_
				TPRS2.ToString( "0.00" ) +			_
				",高温時真空圧（Ｐａ）"

			TextFile.WriteLine( Buff )


			Buff			=			_
				"0,7," +				_
				BakPres.ToString( "0.00" ) +		_
				",吸着停止裏面圧（Ｐａ）"

			TextFile.WriteLine( Buff )


			'
			'	20140127 y.goto
			'	電極ヘッド温度安定待ち時間 (分)
			'
			Buff			=			_
				"0,8," +				_
				PrmTmpStbW.ToString( "0." ) +		_
				",電極ヘッド温度安定待ち時間（分）"

			TextFile.WriteLine( Buff )


			Dim i			As Integer
			Dim j			As Integer
			Dim k			As Integer


			For i = 0 To 3

				' 1行送り
				TextFile.WriteLine( "" )


				Select Case	i

				Case	0

					Buff			= "試験１　大気"

				Case	1

					Buff			= "試験２　真空"

				Case	2

					Buff			= "試験３　真空"

				Case Else

					Buff			= "試験４　真空"

				End Select


				TextFile.WriteLine( Buff )


				If					_
					MESrec.dt( i ).t1siz > 0 Or	_
					MESrec.dt( i ).t2.dsiz > 0 Or	_
					MESrec.dt( i ).t3.dsiz > 0	_
				Then

					Buff		= ( i + 1 ).ToString + ",0"

					Buff		+= "," + MESrec.dt( i ).tmp( 0 ).ToString( "0.0" )

					Buff		+= "," + MESrec.dt( i ).tmp( 1 ).ToString( "0.0" )

					Buff		+= ",サ－モチラ－温度（℃）　ＣＨ１-ＣＨ２"

					TextFile.WriteLine( Buff )


					For j = 0 To MESrec.dt( i ).t1siz - 1

						With MESrec.dt( i ).t1( j )

							Buff			=		_
								( i + 1 ).ToString +		_
								",1," +				_
								.posv.ToString +		_
								",電圧印可箇所 " +		_
								convVoltInPosToStr( .posv )

							TextFile.WriteLine( Buff )


							For k = 0 To .dsiz - 1

								Buff			=		_
									( i + 1 ).ToString +		_
									",2," +				_
									.d( k ).volt.ToString +		_
									",絶縁耐圧　印可電圧（Ｖ）"

								TextFile.WriteLine(Buff)

							Next

						End With

					Next



					With MESrec.dt( i ).t2

						If .dsiz > 0 Then

							Buff			=		_
								( i + 1 ).ToString +		_
								",1," +				_
								.posv.ToString +		_
								",電圧印可箇所 " +		_
								convVoltInPosToStr( .posv )

							TextFile.WriteLine( Buff )


							For j = 0 To .dsiz - 1

								Buff			= ( i + 1 ).ToString + ",3"

								Buff			+= "," + .d( j ).volt1.ToString

								Buff			+= "," + .d( j ).volt2.ToString

								Buff			+= "," + ( .d( j ).bs / 1000.0 ).ToString( "0.00" )

								Buff			+= ",吸着力 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（kPa以上）"

								TextFile.WriteLine( Buff )

							Next

						End If

					End With


					With MESrec.dt( i ).t3

						If .dsiz > 0 Then

							Buff			=			_
								( i + 1 ).ToString +			_
								",1," +					_
								.posv.ToString +			_
								",電圧印可箇所 " +			_
								convVoltInPosToStr( .posv )

							TextFile.WriteLine( Buff )


							For j = 0 To .dsiz - 1

								Buff			= ( i + 1 ).ToString + ",4"

								Buff			+= "," + .d( j ).volt1.ToString

								Buff			+= "," + .d( j ).volt2.ToString

								Buff			+= "," + .d( j ).bs.ToString( "0.0" )

								Buff			+= ",Ｈｅリーク量 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（ml/min以下）"

								TextFile.WriteLine(Buff)

							Next

						End If

					End With

				End If

			Next


			ret		= 0

		Catch ex As Exception

			WriteLog( "", "LG", "SaveTestData Buff=" + Buff + " " + ex.Message )

		Finally

			TextFile.Close()

		End Try



		Return ret


	End Function

	
	Public Function SaveParameter(ByVal agFName As String) As Integer

		Dim ret			As Integer = -1
		Dim TextFile		As IO.StreamWriter
		Dim Buff		As String = ""
		Dim FileName		As String


		' 20201102 s.harada	AQTC対応でフォルダ変更
		'FileName		= Directory.GetCurrentDirectory() + "\test\" + agFName + ".dat"
		FileName		= Directory.GetCurrentDirectory() + "\testTcl\" + agFName + ".dat"


		Try

			' ファイルをオープン
			TextFile		= New IO.StreamWriter	_
			(						_
				New IO.FileStream			_
				(					_
					FileName,			_
					IO.FileMode.Create		_
				),					_
				System.Text.Encoding.Default		_
			)


		Catch ex As Exception

			WriteLog( "", "LG", "SaveParameter File=" + FileName + " " + ex.Message )

			Return ret

		End Try


		Try

			Buff			= "検査パラメータ"

			TextFile.WriteLine( Buff )


			Buff			=		_
				"0,1," +			_
				ESCmd.ToString +		_
				",ESC-2000動作種別　0:モノポール　1:ダイポール"

			TextFile.WriteLine( Buff )


			' 20201102 s.harada	AQTC対応で削除
			'Buff			=				_
			'	"0,2," +					_
			'	( BPRS / 1000.0 ).ToString( "0.00" ) +		_
			'	",ウエハ吸着力リミット値（Ｋｐａ）"

			'TextFile.WriteLine( Buff )


			' 20201102 s.harada	AQTC対応で削除
			'Buff			=				_
			'	"0,3," +					_
			'	( HLPRS / 1000.0 ).ToString( "0.00" ) +		_
			'	",Ｈｅリーク測定時のウエハ裏面圧力（Ｋｐａ）"

			'TextFile.WriteLine( Buff )


			Buff			=				_
				"0,4," +					_
				SCRUSE.ToString +				_
				",ＳＭＣサ－モチラ－使用　0:CH1低温用　1:CH2高温用　2:両方"

			TextFile.WriteLine( Buff )


			Buff			=				_
				"0,5," +					_
				TPRS1.ToString( "0.00" ) +			_
				",低温時真空圧（Ｐａ）"

			TextFile.WriteLine( Buff )


			Buff			=				_
				"0,6," +					_
				TPRS2.ToString( "0.00" ) +			_
				",高温時真空圧（Ｐａ）"

			TextFile.WriteLine( Buff )


			Buff			=				_
				"0,7," +					_
				BakPres.ToString( "0.00" ) +			_
				",吸着停止裏面圧（Ｐａ）"

			TextFile.WriteLine( Buff )


			'
			'	20140127 y.goto
			'	電極ヘッド温度安定待ち時間 (分)
			'
			Buff			=				_
				"0,8," +					_
				PrmTmpStbW.ToString( "0." ) +			_
				",電極ヘッド温度安定待ち時間（分）"

			TextFile.WriteLine( Buff )


			' 20201102 s.harada	AQTC対応で追加
			Buff =
				"0,9," +
				PrmHeFlow.ToString( "0." ) +
				",残留吸着ヘリウム流量（sccm）"

			TextFile.WriteLine( Buff )

			' 20201102 s.harada	AQTC対応で追加
			Buff =
				"0,10," +
				PrmVoltImp.ToString( "0." ) +
				",残留吸着電圧印加時間（秒)"

			TextFile.WriteLine( Buff )

			' 20201102 s.harada	AQTC対応で追加
			Buff =
				"0,11," +
				PrmHeWait.ToString( "0." ) +
				",残留吸着電圧印加停止後Ｈｅ流すまでの待ち時間（秒）"

			TextFile.WriteLine( Buff )

			' 20201102 s.harada	AQTC対応で追加
			Buff =
				"0,12," +
				PrmMaxTim.ToString( "0." ) +
				",吸着力測定時間上限（秒)"

			TextFile.WriteLine( Buff )

			' 20201102 s.harada	AQTC対応で追加
			Buff =
				"0,13," +
				PrmStabVct.ToString( "0.0" ) +
				",残留吸着 安定判断傾き"

			TextFile.WriteLine( Buff )

			' 20201102 s.harada	AQTC対応で追加
			Buff =
				"0,14," +
				PrmStabTim.ToString( "0." ) +
				",残留吸着 安定判断時間(秒)"

			TextFile.WriteLine( Buff )

			' 20201124 y.goto
			' 残留吸着電圧印可時裏面圧力 (Pa)
			Buff	=
				"0,15," +
				PrmBakPrs.ToString( "0." ) +
				",残留吸着電圧印可時裏面圧力 (Pa)"

			TextFile.WriteLine( Buff )

			ret		= 0

		Catch ex As Exception

			WriteLog( "", "LG", "SaveParameter Buff=" + Buff + " " + ex.Message )

			MessageBox.Show( ex.Message )

		Finally

			TextFile.Close()

		End Try


		Return ret

	End Function


	'*****
	'	ソートできないため未使用
	'*****
	Public Function SaveTestDt(ByVal agFName As String, ByVal tmp1 As String, ByVal tmp2 As String, ByVal dt As DataTable, ByVal Sel As Integer) As Integer

		Dim ret As Integer = -1
		Dim TextFile As IO.StreamWriter
		Dim Buff As String = ""
		Dim FileName As String



		'データなし
		If dt.Rows.Count = 0 Then

			Return 0

		End If

		FileName = Directory.GetCurrentDirectory() + "\test\" + agFName + ".dat"


		Try

			'ファイルをオープン
			TextFile = New IO.StreamWriter(FileName, True, System.Text.Encoding.Default)

		Catch ex As Exception

			WriteLog("", "LG", "SaveTestDt File=" + FileName + " " + ex.Message)
			Return ret

		End Try


		Try

			Dim pos As Integer
			Dim bpos As Integer
			Dim i As Integer


			'1行送り
			TextFile.WriteLine("")

			Select Case Sel
				Case 0
					Buff = "試験１　大気"
				Case 1
					Buff = "試験２　真空"
				Case 2
					Buff = "試験３　真空"
				Case Else
					Buff = "試験４　真空"
			End Select

			TextFile.WriteLine(Buff)

			Buff = (Sel + 1).ToString + ",0"

			If tmp1 <> "" And IsNumeric(tmp1) Then
				Buff += "," + tmp1
			Else
				Buff += "," + "0.0"
			End If

			If tmp2 <> "" And IsNumeric(tmp2) Then
				Buff += "," + tmp2
			Else
				Buff += "," + "0.0"
			End If

			Buff += ",サ－モチラ－温度（℃）　ＣＨ１-ＣＨ２"
			TextFile.WriteLine(Buff)

			'絶縁耐圧
			bpos = -1

			For i = 0 To dt.Rows.Count - 1

				If dt.Rows(i)("IV") > 0 Then

					pos = dt.Rows(i)("PV")

					If pos <> bpos Then

						Buff = (Sel + 1).ToString + ",1," + pos.ToString + ",電圧印可箇所 " + convVoltInPosToStr(pos)

						TextFile.WriteLine(Buff)

						bpos = pos

					End If

					Buff = (Sel + 1).ToString + ",2," + dt.Rows(i)("ISO_VOLT") + ",絶縁耐圧　印可電圧（Ｖ）"

					TextFile.WriteLine(Buff)

				End If

			Next


			'吸着力
			bpos = -1

			For i = 0 To dt.Rows.Count - 1

				If dt.Rows(i)("KV") > 0 Then

					pos = dt.Rows(i)("PV")

					If pos <> bpos Then

						Buff = (Sel + 1).ToString + ",1," + pos.ToString + ",電圧印可箇所 " + convVoltInPosToStr(pos)

						TextFile.WriteLine(Buff)

						bpos = pos

					End If

					Buff = (Sel + 1).ToString + ",3"
					Buff += "," + dt.Rows(i)("KYU_VOLT1")
					Buff += "," + dt.Rows(i)("KYU_VOLT2")
					Buff += "," + dt.Rows(i)("KYU_BASE")
					Buff += ",吸着力 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（kPa以上）"
					TextFile.WriteLine(Buff)
				End If
			Next


			'Ｈｅリーク量
			bpos = -1

			For i = 0 To dt.Rows.Count - 1

				If dt.Rows(i)("LV") > 0 Then

					pos = dt.Rows(i)("PV")

					If pos <> bpos Then

						Buff = (Sel + 1).ToString + ",1," + pos.ToString + ",電圧印可箇所 " + convVoltInPosToStr(pos)

						TextFile.WriteLine(Buff)

						bpos = pos

					End If

					Buff = (Sel + 1).ToString + ",4"

					Buff += "," + dt.Rows(i)("LEK_VOLT1")

					Buff += "," + dt.Rows(i)("LEK_VOLT2")

					Buff += "," + dt.Rows(i)("LEK_BASE")

					Buff += ",Ｈｅリーク量 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（ml/min以下）"

					TextFile.WriteLine(Buff)

				End If

			Next

			ret = 0

		Catch ex As Exception

			WriteLog("", "LG", "SaveTestDt Buff=" + Buff + " " + ex.Message)

			MessageBox.Show(ex.Message)

		Finally

			TextFile.Close()

		End Try



		Return ret

	End Function



	'*****
	'	ソートしたビューを保存
	'*****
	'
	'   20200716 s.harada
	'   トーカロ対応
	'
	'	20201102 s.harada	AQTC対応専用に変更
	Public Function SaveTestDtView				_
	(							_
		ByVal agFName		As String,		_
		ByVal ch1use		As Integer,		_
		ByVal tmp1		As String,		_
		ByVal ch2use		As Integer,		_
		ByVal tmp2		As String,		_
		ByVal tmpchg		As Integer,		_
		ByVal dt		As DataTable,		_
		ByVal Sel		As Integer		_
	) As Integer


		Dim ret			As Integer = -1
		Dim TextFile		As IO.StreamWriter
		Dim Buff		As String = ""
		Dim FileName		As String



		' データなし
		If dt.Rows.Count = 0 Then

			Return 0

		End If


		'
		'	20201102 s.harada	AQTC対応用にフォルダ名変更
		'FileName		= Directory.GetCurrentDirectory() + "\test\" + agFName + ".dat"

		FileName		= Directory.GetCurrentDirectory() + "\testTcl\" + agFName + ".dat"


		Try

			'ファイルをオープン
			TextFile		= New IO.StreamWriter		_
			(							_
				FileName,					_
				True,						_
				System.Text.Encoding.Default			_
			)

		Catch ex As Exception

			WriteLog( "", "LG", "SaveTestDt File=" + FileName + " " + ex.Message )

			Return ret

		End Try


		Try

			Dim pos			As Integer
			Dim bpos		As Integer
			' Dim i			As Integer


			' 1行送り
			TextFile.WriteLine("")


			Select Case	Sel

				Case 0

					Buff = "試験１　大気"

				Case 1

					Buff = "試験２　真空"

				Case 2

					Buff = "試験３　真空"

				Case Else

					Buff = "試験４　真空"

			End Select

			TextFile.WriteLine( Buff )



			'
			'	サーモチラー設定情報記述行出力
			'
			Buff			= ( Sel + 1 ).ToString + ",0"


			' CH1使用フラグ
			Buff			+= "," + ch1use.ToString()

			' CH1設定温度
			If tmp1 <> "" And IsNumeric( tmp1 ) Then

				Buff			+= "," + tmp1

			Else

				Buff			+= "," + "0.0"

			End If

			' CH2使用フラグ
			Buff			+= "," + ch2use.ToString()


			' CH2設定温度
			If tmp2 <> "" And IsNumeric( tmp2 ) Then

				Buff			+= "," + tmp2

			Else

				Buff			+= "," + "0.0"

			End If

			' 配管接続有無フラグ
			Buff			+= "," + tmpchg.ToString()


			Buff			+= ",サ－モチラ－CH1使用ﾌﾗｸﾞ, CH1温度, CH2使用ﾌﾗｸﾞ, CH2温度, 配管接続変更有無"

			TextFile.WriteLine(Buff)



			'
			'	絶縁耐圧
			'
			bpos			= -1


			Dim view		As DataView = dt.DefaultView
			Dim rowView		As DataRowView



			For Each rowView In view

				If rowView.Item( "IV" ) > 0 Then

					pos			= rowView.Item( "PV" )

					If pos <> bpos Then

						Buff			=		_
							( Sel + 1 ).ToString +		_
							",1," +				_
							pos.ToString +			_
							",電圧印可箇所 " +		_
							convVoltInPosToStr( pos )

						TextFile.WriteLine( Buff )

						bpos			= pos

					End If


					Buff			=		_
						( Sel + 1 ).ToString +		_
						",2," +				_
						rowView.Item( "ISO_VOLT" ) +	_
						",絶縁耐圧　印可電圧（Ｖ）"

					TextFile.WriteLine( Buff )


				End If

			Next


			'
			'	吸着力
			'
			bpos			= -1

			view			= dt.DefaultView

			For Each rowView In view

				If rowView.Item( "KV" ) > 0 Then

					pos			= rowView.Item( "PV" )

					If pos <> bpos Then

						Buff			=		_
							( Sel + 1 ).ToString +		_
							",1," +				_
							pos.ToString +			_
							",電圧印可箇所 " +		_
							convVoltInPosToStr( pos )

						TextFile.WriteLine( Buff )

						bpos			= pos

					End If

					Buff			= ( Sel + 1 ).ToString + ",3"

					Buff			+= "," + rowView.Item( "KYU_VOLT1" )

					Buff			+= "," + rowView.Item( "KYU_VOLT2" )

					'	20201102 s.harada	判定値を５００Vのみ固定値に設定
					'Buff			+= "," + rowView.Item( "KYU_BASE" )
					If IsNumeric( rowView.Item( "KYU_VOLT1" ) )  AndAlso CInt( rowView.Item( "KYU_VOLT1" ) ) = 500 Then
					
						Buff			+= ",120" 

					Else

						Buff			+= ",0" 

					End If

					'	20201102 s.harada	AQTC対応用にHe流量追加変更
					'Buff			+= ",吸着力 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（kPa以上）"
					Buff			+= "," + rowView.Item( "KYU_HE" )

					Buff			+= ",吸着力 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（秒以下）・ヘリウム流量"


					TextFile.WriteLine( Buff )

				End If

			Next



			'
			'	Ｈｅリーク量
			'
			bpos			= -1

			view			= dt.DefaultView

			For Each rowView In view

				If rowView.Item( "LV" ) > 0 Then

					pos			= rowView.Item( "PV" )

					If pos <> bpos Then

						Buff			=		_
							( Sel + 1 ).ToString +		_
							",1," +				_
							pos.ToString +			_
							",電圧印可箇所 " +		_
							convVoltInPosToStr( pos )

						TextFile.WriteLine( Buff )

						bpos			= pos

					End If


					Buff			= ( Sel + 1 ).ToString + ",4"

					Buff			+= "," + rowView.Item( "LEK_VOLT1" )

					Buff			+= "," + rowView.Item( "LEK_VOLT2" )

					'	20201102 s.harada	判定値を５００Vのみ固定値に設定
					'Buff			+= "," + rowView.Item( "LEK_BASE" )

					'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
					'Buff			+= "," + rowView.Item( "LEK_BASE2" )
					If IsNumeric( rowView.Item( "LEK_VOLT1" ) )  AndAlso CInt( rowView.Item( "LEK_VOLT1" ) ) = 500 Then
					
						Buff			+= ",1.0"	'1kPa
						Buff			+= ",1.5"	'2kPa

					Else

						Buff			+= ",0"		'1kPa
						Buff			+= ",0"		'2kPa

					End If

					'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
					Buff += "," + String.Join("|", rowView.Item("LEK_PTN").ToString.Split(","))
					'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

					'	20201102 s.harada	Ｈｅリーク測定・判定値(2kPa)　追加
					'Buff			+= ",Ｈｅリーク量 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（ml/min以下）"
					'▼2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）
					'Buff += ",Ｈｅリーク量 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値1kPa（sccm以下）・判定値2kPa（sccm以下）"
					Buff += ",Ｈｅリーク量 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値1kPa（sccm以下）・判定値2kPa（sccm以下）・測定圧"
					'▲2024.05.02 TC Kanda （３．Ｈｅリーク量測定のパターン追加／測定有効無効パラメータ追加）

					TextFile.WriteLine( Buff )

				End If

			Next


			'
			'	20201102 s.harada	追加
			'
			'	残留吸着力
			'
			bpos			= -1

			view			= dt.DefaultView

			For Each rowView In view

				If rowView.Item( "ZV" ) > 0 Then

					pos			= rowView.Item( "PV" )

					If pos <> bpos Then

						Buff			=		_
						( Sel + 1 ).ToString +			_
						",1," +					_
						pos.ToString +				_
						",電圧印可箇所 " +			_
						convVoltInPosToStr( pos )

						TextFile.WriteLine( Buff )

						bpos			= pos

					End If

					Buff			= ( Sel + 1 ).ToString + ",5"

					Buff			+= "," + rowView.Item( "ZKU_VOLT1" )

					Buff			+= "," + rowView.Item( "ZKU_VOLT2" )

					'	判定値を５００Vのみ固定値に設定
					'Buff			+= "," + rowView.Item( "ZKU_BASE" )
					If IsNumeric( rowView.Item( "ZKU_VOLT1" ) )  AndAlso CInt( rowView.Item( "ZKU_VOLT1" ) ) = 500 Then
					
						Buff			+= ",300" 

					Else

						Buff			+= ",0" 

					End If

					Buff			+= ",残留吸着力 印可電圧（Ｖ）ＣＨ１・ＣＨ２・判定値（Pa以上）"

					TextFile.WriteLine( Buff )

				End If

			Next


			ret			= 0


		Catch ex As Exception

			WriteLog( "", "LG", "SaveTestDt Buff=" + Buff + " " + ex.Message )

			MessageBox.Show(ex.Message)

		Finally

			TextFile.Close()

		End Try



		Return ret

	End Function



End Module
