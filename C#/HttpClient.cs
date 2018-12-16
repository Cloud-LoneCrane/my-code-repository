
namespace Jiftle.Net
{
    using System.Net.Sockets;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System;

    public class HttpClient
    {
        //declares
        struct strtCookie
        {
            public string Key;
            public string Value;
        };

        string server;
        int port;
        TcpClient client;
        NetworkStream stream;

        byte[] sendBuf;
        byte[] recvBuf;
        byte[] buf;
        string strSend;
        string strRecv;
        string strHeader;//http header
        string strBody;
        int len = 0;
        int startPos = 0, endPos = 0, pos = 0;

        string strCookie = string.Empty;
        string strTmp;
        ArrayList arCookies = new ArrayList();
        bool bFindHeader = false;
        List<strtCookie> ListOfCookies;

        //构造
        public HttpClient(string strServer, int intPort)
        {
            try
            {
                server = strServer;
                port = intPort;

                client = new TcpClient(server, port);
                stream = client.GetStream();
                client.ReceiveTimeout = 3000;

                ListOfCookies = new List<strtCookie>(10);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        ~HttpClient()
        {
            try
            {
                if (client != null)
                    if (client.Connected)
                        client.Close();


            }
            catch (Exception ex)
            {

            }
        }

        public bool Get(string url)
        {
            try
            {

                for (int i = 0; i < arCookies.Count; i++)
                {
                    strCookie += arCookies[i] + "; ";
                }

                strSend = "GET " + url + " HTTP/1.1" + "\r\n" +
                            "Accept: text/html" + "\r\n" +
                            "Accept-Language: zh-cn" + "\r\n" +
                            "User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1)" + "\r\n" +
                            "Accept-Encoding: gzip, deflate" + "\r\n" +
                            "Content-Length: 0" + "\r\n" +
                            "Host: " + server + "\r\n" +
                            "Cookie: " + strCookie + "\r\n" +
                            "Pragma: no-cache" + "\r\n\r\n";
                // \r\n";

                //send data
                sendBuf = System.Text.Encoding.ASCII.GetBytes(strSend);
                stream.Write(sendBuf, 0, sendBuf.Length);

                //recive data
                //len = 1;
                bFindHeader = false;
                int recvlen = 0, countlen = 0;

                do
                {
                    recvBuf = new byte[4096];
                    len = stream.Read(recvBuf, 0, recvBuf.Length);

                    buf = new byte[len];
                    Array.Copy(recvBuf, buf, len); //copy data

                    strRecv = System.Text.Encoding.ASCII.GetString(buf);


                    if (!bFindHeader)
                    {
                        //deal with http header
                        pos = strRecv.IndexOf("\r\n\r\n");
                        if (pos != -1)
                        {
                            bFindHeader = true;
                            strHeader = strRecv.Substring(0, pos + 4);
                            Debug.Print(strHeader);

                            //get content-length 
                            //Content-Length: 130\r\n
                            int posStartContextLength = 0, posEndContextLength = 0;
                            posStartContextLength = strHeader.IndexOf("Content-Length: ");
                            if (posStartContextLength != -1)
                            {
                                posStartContextLength += "Content-Length: ".Length;
                                posEndContextLength = strHeader.IndexOf("\r\n", posStartContextLength);
                                if (posEndContextLength != -1)
                                {
                                    string strContextLength = strHeader.Substring(posStartContextLength, posEndContextLength - posStartContextLength);
                                    countlen = Convert.ToInt32(strContextLength);
                                    recvlen = strRecv.Length - strHeader.Length;
                                }
                            }

                            //get cookie
                            pos = strHeader.IndexOf("Set-Cookie: ");
                            if (pos != -1)
                            {
                                startPos = pos + "Set-Cookie: ".Length;
                                endPos = strHeader.IndexOf("\r\n", startPos);
                                if (endPos != -1)
                                {
                                    strTmp = strHeader.Substring(startPos, endPos - startPos);
                                    string[] arStr = strTmp.Split(new string[] { "; " }, StringSplitOptions.None);
                                    for (int i = 0; i < arStr.Length; i++)
                                    {
                                        if (arStr[i] != "path=/" && arStr[i].IndexOf("expires=") != 0)
                                        {
                                            arCookies.Add(arStr[i]);

                                            string[] arTmpCookie = arStr[i].Split(new char[] { '=' });

                                            if (arTmpCookie.Length > 1)
                                            {
                                                strtCookie tmp;
                                                tmp.Key = arTmpCookie[0];
                                                tmp.Value = arTmpCookie[1];
                                                bool bFindKey = false;
                                                for (int j = 0; j < ListOfCookies.Count; j++)
                                                {
                                                    if (ListOfCookies[j].Key == tmp.Key)
                                                    {
                                                        bFindKey = true;
                                                        break;
                                                    }

                                                }
                                                if (!bFindKey)
                                                    ListOfCookies.Add(tmp);

                                            }


                                        }
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        recvlen += strRecv.Length;
                    }

                } while (recvlen < countlen);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Get(string url,out string retBody)
        {
            try
            {
                retBody = string.Empty;

                for (int i = 0; i < arCookies.Count; i++)
                {
                    strCookie += arCookies[i] + "; ";
                }

                strSend = "GET " + url + " HTTP/1.1" + "\r\n" +
                            "Accept: text/html" + "\r\n" +
                            "Accept-Language: zh-cn" + "\r\n" +
                            "User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1)" + "\r\n" +
                            "Accept-Encoding: gzip, deflate" + "\r\n" +
                            "Content-Length: 0" + "\r\n" +
                            "Host: " + server + "\r\n" +
                            "Cookie: " + strCookie + "\r\n" +
                            "Pragma: no-cache" + "\r\n\r\n";
                // \r\n";

                //send data
                sendBuf = System.Text.Encoding.ASCII.GetBytes(strSend);
                stream.Write(sendBuf, 0, sendBuf.Length);

                //recive data
                //len = 1;
                bFindHeader = false;
                int recvlen = 0, countlen = 0;

                do
                {
                    recvBuf = new byte[4096];
                    len = stream.Read(recvBuf, 0, recvBuf.Length);

                    buf = new byte[len];
                    Array.Copy(recvBuf, buf, len); //copy data

                    strRecv = System.Text.Encoding.ASCII.GetString(buf);


                    if (!bFindHeader)
                    {
                        //deal with http header
                        pos = strRecv.IndexOf("\r\n\r\n");
                        if (pos != -1)
                        {
                            bFindHeader = true;
                            strHeader = strRecv.Substring(0, pos + 4);
                            Debug.Print(strHeader);

                            //get content-length 
                            //Content-Length: 130\r\n
                            int posStartContextLength = 0, posEndContextLength = 0;
                            posStartContextLength = strHeader.IndexOf("Content-Length: ");
                            if (posStartContextLength != -1)
                            {
                                posStartContextLength += "Content-Length: ".Length;
                                posEndContextLength = strHeader.IndexOf("\r\n", posStartContextLength);
                                if (posEndContextLength != -1)
                                {
                                    string strContextLength = strHeader.Substring(posStartContextLength, posEndContextLength - posStartContextLength);
                                    countlen = Convert.ToInt32(strContextLength);
                                    recvlen = strRecv.Length - strHeader.Length;
                                }
                            }

                            //get cookie
                            pos = strHeader.IndexOf("Set-Cookie: ");
                            if (pos != -1)
                            {
                                startPos = pos + "Set-Cookie: ".Length;
                                endPos = strHeader.IndexOf("\r\n", startPos);
                                if (endPos != -1)
                                {
                                    strTmp = strHeader.Substring(startPos, endPos - startPos);
                                    string[] arStr = strTmp.Split(new string[] { "; " }, StringSplitOptions.None);
                                    for (int i = 0; i < arStr.Length; i++)
                                    {
                                        if (arStr[i] != "path=/" && arStr[i].IndexOf("expires=") != 0)
                                        {
                                            arCookies.Add(arStr[i]);

                                            string[] arTmpCookie = arStr[i].Split(new char[] { '=' });

                                            if (arTmpCookie.Length > 1)
                                            {
                                                strtCookie tmp;
                                                tmp.Key = arTmpCookie[0];
                                                tmp.Value = arTmpCookie[1];
                                                bool bFindKey = false;
                                                for (int j = 0; j < ListOfCookies.Count; j++)
                                                {
                                                    if (ListOfCookies[j].Key == tmp.Key)
                                                    {
                                                        bFindKey = true;
                                                        break;
                                                    }

                                                }
                                                if (!bFindKey)
                                                    ListOfCookies.Add(tmp);

                                            }


                                        }
                                    }
                                }
                            }

                        }

                        retBody += strRecv.Substring(strHeader.Length);
                    }
                    else
                    {
                        recvlen += strRecv.Length;
                        retBody += strRecv;
                    }

                } while (recvlen < countlen);

                return true;
            }
            catch (Exception ex)
            {
                retBody = string.Empty;
                return false;
            }

        }

        public bool Post(string url, string Body)
        {
            try
            {
                for (int i = 0; i < arCookies.Count; i++)
                {
                    strCookie += arCookies[i] + "; ";
                }


                //strBody = "userid=lx01&pwd=jift&x=0&y=0";
                strBody = Body;
                strSend = "POST " + url + " HTTP/1.1" + "\r\n" +
                          "Accept: text/html" + "\r\n" +
                          "Accept-Language: zh-cn" + "\r\n" +
                          "User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1)" + "\r\n" +
                          "Accept-Encoding: gzip, deflate" + "\r\n" +
                          "Content-Type: application/x-www-form-urlencoded" + "\r\n" +
                          "Content-Length: " + strBody.Length.ToString() + "\r\n" +
                          "Pragma: no-cache" + "\r\n" +
                            "Host: " + server + "\r\n" +
                    //"Host: work.u7pk.com" + "\r\n" +
                          "Cookie: " + strCookie + "\r\n\r\n" + strBody;

                Debug.Print("-------------------------------------\r\n" + strSend);
                //send data
                sendBuf = System.Text.Encoding.ASCII.GetBytes(strSend);
                stream.Write(sendBuf, 0, sendBuf.Length);

                //recive data
                //deal with http header
                bFindHeader = false;
                int recvlen = 0;
                int countlen = 0;

                do
                {
                    recvBuf = new byte[4096];
                    len = stream.Read(recvBuf, 0, recvBuf.Length);
                    buf = new byte[len];
                    Array.Copy(recvBuf, buf, len); //copy data

                    strRecv = System.Text.Encoding.ASCII.GetString(buf);

                    if(bFindHeader == false)
                    {
                    pos = strRecv.IndexOf("\r\n\r\n");
                                    
                     if (pos != -1)
                    {
                        bFindHeader = true;

                        strHeader = strRecv.Substring(0, pos + 4);
                        Debug.Print(strHeader);

                        //get context-length
                        int posStartCntLgh = 0, posEndCntLgh = 0;
                        posStartCntLgh = strHeader.IndexOf("Content-Length: ");
                        if (posStartCntLgh != -1)
                        {
                            posStartCntLgh += "Content-Length: ".Length;
                            posEndCntLgh = strHeader.IndexOf("\r\n", posStartCntLgh);
                            string strContextLength = strHeader.Substring(posStartCntLgh, posEndCntLgh - posStartCntLgh);
                            countlen = Convert.ToInt32(strContextLength);

                            recvlen = strRecv.Length - strHeader.Length;

                        }

                        //get cookie
                        pos = strHeader.IndexOf("Set-Cookie: ");
                        if (pos != -1)
                        {
                            startPos = pos + "Set-Cookie: ".Length;
                            endPos = strHeader.IndexOf("\r\n", startPos);
                            if (endPos != -1)
                            {
                                strTmp = strHeader.Substring(startPos, endPos - startPos);
                                string[] arStr = strTmp.Split(new string[] { "; " }, StringSplitOptions.None);
                                for (int i = 0; i < arStr.Length; i++)
                                {
                                    //"expires=Fri, 29-Jul-2011 11:33:16 GMT"
                                    if (arStr[i] != "path=/" && arStr[i].IndexOf("expires=") != 0)
                                    {
                                        arCookies.Add(arStr[i]);


                                        string[] arTmpCookie = arStr[i].Split(new char[] { '=' });

                                        if (arTmpCookie.Length > 1)
                                        {
                                            strtCookie tmp;
                                            tmp.Key = arTmpCookie[0];
                                            tmp.Value = arTmpCookie[1];

                                            bool bFindKey = false;
                                            for (int j = 0; j < ListOfCookies.Count; j++)
                                            {
                                                if (ListOfCookies[j].Key == tmp.Key)
                                                {
                                                    bFindKey = true;
                                                }

                                            }
                                            if (!bFindKey)
                                                ListOfCookies.Add(tmp);

                                        }
                                    }

                                }
                            }
                        }

                    }
                }
                    else
                {
                    recvlen += strRecv.Length;
                }

                    //System.Threading.Thread.Sleep(10);  //避免了太快，服务端没有把数据写入到缓冲区
                } while (recvlen < countlen);
                //} while (stream.DataAvailable);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Post(string url, string Body,out string retBody)
        {
            try
            {
                retBody = string.Empty;

                for (int i = 0; i < arCookies.Count; i++)
                {
                    strCookie += arCookies[i] + "; ";
                }


                //strBody = "userid=lx01&pwd=jift&x=0&y=0";
                strBody = Body;
                strSend = "POST " + url + " HTTP/1.1" + "\r\n" +
                          "Accept: text/html" + "\r\n" +
                          "Accept-Language: zh-cn" + "\r\n" +
                          "User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1)" + "\r\n" +
                          "Accept-Encoding: gzip, deflate" + "\r\n" +
                          "Content-Type: application/x-www-form-urlencoded" + "\r\n" +
                          "Content-Length: " + strBody.Length.ToString() + "\r\n" +
                          "Pragma: no-cache" + "\r\n" +
                            "Host: " + server + "\r\n" +
                    //"Host: work.u7pk.com" + "\r\n" +
                          "Cookie: " + strCookie + "\r\n\r\n" + strBody;

                Debug.Print("-------------------------------------\r\n" + strSend);
                //send data
                sendBuf = System.Text.Encoding.ASCII.GetBytes(strSend);
                stream.Write(sendBuf, 0, sendBuf.Length);

                //recive data
                //deal with http header
                bFindHeader = false;
                int recvlen = 0;
                int countlen = 0;

                do
                {
                    recvBuf = new byte[4096];
                    len = stream.Read(recvBuf, 0, recvBuf.Length);
                    buf = new byte[len];
                    Array.Copy(recvBuf, buf, len); //copy data

                    strRecv = System.Text.Encoding.ASCII.GetString(buf);

                    if (!bFindHeader) 
                    { 
                    pos = strRecv.IndexOf("\r\n\r\n");
                    if (pos != -1)
                    {
                        bFindHeader = true;
                        strHeader = strRecv.Substring(0, pos + 4);
                        Debug.Print(strHeader);

                        //get context-length
                        int posStartCntLgh = 0, posEndCntLgh = 0;
                        posStartCntLgh = strHeader.IndexOf("Content-Length: ");
                        if (posStartCntLgh != -1)
                        {
                            posStartCntLgh += "Content-Length: ".Length;
                            posEndCntLgh = strHeader.IndexOf("\r\n", posStartCntLgh);
                            string strContextLength = strHeader.Substring(posStartCntLgh, posEndCntLgh - posStartCntLgh);
                            countlen = Convert.ToInt32(strContextLength);

                            recvlen = strRecv.Length - strHeader.Length;

                        }

                        //get cookie
                        pos = strHeader.IndexOf("Set-Cookie: ");
                        if (pos != -1)
                        {
                            startPos = pos + "Set-Cookie: ".Length;
                            endPos = strHeader.IndexOf("\r\n", startPos);
                            if (endPos != -1)
                            {
                                strTmp = strHeader.Substring(startPos, endPos - startPos);
                                string[] arStr = strTmp.Split(new string[] { "; " }, StringSplitOptions.None);
                                for (int i = 0; i < arStr.Length; i++)
                                {
                                    //"expires=Fri, 29-Jul-2011 11:33:16 GMT"
                                    if (arStr[i] != "path=/" && arStr[i].IndexOf("expires=") != 0)
                                    {
                                        arCookies.Add(arStr[i]);


                                        string[] arTmpCookie = arStr[i].Split(new char[] { '=' });

                                        if (arTmpCookie.Length > 1)
                                        {
                                            strtCookie tmp;
                                            tmp.Key = arTmpCookie[0];
                                            tmp.Value = arTmpCookie[1];

                                            bool bFindKey = false;
                                            for (int j = 0; j < ListOfCookies.Count; j++)
                                            {
                                                if (ListOfCookies[j].Key == tmp.Key)
                                                {
                                                    bFindKey = true;
                                                }

                                            }
                                            if (!bFindKey)
                                                ListOfCookies.Add(tmp);

                                        }
                                    }

                                }
                            }
                        }
                        }
                    }
                    else
                    {
                        strHeader = string.Empty;
                        recvlen += strRecv.Length;
                    }

                    retBody += strRecv.Substring((strHeader.Length));
                    //System.Threading.Thread.Sleep(10);  //避免了太快，服务端没有把数据写入到缓冲区
                } while (recvlen < countlen);
                //} while (stream.DataAvailable);

                return true;

            }
            catch (Exception ex)
            {
                retBody = string.Empty;
                return false;
            }
        }

        private void ListOfCookies_Add(string strCookieElement)
        {

        }

        public bool Download(string url, string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            try
            {
                if (recvBuf != null)
                    Array.Clear(recvBuf, 0, recvBuf.Length);

                //确保所有的数据都已接收完毕
                //do{
                //    recvBuf = new byte[1024];
                //    len = stream.Read(recvBuf, 0, recvBuf.Length);
                //} while (stream.DataAvailable);

                for (int i = 0; i < arCookies.Count; i++)
                {
                    strCookie += arCookies[i] + "; ";
                }

                strSend = "GET " + url + " HTTP/1.1" + "\r\n" +
                            "Accept: text/html" + "\r\n" +
                            "Accept-Language: zh-cn" + "\r\n" +
                            "User-Agent: Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1)" + "\r\n" +
                            "Accept-Encoding: gzip, deflate" + "\r\n" +
                            "Content-Length: 0" + "\r\n" +
                            "Connection: Keep-Alive" + "\r\n" +
                            "Host: " + server + "\r\n" +
                            "Cookie: " + strCookie + "\r\n" +
                            "Pragma: no-cache" + "\r\n\r\n";

                //send data
                sendBuf = System.Text.Encoding.ASCII.GetBytes(strSend);
                stream.Write(sendBuf, 0, sendBuf.Length);

                //FileStream fs = new FileStream(filename, FileMode.Create);
                //recive data
                //len = 1;
                bFindHeader = false;

                // client.ReceiveTimeout = 3000;
                int iWriteLen = 0; //已写入字节数
                int iCountLen = 0; //总长度


                do
                {
                    recvBuf = new byte[1024];
                    len = stream.Read(recvBuf, 0, recvBuf.Length);

                    buf = new byte[len];
                    Array.Copy(recvBuf, buf, len); //copy data

                    strRecv = System.Text.Encoding.ASCII.GetString(buf);
                    Debug.Print(strRecv);


                    if (bFindHeader)
                    {
                        fs.Write(buf, 0, len);
                        iWriteLen += len;
                    }
                    else
                    {
                        //deal with http header
                        pos = strRecv.IndexOf("\r\n\r\n");
                        if (pos != -1)
                        {
                            bFindHeader = true;
                            strHeader = strRecv.Substring(0, pos + 4);
                            Debug.Print(strHeader);

                            //get context-length
                            int posContextLengthPos = 0, posContextLengthPosEnd = 0;
                            posContextLengthPos = strRecv.IndexOf("Content-Length: ");
                            if (posContextLengthPos != -1)
                            {
                                posContextLengthPos += "Content-Length: ".Length;
                                posContextLengthPosEnd = strRecv.IndexOf("\r\n", posContextLengthPos);
                                string strContextLength;
                                strContextLength = strRecv.Substring(posContextLengthPos, posContextLengthPosEnd - posContextLengthPos);

                                iCountLen = Convert.ToInt32(strContextLength);
                            }


                            //write data
                            fs.Write(buf, pos + 4, len - (pos + 4));
                            iWriteLen = len - (pos + 4);

                            //get cookie
                            pos = strHeader.IndexOf("Set-Cookie: ");
                            if (pos != -1)
                            {
                                startPos = pos + "Set-Cookie: ".Length;
                                endPos = strHeader.IndexOf("\r\n", startPos);
                                if (endPos != -1)
                                {
                                    strTmp = strHeader.Substring(startPos, endPos - startPos);
                                    string[] arStr = strTmp.Split(new string[] { "; " }, StringSplitOptions.None);
                                    for (int i = 0; i < arStr.Length; i++)
                                    {
                                        if (arStr[i] != "path=/" && arStr[i].IndexOf("expires=") != 0)
                                            arCookies.Add(arStr[i]);

                                        string[] arTmpCookie = arStr[i].Split(new char[] { '=' });

                                        if (arTmpCookie.Length > 1)
                                        {
                                            strtCookie tmp;
                                            tmp.Key = arTmpCookie[0];
                                            tmp.Value = arTmpCookie[1];

                                            bool bFindKey = false;
                                            for (int j = 0; j < ListOfCookies.Count; j++)
                                            {
                                                if (ListOfCookies[j].Key != tmp.Key)
                                                {
                                                    bFindKey = true;
                                                    break;
                                                }

                                            }
                                            if (!bFindKey)
                                                ListOfCookies.Add(tmp);

                                        }
                                    }
                                }
                            }

                        }
                    }


                    // System.Threading.Thread.Sleep(3);  //避免了太快，服务端没有把数据写入到缓冲区

                } while (iWriteLen < iCountLen);
                //while (stream.DataAvailable);

                fs.Close();
            }
            catch (Exception ex)
            {
                fs.Close();
                return false;
            }
            finally
            {
                fs.Close();
            }

            return true;
        }

    }
}