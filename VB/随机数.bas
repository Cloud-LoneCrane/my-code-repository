'================================================================
'���ܣ�	��������
'������
'	��
'����ֵ��
' 	long  �����
'================================================================
Function Rand() As Long 

On Error GoTo myerr

	Rand = 0
	'��ʼ��ֵ
	Dim MyValue
	Randomize       '   �����������������ʼ���Ķ�����

	Rand = Int((2147483647 * Rnd) + 1)

Exit Function
myerr:
	MsgBox Err.Number & " " & Err.Description, vbInformation
End Function
