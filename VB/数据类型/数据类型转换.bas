'结构体的定长字符串成员在CopyMemory 的第二个参数时，必须使用ByVal  修饰

'参数默认是ByRef 按地址传递
Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" _
(Destination As Any, Source As Any, ByVal Length As Long)

'=================================================
'功能：  字节数组 转换为 长整型
'参数：
'	ByRef bytBuf() As Byte	字节数组(不小于4个字节大小)
'返回值：
'	Long	长整型
'=================================================
Function ByteToLong(ByRef bytBuf() As Byte) As Long

	Dim lngTmp As Long
	
	Call CopyMemory(lngTmp, bytBuf(0), 4)
	ByteToLong = lngTmp
	
End Function


'=================================================
'功能:	长整型 转换为 字节数组
'参数：
'	ByVal lngTmp As Long	长整型
'	ByRef bytBuf() As Byte	字节数组
'=================================================
Function LongToByte(ByVal lngTmp As Long, ByRef bytBuf() As Byte)

	Call CopyMemory(bytBuf(0), lngTmp, 4)

End Function

'=================================================
'功能：  字符串 转换为 字节数组
'参数：
'   [in]    ByVal strSrc As String  字符串
'   [out]   ByRef bytBuf() As Byte 字节数组
'   [in]    ByVal nSIze As Long   字节的个数，应设为字符个数的2倍
'返回值：
'   Byte    字节型 非0为真 如果失败返回值为0
'=================================================
Function StringToByte(ByVal strSrc As String, ByRef bytBuf() As Byte, ByVal nSIze As Long) As Byte

    If UBound(bytBuf) - LBound(bytBuf) + 1 < nSIze Then
        StringToByte = 0
        Exit Function
    End If
   Call CopyMemory(bytBuf(0), ByVal strSrc, nSIze)
   StringToByte = 1
End Function

'=================================================
'功能：	字节数组 转换为 字符串
'参数：
' 	[in]	ByRef bytBuf() As Byte  字节数组
'	[out]	ByRef strDes As String  字符串
'=================================================
Function ByteToString(ByRef bytBuf() As Byte, ByRef strDes As String)
	strDes = StrConv(bytBuf, vbUnicode)
End Function

