'********************************* 输入有效性检验 ***************************************************
Public Sub chkNumAndLetters(KeyAscii As Integer) '检查只能为数字和字母
  If KeyAscii <> 8 Then '退格键不屏蔽
   If (KeyAscii >= 48 And KeyAscii <= 57) Or (KeyAscii >= 65 And KeyAscii <= 90) Or (KeyAscii >= 97 And KeyAscii <= 122) Then
   Else
     KeyAscii = 0
  End If
  End If
End Sub
Public Sub chkNum(KeyAscii As Integer) '检查只能为数字
  If KeyAscii <> 8 Then  '退格键不屏蔽
    If (KeyAscii >= 48 And KeyAscii <= 57) Then
    Else
     KeyAscii = 0
    End If
  End If
End Sub

============================================
Private Sub cboCertType_KeyPress(KeyAscii As Integer)
 Call chkNumAndLetters(KeyAscii) '只能输入数字和字母
End Sub

Private Sub cboCertType_KeyPress(KeyAscii As Integer)
 Call chkNum(KeyAscii) '只能输入数字
End Sub
