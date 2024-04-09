


Public Class FrmWaitCdt


	'
	'	表示更新、判定処理用タイマー・インターバル
	'
	Const DefTimItv		As Long = 1000



	'
	'	状態表示文字列定義
	'

	' 条件待ち状態の時の表示文字列
	Const DefStsWait	As String = "条件待ち"

	' 条件成立状態の時の表示文字列
	Const DefStsOK		As String = "  ＯＫ  "


	' 条件待ちの時の背景色
	Dim DefColWait		As System.Drawing.Color = System.Drawing.Color.FromArgb( 255, 255, 128 )

	' 条件成立カウント中の時の背景色
	Dim DefColOKCnt		As System.Drawing.Color	= System.Drawing.Color.FromArgb( 200, 255, 200 )

	' 条件成立確定の時の背景色
	Dim DefColOKSet		As System.Drawing.Color	= System.Drawing.Color.FromArgb( 100, 255, 100 )




	'
	'	フォームクローズフラグ
	'
	'	0	処理中
	'	1	フォームクローズ
	'
	Public FrmWaitCdtClose	As Integer

	'
	'	フォーム処理結果戻り値
	'
	'	0	処理終了・条件成立
	'	1	処理終了・中断指示
	'
	Public StsFrmWaitCdt	As Integer



	'
	'	パラメータの定義
	'

	' 総合待ち時間 (秒)
	Public PrmWTim		As Long



	' 全条件成立確定時間タイマー値（秒）
	Public PrmTcAllOK	As Long



	' サーモチラーＣＨ１目標温度
	Public PrmAimChl1	As String

	' サーモチラーＣＨ１目標到達判定タイマー値（秒）
	Public PrmTcAimChl1	As Long



	' サーモチラーＣＨ２目標温度
	Public PrmAimChl2	As String

	' サーモチラーＣＨ２目標到達判定タイマー値（秒）
	Public PrmTcAimChl2	As Long



	' 電極ヘッド温度安定待ち時間（秒）
	Public PrmTcAimHed	As String



	' チャンバ内圧力（ピラニ真空計）
	Public PrmAimCmb	As String

	' チャンバ内圧力目標到達判定タイマー値（秒）
	Public PrmTcAimCmb	As Long



	' ウエハ裏面圧力（バラトロン真空計）
	Public PrmAimWbp	As String

	' ウエハ裏面圧力目標到達判定タイマー値（秒）
	Public PrmTcAimWbp	As Long

	' 20200326 追加 y.goto
	' 動作モード
	'
	'	0	従来動作
	'	1	トーカロ向けベント動作
	'
	Public PrmMode		As Integer


	'
	'	ローカル変数
	'

	' 総合待ち時間カタイマーカウンタ
	Dim cntWTim		As Long



	' 全条件成立確定時間タイマーカウンタ
	Dim cntTcAllOK		As Long



	' サーモチラーＣＨ１目標到達チェック実施フラグ
	Dim fAimChl1		As Byte

	' サーモチラーＣＨ１目標温度
	Dim wAimChl1		As Double

	' サーモチラーＣＨ１現在温度
	Dim nowChl1		As Double

	' サーモチラーＣＨ１目標到達判定タイマーカウンタ
	Dim cntAimChl1		As Long



	' サーモチラーＣＨ２目標到達チェック実施フラグ
	Dim fAimChl2		As Byte

	' サーモチラーＣＨ２目標温度
	Dim wAimChl2		As Double

	' サーモチラーＣＨ２現在温度
	Dim nowChl2		As Double

	' サーモチラーＣＨ２目標到達判定タイマーカウンタ
	Dim cntAimChl2		As Long



	' 電極ヘッド温度安定待ち実施フラグ
	Dim fAimHed		As Byte

	' 電極ヘッド温度安定待ち時間
	Dim wAimHed		As Long

	' 電極ヘッド温度安定待ち時間タイマーカウンタ
	Dim cntAimHed		As Long



	' チャンバ内圧力目標到達チェック実施フラグ
	Dim fAimCmb		As Byte

	' チャンバ内圧力（ピラニ真空計）
	Dim wAimCmb		As Double

	' チャンバ内圧力現在値
	Dim nowCmb		As Double

	' チャンバ内圧力目標到達判定タイマーカウンタ
	Dim cntAimCmb		As Long



	' ウエハ裏面圧力目標到達チェック実施フラグ
	Dim fAimWbp		As Byte

	' ウエハ裏面圧力（バラトロン真空計）
	Dim wAimWbp		As Double

	' ウエハ裏面圧力現在温値
	Dim nowWbp		As Double

	' ウエハ裏面圧力目標到達判定タイマーカウンタ
	Dim cntAimWbp		As Long



	' ブリンク表示フラグ
	Dim flgBlink		As Integer



	'*****
	'	フォームクローズイベント
	'*****
	Private Sub FrmWaitCdt_FormClosed						_
	(										_
		ByVal sender		As Object,					_
		ByVal e			As System.Windows.Forms.FormClosedEventArgs	_
	)	Handles Me.FormClosed


		' フォームクローズフラグ・オン
		FrmWaitCdtClose		= 1


	End Sub



	'*****
	'	フォームロードイベント
	'*****
	Private Sub FrmWaitCdt_Load				_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles MyBase.Load



		' フォームクローズフラグ・クリア
		FrmWaitCdtClose		= 0

		'
		'	フォーム処理結果戻り値・初期値セット
		'
		'	ウインドを[X]ボタンでクローズした場合を考慮して
		'	初期値は 1(試験中止) とする
		'
		StsFrmWaitCdt		= 1


		'
		'	フォーム上のコントロールの初期化
		'

		lblCntSyn.Text		= ""

		'
		'	サーモチラーＣＨ１
		'

		' 目標
		lblAimChl1.Text		= ""

		' 現在
		lblNowChl1.Text		= ""

		' カウント
		lblCntChl1.Text		= ""

		' 判定
		lblStsChl1.Text		= ""


		'
		'	サーモチラーＣＨ２
		'

		' 目標
		lblAimChl2.Text		= ""

		' 現在
		lblNowChl2.Text		= ""

		' カウント
		lblCntChl2.Text		= ""

		' 判定
		lblStsChl2.Text		= ""


		'
		'	電極ヘッド温度安定待ち
		'

		' 目標
		lblAimHed.Text		= ""

		' 現在
		lblNowHed.Text		= ""

		' カウント
		lblCntHed.Text		= ""

		' 判定
		lblStsHed.Text		= ""


		'
		'	チャンバ内圧力（ピラニ真空計）
		'

		' 目標
		lblAimCmb.Text		= ""

		' 現在
		lblNowCmb.Text		= ""

		' カウント
		lblCntCmb.Text		= ""

		' 判定
		lblStsCmb.Text		= ""


		'
		'	ウエハ裏面圧力（バラトロン真空計）
		'

		' 目標
		lblAimWbp.Text		= ""

		' 現在
		lblNowWbp.Text		= ""

		' カウント
		lblCntWbp.Text		= ""

		' 判定
		lblStsWbp.Text		= ""


		'
		'	全条件成立
		'

		' 目標
		lblAimAllOK.Text	= ""

		' 現在
		lblNowAllOK.Text	= ""

		' カウント
		lblCntAllOK.Text	= ""

		' 判定
		lblStsAllOK.Text	= ""



		' サーモチラーＣＨ１目標温度
		Try

			wAimChl1		= CDbl( PrmAimChl1 )

			' 目標
			lblAimChl1.Text		= wAimChl1.ToString( "0" )

			' サーモチラーＣＨ１目標到達判定タイマー値・初期値セット
			cntAimChl1		= PrmTcAimChl1

			' サーモチラーＣＨ１目標到達チェック実施フラグ・実施する
			fAimChl1		= 1

		Catch

			'
			'	判定処理は行わない
			'

			' 項目
			lblChl1.ForeColor	= Color.Silver

			' 目標
			lblAimChl1.ForeColor	= Color.Silver

			' 目標・単位
			lblChl1Unit1.ForeColor	= Color.Silver

			' 現在
			lblNowChl1.ForeColor	= Color.Silver

			' 現在・単位
			lblChl1Unit2.ForeColor	= Color.Silver

			' カウント
			lblCntChl1.ForeColor	= Color.Silver

			' 判定
			lblStsChl1.ForeColor	= Color.Silver

			' サーモチラーＣＨ１目標到達チェック実施フラグ・実施しない
			fAimChl1		= 0

		End Try


		' サーモチラーＣＨ２目標温度
		Try

			wAimChl2		= CDbl( PrmAimChl2 )

			' 目標
			lblAimChl2.Text		= wAimChl2.ToString( "0" )

			' サーモチラーＣＨ２目標到達判定タイマー値・初期値セット
			cntAimChl2		= PrmTcAimChl2

			' サーモチラーＣＨ２目標到達チェック実施フラグ・実施する
			fAimChl2		= 1

		Catch


			'
			'	判定処理は行わない
			'

			' 項目
			lblChl2.ForeColor	= Color.Silver

			' 目標
			lblAimChl2.ForeColor	= Color.Silver

			' 目標・単位
			lblChl2Unit1.ForeColor	= Color.Silver

			' 現在
			lblNowChl2.ForeColor	= Color.Silver

			' 現在・単位
			lblChl2Unit2.ForeColor	= Color.Silver

			' カウント
			lblCntChl2.ForeColor	= Color.Silver

			' 判定
			lblStsChl2.ForeColor	= Color.Silver

			' サーモチラーＣＨ２目標到達チェック実施フラグ・実施しない
			fAimChl2		= 0

		End Try


		' 電極ヘッド温度安定待ち時間
		Try

			wAimHed			= CLng( PrmTcAimHed )

			' 目標
			lblAimHed.Text		= wAimHed.ToString( "0" )

			' 電極ヘッド温度安定待ち時間タイマーカウンタ・初期値セット
			cntAimHed		= wAimHed

			' 電極ヘッド温度安定待ち実施フラグ・実施する
			fAimHed			= 1

		Catch

			'
			'	判定処理は行わない
			'

			' 項目
			lblHed.ForeColor	= Color.Silver

			' 目標
			lblAimHed.ForeColor	= Color.Silver

			' 目標・単位
			lblHedUnit1.ForeColor	= Color.Silver

			' 現在
			lblNowHed.ForeColor	= Color.Silver

			' 現在・単位
			lblHedUnit2.ForeColor	= Color.Silver

			' カウント
			lblCntHed.ForeColor	= Color.Silver

			' 判定
			lblStsHed.ForeColor	= Color.Silver

			' 電極ヘッド温度安定待ち実施フラグ・実施しない
			fAimHed			= 0

		End Try



		' チャンバ内圧力（ピラニ真空計）
		Try

			wAimCmb			= CDbl( PrmAimCmb )

			' 目標
			lblAimCmb.Text		= wAimCmb.ToString( "0" )

			' チャンバ内圧力目標到達判定タイマーカウンタ・初期値セット
			cntAimCmb		= PrmTcAimCmb

			' チャンバ内圧力目標到達チェック実施フラグ・実施する
			fAimCmb		= 1

		Catch

			'
			'	判定処理は行わない
			'

			' 項目
			lblCmb.ForeColor	= Color.Silver

			' 目標
			lblAimCmb.ForeColor	= Color.Silver

			' 目標・単位
			lblCmbUnit1.ForeColor	= Color.Silver

			' 現在
			lblNowCmb.ForeColor	= Color.Silver

			' 現在・単位
			lblCmbUnit2.ForeColor	= Color.Silver

			' カウント
			lblCntCmb.ForeColor	= Color.Silver

			' 判定
			lblStsCmb.ForeColor	= Color.Silver

			' チャンバ内圧力目標到達チェック実施フラグ・実施しない
			fAimCmb		= 0

		End Try



		' ウエハ裏面圧力（バラトロン真空計）
		Try

			wAimWbp			= CDbl( PrmAimWbp )

			' 目標
			lblAimWbp.Text		= wAimWbp.ToString( "0" )

			' ウエハ裏面圧力目標到達判定タイマーカウンタ・初期値セット
			cntAimWbp		= PrmTcAimWbp

			' ウエハ裏面圧力目標到達チェック実施フラグ・実施する
			fAimWbp			= 1

		Catch

			'
			'	判定処理は行わない
			'

			' 項目
			lblWbp.ForeColor	= Color.Silver

			' 目標
			lblAimWbp.ForeColor	= Color.Silver

			' 目標・単位
			lblWbpUnit1.ForeColor	= Color.Silver

			' 現在
			lblNowWbp.ForeColor	= Color.Silver

			' 現在・単位
			lblWbpUnit2.ForeColor	= Color.Silver

			' カウント
			lblCntWbp.ForeColor	= Color.Silver

			' 判定
			lblStsWbp.ForeColor	= Color.Silver

			' ウエハ裏面圧力目標到達チェック実施フラグ・実施しない
			fAimWbp			= 0

		End Try



		'
		'	総合待ち時間カタイマーカウント値へ換算
		'

		' 総合待ち時間カタイマーウンタ初期値セット
		cntWTim			= PrmWTim


		'
		'	全条件成立確定時間タイマーカウント値へ換算
		'

		' 目標
		lblAimAllOK.Text	= PrmTcAllOK.ToString( "0" )

		' 全条件成立確定時間タイマーカウンタ・初期値セット
		cntTcAllOK		= PrmTcAllOK



		'
		'	表示更新、判定処理用インターバルタイマー・スタート
		'
		TimItv.Interval		= DefTimItv

		TimItv.Enabled		= True


	End Sub



	'*****
	'	表示更新処理
	'
	'	条件待ちの時はブリンク表示
	'	条件成立の時の表示処理
	'*****
	Private Sub DspRfs				_
	(						_
		ByRef lblsts		As Label,	_
		ByRef lblnow		As Label,	_
		ByRef lblcnt		As Label,	_
		ByVal tcnt		As Long,	_
		ByVal tdef		As Long,	_
		ByRef nowstr		As String	_
	)



		With lblsts

			' 状態表示文字列
			.Text			= IIf( tcnt, DefStsWait, DefStsOK )


			' 現在値を表示
			lblnow.Text		= nowstr


			' 現在値はカウント中のみ表示する
			lblcnt.Text		= IIf( tcnt <> tdef, tcnt.ToString(), "" )


			If tcnt Then

				' 待ちの時の背景色・薄い黄色
				.BackColor		= DefColWait

				' 待ちの時は表示文字をブリンク表示
				.ForeColor		= IIF( flgBlink, Color.Black, .BackColor )

				' 待ちの時の現在値の背景色
				lblnow.BackColor	= IIf( tcnt <> tdef, DefColOKCnt, Color.FromArgb( 255, 150, 150 ) )

				' 待ちの時のカウントの背景色
				lblcnt.BackColor	= IIf( tcnt <> tdef, DefColOKCnt, Color.LightGray )

			Else

				' 条件成立の時の背景色
				.BackColor		= DefColOKSet

				' 条件成立の時の表示文字
				.ForeColor		= Color.Black

			End If


		End With


	End Sub



	'*****
	'	測定開始条件成立判定
	'
	'	<return>
	'	0	条件未成立
	'	!0	条件成立
	'*****
	Public Function JdgTstCond()	As Integer

		Dim rtn			As Integer
		Dim raw			As UShort
		Dim ctmp1		As Double
		Dim ctmp2		As Double
		Dim ccmbprs		As Double
		Dim cwbkprs		As Double



		rtn			= 0

		Do


			'
			'	総合待ち時間カタイマーウンタ・カウンタ値表示
			'
			Dim ts As TimeSpan	= New TimeSpan( 0, 0, cntWTim )

			lblCntSyn.Text		=　ts.ToString( "hh\:mm\:ss" )


			'
			'	サ－モチラーＣＨ１温度モニタ信号を取得
			'
			raw			= aiget( SCRaiTMP1, 30 )

			' ＲＡＷデ－タから温度へ換算
			ctmp1			= cvtr2SCR( raw )
