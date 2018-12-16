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
'���ܣ��������
'=====================================
Public Sub LckMouse() 
	Dim r As RECT
	
	'�����������
	r.Left = 100: r.Top = 100
	r.Right = 100: r.Bottom = 100
	
	'����
	ClipCursor r
End Sub

'=====================================
'���ܣ��������
'=====================================
Public Sub UnlckMouse() 

	'���������lpRect�ǽṹ���ָ�� NULL��ʾ���������
	ClipCursor ByVal 0&
End Sub
