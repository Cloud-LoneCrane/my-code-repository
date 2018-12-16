Dim MyType As System.Type

MyType = Type.GetType(Me.ProductName & ".frmRateTime")
Dim FM As Object = Activator.CreateInstance(MyType)
FM.Show()
