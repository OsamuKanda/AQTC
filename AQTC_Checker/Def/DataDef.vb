

Module DataDef



	'
	'	ﾃﾞｰﾀﾌｧｲﾙ･ﾃﾞｰﾀﾍｯﾀﾞ
	'
	Public Structure DTH


		' 注  番
		Public tno		As String

		' 作  番
		Public mno		As String

		' Ｓ／Ｎ
		Public sno		As String

		' 電極サイズ
		Public inc		As String

		' 電極種類
		Public vo		As String

		' 試験開始日時
		Public sdt		As String

		' 20140123 追加
		' 試験開始ＰＣシステムデイト
		Public pcdt		As String

		' 総合判定結果
		Public okng		As Integer


	End Structure



	'
	'	絶縁耐圧測定条件、測定データ
	'
	Public Structure DTI1


		'
		'	測定条件設定値
		'	絶縁抵抗計測定電圧
		'
		Public volt		As Double

		'
		'	絶縁抵抗計測定値・電流値
		'
		Public amp		As Double

		'
		'	絶縁抵抗計測定値・抵抗値
		'
		Public om		As Double

		'
		'	判定基準
		'
		Public bs		As Double

		'
		'	判定結果
		'
		Public okng		As Integer

		'
		'	チャンバ内圧力（ピラニー真空計）測定値
		'
		Public vq		As Double


	End Structure



	'
	'	絶縁耐圧測定情報
	'
	Public Structure DTZT


		'
		'	高耐圧リレー切り替えポイント番号
		'	絶縁抵抗計の電圧印加箇所
		'
		Public posv		As Integer


		'
		'	絶縁耐圧測定電圧ステップ数
		'
		'	例) 300V, 400V, 500V, 600V, 1000Vで
		'	測定する場合は dsiz は ４ になる
		'
		Public dsiz		As Integer

		'
		'	絶縁耐圧測定条件、測定データ
		'
		'	例)
		'	d( 0 )	300Vでの絶縁耐圧測定
		'	d( 1 )	400Vでの絶縁耐圧測定
		'	d( 2 )	500Vでの絶縁耐圧測定
		'	   :		:
		'
		Public d()		As DTI1


	End Structure



	'
	'	ウエハ吸着力測定条件、測定データ
	'
	Public Structure DTI2


		'
		'	測定条件設定値
		'	ＳＤＣ電源印加電圧 CH1
		'
		Public volt1		As Double

		'
		'	測定条件設定値
		'	ＳＤＣ電源印加電圧 CH2
		'
		Public volt2		As Double

		' 裏面圧力　実測値
		'Public tor()		As Double

		'
		'	ウエハ裏面圧力測定値
		'
		'	途中でウエハ剥離等で失敗した場合は
		'	３回までリトライを行う。
		'	１回で成功した場合はそこで測定終了する。
		'
		'	pa( 0 )	１回目測定値
		'	pa( 1 )	２回目測定値
		'	pa( 2 )	３回目測定値
		'
		'	20201102 裏面圧の到達時間測定に変更のため未使用
		Public pa()		As Double

		'
		'	判定基準
		'	20201102 裏面圧から到達時間測定に変更
		'
		Public bs		As Double

		'
		'	ウエハ裏面圧力到達時間
		'
		'	20201102 S_Harada 裏面圧の到達時間測定に変更
		'	nは裏面圧1kPa、2kPa、3kPa、4kPa、6kPaの5箇所
		'	±500Vで3KPaが判定エラーなら
		'	３回までリトライを行う。
		'	１回で成功した場合はそこで測定終了する。
		'
		'	tmr( n )	１回目測定値
		'
		'Public tmr		As Double
		Public tmr( )		As Double

		'
		'	判定結果
		'
		Public okng		As Integer


		'
		'   20200716 s.harada
		'	ヘリウム流量 トーカロ仕様追加
		'
		Public he		As Double


	End Structure



	'
	'	ウエハ吸着力測定情報
	'
	Public Structure DTKT


		'
		'	高耐圧リレー切り替えポイント番号
		'	ＳＤＣ電源の電圧印加箇所
		'
		Public posv		As Integer

		'
		'	ウエハ吸着力測定ＳＤＣ印加電圧ステップ数
		'
		'	例) SDC印加電圧が ±400V, ±600V で測定する
		'	場合は dsiz は ２ になる
		'
		Public dsiz		As Integer

		'
		'	ウエハ吸着力測定条件、測定データ
		'
		'	例)
		'	d( 0 )	±400Vでの絶縁耐圧測定
		'	d( 1 )	±600Vでの絶縁耐圧測定
		'	   :		:
		'
		Public d()		As DTI2


	End Structure



	'
	'	Ｈｅリーク量測定条件、測定データ
	'
	Public Structure DTI3


		'
		'	測定条件設定値
		'	ＳＤＣ電源印加電圧 CH1
		'
		Public volt1		As Double

		'
		'	測定条件設定値
		'	ＳＤＣ電源印加電圧 CH2
		'
		Public volt2		As Double

		'▼2024.05.02 TC Kanda (測定有効無効パラメータ追加)
		Public ptn As List(Of String)
		'▲2024.05.02 TC Kanda (測定有効無効パラメータ追加)

		'
		'	測定値・Ｈｅリーク量
		'	ＭＦＣ流量モニタ信号電圧値
		'
		'	20201102 s.harada	裏面圧1kPa、2kPa、3kPa、4kPa、6kPaの5箇所の測定に変更
		'Public mfcvolt		As Double
		Public mfcvolt()		As Double

		'
		'	測定値・Ｈｅリーク量
		'	ＭＦＣ流量モニタ信号流量換算値
		'
		'	20201102 s.harada	裏面圧1kPa、2kPa、3kPa、4kPa、6kPaの5箇所の測定に変更
		'	判定エラーなら３回までリトライを行う。
		'	１回で成功した場合はそこで測定終了する。
		'
		'Public cm		As Double
		Public cm()		As Double

		'
		'	測定条件設定値
		'	判定基準
		'
		Public bs		As Double

		'
		'	判定結果
		'
		Public okng		As Integer

		'	20201102 s.harada	判定基準２、判定結果２を追加
		Public bs2		As Double

		Public okng2		As Integer

		'	20201102 s.harada	bpは検査方法変更で削除
		'   20200716 s.harada
		'	ウエハ裏面圧 トーカロ仕様追加
		'
		'Public bp		As Double

	End Structure



	'
	'	Ｈｅリーク量測定情報
	'
	Public Structure DTHG


		'
		'	高耐圧リレー切り替えポイント番号
		'	ＳＤＣ電源電圧印加箇所
		'
		Public posv		As Integer


		'
		'	Ｈｅリーク量測定ＳＤＣ印加電圧ステップ数
		'
		'	例) SDC印加電圧が ±400V, ±600V で測定する
		'	場合は dsiz は ２ になる
		'
		Public dsiz		As Integer


		'
		'	Ｈｅリーク量測定条件、測定データ
		'
		'	例)
		'	d( 0 )	±400VでのＨｅリーク量測定
		'	d( 1 )	±600VでのＨｅリーク量測定
		'	   :		:
		'
		Public d()		As DTI3


	End Structure



	'
	'   20200716 s.harada
	'	残留吸着測定条件、測定データ トーカロ仕様追加
	'
	Public Structure DTI4


		'
		'	測定条件設定値
		'	ＳＤＣ電源印加電圧 CH1
		'
		Public volt1		As Double

		'
		'	測定条件設定値
		'	ＳＤＣ電源印加電圧 CH2
		'
		Public volt2		As Double

		'
		'	ウエハ裏面圧力測定値
		'
		'	pc	印加前測定値
		'	pe	印加中測定値
		'	pd	印加後測定値
		'	pdc	圧力差計算値
		'
		Public pc		As Double

		Public pe		As Double

		Public pd		As Double

		Public pdc		As Double

		'
		'	測定条件設定値
		'	判定基準
		'
		Public bs		As Double

		'
		'	判定結果
		'
		Public okng		As Integer

	End Structure



	'
	'   20200716 s.harada
	'	残留吸着測定情報 トーカロ仕様追加
	'
	Public Structure DTZK


		'
		'	高耐圧リレー切り替えポイント番号
		'	ＳＤＣ電源電圧印加箇所
		'
		Public posv		As Integer


		'
		'	残留吸着測定ＳＤＣ印加電圧ステップ数
		'
		'	例) SDC印加電圧が ±400V, ±600V で測定する
		'	場合は dsiz は ２ になる
		'
		Public dsiz		As Integer


		'
		'	残留吸着測定条件、測定データ
		'
		'	例)
		'	d( 0 )	±400Vでの残留吸着測定
		'	d( 1 )	±600Vでの残留吸着測定
		'	   :		:
		'
		Public d()		As DTI4


	End Structure



	'
	'	絶縁耐圧、ウエハ吸着力、Ｈｅリーク量測定情報
	'
	Public Structure DTREC


		'
		'	20140123 追加 y.goto
		'	サーモチラー使用設定
		'	( 0 )	CH1
		'	( 1 )	CH2
		'	値0	使用しない
		'	値1	使用する
		'
		Public schuse()		As Integer


		'
		'	20140123 追加 y.goto
		'	サーモチラー配管接続変更有無
		'
		Public schchg		As Integer

		'
		'	サーモチラー設定温度
		'
		Public tmp()		As Double

		'
		'	絶縁・耐圧試験データ数
		'	接続パターン数
		'
		'	例) 次の場合は ４ となる
		'		1 電極(内)～(外)	-> t1( 0 )
		'		2 ウエハ～電極(内)	-> t1( 1 )
		'		3 ウエハ～電極(外)	-> t1( 2 )
		'		4 電極(内,外)～基材	-> t1( 3 )
		'
		Public t1siz		As Integer


		'
		'	絶縁耐圧測定情報
		'
		Public t1()		As DTZT


		'
		'	ウエハ吸着力測定情報
		'
		Public t2		As DTKT


		'
		'	Ｈｅリーク量測定情報
		'
		Public t3		As DTHG



		'
		'   20200716 s.harada
		'	残留吸着測定情報 トーカロ仕様追加
		'
		Public t4		As DTZK


	End Structure



	'
	'	ﾃﾞｰﾀﾌｧｲﾙ･測定データレコード
	'
	Public Structure DHrec


		'
		'	電極ﾍｯﾄﾞﾀｲﾌﾟ
		'	名称を文字列で記録
		'
		Public type		As String

		'
		'	データヘッダ
		'
		Public dh		As DTH

		'
		'	測定データ
		'
		'	dt( 0 )		大気中測定
		'	dt( 1 )		真空測定１
		'	dt( 2 )		真空測定２
		'	dt( 3 )		真空測定３
		'
		Public dt()		As DTREC


	End Structure



End Module
