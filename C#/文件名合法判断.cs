 private bool IsFileNameValid(string name)
    {
             bool isFilename = true;
            string[] errorStr =new string []{"/","\\",":",",","*","?","\"","<",">","|"};

            if (string.IsNullOrEmpty(name))
            {
                isFilename= false ;
            }
            else
            {
                for(int i=0;i<errorStr .Length ;i++)
                {
                    if(name.Contains (errorStr[i]))
                    {
                        isFilename = false;
                        break;
                    }
                }
            }
        return isFilename;
    }