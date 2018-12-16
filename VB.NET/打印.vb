'--------------------------------------------------------------------
'VB.Net实现打印机纸张类型自动更换的方法

Dim   ps   As   New   PageSettings   
Dim   pDocument   as   new   PrintDocument   
Dim   myPaperSize   As   System.Drawing.Printing.PaperSize   

If     ps.PrinterSettings.InstalledPrinters.Count   >   0   Then       '检测打印机是否存在   
	  If   ps.PrinterSettings.IsValid   =   True   Then                           '检测打印机是否有效     
			For   Each   myPaperSize   In   ps.PrinterSettings.PaperSizes     '检查该当前打印机是否支持A3的纸张   
					If   myPaperSize.Kind   =   PaperKind.A3   Then   
						  Exit   For   
					End   If   
			next   
			If myPaperSize.Kind   <>   PaperKind.A3   Then  '如果当前打印机不支持A3纸张,那么自定义A3大小的纸张   
				  myPaperSize   =   New   System.Drawing.Printing.PaperSize("A3",   1169,   1654)   
			End   If   
			ps.PaperSize   =   myPaperSize         '设置为指定的纸张   
			ps.Landscape   =   False   
			pDocument.DefaultPageSettings   =   ps   
			pDocument.PrinterSettings.PrinterName   =   ps.PrinterSettings.PrinterName   
	  End   If   
End   If