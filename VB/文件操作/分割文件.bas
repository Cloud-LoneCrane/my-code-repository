'================================================================
'���ܣ��ָ��ļ�Ϊһ������
'������
'   ByVal strFilePath As String     �ļ�����
'   ByRef Var As Variant    ������������и�õ����ݰ�
'    ByVal nSize As Long    ��ĳߴ�
'================================================================
Public Function SplitFile(ByVal strFilePath As String, ByRef var() As String, ByVal nSize As Long)As Byte
	SplitFile = 1
On Error GoTo myerr
    
    Dim lPos As Long
    Dim i As Long
    Dim lFileLen As Long
    
    
    '�ļ��ĳ���
    lFileLen = FileLen(strFilePath)
    i = 0
    lPos = 1
    
    ReDim var(lFileLen / nSize) As String
    
    Open strFilePath For Binary As #1    ' ���ļ���
    
    Do While Not EOF(1)   ' ѭ�����ļ�β��
       
    lPos = Loc(1) '��ǰ�α��λ��
       If lPos + nSize > lFileLen Then
           var(i) = Input(lFileLen - lPos + 1, #1)
              '------------------- log -----------------------
            Open App.Path & "\log.txt" For Append As #3   ' ���ļ���
            
            '   Print #2, "*******************************************"
            Print #3, i
            Print #3, var(i)
            
            Close #3   ' �ر��ļ���
         '------------------------------------------------
            Exit Do
            
       End If
       
       var(i) = Input(nSize, #1)   ' ����һ���ַ���
       
         '------------------- log -----------------------
            Open App.Path & "\log.txt" For Append As #3   ' ���ļ���
            
            '   Print #2, "*******************************************"
            Print #3, i
            Print #3, var(i)
            
            Close #3   ' �ر��ļ���
         '------------------------------------------------
       i = i + 1
    Loop
    Close #1   ' �ر��ļ���

Exit Function
myerr:
      Close #1   ' �ر��ļ���
    MsgBox Err.Number & " " & Err.Description, vbInformation
    SplitFile = 0
End Function

'================================================================
'���ܣ�������ϲ�Ϊһ���ļ�
'������
'   ByVal strFilePath As String     �������ݵ��ļ�
'   ByRef Var As Variant    ���ݰ���ɵ�����
'================================================================
Public Function UnSplitFile(ByVal strFilePath As String, ByRef var() As String)As Byte
	UnSplitFile=1
On Error GoTo myerr
    
    Dim i As Long
    
    Open strFilePath For Output As #1   ' ���ļ���
    
    For i = LBound(var) To UBound(var)
        Print #1, var(i);
    Next
    
    Close #1   ' �ر��ļ���

Exit Function
myerr:
    MsgBox Err.Number & " " & Err.Description, vbInformation
    UnSplitFile=0
End Function