/************************************************************************
 * 功能: 绘制3D边框  
 * 编写人: jiftle 2010-10-8 22:35
 ************************************************************************/
BOOL Draw3Dframe(CDC*pDC,int x1,int y1,int x2,int y2)
{
	CPen pen1(PS_SOLID,1,RGB(64,64,64));
	CPen pen2(PS_SOLID,1,RGB(127,127,127));
	CPen pen3(PS_SOLID,1,RGB(224,224,224));
	
	//top line
	CPen *pOldPen=pDC->SelectObject(&pen1);
	pDC->MoveTo(x1,y1+1);
	pDC->LineTo(x2,y1+1);
	
	pDC->SelectObject(&pen2);
	pDC->MoveTo(x1,y1);
	pDC->LineTo(x2,y1);
	
	pDC->SelectObject(&pen3);
	pDC->MoveTo(x1,y1-2);
	pDC->LineTo(x2,y1-2);
	
	
	//left
	pDC->SelectObject(&pen1);
	pDC->MoveTo(x1+1,y1+2);
	pDC->LineTo(x1+1,y2-2);
	
	pDC->SelectObject(&pen2);
	pDC->MoveTo(x1,y1+2);
	pDC->LineTo(x1,y2-1);
	
	pDC->SelectObject(&pen3);
	pDC->MoveTo(x1-2,y1-2);
	pDC->LineTo(x1-2,y2+1);
	
	//right
	pDC->SelectObject(&pen1);
	pDC->MoveTo(x2+1,y1);
	pDC->LineTo(x2+1,y2+1);
	
	pDC->SelectObject(&pen2);
	pDC->MoveTo(x2,y1+2);
	pDC->LineTo(x2,y2-1);
	
	pDC->SelectObject(&pen3);
	pDC->MoveTo(x2-2,y1+2);
	pDC->LineTo(x2-2,y2-1);
	
	
	//bottom
	pDC->SelectObject(&pen1);
	pDC->MoveTo(x1,y2+1);
	pDC->LineTo(x2+2,y2+1);
	
	pDC->SelectObject(&pen2);
	pDC->MoveTo(x1,y2);
	pDC->LineTo(x2,y2);
	
	pDC->SelectObject(&pen3);
	pDC->MoveTo(x1,y2-2);
	pDC->LineTo(x2,y2-2);
	
	pDC->SelectObject(pOldPen);

	return TRUE;
}
