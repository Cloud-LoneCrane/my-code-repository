//===============================================================
//����:
//	ִ��CMD����
//����:
//	LPCTSTR
//		����
//	char* 
//		��������������Ļ���
//	int 
//		���ջ������Ĵ�С
//����ֵ:
//	BOOL 
//	 TRUE �ɹ���FALSE ʧ��
//===============================================================
BOOL RunCmd(LPCTSTR cmdline, char* readBuf,int maxsize)
{
	SECURITY_ATTRIBUTES sa; 
	HANDLE hRead,hWrite;
	CString strRead;

	sa.nLength = sizeof(SECURITY_ATTRIBUTES); 
	sa.lpSecurityDescriptor = NULL; 
	sa.bInheritHandle = TRUE; 
	if (!CreatePipe(&hRead,&hWrite,&sa,0)) 
	{ 
		return FALSE; 
	} 
	char command[1024]={0};    //����1K�������У������˰� 
	strcpy(command,"Cmd.exe /C "); //�����/c
	strcat(command,cmdline);
	STARTUPINFO si; 
	PROCESS_INFORMATION pi; 
	si.cb = sizeof(STARTUPINFO); 
	GetStartupInfo(&si); 
	si.hStdError = hWrite;            //�Ѵ������̵ı�׼��������ض��򵽹ܵ����� 
	si.hStdOutput = hWrite;           //�Ѵ������̵ı�׼����ض��򵽹ܵ����� 
	si.wShowWindow = SW_HIDE; 
	si.dwFlags = STARTF_USESHOWWINDOW | STARTF_USESTDHANDLES; 
	//�ؼ����裬CreateProcess�����������������MSDN 
	if (!CreateProcess(NULL, command,NULL,NULL,TRUE,NULL,NULL,NULL,&si,&pi)) 
	{ 
		CloseHandle(hWrite); 
		CloseHandle(hRead); 
		return FALSE; 
	} 
	CloseHandle(hWrite);
	char buffer[4096] = {0};          //��4K�Ŀռ����洢��������ݣ�ֻҪ������ʾ�ļ����ݣ�һ��������ǹ����ˡ�
	DWORD bytesRead; 
	while (true) 
	{ 
		if (ReadFile(hRead,buffer,4095,&bytesRead,NULL) == NULL) 
			break; 

		strRead += buffer;
		//buffer�о���ִ�еĽ�������Ա��浽�ı���Ҳ����ֱ����� 
	} 
	CloseHandle(hRead); 

	if (strRead.GetLength() > 0 && maxsize > 0)
	{
		strncpy(readBuf,strRead.GetBuffer(0),maxsize);
	}
	return TRUE;
}
