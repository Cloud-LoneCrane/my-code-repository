//================================================================
// HttpClient.h is Header file of HttpClient class
// Author:	jiftle 
// Date:	2011-08-11

#include "StdAfx.h"
#include "HttpClient.h"


HttpClient::HttpClient(const char* server,const int port)
{
	memset(m_server,0,sizeof(m_server));
	strcpy(m_server,server);
	m_port = port;

	//初始化
	WSADATA wsaData;
	char* szIp = NULL;

	int iResult = WSAStartup(MAKEWORD(2,2),&wsaData);
	if (iResult != 0 ){
		printf("WSAStartup Failed.\n");
		m_connected = false;
		return;
	}

	//创建一个socket 
	m_client = socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);
	if (m_client == INVALID_SOCKET ){
		printf("socket Failed.\n");
		WSACleanup();//socket建立失败，终止对Ws2_32.dll的使用
		m_connected = false;
	}

	struct hostent* lpHostent = gethostbyname(server);

	//绑定一个socket
	sockaddr_in service;

	service.sin_family = AF_INET;
	service.sin_addr.s_addr = *(u_long *)lpHostent->h_addr_list[0]; //inet_addr("127.0.0.1");
	service.sin_port = htons(port);

	char* ip = inet_ntoa(service.sin_addr);

	//连接服务端
	//SOCKET m_socket;
	if (connect(m_client,(SOCKADDR *)&service,sizeof(service)) == SOCKET_ERROR){
		printf("connect Failed %ld.\n",WSAGetLastError());
		WSACleanup();
		m_connected = false;
		return;
	}

	m_connected = true;
}


bool HttpClient::reconnect()
{
	bool bRet = false;

	struct hostent* lpHostent = gethostbyname((const char*)m_server);

	//绑定一个socket
	sockaddr_in service;

	service.sin_family = AF_INET;
	service.sin_addr.s_addr = *(u_long *)lpHostent->h_addr_list[0]; //inet_addr("127.0.0.1");
	service.sin_port = htons(m_port);

	char* ip = inet_ntoa(service.sin_addr);

	int ret = closesocket(m_client);
	
	//创建一个socket 
	m_client = socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);
	if (m_client == INVALID_SOCKET ){
		printf("socket Failed.\n");
		WSACleanup();//socket建立失败，终止对Ws2_32.dll的使用
		m_connected = false;
	}

	//连接服务端
	//SOCKET m_socket;
	if (connect(m_client,(SOCKADDR *)&service,sizeof(service)) == SOCKET_ERROR){
		printf("connect Failed %ld.\n",WSAGetLastError());
	
		int errcode = 0;
		errcode = GetLastError();
		errcode = 0;
		WSACleanup();
		m_connected = false;
		bRet = false; 
	}

	m_connected = true;
	bRet = true;
	return true;
}


