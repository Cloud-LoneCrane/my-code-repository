/*********************** client.cpp ****************************
************   TCP/IP application client example   *************
***************************************************************/

#include <stdio.h>
#include <winsock2.h>
#pragma comment(lib,"ws2_32.lib")

void main()
{
  //��ʼ��
  WSADATA wsaData;
  
  int iResult = WSAStartup(MAKEWORD(2,2),&wsaData);
  if (iResult != 0 ){
	  printf("WSAStartup Failed.\n");
	  return;
  }
  
  //����һ��socket 
  SOCKET client;
  client = socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);
  if (client == INVALID_SOCKET ){
     printf("socket Failed.\n");
	 WSACleanup();//socket����ʧ�ܣ���ֹ��Ws2_32.dll��ʹ��
  }

  //��һ��socket
  	sockaddr_in service;

	service.sin_family = AF_INET;
	service.sin_addr.s_addr = inet_addr("127.0.0.1");
	service.sin_port = htons(27015);

//	if(bind(client,(SOCKADDR *)&service,sizeof(service)) == SOCKET_ERROR) {
//				printf("Error bind ld%.\n",WSAGetLastError());
//	    closesocket(client); //�ر�socket
//		return;
//	}

	//���ӷ����
	SOCKET m_socket;
	if (connect(client,(SOCKADDR *)&service,sizeof(service)) == SOCKET_ERROR){
		printf("connect Failed %ld.\n",WSAGetLastError());
		WSACleanup();
		return;
	}

	printf("The connect has been established...\n");

	//�������ӳɹ��󣬿�ʼ�շ�����
	int bytesSent;
	int bytesRecv = SOCKET_ERROR;
	char sendBuf[128] = "Hello, I'm a tcp client.\n";
	char recvBuf[128] = "";
	
	while(bytesRecv == SOCKET_ERROR){
		bytesRecv = recv(client,recvBuf,128,0);  //��������
        if (bytesRecv == 0 || bytesRecv == WSAECONNRESET){
           printf("Connection Closed.\n");
		   //return;
		   getchar();
		}

		if (bytesRecv < 0){
		   //return;
		printf("���յ����ݵĳ���С��0��\n");
		}
		else
		  printf("Server: %s\n",recvBuf);
	
		printf("Bytes Recv: %ld\n",bytesRecv);
	}

	WSACleanup();
}
