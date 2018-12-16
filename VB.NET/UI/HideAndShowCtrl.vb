''' <summary>
''' 显示下一个控件
''' </summary>
''' <param name="sender"></param>
''' <remarks></remarks>
Private Sub ShowRateCtrl(ByVal sender As System.Object)
	Dim cboTmp As ComboBox

	cboTmp = sender

	Dim strTmpArray() As String '得到控件名
	Dim intTmpT As Integer = 0

	strTmpArray = Split(sender.Name, "_")
	intTmpT = strTmpArray(1)
	intTmpT += 1

	Dim arStrCtrl() As String = {chkRateSet_0.Name, cboEndTime_0.Name, txtRate_0.Name, _
								lblFrom_0.Name, lblStartTime_0.Name, lblTo_0.Name, lblUnit_0.Name _
								} '控件名数组


	If cboTmp.Text <> cboTmp.Items.Item((cboTmp.Items.Count - 1)) Then

		Dim ctrl() As System.Windows.Forms.Control
		Dim i As Integer = 0
		For i = 0 To UBound(arStrCtrl)

			Dim strCtrlNameTmpArray() As String
			Dim intTmp As Integer = 0

			strCtrlNameTmpArray = Split(arStrCtrl(i), "_")
			Try
				intTmp = strCtrlNameTmpArray(1)

				ctrl = Me.Controls.Find(strCtrlNameTmpArray(0) & "_" & intTmpT, True)
			Catch ex As Exception
				Dim strErr As String = ""
				strErr = ex.Message
				ErrorHandler(strErr)
			End Try

			Dim j As Integer = 0
			For j = 0 To UBound(ctrl)
				ctrl(j).Visible = True

				Dim strCtrlName As String = ""

				strCtrlName = ctrl(j).Name
				strCtrlName = strCtrlName.Remove(strCtrlName.Length - 2)

				'StartTime 
				If "lblStartTime" = strCtrlName Then
					ctrl(j).Text = cboTmp.Text
				End If

				'EndTime 
				If TypeOf ctrl(j) Is ComboBox Then
					Dim cboT As ComboBox
					cboT = ctrl(j)

					Try
						cboT.SelectedIndex = cboT.Items.Count - 1
					Catch ex As Exception
						Dim strErr As String = ""
						strErr = ex.Message
						ErrorHandler(strErr)
					End Try

				End If
			Next

		Next
	Else
		Dim ctrl() As System.Windows.Forms.Control
		Dim i As Integer = 0
		For i = 0 To UBound(arStrCtrl)

			Dim strCtrlNameTmpArray() As String
			Dim intTmp As Integer = 0

			strCtrlNameTmpArray = Split(arStrCtrl(i), "_")

			Try
				intTmp = strCtrlNameTmpArray(1)

				ctrl = Me.Controls.Find(strCtrlNameTmpArray(0) & "_" & intTmpT, True)

			Catch ex As Exception
				Dim strErr As String = ""
				strErr = ex.Message
				ErrorHandler(strErr)
			End Try

			Dim j As Integer = 0
			For j = 0 To UBound(ctrl)
				ctrl(j).Visible = False

				'EndTime
				If TypeOf ctrl(j) Is ComboBox Then
					Dim cboT As ComboBox
					cboT = ctrl(j)

					Try
						cboT.SelectedIndex = cboT.Items.Count - 1
					Catch ex As Exception
						Dim strErr As String = ""
						strErr = ex.Message
						ErrorHandler(strErr)
					End Try

				End If
			Next

		Next
	End If

End Sub