
//=======================================================
// 功能： 从文件中加载图片，通过CBitmap的指针 来操作图片COM
//=======================================================
void LoadPictureFile(HDC hdc, LPCTSTR szFile, CBitmap* pBitmap, CSize& mSize) 
{   
	// open file    
	HANDLE hFile = CreateFile(szFile, GENERIC_READ, 0, NULL, OPEN_EXISTING, 0, NULL);   
	_ASSERTE(INVALID_HANDLE_VALUE != hFile);   
	
	// get file size    
	DWORD dwFileSize = GetFileSize(hFile, NULL);   
	_ASSERTE(-1 != dwFileSize);   
	
	LPVOID pvData = NULL;   
	// alloc memory based on file size    
	HGLOBAL hGlobal = GlobalAlloc(GMEM_MOVEABLE, dwFileSize);   
	_ASSERTE(NULL != hGlobal);   
	
	pvData = GlobalLock(hGlobal);   
	_ASSERTE(NULL != pvData);   
	
	DWORD dwBytesRead = 0;   
	// read file and store in global memory    
	BOOL bRead = ReadFile(hFile, pvData, dwFileSize, &dwBytesRead, NULL);   
	_ASSERTE(FALSE != bRead);   
	GlobalUnlock(hGlobal);   
	CloseHandle(hFile);   
	
	LPSTREAM pstm = NULL;   
	// create IStream* from global memory    
	HRESULT hr = CreateStreamOnHGlobal(hGlobal, TRUE, &pstm);   
	_ASSERTE(SUCCEEDED(hr) && pstm);   
	
	// Create IPicture from image file    
	LPPICTURE gpPicture;   
	
	hr = ::OleLoadPicture(pstm, dwFileSize, FALSE, IID_IPicture, (LPVOID *)&gpPicture);   
	_ASSERTE(SUCCEEDED(hr) && gpPicture);      
	pstm->Release();   
	
	OLE_HANDLE m_picHandle;   
	
	gpPicture->get_Handle(&m_picHandle);   
	pBitmap->DeleteObject();   
	pBitmap->Attach((HGDIOBJ) m_picHandle);   
	
	BITMAP bm;   
	GetObject(pBitmap->m_hObject, sizeof(bm), &bm);   
	mSize.cx = bm.bmWidth; //nWidth;    
	mSize.cy = bm.bmHeight; //nHeight;    
}   
