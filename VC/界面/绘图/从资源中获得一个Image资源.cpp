//===============================================================
//功能: 从资源中获得一个Image资源
//参数: 
//	[in] nID
//		资源ID
//	[in] strType
//		资源类型
//	[out] pImg
//		Image对象的指针,请注意:Image * &pImg
//返回值:
//	成功,返回TRUE;失败,返回FALSE;
//备注:
//	GDI+中才会使用
//===============================================================
BOOL CGDIPClockDlg::ImageFromIDResource(UINT nID, LPCTSTR strType,Image * &pImg)
{
	HINSTANCE hInst = AfxGetResourceHandle();
	HRSRC hRsrc = ::FindResource (hInst,MAKEINTRESOURCE(nID),sTR); // type
	if (!hRsrc)
		return FALSE;

	// load resource into memory
	DWORD len = SizeofResource(hInst, hRsrc);
	BYTE* lpRsrc = (BYTE*)LoadResource(hInst, hRsrc);
	if (!lpRsrc)
		return FALSE;

	// Allocate global memory on which to create stream
	HGLOBAL m_hMem = GlobalAlloc(GMEM_FIXED, len);
	BYTE* pmem = (BYTE*)GlobalLock(m_hMem);
	memcpy(pmem,lpRsrc,len);
	IStream* pstm;
	CreateStreamOnHGlobal(m_hMem,FALSE,&pstm);
	
	// load from stream
	pImg=Gdiplus::Image::FromStream(pstm);

	// free/release stuff
	GlobalUnlock(m_hMem);
	pstm->Release();
	FreeResource(lpRsrc);

}