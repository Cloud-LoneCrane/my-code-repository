'-------------START----------------------���ø�ʽ-----------------------------------
	Dim strErr As String = ""
	Try
		 ......
	Catch ex As SystemException
		Dim strErr As String = ""
		strErr = ex.ToString
		ErrorHandler(strErr)

	End Try
'--------------END---------------------���ø�ʽ-----------------------------------           
    ''' <summary>
    ''' д����־
    ''' </summary>
    ''' <remarks>LOGFILE ���Զ����ȫ�ֳ���</remarks>
    Public Sub ErrorHandler(ByVal strErrdescribe As String)

#If DEBUG Then
        MessageBox.Show(strErrdescribe, frmMain.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
#Else
        Dim swTmp As StreamWriter = File.AppendText(g_LOGFILE)
        swTmp.WriteLine(Now & vbTab & strErrdescribe)
        swTmp.Close()
        swTmp = Nothing
#End If

    End Sub