

//////////////////////////////////////////////////////////////////////////   
//   DeleteDirectory 
//   删除文件夹   
//   lpszPath   -   要删除的文件夹路径   
//   返回值：成功返回TRUE，否则返回FALSE   
//   备注：亦可用来删除单个文件   
//////////////////////////////////////////////////////////////////////////   
BOOL DeleteDirectory(LPCTSTR lpszPath) 
{ 
	char szBuf[1024]={0};
	strcpy(szBuf,lpszPath);

    SHFILEOPSTRUCT FileOp; 
    FileOp.fFlags = FOF_NOCONFIRMATION|FOF_NOERRORUI ;  //删除失败时，不显示错误提示
    //FileOp.fFlags = FOF_NOCONFIRMATION ; 
	FileOp.hNameMappings = NULL; 
    FileOp.hwnd = NULL; 
    FileOp.lpszProgressTitle = NULL; 
    FileOp.pFrom = szBuf; //lpszPath; //要以\0\0结尾，否则出错
    FileOp.pTo = NULL; 
    FileOp.wFunc = FO_DELETE; 
    return SHFileOperation(&FileOp) == 0; 
}