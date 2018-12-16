'================================================================
'功能：分割文件为一个数组
'参数：
'   ByVal strFilePath As String     文件名字
'   ByRef Var As Variant    变体用来存放切割好的数据包
'    ByVal nSize As Long    块的尺寸
'================================================================
Public Function SplitFile(ByVal strFilePath As String, ByRef var() As String, ByVal nSize As Long)As Byte
	SplitFile = 1
On Error GoTo myerr
    
    Dim lPos As Long
    Dim i As Long
    Dim lFileLen As Long
    
    
    '文件的长度
    lFileLen = FileLen(strFilePath)
    i = 0
    lPos = 1
    
    ReDim var(lFileLen / nSize) As String
    
    Open strFilePath For Binary As #1    ' 打开文件。
    
    Do While Not EOF(1)   ' 循环至文件尾。
       
    lPos = Loc(1) '当前游标的位置
       If lPos + nSize > lFileLen Then
           var(i) = Input(lFileLen - lPos + 1, #1)
              '------------------- log -----------------------
            Open App.Path & "\log.txt" For Append As #3   ' 打开文件。
            
            '   Print #2, "*******************************************"
            Print #3, i
            Print #3, var(i)
            
            Close #3   ' 关闭文件。
         '------------------------------------------------
            Exit Do
            
       End If
       
       var(i) = Input(nSize, #1)   ' 读入一个字符。
       
         '------------------- log -----------------------
            Open App.Path & "\log.txt" For Append As #3   ' 打开文件。
            
            '   Print #2, "*******************************************"
            Print #3, i
            Print #3, var(i)
            
            Close #3   ' 关闭文件。
         '------------------------------------------------
       i = i + 1
    Loop
    Close #1   ' 关闭文件。

Exit Function
myerr:
      Close #1   ' 关闭文件。
    MsgBox Err.Number & " " & Err.Description, vbInformation
    SplitFile = 0
End Function

'================================================================
'功能：将数组合并为一个文件
'参数：
'   ByVal strFilePath As String     保存数据的文件
'   ByRef Var As Variant    数据包组成的数组
'================================================================
Public Function UnSplitFile(ByVal strFilePath As String, ByRef var() As String)As Byte
	UnSplitFile=1
On Error GoTo myerr
    
    Dim i As Long
    
    Open strFilePath For Output As #1   ' 打开文件。
    
    For i = LBound(var) To UBound(var)
        Print #1, var(i);
    Next
    
    Close #1   ' 关闭文件。

Exit Function
myerr:
    MsgBox Err.Number & " " & Err.Description, vbInformation
    UnSplitFile=0
End Function