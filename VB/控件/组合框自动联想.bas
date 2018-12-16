
'组合框自动联想.frm
Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
Public Const CB_FINDSTRING = &H14C

Private Sub Text1_Change()
   Combo2.ListIndex = SendMessage(Combo2.hwnd, CB_FINDSTRING, -1, ByVal CStr(Text1.Text))
End Sub
