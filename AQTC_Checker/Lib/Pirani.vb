Module Pirani

	'/*
	'	ＭＫＳ社製ピラニ真空計・シリーズ９４５
	'	アナログ出力はリニアライズされていない為、
	'	本変換テーブルで変換する
	'*/
 
	Dim PRStbl(,)		As Double	=
	{
		{	1.3e-02,	0.211		},
		{	6.7e-02,	0.217		},
		{	1.3e-01,	0.225		},
		{	3.3e-01,	0.246		},
		{	6.7e-01,	0.278		},
		{	1.0e+00,	0.307		},
		{	1.3e+00,	0.333		},
		{	2.7e+00,	0.420		},
		{	4.0e+00,	0.491		},
		{	5.3e+00,	0.552		},
		{	6.7e+00,	0.606		},
		{	8.0e+00,	0.655		},
		{	9.3e+00,	0.700		},
		{	1.1e+01,	0.742		},
		{	1.2e+01,	0.781		},
		{	1.3e+01,	0.818		},
		{	2.7e+01,	1.103		},
		{	4.0e+01,	1.306		},
		{	5.3e+01,	1.464		},
		{	6.7e+01,	1.593		},
		{	8.0e+01,	1.702		},
		{	9.3e+01,	1.796		},
		{	1.1e+02,	1.877		},
		{	1.2e+02,	1.949		},
		{	1.3e+02,	2.013		},
		{	2.7e+02,	2.411		},
		{	4.0e+02,	2.609		},
		{	5.3e+02,	2.729		},
		{	6.7e+02,	2.810		},
		{	8.0e+02,	2.868		},
		{	9.3e+02,	2.911		},
		{	1.1e+03,	2.945		},
		{	1.2e+03,	2.973		},
		{	1.3e+03,	2.995		},
		{	3.3e+03,	3.127		},
		{	6.7e+03,	3.172		},
		{	1.3e+04,	3.193		},
		{	-1.0,		-1.0		}
	}



	''/*****
	''	ＭＫＳ社製・９４５ピラニ真空計
	''	ﾓﾆﾀ電圧からＰａへの変換
	''*****/
	'Public Function cvtv2p(ByVal volt As Double) As Double
	'    'volt   ﾓﾆﾀ電圧
	'    Dim i As Integer
	'    Dim pa As Double
	'    Dim nv As Double
	'    Dim npa As Double
	'    Dim prm As Double
	'
	'    '最低電圧以下かチェック
	'    If PRStbl(0, 1) > volt Then
	'        '最低電圧以下
	'        pa = PRStbl(0, 0)       '1.3e-02
	'    ElseIf PRStbl(36, 1) <= volt Then
	'        '最高電圧以上
	'        pa = PRStbl(36, 0)      '1.3e+04
	'    Else
	'        '変換ﾃｰﾌﾞﾙをｻｰﾁ
	'        For i = 1 To 36
	'            If PRStbl(i, 1) < volt Then
	'                Exit For
	'            End If
	'        Next
	'        '２点間の差から傾きを計算
	'        nv = PRStbl(i, 1) - PRStbl(i - 1, 1)
	'        npa = PRStbl(i, 0) - PRStbl(i - 1, 0)
	'        prm = npa / nv
	'
	'        '圧力に変換
	'        pa = (volt - PRStbl(i - 1, 1)) * prm + PRStbl(i - 1, 0)
	'    End If
	'
	'    Return pa
	'End Function

	'*****
	'	※20200207 ２号機用
	'	ＭＫＳ社製・４７５００１－０２－Ｐピラニ真空計
	'
	'	ﾓﾆﾀ電圧からＰａへの変換
	'*****
	Public Function cvtv2p				_
	(						_
		volt			As Double	_
	)	As Double

		Dim pa			As Double

		'
		'	20200227 y.goto 換算式修正
		'
		pa			= Math.Pow( 10, volt - 2 )
	'	pa			= Math.Pow( 10, volt - 4 ) * 133.32

		cvtv2p			= pa

	End Function



	'*****
	'	※20200207 1号機用なので名称を変更
	'	ＭＫＳ社製・９４５ピラニ真空計
	'
	'	ﾓﾆﾀ電圧からＰａへの変換
	'*****
	Public Function OLD_cvtv2p			_
	(						_
		volt			As Double	_
	)	As Double

		Dim i			As Integer
		Dim pa			As Double
		Dim nv			As Double
		Dim npa			As Double
		Dim prm			As Double



		' 最低電圧以下かチェック
		If PRStbl( 0, 1 ) >= volt Then

			' 最低電圧以下
			pa			= 0.013

		ElseIf PRStbl( 36, 1 ) < volt Then

			' 最高電圧以上
			pa			= 13000.0

		Else

			' 変換ﾃｰﾌﾞﾙをｻｰﾁ
			For i = 0 To 35

				If PRStbl( i, 1 ) >= volt Then

					Exit For

				End If

			Next i


			' ２点間の差から傾きを計算
			nv			= PRStbl( i, 1 ) - PRStbl( i - 1, 1 )

			npa			= PRStbl( i, 0 ) - PRStbl( i - 1, 0 )

			prm			= npa / nv

			' 圧力に変換
			pa			= ( volt - PRStbl( i - 1, 1 ) ) * prm + PRStbl( i - 1, 0 )

		End If



		Return pa


	End Function



End Module
