#include "atlbase.h"

	//�ж�ע�����Ŀ�Ƿ����
	CRegKey reg;
	LONG lngRes = 0;
	lngRes = reg.Open(HKEY_LOCAL_MACHINE,
		_T("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\policies\\Explorer\\Run0"));

	if (lngRes == 2)
	{
		AfxMessageBox(_T("������ָ����ע����\r\n"));
	}