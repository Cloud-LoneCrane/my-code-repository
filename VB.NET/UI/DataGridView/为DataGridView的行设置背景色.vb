''' <summary>
''' 为DataGridView的行设置背景色,使用数据绑定时可以用
''' </summary>
''' <param name="sender"></param>
''' <param name="colorOdd">奇数行</param>
''' <param name="colorEven">偶数行</param>
''' <remarks> dgvAcc_DataBindingComplete() 中调用 ,格式:DataGridView_Row_Color(sender, Color.White, Color.Lavender)</remarks>
Public Sub DataGridView_Row_Color(ByVal sender As Object, ByVal colorOdd As System.Drawing.Color, ByVal colorEven As System.Drawing.Color)
	Dim dgvTmp As System.Windows.Forms.DataGridView

	dgvTmp = sender

	'遍历每一行 
	For Each dgvRowTmp As DataGridViewRow In dgvTmp.Rows

		'取特定列的值，列索引是INDEX 
		Dim i As Int32 = 0
		i = dgvRowTmp.Index '当前的索引

		If i Mod 2 = 0 Then '偶数
			dgvRowTmp.DefaultCellStyle.BackColor = colorOdd
		Else '奇数
			dgvRowTmp.DefaultCellStyle.BackColor = colorEven
		End If

	Next
End Sub