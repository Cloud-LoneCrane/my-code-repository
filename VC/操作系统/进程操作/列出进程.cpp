/** �г�ϵͳ�����õĽ��� */
void ListAllProcess()
{
	HANDLE hProc= ::CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS,NULL);
	if (hProc==INVALID_HANDLE_VALUE) printf("error");
	else printf("success");
	
	PROCESSENTRY32 ProcInfo;
	
	BOOL bFlag=Process32First(hProc,&ProcInfo);
	
	printf(_T("%s\r\n"),ProcInfo.szExeFile);
	while(Process32Next(hProc,&ProcInfo))
	{	
		printf(_T("%s\r\n"),ProcInfo.szExeFile);
	}

}


#include "Tlhelp32.h"
#pragma comment(lib, "Kernel32.lib")

#include <afxcoll.h>

/** �г�ϵͳ�����õĽ��� */
BOOL GetCurrentProcessArray(CStringArray &processNameArray)
{
	//CStringArray arProc;
	CString		strProcName;
	HANDLE hProc= CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS,NULL);
	if (hProc==INVALID_HANDLE_VALUE) printf("error");
	else printf("success");

	PROCESSENTRY32 ProcInfo;

	BOOL bFlag=Process32First(hProc,&ProcInfo);
	strProcName = ProcInfo.szExeFile;

	processNameArray.Add(strProcName);
	printf(_T("%s\r\n"),ProcInfo.szExeFile);
	while(Process32Next(hProc,&ProcInfo))
	{	
		printf(_T("%s\r\n"),ProcInfo.szExeFile);
		strProcName = ProcInfo.szExeFile;
		processNameArray.Add(strProcName);
	}

	return TRUE;
}