' debug
' ctmp1	= 30

			'
			'	サーモチラーＣＨ１温度到達判定
			'
			'	目標温度 - 3℃ <= 現在温度 <= 目標温度 + 3℃で
			'	目標到達と判断する
			'
			If							_
				wAimChl1 + 2.6 < ctmp1 Or			_
				wAimChl1 - 2.6 > ctmp1				_
			Then

				' 条件成立していないのでタイマーカウンタを更新
				cntAimChl1		= PrmTcAimChl1

			End If


			If fAimChl1 Then

				' 示更新処理
				DspRfs( lblStsChl1, lblNowChl1, lblCntChl1, cntAimChl1, PrmTcAimChl1, ctmp1.ToString( "0" ) )

			End If



			'
			'	サ－モチラーＣＨ２温度モニタ信号を取得
			'
			raw			= aiget( SCRaiTMP2, 30 )

			' ＲＡＷデ－タから温度へ換算
			ctmp2			= cvtr2SCR( raw )

' debug
' ctmp2		= 31

			'
			'	温度到達判定
			'
			'	目標温度 - 2℃ <= 現在温度 <= 目標温度 + 2℃で
			'	目標到達と判断する
			'
			If							_
				wAimChl2 + 2.0 < ctmp2 Or			_
				wAimChl2 - 2.0 > ctmp2				_
			Then

				' 条件成立していないのでタイマーカウンタを更新
				cntAimChl2		= PrmTcAimChl2

			End If

			If fAimChl2 Then

				' 示更新処理
				DspRfs( lblStsChl2, lblNowChl2, lblCntChl2, cntAimChl2, PrmTcAimChl2, ctmp2.ToString( "0" ) )

			End If



			'
			'	電極ヘッド温度安定待ち時間
			'
			If							_
			(							_
				( fAimChl1 <> 0 And cntAimChl1 <> 0 ) Or	_
				( fAimChl2 <> 0 And cntAimChl2 <> 0 )		_
			)							_
			Then

				cntAimHed		= wAimHed

			End If

			If fAimHed Then

				' 示更新処理
				DspRfs( lblStsHed, lblNowHed, lblCntHed, cntAimHed, wAimHed, cntAimhed.ToString() )

			End If



			'
			'	チャンバ内真空度
			'	ピラニー真空計測定値取得
			'
			raw			= aiget( GPaiPRS, 2 )

			' ＲＡＷデ－タから圧力(Pa)へ換算
			ccmbprs			= cvtr2GP_Pa( raw )

