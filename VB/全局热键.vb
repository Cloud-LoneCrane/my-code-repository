Public Class Form1 
    Public Const WM_HOTKEY = &H312
    Public Const MOD_ALT = &H1
    Public Const MOD_CONTROL = &H2
    Public Const MOD_SHIFT = &H4
    Public Const GWL_WNDPROC = (-4)

 

    Public Declare Auto Function RegisterHotKey Lib "user32.dll" Alias _
        "RegisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Boolean


    Public Declare Auto Function UnRegisterHotKey Lib "user32.dll" Alias _
        "UnregisterHotKey" (ByVal hwnd As IntPtr, ByVal id As Integer) As Boolean

 

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '注册全局热键
        RegisterHotKey(Handle, 0, MOD_CONTROL, Asc("T")) '第一个热键 Ctrl+T
        RegisterHotKey(Handle, 1, Nothing, Keys.F4) '第二个热键 F4
    End Sub

 

    Private Sub Form1_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        '注销全局热键
        UnRegisterHotKey(Handle, 0)
        UnRegisterHotKey(Handle, 1)
    End Sub

 

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_HOTKEY Then
            MsgBox("在这里添加你要执行的代码", MsgBoxStyle.Information, "全局热键")
        End If
        MyBase.WndProc(m)
    End Sub
End Class

