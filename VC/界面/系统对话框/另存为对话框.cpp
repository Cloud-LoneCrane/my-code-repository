//////////////////////////////////////////////////////////////////
//	���Ϊ�Ի��� 
//  remark:
//	GetOpenFileName �򿪶Ի���
//////////////////////////////////////////////////////////////////
	OPENFILENAME ofn;       // �����Ի���ṹ��
    TCHAR szFile[MAX_PATH]; // �����ȡ�ļ����ƵĻ�������           
 
	// ��ʼ��ѡ���ļ��Ի���
	ZeroMemory(&ofn, sizeof(ofn));
	ofn.lStructSize = sizeof(ofn);
	ofn.hwndOwner = m_hWnd;
	ofn.lpstrFile = szFile;

	//
	ofn.lpstrFile[0] = _T('\0');
	ofn.nMaxFile = sizeof(szFile);
	ofn.lpstrFilter = _T("All\0*.*\0Text\0*.TXT\0");
	ofn.nFilterIndex = 1;
	ofn.lpstrFileTitle = NULL;
	ofn.nMaxFileTitle = 0;
	ofn.lpstrInitialDir = NULL;
	ofn.Flags = OFN_SHOWHELP | OFN_OVERWRITEPROMPT;

   // ��ʾ��ѡ���ļ��Ի���
   if (GetSaveFileName(&ofn) )
   {
           //��ʾѡ����ļ���
           OutputDebugString(szFile);
           OutputDebugString(_T("\r\n"));
		   TRACE("%s",szFile);
    }