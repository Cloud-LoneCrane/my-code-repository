        /// <summary>
        /// ���md5��ϣֵ
        /// </summary>
        /// <param name="md5Hash">md5���ʵ��</param>
        /// <param name="input">�����ַ���</param>
        /// <returns>md5ֵ</returns>
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            //ת�������ַ������ֽ���������ϣֵ
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            // ����StringBuilder�ռ��ֽڴ���һ���ַ���
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            // ѭ����ϣֵ��ÿһ���ֽڣ���ʽ��Ϊʮ�������ַ���
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2").ToUpper());//׷��
            }

            // Return the hexadecimal string.
            // ����ʮ�������ַ���
            return sBuilder.ToString();
        }

       

        // Verify a hash against a string.
        static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

  //---------------------------------------------------------------
//example:
            string source = "Hello World!";
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, source);

                Console.WriteLine("The MD5 hash of " + source + " is: " + hash + ".");

                Console.WriteLine("Verifying the hash...");

                if (VerifyMd5Hash(md5Hash, source, hash))
                {
                    Console.WriteLine("The hashes are the same.");
                }
                else
                {
                    Console.WriteLine("The hashes are not same.");
                }
            }
//-------------------------------------------------------------------