//===============================================================
//功能:
//	执行CMD命令
//参数:
//	LPCTSTR
//		命令
//	char* 
//		缓冲区接收命令的回显
//	int 
//		接收缓冲区的大小
//返回值:
//	BOOL 
//	 TRUE 成功，FALSE 失败
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
	char command[1024]={0};    //长达1K的命令行，够用了吧 
	strcpy(command,"Cmd.exe /C "); //必须加/c
	strcat(command,cmdline);
	STARTUPINFO si; 
	PROCESS_INFORMATION pi; 
	si.cb = sizeof(STARTUPINFO); 
	GetStartupInfo(&si); 
	si.hStdError = hWrite;            //把创建进程的标准错误输出重定向到管道输入 
	si.hStdOutput = hWrite;           //把创建进程的标准输出重定向到管道输入 
	si.wShowWindow = SW_HIDE; 
	si.dwFlags = STARTF_USESHOWWINDOW | STARTF_USESTDHANDLES; 
	//关键步骤，CreateProcess函数参数意义请查阅MSDN 
	if (!CreateProcess(NULL, command,NULL,NULL,TRUE,NULL,NULL,NULL,&si,&pi)) 
	{ 
		CloseHandle(hWrite); 
		CloseHandle(hRead); 
		return FALSE; 
	} 
	CloseHandle(hWrite);
	char buffer[4096] = {0};          //用4K的空间来存储输出的内容，只要不是显示文件内容，一般情况下是够用了。
	DWORD bytesRead; 
	while (true) 
	{ 
		if (ReadFile(hRead,buffer,4095,&bytesRead,NULL) == NULL) 
			break; 

		strRead += buffer;
		//buffer中就是执行的结果，可以保存到文本，也可以直接输出 
	} 
	CloseHandle(hRead); 

	if (strRead.GetLength() > 0 && maxsize > 0)
	{
		strncpy(readBuf,strRead.GetBuffer(0),maxsize);
	}
	return TRUE;
}
