

Public Class TestParmDlg


	Public pEscMd		As Integer

	' 20201102 s.harada	トーカロ専用に変更で削除
	'Public pBprs		As Double

	'Public pHlPres		As Double

	Public pScrUse		As Integer

	Public pTprs1		As Double

	Public pTprs2		As Double

	Public pBakPres		As Double

	' 電極ヘッド温度安定待ち時間
	Public	pTmpStbW	As Double


	'
	' 20200716 s.harada
	' 残留吸着用
	'
	' ヘリウム流量
	Public pHeFlow		As Double

	' 電圧印加時間
	Public pVoltImp		As Double

	' 電圧印加停止後Ｈｅ流すまでの待ち時間
	Public pHeWait		As Double

	'	20201102 s.harada	トーカロ専用
	'吸着力測定時間上限
	Public pMaxTim		As Double

	' 安定判断傾き
	Public pAntCvt		As Double

	'安定判断時間
	Public pAntTim		As Double

	'
	'   20201124 y.goto
	'	残留吸着電圧印可時裏面圧力 (Pa)
	'
	Public pBakPrs		As Double


	'*****
	'	試験パラメータ入力ダイアログ
	'	Loadイベント
	'*****
	Private Sub TestParmDlg_Load				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles MyBase.Load


		cboEscMd.Items.Add( "0:" + convEscMdToStr( 0 ) )

		cboEscMd.Items.Add( "1:" + convEscMdToStr( 1 ) )

		cboEscMd.SelectedIndex	= pEscMd



		'
		'	ウエハ吸着力測定リミット値
		'
		'	20201102 s.harada	トーカロ専用に変更のため削除
		'txtBPRS.Text		= ( pBprs / 1000 ).ToString( "0.00" )

		'txtHLPRS.Text		= ( pHlPres / 1000 ).ToString( "0.0" )


		cboScrUse.Items.Add( "0:" + convScrUseToStr( 0 ) )

		cboScrUse.Items.Add( "1:" + convScrUseToStr( 1 ) )

		cboScrUse.Items.Add( "2:" + convScrUseToStr( 2 ) )

		cboScrUse.SelectedIndex	= pScrUse


		txtTRPS1.Text		= pTprs1.ToString( "0.00" )

		txtTRPS2.Text		= pTprs2.ToString( "0.00" )

		txtBakPres.Text		= pBakPres.ToString( "0.0" )

		' 電極ヘッド温度安定待ち時間
		txtTmpStbW.Text		= pTmpStbW.ToString( "0." )



		txtHeFlow.Text		= pHeFlow.ToString( "0.0" )

		txtVoltImp.Text		= pVoltImp.ToString( "0." )

		txtHeWait.Text		= pHeWait.ToString( "0." )

		txtKyuMaxTm.Text	= pMaxTim.ToString( "0." )

		txtAntVct.Text		= pAntCvt.ToString( "0.0" )

		txtAntTim.Text		= pAntTim.ToString( "0." )

		txtBakPrs.Text		= pBakPrs.ToString( "0." )

	End Sub



	Private Sub btnSet_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnSet.Click


		pEscMd			= cboEscMd.Text.Substring( 0, 1 )

		'	20201102 s.harada	トーカロ専用に変更のため削除
		' ウエハ吸着力測定リミット値
		'pBprs			= CDbl( txtBPRS.Text ) * 1000.0

		'pHlPres			= CDbl( txtHLPRS.Text ) * 1000.0

		pScrUse			= cboScrUse.Text.Substring( 0, 1 )

		pTprs1			= CDbl( txtTRPS1.Text )

		pTprs2			= CDbl( txtTRPS2.Text )

		pBakPres		= CDbl( txtBakPres.Text )


		' 電極ヘッド温度安定待ち時間
		pTmpStbW		= CDbl( txtTmpStbW.Text )


		'
		'	20201102 s.harada	AQTC対応追加
		pHeFlow			= CDbl( txtHeFlow.Text )

		pVoltImp		= CDbl( txtVoltImp.Text )

		pHeWait			= CDbl( txtHeWait.Text )

		pMaxTim			= CDbl( txtKyuMaxTm.Text )

		pAntCvt			= CDbl( txtAntVct.Text )

		pAntTim			= CDbl( txtAntTim.Text )

		pBakPrs			= CDbl( txtBakPrs.Text )

		Me.Close()


	End Sub



	Private Sub btnCancel_Click				_
	(							_
		sender			As System.Object,	_
		e			As System.EventArgs	_
	)	Handles btnCancel.Click


		Me.Close()



	End Sub



End Class