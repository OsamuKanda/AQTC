
'*****
'	ＡＩＯ基板関連の関数
'*****
Module AioLib



	'*****
	'	ＲＡＷデ－タから電圧値に変換
	'	0～65535 / +10～-10V
	'*****
	Public Function anar2v				_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' ch:	AIﾁｬﾈﾙ
		' raw:	読み取り値（16ビット）

		Dim volt		As Double
		' Dim rdt As UShort



		' rdt = raw And &HFFFF
		' volt = (20.0 / 65535.0) * rdt - 10.0

		volt			= ( 20.0 / 65536.0 ) * raw - 10.0
' 20211008	volt			= ( 20.0 / 65535.0 ) * raw - 10.0


		Return volt

	End Function



	'/*****
	'	ＲＡＷデ－タから電圧値に変換
	'	ch=0:0～+10V / 0～65535
	'	ch=1:+10～-10V / 0～65535
	'	ch=2:0～5V / 0～65535
	'*****/
	Public Function anar2v				_
	(						_
		ByVal ch		As Integer,	_
		ByVal raw		As Short	_
	)	As Double

		' ch;	/* AOﾁｬﾈﾙ			*/
		' raw;	/* 読み取り値			*/

		Dim volt		As Double



		Select Case	ch

		Case	0

			volt			= anar2v0( raw )

		Case	1

			volt			= anar2v1( raw )

		Case	2

			volt			= anar2v2( raw )

		End Select


		Return volt

	End Function



	'/*****
	'	ＲＡＷデ－タから電圧値に変換
	'
	'	0～65535 / 0～+10V
	'*****/
	Public Function anar2v0			_
	(							_
		ByVal raw		As Short	_
	)	As Double

		'raw;	/* 読み取り値			*/

		Dim volt		As Double


		volt			= 10.0 / 65535.0 * ( raw And &HFFFF )


		Return volt

	End Function



	'/*****
	'	ＲＡＷデ－タから電圧値に変換
	'
	'	0～65535 / -10～+10V
	'*****/
	Public Function anar2v1				_
	(						_
		ByVal raw		As Short	_
	)	As Double

		'raw;	/* 読み取り値			*/
		Dim volt As Double


		volt			= 20.0 / 65536.0 * ( raw And &HFFFF ) - 10.0
' 20211008	volt			= 20.0 / 65535.0 * ( raw And &HFFFF ) - 10.0


		Return volt

	End Function



	'/*****
	'	ＲＡＷデ－タから電圧値に変換
	'
	'	0～65535 / 0～+5V
	'*****/
	Public Function anar2v2				_
	(						_
		ByVal raw		As Short	_
	)	As Double

		'raw;	/* 読み取り値			*/

		Dim volt		As Double


		volt			= 5.0 / 65535.0 * ( raw And &HFFFF )


		Return volt

	End Function




	'*****
	'	電圧値からＲＡＷデ－タに変換
	'
	'	ch=0:0～+10V / 0～65535
	'	ch=1:+10～-10V / 0～65535
	'	ch=2:0～5V / 0～65535
	'*****
	Public Function anav2r				_
	(						_
		ByVal ch		As Integer,	_
		ByVal volt		As Double	_
	)	As Short

		' ch:	AOﾁｬﾈﾙ
		' volt:	電圧値

		Dim raw			As Short



		Select Case ch

		Case	0

			raw			= anav2r0( volt )

		Case	1

			raw			= anav2r1( volt )

		Case	2

			raw			= anav2r2( volt )

		End Select


		Return raw

	End Function



	'*****
	'	電圧値からＲＡＷデ－タに変換
	'	0～+10V / 0～65535
	'*****
	Public Function anav2r0				_
	(						_
		ByVal volt		As Double	_
	)	As Short

		' volt:	電圧値

		Dim rawi		As Integer
		Dim raw			As Short



		If ( volt < 0.0 ) Then

			rawi			= 0

		Else

			rawi			= CInt( ( 65535.0 / 10.0 ) * volt + 0.5 )

			If ( &HFFFF < rawi ) Then

				rawi			= &HFFFF

			End If

		End If


		If rawi < &H8000 Then

			raw			= rawi

		Else

			raw			= rawi - &H10000

		End If



		Return raw

	End Function



	'*****
	'	電圧値からＲＡＷデ－タに変換
	'	-10～+10V / 0～65535
	'*****
	Public Function anav2r1				_
	(						_
		ByVal volt		As Double	_
	)	As Short

		'volt:	電圧値

		Dim rawi		As Integer
		Dim raw			As Short



		If ( volt < -10.0 ) Then

			rawi			= 0

		Else

			rawi			= CInt( ( 65536.0 / 20.0 ) * ( volt + 10.0 ) + 0.5 )
