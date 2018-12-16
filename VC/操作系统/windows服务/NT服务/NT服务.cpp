//------------------ NT服务 -----------------------
// 调用说明：
//	-install[remove] ServiceName DisplayName FilePath
// eg.
//	-install WinM WindowsMedia C:\console.exe
//	-remove WinM
//	-start WinM
//	-stop WinM
//	-query WinM
//==========================================================
#include <stdio.h>
#include <windows.h>

#define LOGFILE	".\\log.txt" //日志文件名

//安装服务函数
BOOL InstallService(char* szFileName,char* szServceName,char* szDisplayName);
BOOL DeleteService(char* szServceName);     //删除服务的函数
BOOL StartService(char* szServceName); //启动服务
BOOL StopService(char* szServceName); //停止服务 
BOOL QueryServiceStatus(char* szServiceName); //查询服务状态
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
		else if(strcmp(argv[1],"-start")==0)
		{
			if(StartService(argv[2]))
			{
				printf("\n\nService %s Start Successed.\n",argv[2]);
			}
			else	
			{
				printf("Service %s Start Failed.\n",argv[2]);
			}
		}
		else if (strcmp(argv[1],"-stop")==0)
		{
			if(StopService(argv[2])) //停止服务
				printf("\n\n Service %s Stop Successfully\n",argv[2]);
			else
				printf("\n\nError Stop Service %s \n",argv[2]);
		}
		else if (strcmp(argv[1],"-query")==0)
		{
			if(!QueryServiceStatus(argv[2])) //停止服务
				printf("\n\n Service %s  Error\n",argv[2]);
		
		}
		else
		{
			printf("\nusage: %s -install[remove]\n",argv[0]);
			return 0;
		}
return 1;
}

//========================================
//功能：安装服务函数
//========================================
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

//========================================
//功能：删除服务
//========================================
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

//========================================
//功能：启动服务
//========================================
BOOL StartService(char* szServceName)
{
	SC_HANDLE schSCManager;
	SC_HANDLE hService;

	//打开服务管理器
	schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);
	if (schSCManager == NULL) 
	{
		printf("faint,open scmanger failed\n");
		return false; 
	}
	//以完全控制的权限打开指定服务
	hService=OpenService(schSCManager,szServceName,SERVICE_ALL_ACCESS);
	if (hService == NULL) 
	{
		printf("faint,open services failt\n");
		return false;
	}
	// 启动服务
	if( ::StartService(hService, NULL, NULL) == FALSE)
	{
		::CloseServiceHandle( hService);
		::CloseServiceHandle(schSCManager);
		return false;
	}

	return true;
}
//========================================
//功能：停止服务
//========================================
BOOL StopService(char* szServceName)
{
	SC_HANDLE schSCManager;
	SC_HANDLE hService;
	
	//打开服务管理器
	schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);
	if (schSCManager == NULL) 
	{
		printf("faint,open scmanger failed\n");
		return false; 
	}
	//以完全控制的权限打开指定服务
	hService=OpenService(schSCManager,szServceName,SERVICE_ALL_ACCESS);
	if (hService == NULL) 
	{
		printf("faint,open services failt\n");
		return false;
	}

	SERVICE_STATUS status;
	// 停止服务
	if( ::ControlService(hService, 
		SERVICE_CONTROL_STOP, &status) == FALSE)
	{
		::CloseServiceHandle(hService);
		::CloseServiceHandle(schSCManager);
		return FALSE;
	}
	// 等待服务停止
	while( ::QueryServiceStatus(hService, &status) == TRUE)
	{
		::Sleep(status.dwWaitHint);
		if( status.dwCurrentState == SERVICE_STOPPED)
		{
			::CloseServiceHandle(hService);
			::CloseServiceHandle(schSCManager);
			return FALSE;
		}
	}
	return true;
}

//========================================
//功能：查询服务状态
//========================================
BOOL QueryServiceStatus(char* szServiceName)
{
	SC_HANDLE schSCManager;
	SC_HANDLE hService;
	
	//打开服务管理器
	schSCManager = OpenSCManager(NULL,NULL,SC_MANAGER_ALL_ACCESS);
	if (schSCManager == NULL) 
	{
		printf("faint,open scmanger failed\n");
		return false; 
	}
	//以完全控制的权限打开指定服务
	hService=OpenService(schSCManager,szServiceName,SERVICE_ALL_ACCESS);
	if (hService == NULL) 
	{
		printf("faint,open services failt\n");
		return false;
	}
	
	// 获得服务的状态
	SERVICE_STATUS status;
	if( ::QueryServiceStatus(hService, &status) == FALSE)
	{	 	
		 	::CloseServiceHandle(hService);
		 	::CloseServiceHandle(schSCManager);
		 	return FALSE;
	}
	
	switch (status.dwCurrentState)
	{
	case SERVICE_STOPPED:
			printf("The service is not running.\n");
			break;
	case SERVICE_RUNNING:
			printf("status: The service is running.\n");
			break;
	case SERVICE_START_PENDING:
		printf("status: SERVICE_START_PENDING\n");
		break;
	}
	return true;
}