bool HttpClient::Get(const char* url,char* body,int maxsize)
{
	bool bRet = false;
	string strUrl;
	string strSend;
	string strHost;
	string strRecv;
	string strHeader;
	int totallen = 0;
	int recvlen = 0;
	bool bFindHeader = false;
	string strBody;
	string strCookie;

	int len = 0;
	char buf[4096] = {0};
	int posStart = 0,posEnd = 0;

	strUrl = url;
	posStart = strUrl.find("http://");
	if (posStart != -1)
	{
		posStart += strlen("http://");
		posEnd = strUrl.find("/",posStart);
		if (posEnd != -1)
		{
			strHost = strUrl.substr(posStart,posEnd-posStart);
		}
	}
		
	//traverse
	for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
	{
		string strT = *i;
		strT += "; ";
		if (strCookie.length() == 0)
		{
			strCookie = "Cookie: ";
			strCookie += strT;
		}
		else
		{
			strCookie += strT;
		}
	}

	strSend = "GET ";
	strSend += url;
	strSend += " HTTP/1.1\r\n";
	strSend += "Accept: text/html\r\n";
	strSend += "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)\r\n";
	strSend += "Host: ";
	strSend += strHost;
	strSend += "\r\n";
	strSend += "Content-Length: 0\r\n";
	strSend += "Connection: Keep-Alive\r\n";
	if (strCookie.length() > 0)
	{
		strCookie += "\r\n";
		strSend += strCookie;
	}
	strSend += "Pragma: no-cache\r\n\r\n";
	
	if (IsConnected())
	{
		len = send(m_client,strSend.c_str(),strSend.length(),0);
		
		TRACE("--------------------send------------------------\r\n");
		char * p = new char[strHeader.length() + 1];
		strcpy(p, strHeader.c_str());
		TRACE("%s",p);
		delete []p; 


		recvlen = 0;
		totallen = 0;
		//strBody.clear();
		strBody = "";
		do 
		{
			memset(buf,0,sizeof(buf));
			len = recv(m_client,buf,sizeof(buf),0);

			if (len == SOCKET_ERROR )
			{
				int errcode = 0;
				errcode = GetLastError();

				errcode = 0;
				bRet = false;
				break;
			}
			else
				strRecv = buf;

			if(bFindHeader)
			{
				//---
				string strTmp = strRecv.substr(0,len); strBody += strTmp;
				recvlen += len;
			}
			else
			{
				posStart = strRecv.find("\r\n\r\n");
				if (posStart != -1)
				{
					bFindHeader = true;

					strHeader = strRecv.substr(0,posStart + 4);
					TRACE("--------------------recv------------------------\r\n");
					char * p = new char[strHeader.length() + 1];
					strcpy(p,strHeader.c_str());
					TRACE("%s",p);
					delete [] p; 
				}

				//获得Content-Length
				string strContentLengthFlag = "Content-Length: ";
				posStart = strRecv.find(strContentLengthFlag); 
				if (posStart != -1)
				{
					posStart += strContentLengthFlag.length();
					posEnd = strRecv.find("\r\n",posStart);
					string strContentLength;
					strContentLength = strRecv.substr(posStart,posEnd-posStart);
					totallen = atoi(strContentLength.c_str());

				}

				strBody += strRecv.substr(strHeader.length(),len-strHeader.length());
				recvlen += len - strHeader.length(); 

				//cookies
				posStart = strHeader.find("Set-Cookie: ");
				if (posStart != -1)
				{
					posStart += strlen("Set-Cookie: ");
					posEnd = strHeader.find("\r\n",posStart);
					if (posEnd != -1)
					{
						string strCookieTmp = strHeader.substr(posStart,posEnd-posStart);
						char *p = NULL;
						p = strtok((char*)strCookieTmp.c_str(),";");
						while(p != NULL)
						{
							string strElemTmp = p;
							bool bFindCookie = false;
							if (strElemTmp != " path=/" && strElemTmp.find("expires=") == -1)
							{
								//traverse
								for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
								{
									string strT = *i;
									if (strT == strElemTmp)
									{
										bFindCookie = true;
									}
								}

								if (!bFindCookie)
								{
									m_listCookies.push_back(strElemTmp);
								}
								
							}
							p = strtok(NULL,";");
						}
					}
				}
			}


		} while (recvlen != totallen );

	}
	else
	{
		if(reconnect())
		{
			len = send(m_client,strSend.c_str(),strSend.length(),0);
			TRACE("--------------------send------------------------\r\n");
			char * p = new char[strHeader.length() + 1];
			strcpy(p, strHeader.c_str());
			TRACE("%s",p);
			delete []p; 

			recvlen = 0;
			totallen = 0;
			//strBody.clear();
			strBody = "";
			do 
			{
				memset(buf,0,sizeof(buf));
				len = recv(m_client,buf,sizeof(buf),0);

			if (len == SOCKET_ERROR )
			{
				int errcode = 0;
				errcode = GetLastError();

				errcode = 0;
				bRet = false;
				break;
			}
			else
				strRecv = buf;
			
			if(bFindHeader)
			{
				//---	
				string strTmp = strRecv.substr(0,len); strBody += strTmp;
				recvlen += len;
			}
			else
			{
				posStart = strRecv.find("\r\n\r\n");
				if (posStart != -1)
				{
					bFindHeader = true;
					strHeader = strRecv.substr(0,posStart + 4);

					TRACE("--------------------recv------------------------\r\n");
					TRACE(strHeader.c_str());
				}

				//获得Content-Length
				string strContentLengthFlag = "Content-Length: ";
				posStart = strRecv.find(strContentLengthFlag); 
				if (posStart != -1)
				{
					posStart += strContentLengthFlag.length();
					posEnd = strRecv.find("\r\n",posStart);
					string strContentLength;
					strContentLength = strRecv.substr(posStart,posEnd-posStart);
					totallen = atoi(strContentLength.c_str());
					
				}

				strBody += strRecv.substr(strHeader.length(),len-strHeader.length());
				recvlen += len - strHeader.length(); 

				//cookies
				posStart = strHeader.find("Set-Cookie: ");
				if (posStart != -1)
				{
					posStart += strlen("Set-Cookie: ");
					posEnd = strHeader.find("\r\n",posStart);
					if (posEnd != -1)
					{
						string strCookieTmp = strHeader.substr(posStart,posEnd-posStart);
						char *p = NULL;
						p = strtok((char*)strCookieTmp.c_str(),";");
						while(p != NULL)
						{
							string strElemTmp = p;
							bool bFindCookie = false;
							if (strElemTmp != " path=/" && strElemTmp.find("expires=") == -1)
							{
								//traverse
								for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
								{
									string strT = *i;
									if (strT == strElemTmp)
									{
										bFindCookie = true;
									}
								}

								if (!bFindCookie)
								{
									m_listCookies.push_back(strElemTmp);
								}

							}
							p = strtok(NULL,";");
						}
					}
				}
			}

			
		} while (recvlen != totallen );
			
	}
		else
			bRet = false;
	}
	
	strcpy(body,strBody.c_str());

	return true;
}



