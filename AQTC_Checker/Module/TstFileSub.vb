

Imports System.IO


Module TstFileSub



	'*****
	'	試験パラメータファイル名をコンボボックスに設定する
	'*****
	Public Function GetTestFiletoCombbox			_
	(							_
		ByVal strFolderName	As String,		_
		ByRef cbo		As ComboBox		_
	)	As Integer

		'-----------------------------------------------------------
		'目的   :試験パラメータファイル名を取得する
		'引数1  :リードするフォルダ（絶対パス）　""は規定フォルダ
		'引数2  :セットするコンボボックス
		'戻り値 :ファイル数
		'備考   :
		'-----------------------------------------------------------


		' 指定フォルダのファイルをすべて取得
		Dim files		As New ArrayList
		Dim strPath		As String
		Dim cnt			As Integer = 0


		' コンボボックスをクリア
		cbo.Items.Clear()


		' フォルダの設定
		If strFolderName <> "" Then

			strPath			= strFolderName

		Else

			strPath			= Directory.GetCurrentDirectory() + "\test"

		End If


		' フォルダ内のファイルを取得する
		Dim fs			As String() = System.IO.Directory.GetFiles( strPath, "*.dat" )


		' ArrayListに追加する
		files.AddRange( fs )


		' ループで全てのファイルを処理する
		Dim fileInfo		As String


		For Each fileInfo In files

			'ファイル名のみ取得
			Dim strFileName		As String = Path.GetFileName( fileInfo )

			' コンボボックスへ拡張子を除いて追加
			cbo.Items.Add( strFileName.Replace( ".dat", "" ) )

			cnt			+= 1

		Next fileInfo


		Return cnt

	End Function


End Module
