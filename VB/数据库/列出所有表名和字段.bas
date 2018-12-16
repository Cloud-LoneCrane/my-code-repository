
'============================================
'功能：列出所有表的名字
'============================================
Private Sub InitFrame()
On Error GoTo myerr

  Dim strConn As String
  Dim rstSchema As New ADODB.Recordset
  Dim i As Long
    '  strConn = "Provider=SQLOLEDB.1;Password=jift;Persist Security Info=True;User ID=sa;Initial Catalog=wbglsql;Data Source=."
      
     '=================  Access     ========================
   strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & txtDBName.Text
    
      AdoConn.Open strConn
            
      Set rstSchema = AdoConn.OpenSchema(adSchemaTables)
        
      i = 0
      Do Until rstSchema.EOF
                  
                  If rstSchema!TAble_type = "TABLE" Then
                    cboTableName.AddItem rstSchema!TABLE_NAME
                  End If
              rstSchema.MoveNext
              i = i + 1
      Loop
      rstSchema.Close: Set rstSchema = Nothing
      AdoConn.Close: Set AdoConn = Nothing

    lblInfoTip.Caption = "共" & i & "个表"
    cboTableName.Text = cboTableName.List(0)
    
    Call cboTableName_Click
    cboFieldName.Text = cboFieldName.List(0)
Exit Sub
myerr:
    MsgBox Err.Number & " " & Err.Description
End Sub