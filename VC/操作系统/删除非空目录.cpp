//////////////////////////////////////////////////////////////////////////   
//   DeleteDirectory 
//   删除一个文件夹   
//   lpszPath   -   要删除的文件夹路径   
//   返回值：成功返回TRUE，否则返回FALSE   
//   备注：亦可用来删除单个文件,目录最后不能带\
//   eg.
//DeleteDirectory(_T("c:\\vc.txt"));   正确
//DeleteDirectory(_T("c:\\vc"));        正确
//DeleteDirectory(_T("c:\\vc\\"));     错误
//////////////////////////////////////////////////////////////////////////   
BOOL DeleteDirectory(LPCTSTR lpszPath) 
{ 
    SHFILEOPSTRUCT FileOp; 
    FileOp.fFlags = FOF_NOCONFIRMATION|FOF_NOERRORUI ;  //删除失败时，不显示错误提示
    //FileOp.fFlags = FOF_NOCONFIRMATION ; 
	FileOp.hNameMappings = NULL; 
    FileOp.hwnd = NULL; 
    FileOp.lpszProgressTitle = NULL; 
    FileOp.pFrom = lpszPath; 
    FileOp.pTo = NULL; 
    FileOp.wFunc = FO_DELETE; 
    return SHFileOperation(&FileOp) == 0; 
}