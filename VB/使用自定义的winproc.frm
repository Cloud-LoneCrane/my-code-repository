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
' ���ܣ����Զ����windowproc��������ϵͳ�ĺ���
'==================================================
Private Sub Command1_Click()
    '��� window procedure�ĵ�ַ
    prevWndProc = GetWindowLong(Text1.hWnd, GWL_WNDPROC)
    '���Զ����window procedure���滻ϵͳ��window procedure
    SetWindowLong Text1.hWnd, GWL_WNDPROC, AddressOf WndProc
    
    Command1.Enabled = False
End Sub

