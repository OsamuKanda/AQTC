Module DHEDDef


	' 真空での試験を行う際の真空圧(Pa)
	Public Const WPRS		= 6.65

	' 真空圧条件になるまでの待ち時間
	Public Const WPRSwaitTm		= 60 * 60 * 100.0

	' Ｈｅ排気の流量設定（ＭＦＣ２の設定電圧）
	Public Const EXPRS		= 1.0

	' ﾒｯｾｰｼﾞ表示行
	Public Const MIPy		= 14


	'
	'	opip()の動作モード
	'

	' (確認) 確認入力
	Public Const OPIPok		= 0

	' (中止 続行) 入力
	Public Const OPIPco		= 1

	' (中止 続行 ｽｷｯﾌﾟ) 入力
	Public Const OPIPcoi		= 2

	' (中止 ﾘﾄﾗｲ) 入力
	Public Const OPIPcr		= 3

	' (Yes No) 入力
	Public Const OPIPyn		= 4

	' (中止 ﾘﾄﾗｲ 続行) 入力
	Public Const OPIPcri		= 5


	' サ－モチラ－目標温度到達待ち時間
	Public Const SCRWAITTM		= 500000.0


	' テストモード
	Public Const TST_AUTO		= 0

	Public Const TST_ZETS		= 1

	Public Const TST_KYUC		= 2

	Public Const TST_HGAS		= 3

	'   20200716 s.harada
	'	トーカロ対応
	Public Const TST_ZKYU		= 4


	'
	'	測定モード
	'
	'	0:自動
	'	1:絶縁耐圧
	'	2:ウエハ吸着力
	'	3:He漏れ量
	'
	Public DHDmd			As Integer


	'
	'	中断フラグ　テストフォームでボタンが押されるとtrueにする
		'各検出モジュールで継続ならfalseにすること
	'
	Public StopFlag			As Boolean = False


	' プログラムＩＤ
	Public PROGID			As String = ""



	''
	''   20200716 s.harada
	''	AQTC対応
	''
	'Public Const SEL_HTC		= 0
	''
	'Public Const SEL_TCL		= 1
	''

End Module
