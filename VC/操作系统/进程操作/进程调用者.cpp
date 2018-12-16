//===============================================================
//功能:
//	获得进程的调用者
//参数:
//	long 
//		进程pid
//返回值:
//	CString 
//		用户名
//===============================================================
CString GetProcessAuther(long pid) 
{ 
	//获得运行进程的用户身份，此处对于8以上的进程没问题，对于8，0进程无法列出(8是Win2000下的，WinXP下为4) 
	SID_NAME_USE   peUse; 
	HANDLE   hp; 
	HANDLE   hToken; 
	int   isok; 
	char   buf[0x400]; 
	char   buf1[100]; 
	char   buf2[100]; 
	DWORD   dwNumBytesRet; 
	DWORD   dwNumBytesRet1; 
	CString strAuther;

	hp=OpenProcess(0x400, 0, pid);//0x400 is PROCESS_QUERY_INFORMATION 
	isok=OpenProcessToken(hp, 0x20008, &hToken);//这个0x20008不知道什么，TOKEN_QUERY？ 
	if(isok) 
	{ 
		isok=GetTokenInformation(hToken, TokenUser, &buf, 0x400, &dwNumBytesRet); 
		if(isok) 
		{ 
			dwNumBytesRet=100; 
			dwNumBytesRet1=100; 
			isok=LookupAccountSid(NULL,(DWORD*)(*(DWORD*)buf),buf1,&dwNumBytesRet,buf2,
				&dwNumBytesRet1,&peUse); 
			if(isok) 
			{ 
				strAuther.Format(_T("%s"),buf1); 
			} 
			
			CloseHandle(hToken); 
		} 
	} 
	
	CloseHandle(hp); 
	return strAuther;
}
