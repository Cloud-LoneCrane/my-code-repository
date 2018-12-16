''' <summary>
''' 根据父节点的状态改变它们的复选框状态
''' </summary>
''' <param name="treeNode"></param>
''' <param name="nodeChecked"></param>
''' <remarks></remarks>
Private Sub CheckAllChildNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
	Dim node As TreeNode
	For Each node In treeNode.Nodes
		node.Checked = nodeChecked
		If node.Nodes.Count > 0 Then
			Me.CheckAllChildNodes(node, nodeChecked)
		End If
	Next node
End Sub

''' <summary>
''' 检查所有的本层节点，如果所有节点都选中，则父节点也选中，如果所有节点非选中，则父节点也不选中
''' </summary>
''' <param name="treeNode"></param>
''' <param name="nodeChecked"></param>
''' <remarks></remarks>
Private Sub CheckAllFatherNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
	Dim node As TreeNode
	Dim isSame As Boolean = True '记下nodeChecked的状态

	If Not treeNode.Parent Is Nothing Then
		For Each node In treeNode.Parent.Nodes
			'碰到与nodeChecked状态不一样的节点
			If node.Checked <> nodeChecked Then
				isSame = False
				Exit For
			End If
		Next
		If isSame = False Then
			'当所有节点中有一个不一样的节点时，肯定存在一个是选中的节点，所以父节点为选中
			treeNode.Parent.Checked = True
		Else
			'当所有节点的状态都一样时，父节点状态等于节点状态
			treeNode.Parent.Checked = nodeChecked
		End If
		CheckAllFatherNodes(treeNode.Parent, treeNode.Parent.Checked)
	End If
	node = Nothing
End Sub

''' <summary>
''' 然后在TreeView 控件的 AfterCheck 事件中添加如下代码
''' </summary>
''' <param name="sender"></param>
''' <param name="e"></param>
''' <remarks></remarks>
Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck
	If e.Action <> TreeViewAction.Unknown Then
		If e.Node.Nodes.Count > 0 Then
			Me.CheckAllChildNodes(e.Node, e.Node.Checked)
		End If
		CheckAllFatherNodes(e.Node, e.Node.Checked)
		Debug.WriteLine(e.Node.Text)
	End If
End Sub
'注意，因为每个节点复选框的状态改变后都会引发 AfterCheck 事件，我们为了避免无限次地进入递归中，就需要判断TreeViewAction的状态，所有被我们使用代码自动选中的节点的TreeViewAction 状态为 Unknown，因为可以避免我们调用递归过程的时候出问题。