' debug
' ccmbprs		= 4.9

			'
			'	目標圧到達チェック
			'
			' 動作モード
			If PrmMode = 0 Then

				' 従来動作モード
				If ( wAimCmb <= ccmbprs ) Then

					' 条件成立していないのでタイマーカウンタを更新
					cntAimCmb		= PrmTcAimCmb

				End If

			Else

				' トーカロベント動作
				If ( wAimCmb > ccmbprs ) Then

					' 条件成立していないのでタイマーカウンタを更新
					cntAimCmb		= PrmTcAimCmb

				End If

			End If

			If fAimCmb Then

				' 示更新処理
				DspRfs( lblStsCmb, lblNowCmb, lblCntCmb, cntAimCmb, PrmTcAimCmb, ccmbprs.ToString( "0" ) )

			End If



			'
			'	ウエハ裏面圧力
			'	バラトロン真空計の値を取得
			'
			raw			= aiget( GMaiPRS, 1 )

			' ＲＡＷデ－タからＰａへ換算
			cwbkprs			= cvtr2GM_Pa( raw )

'debug
' cwbkprs		= 78


			'
			'	目標圧到達チェック
			'
			If ( wAimWbp <= cwbkprs ) Then

				' 条件成立していないのでタイマーカウンタを更新
				cntAimWbp		= PrmTcAimWbp

			End If

			If fAimWbp Then

				' 示更新処理
				DspRfs( lblStsWbp, lblNowWbp, lblCntWbp, cntAimWbp, PrmTcAimWbp, cwbkprs.ToString( "0" ) )

			End If



			'
			'	測定開始条件が全て成立したかチェック
			'
			If							_
				(						_
					fAimChl1 = 0 Or				_
					( fAimChl1 <> 0 And 0 >= cntAimChl1 )	_
				) And						_
				(						_
					fAimChl2 = 0  Or			_
					( fAimChl2 <> 0 And 0 >= cntAimChl2 )	_
				) And						_
				(						_
					fAimCmb = 0 Or				_
					( fAimCmb <> 0 And 0 >= cntAimCmb )	_
				) And						_
				(						_
					fAimWbp = 0 Or				_
					( fAimWbp <> 0 And  0 >= cntAimWbp )	_
				) And						_
				(						_
					fAimHed = 0 Or				_
					( fAimHed <> 0 And 0 >= cntAimHed )	_
				)						_
			Then

				If ( 0 >= cntTcAllOK ) Then

					' 全条件成立した
					rtn			= 1

				End If

			Else

				cntTcAllOK		= PrmTcAllOK

			End If


			' 示更新処理
			DspRfs( lblStsAllOK, lblNowAllOK, lblCntAllOK, cntTcAllOK, PrmTcAllOK, cntTcAllOK.ToString() )


			Application.DoEvents()


		Loop While False



		return rtn

	End Function



	'*****
	'	タイマーのカウントダウン
	'*****
	Private Sub TimDCnt()


		' 総合待ち時間カタイマーウンタ
		If cntWTim Then

			cntWTim			-= 1

		End If

		' 全条件成立確定時間タイマーカウンタ
		If cntTcAllOK Then

			cntTcAllOK		-= 1

		End If

		' サーモチラーＣＨ１目標到達判定タイマー値
		If cntAimChl1 Then

			cntAimChl1		-= 1

		End If

		' サーモチラーＣＨ２目標到達判定タイマー値
		If cntAimChl2 Then

			cntAimChl2		-= 1

		End If

		' 電極ヘッド温度安定待ち時間
		If cntAimHed Then

			cntAimHed		-= 1

		End If

		' チャンバ内圧力目標到達判定タイマー値
		If cntAimCmb Then

			cntAimCmb		-= 1

		End If

		' ウエハ裏面圧力目標到達判定タイマー値
		If cntAimWbp Then

			cntAimWbp		-= 1

		End If


	End Sub


	'*****
	'	表示更新インターバルタイマー
	'*****
	Private Sub TimItv_Tick					_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles TimItv.Tick

		Dim sts			As Integer
		Dim rslt		As DialogResult 



		' インターバルタイマー動作を一旦禁止にする
		TimItv.Enabled		= False

		' ブリンク表示フラグを反転
		flgBlink		= Not flgBlink

		' タイマーのカウントダウン
		TimDCnt()

		' 測定開始条件成立判定
		sts			= JdgTstCond()


		'
		'	全ての条件成立したか、
		'	総合待ち時間を経過したら終了
		'
		If sts Then

			' 条件成立
			StsFrmWaitCdt		= 0

			' 試験開始条件待ち処理終了・フォームを閉じる
			Me.Close()


		ElseIf  cntWTim = 0 Then


			'
			'	パトライト・オペレータ確認要求表示Ａ
			'
			PTLOpeReqTypeAon()

			rslt			= MessageBox.Show	_
			(						_
				"試験開始条件待ちタイムアウト発生、試験を中止しますか？",			_
				"確認",					_
				MessageBoxButtons.YesNo			_
			)


			'
			'	パトライト・オペレータ確認要求表示Ａ終了
			'
			PTLOpeReqTypeAoff()


			If rslt = Windows.Forms.DialogResult.Yes Then

				' フォーム処理結果戻り値・試験中止をセット
				StsFrmWaitCdt		= 1

				' 試験開始条件待ち処理終了・フォームを閉じる
				Me.Close()

			Else

				' 総合待ち時間カタイマーウンタ初期値セット
				cntWTim			= PrmWTim

				' インターバルタイマー動作を許可する
				TimItv.Enabled		= True

			End If

		Else

			' インターバルタイマー動作を許可する
			TimItv.Enabled		= True

		End If



	End Sub



	'*****
	'	試験中止ボタンクリックイベント
	'*****
	Private Sub BtnStop_Click				_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles BtnStop.Click



		' インターバルタイマー動作を禁止する
		TimItv.Enabled		= False


		If							_
			MessageBox.Show					_
			(						_
				"試験を中止しますか？",			_
				"確認",					_
				MessageBoxButtons.YesNo			_
			) = Windows.Forms.DialogResult.Yes		_
		Then

			' フォーム処理結果戻り値・試験中止をセット
			StsFrmWaitCdt		= 1

			Me.Close()

		Else

			' インターバルタイマー動作を許可する
			TimItv.Enabled		= True

		End If


	End Sub



	'*****
	'	待ちをスキップする for debug
	'*****
	Private Sub BtnSkip_Click				_
	(							_
		ByVal sender		As System.Object,	_
		ByVal e			As System.EventArgs	_
	)	Handles BtnSkip.Click




		' インターバルタイマー動作を禁止する
		TimItv.Enabled		= False


		If							_
			MessageBox.Show					_
			(						_
				"待たなくていいですか？",		_
				"確認",					_
				MessageBoxButtons.YesNo			_
			) = Windows.Forms.DialogResult.Yes		_
		Then

			' フォーム処理結果戻り値
			StsFrmWaitCdt		= 0

			Me.Close()

		Else

			' インターバルタイマー動作を許可する
			TimItv.Enabled		= True

		End If



	End Sub




End Class