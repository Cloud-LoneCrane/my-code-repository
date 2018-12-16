Const HWND_TOPMOST = -1
Const SWP_NOMOVE = &H2

Private Declare Function SetWindowPos Lib "user32" (ByVal hwnd As Long, _
ByVal hWndInsertAfter As Long, ByVal x As Long, ByVal y As Long, ByVal cx As Long, ByVal cy As Long, ByVal wFlags As Long) As Long

'´°¿ÚÖÃÇ° 
Call SetWindowPos(Me.hwnd, HWND_TOPMOST, 0, 0, 0, 0, 3)