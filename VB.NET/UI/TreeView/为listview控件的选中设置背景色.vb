
''' <summary>
''' 为treeview控件的选中设置背景色
''' </summary>
''' <param name="tvwT">Treeview控件</param>
''' <param name="colorM">背景色</param>
''' <remarks>在treeview的AfterSelect中调用</remarks>
Private Sub TreeView_BackColor_Set(ByRef tvwT As TreeView, ByVal colorM As Color)
	TraverseTreeNodes(tvwT.Nodes)
	tvwT.SelectedNode.BackColor = colorM
End Sub

''' <summary>
''' 递归遍历treeview的所有节点
''' </summary>
''' <param name="nods"></param>
''' <remarks></remarks>
Private Sub TraverseTreeNodes(ByVal nods As System.Windows.Forms.TreeNodeCollection)
	For Each nod As TreeNode In nods
		nod.BackColor = Color.White
		TraverseTreeNodes(nod.Nodes)
	Next
End Sub