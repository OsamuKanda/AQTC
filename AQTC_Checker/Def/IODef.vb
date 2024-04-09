
Module IODef


	Public Const DIO_ON			= 1

	Public Const DIO_OFF			= 0
 
	Public Const ADnCH			= 8

	Public Const DInCH			= 10

	'１チャネルあたりの平均化回数
	Public Const ADnAVG			= 5

	'流量の平均化回数
	Public Const MFCAVG			= 100

	'リングバッファの長さ
	'Public Const ADnRNG = 100
' 20140207 y.goto
'	Public Const ADnRNG			= 200
	Public Const ADnRNG			= 1000


	'パトライト点灯、点滅指示フラグ
	'bit0	赤・点灯指示フラグ
	'bit1	赤・点滅指示フラグ
	'bit2	黄・点灯指示フラグ
	'bit3	黄・点滅指示フラグ
	'bit4	緑・点灯指示フラグ
	'bit5	緑・点滅指示フラグ
	Public Const PTLctlREDon		= &H1

	Public Const PTLctlREDfl		= &H2

	Public Const PTLctlYELon		= &H4

	Public Const PTLctlYELfl		= &H8

	Public Const PTLctlGRNon		= &H10

	Public Const PTLctlGRNfl		= &H20



	'パトライト点滅動作・ON期間	10ms
	Public Const PTLonCLK			= 50

	'パトライト点滅動作・OFF期間	10ms
	Public Const PTLoffCLK			= 50

	'電極モード
	Public Const POL_MON			= 0
	Public Const POL_DIE			= 1

	'測定機器
	Public Const MES_IOS			= 0
	Public Const MES_ESC			= 1

	'電極接続
	Public Const CON_OFF			= -1

