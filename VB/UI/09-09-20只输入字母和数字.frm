'********************************* ������Ч�Լ��� ***************************************************
Public Sub chkNumAndLetters(KeyAscii As Integer) '���ֻ��Ϊ���ֺ���ĸ
  If KeyAscii <> 8 Then '�˸��������
   If (KeyAscii >= 48 And KeyAscii <= 57) Or (KeyAscii >= 65 And KeyAscii <= 90) Or (KeyAscii >= 97 And KeyAscii <= 122) Then
   Else
     KeyAscii = 0
  End If
  End If
End Sub
Public Sub chkNum(KeyAscii As Integer) '���ֻ��Ϊ����
  If KeyAscii <> 8 Then  '�˸��������
    If (KeyAscii >= 48 And KeyAscii <= 57) Then
    Else
     KeyAscii = 0
    End If
  End If
End Sub

============================================
Private Sub cboCertType_KeyPress(KeyAscii As Integer)
 Call chkNumAndLetters(KeyAscii) 'ֻ���������ֺ���ĸ
End Sub

Private Sub cboCertType_KeyPress(KeyAscii As Integer)
 Call chkNum(KeyAscii) 'ֻ����������
End Sub
