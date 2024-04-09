

Module SysErrLib



	'/*****
	'	システムエラー発生の通知
	'	システムエラーメッセージをメッセージボックスに表示し確認入力を待つ
	'	メッセージと発生日付、時間をログに保存する
	'
	'	<return>
	'	確認入力された文字のコード
	'*****/
	Public Function syserr				_
	(						_
		ByVal msg		As String	_
	)	As Integer

		'msg;		ｴﾗｰﾒｯｾｰｼﾞ

		Dim buf			As String
		Dim rtn			As Integer



		' ﾒｯｾｰｼﾞを表示して、ｵﾍﾟﾚｰﾀの確認入力を受け付ける
		buf			= PROGID + "ｼｽﾃﾑｴﾗｰ" + msg

		WriteLog( "", "LG", buf )

		rtn			= flipbz1r( OPIPok, buf )



		Return rtn

	End Function



	'*****
	'	メッセージ表示してオペレータの確認入力を待つ
	'*****
	Public Function flipprc				_
	(						_
		ByVal md		As Integer,	_
		ByVal bf		As String	_
	)

		Dim rtn			As Integer



		Dim dlg			As New MessageDlg

		dlg.btn			= md

		dlg.msg			= bf

		dlg.ShowDialog()

		rtn			= dlg.DialogResult

		dlg.Dispose()



		Return ( rtn )

	End Function



	'*****
	'	メッセージ表示してオペレータの確認入力を待つ
	'	パトライト赤点滅
	'*****
	Public Function flipr			_
	(						_
		ByVal md		As Integer,	_
		ByVal bf		As String	_
	)

		Dim rtn			As Integer



		'
		'	パトライト赤点滅
		'
		PTLctl			= PTLctl Or PTLctlREDfl


		rtn			= flipprc( md, bf )


		'
		'	パトライト赤点滅終了
		'
		PTLctl			= PTLctl And ( Not  PTLctlREDfl )



		Return ( rtn )

	End Function



	'*****
	'	メッセージ表示してオペレータの確認入力を待つ
	'	パトライトブザー吹鳴有り
	'*****
	Public Function flipbz1r			_
	(						_
		ByVal md		As Integer,	_
		ByVal bf		As String	_
	)

		Dim rtn			As Integer



		'
		'	パトライト・オペレータ確認要求表示Ａ
		'
		PTLOpeReqTypeAon()


		rtn			= flipprc( md, bf )


		'
		'	パトライト・オペレータ確認要求表示Ａ終了
		'
		PTLOpeReqTypeAoff()



		Return ( rtn )

	End Function



End Module
