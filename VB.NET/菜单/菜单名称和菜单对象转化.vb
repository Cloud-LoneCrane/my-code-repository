'''   -----------------------------------------------------------------------------   
'''   <summary>   
'''   ���ݲ˵����Ʒ��ز˵�����   
'''   </summary>   
'''   <param   name="FormIstance">����ʵ��</param>   
'''   <param   name="MenuItemName">�˵�����</param>   
'''   <returns>�˵�</returns>   
'''   <remarks>   
'''   </remarks>   
'''   <history>   
'''   [lzmtw] 2005-10-21 Created   
'''   </history>   
'''   -----------------------------------------------------------------------------   
Private   Function   GetMenuItemByName(ByVal   FormIstance   As   Form,   ByVal   MenuItemName   As   String)   As   MenuItem   
	  Dim   FindBinding   As   System.Reflection.BindingFlags   
	  FindBinding   =   BindingFlags.Instance   Or   BindingFlags.NonPublic   Or   BindingFlags.Public   
	  Dim   MenuItemFieldInfo   As   System.Reflection.FieldInfo   
	  MenuItemFieldInfo   =   FormIstance.GetType.GetField("_"   &   MenuItemName,   FindBinding)   
	  If   MenuItemFieldInfo   Is   Nothing   Then   
			  Return   Nothing   
	  Else   
			  Return   CType(MenuItemFieldInfo.GetValue(FormIstance),   MenuItem)   
	  End   If   
End   Function   

'''   -----------------------------------------------------------------------------   
'''   <summary>   
'''   �����˵����󷵻ز˵�����   
'''   </summary>   
'''   <param   name="FormIstance">����ʵ��</param>   
'''   <param   name="MenuItem">�˵�</param>   
'''   <returns>�˵�����</returns>   
'''   <remarks>   
'''   </remarks>   
'''   <history>   
'''   [lzmtw] 2005-10-21 Created   
'''   </history>   
'''   -----------------------------------------------------------------------------   
Private   Function   GetMenuItemName(ByVal   FormIstance   As   Form,   ByVal   MenuItem   As   MenuItem)   As   String   
	  Dim   FindBinding   As   System.Reflection.BindingFlags   
	  FindBinding   =   BindingFlags.Instance   Or   BindingFlags.NonPublic   Or   BindingFlags.Public   
	  Dim   MenuItemFieldInfo   As   System.Reflection.FieldInfo   
	  For   Each   MenuItemFieldInfo   In   FormIstance.GetType.GetFields(FindBinding)   
			  If   MenuItemFieldInfo.GetValue(FormIstance)   Is   MenuItem   Then   
					  Return   MenuItemFieldInfo.Name.Substring(1)   
			  End   If   
	  Next   
	  Return   String.Empty   
End   Function