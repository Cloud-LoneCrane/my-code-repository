//////////////////////////////////////////
//	ʹ�ö�ʱ��
//////////////////////////////////////////

//----------------------��MFC ��--------------------------
//һ �����Ϣ WM_TIMER 
// Class Wizard Ϊ�Զ���� OnTimer(UINT nIDEvent) 
//�� ��3�������ǻص�����
SetTimer(1,1000,NULL);//������ʱ��

//�� �� OnTimer(UINT nIDEvent) ����Ӵ������
// nIDEvent Ϊ��ʱ���ı��ID

//��   KillTimer(m_nTimer);   �رն�ʱ��


/* *************************************************************
 *  UINT SetTimer( UINT nIDEvent, UINT nElapse, 
 *     void (CALLBACK EXPORT* lpfnTimer)(HWND, UINT, UINT, DWORD) );
 ***************************************************************/

//------------------------����MFC ��-------------------------
	//=========== ��ʼѭ�� =======================
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
// ���ܣ���ʱ��������
//==================================================       
VOID CALLBACK TimerProc(HWND hWnd,UINT nMsg,UINT nIDEvent,DWORD dwTime)
{
    //....................
}