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
' ����: ģ�����ĵ��
' ������
'   x �������Ļ���Ͻǵ� x����
'   y �������Ļ���Ͻǵ� y����
'=======================================
Function Click(x As Long, y As Long)

    '��������洢��ǰ����λ��
    Dim OldPoint As POINTAPI
    
    '��õ�ǰ����λ��
    Call GetCursorPos(OldPoint)
    
    '�ƶ���굽ָ��λ�ã���泬���������ڵ�λ�õ���Ļ���꣩
    Call SetCursorPos(x, y)
    
    '����������
    mouse_event MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0
    '�ͷ�������
    mouse_event MOUSEEVENTF_LEFTUP, 0, 0, 0, 0
    
    '�ƶ���굽ԭ��λ��
    Call SetCursorPos(OldPoint.x, OldPoint.y)
    
End Function