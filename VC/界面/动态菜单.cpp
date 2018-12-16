//动态创建菜单	
void CreateMenu()
{	
	CMenu Menu,PopupMenu;
	
	//创建一个顶级菜单
	Menu.CreateMenu();   
		//创建弹出菜单
		PopupMenu.CreatePopupMenu();   
		PopupMenu.AppendMenu(MF_STRING,150,"&New");   
		PopupMenu.AppendMenu(MF_STRING,151,"&Open");   
		PopupMenu.AppendMenu(MF_STRING,152,"&Close");   
		PopupMenu.AppendMenu(MF_STRING,153,"E&xit"); 
	
	//把弹出菜单和上级菜单关联起来
	Menu.AppendMenu(MF_POPUP,(UINT)PopupMenu.m_hMenu, "&File");   //关键！   
	
	//为顶级菜单添加新项
	Menu.AppendMenu(MF_STRING,   154,   "&Edit");   

	SetMenu(&Menu);   //为窗口设置菜单CDialog
	Menu.Detach();  //分离一个菜单句柄
	PopupMenu.Detach();  

}