' 20140218 使用しないようにする
'	Public Const CON_ON			= 1

	' ダイポール電極接続  ａ・内ー外
	Public Const CON_IN_OUT			= 1

	' ダイポール電極接続  ｂ・ウエハー内
	Public Const CON_WAF_IN			= 2

	' ダイポール電極接続  ｃ・ウエハー外
	Public Const CON_WAF_OUT		= 3

	' ダイポール電極接続  ｄ・内＋外ー基材
	Public Const CON_INOUT_BASE		= 4

	' ダイポール電極接続  ｅ・内ー基材
	Public Const CON_IN_BASE		= 5

	' ダイポール電極接続  ｆ・外ー基材
	Public Const CON_OUT_BASE		= 6

	' ダイポール電極接続  ｇ・ウエハー基材
	Public Const CON_WAF_BASE		= 7

	Public Const CON_POLE_MAX		= 7       '接続数  20131222

	'ＰＩＤ接続
	Public Const PID_ON			= 1
	Public Const PID_OFF			= 0



	'/*****
	'	ＡＤ基板－ＡＩチャネル割当 (PCI-3135)
	'
	'	・分解能１６ビット
	'	・チャネル数１６ＣＨのうち、１～８ＣＨを配線
	'	・入力レンジは全チャネルとも －１０Ｖ～＋１０Ｖ 固定
	'*****/

	'
	'	AIX01 EXAIO AIN1	ﾊﾞﾗﾄﾛﾝ真空計測定値
	'		ウエハ裏面圧力測定
	'
	'	0～5[V]	/ 13.3KPa
	'
	Public Const GMaiPRS			= 1


	'
	'	AIX02 EXAIO AIN2	ﾋﾟﾗﾆ真空計測定値
	'		チャンバ内の真空度測定
	'
	'	0～5[V] / 13KPa
	'
	Public Const GPaiPRS			= 2


	'
	'	AIX03 EXAIO AIN3	MFC1流量モニタ信号
	'
	'	0～5[V]		0～50CCM
	'
	Public Const MFCaiFLW			= 3


	'
	'	AIX04 EXAIO AIN4	ESC-2000･CH1出力電圧ﾓﾆﾀ信号
	'
	'   	-10～+10[V]	-1000～+1000[V]
	'
	Public Const ESCaiMON1			= 4


	'
	'	AIX05 EXAIO AIN5	ESC-2000･CH2出力電圧ﾓﾆﾀ信号
	'
	'	-10～+10[V]	-1000～+1000[V]
	'
	Public Const ESCaiMON2			= 5


	'
	'	AIX06 EXAIO AIN6	サーモチラー･CH1ﾓﾆﾀ出力
	'
	'   	-10～+10[V]	-100～+100℃
	'
	Public Const SCRaiTMP1			= 6


	'
	'	AIX07 EXAIO AIN7	サーモチラー･CH2ﾓﾆﾀ出力
	'
	'	-10～+10[V]	-100～+100℃
	'
	Public Const SCRaiTMP2			= 7


	'
	'	AIX08 EXAIO AIN8		(予備)
	'
	Public Const AIrsv1			= 8



	'/*****
	'	ＤＡ基板－ＡＯチャネル割当 (PCI-3310)
	'
	'	・分解能１６ビット
	'	・チャネル数４ＣＨで、全て使用
	'	・入力レンジは全チャネル毎に設定可能
	'*****/

	'
	'	AOX01 EXAO1 VOUT1	PID･RSP設定
	'
	'	PID調節計のリモートセットポイント(RSP)の設定値出力
	'	設定するウエハ裏面圧力をバラトロン真空計の測定電圧に換算する
	'	V = 0.5 × log10( 100 × ( 圧力値[Pa] / 133.32 ) ×1e+3 )
	'	※PID調節計の設定 C10, C11, C12 による
	'
	'	0～10[V] / 0.000～9.999
	'
	Public Const PIDaoRSP			= 1


	'
	'	AOX02 EXAO1 VOUT2	MFC･CONTROL1
	'
	'	MFC1の流量設定信号のPCからの設定出力
	'	※RYFCがOFFの時に VOUT2とMFC1が接続され、本信号が有効となる
	'
	'	0～5[V] / 0～50CCM
	'
	Public Const MFCaoSETPT1		= 2

	'
	'	AOX03 EXAO1 VOUT3	ESC-2000･CH1出力電圧設定
	'
	'	-10～+10[V]	-1000～+1000[V]
	'
	Public Const ESCaoVOLT1			= 3

	'
	'	AOX04 EXAO1 VOUT4	ESC-2000･CH2出力電圧設定
	'
	'	-10～+10[V]	-1000～+1000[V]
	'
	Public Const ESCaoVOLT2			= 4



	'/*****
	'	ＤＡ基板－ＡＯチャネル割当 (PCI-340416)
	'
	'	・分解能１６ビット
	'	・チャネル数４ＣＨで、３ＣＨ使用
	'	・入力レンジは全チャネル毎に設定可能
	'*****/

	'
	'	AOX05 EXAO2 AOUT1	サーモチラー･ﾘﾓｰﾄCH1温度設定
	'
	'	-10～+10[V]	-100～+100℃
	'
	Public Const SCRaoREMOTE1		= 5

	'
	'	AOX06 EXAO2 AOUT2	サーモチラー･ﾘﾓｰﾄCH2温度設定
	'
	'	-10～+10[V]	-100～+100℃
	'
	Public Const SCRaoREMOTE2		= 6

	'
	'	AOX07 EXAO2 AOUT3	MFC･CONTROL2
	'	※2014-01-09 現時点でMFC2は未使用(未接続)
	'
	'	0～5[V] / 0～??CCM
	'
	Public Const MFCaoSETPT2		= 7


	'
	'	AOX08 EXAO2 AOUT4	予備
	'
	'	-10～+10[V]
	'
	Public Const AOrsv1			= 8



	'/*****
	'	ＤＩＯ基板－ＤＩチャネル割当 (PCI-2760C)
	'*****/

	' DIX01 EXDIO IN1		ESC-2000･CH1正常/異常
	Public Const ESCdiERR1			= 1

	' DIX02 EXDIO IN2		ESC-2000･CH2正常/異常
	Public Const ESCdiERR2			= 2

	' DIX03 EXDIO IN3		ESC-2000･CH1ｽﾃｰﾀｽ
	Public Const ESCdiSTAT1			= 3

	' DIX04 EXDIO IN4		ESC-2000･CH2ｽﾃｰﾀｽ
	Public Const ESCdiSTAT2			= 4

	' DIX05 EXDIO IN5		サーモチラー･CH1運転中信号
	Public Const SCRdiNORMAL1		= 5

	' DIX06 EXDIO IN6		サーモチラー･CH2運転中信号
	Public Const SCRdiNORMAL2		= 6

	' DIX07 EXDIO IN7		サーモチラー･CH1異常信号(運点停止)
	Public Const SCRdiTROUBLE1		= 7

	' DIX08 EXDIO IN8		サーモチラー･CH2異常信号(運点停止)
	Public Const SCRdiTROUBLE2		= 8

	' DIX09 EXDIO IN9		サーモチラー･CH1温度OK信号
	Public Const SCRdiTOK1			= 9

	' DIX10 EXDIO IN10		サーモチラー･CH2温度OK信号
	Public Const SCRdiTOK2			= 10

	' DIX EXDIO IN11	(予備)
	Public Const MAEdiRSV1			= 11

	' DIX EXDIO IN12	(予備)
	Public Const MAEdiRSV2			= 12

	' DIX EXDIO IN13	(予備)
	Public Const MAEdiRSV3			= 13

	' DIX EXDIO IN14	(予備)
	Public Const MAEdiRSV4			= 14

	' DIX EXDIO IN15	(予備)
	Public Const MAEdiRSV5			= 15

	' DIX EXDIO IN16	(予備)
	Public Const MAEdiRSV6			= 16

	' DIX EXDIO IN17	(予備)
	Public Const MAEdiRSV7			= 17

	' DIX EXDIO IN18	(予備)
	Public Const MAEdiRSV8			= 18

	' DIX EXDIO IN19	(予備)
	Public Const MAEdiRSV9			= 19

	' DIX EXDIO IN20	(予備)
	Public Const MAEdiRSV10			= 20

	' DIX EXDIO IN21	(予備)
	Public Const MAEdiRSV11			= 21

	' DIX EXDIO IN22	(予備)
	Public Const MAEdiRSV12			= 22

	' DIX EXDIO IN23	(予備)
	Public Const MAEdiRSV13			= 23

	' DIX EXDIO IN24	(予備)
	Public Const MAEdiRSV14			= 24

	' DIX EXDIO IN25	(予備)
	Public Const MAEdiRSV15			= 25

	' DIX EXDIO IN26	(予備)
	Public Const MAEdiRSV16			= 26

	' DIX EXDIO IN27	(予備)
	Public Const MAEdiRSV17			= 27

	' DIX EXDIO IN28	(予備)
	Public Const MAEdiRSV18			= 28

	' DIX EXDIO IN29	(予備)
	Public Const MAEdiRSV19			= 29

	' DIX EXDIO IN30	(予備)
	Public Const MAEdiRSV20			= 30

	' DIX EXDIO IN31	(予備)
	Public Const MAEdiRSV21			= 31

	' DIX EXDIO IN32	(予備)
	Public Const MAEdiRSV22			= 32



	'/*****
	'		ＤＩＯ基板－ＤＯチャネル割当 (PCI-2760C)
	'*****/

	' DOX01 EXDIO OUT1	ｼﾝｸﾞﾅﾙﾀﾜｰBUZZER1
	Public Const MAEdoBZ1			= 1

	' 20200205 y.goto
	' １号機では２種類ブザーが有ったが２号機では１種類
	' DOX02 EXDIO OUT2	ｼﾝｸﾞﾅﾙﾀﾜｰBUZZER2
