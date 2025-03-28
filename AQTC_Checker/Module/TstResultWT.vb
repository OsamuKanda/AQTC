

Imports System.IO



Module TstResultWT


	'
	'	測定データファイル名（パス、拡張子を含まない）
	'
	Public TstRstFname	As String

	'
	'	測定データファイル、波形記録ファイル保存フォルダパス
	'
	Public TstRstFolpth	As String

	'
	'	測定データファイルパス
	'
	Public TstRstFpath	As String



	'*****
	'	パスを含む測定データフォルダ名を作成
	'*****
	Public Function MkFldName				_
	(							_
		ByVal fname		As String		_
	)	As String

		Dim fpth		As String


		fpth			= Directory.GetCurrentDirectory() + "\data\" + fname


		return fpth

	End Function



	'*****
	'	パスを含む測定データファイル名を作成
	'*****
	Public Function MkDatFName				_
	(							_
		ByVal fname		As String		_
	)	As String

		Dim fldpth		As String
		Dim filpth		As String


		' パスを含む測定データフォルダ名を作成
		fldpth			= MkFldName( fname )

		filpth			= fldpth + "\" + fname + ".csv"


		return filpth

	End Function



	Public Function CheckDataFile				_
	(							_
		ByVal agFName		As String		_
	)	As Integer

		Dim FileName		As String



		' パスを含む測定データファイル名を作成
		FileName		= MkDatFName( agFName )



		Return File.Exists( FileName )

	End Function



	'*****
	'	試験結果をファイルに書出す
	'*****
	Public Function SaveResultData() As Integer

		Dim ret As Integer = -1
		Dim TextFile As IO.StreamWriter
		Dim Buff As String = ""



		Try

			'
			'	データ格納フォルダの作成
			'
			If Dir(TstRstFolpth, vbDirectory) = "" Then

				MkDir(TstRstFolpth)

			End If


			' ファイルをオープン
			TextFile = New IO.StreamWriter(New IO.FileStream(TstRstFpath, IO.FileMode.Create), System.Text.Encoding.UTF8)

		Catch ex As Exception

			WriteLog("", "LG", "SaveResultData File=" + TstRstFpath + " " + ex.Message)

			Return ret

		End Try


		Try

			'
			'	1行目
			'	20140123 検査開始時間を追加
			'	20140123 検査開始ＰＣ日時を追加
			'
			Buff = IIf _
			(
				MESrec.dh.okng = 0,
				"合格",
				"不合格"
			) +
			",検査日付," +
			MESrec.dh.sdt +
			", PC日時," +
			MESrec.dh.pcdt


			TextFile.WriteLine(Buff)


			Buff = "項目," + MESrec.type

			TextFile.WriteLine(Buff)


			Buff =
				"作#," +
				MESrec.dh.mno +
				",電極サイズ," +
				MESrec.dh.inc +
				",インチ"

			TextFile.WriteLine(Buff)


			Buff = "注#," + MESrec.dh.tno + ",電極種類," + MESrec.dh.vo

			TextFile.WriteLine(Buff)


			Buff = "Ｓ／Ｎ," + MESrec.dh.sno

			TextFile.WriteLine(Buff)


			Dim i As Integer
			Dim j As Integer
			Dim k As Integer


			For i = 0 To 3


				'データがなければ何もしない
				If MESrec.dt(i).t1siz + MESrec.dt(i).t2.dsiz + MESrec.dt(i).t3.dsiz + MESrec.dt(i).t4.dsiz = OPIPco Then
					Continue For
				End If

				For j = 0 To MESrec.dt(i).t1siz - 1

					With MESrec.dt(i).t1(j)

						'
						'	1行送り
						'
						TextFile.WriteLine("")


						'
						'
						'
						' 電圧印加場所文字列
						Buff = convVoltInPosToStr(.posv)

						Buff += "　　試験条件," + convVacumStr2(i)

						TextFile.WriteLine(Buff)



						'
						'
						'
						Buff = "温度CH1,温度CH2,印加電圧,電流値,絶縁抵抗,判定基準,判定,真空度"

						TextFile.WriteLine(Buff)


						For k = 0 To .dsiz - 1

							' サーモチラーＣＨ１使用状況と設定温度
							Buff = convTempCH1ToStr _
								(
									MESrec.dt(i).tmp(0),
									MESrec.dt(i).schuse(0)
								)


							Buff +=
								"," +
								convTempCH2ToStr _
								(
									MESrec.dt(i).tmp(1),
									MESrec.dt(i).schuse(1)
								)


							Buff +=
								"," +
								.d(k).volt.ToString


							' 20140204 y.goto
							' 電流値は μA 固定とする
							Buff +=
								"," +
								convAmp2uAmp(.d(k).amp) +
								",μA"


							' 20140204 y.goto
							' 抵抗値は MΩ 固定とする
							Buff +=
								"," +
								convOm2MOm(.d(k).om) +
								",MΩ"


							Buff += ",-"

							Buff += ",-"

							Buff +=
								"," +
								.d(k).vq.ToString("0.000") +
								",Pa"

							TextFile.WriteLine(Buff)

						Next

					End With

				Next


				'▼ 2024.05.27 TC Kanda （４．測定の順番変更）

				With MESrec.dt(i).t3

					If .dsiz > 0 Then

						'
						'	1行送り
						'
						TextFile.WriteLine("")


						'
						'
						'
						Buff = "電極とウェハ間のＨｅリーク量"

						' 電圧印加場所文字列
						Buff += "  " + convVoltInPosToStr(.posv)

						Buff += "　試験条件:" + convVacumStr2(i)

						TextFile.WriteLine(Buff)


						'
						' 20201102 S_Harada 測定方法変更
						'Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,ＭＦＣ電圧,リーク量,判定基準,判定"
						'▼ 2024.06.11 TC Kanda （その他．ログファイルのタイトル位置修正）
						'Buff = "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,ＭＦＣ電圧1Kpa,リーク量1Kpa,ＭＦＣ電圧2Kpa,リーク量2Kpa" _
						'			+ ",ＭＦＣ電圧3Kpa,リーク量3Kpa,ＭＦＣ電圧4Kpa,リーク量4Kpa,ＭＦＣ電圧6Kpa,リーク量6Kpa" _
						'			+ ",判定基準１,判定１,判定基準２,判定２"
						Buff = "温度CH1,,,温度CH2,,,印加電圧CH1,,印加電圧CH2,,ＭＦＣ電圧1Kpa,,リーク量1Kpa,,ＭＦＣ電圧2Kpa,,リーク量2Kpa,,ＭＦＣ電圧3Kpa,,リーク量3Kpa,,ＭＦＣ電圧4Kpa,,リーク量4Kpa,,ＭＦＣ電圧6Kpa,,リーク量6Kpa,,判定基準１,,判定１,判定基準２,,判定２,判定基準３,,判定３,判定基準４,,判定４,判定基準５,
