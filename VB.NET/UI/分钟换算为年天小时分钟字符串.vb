''' <summary>
''' ���ӻ���Ϊ����Сʱ�����ַ���
''' </summary>
''' <param name="lngTmp">����</param>
''' <returns>����Сʱ�����ַ��� eg.1��2��3Сʱ36����</returns>
''' <remarks></remarks>
Public Function GetDayHourMinuFromLong(ByVal lngTmp As Long) As String
	Dim lngYear, lngDay, lngHour, lngMinute As Long
	Dim strTmp As String = ""
	Dim strYear, strDay, strHour, strMinute As String

	Try
		strYear = "��"
		strDay = "��"
		strHour = "Сʱ"
		strMinute = "����"

		lngYear = 0
		lngDay = 0
		lngHour = 0
		lngMinute = 0

		lngYear = lngTmp \ (60 * 24 * 365)
		lngDay = (lngTmp - lngYear * (60 * 24 * 365)) \ (60 * 24)
		lngHour = (lngTmp - lngYear * (60 * 24 * 365) - lngDay * (60 * 24)) \ 60
		lngMinute = lngTmp Mod 60

		If lngYear > 0 Then
			strTmp = lngYear & strYear & lngDay & strDay & lngHour & strHour & lngMinute & strMinute
		Else
			If lngDay > 0 Then
				strTmp = lngDay & strDay & lngHour & strHour & lngMinute & strMinute
			Else
				If lngHour > 0 Then
					strTmp = lngHour & strHour & lngMinute & strMinute
				Else
					strTmp = lngMinute & strMinute
				End If
			End If
		End If
	Return strTmp
	
	Catch ex As Exception
#If DEBUG Then
		MsgBox(ex.ToString)
#End If
		Return strTmp
	End Try
End Function