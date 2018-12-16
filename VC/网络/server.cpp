/********************************* server.cpp ************************
************* TCP/IP application server example **********************
********************** 2009-11-05 ***********************************/

#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib,"ws2_32.lib")

void main()
{
	//初始化 
	WSADATA wsaData; 
	int iResult = WSAStartup(MAKEWORD(2,2),&wsaData);
	if(iResult != 0 ){
		printf("WSAStartup Failed.\n");
		return ;
	}

	//建立socket
	SOCKET server;
	server = socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);
    if(server == INVALID_SOCKET){
	  printf("socket Error %ld.\n",WSAGetLastError());
	  WSACleanup(); 
	  return;
	}

	//绑定socket
	sockaddr_in service;

	service.sin_family = AF_INET;
	service.sin_addr.s_addr = inet_addr("127.0.0.1");
	service.sin_port = htons(27015);

	if(bind(server,(SOCKADDR*)&service,sizeof(service)) == SOCKET_ERROR){
		printf("Error bind .\n");
	    closesocket(server); //关闭一个已经存在的socket
		return;
	}

	//监听socket
	if(listen(server,1) == SOCKET_ERROR ){ 
		//给socket serve设置一个监听的状态，让他可以接受一个新的连接
		printf("Error listening on socket.\n");
	}

	//接受一个连接
	SOCKET AcceptSocket;
	printf("Waiting for a client to connect ... \n");
	while(1){
		AcceptSocket = SOCKET_ERROR;
			while(AcceptSocket == SOCKET_ERROR){
				AcceptSocket = accept(server,NULL,NULL); 
			}//while(AcceptSocket == SOCKET_ERROR)
	printf("----------- Client Connected.---------------\n");
	server = AcceptSocket;
    break;
	}//while(1)

 //发送数据
	int bytesSent ;
	int bytesRecv = SOCKET_ERROR; // #define SOCKET_ERROR (-1)
    
	char sendBuf[128] = "Hello,I'm Server. What are you doing ? Can I help you ?\n";
    char recvBuf[128] = ""; //接收来自客户端发送过来的数据

	bytesRecv = recv(server,recvBuf,128,0);
	printf("Bytes Recv: %ld\n",bytesRecv);

	bytesSent = send(server,sendBuf,sizeof(sendBuf),0);
    printf("BytesSend: %ld\n",bytesSent);

	return;

}//main()
