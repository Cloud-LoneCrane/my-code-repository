  '*************************************************************************
  '   ��������:'�о��ļ����µ������ļ���,�ӵ�������
  '*************************************************************************
  Public Function ListFolderFiles(ByRef myArrayFiles() As String, strFolder As String)
          Dim strFileName     As String
          Dim intI     As Integer
          strFileName = Dir(strFolder & "/")
          If strFileName <> "" Then
                  ReDim myArrayFiles(0) As String
                  intI = 0
                  While strFileName <> ""
                          Debug.Print strFileName
                          ReDim Preserve myArrayFiles(intI) As String
                          myArrayFiles(intI) = strFileName
                          intI = intI + 1
                          strFileName = Dir()
                  Wend
          End If
  End Function

