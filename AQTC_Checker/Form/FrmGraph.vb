'*****
'	ＡＩサンプリングデータのグラグ表示
'
'	author		y.goto
'	date		2013-12-25
'	language	vb2010
'
'	<説明>
'	VBﾌｫｰﾑに貼り付けた PictureBox に AIｻﾝﾌﾟﾘﾝｸﾞﾃﾞｰﾀを
'	ﾘｱﾙﾀｲﾑに折れ線ｸﾞﾗﾌとして表示する
'
'	<使用方法>
'	FrmGraphﾌｫｰﾑを使用するﾌﾟﾛｼﾞｪｸﾄに取り込む。
'		追加→既存の項目を選択し以下のﾌｧｲﾙを選択する
'			FrmGraph.Designer.vb
'			FrmGraph.resx
'			FrmGraph.vb
'	FrmGraphﾌｫｰﾑをﾛｰﾄﾞする
'	表示させたいﾃﾞｰﾀが確定し、ｸﾞﾗﾌ表示を更新したいﾀｲﾐﾝｸﾞで
'	RefsGraph 関数を呼び出せばｸﾞﾗﾌ表示が更新される
'
'*****
Public Class FrmGraph



	'
	'	グラフ描画データバッファ
	'
	'	Grpx().X はグラフの横軸なので0,1,2,3 ･･･の値が入る
	'	Grpx().Y は測定データが入る
	''

	' AIX01･ウエハ裏面圧(ﾊﾞﾗﾄﾛﾝ真空計)グラフ描画データバッファ
	Dim GrpBPrs()		As Point

	' AIX01･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinBPRS		As Long

	' AIX01･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxBPRS		As Long

	' AIX01･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopBPRS		As Double

	' AIX01･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmBPRS		As Double



	' AIX02・チャンバ内圧力(ﾋﾟﾗﾆ真空計)グラフ描画データバッファ
	Dim GrpCPrs()		As Point

	' AIX02･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinCPrs		As Long

	' AIX02･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxCPrs		As Long

	' AIX02･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopCPrs		As Double

	' AIX02･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmCPrs		As Double



	' AIX03・He流量(MFC1流量ﾓﾆﾀ信号)グラフ描画データバッファ
	Dim GrpFlwM()		As Point

	' AIX03･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinFlwM		As Long

	' AIX03･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxFlwM		As Long

	' AIX03･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopFlwM		As Double

	' AIX03･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmFlwM		As Double



	' AIX04・SDC電源CH1出力電圧(SDC-CH1電圧ﾓﾆﾀ信号)グラフ描画データバッファ
	Dim GrpSDC1()		As Point

	' AIX04･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinSDC1		As Long

	' AIX04･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxSDC1		As Long

	' AIX04･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopSDC1		As Double

	' AIX04･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmSDC1		As Double



	' AIX05・SDC電源CH2出力電圧(SDC-CH2電圧ﾓﾆﾀ信号)グラフ描画データバッファ
	Dim GrpSDC2()		As Point

	' AIX05･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinSDC2		As Long

	' AIX05･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxSDC2		As Long

	' AIX05･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopSDC2		As Double

	' AIX05･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmSDC2		As Double



	' AIX06・サ－モチラ－CH1温度(温度ﾓﾆﾀ信号)グラフ描画データバッファ
	Dim GrpClt1()		As Point

	' AIX06･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinClt1		As Long

	' AIX06･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxClt1		As Long

	' AIX06･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopClt1		As Double

	' AIX06･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmClt1		As Double



	' AIX07・サ－モチラ－CH2温度(温度ﾓﾆﾀ信号)グラフ描画データバッファ
	Dim GrpClt2()		As Point

	' AIX07･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinClt2		As Long

	' AIX07･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxClt2		As Long

	' AIX07･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopClt2		As Double

	' AIX07･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmClt2		As Double



	' AIX03･He流量・平均値グラフ描画データバッファ
	Dim GrpFlwA()		As Point

	' AIX03･ピクチャボックスの描画領域Ｙ軸最小ドット位置
	Dim GYDMinFlwA		As Long

	' AIX03･ピクチャボックスの描画領域Ｙ軸最大ドット位置
	Dim GYDMaxFlwA		As Long

	' AIX03･Ｙ軸最小ドット位置の目盛の値
	Dim GYVTopFlwA		As Double

	' AIX03･Ｙ軸最大ドット位置の目盛の値
	Dim GYVBtmFlwA		As Double



	' 測定データ・ウエハ裏面圧
	Public MesBkp		As Double

	' 測定データ・チャンバ内圧力
	Public MesCbp		As Double

	' 測定データ・Ｈｅ流量
	Dim MesFlwM		As Double

	' 測定データ・Ｈｅ流量移動平均
	Dim MesFlwA		As Double

	' 測定データ・SDC-CH1電圧
	Dim MesSDCV1		As Double

	' 測定データ・SDC-CH2電圧
	Dim MesSDCV2		As Double

	' 測定データ・サ－モチラ－CH1温度
	Dim MesTmp1		As Double

	' 測定データ・サ－モチラ－CH2温度
	Dim MesTmp2		As Double



	' ブリンク状態フラグ
	Dim BlkFlg		As Integer




	'*****
	'	FrmGraph単体動作確認用関数
	'	本番では削除すること
	'*****
	Private Sub getdata					_
	(							_
		ByRef raw			As Long,	_
		ByRef volt			As Double,	_
		ByRef mdat			As Double,	_
		ch				As Integer,	_
		navg				As Integer	_
	)

		Dim torr			as Double
		Dim pa				as Double



		' リングバッファから移動平均取り出し
		raw		= aiget( ch, navg )

		volt		= anar2v( raw )


		Select Case ch

		'
		'	バラトロン真空計 0～5V
		'
                Case	1

		'	volt			= Rnd( 1 ) * 5.0 

			torr			= Volt2Torr( volt )

			pa			= Torr2Pa( torr )

			mdat			= pa
			' AIX01.Text = pa.ToString("0.00E+00") + "Pa"

		'
		'	ピラニ真空計 0～7V
		'
		Case	2

		'	volt			= Rnd( 1 ) * 5.0 

			mdat			= cvtv2p( volt )

			' AIX02.Text = pa.ToString("0.00E+00") + "Pa"


		'
		'	MFC流量 0～5V
		'
		Case	3

		'	volt			= Rnd( 1 ) * 5.0 

			mdat			= cvtv2Ccm( volt )

			' AIX03.Text = ccm.ToString("0.0") + "CCM"


		'
		'	ESC CH1 出力電圧モニタ ±10V
		'
		Case	4

		'	volt			= Rnd( 1 ) * 20.0 - 10.0 

			mdat			= volt * 100.0

			' AIX04.Text = (adv * 100).ToString("0") + "V"


		'
		'	ESC CH2 出力電圧モニタ ±10V
		'
		Case	5

		'	volt			= Rnd( 1 ) * 20.0 - 10.0 

			mdat			= volt * 100.0

			' AIX05.Text = (adv * 100).ToString("0") + "V"


		'
		'	サ－モチラ－ CH1 温度モニタ電圧出力 ±10V
		'
		Case	6

		'	volt			= Rnd( 1 ) * 20.0 - 10.0 

			mdat			= volt * 10.0

			' AIX06.Text = (adv * 10).ToString("0.0") + "℃"


		'
		'	サ－モチラ－ CH2 温度モニタ電圧出力 ±10V
		'
		Case	7

		'	volt			= Rnd( 1 ) * 20.0 - 10.0 

			mdat			= volt * 10.0

			' AIX07.Text = (adv * 10).ToString("0.0") + "℃"

		End Select


	End Sub



	'*****
	'	グラフ表示フォームのロード、初期化処理
	'*****
	Private Sub FrmGraph_Load( ByVal sender As System.Object,  ByVal e As System.EventArgs) Handles MyBase.Load

		Dim i		As Integer



		'
		'
		'	AIX01･ウエハ裏面圧(ﾊﾞﾗﾄﾛﾝ真空計)グラフ描画関連変数初期化

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpBPrs, CPcbBPrs.Width - 5 )

		' AIX01･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinBPRS		= 10

		' AIX01･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxBPRS		= CPcbBPrs.Height - 20

		' AIX01･Ｙ軸最小ドット位置の目盛の値
		GYVTopBPRS		= 5.0

		' AIX01･Ｙ軸最大ドット位置の目盛の値
		GYVBtmBPRS		= 0.0

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpBPrs.Length - 1

			' Ｘドット位置初期化
			GrpBPrs( i ).X		= i

			' Ｙドット位置初期化
			GrpBPrs( i ).Y		= Cvtydot	_
			(					_
				GYDMinBPRS,			_
				GYDMaxBPRS,			_
				GYVTopBPRS,			_
				GYVBtmBPRS,			_
				0.0				_
			)

		Next i



		'
		'	AIX02	チャンバ内圧力(ﾋﾟﾗﾆ真空計)グラフ描画関連変数初期化
		'

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpCPrs, CPcbCPrs.Width - 5 )

		' AIX02･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinCPrs		= 10

		' AIX02･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxCPrs		= CPcbCPrs.Height - 20

		' AIX02･Ｙ軸最小ドット位置の目盛の値
		GYVTopCPrs		= 7.0