' 20211008		rawi			= CInt( ( 65535.0 / 20.0 ) * ( volt + 10.0 ) + 0.5 )

			If ( &HFFFF < rawi ) Then

				rawi			= &HFFFF

			End If

		End If


		If rawi < &H8000 Then

			raw			= rawi

		Else

			raw			= rawi - &H10000

		End If


		Return raw

	End Function



	'*****
	'	電圧値からＲＡＷデ－タに変換
	'	0～5V / 0～65535
	'*****
	Public Function anav2r2				_
	(						_
		ByVal volt		As Double	_
	)	As Short

		' volt:	電圧値	
			
		Dim rawi		As Integer
		Dim raw			As Short



		If ( volt < 0.0 ) Then

			rawi			= 0

		Else

			rawi			= CInt( ( 65535.0 / 5.0 ) * volt + 0.5 )

			If ( &HFFFF < rawi ) Then

				rawi			= &HFFFF

			End If

		End If


		If rawi < &H8000 Then

			raw			= rawi

		Else

			raw			= rawi - &H10000

		End If



		Return raw

	End Function



	'*****
	'	流量モニタ電圧を流量へ換算
	'	0～5V / 0～50CCM 
	'	dat	= volt * ( 50.0 / 5.0 );
	'*****
	Public Function cvtv2Ccm			_
	(						_
		ByVal volt		As Double	_
	)	As Double

		' volt:	    モニタ電圧値
		
		Dim dat			As Double



		' 流量へ換算					
		dat			= volt * 10.0


		Return dat

	End Function



	'*****
	'	ＭＦＣの流量モニタ電圧を流量へ換算
	'	0～5V / 0～50CCM (AIﾚﾝｼﾞは±10V)
	'	dat	= volt * ( 50.0 / 5.0 );
	'*****
	Public Function cvtr2MFCop			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' raw;		AI読み取り値
			
		Dim volt		As Double
		Dim dat			As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		' 流量へ換算							
		dat			= volt * 10.0


		Return dat

	End Function



	'*****
	'	ＭＦＣの流量モニタ電圧を流量へ換算
	'	0～5V / 0～50CCM (AIﾚﾝｼﾞは±10V)
	'	dat	= volt * ( 50.0 / 5.0 );
	'*****
	Public Function cvtr2MFCop			_
	(						_
		ByVal raw		As UShort,	_
		ByRef v			As Double	_
	)	As Double

		' raw;		AI読み取り値			
		' v;		RAW->電圧変換値

		Dim volt		As Double
		Dim dat			As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		v			= volt

		' 流量へ換算							
		dat			= volt * 10.0



		Return dat

	End Function



	'*****
	'	※20140109 未使用
	'
	'	ＭＦＣのバルブ電圧モニタを電圧に変換
	'	0～10V / バルブ開閉状態 (0V=全閉, 10V=全開)
	'*****
	Public Function cvtr2MFCvm			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' raw:		AI読み取り値			

		Dim volt		As Double



		' 電圧値に変換							
		volt			= anar2v( raw )



		Return ( volt )

	End Function



	'*****
	'	ＭＦＣの流量を流量設定信号(RAW)へ換算
	'	AOX02:ＭＦＣ・ＳＥＴ　ＰＴ
	'	0～5V / 0～50CCM
	'*****
	Public Function cvtf2MFCset			_
	(						_
		ByVal flow		As Double	_
	)	As Short

		' flow;		設定流量			

		Dim raw			As Short
		Dim volt		As Double



		' 電圧値に変換						
		volt			= flow * 5.0 / 50.0

		' 電圧値から RAW データに変換					
		raw			= anav2r2( volt )



		Return raw

	End Function



	'*****
	'	ＭＦＣの流量を流量設定信号(RAW)へ換算
	'	AOX02:ＭＦＣ・ＳＥＴ　ＰＴ
	'	0～5V / 0～50CCM
	'*****
	Public Function cvtf2MFCset			_
	(						_
		ByVal flow		As Double,	_
		ByRef v			As Double	_
	)	As Short

		'flow;		設定流量			
		'v;         flow->電圧変換値

		Dim raw			As Short
		Dim volt		As Double



		' 電圧値に変換	(0～5V / 0～10CCM) の場合					
		' volt			= flow * 5.0 / 10.0

		' 電圧値に変換	(0～5V / 0～50CCM) の場合					
		volt			= flow * 5.0 / 50.0


		v			= volt

		' 電圧値から RAW データに変換					
		raw			= anav2r2( volt )



		Return raw


	End Function



	'*****
	'	ＭＦＣの流量を流量設定信号(RAW)へ換算
	'	AOX02:ＭＦＣ・ＳＥＴ　ＰＴ
	'	-10～+10V / 0～50CCM
	'*****
	Public Function cvtf2MFCset2			_
	(						_
		ByVal flow		As Double,	_
		ByRef v			As Double	_
	)	As Short

		' flow;		設定流量			
		' v;		flow->電圧変換値

		Dim raw			As Short
		Dim volt		As Double



		' 電圧値に変換						
		volt			= flow * 5.0 / 50.0

		v			= volt

		' 電圧値から RAW データに変換					
		raw			= anav2r1( volt )



		Return raw

	End Function



	'*****
	'	圧力 [Pa] から [Torr] に換算
	'*****
	Public Function Pa2Torr				_
	(						_
		pa			As Double	_
	)	As Double

		Dim torr		As Double


		' Pa から Torr への換算
		torr			= pa / 133.32


		Pa2Torr			= torr


	End Function



	'*****
	'	圧力 [Torr] から [Pa] に換算
	'*****
	Public Function Torr2Pa				_
	(						_
		torr			As Double	_
	)	As Double

		Dim pa			As Double



		' Torr から Pa への換算
		pa			= torr * 133.32


		Torr2Pa			= pa

	End Function



	'*****
	'       圧力[Torr]から測定電圧に換算
	'*****
	Public Function Torr2Volt			_
	(						_
		torr			As Double	_
	)	As Double

		Dim volt		As Double


		' Torr から 測定電圧への換算
		volt			= 0.5 * Math.Log10( 100 * torr * 1000 )


		Torr2Volt = volt

	End Function



	'*****
	'       バラトロン真空計の測定電圧から圧力[Torr]に換算
	'
	'	20211008 PDR2000AJが故障したためPR4000Bに交換
	'	真空計		626B12TBE / MKS
	'	コントローラ	PR4000B / MKS
	'
	'	Ptoll	= AI測定電圧 * 10.0	[Torr]	( 1 [Torr] = 133.322 [Pa] )
	'	PPa	= AI測定電圧 * 1333.22	[Pa]
	'
	'	0～10[V] / 0～100[Torr]
	'	0～10[V] / 0～13332.2
	'
	'	20211008下記は故障したもの
	'	コントローラ	PDR2000AJ / MKS ※故障
	'
	' xxxx	0～4.5V / 0.0013332～1333200 [Pa]
	' xxxx		  0.00001  ～  10000 [Torr]
	'*****
	Public Function Volt2Torr			_
	(						_
		volt			As Double	_
	)	As Double

		Dim torr		As Double


		' 電圧 から Torr への換算
