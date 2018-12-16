/*	CreateLink(CSIDL_DESKTOP,"我的浏览器",
			"C:\\Program Files\\Internet Explorer\\iexplore.exe",
			"C:\\Program Files\\Internet Explorer\\",
			"www.baidu.com");
		RemoveLink(CSIDL_DESKTOP,"我的浏览器");
*/
		
/** 创建快捷方式 
 *@param int nFolder 
 *	A CSIDL value that identifies the folder of interest. 
 *@param LPCTSTR strShortcutName
 *	快捷方式名字
 *@param LPCTSTR strFileName 
 *	可执行文件名全路径
 *@param LPCTSTR strAppPath
 *	起始位置
 *@param LPCTSTR arg
 *	参数
 *@return True 成功, False 失败; 
 *@note 调用格式:
 * 	CreateLink(CSIDL_DESKTOP,"我的浏览器",
 *   "C:\\Program Files\\Internet Explorer\\iexplore.exe",
 *   "C:\\Program Files\\Internet Explorer\\",
 *   "www.baidu.com");
 */
BOOL CreateLink(int nFolder, LPCTSTR strShortcutName, 
							 LPCTSTR strFileName, LPCTSTR strAppPath,
							 LPCTSTR arg)
{
	CoInitialize(NULL);

	//File system directory that contains the directories for the
	//common program groups that appear on the Start menu for all
	// users.
	LPITEMIDLIST pidl;

	// Get a pointer to an item ID list that represents the path
	// of a special folder
	// 获得一个ID列表项的指针表示特定文件夹的路径
	HRESULT hr = SHGetSpecialFolderLocation(NULL, nFolder, &pidl);

	// Convert the item ID list's binary representation into a file
	// system path
	// ID转换为路径
	char szPath[_MAX_PATH];
	BOOL f = SHGetPathFromIDList(pidl, szPath);

	// Allocate a pointer to an IMalloc interface
	// 分配一个指向IMalloc接口的指针
	LPMALLOC pMalloc;

	// Get the address of our task allocator's IMalloc interface
	hr = SHGetMalloc(&pMalloc);

	// Free the item ID list allocated by SHGetSpecialFolderLocation
	pMalloc->Free(pidl);

	// Free our task allocator
	pMalloc->Release();

	CString szLinkName = strShortcutName;
	szLinkName += _T(".lnk") ;

	CString szTemp = szLinkName;
	szLinkName.Format( "%s\\%s", szPath, szTemp);


	HRESULT hres = NULL;
	IShellLink* psl = NULL;

	// Get a pointer to the IShellLink interface.
	hres = CoCreateInstance(CLSID_ShellLink, NULL,
		CLSCTX_INPROC_SERVER, IID_IShellLink,
		reinterpret_cast<void**>(&psl));
	if(SUCCEEDED(hres))
	{ 
		IPersistFile* ppf = NULL;

		// Set the path to the shortcut target		
		psl->SetPath(strFileName);		
		if(arg)
			psl->SetArguments(arg);
		psl->SetWorkingDirectory(strAppPath);

		// Query IShellLink for the IPersistFile interface for
		// saving the shortcut in persistent storage.
		hres = psl->QueryInterface(IID_IPersistFile,
			reinterpret_cast<void**>(&ppf));

		if (SUCCEEDED(hres))
		{
			WCHAR wsz[MAX_PATH]={0};

			// Ensure that the string is WideChar.
			// 确保字符串是UNiCODE
			MultiByteToWideChar(CP_ACP, 0, szLinkName, -1,
				wsz, MAX_PATH);

			// Save the link by calling IPersistFile::Save.
			hres = ppf->Save(wsz, TRUE);
			ppf->Release();
		}
		psl->Release();

		return TRUE;
	}

	return FALSE;
}

/** 删除快捷方式 
 *@param int nFolder
 *    特殊文件夹的id.A CSIDL value that identifies the folder of interest.
 *@param LPCTSTR szShortcut 快捷方式的名字
 *@note 例子:RemoveLink(CSIDL_DESKTOP,"我的浏览器");
 */

BOOL RemoveLink(int nFolder, LPCTSTR szShortcut)
{
	CoInitialize(NULL);

	//File system directory that contains the directories for the
	//common program groups that appear on the Start menu for all
	// users.
	LPITEMIDLIST pidl;

	// Get a pointer to an item ID list that represents the path
	// of a special folder
	HRESULT hr = SHGetSpecialFolderLocation(NULL, nFolder, &pidl);

	// Convert the item ID list's binary representation into a file
	// system path
	char szPath[_MAX_PATH];
	BOOL f = SHGetPathFromIDList(pidl, szPath);

	// Allocate a pointer to an IMalloc interface
	LPMALLOC pMalloc;

	// Get the address of our task allocator's IMalloc interface
	hr = SHGetMalloc(&pMalloc);

	// Free the item ID list allocated by SHGetSpecialFolderLocation
	pMalloc->Free(pidl);

	// Free our task allocator
	pMalloc->Release();

	CString szLinkName = szShortcut;
	szLinkName += _T(".lnk") ;

	CString szTemp = szLinkName;
	szLinkName.Format( "%s\\%s", szPath, szTemp);

	DeleteFile(szLinkName);

	CoUninitialize();

	return TRUE;
}

