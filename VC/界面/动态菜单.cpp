//��̬�����˵�	
void CreateMenu()
{	
	CMenu Menu,PopupMenu;
	
	//����һ�������˵�
	Menu.CreateMenu();   
		//���������˵�
		PopupMenu.CreatePopupMenu();   
		PopupMenu.AppendMenu(MF_STRING,150,"&New");   
		PopupMenu.AppendMenu(MF_STRING,151,"&Open");   
		PopupMenu.AppendMenu(MF_STRING,152,"&Close");   
		PopupMenu.AppendMenu(MF_STRING,153,"E&xit"); 
	
	//�ѵ����˵����ϼ��˵���������
	Menu.AppendMenu(MF_POPUP,(UINT)PopupMenu.m_hMenu, "&File");   //�ؼ���   
	
	//Ϊ�����˵��������
	Menu.AppendMenu(MF_STRING,   154,   "&Edit");   

	SetMenu(&Menu);   //Ϊ�������ò˵�CDialog
	Menu.Detach();  //����һ���˵����
	PopupMenu.Detach();  

}