bool HttpClient::Download(const char* url,const char* filename)
{
	bool bRet = false;
	string strUrl;
	string strSend;
	string strHost;
	string strRecv;
	string strHeader;
	int totallen = 0;
	int recvlen = 0;
	int bodylen = 0;
	int headerlen = 0;
	bool bFindHeader = false;
	string strBody;
	string strCookie;
	FILE * pFile;
	
	pFile = fopen (filename , "wb");
	if (pFile == NULL)
		bRet = false;
	
	//fwrite(strBody.c_str(),1,strlen(strBody.c_str()),pFile);
	
	int len = 0;
	char buf[4096] = {0};
	int posStart = 0,posEnd = 0;

	strUrl = url;
	posStart = strUrl.find("http://");
	if (posStart != -1)
	{
		posStart += strlen("http://");
		posEnd = strUrl.find("/",posStart);
		if (posEnd != -1)
		{
			strHost = strUrl.substr(posStart,posEnd-posStart);
		}
	}

	//traverse
	for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
	{
		string strT = *i;
		strT += "; ";
		if (strCookie.length() == 0)
		{
			strCookie = "Cookie: ";
			strCookie += strT;
		}
		else
		{
			strCookie += strT;
		}
	}

	strSend = "GET ";
	strSend += url;
	strSend += " HTTP/1.1\r\n";
	strSend += "Accept: text/html\r\n";
	strSend += "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)\r\n";
	strSend += "Host: ";
	strSend += strHost;
	strSend += "\r\n";
	strSend += "Content-Length: 0\r\n";
	strSend += "Connection: Keep-Alive\r\n";
	if (strCookie.length() > 0)
	{
		strCookie += "\r\n";
		strSend += strCookie;
	}

	strSend += "Pragma: no-cache\r\n\r\n";

	if (IsConnected())
	{
		len = send(m_client,strSend.c_str(),strSend.length(),0);
		TRACE("--------------------send------------------------\r\n");
		char * p = new char[strHeader.length() + 1];
		strcpy(p, strHeader.c_str());
		TRACE("%s",p);
		delete []p; p=NULL;

		recvlen = 0;
		totallen = 0;
		//strBody.clear();
		strBody = "";
		do 
		{
			bodylen = 0;
			headerlen = 0;

			memset(buf,0,sizeof(buf));
			len = recv(m_client,buf,sizeof(buf),0);//得到数据

			if (len == SOCKET_ERROR)
			{
				int errcode = 0;
				errcode = GetLastError();

				errcode = 0;
				bRet = false;
				break;
			}
			else
				strRecv = buf;

			posStart = strRecv.find("\r\n\r\n");
			if(posStart != -1)
				bFindHeader = true;
			else
				bFindHeader = false;

			if(!bFindHeader)
			{//没有找到头,就都是内容了
					
				string strTmp = strRecv.substr(0,len); 
				strBody = strTmp;//内容
				recvlen += len;
				bodylen = len;
			}
			else
			{
				posStart = strRecv.find("\r\n\r\n");
				if (posStart != -1)
				{
					bFindHeader = true;

					headerlen = posStart + 4;
					strHeader = strRecv.substr(0,posStart + 4);
					TRACE("--------------------recv------------------------\r\n");
					TRACE("%s",strHeader.c_str());
				}

				//获得Content-Length
				string strContentLengthFlag = "Content-Length: ";
				posStart = strRecv.find(strContentLengthFlag); 
				if (posStart != -1)
				{
					posStart += strContentLengthFlag.length();
					posEnd = strRecv.find("\r\n",posStart);
					string strContentLength;
					strContentLength = strRecv.substr(posStart,posEnd-posStart);
					totallen = atoi(strContentLength.c_str());
				}

				//内容
				if (len>strHeader.length())
				{
					strBody = strRecv.substr(strHeader.length(),len-strHeader.length());
				}
				else
					strBody = "";

				//recvlen += len - strHeader.length(); 
				bodylen = len - strHeader.length();
				recvlen += bodylen;

				//cookies
				posStart = strHeader.find("Set-Cookie: ");
				if (posStart != -1)
				{
					posStart += strlen("Set-Cookie: ");
					posEnd = strHeader.find("\r\n",posStart);
					if (posEnd != -1)
					{
						string strCookieTmp = strHeader.substr(posStart,posEnd-posStart);
						char *p = NULL;
						p = strtok((char*)strCookieTmp.c_str(),";");
						while(p != NULL)
						{
							string strElemTmp = p;
							bool bFindCookie = false;
							if (strElemTmp != " path=/" && strElemTmp.find("expires=") == -1)
							{
								//traverse
								for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
								{
									string strT = *i;
									if (strT == strElemTmp)
									{
										bFindCookie = true;
									}
								}

								if (!bFindCookie)
								{
									m_listCookies.push_back(strElemTmp);
								}

							}
							p = strtok(NULL,";");
						}
					}
				}
			}
		
// 			if (bFindHeader)
// 			{
				//if(strBody.length()>0)
				//{
					//fwrite(strBody.c_str(),1,strlen(strBody.c_str()),pFile);
				  //  fwrite(strBody.c_str(),1,sizeof(strBody.c_str()),pFile);
				//}
// 			}
// 			else
// 				fwrite(buf,1,sizeof(buf),pFile);

			//写入数据到文件
			if (bFindHeader)
			{
				fwrite(buf+headerlen,1,bodylen,pFile);
			}
			else
				fwrite(buf,1,len,pFile);

		TRACE("receive data len: %d\n",bodylen);
		} while (recvlen < totallen );

	}
	else
	{/*
		if(reconnect())
		{
			len = send(m_client,strSend.c_str(),strSend.length(),0);
			TRACE("--------------------send------------------------\r\n");
			char * p = new char[strHeader.length() + 1];
			strcpy(p, strHeader.c_str());
			TRACE("%s",p);
			delete []p; 

			recvlen = 0;
			totallen = 0;
			//strBody.clear();
			strBody = "";

			//循环下载
			do 
			{
				memset(buf,0,sizeof(buf));
				len = recv(m_client,buf,sizeof(buf),0);

				if (len == SOCKET_ERROR )
				{
					int errcode = 0;
					errcode = GetLastError();

					errcode = 0;
					bRet = false;
					break;
				}
				else
					strRecv = buf;

				if(bFindHeader)
				{
					//---	
					string strTmp = strRecv.substr(0,len); strBody += strTmp;
					recvlen += len;
				}
				else
				{
					posStart = strRecv.find("\r\n\r\n");
					if (posStart != -1)
					{
						bFindHeader = true;

						strHeader = strRecv.substr(0,posStart + 4);
						TRACE("--------------------recv------------------------\r\n");
						TRACE(strHeader.c_str());

					}

					//获得Content-Length
					string strContentLengthFlag = "Content-Length: ";
					posStart = strRecv.find(strContentLengthFlag); 
					if (posStart != -1)
					{
						posStart += strContentLengthFlag.length();
						posEnd = strRecv.find("\r\n",posStart);
						string strContentLength;
						strContentLength = strRecv.substr(posStart,posEnd-posStart);
						totallen = atoi(strContentLength.c_str());

					}

					strBody += strRecv.substr(strHeader.length(),len-strHeader.length());
					recvlen += len - strHeader.length(); 
					
					//cookies
					posStart = strHeader.find("Set-Cookie: ");
					if (posStart != -1)
					{
						posStart += strlen("Set-Cookie: ");
						posEnd = strHeader.find("\r\n",posStart);
						if (posEnd != -1)
						{
							string strCookieTmp = strHeader.substr(posStart,posEnd-posStart);
							char *p = NULL;
							p = strtok((char*)strCookieTmp.c_str(),";");
							while(p != NULL)
							{
								string strElemTmp = p;
								bool bFindCookie = false;
								if (strElemTmp != " path=/" && strElemTmp.find("expires=") == -1)
								{
									//traverse
									for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
									{
										string strT = *i;
										if (strT == strElemTmp)
										{
											bFindCookie = true;
										}
									}

									if (!bFindCookie)
									{
										m_listCookies.push_back(strElemTmp);
									}

								}
								p = strtok(NULL,";");
							}
						}
					}
				}


			} while (recvlen != totallen );

		}
		else
			bRet = false;
	*/}
	
	//close file
	if (pFile)
		fclose(pFile);

/*	
	FILE * pFile;
	
	pFile = fopen (filename , "wb");
	if (pFile == NULL)
		bRet = false;
	else
	{	
		fwrite(strBody.c_str(),1,strlen(strBody.c_str()),pFile);
		
		fclose (pFile);
	}
*/
	return true;
}


