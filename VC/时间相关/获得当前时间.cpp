//===============================================================
//功能:
//		获得当前时间
//参数:
//		无
//返回值:
//		CString 
//备注:
//===============================================================
CString GetCurTime()
{
	CString strTime;
	COleDateTime oleTime;
	
	oleTime = COleDateTime::GetCurrentTime();
	strTime = oleTime.Format("%Y-%m-%d %H:%M:%S");
	
	return strTime;
}