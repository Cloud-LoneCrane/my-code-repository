
//用于没有掩码图,只有指定透明色,不进行伸缩
void DrawTransBitmap( HDC hdcDest,      // 目标DC
							  int nXOriginDest,   // 目标X偏移
							  int nYOriginDest,   // 目标Y偏移
							  int nWidthDest,     // 目标宽度
							  int nHeightDest,    // 目标高度
							  HDC hdcSrc,         // 源DC
							  int nXOriginSrc,    // 源X起点
							  int nYOriginSrc,    // 源Y起点
							  COLORREF crTransparent  // 透明色,COLORREF类型
							  )
{
								  HBITMAP hOldImageBMP, hImageBMP = CreateCompatibleBitmap(hdcDest, nWidthDest, nHeightDest);    // 创建兼容位图
								  HBITMAP hOldMaskBMP, hMaskBMP = CreateBitmap(nWidthDest, nHeightDest, 1, 1, NULL);            // 创建单色掩码位图
								  HDC        hImageDC = CreateCompatibleDC(hdcDest);//临时DC 
								  HDC        hMaskDC = CreateCompatibleDC(hdcDest);//临时掩码DC 
								  hOldImageBMP = (HBITMAP)SelectObject(hImageDC, hImageBMP);
								  hOldMaskBMP = (HBITMAP)SelectObject(hMaskDC, hMaskBMP);

								  // 将源DC中的位图拷贝到临时DC中,源DC已经载入位图
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, SRCCOPY);

								  // 设置临时DC的透明色
								  SetBkColor(hImageDC, crTransparent);

								  // 生成透明区域为白色，其它区域为黑色的临时掩码DC的掩码位图
								  // 位图来自临时DC
								  BitBlt(hMaskDC, 0, 0, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCCOPY);

								  // 生成透明区域为黑色，其它区域保持不变的位图
								  SetBkColor(hImageDC, RGB(0,0,0));
								  SetTextColor(hImageDC, RGB(255,255,255));
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // 透明部分保持屏幕不变，其它部分变成黑色
								  SetBkColor(hdcDest,RGB(255,255,255));
								  SetTextColor(hdcDest,RGB(0,0,0));
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // "或"运算,生成最终效果
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCPAINT);

								  // 清理、恢复    
								  SelectObject(hImageDC, hOldImageBMP);
								  DeleteDC(hImageDC);
								  SelectObject(hMaskDC, hOldMaskBMP);
								  DeleteDC(hMaskDC);
								  DeleteObject(hImageBMP);
								  DeleteObject(hMaskBMP);
} 


//用于没有掩码图,只有指定透明色，可以进行伸缩
void DrawTransBitmap( HDC hdcDest,      // 目标DC
							  int nXOriginDest,   // 目标X偏移
							  int nYOriginDest,   // 目标Y偏移
							  int nWidthDest,     // 目标宽度
							  int nHeightDest,    // 目标高度
							  HDC hdcSrc,         // 源DC
							  int nXOriginSrc,    // 源X起点
							  int nYOriginSrc,    // 源Y起点
							  int nWidthSrc,      // 源宽度
							  int nHeightSrc,     // 源高度
							  COLORREF crTransparent  // 透明色,COLORREF类型
							  )
{
								  HBITMAP hOldImageBMP, hImageBMP = CreateCompatibleBitmap(hdcDest, nWidthDest, nHeightDest);    // 创建兼容位图
								  HBITMAP hOldMaskBMP, hMaskBMP = CreateBitmap(nWidthDest, nHeightDest, 1, 1, NULL);            // 创建单色掩码位图
								  HDC        hImageDC = CreateCompatibleDC(hdcDest);
								  HDC        hMaskDC = CreateCompatibleDC(hdcDest);
								  hOldImageBMP = (HBITMAP)SelectObject(hImageDC, hImageBMP);
								  hOldMaskBMP = (HBITMAP)SelectObject(hMaskDC, hMaskBMP);

								  // 将源DC中的位图拷贝到临时DC中
								  if (nWidthDest == nWidthSrc && nHeightDest == nHeightSrc)
									 {
										  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, SRCCOPY);
								  }
								  else
									 {
										  StretchBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, 
											  hdcSrc, nXOriginSrc, nYOriginSrc, nWidthSrc, nHeightSrc, SRCCOPY);
								  }

								  // 设置透明色
								  SetBkColor(hImageDC, crTransparent);

								  // 生成透明区域为白色，其它区域为黑色的掩码位图
								  BitBlt(hMaskDC, 0, 0, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCCOPY);

								  // 生成透明区域为黑色，其它区域保持不变的位图
								  SetBkColor(hImageDC, RGB(0,0,0));
								  SetTextColor(hImageDC, RGB(255,255,255));
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // 透明部分保持屏幕不变，其它部分变成黑色
								  SetBkColor(hdcDest,RGB(0xff,0xff,0xff));
								  SetTextColor(hdcDest,RGB(0,0,0));
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // "或"运算,生成最终效果
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCPAINT);

								  SelectObject(hImageDC, hOldImageBMP);
								  DeleteDC(hImageDC);
								  SelectObject(hMaskDC, hOldMaskBMP);
								  DeleteDC(hMaskDC);
								  DeleteObject(hImageBMP);
								  DeleteObject(hMaskBMP);

}


