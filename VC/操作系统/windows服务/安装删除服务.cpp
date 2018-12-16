//------------------ 安装、卸载服务 -----------------------
// 调用说明：
//	-install[remove] ServiceName DisplayName FilePath
// eg.
//	-install WinM WindowsMedia C:\console.exe
//	-remove WinM
//==========================================================
#include <stdio.h>
#include <windows.h>

#define LOGFILE	".\\log.txt" //日志文件名

// SERVICE_STATUS m_ServiceStatus; //服务状态
// SERVICE_STATUS_HANDLE m_ServiceStatusHandle; //服务状态值的句柄

//安装服务函数
BOOL InstallService(char* szFileName,char* szServceName,char* szDisplayName);
BOOL DeleteService(char* szServceName);     //删除服务的函数
int WriteToLog(char* str);

int main(int argc, char* argv[])
{
	printf("\twindows based service demo\n");

    if(argc!=5 && argc != 3)
	{
		printf("usage: %s -install[remove] ServiceName DisplayName FilePath\n",argv[0]);
		return 0;
	}
		if(strcmp(argv[1],"-install")==0) // install
		{
			if(InstallService(argv[2],argv[3],argv[4])) //安装服务
				printf("\n\nService Installed Sucessfully\n");
			else
				printf("\n\nError Installing Service\n");
		}
	else if(strcmp(argv[1],"-remove")==0)    // remove
		{
			if(DeleteService(argv[2]))	//卸载服务
				printf("\n\nService remove sucessfully\n");
			else
				printf("\n\nError removing Service\n");
		} 
		else
		{
			printf("\nusage: %s -install[remove]\n",argv[0]);
			return 0;
		}
return 1;
}

//安装服务函数
BOOL InstallService(char* szServceName,char* szDisplayName,char* szFileName)   
{
	SC_HANDLE schSCManager,schService;

	//创建一个到服务控制管理器的连接
	schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS); 
	if (schSCManager == NULL) 
	{
		printf("open scmanger failed,maybe you do not have the privilage to do this\n");
		return false;
	}

	LPCTSTR lpszBinaryPathName=szFileName;
	//将服务的信息添加到SCM的数据库
	schService = CreateService(
		schSCManager,
		szServceName,  //服务的名字
		szDisplayName, //显示的名字
		SERVICE_ALL_ACCESS, // desired access 
		SERVICE_WIN32_OWN_PROCESS, // service type 
		SERVICE_AUTO_START, // start type 
		SERVICE_ERROR_NORMAL, // error control type 
		lpszBinaryPathName, // service's binary 
		NULL, // no load ordering group 
		NULL, // no tag identifier 
		NULL, // no dependencies 
		NULL, // LocalSystem account 
		NULL); // no password 

	if (schService == NULL) 
	{//invoke 调用
		printf("faint,we failed just because we invoke createservices failed\n");
		return false; 
	}
	CloseServiceHandle(schService); 
	return true;
}

//卸载服务
BOOL DeleteService(char* szServceName) 
{
	SC_HANDLE schSCManager;
	SC_HANDLE hService;
	schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);
 
	if (schSCManager == NULL) 
	{
		printf("faint,open scmanger failed\n");
		return false; 
	}
	hService=OpenService(schSCManager,szServceName,SERVICE_ALL_ACCESS);
	if (hService == NULL) 
	{
		printf("faint,open services failt\n");
		return false;
	}
  
	//删除服务
	if(DeleteService(hService)==0)
	{	
		printf("Delete Service failed.\n");
		return false;
    }
	if(CloseServiceHandle(hService)==0)
	{	
		printf("Close Service failed.\n");
		return false;
    }
	else
		return true;
}

int WriteToLog(char* str)
{
	FILE* log;
	log = fopen(LOGFILE, "a+");
	if (log == NULL)
		return -1;
	fprintf(log, "%s\n", str);
	fclose(log);
	return 0;
	
}
