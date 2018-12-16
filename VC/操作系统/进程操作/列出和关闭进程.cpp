////////////////////////////////
//	#include <Tlhelp32.h>
///////////////////////////////

//============================================
// 功能：列出进程
//============================================
void pslist(void)
{
	HANDLE hProcessSnap = NULL;
	PROCESSENTRY32 pe32= {0};
	
	//创建一个进程快照(TH32CS_SNAPPROCESS 进程的快照)
	hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
	if (hProcessSnap == (HANDLE)-1)
	{
		printf("\nCreateToolhelp32Snapshot() failed:%d",GetLastError());
		return ;
	}
	pe32.dwSize = sizeof(PROCESSENTRY32);
	printf("\nProcessName　　　　 ProcessID");
	
	//获得第一个进程
	if (Process32First(hProcessSnap, &pe32))
	{
		char a[5];
		
		//遍历所有进程
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
	
	//关闭一个进程的句柄
	CloseHandle (hProcessSnap);
	return;
}

//============================================
//功能:	提升权限
//参数：
//	HANDLE hToken
//	LPCTSTR lpszPrivilege
//	BOOL bEnablePrivilege
//============================================
BOOL SetPrivilege(HANDLE hToken,LPCTSTR lpszPrivilege,
				  BOOL bEnablePrivilege)
{
	TOKEN_PRIVILEGES tp;
	LUID luid;
	
	//查找一个进程权限的名字
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
	
	//提升权限
	AdjustTokenPrivileges(
		hToken, 
		FALSE, 
		&tp, 
		sizeof(TOKEN_PRIVILEGES), 
		(PTOKEN_PRIVILEGES) NULL, 
		(PDWORD) NULL); 

	//获得错误信息
	if (GetLastError() != ERROR_SUCCESS) 
	{ 
		printf("AdjustTokenPrivileges failed: %u\n", GetLastError() ); 
		return FALSE; 
	} 
	return TRUE;
}


//============================================
// 功能：关闭进程
//============================================
BOOL killps(DWORD id)//杀进程函数
{
	HANDLE hProcess=NULL,hProcessToken=NULL;
	BOOL IsKilled=FALSE,bRet=FALSE;
	__try
	{
		//打开一个令牌
		if(!OpenProcessToken(GetCurrentProcess(),TOKEN_ALL_ACCESS,&hProcessToken))
		{
		printf("\nOpen Current Process Token failed:%d",GetLastError());
		__leave;
		}
		//printf("\nOpen Current Process Token ok!");
		//设置权限
		if(!SetPrivilege(hProcessToken,SE_DEBUG_NAME,TRUE))
		{
			__leave;
		}
		printf("\nSetPrivilege ok!");

		//打开进程，获得对这个进程的所有权限
		if((hProcess=OpenProcess(PROCESS_ALL_ACCESS,FALSE,id))==NULL)
		{
			printf("\nOpen Process %d failed:%d",id,GetLastError());
			__leave;
		}
		//printf("\nOpen Process %d ok!",id);
		//结束这个进程
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

