''' <summary>
''' ���ݸ��ڵ��״̬�ı����ǵĸ�ѡ��״̬
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
''' ������еı���ڵ㣬������нڵ㶼ѡ�У��򸸽ڵ�Ҳѡ�У�������нڵ��ѡ�У��򸸽ڵ�Ҳ��ѡ��
''' </summary>
''' <param name="treeNode"></param>
''' <param name="nodeChecked"></param>
''' <remarks></remarks>
Private Sub CheckAllFatherNodes(ByVal treeNode As TreeNode, ByVal nodeChecked As Boolean)
	Dim node As TreeNode
	Dim isSame As Boolean = True '����nodeChecked��״̬

	If Not treeNode.Parent Is Nothing Then
		For Each node In treeNode.Parent.Nodes
			'������nodeChecked״̬��һ���Ľڵ�
			If node.Checked <> nodeChecked Then
				isSame = False
				Exit For
			End If
		Next
		If isSame = False Then
			'�����нڵ�����һ����һ���Ľڵ�ʱ���϶�����һ����ѡ�еĽڵ㣬���Ը��ڵ�Ϊѡ��
			treeNode.Parent.Checked = True
		Else
			'�����нڵ��״̬��һ��ʱ�����ڵ�״̬���ڽڵ�״̬
			treeNode.Parent.Checked = nodeChecked
		End If
		CheckAllFatherNodes(treeNode.Parent, treeNode.Parent.Checked)
	End If
	node = Nothing
End Sub

''' <summary>
''' Ȼ����TreeView �ؼ��� AfterCheck �¼���������´���
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
'ע�⣬��Ϊÿ���ڵ㸴ѡ���״̬�ı�󶼻����� AfterCheck �¼�������Ϊ�˱������޴εؽ���ݹ��У�����Ҫ�ж�TreeViewAction��״̬�����б�����ʹ�ô����Զ�ѡ�еĽڵ��TreeViewAction ״̬Ϊ Unknown����Ϊ���Ա������ǵ��õݹ���̵�ʱ������⡣
