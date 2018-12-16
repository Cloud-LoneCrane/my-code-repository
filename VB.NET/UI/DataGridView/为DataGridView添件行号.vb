''' <summary>
''' 为DataGridView添件行号
''' </summary>
''' <param name="sender"></param>
''' <param name="e"></param>
''' <remarks>在 DataGridView1_RowPostPaint()过程中调用 eg.DataGridView_RowNum_ADD(sender, e)</remarks>
Public Sub DataGridView_RowNum_ADD(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs)
	Dim dgvTmp As System.Windows.Forms.DataGridView

	dgvTmp = sender
	Dim rectangle As Rectangle = New Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, _
					  dgvTmp.RowHeadersWidth - 4, _
					  e.RowBounds.Height)

	TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), _
		dgvTmp.RowHeadersDefaultCellStyle.Font, _
		rectangle, _
		dgvTmp.RowHeadersDefaultCellStyle.ForeColor, _
		TextFormatFlags.VerticalCenter Or TextFormatFlags.Right)
End Sub