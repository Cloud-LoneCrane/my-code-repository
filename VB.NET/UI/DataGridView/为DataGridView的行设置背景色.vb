''' <summary>
''' ΪDataGridView�������ñ���ɫ,ʹ�����ݰ�ʱ������
''' </summary>
''' <param name="sender"></param>
''' <param name="colorOdd">������</param>
''' <param name="colorEven">ż����</param>
''' <remarks> dgvAcc_DataBindingComplete() �е��� ,��ʽ:DataGridView_Row_Color(sender, Color.White, Color.Lavender)</remarks>
Public Sub DataGridView_Row_Color(ByVal sender As Object, ByVal colorOdd As System.Drawing.Color, ByVal colorEven As System.Drawing.Color)
	Dim dgvTmp As System.Windows.Forms.DataGridView

	dgvTmp = sender

	'����ÿһ�� 
	For Each dgvRowTmp As DataGridViewRow In dgvTmp.Rows

		'ȡ�ض��е�ֵ����������INDEX 
		Dim i As Int32 = 0
		i = dgvRowTmp.Index '��ǰ������

		If i Mod 2 = 0 Then 'ż��
			dgvRowTmp.DefaultCellStyle.BackColor = colorOdd
		Else '����
			dgvRowTmp.DefaultCellStyle.BackColor = colorEven
		End If

	Next
End Sub