/************************************************************************/
/* ���ܣ� ���豸�������ϻ�ͼ                          		                              */
/************************************************************************/

/** ���豸�������ϻ�ͼ
 * @param pDC  
 *	DC��ָ�롣
 * @param IDB_PIC
 *	ͼƬ��Դ��ID��
 * @param rect 
 *	�������� pain area��
 * @note 
 *	��OnPaint()�е��ü��ɡ�
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
	
	//���Bitmap�Ĵ�С
	BITMAP mp;
	if(!bitmap.GetBitmap(&mp))
		return false;

	//pDC->BitBlt(0,0,rect.Width(),rect.Height(),&mem_dc,0,0,SRCCOPY);
	pDC->StretchBlt(0,0,rect.Width(),rect.Height(),&mem_dc,0,0,mp.bmWidth,mp.bmHeight,SRCCOPY);

	bitmap.DeleteObject();

	return true;
}