,判定５"
						'▲ 2024.06.11 TC Kanda （その他．ログファイルのタイトル位置修正）

						TextFile.WriteLine(Buff)

					End If


					For j = 0 To .dsiz - 1

						Buff = convTempCH1ToStr _
						(
							MESrec.dt(i).tmp(0),
							MESrec.dt(i).schuse(0)
						)

						Buff +=
							"," +
							convTempCH2ToStr _
							(
								MESrec.dt(i).tmp(1),
								MESrec.dt(i).schuse(1)
							)

						Buff +=
							"," +
							.d(j).volt1.ToString("0") +
							",V"

						Buff +=
							"," +
							IIf _
							(
								ESCmd = POL_DIE,
								.d(j).volt2.ToString("0"),
								"-"
							) +
							",V"

						Buff +=
							"," +
							.d(j).mfcvolt(0).ToString("0.00000") +
							",V"

						Buff +=
							"," +
							.d(j).cm(0).ToString("0.00000") +
							",sccm"

						Buff +=
							"," +
							.d(j).mfcvolt(1).ToString("0.00000") +
							",V"

						Buff +=
							"," +
							.d(j).cm(1).ToString("0.00000") +
							",sccm"

						Buff +=
							"," +
							.d(j).mfcvolt(2).ToString("0.00000") +
							",V"

						Buff +=
							"," +
							.d(j).cm(2).ToString("0.00000") +
							",sccm"

						Buff +=
							"," +
							.d(j).mfcvolt(3).ToString("0.00000") +
							",V"

						Buff +=
							"," +
							.d(j).cm(3).ToString("0.00000") +
							",sccm"

						Buff +=
							"," +
							.d(j).mfcvolt(4).ToString("0.00000") +
							",V"

						Buff +=
							"," +
							.d(j).cm(4).ToString("0.00000") +
							",sccm"



						For l As Integer = 0 To .d(j).bs.Length - 1
							If .d(j).bs(l) > 0 Then
								Buff +=
								"," +
								.d(j).bs(l).ToString("0.000") +
								",sccm以下"
							Else
								Buff +=
								"," +
								",sccm以下"
							End If
							If .d(j).okng(l) = True Then
								Buff +=
									"," +
									"合"
							ElseIf .d(j).okng(l) = False Then
								Buff +=
									"," +
									"否"
							ElseIf Not .d(j).okng(l).HasValue Then
								Buff +=
									"," +
									" "
							End If

						Next
						TextFile.WriteLine(Buff)
					Next

				End With
				'▲ 2024.06.27 TC Kanda （４．測定の順番変更）


				With MESrec.dt(i).t2

					If .dsiz > 0 Then


						'
						'	1行送り
						'
						TextFile.WriteLine("")


						'
						'
						'
						Buff = "吸着力試験"

						' 電圧印加場所文字列
						Buff += "  " + convVoltInPosToStr(.posv)

						Buff += "　試験条件:" + convVacumStr2(i)

						TextFile.WriteLine(Buff)


						'
						'
						' 20201102 S_Harada AQTC用に変更
						'Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,裏面圧力１,裏面圧力２,裏面圧力３,判定基準,到達時間,判定"
						'▼ 2024.06.11 TC Kanda （その他．ログファイルのタイトル位置修正）
						'Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,Ｈｅ流量,到達時間1Kpa,到達時間2Kpa,到達時間3Kpa,到達時間4Kpa,到達時間6Kpa,判定基準,判定"
						Buff = "温度CH1,,, 温度CH2,,, 印加電圧CH1,, 印加電圧CH2,, Ｈｅ流量,, 到達裏面圧の最大値(kPa),,到達時間1Kpa,, 到達時間2Kpa,, 到達時間3Kpa,, 到達時間4Kpa,, 到達時間6Kpa,, 判定基準,, 判定"
						'▲ 2024.06.11 TC Kanda （その他．ログファイルのタイトル位置修正）

						TextFile.WriteLine(Buff)

					End If

					For j = 0 To .dsiz - 1

						Dim buf As New List(Of String)

						For k = 0 To MESrec.dt(i).tmp.Length - 1
							buf.Add("CH" + (k + 1).ToString)
							If MESrec.dt(i).schuse(0) Then
								buf.Add(MESrec.dt(i).tmp(k).ToString)
							Else
								buf.Add("-")
							End If
							buf.Add("℃")
						Next

						buf.Add(.d(j).volt1.ToString("0"))
						buf.Add("V")

						If ESCmd = POL_DIE Then
							buf.Add(.d(j).volt2.ToString("0"))
						Else
							buf.Add(.d(j).volt2.ToString("-"))
						End If
						buf.Add("V")


						buf.Add(.d(j).he.ToString("0.00"))
						buf.Add("sccm")

						buf.Add(.d(j).maxPa.ToString("0"))
						buf.Add("kPa")

						For k = 0 To .d(j).tmr.Length - 1
							buf.Add(.d(j).tmr(k).ToString)
							buf.Add("sec")
						Next

						buf.Add(.d(j).arrivalTime.ToString("0."))
						buf.Add("sec以下")

						If .d(j).okng = 0 Then
							buf.Add("合")
						ElseIf .d(j).okng = 1 Then
							buf.Add("否")
						Else
							buf.Add("")
						End If

						TextFile.WriteLine(String.Join(",", buf))

					Next

				End With

				With MESrec.dt(i).t4

					If .dsiz > 0 Then


						'
						'	1行送り
						'
						TextFile.WriteLine("")


						'
						'
						'
						Buff = "残留吸着力試験"

						' 電圧印加場所文字列
						Buff += "  " + convVoltInPosToStr(.posv)

						Buff += "　試験条件:" + convVacumStr2(i)

						TextFile.WriteLine(Buff)


						'▼ 2024.06.11 TC Kanda （その他．ログファイルのタイトル位置修正）
						'Buff = "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,He流量,印加前裏面圧力,印加後裏面圧力,圧力差,判定基準,判定"
						Buff = "温度CH1,,,温度CH2,,,印加電圧CH1,,印加電圧CH2,,He流量,,印加前裏面圧力,,印加後裏面圧力,,圧力差,,判定基準,,判定"
						'▲ 2024.06.11 TC Kanda （その他．ログファイルのタイトル位置修正）
						TextFile.WriteLine(Buff)

					End If
					'データ行の出力
					For j = 0 To .dsiz - 1
						Dim buf As New List(Of String)

						For k = 0 To MESrec.dt(i).tmp.Length - 1
							buf.Add("CH" + (k + 1).ToString())
							If MESrec.dt(i).schuse(k) Then
								buf.Add(MESrec.dt(i).tmp(k).ToString())
							Else
								buf.Add("-")
							End If
							buf.Add("℃")
						Next

						buf.Add(.d(j).volt1.ToString("0"))
						buf.Add("V")
						If ESCmd = POL_DIE Then
							buf.Add(.d(j).volt2.ToString("0"))
						Else
							buf.Add(.d(j).volt2.ToString("-"))
						End If
						buf.Add("V")

						buf.Add(PrmHeFlow.ToString("0.0"))
						buf.Add(PrmHeFlow.ToString("sccm"))

						buf.Add(.d(j).pc.ToString("0.0"))
						buf.Add("Pa")

						buf.Add(.d(j).pd.ToString("0.0"))
						buf.Add("Pa")

						buf.Add(.d(j).pdc.ToString("0.0"))
						buf.Add("Pa")

						buf.Add(.d(j).bs.ToString("0.0"))
						buf.Add("Pa以下")

						If .d(j).okng = 0 Then
							buf.Add("合")
						ElseIf .d(j).okng = 1 Then
							buf.Add("否")
						Else
							buf.Add("")
						End If

						TextFile.WriteLine(String.Join(",", buf))

					Next

				End With

			Next


		Catch ex As Exception

			WriteLog("", "LG", "SaveResultData Buff=" + Buff + " " + ex.Message)

			Throw ex

		Finally

			TextFile.Close()

		End Try


		Return ret

	End Function


End Module
