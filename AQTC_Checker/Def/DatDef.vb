

Module DatDef



	' ESC-2000動作ﾓｰﾄﾞ (0=ﾓﾉﾎﾟｰﾙ / 1=ﾀﾞｲﾎﾟｰﾙ)
	Public ESCmd As Integer

	' 20201102 s.harada	トーカロ専用で未使用
	' ウエハ裏面圧力リミット値
	Public BPRS As Double

	' 20201102 s.harada	トーカロ専用で未使用
	' Heﾘｰｸ測定時のｳｴｱ裏面圧力(PIDのｾｯﾄﾎﾟｲﾝﾄ)
	Public HLPRS As Double

	' サ－モチラ－使用CH　0:CH1 1:CH2 2:両方
	Public SCRUSE As Integer = 0

	' 真空圧試験条件(温度１)
	Public TPRS1 As Double = WPRS

	' 真空圧試験条件(温度２)
	Public TPRS2 As Double = WPRS

	' ウエハ吸着停止するときのウエハ裏面圧
	Public BakPres As Double = 80.0

	'
	'	20140127
	'	電極ヘッド温度安定待ち時間 (分)
	'
	'	サーモチラーの温度安定後、ここで指定した
	'	時間ほど待って検査を開始する
	'
	Public PrmTmpStbW		As Double = 0.0


	'
	'   20200716 s.harada
	'   残留吸着 Ｈｅ流量(ccm)
	'
	Public PrmHeFlow		As Double = 5.0

	'
	'   20200716 s.harada
	'   残留吸着 電圧印加時間(秒)
	'
	Public PrmVoltImp		As Double = 300.0

	'
	'   20200716 s.harada
	'   残留吸着 電圧印加停止後Ｈｅ流すまでの待ち時間(秒)
	'
	Public PrmHeWait		As Double = 0.0

	'
	'   20201102 s.harada
	'   吸着力測定時間上限
	'
	Public PrmMaxTim		As Double = 600

	'
	'   20201102 s.harada
	'   残留吸着 安定判断傾き
	'
	Public PrmStabVct		As Double = 0.1

	'
	'   20201102 s.harada
	'   残留吸着 安定判断時間(秒)
	'
	Public PrmStabTim		As Double = 20

	'
	'   20201124 y.goto
	'	残留吸着電圧印可時裏面圧力 (Pa)
	'
	Public PrmBakPrs		As Double = 1500.0

	'
	'	20210201 追加 y.goto
	'	電圧印可後He導入前裏面圧力 (Pa)
	'
	Public PrmBak2Prc		As Double = 30.0

	' ｼｽﾃﾑ設定･ﾚｺｰﾄﾞﾊﾞｯﾌｧ
	'MSF		SYSrec;

	' ﾃﾞｰﾀﾌｧｲﾙ･測定ﾃﾞｰﾀﾚｺｰﾄﾞﾊﾞｯﾌｧ
	Public MESrec As DHrec


	Public Const DefStrNotUse		As String	= "使用しない"

	Public Const DefStrUse			As String	= "使用する"

	Public Const DefStrTmpNoChg		As String	= "配管接続変更無し"

	Public Const DefStrTmpChg		As String	= "配管接続変更有り"


End Module
