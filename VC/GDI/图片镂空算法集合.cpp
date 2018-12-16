
//����û������ͼ,ֻ��ָ��͸��ɫ,����������
void DrawTransBitmap( HDC hdcDest,      // Ŀ��DC
							  int nXOriginDest,   // Ŀ��Xƫ��
							  int nYOriginDest,   // Ŀ��Yƫ��
							  int nWidthDest,     // Ŀ����
							  int nHeightDest,    // Ŀ��߶�
							  HDC hdcSrc,         // ԴDC
							  int nXOriginSrc,    // ԴX���
							  int nYOriginSrc,    // ԴY���
							  COLORREF crTransparent  // ͸��ɫ,COLORREF����
							  )
{
								  HBITMAP hOldImageBMP, hImageBMP = CreateCompatibleBitmap(hdcDest, nWidthDest, nHeightDest);    // ��������λͼ
								  HBITMAP hOldMaskBMP, hMaskBMP = CreateBitmap(nWidthDest, nHeightDest, 1, 1, NULL);            // ������ɫ����λͼ
								  HDC        hImageDC = CreateCompatibleDC(hdcDest);//��ʱDC 
								  HDC        hMaskDC = CreateCompatibleDC(hdcDest);//��ʱ����DC 
								  hOldImageBMP = (HBITMAP)SelectObject(hImageDC, hImageBMP);
								  hOldMaskBMP = (HBITMAP)SelectObject(hMaskDC, hMaskBMP);

								  // ��ԴDC�е�λͼ��������ʱDC��,ԴDC�Ѿ�����λͼ
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, SRCCOPY);

								  // ������ʱDC��͸��ɫ
								  SetBkColor(hImageDC, crTransparent);

								  // ����͸������Ϊ��ɫ����������Ϊ��ɫ����ʱ����DC������λͼ
								  // λͼ������ʱDC
								  BitBlt(hMaskDC, 0, 0, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCCOPY);

								  // ����͸������Ϊ��ɫ���������򱣳ֲ����λͼ
								  SetBkColor(hImageDC, RGB(0,0,0));
								  SetTextColor(hImageDC, RGB(255,255,255));
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // ͸�����ֱ�����Ļ���䣬�������ֱ�ɺ�ɫ
								  SetBkColor(hdcDest,RGB(255,255,255));
								  SetTextColor(hdcDest,RGB(0,0,0));
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // "��"����,��������Ч��
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCPAINT);

								  // �����ָ�    
								  SelectObject(hImageDC, hOldImageBMP);
								  DeleteDC(hImageDC);
								  SelectObject(hMaskDC, hOldMaskBMP);
								  DeleteDC(hMaskDC);
								  DeleteObject(hImageBMP);
								  DeleteObject(hMaskBMP);
} 


//����û������ͼ,ֻ��ָ��͸��ɫ�����Խ�������
void DrawTransBitmap( HDC hdcDest,      // Ŀ��DC
							  int nXOriginDest,   // Ŀ��Xƫ��
							  int nYOriginDest,   // Ŀ��Yƫ��
							  int nWidthDest,     // Ŀ����
							  int nHeightDest,    // Ŀ��߶�
							  HDC hdcSrc,         // ԴDC
							  int nXOriginSrc,    // ԴX���
							  int nYOriginSrc,    // ԴY���
							  int nWidthSrc,      // Դ���
							  int nHeightSrc,     // Դ�߶�
							  COLORREF crTransparent  // ͸��ɫ,COLORREF����
							  )
{
								  HBITMAP hOldImageBMP, hImageBMP = CreateCompatibleBitmap(hdcDest, nWidthDest, nHeightDest);    // ��������λͼ
								  HBITMAP hOldMaskBMP, hMaskBMP = CreateBitmap(nWidthDest, nHeightDest, 1, 1, NULL);            // ������ɫ����λͼ
								  HDC        hImageDC = CreateCompatibleDC(hdcDest);
								  HDC        hMaskDC = CreateCompatibleDC(hdcDest);
								  hOldImageBMP = (HBITMAP)SelectObject(hImageDC, hImageBMP);
								  hOldMaskBMP = (HBITMAP)SelectObject(hMaskDC, hMaskBMP);

								  // ��ԴDC�е�λͼ��������ʱDC��
								  if (nWidthDest == nWidthSrc && nHeightDest == nHeightSrc)
									 {
										  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, SRCCOPY);
								  }
								  else
									 {
										  StretchBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, 
											  hdcSrc, nXOriginSrc, nYOriginSrc, nWidthSrc, nHeightSrc, SRCCOPY);
								  }

								  // ����͸��ɫ
								  SetBkColor(hImageDC, crTransparent);

								  // ����͸������Ϊ��ɫ����������Ϊ��ɫ������λͼ
								  BitBlt(hMaskDC, 0, 0, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCCOPY);

								  // ����͸������Ϊ��ɫ���������򱣳ֲ����λͼ
								  SetBkColor(hImageDC, RGB(0,0,0));
								  SetTextColor(hImageDC, RGB(255,255,255));
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // ͸�����ֱ�����Ļ���䣬�������ֱ�ɺ�ɫ
								  SetBkColor(hdcDest,RGB(0xff,0xff,0xff));
								  SetTextColor(hdcDest,RGB(0,0,0));
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hMaskDC, 0, 0, SRCAND);

								  // "��"����,��������Ч��
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCPAINT);

								  SelectObject(hImageDC, hOldImageBMP);
								  DeleteDC(hImageDC);
								  SelectObject(hMaskDC, hOldMaskBMP);
								  DeleteDC(hMaskDC);
								  DeleteObject(hImageBMP);
								  DeleteObject(hMaskBMP);

}