//指定掩码图,和掩码图属于不同图片
void DrawTransBitmap( HDC hdcDest,      // 目标DC
							  int nXOriginDest,   // 目标X偏移
							  int nYOriginDest,   // 目标Y偏移
							  int nWidthDest,     // 目标宽度
							  int nHeightDest,    // 目标高度
							  HDC hdcSrc,         // 源DC
							  HDC hdcMask,
							  int nXOriginSrc,    // 源X起点
							  int nYOriginSrc,    // 源Y起点
							  COLORREF crTransparent  // 透明色,COLORREF类型
							  )
{

								  HBITMAP hOldImageBMP, hImageBMP = CreateCompatibleBitmap(hdcDest, nWidthDest, nHeightDest);    // 创建兼容位图
								  HDC        hImageDC = CreateCompatibleDC(hdcDest);//临时DC 
								  hOldImageBMP = (HBITMAP)SelectObject(hImageDC, hImageBMP);

								  // 将源DC中的位图拷贝到临时DC中,源DC已经载入位图
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, SRCCOPY);
								  // 设置临时DC的透明色
								  SetBkColor(hImageDC, crTransparent);
								  // 生成透明区域为黑色，其它区域保持不变的位图
								  SetBkColor(hImageDC, RGB(0,0,0));
								  SetTextColor(hImageDC, RGB(255,255,255));
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcMask, 0, 0, SRCAND);
								  // 透明部分保持屏幕不变，其它部分变成黑色
								  SetBkColor(hdcDest,RGB(255,255,255));
								  SetTextColor(hdcDest,RGB(0,0,0));
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hdcMask, 0, 0, SRCAND);
								  // "或"运算,生成最终效果
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCPAINT);
								  // 清理、恢复    
								  SelectObject(hImageDC, hOldImageBMP);
								  DeleteDC(hImageDC);
								  DeleteObject(hImageBMP);
} 

//指定图片和掩码图同属于一张图片
void DrawTransBitmap(HDC hDC, int nPosX, int nPosY, int nCX, int nCY, HBITMAP hObj)
{
	HDC hMemDC= CreateCompatibleDC(hDC);
	HBITMAP hOldBMP=(HBITMAP)::SelectObject(hMemDC,hObj);
	BitBlt(hDC,nPosX,nPosY,nCX,nCY,    hMemDC,nCX,0,SRCAND);
	BitBlt(hDC,nPosX,nPosY,nCX,nCY,    hMemDC,0,0,SRCPAINT);
	SelectObject(hMemDC,hOldBMP);
	DeleteDC(hMemDC);

}  

HRGN CreateBitmapRgn(int nWidth,int nHeight,HBITMAP hbmp, COLORREF TransColor)
{

	HDC  hmemDC;
	//创建与传入DC兼容的临时DC
	hmemDC = ::CreateCompatibleDC(NULL);

	HBITMAP hOldBmp = (HBITMAP)::SelectObject(hmemDC,hbmp);


	//创建总的窗体区域，初始region为0
	HRGN hrgn;
	hrgn = ::CreateRectRgn(0,0,0,0);


	int y;
	for(y=0;y<nHeight ;y++)
	{
			HRGN rgnTemp; //保存临时region

			int iX = 0;
			do
			{
				//跳过透明色找到下一个非透明色的点.
				while (iX < nWidth  && ::GetPixel(hmemDC,iX, y) == TransColor)
					iX++;

				//记住这个起始点
				int iLeftX = iX;

				//寻找下个透明色的点
				while (iX < nWidth  && ::GetPixel(hmemDC,iX, y) != TransColor)
					++iX;

				//创建一个包含起点与重点间高为1像素的临时“region”
				rgnTemp=::CreateRectRgn(iLeftX, y, iX, y+1);

				//合并到主"region".
				CombineRgn( hrgn,hrgn,rgnTemp, RGN_OR);

				//删除临时"region",否则下次创建时和出错
				::DeleteObject(rgnTemp);
			}while(iX <nWidth );
			iX = 0;
	}


	::SelectObject(hmemDC,hOldBmp);
	::DeleteDC(hmemDC);

	return hrgn;

}
