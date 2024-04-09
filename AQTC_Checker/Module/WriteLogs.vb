Imports System.IO


Module WriteLogs



	'*****
	'	ログ保存
	'*****
	Public Sub WriteLog				_
	(						_
		ByVal AppPath		As String,	_
		ByVal SEL		As String,	_
		ByVal MES		As String	_
	)

		Dim Buf			As String
		Dim FName		As String
		Dim FPath		As String
		Dim dt			As DateTime = DateTime.Now



		Try

			'
			'	アプリケーションのパス取得
			'
			If AppPath <> "" Then

				FPath			= AppPath

			Else

				FPath			= Directory.GetCurrentDirectory()

			End If


			'
			'	ログ年フォルダの作成
			'
			FPath			= FPath & "\log"

			If Dir( FPath, vbDirectory ) = "" Then

				MkDir( FPath )

			End If

			Buf			= dt.ToString( "HH:mm:ss " ) & MES



			'
			'	ファイルのオープン
			'
			If SEL <> "" Then

				FName			= FPath & "\" & SEL & dt.ToString( "yyyyMMdd" ) & ".log"

			Else

				FName			= FPath & "\BK" & dt.ToString( "yyyyMMdd" ) & ".log"

			End If


			Dim TextFile		As IO.StreamWriter = Nothing


			Try

				TextFile		= New IO.StreamWriter( FName, True, System.Text.Encoding.Default )

				TextFile.WriteLine( Buf )

				TextFile.Close()

			Catch ex As Exception

			Finally

				If Not TextFile Is Nothing Then

					TextFile.Dispose()

				End If

				TextFile		= Nothing

			End Try



		Catch ex As Exception

			' 念のためここもエラートラップ

		End Try



	End Sub



	'*****
	'	保持期限の過ぎたログファイルを削除する
	'*****
	Public Function DeleteLogFile				_
	(							_
		ByVal strFolderName		As String,	_
		ByVal strCode			As String,	_
		ByVal intMaintenanceTimeLimit	As Integer	_
	)	As Integer

		'-----------------------------------------------------------
		' 目的   :保持期限の過ぎたファイルを削除する
		' 引数1  :検索するフォルダ
		' 引数2  :識別子
		' 引数3  :ファイル保持日数
		' 戻り値 :なし
		' 備考   :エラーがあった場合やファイル名が想定外の物は削除せず続行する
		'-----------------------------------------------------------


		'
		'	指定フォルダのファイルをすべて取得
		'
		Dim files			As New ArrayList
		Dim searchPattern		As String = strCode + "*.log"
		Dim strPath			As String



		If strFolderName <> "" Then

			strPath			= strFolderName

		Else

			strPath			= Directory.GetCurrentDirectory() + "\log"

		End If


		'
		'	folderにあるファイルを取得する
		'
		Dim fs				As String() = System.IO.Directory.GetFiles( strPath, searchPattern )


		' ArrayListに追加する
		files.AddRange( fs )


		'
		'	ループで全てのファイルを処理する
		'
		Dim fileInfo			As String


		For Each fileInfo In files

			' ファイル名のみ取得
			Dim strFileName			As String = Path.GetFileName( fileInfo )

			' ファイルの長さ判定
			If ( strFileName.Length = ( strCode + "yyyyMMdd.log" ).Length ) Then

				Try

					' 年月日に変換する
					Dim fileDate		As DateTime = DateTime.ParseExact	_
					(								_
						strFileName.Substring( strCode.Length, 8 ),		_
						"yyyyMMdd",						_
						Nothing							_
					)

					' 差を求める
					Dim span		As TimeSpan = DateTime.Now.Subtract( fileDate )

					'日付の差とメンテナンスデータ保持期限(intMaintenanceTimeLimit)を比較
					If ( span.Days > intMaintenanceTimeLimit ) Then

						' ファイル日付が保持期限を越えている場合
						' ファイルの削除を行う
						System.IO.File.Delete( fileInfo )

					End If

				Catch ex As Exception

					' エラーが発生した場合、無視して続ける

				End Try

			End If

		Next fileInfo



		Return 0

	End Function



	'*****
	'	保持期限の過ぎたフォルダーを削除する
	'*****
	Public Function DeleteLogFolder				_
	(							_
		ByVal strFolderName		As String,	_
		ByVal strCode			As String,	_
		ByVal intMaintenanceTimeLimit	As Integer	_
	)	As Integer

		'-----------------------------------------------------------
		' 目的   :保持期限の過ぎたフォルダーを削除する
		' 引数1  :検索するフォルダ
		' 引数2  :識別子
		' 引数3  :ファイル保持日数
		' 戻り値 :なし
		' 備考   :エラーがあった場合やファイル名が想定外の物は削除せず続行する
		'-----------------------------------------------------------



		'
		'	指定フォルダ内のフォルダーをすべて取得
		'
		Dim folder			As New ArrayList
		'Dim searchPattern As String = strCode + "*"


		'
		'	フォルダーにあるファイルを取得する
		'
		Dim fs				As String() = System.IO.Directory.GetDirectories( strFolderName )


		' ArrayListに追加する
		folder.AddRange( fs )


		'
		'	ループで全てのフォルダーを処理する
		'
		Dim fileInfo			As String


		For Each fileInfo In folder


			' ファイル名のみ取得
			Dim strDirName			As String = Path.GetFileName( fileInfo )

			' ファイルの長さ判定
			If ( strDirName.Length = ( strCode + "yyyyMMdd" ).Length) Then

				Try

					' 年月日に変換する
					Dim fileDate			As DateTime = DateTime.ParseExact	_
					(									_
						strDirName.Substring( strCode.Length, 8 ),			_
						"yyyyMMdd",							_
						Nothing								_
					)

					' 差を求める
					Dim span			As TimeSpan = DateTime.Now.Subtract( fileDate )

					' 日付の差とメンテナンスデータ保持期限(intMaintenanceTimeLimit)を比較
					If ( span.Days > intMaintenanceTimeLimit ) Then

						' ファイル日付が保持期限を越えている場合
						' ファイルの削除を行う
						System.IO.Directory.Delete( fileInfo, True )

					End If

				Catch ex As Exception

					' エラーが発生した場合、無視して続ける

				End Try

			End If

		Next fileInfo



		Return 0



	End Function



End Module
