

Module convToStrlib



	'*****
	'	真空状態フラグを文字列に変換
	'*****
	Public Function convVacumStr			_
	(						_
		ByVal vac		As Integer	_
	)	As String

		'vac;		真空状態フラグ	0 = 大気圧  	1 = 真空圧

		Return IIf( vac = 0, "大気", "真空" )


	End Function



	'*****
	'	真空状態フラグを文字列に変換
	'*****
	Public Function convVacumStr2			_
	(						_
		ByVal vac		As Integer	_
	)	As String

		' vac;		真空状態フラグ	0 = 大気圧  	1 = 真空圧


		Return IIf( vac = 0, "大気圧", "真空圧" )


	End Function



	Public Function convFunikiToStr			_
	(						_
		ByVal no		As Integer	_
	)	As String

		' no:雰囲気　0:大気　1:真空(低温）　2:真空（高温）　3:真空（高温２）
		Dim rStr		As String = ""



		Select Case	no

		Case	0

			rStr			= "大気"

		Case	1

			rStr			= "真空（低温）"

		Case	2

			rStr			= "真空（高温）"

		Case	3

			rStr			= "真空（高温２）"

		End Select



		Return rStr

	End Function



	Public Function convTempInOutToStr		_
	(						_
		ByVal tmp1		As Double,	_
		ByVal tmp2		As Double,	_
		ByVal usef()		As Integer	_
	)	As String

		' tmp1:サ－モチラ－CH1　温度
		' tmp2:サ－モチラ－CH2　温度
		' usef():サ－モチラ－ＣＨ使用状況　0:使用しない　1:使用する

		Dim tStr		As String = ""



		tStr			= ""

		If usef( 0 ) <> 0 Then

			tStr			+= "CH1 " + tmp1.ToString( "0℃ " )

		End If


		If usef( 1 ) <> 0 Then

			tStr			+= "CH2 " + tmp2.ToString( "0℃" )

		End If



		Return tStr

	End Function



	'*****
	'	サーモチラーＣＨ１使用状況、設定温度文字列作成
	'*****
	Public Function convTempCH1ToStr		_
	(						_
		ByVal tmp		As Double,	_
		ByVal usef		As Integer	_
	)	As String

		' tmp  : 設定温度
		' usef : 0=未使用, 使用
 

		Return IIf( usef <> 0, "CH1," + tmp.ToString( "0" ), "CH1,-" ) + ",℃"


	End Function



	'*****
	'	サーモチラーＣＨ２使用状況、設定温度文字列作成
	'*****
	Public Function convTempCH2ToStr		_
	(						_
		ByVal tmp		As Double,	_
		ByVal usef		As Integer	_
	)	As String

		' tmp  : 設定温度
		' usef : 0=未使用, 使用
 

		Return IIf( usef <> 0, "CH2," + tmp.ToString( "0" ), "CH2,-" ) + ",℃"


	End Function



	'*****
	'	電圧印加箇所番号から電圧印加場所文字列への変換
	'
	'	<return>
	'	String
	'	電圧印加箇所文字列
	'*****
	Public Function convVoltInPosToStr		_
	(						_
		ByVal pos		As Integer	_
	)	As String

		' pos:電圧印加位置

		Dim posv		As String = ""



		Select Case	pos

		Case	0

			posv			= "基材(-)－ウエハ間(+)"

		Case	1

			posv			= "埋込電極(内)－埋込電極(外)間"

		Case	2

			posv			= "ウエハ－埋込電極(内)間"

		Case	3

			posv			= "ウエハ－埋込電極(外)間"

		Case	4

			posv			= "埋込電極(内)(外)－基材間"

		Case	5

			posv			= "埋込電極(内)－基材間"

		Case	6

			posv			= "埋込電極(外)－基材間"

		Case	7

			posv			= "ウエハ－基材間"

		End Select



		Return posv

	End Function



	Public Function convOnOffToStr			_
	(						_
		ByVal dt		As Integer	_
	)	As String


		Return IIf( dt = DIO_OFF, "OFF", "ON" )


	End Function



	Public Function convEscMdToStr			_
	(						_
		ByVal escmd		As Integer	_
	)	As String


		Return IIf( escmd = 1, "ダイポール", "モノポール" )


	End Function



	Public Function convScrUseToStr			_
	(						_
		ByVal tUse		As Integer	_
	)	As String

		Dim str			As String



		Select Case	tUse

		Case	0

			str			= "CH1"

		Case	1

			str			= "CH2"

		Case Else

			str			= "CH1-CH2"

		End Select



		Return str

	End Function



	Public Function convAmpAddTani			_
	(						_
		ByVal amp		As Double	_
	)	As String



		Select Case	amp

		Case Is < 100 * 0.000000001

			Return ( amp * 1000000000.0 ).ToString( "0.00 nA" )

		Case Is < 100 * 0.000001

			Return ( amp * 1000000.0 ).ToString( "0.00 μA" )

		Case Else

			Return ( amp * 1000.0 ).ToString( "0.00 mA" )

		End Select


	End Function



	'*****
	'	電流値をμA単位に変換する
	'
	'	20140204追加
	'	絶縁抵抗測定結果の電流値の単位は
	'	μA固定にする
	'*****
	Public Function convAmp2uAmp			_
	(						_
		ByVal amp		As Double	_
	)	As String


		Return ( amp * 1000000.0 ).ToString( "0.00" )


	End Function



	Public Function convOmAddTani			_
	(						_
		ByVal om		As Double	_
	)	As String


		Select Case	om

		Case Is >= 100 * 1000000.0

			Return ( om / 1000000000.0 ).ToString( "0.000 GΩ" )

		Case Is >= 100 * 1000.0

			Return ( om / 1000000.0 ).ToString( "0.000 MΩ" )

		Case Else

			Return ( om / 1000.0).ToString( "0.000 KΩ" )

		End Select


	End Function




	'*****
	'	抵抗値をMΩ単位に変換する
	'
	'	20140204追加
	'	絶縁抵抗測定結果の抵抗値の単位は
	'	MΩ固定にする
	'*****
	Public Function convOm2MOm			_
	(						_
		ByVal om		As Double	_
	)	As String


		Return ( om / 1000000.0 ).ToString( "0.000" )


	End Function


	'*****
	'	圧力表示[Pa]文字列の作成 N6
	'
	'	1,333,200       
	'	  133,320       
	'	   13,332       
	'	    1,333.2     
	'	      133.32    
	'	       13.332   
	'	        1.3332  
	'	        0.13332 
	'	        0.013332
	'	        0.001333
	'
	'*****
	Public Function mkpastrN6			_
	(						_
		ByVal pa		As Double	_
	)	As String

		Dim str			As String

		' 数値によって表示フォーマットを変更する
		If 1000000 <= pa Then

			' 1,000,000以上の時の表示			9,999,999
			str		= pa.ToString( "#,0" )

		ElseIf 100000 <= pa Then

			' 1,000,000未満～100,000 以上の時の表示		__999,999
			str		= "  " + pa.ToString( "#,0" )

		ElseIf 10000 <= pa Then

			' 100,000未満～10,000 以上の時の表示		___99,999
			str		= "   " + pa.ToString( "#,0" )

		ElseIf 1000 <= pa Then

			' 10,000未満～1,000 以上の時の表示		____9,999.9
			str		= "    " + pa.ToString( "N1" )

		ElseIf 100 <= pa Then

			' 1000未満～100 以上の時の表示			______999.99
			str		= "      " + pa.ToString( "N2" )

		ElseIf 10 <= pa Then

			' 100未満～10 以上の時の表示			_______99.999
			str		= "       " + pa.ToString( "N3" )

		ElseIf 1 <= pa Then

			' 10未満～1 以上の時の表示			________9.9999
			str		= "        " + pa.ToString( "N4" )

		ElseIf 0.1 <= pa Then

			' 1未満～0.1 以上の時の表示			________0.99999
			str		= "        " + pa.ToString( "N5" )

		Else

			' 0.1 未満の時の表示				________0.099999
			str		= "        " + pa.ToString( "N6" )

		End If

		Return str

	End Function


	'*****
	'	圧力表示[Pa]文字列の作成 N3
	'
	'	1,333,200    
	'	__133,320    
	'	___13,332    
	'	____1,333.2  
	'	______133.32 
	'	_______13.332
	'	________1.333
	'	________0.133
	'	________0.013
	'	________0.001
	'
	'*****
	Public Function mkpastrN3			_
	(						_
		ByVal pa		As Double	_
	)	As String

		Dim str			As String

		' 数値によって表示フォーマットを変更する
		If 1000000 <= pa Then

			' 1,000,000以上の時の表示			9,999,999    
			str		= pa.ToString( "#,0" )

		ElseIf 100000 <= pa Then

			' 1,000,000未満～100,000 以上の時の表示		__999,999    
			str		= "  " + pa.ToString( "#,0" )

		ElseIf 10000 <= pa Then

			' 100,000未満～10,000 以上の時の表示		___99,999    
			str		= "   " + pa.ToString( "#,0" )

		ElseIf 1000 <= pa Then

			' 10,000未満～1,000 以上の時の表示		____9,999.9  
			str		= "    " + pa.ToString( "N1" )

		ElseIf 100 <= pa Then

			' 1000未満～100 以上の時の表示			______999.99 
			str		= "      " + pa.ToString( "N2" )

		ElseIf 10 <= pa Then

			' 100未満～10 以上の時の表示			_______99.999
			str		= "       " + pa.ToString( "N3" )

		Else

			' 10未満の時の表示				________9.999
			str		= "        " + pa.ToString( "N3" )

		End If

		Return str

	End Function



End Module
