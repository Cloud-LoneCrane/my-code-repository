
//===================================================================
// 功能 ：将文件内容读入到剪贴板上
// 注意：文件的编码必须为ANSI，否则CFile.Read不能
//		正确地读取
//===================================================================
void ReadToClip(CString strFile)
{
	//打开文件
	CFile file;
	if(!file.Open(strFile,CFile::modeRead))
	{
		CString strCaption;
		GetWindowText(strCaption);
		MessageBox(_T("Can't open the file."),strCaption,MB_OK|MB_ICONINFORMATION);
		return;
	}

	int iFileLenght=file.GetLength();
	
	//将文件内容写入到缓冲区
	TCHAR*	buf= new TCHAR[iFileLenght/sizeof(TCHAR)];
	memset(buf,0,iFileLenght/sizeof(TCHAR));
 	file.Read(buf,iFileLenght/sizeof(TCHAR));

	//关闭文件
	file.Close();
	
	
  HGLOBAL   hGlobal=GlobalAlloc(GHND|GMEM_SHARE,iFileLenght/sizeof(TCHAR)+1);     //分配指定长度内存   
  TCHAR   *pGlobal=(TCHAR*)GlobalLock(hGlobal);     //锁住内存并获得首指针   
  for(UINT   i=0;i<iFileLenght/sizeof(TCHAR);i++)   
		 pGlobal[i]=buf[i];     //数据写入内存   

  GlobalUnlock(hGlobal);     //解锁内存块   
  ::OpenClipboard(NULL);     //打开剪贴板   
  EmptyClipboard();     //清空剪贴板   
  //SetClipboardData( CF_UNICODETEXT,hGlobal);     //将ID放入剪贴板 CF_UNICODETEXT 
  SetClipboardData(CF_TEXT,hGlobal);
  CloseClipboard();     //关闭剪贴板

  delete []buf;
}