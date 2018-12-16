//创建匿名管道

#include "Windows.h"
#include "stdio.h"

void main()
{
	SECURITY_ATTRIBUTES sa;	//创建匿名管道
	HANDLE hRead,hWrite;

	sa.nLength = sizeof(sa);
	sa.lpSecurityDescriptor = NULL;
	sa.bInheritHandle = TRUE;

	if(!CreatePipe(&hRead,&hWrite,&sa,0))
	{
		printf("Error at CreatePipe...\n");
		return ;
	}

	STARTUPINFO si;
	PROCESS_INFORMATION pi;

	si.cb =sizeof(STARTUPINFO);
	GetStartupInfo(&si);
	si.hStdError= hWrite;
//	si.hStdInput = hWrite;
	si.hStdOutput =hWrite;

	si.wShowWindow = SW_HIDE;
	si.dwFlags = STARTF_USESHOWWINDOW |STARTF_USESTDHANDLES;
	
	if (!CreateProcess(NULL,"mysql.exe -uroot -pjift yqsnew<yqsnew.bak",NULL,NULL,TRUE,CREATE_NO_WINDOW,NULL,NULL,&si,&pi))
	{
		printf("Error at CreatePriocess...\n");
		return;
	}

	CloseHandle(hWrite);

	char buffer[2048]={0};

	FILE *file = NULL ;
	file=fopen("C:\\file.txt","a");
	int i=0;
	
	for (DWORD bytesRead;ReadFile(hRead,buffer,2048,&bytesRead,NULL);memset(buffer,0,2048))
	{
	
			fprintf(file,"%s\n",buffer);


	}
	
	fclose(file);

}


