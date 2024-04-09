'*****
'	動作ログ表示用ウインドウクラス
'
'	リストビューを使用して動作ログを表示する
'*****
Public Class FrmLog


	'
	'	最大ログ表示件数
	'
	Const LogEndPos		As Integer = 5000



	'*****
	'	ログ表示初期処理
	'*****
	Public Sub LogDspInit()



		With ListView1

			'項目を表示する方法を設定
			.View			= View.Details

			.Columns.Add( " 時刻    メッセージ", 3000, HorizontalAlignment.Left )

		'	.Columns.Add( "メッセージ", 3000, HorizontalAlignment.Left )

		End With


	End Sub



	'*****
	'	ログ表示リストビューに１件追加
	'*****
	Public Sub  LogDspAdd					_
	(							_
		ByRef dtstr		As String,		_
		ByRef msg		As String,		_
		ByVal col		As System.Drawing.Color	_
	)

		Dim wkstr		As String
		Dim ptr			As integer



		With ListView1


			'
			'	ログメッセージが指定件数に達している場合は
			'	最も古いメッセージを１件削除する
			'
			If LogEndPos <= .Items.Count then

				'
				'	最も古いログを１件削除
				'
				.Items.RemoveAt( 0 )

			End If



			'
			'	日時文字列の指定が無い場合は現在時刻とする
			'
			If dtstr = "" Then

				wkstr			= DateTime.Now.ToString( "yy/MM/dd  HH:mm:ss" )

			Else

				wkstr			= dtstr

			End If


			' 指定メッセージをリストビューへ追加する位置
			ptr			= .Items.Count


			'
			'	メッセージをリストビューへ追加
			'
			.Items.Add( wkstr + "  " + msg, ptr )

		'	.Items( ptr ).SubItems.Add( msg )

			' 背景色
		'	.Items( ptr ).BackColor		= Color.DarkGray


			If col <> Color.Empty Then

				' 前景色
				.Items( ptr ).ForeColor		= col

			End If



			'
			'	追加したメッセージが表示されるようにする
			'
			.Items( .Items.Count - 1 ).EnsureVisible

			' 20200508 ログファイルに記録
			WriteLog( "", "LG", wkstr + "  " + msg )

		End With


	End Sub



End Class