
'限制输入长度
Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
Private Const CB_LIMITTEXT = &H141
Private Const EM_LIMITTEXT = &HC5

Private Sub Form_Load()
    SendMessage Text1.hwnd, _
        EM_LIMITTEXT, 4, 0
    SendMessage Combo1.hwnd, _
        CB_LIMITTEXT, 4, 0
End Sub        