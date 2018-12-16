// CSplit.h: interface for the CCSplit class.
//
// 功能: 实现VB中的Split函数,把字符串转换为数组
// 编写人: 不知道
// 编写时间: 不知道
// 修改人: jiftle
// 修改时间: 2012-02-03 15:31
// 来源: http://download.csdn.net/download/aryao/1874193
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_CSPLIT_H__9B0A0E0C_A2B5_424A_A484_047204D75418__INCLUDED_)
#define AFX_CSPLIT_H__9B0A0E0C_A2B5_424A_A484_047204D75418__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CCSplit  
{
public:
	char * GetData(int iAdr);
	void FreeAry();
	int toSplit(char *str,char splitchar);
	CCSplit();
	virtual ~CCSplit();
private:
	int aryLen;
	char **aryData;
};

#endif // !defined(AFX_CSPLIT_H__9B0A0E0C_A2B5_424A_A484_047204D75418__INCLUDED_)
