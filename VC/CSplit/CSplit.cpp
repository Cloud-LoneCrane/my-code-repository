// CSplit.cpp: implementation of the CCSplit class.
//
// 功能: 实现VB中的Split函数,把字符串转换为数组
// 编写人: 不知道
// 编写时间: 不知道
// 修改人: jiftle
// 修改时间: 2012-02-03 15:31
// 来源: http://download.csdn.net/download/aryao/1874193
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "CSplit.h"
#include <stdio.h>
#include <stdlib.h>
#include  <memory.h>
#include <string.h> 

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CCSplit::CCSplit()
{
	aryData=NULL;
	aryLen=0;
}

CCSplit::~CCSplit()
{
	FreeAry();
}

int CCSplit::toSplit(char *str, char splitchar)
{
	if (str!=NULL)
	{
		FreeAry();
		int i;
		char *ps,*pe,*tmp;
		if(strlen(str)==0)
			return 0;
		
		pe=str;
		ps=str;
		aryLen=0;
		while( (pe=strchr(pe,splitchar))>0 )
		{aryLen++;
		pe++;
		ps=pe;
		}
		if(strlen(ps)!=0)
			aryLen++;
		aryData=(char **)malloc(sizeof(char *)*(aryLen));
		if(aryLen==1)
		{
			aryData[0]=(char *)malloc(strlen(str)+1);
			strncpy(aryData[0],str,strlen(str));
			aryData[0][strlen(str)]='\0';
		}
		else
		{
			pe=(char *)str;
			ps=(char *)str;
			i=0;
			while( (pe=strchr(pe,splitchar))>0 )
			{
				aryData[i]=(char *)malloc(pe-ps+1);
				memset(aryData[i],0,pe-ps+1);
				strncpy(aryData[i],ps,pe-ps);
				//p[i][pe-ps+1]='\0';
				pe++;
				ps=pe;
				i++;
			}
			int iTmp;
			iTmp=strlen(ps);
			if(iTmp>0)
			{
				aryData[i]=(char *)malloc(iTmp+1);
				memset(aryData[i],0,iTmp+1);
				strncpy(aryData[i],ps,iTmp);
			}
		}
	}
	return aryLen;
}

void CCSplit::FreeAry()
{
	if (aryLen>0 && aryData!=NULL)
	{
		int i;//
		for(i=0;i<aryLen;i++)
		{
			free(aryData[i]);
		}
		free(aryData);
		aryData=NULL;
		aryLen=0;
		printf("释放内存资源完成\n");
	}
}

char * CCSplit::GetData(int iAdr)
{
	if (iAdr>=1 && iAdr<=aryLen)
	{
		return aryData[iAdr-1];
	}
	else
		return NULL;
}
