Module MBPPrc

	'
	'	20200213 y.goto
	'	メカニカルブースターポンプ動作シーケンス
	'
	'	メカニカルブースタポンプ MBS-053 / ULVAC (以後MBPと記述する)の動作シーケンスを記述
	'	MBPは大気圧から動作させてはならず、一定圧に低下してから動作させる必要が有る。
	'
	'	DefMBPSTART	MBP起動開始圧力 [Pa]
	'			MBPが動作開始する圧力を指定する
	'	DefMBPSTOP	MBP停止圧力 [Pa]
	'			MBPが動作停止する圧力を指定する
	'
	'	スレッショルドを持たせる為、起動開始圧力と停止圧力を設けた。
	'
	'	20200219 y.goto
	'	MBPの起動は20Pa以下
	'

	' MBP起動開始圧力 [Pa]
	Const DefMBPSTART	As Double	= 19.0

	' MBP停止圧力 [Pa]
	Const DefMBPSTOP	As Double	= 21.0

End Module
