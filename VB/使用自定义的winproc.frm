Public Const GWL_WNDPROC = (-4)
Public Const WM_RBUTTONDOWN = &H204

Declare Function CallWindowProc Lib "user32" Alias "CallWindowProcA" (ByVal lpPrevWndFunc As Long, ByVal hWnd As Long, ByVal Msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
Declare Function GetWindowLong Lib "user32" Alias "GetWindowLongA" (ByVal hWnd As Long, ByVal nIndex As Long) As Long
Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hWnd As Long, ByVal nIndex As Long, ByVal dwNewLong As Long) As Long

Public prevWndProc As Long

Function WndProc(ByVal hWnd As Long, ByVal Msg As Long, ByVal wParam As Long, ByVal lParam As Long) As Long
    If Msg = WM_RBUTTONDOWN Then
    Else
        WndProc = CallWindowProc(prevWndProc, hWnd, Msg, wParam, lParam)
    End If
End Function

'==================================================
' 功能：用自定义的windowproc函数代替系统的函数
'==================================================
Private Sub Command1_Click()
    '获得 window procedure的地址
    prevWndProc = GetWindowLong(Text1.hWnd, GWL_WNDPROC)
    '用自定义的window procedure来替换系统的window procedure
    SetWindowLong Text1.hWnd, GWL_WNDPROC, AddressOf WndProc
    
    Command1.Enabled = False
End Sub