'	Public Const MAEdoBZ2			= 2

	' DOX03 EXDIO OUT3	ｼﾝｸﾞﾅﾙﾀﾜｰRED
	Public Const MAEdoLEDR			= 3

	' DOX04 EXDIO OUT4	ｼﾝｸﾞﾅﾙﾀﾜｰYELLOW
	Public Const MAEdoLEDY			= 4

	' DOX05 EXDIO OUT5	ｼﾝｸﾞﾅﾙﾀﾜｰGREEN
	Public Const MAEdoLEDG			= 5

	' DOX06 EXDIO OUT6	真空ﾎﾟﾝﾌﾟ制御用
	Public Const MAEdoPUMP			= 6

	' DOX07 EXDIO OUT7	MFCﾊﾞﾙﾌﾞ強制ｵｰﾌﾟﾝ
	Public Const MAEdoFOPN			= 7

	' DOX08 EXDIO OUT8	MFCﾊﾞﾙﾌﾞ強制ｸﾛｰｽﾞ
	Public Const MAEdoFCLS			= 8

	' DOX09 EXDIO OUT9	ESC-2000･CH1外部ｲﾝﾀｰﾛｯｸ信号
	Public Const ESCdoITL1			= 9

	' DOX10 EXDIO OUT10	ESC-2000･CH2外部ｲﾝﾀｰﾛｯｸ信号
	Public Const ESCdoITL2			= 10

	' DOX11 EXDIO OUT11	ESC-2000･CH1外部起動信号
	Public Const ESCdoSTART1		= 11

	' DOX12 EXDIO OUT12	ESC-2000･CH2外部起動信号
	Public Const ESCdoSTART2		= 12

	' DOX13 EXDIO OUT13	サーモチラー･ﾘﾓｰﾄ運転ﾓｰﾄﾞ切替信号
	Public Const MAEdoSRMT			= 13

	' DOX14 EXDIO OUT14	サーモチラー･CH1 運転開始/停止信号
	Public Const MAEdoS1RUN			= 14

	' DOX15 EXDIO OUT15	サーモチラー･CH2 運転開始/停止信号
	Public Const MAEdoS2RUN			= 15

	' DOX16 EXDIO OUT16	PID 運転ｽﾀｰﾄ/ｽﾄｯﾌﾟ
	Public Const MAEdoPIDSTART		= 16

	' DOX17 EXDIO OUT17	PID LSP/RSP切替
	Public Const MAEdoPIDRSP		= 17

	' DOX18 EXDIO OUT18	MFC設定入力切替用リレー
	Public Const MAEdoRYFC			= 18

	' DOX19 EXDIO OUT19	高耐圧ﾘﾚｰ･ﾓﾉﾎﾟｰﾙ､ﾀﾞｲﾎﾟｰﾙ電極ﾍｯﾄﾞ接続切替
	'	OFF=絶縁抵抗計, ON=ESC-2000･CH1(+)
	Public Const MAEdoRYHV01		= 19

	' DOX20 EXDIO OUT20	高耐圧ﾘﾚｰ･ﾓﾉﾎﾟｰﾙ専用電極ﾍｯﾄﾞ接続切替
	'	OFF=絶縁抵抗計, ON=ESC-2000･CH1(-)
	Public Const MAEdoRYHV02		= 20

	' DOX21 EXDIO OUT21	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ専用電極ﾍｯﾄﾞ接続切替
	'	OFF=絶縁抵抗計, ON=ESC-2000･CH2(+)
	Public Const MAEdoRYHV03		= 21

	' DOX22 EXDIO OUT22	高耐圧ﾘﾚｰ･ﾓﾉﾎﾟｰﾙ(+)
	Public Const MAEdoRYHV10		= 22

	' DOX23 EXDIO OUT23	高耐圧ﾘﾚｰ･ﾓﾉﾎﾟｰﾙ(-)
	Public Const MAEdoRYHV11		= 23

	' DOX24 EXDIO OUT24	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･ｳｴﾊ CH1
	Public Const MAEdoRYHV12		= 24

	' DOX25 EXDIO OUT25	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･ｳｴﾊ CH2
	Public Const MAEdoRYHV13		= 25

	' DOX26 EXDIO OUT26	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･電極外(+) CH1
	Public Const MAEdoRYHV14		= 26

	' DOX27 EXDIO OUT27	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･電極外(+) CH2
	Public Const MAEdoRYHV15		= 27

	' DOX28 EXDIO OUT28	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･電極内(-) CH1
	Public Const MAEdoRYHV16		= 28

	' DOX29 EXDIO OUT29	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･電極内(-) CH2
	Public Const MAEdoRYHV17		= 29

	' DOX30 EXDIO OUT30	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･基材(-) CH1
	Public Const MAEdoRYHV18		= 30

	' DOX31 EXDIO OUT31	高耐圧ﾘﾚｰ･ﾀﾞｲﾎﾟｰﾙ･基材(-) CH2
	Public Const MAEdoRYHV19		= 31

	'	※ 20200901 本バルブはトーカロ様では未装着
	'	DOX32 EXDIO OUT32	ウエハ裏面圧開放バルブ制御
	'
	'	ON	ウエハ裏面と真空ポンプを接続して裏面圧力を排気する
	'	OFF	接続閉
	'
	Public Const MAEdoPRG			= 32

	'/*****
	'		EXDIO2基板－ＤＯチャネル割当 (PCI-2760C)
	'*****/

	' DOX33 MV EXDIO2 OUT1	RYE1 SV1 MV
	Public Const EXSdoRYE1			= 33

	' DOX34 G1 EXDIO2 OUT2	RYE2 SV2 MFC 2次側バルブ開
	Public Const EXSdoRYE2			= 34

	' DOX35 G4 EXDIO2 OUT3	RYE3 SV3 MFC 配管真空引きバルブ開
	Public Const EXSdoRYE3			= 35

	' DOX36 LV EXDIO2 OUT4	RYE4 SV4 MFC LV開
	Public Const EXSdoRYE4			= 36

	' DOX37 XX EXDIO2 OUT5	RYE5 (予備)
	Public Const EXSdoRYE5			= 37

	' DOX38 MB EXDIO2 OUT6	RYMB MS-053メカニカルブースタポンプON/OFF制御
	Public Const EXSdoMBP			= 38


	'/*****
	'		EXDIO2基板－DIチャネル割当 (PCI-2760C)
	'*****/

	' DIX33 EXDIO2 IN1	TH1 ドライポンプ異常信号
	Public Const EXSdiTH1			= 33

	' DIX34 EXDIO2 IN2	TH2 メカニカルブースタポンプ異常信号
	Public Const EXSdiTH2			= 34

	' DIX35 EXDIO2 IN3	MC1 ドライポンプ運転表示信号
	Public Const EXSdiMC1			= 35

	' DIX36 EXDIO2 IN4	MC2 メカニカルブースタポンプ運転表示信号
	Public Const EXSdiMC2			= 36

	' DIX37 EXDIO2 IN5	PXS1 ＭＶ開　近接信号
	Public Const EXSdiPXS1			= 37

	' DIX38 EXDIO2 IN6	PXS2 ＭＶ閉　近接信号
	Public Const EXSdiPXS2			= 38

	' DIX39 EXDIO2 IN7	PS1 圧縮エアー元圧信号
	Public Const EXSdiPS1			= 39

	' DIX40 EXDIO2 IN8	PS2 大気圧信号
	Public Const EXSdiPS2			= 40

	' DIX41 EXDIO2 IN9PS3	PS3 Ｈｅ元圧信号
	Public Const EXSdiPS3			= 41

	' DIX42 EXDIO2 IN10	(DI予備1)
	Public Const EXSdiRSV1			= 42


	' ＧＰＩＢボード＃１
	' Interface社製 PCI-4302
	Public Const GPIB1DLM			= "CRLF+EOI"

	Public Const GPIB1TOUT			= 3000           'ﾀｲﾑｱｳﾄ ms



End Module
