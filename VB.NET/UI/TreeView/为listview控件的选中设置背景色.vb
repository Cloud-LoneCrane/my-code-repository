
''' <summary>
''' Ϊtreeview�ؼ���ѡ�����ñ���ɫ
''' </summary>
''' <param name="tvwT">Treeview�ؼ�</param>
''' <param name="colorM">����ɫ</param>
''' <remarks>��treeview��AfterSelect�е���</remarks>
Private Sub TreeView_BackColor_Set(ByRef tvwT As TreeView, ByVal colorM As Color)
	TraverseTreeNodes(tvwT.Nodes)
	tvwT.SelectedNode.BackColor = colorM
End Sub

''' <summary>
''' �ݹ����treeview�����нڵ�
''' </summary>
''' <param name="nods"></param>
''' <remarks></remarks>
Private Sub TraverseTreeNodes(ByVal nods As System.Windows.Forms.TreeNodeCollection)
	For Each nod As TreeNode In nods
		nod.BackColor = Color.White
		TraverseTreeNodes(nod.Nodes)
	Next
End Sub