'
' 20200217 y.goto ピラニ真空計スケール変更 0-5V -> 0-7V
'		GYVTopCPrs		= 5.0

		' AIX02･Ｙ軸最大ドット位置の目盛の値
		GYVBtmCPrs		= 0.0

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpCPrs.Length - 1

			' Ｘドット位置初期化
			GrpCPrs( i ).X		= i

			' Ｙドット位置初期化
			GrpCPrs( i ).Y		= Cvtydot	_
			(					_
				GYDMinCPrs,			_
				GYDMaxCPrs,			_
				GYVTopCPrs,			_
				GYVBtmCPrs,			_
				0.0				_
			)

		Next i



		'
		'	AIX03	He流量(MFC1流量ﾓﾆﾀ信号)グラフ描画関連変数初期化
		'

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpFlwM, CPcbFlwM.Width - 5 )

		' AIX03･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinFlwM		= 10

		' AIX03･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxFlwM		= CPcbFlwM.Height - 20

		' AIX03･Ｙ軸最小ドット位置の目盛の値
		GYVTopFlwM		= 5.0

		' AIX03･Ｙ軸最大ドット位置の目盛の値
		GYVBtmFlwM		= 0.0

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpFlwM.Length - 1

			' Ｘドット位置初期化
			GrpFlwM( i ).X		= i

			' Ｙドット位置初期化
			GrpFlwM( i ).Y		= Cvtydot	_
			(					_
				GYDMinFlwM,			_
				GYDMaxFlwM,			_
				GYVTopFlwM,			_
				GYVBtmFlwM,			_
				0.0				_
			)

		Next i



		'
		'	AIX04	SDC電源CH1出力電圧(SDC-CH1電圧ﾓﾆﾀ信号)グラフ描画関連変数初期化
		'

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpSDC1, CPcbSDC1.Width - 5 )

		' AIX04･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinSDC1		= 10

		' AIX04･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxSDC1		= CPcbSDC1.Height - 20

		' AIX04･Ｙ軸最小ドット位置の目盛の値
		GYVTopSDC1		= +10.0

		' AIX04･Ｙ軸最大ドット位置の目盛の値
		GYVBtmSDC1		= -10.0

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpSDC1.Length - 1

			' Ｘドット位置初期化
			GrpSDC1( i ).X		= i

			' Ｙドット位置初期化
			GrpSDC1( i ).Y		= Cvtydot	_
			(					_
				GYDMinSDC1,			_
				GYDMaxSDC1,			_
				GYVTopSDC1,			_
				GYVBtmSDC1,			_
				0.0				_
			)

		Next i



		'
		'	AIX05	SDC電源CH2出力電圧(SDC-CH2電圧ﾓﾆﾀ信号)グラフ描画関連変数初期化
		'

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpSDC2, CPcbSDC2.Width - 5 )

		' AIX05･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinSDC2		= 10

		' AIX05･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxSDC2		= CPcbSDC2.Height - 20

		' AIX05･Ｙ軸最小ドット位置の目盛の値
		GYVTopSDC2		= +10.0

		' AIX05･Ｙ軸最大ドット位置の目盛の値
		GYVBtmSDC2		= -10.0

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpSDC2.Length - 1

			' Ｘドット位置初期化
			GrpSDC2( i ).X		= i

			' Ｙドット位置初期化
			GrpSDC2( i ).Y		= Cvtydot	_
			(					_
				GYDMinSDC2,			_
				GYDMaxSDC2,			_
				GYVTopSDC2,			_
				GYVBtmSDC2,			_
				0.0				_
			)

		Next i




		'
		'	AIX06	サ－モチラ－CH1温度(温度ﾓﾆﾀ信号)グラフ描画関連変数初期化
		'	最大で +80℃～-20℃
		'

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpClt1, CPcbClt1.Width - 5 )

		' AIX06･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinClt1		= 10

		' AIX06･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxClt1		= CPcbClt1.Height - 20

		' AIX06･Ｙ軸最小ドット位置の目盛の値
		' GYVTopClt1		= +10.0
		GYVTopClt1		= +8.5

		' AIX06･Ｙ軸最大ドット位置の目盛の値
		' GYVBtmClt1		= -10.0
		GYVBtmClt1		= -2.5

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpClt1.Length - 1

			' Ｘドット位置初期化
			GrpClt1( i ).X		= i

			' Ｙドット位置初期化
			GrpClt1( i ).Y		= Cvtydot	_
			(					_
				GYDMinClt1,			_
				GYDMaxClt1,			_
				GYVTopClt1,			_
				GYVBtmClt1,			_
				0.0				_
			)

		Next i



		'
		'	AIX07	サ－モチラ－CH2温度(温度ﾓﾆﾀ信号)グラフ描画関連変数初期化
		'	最大で +80℃～-20℃
		'

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpClt2, CPcbClt2.Width - 5 )

		' AIX07･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinClt2		= 10

		' AIX07･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxClt2		= CPcbClt2.Height - 20

		' AIX07･Ｙ軸最小ドット位置の目盛の値
		' GYVTopClt2		= +10.0
		GYVTopClt2		= +8.5

		' AIX07･Ｙ軸最大ドット位置の目盛の値
		' GYVBtmClt2		= -10.0
		GYVBtmClt2		= -2.5

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpClt2.Length - 1

			' Ｘドット位置初期化
			GrpClt2( i ).X		= i

			' Ｙドット位置初期化
			GrpClt2( i ).Y		= Cvtydot	_
			(					_
				GYDMinClt2,			_
				GYDMaxClt2,			_
				GYVTopClt2,			_
				GYVBtmClt2,			_
				0.0				_
			)

		Next i



		'
		'	AIX03	He流量移動平均グラフ描画関連変数初期化
		'

		' グラフ表示データの配列サイズの決定
		Array.Resize( GrpFlwA, CPcbFlwA.Width - 5 )

		' AIX03･ピクチャボックスの描画領域Ｙ軸最小ドット位置
		GYDMinFlwA		= 10

		' AIX03･ピクチャボックスの描画領域Ｙ軸最大ドット位置
		GYDMaxFlwA		= CPcbFlwA.Height - 20

		' AIX03･Ｙ軸最小ドット位置の目盛の値
		GYVTopFlwA		= 5.0

		' AIX03･Ｙ軸最大ドット位置の目盛の値
		GYVBtmFlwA		= 0.0

		' グラフ描画データバッファ初期値セット
		For i = 0 To GrpFlwA.Length - 1

			' Ｘドット位置初期化
			GrpFlwA( i ).X		= i

			' Ｙドット位置初期化
			GrpFlwA( i ).Y		= Cvtydot	_
			(					_
				GYDMinFlwA,			_
				GYDMaxFlwA,			_
				GYVTopFlwA,			_
				GYVBtmFlwA,			_
				0.0				_
			)

		Next i



		'
		'	インターバルタイマー１のスタート
		'
		TmrBlk.Interval		= 300

		TmrBlk.Enabled		= True



	End Sub



	'*****
	'	グラフＹ軸ドット位置への換算
	'*****
	Public Function Cvtydot					_
	(							_
		ydotmin			As Long,		_
		ydotmax			As Long,		_
		yvaltop			As Double,		_
		yvalbtm			As Double,		_
		newdat			As Double		_
	)	As Long

		Dim nydot		As Long
		Dim pydot		As Double



		'
		'	最新データをグラフＹドット位置へ換算する
		'

		' 描画領域のY軸ドット数
		nydot			= ydotmax - ydotmin

		' Ｙ軸ドット位置へ換算
		pydot			= nydot - ( nydot / ( yvaltop - yvalbtm ) ) * ( newdat - yvalbtm ) + ydotmin

		Cvtydot			= pydot



	End Function




	'*****
	'	ピクチャボックスに折れ線グラフを描画する
	'
	'	picbox			As PictureBox,		_
	'		描画対象ピクチャーボックス
	'
	'	grppnt()		As Point,		_
	'		描画ポイントデータバッファ
	'		グラフ表示データを保持しておくバッファ
	'
	'	ydotmin			As Long,		_
	'		ピクチャボックスの描画領域Ｙ軸最小ドット位置を指定
	'
	'	ydotmax			As Long,		_
	'		ピクチャボックスの描画領域Ｙ軸最大ドット位置を指定
	'		
	'	yvaltop			As Double,		_
	'		Ｙ軸の ydotmin 位置の目盛の値
	'
	'	yvalbtm			As Double,		_
	'		Ｙ軸の ydotmax 位置の目盛の値
	'
	'	newdat			As Double,		_
	'		最新のデータ値
	'
	'	unit			As String
	'		Ｙ軸目盛の単位
	'
	'*****
	Public Sub GrpDisp						_
	(								_
		picbox			As PictureBox,			_
		grpcol			As Pen,			_
		grppnt()		As Point,			_
		ydotmin			As Long,			_
		ydotmax			As Long,			_
		yvaltop			As Double,			_
		yvalbtm			As Double,			_
		newdat			As Double,			_
		unit			As String			_
	)

		'描画先とするImageオブジェクトを作成する
		Dim canvas		As New Bitmap( picbox.Width, picbox.Height )

		'ImageオブジェクトのGraphicsオブジェクトを作成する
		Dim g			As Graphics = Graphics.FromImage( canvas )

		'フォントオブジェクトの作成
		Dim fnt			As New Font( "MS UI Gothic", 8 )

		Dim ii			As Integer

		Dim endidx		As Integer



		'
		'	クリップ領域の設定
		'

		' g.Clip = New Region(New Rectangle( 10, ydotmin, 100, ydotmax ) )


		' グラフデータの配列最期の番号
		endidx			= grppnt.Length - 1


		'
		'	グラフ描画データを１データ左へシフトする
		'
		'	grppnt( 0 )		最も古いデータ
		'	   |
		'	grppnt( endidx )	最新のデータ
		'
		For ii = 0 to endidx - 1

			grppnt( ii ).Y		= grppnt( ii + 1 ).Y

		Next


		'
		'	最新データをグラフＹドット位置へ換算する
		'
		grppnt( endidx ).Y	= Cvtydot( ydotmin, ydotmax, yvaltop, yvalbtm, newdat )


		'
		'	折れ線を引く
		'

		' ウエハ裏面圧(ﾊﾞﾗﾄﾛﾝ真空計)
		g.DrawLines( grpcol, grppnt )



		'
		'	Ｙ軸上限、下限の数値をグラフに描画する
		'

		' Ｙ軸上限
		g.DrawString			_
		(				_
			yvaltop.ToString( "0.00" + unit ) ,		_
			fnt,			_
			Brushes.White,		_
			10,			_
			Cvtydot( ydotmin, ydotmax, yvaltop, yvalbtm, yvaltop )	_
		)

		' Ｙ軸下限
		g.DrawString			_
		(				_
			yvalbtm.ToString( "0.00" + unit ) ,		_
			fnt,			_
			Brushes.White,		_
			10,			_
			Cvtydot( ydotmin, ydotmax, yvaltop, yvalbtm, yvalbtm )	_
		)


		'
		'	リソースを解放する
		'
		fnt.Dispose()

		g.Dispose()


		'PictureBoxに表示する
		picbox.Image		= canvas



	End Sub



	'*****
	'	数値を補助単位付き数値へ変換
	'
	'	<return>
	'	String
	'	補助単位付き数字文字列
	'*****
	Public Function cvtn2subunitn				_
	(							_
		ByRef sbn		As Double,		_
		ByRef sbu		As String,		_
		strfmt			As String,		_
		ByVal num		As Double		_
	)	As String

		Dim sgn			As Double
		Dim absv		As Double
		Dim wkn			As Double



		' 符号を取り出し、数値を絶対値にする
		If 0.0 > num Then

			' - 符号
			sgn			= -1.0

			' 数値を絶対値にする
			absv			= num * -1.0

		Else

			' + 符号
			sgn			= +1.0

			' 数値を絶対値にする
			absv			= num

		End If


		wkn			= absv
		sbu			= ""


		If 1.0 <= absv Then

			If 1.0e12 <= absv Then

				' T 単位へ換算
				wkn			= absv / 1.0e12

				' 補助単位
				sbu			= "T"

			ElseIf 1.0e9 <= absv Then

				' G 単位へ換算
				wkn			= absv / 1.0e9

				' 補助単位
				sbu			= "G"

			ElseIf 1.0e6 <= absv Then

				' M 単位へ換算
				wkn			= absv / 1.0e6

				' 補助単位
				sbu			= "M"

			ElseIf 1.0e3 <= absv Then

				' K 単位へ換算
				wkn			= absv / 1.0e3

				' 補助単位
				sbu			= "K"

			End If
 
		Else

			If 1.0e-12 >= absv Then

				' p 単位へ換算
				wkn			= absv / 1.0e-12

				' 補助単位
				sbu			= "p"

			ElseIf 1.0e-9 >= absv Then

				' n 単位へ換算
				wkn			= absv / 1.0e-9

				' 補助単位
				sbu			= "n"

			ElseIf 1.0e-6 >= absv Then

				' μ 単位へ換算
				wkn			= absv / 1.0e-6

				' 補助単位
				sbu			= "u"

			ElseIf 1.0e-3 >= absv Then

				' m 単位へ換算
				wkn			= absv / 1.0e-3

				' 補助単位
				sbu			= "m"

			End If

		End If

		' 符号を元に戻す
		sbn			= wkn * sgn

		' 補助単位数値文字列に変換
		cvtn2subunitn		= sbn.ToString( strfmt ) + sbu


	End Function



	'*****
	'	グラフ表示更新処理
	'*****
	Public Sub RefsGraph()

		Dim airaw		As UShort
		Dim aivolt		As Double
		Dim sbn			As Double
		Dim sbu			As String = ""
		Dim sbustr		As String



		'
		'	AIX01  ウエハ裏面圧(ﾊﾞﾗﾄﾛﾝ真空計)
		'

		' AIX01･測定値データをリングバッファから取り出す
		getdata( airaw, aivolt, MesBkp, 1, 1 )

		' AIX01･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbBPrs,			_
			Pens.Yellow,			_
			GrpBPrs,			_
			GYDMinBPRS,			_
			GYDMaxBPRS,			_
			GYVTopBPRS,			_
			GYVBtmBPRS,			_
			aivolt,				_
			"V"				_
		)

		' AIX01･測定電圧値表示
		LabelBPrsV.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' 換算値を補助単位付き数値に変換する
		sbustr			= cvtn2subunitn( sbn, sbu, "000.0", MesBkp )

		' AIX01･換算値表示
		LabelBPrsC.Text		= mkpastrN3( MesBkp )
