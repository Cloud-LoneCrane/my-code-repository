   /// <summary>
        /// 列出系统已安装的所有打印机
        /// </summary>
        void Printers_Print()
        {
            //列出所有打印机
            string strTmp = "";
            int i = 1;

            Console.WriteLine();
            Console.WriteLine(strTmp.PadLeft(50, '-'));
            Console.WriteLine("系统已安装的打印机(共" + System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count + 
            "个):");
            Console.WriteLine(strTmp.PadLeft(50, '-'));

         
            foreach (string strPrinterName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                    Console.WriteLine("第" + i + "个打印机名：" +  strPrinterName);
                    i++;
                                                  
                    Console.WriteLine(strTmp.PadLeft(50, '-'));
                    System.Drawing.Printing.PrinterSettings prtTmp= new System.Drawing.Printing.PrinterSettings();
                    prtTmp.PrinterName = strPrinterName;
                    if(prtTmp.IsValid)
                    {
                        Console.WriteLine("支持的分辨率：");          
                        foreach(System.Drawing.Printing.PrinterResolution Resolution in prtTmp.PrinterResolutions)
                        {
                            Console.WriteLine("\t" + Resolution.ToString());
                        }

                        Console.WriteLine("支持的打印纸尺寸：");
                        foreach (System.Drawing.Printing.PaperSize size in prtTmp.PaperSizes)
                        {
                            Console.WriteLine("\t" + size.ToString());
                        }

                    }  
                 }


            Console.WriteLine(strTmp.PadLeft(50, '-'));
            Console.WriteLine();
        }