bool HttpClient::IsConnected()
{
	INT		iRet =   0; 
	bool	bRet =   TRUE; 

	struct timeval timeout   =   {   0,   0   }; 
	fd_set readSocketSet; 

	FD_ZERO(&readSocketSet); 
	FD_SET(m_client,&readSocketSet); 

	iRet = ::select(0,&readSocketSet,   NULL,   NULL,   &timeout); 
	bRet = (iRet > 0); 

	if(bRet){ 
		bRet = FD_ISSET(m_client,&readSocketSet); 
	} 

	return !bRet;
}


HttpClient::~HttpClient(void)
{
	closesocket(m_client);
	m_connected = false;

	WSACleanup();//socket建立失败，终止对Ws2_32.dll的使用
}




bool HttpClient::Post(const char* url,char* body,char* retbody,int maxsize)
{
	bool bRet = false;
	string strUrl;
	string strSend;
	string strHost;
	string strRecv;
	string strHeader;
	int totallen = 0;
	int recvlen = 0;
	bool bFindHeader = false;
	string strBody;
	string strCookie;
	string strSendBody;

	int len = 0;
	char buf[4096] = {0};
	int posStart = 0,posEnd = 0;

	strUrl = url;
	strSendBody = body;
	posStart = strUrl.find("http://");
	if (posStart != -1)
	{
		posStart += strlen("http://");
		posEnd = strUrl.find("/",posStart);
		if (posEnd != -1)
		{
			strHost = strUrl.substr(posStart,posEnd-posStart);
		}
	}

	//traverse
	for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
	{
		string strT = *i;
		strT += "; ";
		if (strCookie.length() == 0)
		{
			strCookie = "Cookie: ";
			strCookie += strT;
		}
		else
		{
			strCookie += strT;
		}
	}

	//content-length
	char pContentLength[5]={0};
	int iContentLength = strSendBody.length();
	itoa(iContentLength,pContentLength,10);

	strSend = "POST ";
	strSend += url;
	strSend += " HTTP/1.1\r\n";
	strSend += "Accept: text/html\r\n";
	strSend += "User-Agent: Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)\r\n";
	strSend += "Host: ";
	strSend += strHost;
	strSend += "\r\n";
	strSend += "Content-Type: application/x-www-form-urlencoded\r\n";
	strSend += "Content-Length: ";
	strSend += pContentLength;
	strSend += "\r\n";
	strSend += "Connection: Keep-Alive\r\n";
	if (strCookie.length() > 0)
	{
		strCookie += "\r\n";
		strSend += strCookie;
	}
	strSend += "Pragma: no-cache\r\n\r\n";
	strSend += strSendBody;

	if (IsConnected())
	{
		len = send(m_client,strSend.c_str(),strSend.length(),0);

		TRACE("--------------------send------------------------\r\n");
		char * p = new char[strHeader.length() + 1];
		strcpy(p, strHeader.c_str());
		TRACE("%s",p);
		delete []p; 
		TRACE("\r\n");

		recvlen = 0;
		totallen = 0;
		//strBody.clear();
		strBody = "";
		do 
		{
			memset(buf,0,sizeof(buf));
			len = recv(m_client,buf,sizeof(buf),0);

			if (len == SOCKET_ERROR )
			{
				int errcode = 0;
				errcode = GetLastError();

				errcode = 0;
				bRet = false;
				break;
			}
			else
				strRecv = buf;

			if(bFindHeader)
			{
				//---	
				string strTmp = strRecv.substr(0,len); strBody += strTmp;
				recvlen += len;
			}
			else
			{
				posStart = strRecv.find("\r\n\r\n");
				if (posStart != -1)
				{
					bFindHeader = true;

					strHeader = strRecv.substr(0,posStart + 4);
					TRACE("--------------------recv------------------------\r\n");
					TRACE(strHeader.c_str());
				}

				//获得Content-Length
				string strContentLengthFlag = "Content-Length: ";
				posStart = strRecv.find(strContentLengthFlag); 
				if (posStart != -1)
				{
					posStart += strContentLengthFlag.length();
					posEnd = strRecv.find("\r\n",posStart);
					string strContentLength;
					strContentLength = strRecv.substr(posStart,posEnd-posStart);
					totallen = atoi(strContentLength.c_str());

				}

				strBody += strRecv.substr(strHeader.length(),len-strHeader.length());
				recvlen += len - strHeader.length(); 

				//cookies
				posStart = strHeader.find("Set-Cookie: ");
				if (posStart != -1)
				{
					posStart += strlen("Set-Cookie: ");
					posEnd = strHeader.find("\r\n",posStart);
					if (posEnd != -1)
					{
						string strCookieTmp = strHeader.substr(posStart,posEnd-posStart);
						char *p = NULL;
						p = strtok((char*)strCookieTmp.c_str(),";");
						while(p != NULL)
						{
							string strElemTmp = p;
							bool bFindCookie = false;
							
							if (strElemTmp != " path=/" && strElemTmp.find("expires=") == -1)
							{
								//traverse
								for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
								{
									string strT = *i;
									if (strT == strElemTmp)
									{
										bFindCookie = true;
									}
								}

								if (!bFindCookie)
								{
									m_listCookies.push_back(strElemTmp);
								}

							}
							p = strtok(NULL,";");
						}
					}
				}
			}


		} while (recvlen != totallen );

	}
	else
	{
		if(reconnect())
		{
			len = send(m_client,strSend.c_str(),strSend.length(),0);
			TRACE("--------------------send------------------------\r\n");
			char * p = new char[strHeader.length() + 1];
			strcpy(p, strHeader.c_str());
			TRACE("%s",p);
			delete []p; 

			recvlen = 0;
			totallen = 0;
			//strBody.clear();
			strBody = "";
			do 
			{
				memset(buf,0,sizeof(buf));
				len = recv(m_client,buf,sizeof(buf),0);

				if (len == SOCKET_ERROR )
				{
					int errcode = 0;
					errcode = GetLastError();

					errcode = 0;
					bRet = false;
					break;
				}
				else
					strRecv = buf;

				if(bFindHeader)
				{
					//---	
					string strTmp = strRecv.substr(0,len); strBody += strTmp;
					recvlen += len;
				}
				else
				{
					posStart = strRecv.find("\r\n\r\n");
					if (posStart != -1)
					{
						bFindHeader = true;
						strHeader = strRecv.substr(0,posStart + 4);

						TRACE("--------------------recv------------------------\r\n");
						TRACE(strHeader.c_str());
					}

					//获得Content-Length
					string strContentLengthFlag = "Content-Length: ";
					posStart = strRecv.find(strContentLengthFlag); 
					if (posStart != -1)
					{
						posStart += strContentLengthFlag.length();
						posEnd = strRecv.find("\r\n",posStart);
						string strContentLength;
						strContentLength = strRecv.substr(posStart,posEnd-posStart);
						totallen = atoi(strContentLength.c_str());

					}

					strBody += strRecv.substr(strHeader.length(),len-strHeader.length());
					recvlen += len - strHeader.length(); 

					//cookies
					posStart = strHeader.find("Set-Cookie: ");
					if (posStart != -1)
					{
						posStart += strlen("Set-Cookie: ");
						posEnd = strHeader.find("\r\n",posStart);
						if (posEnd != -1)
						{
							string strCookieTmp = strHeader.substr(posStart,posEnd-posStart);
							char *p = NULL;
							p = strtok((char*)strCookieTmp.c_str(),";");
							while(p != NULL)
							{
								string strElemTmp = p;
								bool bFindCookie = false;
								if (strElemTmp != " path=/" && strElemTmp.find("expires=") == -1)
								{
									//traverse
									for (list<string>::iterator i = m_listCookies.begin(); i != m_listCookies.end(); i++)
									{
										string strT = *i;
										if (strT == strElemTmp)
										{
											bFindCookie = true;
										}
									}

									if (!bFindCookie)
									{
										m_listCookies.push_back(strElemTmp);
									}

								}
								p = strtok(NULL,";");
							}
						}
					}
				}


			} while (recvlen != totallen );

		}
		else
			bRet = false;
	}

	strcpy(retbody,strBody.c_str());

	return true;
}