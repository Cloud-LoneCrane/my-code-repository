   /// <summary>
        /// �г�ϵͳ�Ѱ�װ�����д�ӡ��
        /// </summary>
        void Printers_Print()
        {
            //�г����д�ӡ��
            string strTmp = "";
            int i = 1;

            Console.WriteLine();
            Console.WriteLine(strTmp.PadLeft(50, '-'));
            Console.WriteLine("ϵͳ�Ѱ�װ�Ĵ�ӡ��(��" + System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count + 
            "��):");
            Console.WriteLine(strTmp.PadLeft(50, '-'));

         
            foreach (string strPrinterName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                    Console.WriteLine("��" + i + "����ӡ������" +  strPrinterName);
                    i++;
                                                  
                    Console.WriteLine(strTmp.PadLeft(50, '-'));
                    System.Drawing.Printing.PrinterSettings prtTmp= new System.Drawing.Printing.PrinterSettings();
                    prtTmp.PrinterName = strPrinterName;
                    if(prtTmp.IsValid)
                    {
                        Console.WriteLine("֧�ֵķֱ��ʣ�");          
                        foreach(System.Drawing.Printing.PrinterResolution Resolution in prtTmp.PrinterResolutions)
                        {
                            Console.WriteLine("\t" + Resolution.ToString());
                        }

                        Console.WriteLine("֧�ֵĴ�ӡֽ�ߴ磺");
                        foreach (System.Drawing.Printing.PaperSize size in prtTmp.PaperSizes)
                        {
                            Console.WriteLine("\t" + size.ToString());
                        }

                    }  
                 }


            Console.WriteLine(strTmp.PadLeft(50, '-'));
            Console.WriteLine();
        }