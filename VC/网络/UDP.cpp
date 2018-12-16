//UDP Socket编程 C/C++实现 （Windows Platform SDK） 
//Server: 
//------------------------------------------------------- 
#pragma comment (lib,"ws2_32.lib") 
#include <Winsock2.h> 
#include <stdio.h>  

void main()  
{  
    //版本协商  
    WORD wVersionRequested;  
    WSADATA wsaData;  
    int err;  
    wVersionRequested = MAKEWORD( 1, 1 );  
    err = WSAStartup( wVersionRequested, &wsaData );  
    if ( err != 0 ) {  
        /* Tell the user that we could not find a usable */ 
        /* WinSock DLL.                                  */ 
        return;  
    }  
    /* Confirm that the WinSock DLL supports 2.2.*/ 
    /* Note that if the DLL supports versions greater    */ 
    /* than 2.2 in addition to 2.2, it will still return */ 
    /* 2.2 in wVersion since that is the version we      */ 
    /* requested.                                        */ 
    if ( LOBYTE( wsaData.wVersion ) != 1 ||  
        HIBYTE( wsaData.wVersion ) != 1) {  
        /* Tell the user that we could not find a usable */ 
        /* WinSock DLL.                                  */ 
        WSACleanup( );  
         return;   
    }  
    /* The WinSock DLL is acceptable. Proceed. */ 
    //创建数据报套接字   
    SOCKET svr = socket(AF_INET,SOCK_DGRAM,0);  
    //创建本地地址信息  
    SOCKADDR_IN addr;  
    addr.sin_family = AF_INET;  
    addr.sin_port = htons(8421);  
    addr.sin_addr.S_un.S_addr = htonl(INADDR_ANY);  
   int len = sizeof(sockaddr);  
    bind(svr,(sockaddr*)&addr,len);  
    //创建客户端地址对象  
    SOCKADDR_IN addrClient;  
    char recvBuf[128];  
    char sendBuf[128];  
    char tempBuf[256]; 
    while(true)  
    {  
        //接收数据  
        recvfrom(svr,recvBuf,128,0,(sockaddr*)&addrClient,&len);  
        char* ipClient = inet_ntoa(addrClient.sin_addr);  
        sprintf(tempBuf,"%s said: %s\n",ipClient,recvBuf);  
        printf("%s",tempBuf);  
        gets(sendBuf);  
        //发送数据  
        sendto(svr,sendBuf,strlen(sendBuf)+1,0,(sockaddr*)&addrClient,len);  
    }  
    closesocket(svr);  
    WSACleanup();  
} 


Client: 
------------------------------------------------------ 
#pragma comment (lib,"ws2_32.lib") 
#include <Winsock2.h> 
#include <stdio.h>  

void main()  
{  
    //版本协商  
    WORD wVersionRequested;  
    WSADATA wsaData;  
    int err;  
    wVersionRequested = MAKEWORD( 1, 1 ); 
    err = WSAStartup( wVersionRequested, &wsaData );  
    if ( err != 0 ) {  
        /* Tell the user that we could not find a usable */ 
        /* WinSock DLL.                                  */ 
        return;  
    }  
    /* Confirm that the WinSock DLL supports 2.2.*/ 
    /* Note that if the DLL supports versions greater    */ 
    /* than 2.2 in addition to 2.2, it will still return */ 
    /* 2.2 in wVersion since that is the version we      */ 
    /* requested.                                        */ 
    if ( LOBYTE( wsaData.wVersion ) != 1 ||  
        HIBYTE( wsaData.wVersion ) != 1 ) {  
        /* Tell the user that we could not find a usable */ 
        /* WinSock DLL.                                  */ 
        WSACleanup( );  
        return;   
    }  

    /* The WinSock DLL is acceptable. Proceed. */ 
    //创建服务器套接字  
    SOCKET Svr = socket(AF_INET,SOCK_DGRAM,0);  
    //创建地址  
    SOCKADDR_IN addrSvr;  
    addrSvr.sin_family = AF_INET;  
    addrSvr.sin_port = htons(6000);  
    addrSvr.sin_addr.S_un.S_addr = inet_addr("127.0.0.1");  
    char recvBuf[128];  
    char sendBuf[128];  
    int len = sizeof(sockaddr);  
    while(true)  
    {  
        gets(sendBuf);  
        //发送数据  
        sendto(Svr,sendBuf,strlen(sendBuf)+1,0,(sockaddr*)&addrSvr,len);  
        //接收数据  
        recvfrom(Svr,recvBuf,128,0,(sockaddr*)&addrSvr,&len);  
        char* ipSvr = inet_ntoa(addrSvr.sin_addr);  
        printf("%s said: %s\n",ipSvr,recvBuf);  
    }  
    closesocket(Svr);  
    WSACleanup();  
} 
