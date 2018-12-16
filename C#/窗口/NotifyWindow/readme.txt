http://www.codeproject.com/KB/miscctrl/RobMisNotifyWindow.aspx
使用说明: 
	1 将NotifyWindow class添加到当前工程的命名空间;
	2 
	//---------------------Declares and Calls--------------------------------------------
		NotifyWindow nw = new NotifyWindow("标题","内容");
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