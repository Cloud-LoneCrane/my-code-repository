  Public Function WeekText2WeekBitValue(ByVal strText As String) As Short
        Dim shtValue As Short = 0
        If strText.IndexOf("星期一") <> -1 Then shtValue = shtValue Or 1
        If strText.IndexOf("星期二") <> -1 Then shtValue = shtValue Or 2
        If strText.IndexOf("星期三") <> -1 Then shtValue = shtValue Or 4
        If strText.IndexOf("星期四") <> -1 Then shtValue = shtValue Or 8
        If strText.IndexOf("星期五") <> -1 Then shtValue = shtValue Or 16
        If strText.IndexOf("星期六") <> -1 Then shtValue = shtValue Or 32
        If strText.IndexOf("星期日") <> -1 Then shtValue = shtValue Or 64
        Return shtValue
    End Function