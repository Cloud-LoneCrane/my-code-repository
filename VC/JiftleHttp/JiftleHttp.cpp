
#include "JiftleHttp.h"
#include "StdAfx.h"

#include <afxinet.h>
#include <stdio.h>
#include <string.h>


#pragma comment(lib,"Wininet.lib")

bool DownloadUrlFile(CString strUrlFile,CString strLocalFile)
{
#define  MAXBLOCKSIZE 100
	
//获得主目录
	CString strHomeDir;
	TCHAR	szPath[MAX_PATH];
	int pos = 0;
	CString strFile;

	GetModuleFileName(NULL,szPath,sizeof(szPath)/sizeof(TCHAR));
	strHomeDir = szPath;
	pos = strHomeDir.ReverseFind(_T('\\'));
	if (pos > 0)
		strHomeDir = strHomeDir.Mid(0,pos+1);
	else
		strHomeDir = _T("");

	strFile += strHomeDir;
	strFile += strLocalFile;

	HINTERNET hSession = InternetOpen("IE/1.0",INTERNET_OPEN_TYPE_PRECONFIG,NULL,NULL,0);
	if(hSession == NULL) 
		return FALSE;
	if(hSession != NULL)
	{
        HINTERNET handle2 = InternetOpenUrl(hSession, strUrlFile, NULL, 0,INTERNET_FLAG_DONT_CACHE,0);
		if(handle2 == NULL)
			return FALSE;
		
		if(handle2 != NULL)
		{
			byte  Temp[MAXBLOCKSIZE];
            ULONG Number = 1;
            
            FILE  * stream;
			if((stream = fopen(strFile,"wb"))  !=  NULL ) // 这里只是个测试，因此写了个死的文件路径 
			{
				while(Number>0)
				{
                    InternetReadFile(handle2, Temp, MAXBLOCKSIZE-1,&Number);
					fwrite(Temp,sizeof(char),Number,stream);
                } 
                fclose( stream );
            } 
            
            InternetCloseHandle(handle2);
            handle2  =  NULL;
        } 
        InternetCloseHandle(hSession);
        hSession  =  NULL;
    } 
	
	return TRUE;
}
