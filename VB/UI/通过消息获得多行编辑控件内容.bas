Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd _
As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long

Private Declare Sub CopyMemory Lib "kernel32" Alias "RtlMoveMemory" (hpvDest As Any, _
hpvSource As Any, ByVal cbCopy As Long)

Const EM_GETLINECOUNT = &HBA
Const EM_GETLINE = &HC4

Private Sub Command1_Click()
    Dim lCount As Long, l As Long, i As Integer, k As Long, a As String
    
    '获得多行编辑控件的总行数
    lCount = SendMessage(Text1.hwnd, EM_GETLINECOUNT, 0, ByVal 0)
    
    If lCount = 0 Then MsgBox "No lines!": Exit Sub
    ReDim aLines(1 To lCount) As String
    
    i = 512
    a = Space(512) '返回指定数目的空格
    
    List1.Clear
    
    For l = 0 To lCount - 1
    '复制2到内存中
        Call CopyMemory(ByVal a$, i, 2)
        
        '获得编辑控件指定行的内容
        k = SendMessage(Text1.hwnd, EM_GETLINE, l, ByVal a)
        
        aLines(l + 1) = Left(a, k)
        List1.AddItem aLines(l + 1)
    Next
    
End Sub

Private Sub Form_Load()
    Dim l As Long
    Show
    For l = 1 To 50
        Text1.SelStart = Len(Text1.Text): Text1.SelLength = 0
        Text1.SelText = "This is line " & CStr(l) & vbCrLf
    Next
End Sub