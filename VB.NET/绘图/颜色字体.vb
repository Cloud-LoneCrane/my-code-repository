    '�԰������������ɫ��Ϣ���ַ������з���
        Dim intPliter = 0, intPliter1 As Integer = 0
        Dim strFontName As String = ""
        Dim sngFontSize = 0, strFontColor As Single = 0

        intPliter = InStr(strValue, "*")
        intPliter1 = InStr(intPliter + 1, strValue, "*")

        strFontName = Mid(strValue, 1, intPliter - 1)
        sngFontSize = Mid(strValue, intPliter + 1, intPliter1 - intPliter - 1)
        strFontColor = Mid(strValue, intPliter1 + 1)

        '�������� 
        txtZ_NameAndTelFont.Font = New System.Drawing.Font(strFontName, 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(134, Byte))
        txtZ_NameAndTelFont.Text = strFontName & " " & sngFontSize '������ʾ����
        txtZ_NameAndTelFont.ForeColor = Color.FromArgb(CInt(strFontColor)) '������ɫ