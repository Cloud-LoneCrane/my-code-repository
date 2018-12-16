
//===============================================================
//功能:
//	画透明矩形
//参数:
//	CDC
//		目标DC
//	CRect
//		矩形区域
//	COLORREF
//		矩形的填充颜色
//	BYTE
//		透明度(0-255)
//返回值:
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
