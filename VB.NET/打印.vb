'--------------------------------------------------------------------
'VB.Netʵ�ִ�ӡ��ֽ�������Զ������ķ���

Dim   ps   As   New   PageSettings   
Dim   pDocument   as   new   PrintDocument   
Dim   myPaperSize   As   System.Drawing.Printing.PaperSize   

If     ps.PrinterSettings.InstalledPrinters.Count   >   0   Then       '����ӡ���Ƿ����   
	  If   ps.PrinterSettings.IsValid   =   True   Then                           '����ӡ���Ƿ���Ч     
			For   Each   myPaperSize   In   ps.PrinterSettings.PaperSizes     '���õ�ǰ��ӡ���Ƿ�֧��A3��ֽ��   
					If   myPaperSize.Kind   =   PaperKind.A3   Then   
						  Exit   For   
					End   If   
			next   
			If myPaperSize.Kind   <>   PaperKind.A3   Then  '�����ǰ��ӡ����֧��A3ֽ��,��ô�Զ���A3��С��ֽ��   
				  myPaperSize   =   New   System.Drawing.Printing.PaperSize("A3",   1169,   1654)   
			End   If   
			ps.PaperSize   =   myPaperSize         '����Ϊָ����ֽ��   
			ps.Landscape   =   False   
			pDocument.DefaultPageSettings   =   ps   
			pDocument.PrinterSettings.PrinterName   =   ps.PrinterSettings.PrinterName   
	  End   If   
End   If