
''' <summary>
''' 用颜色添充列表框
''' </summary>
''' <remarks></remarks>
Private Sub FillListBoxWithColors(ByVal ComboBox1 As ComboBox)
	Me.ComboBox1.DrawMode = DrawMode.OwnerDrawFixed
	Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
	Me.ComboBox1.ItemHeight = 15
	'避免闪烁beginupdate
	Me.ComboBox1.BeginUpdate()
	ComboBox1.Items.Clear()
	Dim pi As Reflection.PropertyInfo
	For Each pi In GetType(Color).GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
		Me.ComboBox1.Items.Add(pi.Name)
	Next
	ComboBox1.EndUpdate()
End Sub
Private Sub ComboBox1_DrawItem(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawItemEventArgs) Handles ComboBox1.DrawItem

	If e.Index < 0 Then Exit Sub

	Dim rect As Rectangle = e.Bounds '每一项的边框

	'绘制项如果被选中则显示高亮显示背景,否则用白色
	If e.State And DrawItemState.Selected Then
		e.Graphics.FillRectangle(SystemBrushes.Highlight, rect)
	Else
		e.Graphics.FillRectangle(SystemBrushes.Window, rect)
	End If
	Dim colorname As String = ComboBox1.Items(e.Index)
	Dim b As New SolidBrush(Color.FromName(colorname))
	'缩小选定项区域()
	rect.Inflate(-1, -2)
	rect.Width = 20
	'填充颜色(文字对应的颜色)
	e.Graphics.FillRectangle(b, rect)
	'绘制边框()
	e.Graphics.DrawRectangle(Pens.Black, rect)
	Dim b2 As Brush
	'确定显示的文字的颜色()
	If CInt(b.Color.R) + CInt(b.Color.G) + CInt(b.Color.B) > 128 * 3 Then
		b2 = Brushes.Black
	Else
		b2 = Brushes.White
	End If
	e.Graphics.DrawString(colorname, Me.ComboBox1.Font, b2, rect.Right + 2, rect.Y)
End Sub

Private Sub frmSetHosts_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	FillListBoxWithColors(ComboBox1)
End Sub