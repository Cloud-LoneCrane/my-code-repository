'''   -----------------------------------------------------------------------------   
'''   <summary>   
'''   依据菜单名称返回菜单对象   
'''   </summary>   
'''   <param   name="FormIstance">窗体实例</param>   
'''   <param   name="MenuItemName">菜单名称</param>   
'''   <returns>菜单</returns>   
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
'''   给出菜单对象返回菜单名称   
'''   </summary>   
'''   <param   name="FormIstance">窗体实例</param>   
'''   <param   name="MenuItem">菜单</param>   
'''   <returns>菜单名称</returns>   
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