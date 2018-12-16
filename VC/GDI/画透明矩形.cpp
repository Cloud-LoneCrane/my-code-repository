
//===============================================================
//����:
//	��͸������
//����:
//	CDC
//		Ŀ��DC
//	CRect
//		��������
//	COLORREF
//		���ε������ɫ
//	BYTE
//		͸����(0-255)
//����ֵ:
//	void
//===============================================================
void DrawAlphaRect(CDC *pDC,CRect& r,COLORREF clr,BYTE alpha) 
{ 
	CDC   memdc; 
	memdc.CreateCompatibleDC(pDC); 
	CBitmap		bmp,*pOldBitmap; 
	bmp.CreateCompatibleBitmap(pDC,r.Width(),r.Height()); 
	pOldBitmap = memdc.SelectObject(&bmp); 
	memdc.FillSolidRect(0,0,r.Width(),r.Height(),clr); 
	BLENDFUNCTION bf; 
	bf.BlendOp = AC_SRC_OVER; 
	bf.BlendFlags =   0; 
	bf.SourceConstantAlpha = alpha; 
	bf.AlphaFormat = 0; 
	pDC-> AlphaBlend(r.left,r.top,r.Width(),r.Height(),&memdc,0,0,r.Width(),r.Height(),bf); 
	memdc.SelectObject(pOldBitmap); 
} 
