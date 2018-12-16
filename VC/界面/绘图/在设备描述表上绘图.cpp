/************************************************************************/
/* 功能： 在设备描述表上绘图                          		                              */
/************************************************************************/

/** 在设备描述表上绘图
 * @param pDC  
 *	DC的指针。
 * @param IDB_PIC
 *	图片资源的ID。
 * @param rect 
 *	绘制区域 pain area。
 * @note 
 *	在OnPaint()中调用即可。
 */
bool PaintPic(CDC* pDC,UINT IDB_PIC,CRect rect)
{
	if (pDC== NULL) return false;
	CBitmap	bitmap;
	if(!bitmap.LoadBitmap(IDB_PIC))
		return false;

	CDC mem_dc;
	if(!mem_dc.CreateCompatibleDC(pDC))
		return false;
	mem_dc.SelectObject(&bitmap);
	
	//获得Bitmap的大小
	BITMAP mp;
	if(!bitmap.GetBitmap(&mp))
		return false;

	//pDC->BitBlt(0,0,rect.Width(),rect.Height(),&mem_dc,0,0,SRCCOPY);
	pDC->StretchBlt(0,0,rect.Width(),rect.Height(),&mem_dc,0,0,mp.bmWidth,mp.bmHeight,SRCCOPY);

	bitmap.DeleteObject();

	return true;
}
