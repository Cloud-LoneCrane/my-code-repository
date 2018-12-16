'���ͨ���ؼ���ʵ�����Ʒ���һ���ؼ���
'2008��12��25�� ������ ���� 05:49
'���ͨ���ؼ���ʵ�����Ʒ���һ���ؼ��� 
'������ʹ�� Reflection��ͨ���ؼ�����������������ʵ����������һЩʾ�����룺

//C# 

private void Form1_Load(object sender, System.EventArgs e)
{
    ComboBox c = (ComboBox)this.ControlFromName("combobox1");
    c.Items.Add("1");
    this.GetControls();
}

private Control ControlFromName(string name)
{
    object o = this.GetType().GetField(name,
      System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
      System.Reflection.BindingFlags.IgnoreCase).GetValue(this);

    return((Control)o);
}

private void GetControls()
{
    System.Reflection.FieldInfo[] fis = this.GetType().GetFields
    (
        System.Reflection.BindingFlags.NonPublic | 
        System.Reflection.BindingFlags.Instance |
        System.Reflection.BindingFlags.IgnoreCase
    );

    foreach(System.Reflection.FieldInfo fi in fis)
    {
        if (fi.GetValue(this) is Control)
            MessageBox.Show(fi.Name);
    }
}
'VB .NET

Private Function ControlFromName(ByVal name As String) As Control
    Dim o As ObjectDim o As Object
    o = Me.GetType().GetField(name, Reflection.BindingFlags.NonPublic Or _
      Reflection.BindingFlags.Instance Or _
      Reflection.BindingFlags.IgnoreCase).GetValue(Me)
   
    Return (CType(o, Control))
End Function

Private Sub Form1_Load(ByVal sender As System.Object, _
  ByVal e As System.EventArgs) Handles MyBase.Load
    Dim c As ComboBox
    c = CType(ControlFromName("_combobox1"), ComboBox)
    c.Items.Add("1")
    Me.GetControls()e.GetControls()
End Sub

Private Sub GetControls()
    Dim fis As System.Reflection.FieldInfo()

    fis = Me.GetType().GetFields(Reflection.BindingFlags.NonPublic Or _
      Reflection.BindingFlags.Instance Or _
      Reflection.BindingFlags.IgnoreCase)

    For Each fi As Reflection.FieldInfo In fis
        If TypeOf (fi.GetValue(Me)) Is Control Then
            MessageBox.Show(fi.Name)
        End Ifnd If
    Next
End Sub
 