'
' 20200217 y.goto 表示方法変更
'		LabelBPrsC.Text		= MesBkp.ToString( "0.0" ).PadLeft( 8 ) + "[Pa]"
' 20140107
'		LabelBPrsC.Text		= sbn.ToString( "#0.0" ).PadLeft( 6 ) + " [" + sbu + "Pa]"



		'
		'	AIX02	チャンバ内圧力(ﾋﾟﾗﾆ真空計)
		'

		' AIX02･測定値データをリングバッファから取り出す
		getdata( airaw, aivolt, MesCbp, 2, 1 )


		' AIX02･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbCPrs,			_
			Pens.YellowGreen,		_
			GrpCPrs,			_
			GYDMinCPrs,			_
			GYDMaxCPrs,			_
			GYVTopCPrs,			_
			GYVBtmCPrs,			_
			aivolt,				_
			"V"				_
		)					_

		' AIX02･測定電圧値表示
		LabelCPrsV.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' 換算値を補助単位付き数値に変換する
		sbustr			= cvtn2subunitn( sbn, sbu, "000.0", MesCbp )

		' AIX02･換算値表示
		LabelCPrsC.Text		= mkpastrN3( MesCbp )
'
' 20200217 y.goto 表示方法変更
'		LabelCPrsC.Text		= MesCbp.ToString( "0.0" ).PadLeft( 8 ) + "[Pa]"
' 20140108
'		LabelCPrsC.Text		= sbn.ToString( "#0.0" ).PadLeft( 6 ) + " [" + sbu + "Pa]"

		If 5.0 < MesCbp Then

			LabelCPrsC.BackColor	= Color.Yellow

		Else

			LabelCPrsC.BackColor	= Color.GreenYellow

		End If



		'
		'	AIX03	He流量(MFC1流量ﾓﾆﾀ信号)
		'

		' AIX03･測定値データをリングバッファから取り出す
		getdata( airaw, aivolt, MesFlwM, 3, 1 )

		' AIX03･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbFlwM,			_
			Pens.White,			_
			GrpFlwM,			_
			GYDMinFlwM,			_
			GYDMaxFlwM,			_
			GYVTopFlwM,			_
			GYVBtmFlwM,			_
			aivolt,				_
			"V"				_
		)					_

		' AIX03･測定電圧値表示
		LabelFlwMV.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' AIX03･換算値表示
		LabelFlwMC.Text		= MesFlwM.ToString( "#0.00" ).PadLeft( 6 )
