public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
public Const LB_FINDSTRING = &H18F
Public Const LB_ERR = (-1)

'===================================================
'���ܣ������
'===================================================
Private Sub cmdAddColumn_Click()

	Dim strItem As String
	Dim lngRes As Long

	lngRes = SendMessage(listColumn.hwnd, LB_FINDSTRING, 0, ByVal strTmp)
	If LB_ERR = lngRes Then
		listColumn.AddItem strTmp
	Else
	MsgBox "�����ظ����", vbInformation
	End If
	
End Sub	