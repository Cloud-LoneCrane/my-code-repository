'��������Ƿ�ӵ�й�������
If My.Computer.Network.IsAvailable = True Then
    MsgBox("Computer is connected.")
Else
    MsgBox("Computer is not connected.")
End If

'Ping ������
If My.Computer.Network.Ping("198.01.01.01") Then
  MsgBox("Server pinged successfully.")
Else
  MsgBox("Ping request timed out.")
End If

'�ϴ�
My.Computer.Network.UploadFile("H:\��ϰ\YQS.sln", "ftp://127.0.0.1/YQS.sln")
'����     
My.Computer.Network.DownloadFile("ftp://127.0.0.1/YQS.sln", "E:\YAS.sln")