''' <summary>
''' ���ƴ���ͷ��ֱ��
''' </summary>
Private Sub DrawArrowLine()
	Dim g As Graphics = grpVipLevelSet.CreateGraphics()
	Dim pen As Pen = New Pen(Color.Black, 1)
	g.DrawLine(pen, 20, 40, 20, 230)
	g.DrawLine(pen, 15, 215, 20, 230)
	g.DrawLine(pen, 25, 215, 20, 230)
End Sub