Private Declare Sub mouse_event Lib "user32" _
    ( _
    ByVal dwFlags As Long, _
    ByVal dx As Long, _
    ByVal dy As Long, _
    ByVal cButtons As Long, _
    ByVal dwExtraInfo As Long _
    )
Const MOUSEEVENTF_LEFTDOWN = &H2
Const MOUSEEVENTF_LEFTUP = &H4

Private Type POINTAPI
    x As Long
    y As Long
End Type


Private Declare Function GetCursorPos Lib "user32" (lpPoint As POINTAPI) As Long

Private Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long


'=======================================
' 功能: 模拟鼠标的点击
' 参数：
'   x 相对与屏幕左上角的 x坐标
'   y 相对与屏幕左上角的 y坐标
'=======================================
Function Click(x As Long, y As Long)

    '定义变量存储当前光标的位置
    Dim OldPoint As POINTAPI
    
    '获得当前光标的位置
    Call GetCursorPos(OldPoint)
    
    '移动光标到指定位置（广告超级连接所在的位置的屏幕坐标）
    Call SetCursorPos(x, y)
    
    '按下鼠标左键
    mouse_event MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0
    '释放鼠标左键
    mouse_event MOUSEEVENTF_LEFTUP, 0, 0, 0, 0
    
    '移动光标到原来位置
    Call SetCursorPos(OldPoint.x, OldPoint.y)
    
End Function