//ָ������ͼ,������ͼ���ڲ�ͬͼƬ
void DrawTransBitmap( HDC hdcDest,      // Ŀ��DC
							  int nXOriginDest,   // Ŀ��Xƫ��
							  int nYOriginDest,   // Ŀ��Yƫ��
							  int nWidthDest,     // Ŀ����
							  int nHeightDest,    // Ŀ��߶�
							  HDC hdcSrc,         // ԴDC
							  HDC hdcMask,
							  int nXOriginSrc,    // ԴX���
							  int nYOriginSrc,    // ԴY���
							  COLORREF crTransparent  // ͸��ɫ,COLORREF����
							  )
{

								  HBITMAP hOldImageBMP, hImageBMP = CreateCompatibleBitmap(hdcDest, nWidthDest, nHeightDest);    // ��������λͼ
								  HDC        hImageDC = CreateCompatibleDC(hdcDest);//��ʱDC 
								  hOldImageBMP = (HBITMAP)SelectObject(hImageDC, hImageBMP);

								  // ��ԴDC�е�λͼ��������ʱDC��,ԴDC�Ѿ�����λͼ
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcSrc, nXOriginSrc, nYOriginSrc, SRCCOPY);
								  // ������ʱDC��͸��ɫ
								  SetBkColor(hImageDC, crTransparent);
								  // ����͸������Ϊ��ɫ���������򱣳ֲ����λͼ
								  SetBkColor(hImageDC, RGB(0,0,0));
								  SetTextColor(hImageDC, RGB(255,255,255));
								  BitBlt(hImageDC, 0, 0, nWidthDest, nHeightDest, hdcMask, 0, 0, SRCAND);
								  // ͸�����ֱ�����Ļ���䣬�������ֱ�ɺ�ɫ
								  SetBkColor(hdcDest,RGB(255,255,255));
								  SetTextColor(hdcDest,RGB(0,0,0));
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hdcMask, 0, 0, SRCAND);
								  // "��"����,��������Ч��
								  BitBlt(hdcDest, nXOriginDest, nYOriginDest, nWidthDest, nHeightDest, hImageDC, 0, 0, SRCPAINT);
								  // �����ָ�    
								  SelectObject(hImageDC, hOldImageBMP);
								  DeleteDC(hImageDC);
								  DeleteObject(hImageBMP);
} 

//ָ��ͼƬ������ͼͬ����һ��ͼƬ
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
	//�����봫��DC���ݵ���ʱDC
	hmemDC = ::CreateCompatibleDC(NULL);

	HBITMAP hOldBmp = (HBITMAP)::SelectObject(hmemDC,hbmp);


	//�����ܵĴ������򣬳�ʼregionΪ0
	HRGN hrgn;
	hrgn = ::CreateRectRgn(0,0,0,0);


	int y;
	for(y=0;y<nHeight ;y++)
	{
			HRGN rgnTemp; //������ʱregion

			int iX = 0;
			do
			{
				//����͸��ɫ�ҵ���һ����͸��ɫ�ĵ�.
				while (iX < nWidth  && ::GetPixel(hmemDC,iX, y) == TransColor)
					iX++;

				//��ס�����ʼ��
				int iLeftX = iX;

				//Ѱ���¸�͸��ɫ�ĵ�
				while (iX < nWidth  && ::GetPixel(hmemDC,iX, y) != TransColor)
					++iX;

				//����һ������������ص���Ϊ1���ص���ʱ��region��
				rgnTemp=::CreateRectRgn(iLeftX, y, iX, y+1);

				//�ϲ�����"region".
				CombineRgn( hrgn,hrgn,rgnTemp, RGN_OR);

				//ɾ����ʱ"region",�����´δ���ʱ�ͳ���
				::DeleteObject(rgnTemp);
			}while(iX <nWidth );
			iX = 0;
	}


	::SelectObject(hmemDC,hOldBmp);
	::DeleteDC(hmemDC);

	return hrgn;

}
