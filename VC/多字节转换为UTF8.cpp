//===============================================================
//功能: 
//	多字节转换为UTF8
//参数:
//返回值:
//备注: CP_ACP=ANSI,CP_UTF8=utf-8
//调用方法：　UTF8Convert(strSQL,CP_ACP,CP_UTF8);
//===============================================================
CString UTF8Convert(CString &str,int sourceCodepage,int targetCodepage)
{
	int len=str.GetLength(); 
	
	int unicodeLen=MultiByteToWideChar(sourceCodepage,0,str,-1,NULL,0); 
	
	wchar_t * pUnicode; 
	pUnicode=new wchar_t[unicodeLen+1]; 
	
	memset(pUnicode,0,(unicodeLen+1)*sizeof(wchar_t)); 
	
	
	MultiByteToWideChar(sourceCodepage,0,str,-1,(LPWSTR)pUnicode,unicodeLen); 
	
	BYTE * pTargetData; 
	int targetLen=WideCharToMultiByte(targetCodepage,0,(LPWSTR)pUnicode,-1,(char *)pTargetData,0,NULL,NULL); 
	
	pTargetData=new BYTE[targetLen+1]; 
	memset(pTargetData,0,targetLen+1); 
	
	WideCharToMultiByte(targetCodepage,0,(LPWSTR)pUnicode,-1,(char *)pTargetData,targetLen,NULL,NULL); 
	
	CString rt; 
	rt.Format("%s",pTargetData); 
	
	delete pUnicode; 
	delete pTargetData; 
	return rt; 
}