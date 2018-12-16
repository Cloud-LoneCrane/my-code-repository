
''' <summary>
''' ����ɫ����б��
''' </summary>
''' <remarks></remarks>
Private Sub FillListBoxWithColors(ByVal ComboBox1 As ComboBox)
	Me.ComboBox1.DrawMode = DrawMode.OwnerDrawFixed
	Me.ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
	Me.ComboBox1.ItemHeight = 15
	'������˸beginupdate
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

	Dim rect As Rectangle = e.Bounds 'ÿһ��ı߿�

	'�����������ѡ������ʾ������ʾ����,�����ð�ɫ
	If e.State And DrawItemState.Selected Then
		e.Graphics.FillRectangle(SystemBrushes.Highlight, rect)
	Else
		e.Graphics.FillRectangle(SystemBrushes.Window, rect)
	End If
	Dim colorname As String = ComboBox1.Items(e.Index)
	Dim b As New SolidBrush(Color.FromName(colorname))
	'��Сѡ��������()
	rect.Inflate(-1, -2)
	rect.Width = 20
	'�����ɫ(���ֶ�Ӧ����ɫ)
	e.Graphics.FillRectangle(b, rect)
	'���Ʊ߿�()
	e.Graphics.DrawRectangle(Pens.Black, rect)
	Dim b2 As Brush
	'ȷ����ʾ�����ֵ���ɫ()
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