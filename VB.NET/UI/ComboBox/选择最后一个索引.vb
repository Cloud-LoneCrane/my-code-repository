''' <summary>
''' ѡ��ComboBox Items�����һ��
''' </summary>
''' <param name="cboTmp">ComboBox</param>
''' <remarks></remarks>
Public Sub ComboBox_SelectLastIndex(ByVal cboTmp As ComboBox)
	cboTmp.SelectedIndex = cboTmp.Items.Count - 1
End Sub

''' <summary>
''' ѡ��ComboBox Items�ĵ�һ��
''' </summary>
''' <param name="cboTmp">ComboBox</param>
''' <remarks></remarks>
Public Sub ComboBox_SelectFirstIndex(ByVal cboTmp As ComboBox)
	cboTmp.SelectedIndex = 0
End Sub
