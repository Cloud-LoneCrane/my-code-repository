'下面两个是必须添加的引用   
Imports   System.Reflection   
Imports   System.ComponentModel   

	  '调用示例:   
	  'Private   Sub   Button2_Click(ByVal   sender   As   System.Object,   ByVal   e   As   System.EventArgs)   Handles   Button2.Click   
	  '         Dim   i   As   Integer   
	  '         For   i   =   1   To   2   
	  '                 SetValueControlProperty(Me,   "Label"   &   i.ToString,   "Text",   GetValueControlProperty(Me,   "TextBox"   &   i.ToString,   "Text"))   
	  '         Next   i   
	  'End   Sub   

'***********************************************************   
'函数作用:根据控件的   名字,属性名   取控件属性的值.   
'函数名称:GetValueControlProperty   
'参数:ClassInstance   调用方法的类名(一般是窗体,用ME取代)   
'           ControlName:控件名   
'           PropertyName:控件的属性名   
'返回值:Object,该控件该属性的值.   
'***********************************************************   
Public   Function   GetValueControlProperty(ByVal   ClassInstance   As   Object,   ByVal   ControlName   As   String,   ByVal   PropertyName   As   String)   As   Object   
	  Dim   Result   As   Object   
	  Dim   myType   As   Type   =   ClassInstance.GetType   

	  Dim   myFieldInfo   As   FieldInfo   =   myType.GetField("_"   &   ControlName,   BindingFlags.NonPublic   Or   _   
																BindingFlags.Instance   Or   BindingFlags.Public   Or   BindingFlags.Instance)   
	  If   Not   myFieldInfo   Is   Nothing   Then   
			  Dim   properties   As   PropertyDescriptorCollection   =   TypeDescriptor.GetProperties(myType)   
			  Dim   myProperty   As   PropertyDescriptor   =   properties.Find(PropertyName,   False)   
			  If   Not   myProperty   Is   Nothing   Then   
					  Dim   ctr   As   Object   
					  ctr   =   myFieldInfo.GetValue(ClassInstance)   
					  Try   
							  Result   =   myProperty.GetValue(ctr)   
					  Catch   ex   As   Exception   
							  MsgBox(ex.Message)   
					  End   Try   
			  End   If   
	  End   If   
	  Return   Result   
End   Function   

'***********************************************************   
'函数作用:根据控件名和属性名赋值   
'函数名称:SetValueControlProperty   
'参数:ClassInstance   调用方法的类名(一般是窗体,用ME取代)   
'           ControlName:控件名   
'           PropertyName:控件的属性名   
'           Value:设置到该控件属性的值.   
'返回值:Object   成功:返回一个具体的控件,失败:NOTHING   
'***********************************************************   
Public   Function   SetValueControlProperty(ByVal   ClassInstance   As   Object,   ByVal   ControlName   As   String,   ByVal   PropertyName   As   String,   ByVal   Value   As   Object)   As   Object   
	  Dim   Result   As   Object   
	  Dim   myType   As   Type   =   ClassInstance.GetType   
	  Dim   myFieldInfo   As   FieldInfo   =   myType.GetField("_"   &   ControlName,   BindingFlags.NonPublic   _   
						  Or   BindingFlags.Instance   Or   BindingFlags.Public   Or   BindingFlags.Instance)     '加"_"这个是特要紧的   
	  If   Not   myFieldInfo   Is   Nothing   Then   
			  Dim   properties   As   PropertyDescriptorCollection   =   TypeDescriptor.GetProperties(myType)   
			  Dim   myProperty   As   PropertyDescriptor   =   properties.Find(PropertyName,   False)     '这里设为True就不用区分大小写了   
			  If   Not   myProperty   Is   Nothing   Then   
					  Dim   ctr   As   Object   
					  ctr   =   myFieldInfo.GetValue(ClassInstance)   '取得控件实例   
					  Try   
							  myProperty.SetValue(ctr,   Value)   
							  Result   =   ctr   
					  Catch   ex   As   Exception   
							  Result   =   Nothing   
							  'MsgBox(ex.Message)   
					  End   Try   
			  End   If   
	  End   If   
	  Return   Result   
End   Function