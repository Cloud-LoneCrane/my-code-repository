//===============================================================
//����:
//	��ý��̵ĵ�����
//����:
//	long 
//		����pid
//����ֵ:
//	CString 
//		�û���
//===============================================================
CString GetProcessAuther(long pid) 
{ 
	//������н��̵��û���ݣ��˴�����8���ϵĽ���û���⣬����8��0�����޷��г�(8��Win2000�µģ�WinXP��Ϊ4) 
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
	isok=OpenProcessToken(hp, 0x20008, &hToken);//���0x20008��֪��ʲô��TOKEN_QUERY�� 
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
