//===============================================================
//����:
//		��õ�ǰʱ��
//����:
//		��
//����ֵ:
//		CString 
//��ע:
//===============================================================
CString GetCurTime()
{
	CString strTime;
	COleDateTime oleTime;
	
	oleTime = COleDateTime::GetCurrentTime();
	strTime = oleTime.Format("%Y-%m-%d %H:%M:%S");
	
	return strTime;
}