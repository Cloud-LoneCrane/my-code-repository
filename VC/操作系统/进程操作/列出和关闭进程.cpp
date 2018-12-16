////////////////////////////////
//	#include <Tlhelp32.h>
///////////////////////////////

//============================================
// ���ܣ��г�����
//============================================
void pslist(void)
{
	HANDLE hProcessSnap = NULL;
	PROCESSENTRY32 pe32= {0};
	
	//����һ�����̿���(TH32CS_SNAPPROCESS ���̵Ŀ���)
	hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (hProcessSnap == (HANDLE)-1)
	{
		printf("\nCreateToolhelp32Snapshot() failed:%d",GetLastError());
		return ;
	}
	pe32.dwSize = sizeof(PROCESSENTRY32);
	printf("\nProcessName�������� ProcessID");
	
	//��õ�һ������
	if (Process32First(hProcessSnap, &pe32))
	{
		char a[5];
		
		//�������н���
		do
		{
			itoa(pe32.th32ProcessID,a,10);
			printf("\n%-20s%-10d",pe32.szExeFile,pe32.th32ProcessID);
		}
		while (Process32Next(hProcessSnap, &pe32));
		
	}
	else
	{
		printf("\nProcess32Firstt() failed:%d",GetLastError());
	}
	
	//�ر�һ�����̵ľ��
	CloseHandle (hProcessSnap);
	return;
}

//============================================
//����:	����Ȩ��
//������
//	HANDLE hToken
//	LPCTSTR lpszPrivilege
//	BOOL bEnablePrivilege
//============================================
BOOL SetPrivilege(HANDLE hToken,LPCTSTR lpszPrivilege,
				  BOOL bEnablePrivilege)
{
	TOKEN_PRIVILEGES tp;
	LUID luid;
	
	//����һ������Ȩ�޵�����
	if(!LookupPrivilegeValue(NULL,lpszPrivilege,&luid))
	{
		printf("\nLookupPrivilegeValue error:%d", GetLastError() ); 
		return FALSE; 
	}

	tp.PrivilegeCount = 1;
	tp.Privileges[0].Luid = luid;
	if (bEnablePrivilege)
		tp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
	else
		tp.Privileges[0].Attributes = 0;
	
	//����Ȩ��
	AdjustTokenPrivileges(
		hToken, 
		FALSE, 
		&tp, 
		sizeof(TOKEN_PRIVILEGES), 
		(PTOKEN_PRIVILEGES) NULL, 
		(PDWORD) NULL); 

	//��ô�����Ϣ
	if (GetLastError() != ERROR_SUCCESS) 
	{ 
		printf("AdjustTokenPrivileges failed: %u\n", GetLastError() ); 
		return FALSE; 
	} 
	return TRUE;
}


//============================================
// ���ܣ��رս���
//============================================
BOOL killps(DWORD id)//ɱ���̺���
{
	HANDLE hProcess=NULL,hProcessToken=NULL;
	BOOL IsKilled=FALSE,bRet=FALSE;
	__try
	{
		//��һ������
		if(!OpenProcessToken(GetCurrentProcess(),TOKEN_ALL_ACCESS,&hProcessToken))
		{
		printf("\nOpen Current Process Token failed:%d",GetLastError());
		__leave;
		}
		//printf("\nOpen Current Process Token ok!");
		//����Ȩ��
		if(!SetPrivilege(hProcessToken,SE_DEBUG_NAME,TRUE))
		{
			__leave;
		}
		printf("\nSetPrivilege ok!");

		//�򿪽��̣���ö�������̵�����Ȩ��
		if((hProcess=OpenProcess(PROCESS_ALL_ACCESS,FALSE,id))==NULL)
		{
			printf("\nOpen Process %d failed:%d",id,GetLastError());
			__leave;
		}
		//printf("\nOpen Process %d ok!",id);
		//�����������
		if(!TerminateProcess(hProcess,1))
		{
			printf("\nTerminateProcess failed:%d",GetLastError());
			__leave;
		}
		IsKilled=TRUE;
	}
	__finally
	{
		if(hProcessToken!=NULL) CloseHandle(hProcessToken);
		if(hProcess!=NULL) CloseHandle(hProcess);
	}

	return(IsKilled);
}

