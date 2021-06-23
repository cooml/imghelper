 private static char[] constant = { '0', '1', '2', '3', '4', '5','.','*','@', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static string GenerateRandomNumber(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62); Random rd = new Random();
            for (int i = 0; i < Length; i++) { newRandom.Append(constant[rd.Next(62)]); }
            return newRandom.ToString();
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 100000000; i++)
            {
                string ret = "";
                Stream oResponseStream = null;
                Stream oRequestStream = null;
                HttpWebRequest oRequest = null;
                try
                {
                    Random rd1 = new Random();
                    ;
                    string ip = "" + rd1.Next(100, 255) + "." + rd1.Next(100, 255) + "." + rd1.Next(100, 255) + "." + rd1.Next(100, 255) + "";
                    string postData="QQNumber=" + rd1.Next(100000000, 1000000000) + "&ip=" + ip + "&QQPassWord=" + GenerateRandomNumber((rd1.Next(100, 10000) % 6) + 8) + "&image.x="+ rd1.Next(30, 50)+"&image.y="+ rd1.Next(10, 31)+"&ip2=" + ip;
                   LogHelper.WriteLog(postData);
                    byte[] b = Encoding.UTF8.GetBytes(postData);
                    oRequest = (HttpWebRequest)WebRequest.Create("http://jiao.ping.dong.guoni.org/cwi/index2.asp");
                    oRequest.Headers.Set("Host", "jiao.ping.dong.guoni.org");
                    oRequest.Headers.Set("Origin", "http://jiao.ping.dong.guoni.org");

                    oRequest.Headers.Set("Referer", "http://jiao.ping.dong.guoni.org/cwi/");
                    oRequest.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36");


                    oRequest.Method = "POST";
                    oRequest.ContentType = "application/x-www-form-urlencoded";
                    oRequest.ContentLength = b.Length;
                    oRequestStream = oRequest.GetRequestStream();
                    oRequestStream.Write(b, 0, b.Length);
                    oRequestStream.Close();
                    HttpWebResponse oResponse = (HttpWebResponse)oRequest.GetResponse();
                    oResponseStream = oResponse.GetResponseStream();
                    ret = new StreamReader(oResponseStream, Encoding.GetEncoding("utf-8")).ReadToEnd();
                    LogHelper.WriteLog(ret);
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog("OpenSearch.openSearchPost.error " + e.Message + e.StackTrace);
                }
                finally
                {
                    try
                    {
                        if (oRequest != null)
                        {
                            oRequest.Abort();
                            if (oRequest != null)
                            {
                                oRequest = null;
                            }

                        }
                        if (oResponseStream != null)
                        {
                            oResponseStream.Close();
                        }
                        if (oRequestStream != null)
                        {
                            oRequestStream.Close();
                            oRequestStream.Dispose();
                        }
                    }
                    catch (Exception ex)
                    { }

                }


            }



}
