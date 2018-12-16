'检查计算机是否拥有工作连接
If My.Computer.Network.IsAvailable = True Then
    MsgBox("Computer is connected.")
Else
    MsgBox("Computer is not connected.")
End If

'Ping 服务器
If My.Computer.Network.Ping("198.01.01.01") Then
  MsgBox("Server pinged successfully.")
Else
  MsgBox("Ping request timed out.")
End If

'上传
My.Computer.Network.UploadFile("H:\练习\YQS.sln", "ftp://127.0.0.1/YQS.sln")
'下载     
My.Computer.Network.DownloadFile("ftp://127.0.0.1/YQS.sln", "E:\YAS.sln")