'
' 20200217 y.goto 表示方法変更
'		LabelFlwMC.Text		= MesFlwM.ToString( "#0.00" ).PadLeft( 6 ) + "[ccm]"

		If 0.2 < MesFlwM Then

			LabelFlwMC.BackColor	= Color.LightPink

		Else

			LabelFlwMC.BackColor	= Color.White

		End If



		'
		'	AIX04	SDC電源CH1出力電圧(SDC-CH1電圧ﾓﾆﾀ信号)
		'

		' AIX04･測定値データをリングバッファから取り出す
		getdata( airaw, aivolt, MesSDCV1, 4, 1 )

		' AIX04･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbSDC1,			_
			Pens.Orange,			_
			GrpSDC1,			_
			GYDMinSDC1,			_
			GYDMaxSDC1,			_
			GYVTopSDC1,			_
			GYVBtmSDC1,			_
			aivolt,				_
			"V"				_
		)					_

		' AIX04･測定電圧値表示
		LabelSDC1V.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' AIX04･換算値表示
		LabelSDC1C.Text		= MesSDCV1.ToString( "#0.0" ).PadLeft( 6 )
'
' 20200217 y.goto 表示方法変更
'		LabelSDC1C.Text		= MesSDCV1.ToString( "#0.0" ).PadLeft( 6 ) + "[V]"


		'
		'	AIX05	SDC電源CH2出力電圧(SDC-CH2電圧ﾓﾆﾀ信号)
		'

		' AIX05･測定値データをリングバッファから取り出す
		getdata( airaw, aivolt, MesSDCV2, 5, 1 )

		' AIX05･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbSDC2,			_
			Pens.Orange,			_
			GrpSDC2,			_
			GYDMinSDC2,			_
			GYDMaxSDC2,			_
			GYVTopSDC2,			_
			GYVBtmSDC2,			_
			aivolt,				_
			"V"				_
		)					_

		' AIX05･測定電圧値表示
		LabelSDC2V.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' AIX05･換算値表示
		LabelSDC2C.Text		= MesSDCV2.ToString( "#0.0" ).PadLeft( 6 )
