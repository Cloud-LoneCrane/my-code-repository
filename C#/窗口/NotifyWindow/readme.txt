http://www.codeproject.com/KB/miscctrl/RobMisNotifyWindow.aspx
ʹ��˵��: 
	1 ��NotifyWindow class��ӵ���ǰ���̵������ռ�;
	2 
	//---------------------Declares and Calls--------------------------------------------
		NotifyWindow nw = new NotifyWindow("����","����");
		nw.SetDimensions(150, 150);
		
		nw.TitleClicked += new System.EventHandler (titleClick);
		nw.TextClicked += new System.EventHandler (TextClick);
		nw.Notify();
                   

	//----------------------------Event Handlers-------------------------------------
		protected void titleClick (object sender, System.EventArgs e)
		{
			MessageBox.Show ("Title text clicked");
		}

		protected void textClick (object sender, System.EventArgs e)
		{
			MessageBox.Show ("Text clicked");
		}