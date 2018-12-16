
'********************************
'***针对(客户端)密码的解密过程***
'********************************
Function openpassword(pass As String, passkey As String) As String
Dim i As Integer
Dim j As Integer
Dim sourcepass As String
Dim targerpass As String
Dim str As String
Dim X() As Integer
Dim n As Integer
Dim k As Integer
   On Error GoTo myerr
         openpassword = ""
         i = InStr(pass, "(")
         j = InStr(pass, ")")
         If i <> 0 And j <> 0 Then
             sourcepass = Mid(pass, i + 1, j - 2)
             i = 1
             str = sourcepass
             Do
                i = InStr(str, ";")
                If i <> 0 Then
                  n = n + 1
                  str = Mid(str, i + 1)
                Else
                   i = -1
                End If
             Loop While i <> -1
             
            
             ReDim Preserve X(n)
             str = sourcepass
             i = 1
             j = 1
             For k = 0 To n
                 If k <> n Then
                    i = InStr(str, ";")
                    X(k) = Mid(str, 1, i - 1)
                    str = Mid(str, i + 1)
                 Else
                    X(k) = CInt(str)
                 End If
             Next
             j = 1
             For i = 0 To n
                 If j - 1 = Len(passkey) Then
                    j = 1
                 End If
                 targerpass = targerpass + Chr(X(i) - Asc(Mid(passkey, j, 1)))
                 j = j + 1
                 
             Next
             openpassword = targerpass
         End If
         Exit Function
myerr:
    MsgBox "openpassword" & Err.Source & Err.Number & Err.Description & Format(Now, "yyyy-mm-dd hh:nn:ss") & vbCrLf
End Function
