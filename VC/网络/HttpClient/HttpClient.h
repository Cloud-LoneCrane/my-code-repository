//================================================================
// HttpClient.h is Header file of HttpClient class
// Author:	jiftle 
// Date:	2011-08-11

#pragma once

//scoket lib
#include "Winsock2.h"
#pragma comment(lib,"Ws2_32.lib")

#include <string>
#include <list>

using namespace std;

class HttpClient
{
public:
	HttpClient(const char* server,const int port);
	~HttpClient(void);

	
	bool Get(const char* url,char* body,int maxsize);//有的回传数据很大，body应该声明的足够大
	bool Download(const char* url,const char* filename);
	bool Post(const char* url,char* body,char* retbody,int maxsize);

private:
	bool reconnect();
	bool IsConnected();

private:
	SOCKET	m_client;
	
public:
	bool m_connected;
	char	m_server[256];
	int		m_port;
	list<string> m_listCookies;

};
