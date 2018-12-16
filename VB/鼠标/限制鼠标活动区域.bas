Public Declare Function GetWindowRect Lib "user32" (ByVal hwnd As Long, lpRect As RECT) As Long
Public Declare Function ClipCursor Lib "user32" (lpRect As Any) As Long
Public Declare Function SetCursorPos Lib "user32" (ByVal x As Long, ByVal y As Long) As Long

Public Type RECT
	Left As Long
	Top As Long
	Right As Long
	Bottom As Long
End Type

'=====================================
'功能：锁定鼠标
'=====================================
Public Sub LckMouse() 
	Dim r As RECT
	
	'设置鼠标活动区域
	r.Left = 100: r.Top = 100
	r.Right = 100: r.Bottom = 100
	
	'锁定
	ClipCursor r
End Sub

'=====================================
'功能：解除锁定
'=====================================
Public Sub UnlckMouse() 

	'解除锁定（lpRect是结构体的指针 NULL表示解除锁定）
	ClipCursor ByVal 0&
End Sub
