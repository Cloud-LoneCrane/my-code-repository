''' <summary>
''' 选择ComboBox Items的最后一项
''' </summary>
''' <param name="cboTmp">ComboBox</param>
''' <remarks></remarks>
Public Sub ComboBox_SelectLastIndex(ByVal cboTmp As ComboBox)
	cboTmp.SelectedIndex = cboTmp.Items.Count - 1
End Sub

''' <summary>
''' 选择ComboBox Items的第一项
''' </summary>
''' <param name="cboTmp">ComboBox</param>
''' <remarks></remarks>
Public Sub ComboBox_SelectFirstIndex(ByVal cboTmp As ComboBox)
	cboTmp.SelectedIndex = 0
End Sub
