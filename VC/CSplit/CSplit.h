// CSplit.h: interface for the CCSplit class.
//
// ����: ʵ��VB�е�Split����,���ַ���ת��Ϊ����
// ��д��: ��֪��
// ��дʱ��: ��֪��
// �޸���: jiftle
// �޸�ʱ��: 2012-02-03 15:31
// ��Դ: http://download.csdn.net/download/aryao/1874193
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
