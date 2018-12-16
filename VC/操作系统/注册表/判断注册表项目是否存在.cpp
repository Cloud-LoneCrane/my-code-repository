#include "atlbase.h"

	//判断注册表项目是否存在
	CRegKey reg;
	LONG lngRes = 0;
	lngRes = reg.Open(HKEY_LOCAL_MACHINE,
		_T("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\policies\\Explorer\\Run0"));

	if (lngRes == 2)
	{
		AfxMessageBox(_T("不存在指定的注册表项！\r\n"));
	}