'
' 20200217 y.goto 表示方法変更
'		LabelSDC2C.Text		= MesSDCV2.ToString( "#0.0" ).PadLeft( 6 ) + "[V]"



		'
		'	AIX06	サ－モチラ－CH1温度(温度ﾓﾆﾀ信号)
		'

		' AIX06･測定値データをリングバッファから取り出す
		getdata( airaw, aivolt, MesTmp1, 6, 30 )

		' AIX06･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbClt1,			_
			Pens.LightYellow,		_
			GrpClt1,			_
			GYDMinClt1,			_
			GYDMaxClt1,			_
			GYVTopClt1,			_
			GYVBtmClt1,			_
			aivolt,				_
			"V"				_
		)					_

		' AIX06･測定電圧値表示
		LabelClt1V.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' AIX06･換算値表示
		LabelClt1C.Text		= MesTmp1.ToString( "#0.0" ).PadLeft( 5 )
'
' 20200217 y.goto 表示方法変更
'		LabelClt1C.Text		= MesTmp1.ToString( "#0.0" ).PadLeft( 5 ) + "[℃]"



		'
		'	AIX07	サ－モチラ－CH2温度(温度ﾓﾆﾀ信号)
		'

		' AIX07･測定値データをリングバッファから取り出す
		getdata( airaw, aivolt, MesTmp2, 7, 30 )

		' AIX07･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbClt2,			_
			Pens.Olive,			_
			GrpClt2,			_
			GYDMinClt2,			_
			GYDMaxClt2,			_
			GYVTopClt2,			_
			GYVBtmClt2,			_
			aivolt,				_
			"V"				_
		)					_

		' AIX07･測定電圧値表示
		LabelClt2V.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' AIX07･換算値表示
		LabelClt2C.Text		= MesTmp2.ToString( "#0.0" ).PadLeft( 5 )
