'================================================================
'功能：	获得随机数
'参数：
'	无
'返回值：
' 	long  随机数
'================================================================
Function Rand() As Long 

On Error GoTo myerr

	Rand = 0
	'开始赋值
	Dim MyValue
	Randomize       '   对随机数生成器做初始化的动作。

	Rand = Int((2147483647 * Rnd) + 1)

Exit Function
myerr:
	MsgBox Err.Number & " " & Err.Description, vbInformation
End Function
