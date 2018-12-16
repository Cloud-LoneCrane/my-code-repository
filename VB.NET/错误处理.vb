Try
	cboT.SelectedIndex = cboT.Items.Count - 1
Catch ex As Exception
	Dim strErr As String = ""
	strErr = ex.Message
	ErrorHandler(strErr)
End Try



            Try
                '÷¥––SQL√¸¡Ó
                count = cmd.ExecuteNonQuery()
     Catch ex As OleDb.OleDbException
                Dim strErr As String = ""
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As Odbc.OdbcException
                Dim strErr As String = ""
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As SqlException
                Dim strErr As String = ""
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As MySqlException
                Dim strErr As String = ""
                strErr = ex.ErrorCode & Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            Catch ex As SystemException
                Dim strErr As String = ""
                strErr = Chr(32) & ex.Message
                ErrorHandler(strErr)
                Return -1
            End Try