'
' 20200217 y.goto 表示方法変更
'		LabelClt2C.Text		= MesTmp2.ToString( "#0.0" ).PadLeft( 5 ) + "[℃]"



		'
		'	AIX03	He流量移動平均
		'

		' AIX03･測定値データをリングバッファから取り出す
	'	getdata( airaw, aivolt, MesFlwA, 3, 20 )
		getdata( airaw, aivolt, MesFlwA, 3, DefFlowNAvg )

		' AIX03･折れ線グラフ表示
		GrpDisp					_
		(					_
			CPcbFlwA,			_
			Pens.LimeGreen,			_
			GrpFlwA,			_
			GYDMinFlwA,			_
			GYDMaxFlwA,			_
			GYVTopFlwA,			_
			GYVBtmFlwA,			_
			aivolt,				_
			"V"				_
		)					_

		' AIX03･測定電圧値表示
		LabelFlwAV.Text		= aivolt.ToString( "#0.000" ).PadLeft( 7 )

		' AIX03･換算値表示
		LabelFlwAC.Text		= MesFlwA.ToString( "#0.00" ).PadLeft( 6 )
'
' 20200217 y.goto 表示方法変更
'		LabelFlwAC.Text		= MesFlwA.ToString( "#0.00" ).PadLeft( 6 ) + "[ccm]"

		If 0.2 < MesFlwA Then

			LabelFlwAC.BackColor	= Color.LightPink

		Else

			LabelFlwAC.BackColor	= Color.White

		End If



	End Sub



	'*****
	'	ブリンク処理タイマー
	'*****
	Private Sub TmrBlk_Tick					_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles TmrBlk.Tick




		' ブリンク状態フラグを反転
		BlkFlg			= Not BlkFlg



		'
		'	SDC-CH1に電圧が印加されていればブリンク表示する
		'
		If -10.0 > MesSDCV1 Or +10.0 < MesSDCV1 Then


			If BlkFlg Then

				LabelSDC1C.BackColor	= Color.Red

			Else

				LabelSDC1C.BackColor	= Color.White

			End If

		Else

			LabelSDC1C.BackColor	= Color.White

		End If



		'
		'	SDC-CH2に電圧が印加されていればブリンク表示する
		'
		If -10.0 > MesSDCV2 Or +10.0 < MesSDCV2 Then


			If BlkFlg Then

				LabelSDC2C.BackColor	= Color.Red

			Else

				LabelSDC2C.BackColor	= Color.White

			End If

		Else

			LabelSDC2C.BackColor	= Color.White

		End If




		'
		'	ウエハ裏面圧が一定以上の時はブリンク表示
		'
		If 100.0 < MesBkp Then

			If BlkFlg Then

				LabelBPrsC.BackColor	= Color.Orange

			Else

				LabelBPrsC.BackColor	= Color.White

			End If

		Else

			LabelBPrsC.BackColor	= Color.White

		End If




	End Sub



End Class
