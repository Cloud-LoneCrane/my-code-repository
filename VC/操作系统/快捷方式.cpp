/*	CreateLink(CSIDL_DESKTOP,"�ҵ������",
			"C:\\Program Files\\Internet Explorer\\iexplore.exe",
			"C:\\Program Files\\Internet Explorer\\",
			"www.baidu.com");
		RemoveLink(CSIDL_DESKTOP,"�ҵ������");
*/
		
/** ������ݷ�ʽ 
 *@param int nFolder 
 *	A CSIDL value that identifies the folder of interest. 
 *@param LPCTSTR strShortcutName
 *	��ݷ�ʽ����
 *@param LPCTSTR strFileName 
 *	��ִ���ļ���ȫ·��
 *@param LPCTSTR strAppPath
 *	��ʼλ��
 *@param LPCTSTR arg
 *	����
 *@return True �ɹ�, False ʧ��; 
 *@note ���ø�ʽ:
 * 	CreateLink(CSIDL_DESKTOP,"�ҵ������",
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
	// ���һ��ID�б����ָ���ʾ�ض��ļ��е�·��
	HRESULT hr = SHGetSpecialFolderLocation(NULL, nFolder, &pidl);

	// Convert the item ID list's binary representation into a file
	// system path
	// IDת��Ϊ·��
	char szPath[_MAX_PATH];
	BOOL f = SHGetPathFromIDList(pidl, szPath);

	// Allocate a pointer to an IMalloc interface
	// ����һ��ָ��IMalloc�ӿڵ�ָ��
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
			// ȷ���ַ�����UNiCODE
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

/** ɾ����ݷ�ʽ 
 *@param int nFolder
 *    �����ļ��е�id.A CSIDL value that identifies the folder of interest.
 *@param LPCTSTR szShortcut ��ݷ�ʽ������
 *@note ����:RemoveLink(CSIDL_DESKTOP,"�ҵ������");
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

