
 Dim intWidth As Integer = 0
        intWidth = lvwFilelist.Width / 2


        With lvwFilelist
            .Columns.Add("共享资源", intWidth, HorizontalAlignment.Left)
            .Columns.Add("路径", intWidth, HorizontalAlignment.Left)
        End With

        Dim objWMIService As Object = GetObject("winmgmts:\\" & "127.0.0.1")
        Dim colShares = objWMIService.ExecQuery("Select * from Win32_Share")

        GetSharedFolders("127.0.0.1")

        Dim objshare
        For Each objshare In colShares
            If objshare.Type = 0 Then

                Dim lvwItem As New ListViewItem

                lvwItem.Text = objshare.name
                lvwItem.SubItems.Add(objshare.Path)

                lvwFilelist.Items.Add(lvwItem)

            End If
        Next