'
'	20211008 y.goto
'	バラトロン圧力計表示器変更に伴う修正 PDR2000 -> PR4000B
'
		torr			= volt * 10.0
''''		torr			= Math.Pow( 10, volt / 0.5 - 2 ) / 1000


		Volt2Torr		= torr

	End Function



	'*****
	'	バラトロン真空計（６２６Ｂ）の読み取り値をＴｏｒｒに変換
	'*****
	Public Function cvtr2GM_Torr			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' raw;		AI読み取り値
		
		Dim volt		As Double
		Dim torr		As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		' 電圧値からtorrへ変換						
		torr			= Volt2Torr( volt )


		Return torr

	End Function



	'*****
	'	バラトロン真空計（６２６Ｂ）の読み取り値をＴｏｒｒに変換
	'*****
	Public Function cvtr2GM_Torr			_
	(						_
		ByVal raw		As UShort,	_
		ByRef v			As Double	_
	)	As Double

		' raw;		AI読み取り値			
		' v;		RAW->電圧変換値

		Dim volt		As Double
		Dim torr		As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		v			= volt

		' 電圧値からtorrへ変換						
		torr			= Volt2Torr( volt )



		Return torr

	End Function



	'*****
	'	バラトロン真空計（６２６Ｂ）の読み取り値をＰａに変換
	'*****
	Public Function cvtr2GM_Pa			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' raw;		AI読み取り値

		Dim volt		As Double
		Dim torr		As Double
		Dim pa			As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		' 電圧値からtorrへ変換						
		torr			= Volt2Torr( volt )

		' torrから圧力へ変換						
		pa			= Torr2Pa( torr )


		Return pa

	End Function



	'*****
	'	バラトロン真空計（６２６Ｂ）の電圧値をＰａに変換
	'*****
	Public Function cvtv2GM_Pa			_
	(						_
		ByVal volt		As Double	_
	)	As Double

		' volt;		電圧値

		Dim torr		As Double
		Dim pa			As Double



		' 電圧値からtorrへ変換						
		torr			= Volt2Torr( volt )

		' torrから圧力へ変換						
		pa			= Torr2Pa( torr )



		Return pa

	End Function



	'*****
	'	バラトロン真空計（６２６Ｂ）の読み取り値をＰａに変換
	'*****
	Public Function cvtr2GM_Pa			_
	(						_
		ByVal raw		As UShort,	_
		ByRef v			As Double	_
	)	As Double

		' raw;		AI読み取り値
		' v;		RAW->電圧変換値

		Dim volt		As Double
		Dim torr		As Double
		Dim pa			As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		v			= volt

		' 電圧値からtorrへ変換						
		torr			= Volt2Torr( volt )


		' torrから圧力へ変換						
		pa			= Torr2Pa( torr )



		Return pa

	End Function



	'*****
	'	バラトロン真空計（６２６Ｂ）のＰａ値をＰＩＤ設定値に換算
	'
	'	20211008 PDR2000AJが故障したためPR4000Bに交換
	'	真空計		626B12TBE / MKS
	'	コントローラ	PR4000B / MKS
	'
	'	バラトン真空計表示器の測定電圧は、直にPIDのPV入力(現在値)へ接続してある。
	'	従ってPIDのRSP電圧も、PVと同じスケールの電圧を入れてやる必要が有る。
	'
	'	PIDのPVレンジ	0～ 5[V] <--- バラトロン表示器圧力測定電圧値
	'	PIDのRSPレンジ  0～10[V] <--- PC AOX01 レンジ 0～5[V]
	'
	'	Ptoll	= AI測定電圧 * 10.0	[Torr]	( 1 [Torr] = 133.322 [Pa] )
	'	PPa	= AI測定電圧 * 1333.22	[Pa]
	'
	'	0～10[V] / 0～100[Torr]
	'	0～10[V] / 0～13332.2
	'
	'	従って Pa -> AOX出力電圧値への換算式は下記の通り
	'
	'	V	= P [Pa] / 1333.22
	'
	'	20211008下記は故障したもの
	' xxxx	AIX02	626B12TBE		ﾊﾞﾗﾄﾛﾝ真空計
	' xxxx	0～10V/0～13.3322 [KPa]
	'*****
	Public Function cvtp2PIDset			_
	(						_
		ByVal pa		As Double	_
	)	As Short

		' raw;		AI読み取り値
			
		Dim volt		As Double
		Dim torr		As Double
		Dim raw			As Short

'
'	20211008 y.goto
'	バラトロン圧力計表示器変更に伴う修正 PDR2000 -> PR4000B
'
		' 圧力[Pa]から電圧りへ変換
		volt			= pa / 1333.22

''''		' 圧力からtorrへ変換						
''''		torr			= Pa2Torr( pa )

''''		' torrから電圧値へ変換						
''''		volt			= Torr2Volt( torr )

		' 電圧値から RAW データに変換					
		raw			= anav2r2( volt )



		Return raw

	End Function



	'*****
	'	バラトロン真空計（６２６Ｂ）のＰａ値をＰＩＤ設定値に換算
	'
	'	20211008 PDR2000AJが故障したためPR4000Bに交換
	'	真空計		626B12TBE / MKS
	'	コントローラ	PR4000B / MKS
	'
	'	バラトン真空計表示器の測定電圧は、直にPIDのPV入力(現在値)へ接続してある。
	'	従ってPIDのRSP電圧も、PVと同じスケールの電圧を入れてやる必要が有る。
	'
	'	PIDのPVレンジ	0～ 5[V] <--- バラトロン表示器圧力測定電圧値
	'	PIDのRSPレンジ  0～10[V] <--- PC AOX01 レンジ 0～5[V]
	'
	'	Ptoll	= AI測定電圧 * 10.0	[Torr]	( 1 [Torr] = 133.322 [Pa] )
	'	PPa	= AI測定電圧 * 1333.22	[Pa]
	'
	'	0～10[V] / 0～100[Torr]
	'	0～10[V] / 0～13332.2
	'
	'	従って Pa -> AOX出力電圧値への換算式は下記の通り
	'
	'	V	= P [Pa] / 1333.22
	'
	'	20211008下記は故障したもの
	' xxxx	AIX02	626B12TBE		ﾊﾞﾗﾄﾛﾝ真空計
	' xxxx	0～10V/0～13.3322 [KPa]
	'*****
	Public Function cvtp2PIDset			_
	(						_
		ByVal pa		As Double,	_
		ByRef v			As Double	_
	)	As Short

		' raw;		AI読み取り値

		Dim volt		As Double
		Dim torr		As Double
		Dim raw			As Short


'

'	20211008 y.goto
'	バラトロン圧力計表示器変更に伴う修正 PDR2000 -> PR4000B
'
		' 圧力[Pa]から電圧りへ変換
		volt			= pa / 1333.22

''''		' 圧力からtorrへ変換						
''''		torr			= Pa2Torr( pa )

''''		' torrから電圧値へ変換						
''''		volt			= Torr2Volt( torr )

		v			= volt

		' 電圧値から RAW データに変換					
		raw			= anav2r2( volt )



		Return raw

	End Function



	'*****
	'	ピラニー真空計の読み取り値をＴｏｒｒに変換
	'	AIX02	945		ﾋﾟﾗﾆｰ真空計
	'	0～10V/1000Pa
	'*****
	Public Function cvtr2GP_Torr			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' raw;		AI読み取り値

		Dim volt		As Double
		Dim pa			As Double
		Dim torr		As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		' ＭＫＳ社製・９４５ピラニ真空計・ﾓﾆﾀ電圧からＰａへの変換	
		pa			= cvtv2p( volt )

		' ＰａからＴｏｒｒへの変換
		torr			= Pa2Torr( pa )



		Return torr

	End Function



	'*****
	'	ピラニー真空計の読み取り値をＴｏｒｒに変換
	'	AIX02	945		ﾋﾟﾗﾆｰ真空計
	'	0～10V/1000Pa
	'*****
	Public Function cvtr2GP_Torr			_
	(						_
		ByVal raw		As UShort,	_
		ByRef v			As Double	_
	)	As Double

		'raw;		AI読み取り値
		'v;		RAW->電圧変換値

		Dim volt		As Double
		Dim pa			As Double
		Dim torr		As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		v			= volt

		' ＭＫＳ社製・９４５ピラニ真空計・ﾓﾆﾀ電圧からＰａへの変換	
		pa			= cvtv2p( volt )

		' ＰａからＴｏｒｒへの変換
		torr			= Pa2Torr( pa )



		Return torr

	End Function



	'*****
	'	ピラニー真空計の読み取り値をＰａに変換
	'	AIX02	945		ﾋﾟﾗﾆｰ真空計
	'	0～10V/1000Pa
	'*****
	Public Function cvtr2GP_Pa			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' raw;		AI読み取り値

		Dim volt		As Double
		Dim pa			As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		' ＭＫＳ社製・９４５ピラニ真空計・ﾓﾆﾀ電圧からＰａへの変換	
		pa			= cvtv2p( volt )



		Return pa

	End Function



	'*****
	'	ピラニー真空計の読み取り値をＰａに変換
	'	AIX02	945		ﾋﾟﾗﾆｰ真空計
	'	0～10V/1000Pa
	'*****
	Public Function cvtr2GP_Pa			_
	(						_
		ByVal raw		As UShort,	_
		ByRef v			As Double	_
	)	As Double

		' raw;		AI読み取り値

		Dim volt		As Double
		Dim pa			As Double



		' 電圧値に変換							
		volt			= anar2v( raw )

		v			= volt

		' ＭＫＳ社製・９４５ピラニ真空計・ﾓﾆﾀ電圧からＰａへの変換	
		pa			= cvtv2p( volt )



		Return pa

	End Function



	'/*****
	'	サ－モチラ－測定温度へ換算
	'	SMC製:CH1,CH2	-100[℃]～+100[℃] /  -10[V]～+10[V]
	'*****/
	Public Function cvtr2SCR			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		' raw;		/* AI読み取り値			*/

		Dim volt		As Double
		Dim dat			As Double



		'/* 電圧値に変換				*/
		volt			= anar2v( raw )

		'/* 電圧値から温度に換算			*/
		dat			= volt * ( ( +100.0 - -100.0 ) / 20.0 )


		Return ( dat )

	End Function



	'/*****
	'	サ－モチラ－測定温度へ換算
	'	SMC製:CH1,CH2	-100[℃]～+100[℃] /  -10[V]～+10[V]
	'*****/
	Public Function cvtr2SCR			_
	(						_
		ByVal raw		As UShort,	_
		ByRef v			As Double	_
	)	As Double

		' raw;		/* AI読み取り値			*/
		' v;		/* RAW->電圧変換値		*/

		Dim volt		As Double
		Dim dat			As Double



		' 電圧値に変換
		volt			= anar2v( raw )

		v			= volt

		' 電圧値から温度に換算
		dat			= volt * ( ( +100.0 - -100.0 ) / 20.0 )



		Return ( dat )

	End Function



	'/*****
	'	サ－モチラ－設定温度への換算
	'	AOUT1,2:リモート温度設定
	'	SMC製:CH1,CH2	-10[V]～+10[V] /  -100[℃]～+100[℃]
	'*****/
	Public Function cvtt2SCR			_
	(						_
		ByVal tmp		As Double,	_
		ByRef v			As Double	_
	)	As Short

		' tmp;		設定温度
		' v;		RAW->電圧変換値

		Dim volt		As Double
		Dim raw			As Short



		' 設定温度を電圧値に変換
		volt			= tmp * ( ( 10.0 - -10.0 ) / ( +100.0 - -100.0 ) )

		v			= volt

		' 電圧値から RAW データに換算
		raw			= anav2r1( volt )



		Return ( raw )

	End Function



	'/*****
	'	電源出力電圧ﾓﾆﾀを出力電圧値へ換算
	'	ESC-2000	-1000[V]～+1000[V] / -10[V]～+10[V]
	'*****/
	Public Function cvtr2ESC			_
	(						_
		ByVal raw		As UShort	_
	)	As Double

		 'raw;		AI読み取り値

		Dim volt		As Double
		Dim dat			As Double



		' 電圧値に変換
		volt			= anar2v( raw )

		' 電圧値からＥＳＣ出力電圧値に換算
		dat			= volt * 100.0



		Return ( dat )

	End Function



	'/*****
	'	電源出力電圧ﾓﾆﾀを出力電圧値へ換算
	'	ESC-2000	-1000[V]～+1000[V] / -10[V]～+10[V]
	'*****/
	Public Function cvtr2ESC			_
	(						_
		ByVal raw		As UShort,	_
		ByRef v			As Double	_
	)	As Double

		'raw;		AI読み取り値
		'v;		RAW->電圧変換値

		Dim volt		As Double
		Dim dat			As Double



		' 電圧値に変換
		volt			= anar2v( raw )

		v			= volt

		' 電圧値からＥＳＣ出力電圧値に換算
		dat			= volt * 100.0



		Return ( dat )

	End Function



	'/*****
	'	ＥＳＣ・Ｖ１外部設定信号の電圧値からＲＡＷデ－タへ換算
	'	ESC-2000	-10[V]～+10[V] / -1000[V]～+1000[V]
	'*****/
	Public Function cvtv2ESC			_
	(						_
		ByVal v1		As Double,	_
		ByRef v			As Double	_
	)	As Short

		' v1;		V1電圧値
		' v;		RAW->電圧変換値

		Dim volt		As Double
		Dim raw			As Short



		' ＥＳＣ設定電圧から出力電圧値に換算
		volt			= v1 * ( ( +10.0 - -10.0 ) / ( +1000.0 - -1000.0 ) )

		v			= volt

		' 電圧値から RAW データに換算
		raw			= anav2r1( volt )



		Return (raw)

	End Function

End Module
