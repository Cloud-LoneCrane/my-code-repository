//===============================================================
//功能:
//	网卡地址
//参数:
//	无
//返回值:
//	网卡地址
//备注:
//===============================================================
bool GetHostMAC(string & strMac)
{
	//string strMac;
	string strGateway;
	
	PIP_ADAPTER_INFO pAdapterInfo;
	PIP_ADAPTER_INFO pAdapter=NULL;
	DWORD dwRetVal=0;
	pAdapterInfo=(IP_ADAPTER_INFO*)malloc(sizeof(IP_ADAPTER_INFO));
	ULONG ulOutBufLen=sizeof(IP_ADAPTER_INFO);
	
	if((dwRetVal=GetAdaptersInfo(pAdapterInfo,&ulOutBufLen))==NO_ERROR)
	{
		pAdapter=pAdapterInfo;
		while(pAdapter)
		{
			// 			printf("------------------------------------------------------------\r\n");
			// 			printf("Adapter Info:\r\n");
			// 			printf("------------------------------------------------------------\r\n");
			// 			printf("%-12s %s\n","AdapterName:",pAdapter->AdapterName);
			// 			printf("%-12s %s\n","AdapterDesc:",pAdapter->Description);
			// 			printf("%-12s ","AdapterAddr:");
			for(UINT i=0;i<pAdapter->AddressLength;i++)
			{
				char buf[4] = {0};
				if (i<pAdapter->AddressLength - 1)
					sprintf(buf,"%02X%c",pAdapter->Address[i],'-');
				else
					sprintf(buf,"%02X",pAdapter->Address[i]);
				strMac += buf;
			}
			// 			printf("\r\n");
			// 			printf("%-12s %d\r\n","AdapterType:",pAdapter->Type);
			// 			printf("%-12s %s\r\n","IPAddress:",pAdapter->IpAddressList.IpAddress.String);
			// 			printf("%-12s %s\r\n","IPMask:",pAdapter->IpAddressList.IpMask.String);
			// 			printf("%-12s %s\r\n","Gateway:",pAdapter->GatewayList.IpAddress.String);
			// 			printf("------------------------------------------------------------\r\n");
			
			char szBuf[50] = {0};
			sprintf(szBuf,"%s",pAdapter->GatewayList.IpAddress.String);
			strGateway = szBuf;
			if (strGateway.length() == 0)
			{
				strMac.clear();
			}
			pAdapter=pAdapter->Next;
		}
		
	}
	else
	{
		//("Get Net Adapter Information.\r\n");
	}
	
	free(pAdapterInfo);
	if (strMac.length() == 0)
		return false;
	else
		return true;
}
