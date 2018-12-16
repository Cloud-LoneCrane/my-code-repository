'�ṹ��Ķ����ַ�����Ա��CopyMemory �ĵڶ�������ʱ������ʹ��ByVal  ����

'����Ĭ����ByRef ����ַ����
Public Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" _
(Destination As Any, Source As Any, ByVal Length As Long)

'=================================================
'���ܣ�  �ֽ����� ת��Ϊ ������
'������
'	ByRef bytBuf() As Byte	�ֽ�����(��С��4���ֽڴ�С)
'����ֵ��
'	Long	������
'=================================================
Function ByteToLong(ByRef bytBuf() As Byte) As Long

	Dim lngTmp As Long
	
	Call CopyMemory(lngTmp, bytBuf(0), 4)
	ByteToLong = lngTmp
	
End Function


'=================================================
'����:	������ ת��Ϊ �ֽ�����
'������
'	ByVal lngTmp As Long	������
'	ByRef bytBuf() As Byte	�ֽ�����
'=================================================
Function LongToByte(ByVal lngTmp As Long, ByRef bytBuf() As Byte)

	Call CopyMemory(bytBuf(0), lngTmp, 4)

End Function

'=================================================
'���ܣ�  �ַ��� ת��Ϊ �ֽ�����
'������
'   [in]    ByVal strSrc As String  �ַ���
'   [out]   ByRef bytBuf() As Byte �ֽ�����
'   [in]    ByVal nSIze As Long   �ֽڵĸ�����Ӧ��Ϊ�ַ�������2��
'����ֵ��
'   Byte    �ֽ��� ��0Ϊ�� ���ʧ�ܷ���ֵΪ0
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
'���ܣ�	�ֽ����� ת��Ϊ �ַ���
'������
' 	[in]	ByRef bytBuf() As Byte  �ֽ�����
'	[out]	ByRef strDes As String  �ַ���
'=================================================
Function ByteToString(ByRef bytBuf() As Byte, ByRef strDes As String)
	strDes = StrConv(bytBuf, vbUnicode)
End Function

