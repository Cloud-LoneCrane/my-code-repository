'���������Ǳ�����ӵ�����   
Imports   System.Reflection   
Imports   System.ComponentModel   

	  '����ʾ��:   
	  'Private   Sub   Button2_Click(ByVal   sender   As   System.Object,   ByVal   e   As   System.EventArgs)   Handles   Button2.Click   
	  '         Dim   i   As   Integer   
	  '         For   i   =   1   To   2   
	  '                 SetValueControlProperty(Me,   "Label"   &   i.ToString,   "Text",   GetValueControlProperty(Me,   "TextBox"   &   i.ToString,   "Text"))   
	  '         Next   i   
	  'End   Sub   

'***********************************************************   
'��������:���ݿؼ���   ����,������   ȡ�ؼ����Ե�ֵ.   
'��������:GetValueControlProperty   
'����:ClassInstance   ���÷���������(һ���Ǵ���,��MEȡ��)   
'           ControlName:�ؼ���   
'           PropertyName:�ؼ���������   
'����ֵ:Object,�ÿؼ������Ե�ֵ.   
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
'��������:���ݿؼ�������������ֵ   
'��������:SetValueControlProperty   
'����:ClassInstance   ���÷���������(һ���Ǵ���,��MEȡ��)   
'           ControlName:�ؼ���   
'           PropertyName:�ؼ���������   
'           Value:���õ��ÿؼ����Ե�ֵ.   
'����ֵ:Object   �ɹ�:����һ������Ŀؼ�,ʧ��:NOTHING   
'***********************************************************   
Public   Function   SetValueControlProperty(ByVal   ClassInstance   As   Object,   ByVal   ControlName   As   String,   ByVal   PropertyName   As   String,   ByVal   Value   As   Object)   As   Object   
	  Dim   Result   As   Object   
	  Dim   myType   As   Type   =   ClassInstance.GetType   
	  Dim   myFieldInfo   As   FieldInfo   =   myType.GetField("_"   &   ControlName,   BindingFlags.NonPublic   _   
						  Or   BindingFlags.Instance   Or   BindingFlags.Public   Or   BindingFlags.Instance)     '��"_"�������Ҫ����   
	  If   Not   myFieldInfo   Is   Nothing   Then   
			  Dim   properties   As   PropertyDescriptorCollection   =   TypeDescriptor.GetProperties(myType)   
			  Dim   myProperty   As   PropertyDescriptor   =   properties.Find(PropertyName,   False)     '������ΪTrue�Ͳ������ִ�Сд��   
			  If   Not   myProperty   Is   Nothing   Then   
					  Dim   ctr   As   Object   
					  ctr   =   myFieldInfo.GetValue(ClassInstance)   'ȡ�ÿؼ�ʵ��   
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