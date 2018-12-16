//////////////////////////////////////////
//	使用定时器
//////////////////////////////////////////

//----------------------　MFC ：--------------------------
//一 添加消息 WM_TIMER 
// Class Wizard 为自动添加 OnTimer(UINT nIDEvent) 
//二 第3个参数是回调函数
SetTimer(1,1000,NULL);//创建定时器

//三 在 OnTimer(UINT nIDEvent) 中添加处理代码
// nIDEvent 为定时器的编号ID

//四   KillTimer(m_nTimer);   关闭定时器


/* *************************************************************
 *  UINT SetTimer( UINT nIDEvent, UINT nElapse, 
 *     void (CALLBACK EXPORT* lpfnTimer)(HWND, UINT, UINT, DWORD) );
 ***************************************************************/

//------------------------　非MFC ：-------------------------
	//=========== 开始循环 =======================
		MSG Msg;
		UINT TimerId = SetTimer(NULL,0,1000,&TimerProc);
		
		if (!TimerId)
		{
			printf("SetTimer failed.\n");
			return 0;
		}
		
		while (GetMessage(&Msg,NULL,0,0))
		{
			//if (Msg.message == WM_TIMER)
	
				DispatchMessage(&Msg);
	 	}
		 KillTimer(NULL, TimerId);

//==================================================
// 功能：定时器处理函数
//==================================================       
VOID CALLBACK TimerProc(HWND hWnd,UINT nMsg,UINT nIDEvent,DWORD dwTime)
{
    //....................
}