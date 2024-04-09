

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

		Dim ret			As Integer = -1
		Dim TextFile		As IO.StreamWriter
		Dim Buff		As String = ""



		Try

			'
			'	データ格納フォルダの作成
			'
			If Dir( TstRstFolpth, vbDirectory ) = "" Then

				MkDir( TstRstFolpth )

			End If


			' ファイルをオープン
			TextFile		= New IO.StreamWriter		_
			(							_
				New IO.FileStream				_
				(						_
					TstRstFpath,				_
					IO.FileMode.Create			_
				),						_
				System.Text.Encoding.Default			_
			)


		Catch ex As Exception

			WriteLog( "", "LG", "SaveResultData File=" + TstRstFpath + " " + ex.Message )

			Return ret

		End Try


		Try

			'
			'	1行目
			'	20140123 検査開始時間を追加
			'	20140123 検査開始ＰＣ日時を追加
			'
			Buff			= IIf		_
			(					_
				MESrec.dh.okng = 0,		_
				"合格",				_
				"不合格"			_
			) +					_
			",検査日付," +				_
			MESrec.dh.sdt +				_
			", PC日時," +				_
			MESrec.dh.pcdt


			TextFile.WriteLine( Buff )


			Buff			= "項目," + MESrec.type

			TextFile.WriteLine( Buff )


			Buff			=		_
				"作#," +			_
				MESrec.dh.mno +			_
				",電極サイズ," +		_
				MESrec.dh.inc +			_
				",インチ"

			TextFile.WriteLine( Buff )


			Buff			= "注#," + MESrec.dh.tno + ",電極種類," + MESrec.dh.vo

			TextFile.WriteLine( Buff )


			Buff			= "Ｓ／Ｎ," + MESrec.dh.sno

			TextFile.WriteLine( Buff )


			Dim i			As Integer
			Dim j			As Integer
			Dim k			As Integer


			For i = 0 To 3

				'
				'   20200716 s.harada
				'	AQTC対応
				'
				If								_
					MESrec.dt( i ).t1siz > 0 Or				_
					MESrec.dt( i ).t2.dsiz > 0 Or				_
					MESrec.dt( i ).t3.dsiz > 0 Or				_
					MESrec.dt( i ).t4.dsiz > 0 				_
				Then
					'If					_
					'	MESrec.dt( i ).t1siz > 0 Or	_
					'	MESrec.dt( i ).t2.dsiz > 0 Or	_
					'	MESrec.dt( i ).t3.dsiz > 0	_
					'Then

					For j = 0 To MESrec.dt( i ).t1siz - 1

						With MESrec.dt( i ).t1( j )

							'
							'	1行送り
							'
							TextFile.WriteLine( "" )


							'
							'
							'
							' 電圧印加場所文字列
							Buff			= convVoltInPosToStr( .posv )

							Buff			+= "　　試験条件," + convVacumStr2( i )

							TextFile.WriteLine( Buff )



							'
							'
							'
							Buff			= "温度CH1,温度CH2,印加電圧,電流値,絶縁抵抗,判定基準,判定,真空度"

							TextFile.WriteLine( Buff )


							For k = 0 To .dsiz - 1

								' サーモチラーＣＨ１使用状況と設定温度
								Buff			= convTempCH1ToStr	_
									(					_
										MESrec.dt( i ).tmp( 0 ),	_
										MESrec.dt( i ).schuse( 0 )	_
									)


								Buff			+=			_
									"," +					_
									convTempCH2ToStr			_
									(					_
										MESrec.dt( i ).tmp( 1 ),	_
										MESrec.dt( i ).schuse( 1 )	_
									)


								Buff			+=			_
									"," +						_
									.d( k ).volt.ToString


								' 20140204 y.goto
								' 電流値は μA 固定とする
								Buff			+=			_
									"," +					_
									convAmp2uAmp( .d( k ).amp ) +		_
									",μA"


								' 20140204 y.goto
								' 抵抗値は MΩ 固定とする
								Buff			+=			_
									"," +					_
									convOm2MOm( .d( k ).om ) +		_
									",MΩ"


								Buff			+= ",-"

								Buff			+= ",-"

								Buff			+=			_
									"," +					_
									.d( k ).vq.ToString( "0.000" ) +	_
									",Pa"

								TextFile.WriteLine( Buff )

							Next

						End With

					Next


					With MESrec.dt( i ).t2

						If .dsiz > 0 Then


							'
							'	1行送り
							'
							TextFile.WriteLine( "" )


							'
							'
							'
							Buff			= "吸着力試験"

							' 電圧印加場所文字列
							Buff			+= "  " + convVoltInPosToStr( .posv )

							Buff			+= "　試験条件:" + convVacumStr2( i )

							TextFile.WriteLine(Buff)


							'
							'
							' 20201102 S_Harada AQTC用に変更
							'Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,裏面圧力１,裏面圧力２,裏面圧力３,判定基準,到達時間,判定"
							Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,Ｈｅ流量,到達時間1Kpa,到達時間2Kpa,到達時間3Kpa,到達時間4Kpa,到達時間6Kpa,判定基準,判定"

							TextFile.WriteLine( Buff )

						End If

						For j = 0 To .dsiz - 1

							Buff			= convTempCH1ToStr		_
							(							_
								MESrec.dt( i ).tmp( 0 ),			_
								MESrec.dt( i ).schuse( 0 )			_
							)

							Buff			+=			_
								"," +					_
								convTempCH2ToStr			_
								(					_
									MESrec.dt( i ).tmp( 1 ),	_
									MESrec.dt( i ).schuse( 1 )	_
								)

							Buff			+=			_
								"," +					_
								.d( j ).volt1.ToString( "0" ) +		_
								",V"

							Buff			+=				_
								"," +						_
								IIf						_
								(						_
									ESCmd = POL_DIE,			_
									.d( j ).volt2.ToString( "0" ),		_
									"-"					_
								) +						_
								",V"

							'
							' 20201102 S_Harada AQTC用に変更
							'Buff			+=					_
							'	"," +							_
							'	( .d( j ).pa( 0 ) / 1000 ).ToString( "0.00" ) +		_
							'	",KPa"
							'Buff			+=					_
							'	"," +							_
							'	( .d( j ).pa( 1 ) / 1000 ).ToString( "0.00" ) +		_
							'	",KPa"

							'Buff			+=					_
							'	"," +							_
							'	( .d( j ).pa( 2 ) / 1000 ).ToString( "0.00" ) +		_
							'	",KPa"

							'Buff			+=					_
							'	"," +							_
							'	( .d( j ).bs / 1000).ToString( "0.00" ) +		_
							'	",KPa以上"

							'Buff			+=			_
							'	"," +					_
							'	.d( j ).tmr.ToString( "0.0" ) +		_
							'	",sec"

							Buff			+=				_
								"," +						_
								.d( j ).he.ToString( "0.00" ) +			_
								",sccm"

							Buff			+=			_
								"," +					_
								.d( j ).tmr( 0 ).ToString( "0." ) +		_
								",sec"

							Buff			+=			_
								"," +					_
								.d( j ).tmr( 1 ).ToString( "0." ) +		_
								",sec"

							Buff			+=			_
								"," +					_
								.d( j ).tmr( 2 ).ToString( "0." ) +		_
								",sec"

							Buff			+=			_
								"," +					_
								.d( j ).tmr( 3 ).ToString( "0." ) +		_
								",sec"

							Buff			+=			_
								"," +					_
								.d( j ).tmr( 4 ).ToString( "0." ) +		_
								",sec"

							Buff			+=			_
								"," +					_
								.d( j ).bs.ToString( "0." ) +		_
								",sec以下"

							' 20201102 S_Harada 判定無に対応
							'Buff			+=			_
							'	"," +					_
							'	IIf( .d( j ).okng = 0, "合", "否" )
							If .d( j ).okng = 0 Then

								Buff			+=			_
									"," +					_
									"合"

							ElseIf .d( j ).okng = 1 then

								Buff			+=			_
									"," +					_
									"否"

							Else

								Buff			+=			_
									"," +					_
									" "

							End If


							TextFile.WriteLine( Buff )

						Next

					End With


					With MESrec.dt( i ).t3

						If .dsiz > 0 Then

							'
							'	1行送り
							'
							TextFile.WriteLine( "" )


							'
							'
							'
							Buff			= "電極とウェハ間のＨｅリーク量"

							' 電圧印加場所文字列
							Buff			+= "  " + convVoltInPosToStr( .posv )

							Buff			+= "　試験条件:" + convVacumStr2( i )

							TextFile.WriteLine( Buff )


							'
							' 20201102 S_Harada 測定方法変更
							'Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,ＭＦＣ電圧,リーク量,判定基準,判定"
							Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,ＭＦＣ電圧1Kpa,リーク量1Kpa,ＭＦＣ電圧2Kpa,リーク量2Kpa" _
										+ ",ＭＦＣ電圧3Kpa,リーク量3Kpa,ＭＦＣ電圧4Kpa,リーク量4Kpa,ＭＦＣ電圧6Kpa,リーク量6Kpa" _
										+ ",判定基準１,判定１,判定基準２,判定２"

							TextFile.WriteLine( Buff )

						End If


						For j = 0 To .dsiz - 1

							Buff			= convTempCH1ToStr		_
							(							_
								MESrec.dt( i ).tmp( 0 ),			_
								MESrec.dt( i ).schuse( 0 )			_
							)

							Buff			+=				_
								"," +						_
								convTempCH2ToStr				_
								(						_
									MESrec.dt( i ).tmp( 1 ),		_
									MESrec.dt( i ).schuse( 1 )		_
								)

							Buff			+=				_
								"," +						_
								.d( j ).volt1.ToString( "0" ) +			_
								",V"

							Buff			+=				_
								"," +						_
								IIf						_
								(						_
									ESCmd = POL_DIE,			_
									.d( j ).volt2.ToString( "0" ),		_
									"-"					_
								) +						_
								",V"

							' 20201102 S_Harada 測定方法変更
							'Buff			+=				_
							'	"," +						_
							'	.d( j ).mfcvolt.ToString( "0.00000" ) +		_
							'	",V"

							'Buff			+=				_
							'	"," +						_
							'	.d( j ).cm.ToString( "0.00000" ) +		_
							'	",ml/min"
							Buff			+=				_
								"," +						_
								.d( j ).mfcvolt( 0 ).ToString( "0.00000" ) +	_
								",V"

							Buff			+=				_
								"," +						_
								.d( j ).cm( 0 ).ToString( "0.00000" ) +		_
								",sccm"

							Buff			+=				_
								"," +						_
								.d( j ).mfcvolt( 1 ).ToString( "0.00000" ) +	_
								",V"

							Buff			+=				_
								"," +						_
								.d( j ).cm( 1 ).ToString( "0.00000" ) +		_
								",sccm"

							Buff			+=				_
								"," +						_
								.d( j ).mfcvolt( 2 ).ToString( "0.00000" ) +	_
								",V"

							Buff			+=				_
								"," +						_
								.d( j ).cm( 2 ).ToString( "0.00000" ) +		_
								",sccm"

							Buff			+=				_
								"," +						_
								.d( j ).mfcvolt( 3 ).ToString( "0.00000" ) +	_
								",V"

							Buff			+=				_
								"," +						_
								.d( j ).cm( 3 ).ToString( "0.00000" ) +		_
								",sccm"

							Buff			+=				_
								"," +						_
								.d( j ).mfcvolt( 4 ).ToString( "0.00000" ) +	_
								",V"

							Buff			+=				_
								"," +						_
								.d( j ).cm( 4 ).ToString( "0.00000" ) +		_
								",sccm"


							' 20201102 S_Harada 単位変更
							'Buff			+=				_
							'	"," +						_
							'	.d( j ).bs.ToString( "0.0" ) +			_
							'	"ml/m以下"
							Buff			+=				_
								"," +						_
								.d( j ).bs.ToString( "0.0" ) +			_
								"sccm以下"

							' 20201102 S_Harada 判定無に対応
							'Buff			+=				_
							'	"," +						_
							'	IIf						_
							'	(						_
							'		.d( j ).okng = 0,			_
							'		"合",					_
							'		"否"					_
							'	)
							If .d( j ).okng = 0 Then

								Buff			+=			_
									"," +					_
									"合"

							ElseIf .d( j ).okng = 1 then

								Buff			+=			_
									"," +					_
									"否"

							Else

								Buff			+=			_
									"," +					_
									" "

							End If

							' 20201102 S_Harada AQTC用に追加
							Buff			+=				_
								"," +						_
								.d( j ).bs2.ToString( "0.0" ) +			_
								"sccm以下"

							If .d( j ).okng2 = 0 Then

								Buff			+=			_
									"," +					_
									"合"

							ElseIf .d( j ).okng2 = 1 then

								Buff			+=			_
									"," +					_
									"否"

							Else

								Buff			+=			_
									"," +					_
									" "

							End If

							TextFile.WriteLine( Buff )

						Next

					End With


					With MESrec.dt( i ).t4

						If .dsiz > 0 Then


							'
							'	1行送り
							'
							TextFile.WriteLine( "" )


							'
							'
							'
							Buff			= "残留吸着力試験"

							' 電圧印加場所文字列
							Buff			+= "  " + convVoltInPosToStr( .posv )

							Buff			+= "　試験条件:" + convVacumStr2( i )

							TextFile.WriteLine( Buff )


							Buff			= "温度CH1,温度CH2,印加電圧CH1,印加電圧CH2,He流量,印加前裏面圧力,印加後裏面圧力,圧力差,判定基準,判定"

							TextFile.WriteLine( Buff )

						End If

						For j = 0 To .dsiz - 1

							Buff			= convTempCH1ToStr		_
							(							_
								MESrec.dt( i ).tmp( 0 ),			_
								MESrec.dt( i ).schuse( 0 )
							)

							Buff			+=				_
								"," +						_
								convTempCH2ToStr				_
								(						_
									MESrec.dt( i ).tmp( 1 ),		_
									MESrec.dt( i ).schuse( 1 )		_
								)

							Buff			+=				_
								"," +						_
								.d( j ).volt1.ToString( "0" ) +			_
								",V"

							Buff			+=				_
								"," +						_
								IIf						_
								(						_
									ESCmd		= POL_DIE,		_
									.d( j ).volt2.ToString( "0" ),		_
									"-"					_
								) +						_
								",V"

							' 20201102 S_Harada 単位変更ccm->sccm KPa->Pa
							Buff			+=				_
								"," +						_
								PrmHeFlow.ToString( "0.0" ) +			_
								",sccm"

							Buff			+=				_
								"," +						_
								.d( j ).pc.ToString( "0.0" ) +			_
								",Pa"

							Buff			+=				_
								"," +						_
								.d( j ).pd.ToString( "0.0" ) +			_
								",Pa"

							Buff			+=				_
								"," +						_
								.d( j ).pdc.ToString( "0.0" ) +			_
								",Pa"

							Buff			+=				_
								"," +						_
								.d( j ).bs.ToString( "0.0" ) +			_
								",Pa以下"

							' 20201102 S_Harada 判定無に対応
							'Buff			+=				_
							'	"," +						_
							'	IIf( .d( j ).okng = 0, "合", "否" )
							If .d( j ).okng = 0 Then

								Buff			+=			_
									"," +					_
									"合"

							ElseIf .d( j ).okng = 1 then

								Buff			+=			_
									"," +					_
									"否"

							Else

								Buff			+=			_
									"," +					_
									" "

							End If

							TextFile.WriteLine( Buff )

						Next

					End With

				End If

			Next


		Catch ex As Exception

			WriteLog( "", "LG", "SaveResultData Buff=" + Buff + " " + ex.Message )

			Throw ex

		Finally

			TextFile.Close()

		End Try


		Return ret

	End Function


End Module
