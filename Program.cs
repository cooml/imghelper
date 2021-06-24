using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace imgtobase64
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            SendImg(@"C:\Users\luoys4\Desktop\xc");

        }

        public static void GetAllDirList(string strBaseDir)
        {
            DirectoryInfo di = new DirectoryInfo(strBaseDir);
            DirectoryInfo[] diA = di.GetDirectories();
            for (int i = 0; i < diA.Length; i++)
            {
                Console.WriteLine("目录：" + diA[i].FullName);
                HtmlToLocal(diA[i].FullName);
                //al.Add(diA[i].FullName);
                //diA[i].FullName是某个子目录的绝对地址，把它记录在ArrayList中 
                GetAllDirList(diA[i].FullName);
                //注意：递归了。逻辑思维正常的人应该能反应过来 
            }
            HtmlToLocal(strBaseDir);
        }
        public static void SendImg(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] inf = dir.GetFiles();
            foreach (FileInfo finf in inf)
            {
                var htmlSend = Base64Helper.ImgToBase64(finf.FullName);
                var imgDetail = GetTakePicDate(finf.FullName);

                if (!string.IsNullOrEmpty(htmlSend))
                {
                    var res = SentEmil.fnSendMailLive("1102875872@qq.com", htmlSend, "美好记忆--" + finf.Name + "--" + imgDetail);

                    Console.WriteLine(res + finf.Name);
                    if (res != "发送成功!")
                    {
                        Thread.Sleep(2000);
                        res = SentEmil.fnSendMailLive("1102875872@qq.com", htmlSend, "美好记忆--" + finf.Name + "--" + imgDetail);
                    }
                    Thread.Sleep(2000);
                }
            }


        }
        public static void HtmlToLocal(string path)
        {

            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] inf = dir.GetFiles();
            var i = 0;
            foreach (FileInfo finf in inf)
            {
                i++;
                var html = Base64Helper.ImgToBase64(finf.FullName);
                if (!string.IsNullOrEmpty(html))
                {
                    var imgDetail = GetTakePicDate(finf.FullName);
                    var res = WritToFile(html, path + "\\imghtml" + "\\" + finf.Name + imgDetail + ".htm", path + "\\imghtml");

                    Console.WriteLine("完成:" + res + "---" + finf.Name + "  " + i + "/" + inf.Length);
                }
            }

        }
        public static bool WritToFile(string html, string file, string direcory)
        {
            try
            {
                if (!Directory.Exists(direcory))
                { Directory.CreateDirectory(direcory); }
                FileStream fileStream = new FileStream(file, FileMode.Create);
                StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
                streamWriter.Write(html + "\r\n");
                streamWriter.Flush();
                streamWriter.Close();
                fileStream.Close();
            }
            catch (System.Exception e)
            {

                return false;
            }

            return true;
        }

        /// 获中的照片拍摄日期和相机型号

        /// </summary>

        /// <param name="fileName">文件名</param>

        /// <returns>拍摄日期</returns>

        private static string GetTakePicDate(string fileName)

        {
            var retStr = "";
            var v = Imghelp.GetExifByMe(fileName);
            if (v != null)
            {
                foreach (var item in v)
                {
                    retStr += (item.Key + "-" + item.Value.Replace("\0", "") + "-").Replace(" ", "").Replace("/", "_").Replace("-", "_").Replace(":", "_");

                }

            }

            return retStr;

        }

    }
}
