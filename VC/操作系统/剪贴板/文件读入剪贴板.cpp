
//===================================================================
// ���� �����ļ����ݶ��뵽��������
// ע�⣺�ļ��ı������ΪANSI������CFile.Read����
//		��ȷ�ض�ȡ
//===================================================================
void ReadToClip(CString strFile)
{
	//���ļ�
	CFile file;
	if(!file.Open(strFile,CFile::modeRead))
	{
		CString strCaption;
		GetWindowText(strCaption);
		MessageBox(_T("Can't open the file."),strCaption,MB_OK|MB_ICONINFORMATION);
		return;
	}

	int iFileLenght=file.GetLength();
	
	//���ļ�����д�뵽������
	TCHAR*	buf= new TCHAR[iFileLenght/sizeof(TCHAR)];
	memset(buf,0,iFileLenght/sizeof(TCHAR));
 	file.Read(buf,iFileLenght/sizeof(TCHAR));

	//�ر��ļ�
	file.Close();
	
	
  HGLOBAL   hGlobal=GlobalAlloc(GHND|GMEM_SHARE,iFileLenght/sizeof(TCHAR)+1);     //����ָ�������ڴ�   
  TCHAR   *pGlobal=(TCHAR*)GlobalLock(hGlobal);     //��ס�ڴ沢�����ָ��   
  for(UINT   i=0;i<iFileLenght/sizeof(TCHAR);i++)   
		 pGlobal[i]=buf[i];     //����д���ڴ�   

  GlobalUnlock(hGlobal);     //�����ڴ��   
  ::OpenClipboard(NULL);     //�򿪼�����   
  EmptyClipboard();     //��ռ�����   
  //SetClipboardData( CF_UNICODETEXT,hGlobal);     //��ID��������� CF_UNICODETEXT 
  SetClipboardData(CF_TEXT,hGlobal);
  CloseClipboard();     //�رռ�����

  delete []buf;
}