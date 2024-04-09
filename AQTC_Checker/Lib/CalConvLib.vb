Module CalConvLib


	'/*****
	'	傾向を計算
	'*****/
	'double
	Public Function calvct				_
	(						_
		ByVal dno		As Integer,	_
		ByVal dat		As Double,	_
		ByRef rbf()		As Single,	_
		ByVal nrbf		As Integer	_
	)	As Double

		'dno;		ﾃﾞｰﾀ番号ｶｳﾝﾀ
		'		何番目のﾃﾞｰﾀかを表す数字で、
		'		ﾃﾞｰﾀ発生毎に +1 する
		'		0～
		'dat;		最新データ
		'rbf();		ﾃﾞｰﾀ蓄積するﾘﾝｸﾞﾊﾞｯﾌｧ
		'nrbf;		rbf()のサイズ

		Dim i			As Integer
		Dim n			As Integer
		Dim ndt			As Integer
		Dim avg			As Double



		' データをリングバッファに保存
		rbf( dno Mod nrbf )	= CSng( dat )

		' 傾向計算結果変数クリア
		avg			= 0.0

		' 最初のデータ以外なら、傾向を計算する
		If dno <> 0 Then

			' 有効データ数を求める(最大はnrbf)	
			If ( nrbf <= dno + 1 ) Then

				' 有効データ数は nrbf 個
				ndt			= nrbf - 1

			Else

				' 有効データ数は dno 個
				ndt			= dno

			End If


			' 各々１個前のデータとの差の累計を計算
			i			= dno

			For n = ndt To 1 Step -1

				' 前ﾃﾞｰﾀとの差を計算
				avg			+= rbf( ( i - 1 ) Mod nrbf ) - rbf( i Mod nrbf )

				i			-= 1

			Next


			' 差の平均を計算する
			' avg /= CDbl(ndt - 1)
			avg			/= CDbl( ndt )

		End If



	Return avg


	End Function



End Module
