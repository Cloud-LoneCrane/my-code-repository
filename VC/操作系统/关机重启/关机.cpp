#define NTSTATUS         LONG

typedef enum _SHUTDOWN_ACTION {
		ShutdownNoReboot,
		ShutdownReboot,
		ShutdownPowerOff
}SHUTDOWN_ACTION;

typedef NTSTATUS (* pNtShutdownSystem)(IN SHUTDOWN_ACTION  Action);
pNtShutdownSystem NtShutdownSystem=NULL;

/** NtShutdownSystem �ػ�ǰ��֪ͨ����Ӧ�ó��� */
BOOL NtShutdownSystem_Init()
{
	HINSTANCE hDll; //����һ��ntdll.dllʵ���ļ���� 
	hDll = LoadLibrary("ntdll.dll");//����ntdll.dll��̬���ӿ�
	NtShutdownSystem=NULL;
	NtShutdownSystem = (pNtShutdownSystem)GetProcAddress(hDll,_T("NtShutdownSystem")); 
	FreeLibrary(hDll);	
}

/** �رջ��� */
BOOL FinalShutComputer(int shutstyle)
{
	OSVERSIONINFO	ovi = { 0 };
	
	ovi.dwOSVersionInfoSize = sizeof(OSVERSIONINFO);
	GetVersionEx(&ovi);
	
	BOOL bFlag=FALSE;
	switch(shutstyle)
	{
	case SHUT_POWEROFF:
		if(ovi.dwMajorVersion < 5 ||
			(ovi.dwMajorVersion == 5 &&
			ovi.dwMinorVersion < 1))
			bFlag=ExitWindowsEx(EWX_POWEROFF|EWX_FORCE, 0);
		else
			bFlag=InitiateSystemShutdown(0, 0, 0, TRUE, FALSE);
		break;
	case SHUT_LOGOFF:
		bFlag=ExitWindowsEx(EWX_LOGOFF|0x04, 0);
		break;
	case SHUT_UNSETUP:
		//do nothing, leave the thread
		break;
	case SHUT_REBOOT:
	default:
		if(ovi.dwMajorVersion < 5 ||
			(ovi.dwMajorVersion == 5 &&
			ovi.dwMinorVersion < 1))
			bFlag=ExitWindowsEx(EWX_REBOOT|EWX_FORCE, 0);
		else
			bFlag=InitiateSystemShutdown(0, 0, 0, TRUE, TRUE);
		break;
	}
	
	return bFlag;
}

/** �ػ� */
void CCloseCompDlg::OnBtnShutdown() 
{
	NtShutdownSystem_Init();
	BOOL bFlag =FinalShutComputer(SHUT_REBOOT);
	
	bFlag =FinalShutComputer(SHUT_REBOOT);
	
	if (bFlag == FALSE)
	{
		if (NtShutdownSystem == NULL)
		{
			OnBtnNtshutdown();
		}
		
		NtShutdownSystem(ShutdownReboot);
	}
}

//===============================================================
//����: ��ùػ�Ȩ��
//===============================================================
BOOL GetShutdownPrivilege()
{
	HANDLE hToken; //ָ���� 
	TOKEN_PRIVILEGES tkp; 
	
	// Get a token for this process.  hu1
	if (!OpenProcessToken(GetCurrentProcess(), 
		TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, &hToken)) 
	{
		TRACE("OpenProcessToken"); 
		return FALSE;
	}
	// Get the LUID for the shutdown privilege. 
	LookupPrivilegeValue(NULL, SE_SHUTDOWN_NAME, 
		&tkp.Privileges[0].Luid); 
	
	tkp.PrivilegeCount = 1;  // one privilege to set    
	tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED; 
	
	// Get the shutdown privilege for this process. 
	//��ùػ�Ȩ��
	AdjustTokenPrivileges(hToken, FALSE, &tkp, 0, 
		(PTOKEN_PRIVILEGES)NULL, 0); 
	
	// Cannot test the return value of AdjustTokenPrivileges. 
	if (GetLastError() != ERROR_SUCCESS) 
	{
		TRACE("AdjustTokenPrivileges"); 
		return FALSE;
	}
	
	return TRUE;
}
