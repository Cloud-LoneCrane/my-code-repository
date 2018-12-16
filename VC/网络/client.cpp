/*********************** client.cpp ****************************
************   TCP/IP application client example   *************
***************************************************************/

#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib,"ws2_32.lib")

void main()
{
  //初始化
  WSADATA wsaData;
  
  int iResult = WSAStartup(MAKEWORD(2,2),&wsaData);
  if (iResult != 0 ){
	  printf("WSAStartup Failed.\n");
	  return;
  }
  
  //创建一个socket 
  SOCKET client;
  client = socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);
  if (client == INVALID_SOCKET ){
     printf("socket Failed.\n");
	 WSACleanup();//socket建立失败，终止对Ws2_32.dll的使用
  }

  //绑定一个socket
  	sockaddr_in service;

	service.sin_family = AF_INET;
	service.sin_addr.s_addr = inet_addr("127.0.0.1");
	service.sin_port = htons(27015);

//	if(bind(client,(SOCKADDR *)&service,sizeof(service)) == SOCKET_ERROR) {
//				printf("Error bind ld%.\n",WSAGetLastError());
//	    closesocket(client); //关闭socket
//		return;
//	}

	//连接服务端
	SOCKET m_socket;
	if (connect(client,(SOCKADDR *)&service,sizeof(service)) == SOCKET_ERROR){
		printf("connect Failed %ld.\n",WSAGetLastError());
		WSACleanup();
		return;
	}

	printf("The connect has been established...\n");

	//连接连接成功后，开始收发数据
	int bytesSent;
	int bytesRecv = SOCKET_ERROR;
	char sendBuf[128] = "Hello, I'm a tcp client.\n";
	char recvBuf[128] = "";
	
	while(bytesRecv == SOCKET_ERROR){
		bytesRecv = recv(client,recvBuf,128,0);  //接收数据
        if (bytesRecv == 0 || bytesRecv == WSAECONNRESET){
           printf("Connection Closed.\n");
		   //return;
		   getchar();
		}

		if (bytesRecv < 0){
		   //return;
		printf("接收到数据的长度小于0。\n");
		}
		else
		  printf("Server: %s\n",recvBuf);
	
		printf("Bytes Recv: %ld\n",bytesRecv);
	}

	WSACleanup();
}
