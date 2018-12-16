/////////////////////////////////////////////////////////
//	获得机器名.cpp
//	#include <windows.h>
//	 Library: Use Kernel32.lib.
/////////////////////////////////////////////////////////

char szHostName[128];
memset(szHostName,0,sizeof(szHostName)/sizeof(char));

DWORD len=sizeof(szHostName);
GetComputerName(szHostName,&len);
cout<<szHostName<<endl;

/////////////////////////////////////////////////////////
//	功能：获得机器名和ip
/////////////////////////////////////////////////////////
#include "iostream.h"

#include <winsock2.h>
#pragma comment(lib,"ws2_32.lib")

void main()
{
	char szHostName[128];
	memset(szHostName,0,sizeof(szHostName)/sizeof(char));
	
	WSADATA wsaData;
	if(WSAStartup(MAKEWORD(2,0),&wsaData)==0)
	{
		if (gethostname(szHostName,sizeof(szHostName)/sizeof(char))==0)
		{		
			struct hostent * pHost;	
			int i; 	
			pHost = gethostbyname(szHostName); 

			for( i = 0; pHost!= NULL && pHost->h_addr_list[i]!= NULL; i++ ) 	
			{				 
				char *pSz=inet_ntoa(*(struct in_addr *)pHost->h_addr_list[i]);
				//输出机器名和ip地址
				cout<<szHostName<<" "<<pSz<<endl;
			}
		}
	}
	WSACleanup();
}
