	'压缩
	Sub CompressionClick(sender As Object, e As EventArgs)
		Dim strSrcFile As String = ""
		Dim strDesFile As String = ""
		
		strSrcFile = Trim(txtSrcFile.Text)
		strDesFile = Trim(txtDesFile.Text)
		
		'check whether the file exist
		If( System.IO.File.Exists(strSrcFile) = False) Then
			Return
		End If
		
		Dim srcStream As System.IO.FileStream = Nothing
		Dim desStream As System.IO.FileStream = Nothing
		Dim compStream As System.IO.Compression.GZipStream
		
		Try
			srcStream = New System.IO.FileStream(strSrcFile, _
				System.IO.FileMode.Open,System.IO.FileAccess.Read, _
				System.IO.FileShare.Read)
			'define the buffer for file
			Dim buf(srcStream.Length-1) As Byte
			
			srcStream.Read(buf,0,buf.Length)
			
			desStream = New System.IO.FileStream(strDesFile, _
				System.IO.FileMode.OpenOrCreate,System.IO.FileAccess.Write)
			
			compStream = New System.IO.Compression.GZipStream(desStream, _
				System.IO.Compression.CompressionMode.Compress,True)
			
			compStream.Write(buf,0,buf.Length)
			
			MessageBox.Show("文件压缩完毕")
			
		Catch ex As Exception
			MsgBox(ex.ToString)
		Finally
			If (Not srcStream Is Nothing) Then
				srcStream.Close
			End If
			
			If (Not compStream Is Nothing) Then
				compStream.Close
			End If
			
			If (Not desStream Is Nothing) Then
				desStream.Close
			End If
		End Try
		
	End Sub
	
	'解压缩
	Sub UnCompressionClick(sender As Object, e As EventArgs)
		Dim strSrcFile As String = ""
		Dim strDesFile As String = ""
		
		strSrcFile = Trim(Me.txtSrcFile.Text)
		strDesFile = Trim(Me.txtDesFile.Text)
		
		If Not (System.IO.File.Exists(strSrcFile)) Then
			MsgBox("文件不存在")
			Me.txtSrcFile.Focus
			Return
		End If
		
		Dim srcStream As System.IO.FileStream = Nothing
		Dim desStream As System.IO.FileStream = Nothing
		Dim decompStream As System.IO.Compression.GZipStream = Nothing
		
		Try
			srcStream = New System.IO.FileStream(strSrcFile,System.IO.FileMode.Open)
			decompStream = New System.IO.Compression.GZipStream(srcStream, _
				System.IO.Compression.CompressionMode.Decompress,True)
			
			Dim buf(4) As Byte
			Dim pos As Integer = srcStream.Length - 4
			
			srcStream.Position = Pos
			srcStream.Read(buf,0,4)
			srcStream.Position = 0
			
			Dim length As Integer= BitConverter.ToInt32(buf,0)
			Dim data(length + 100) As Byte
			Dim offset As Integer = 0
			Dim total As Integer = 0
			Dim bytesRead As Integer = DecompStream.Read(data,offset,100)
			
			While(bytesRead)
				offset += bytesRead
				total += bytesRead
				bytesRead = decompStream.Read(data,offset,100)
			End While
			
			desStream = New System.IO.FileStream(strDesFile, _
			System.IO.FileMode.Create)
			desStream.Write(data,0,total)
			desStream.Flush
			MsgBox("文件解压缩完毕")
		Catch ex As Exception
			MsgBox(ex.ToString)
			Exit Sub
		Finally
			If Not (srcStream Is Nothing) Then 
				srcStream.Close
			End If
			If Not (desStream Is Nothing) Then 
				desStream.Close
			End If
			
			If Not (decompstream Is Nothing ) Then 
				decompstream.Close
			End If
		End Try
		
	End Sub