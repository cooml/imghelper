using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace imgtobase64
{
    class Base64Helper
    {
        private static List<string> Exts = new List<string>() { "jpg", "jpeg", "gif", "bmp", "png", "ico", "heic" };
        /// <summary>
        /// 图片转Base64
        /// </summary>
        /// <param name="ImageFileName">图片的完整路径</param>
        /// <returns></returns>
        public static string ImgToBase64(string ImageFileName)
        {
            return Base64(ImageFileName);

            try
            {
                string ext = Path.GetExtension(ImageFileName).ToLower().Substring(1);
                if (Exts.Contains(ext))
                {
                    Bitmap bmp = new Bitmap(ImageFileName);

                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    ms.Close();
                    //return "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><title>"+ Path.GetFileNameWithoutExtension(ImageFileName) + "</title><style>div{word-wrap:break-word;word-break:normal;}.imgold{height:50%;}.imgnew{height:100%;}</style></head><body><SCRIPT LANGUAGE=\"JavaScript\">var isnew=false;function imgnew(){var obt=document.getElementById(\"img1\");if(isnew){obt.setAttribute(\"class\", \"imgold\");}else{obt.setAttribute(\"class\", \"imgnew\");}isnew=!isnew;}</script><table><tr><td align=\"center\"><img src=\"" + "data:image/" + ext + ";base64," + Convert.ToBase64String(arr) + "\" class=\"imgold\" name=\"image1\" id=\"img1\"  title=\"单击放大,和缩小。\" onclick=\"imgnew()\" style=\"cursor:pointer;\"/></td></tr></table><div><a href=\"http://www.onezl.com/img.htm\">查看原图片</a>拷贝图片码:</div><div>" + ("data:image/" + ext + ";base64," + Convert.ToBase64String(arr)) +"</div></body></html>";
                    //return "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><title>"+ Path.GetFileNameWithoutExtension(ImageFileName) 
                    //+ "</title><style>div{word-wrap:break-word;word-break:normal;}.imgold{height:50%;}.imgnew{height:100%;}</style></head><body><SCRIPT LANGUAGE=\"JavaScript\">function copyUrl(){var tag = document.createElement('input');tag.setAttribute('id', 'cp_zdy_input');var Url2=document.getElementById(\"img1\");tag.value = Url2.getAttribute(\"src\");document.getElementsByTagName('body')[0].appendChild(tag);document.getElementById('cp_zdy_input').select();document.execCommand('copy');document.getElementById('cp_zdy_input').remove();}var isnew=false;function imgnew(){var obt=document.getElementById(\"img1\");if(isnew){obt.setAttribute(\"class\", \"imgold\");}else{obt.setAttribute(\"class\", \"imgnew\");}isnew=!isnew;}</script><table><tr><td align=\"center\"><img src=\"" + "data:image/" + ext + ";base64," + Convert.ToBase64String(arr) + "\" class=\"imgold\" name=\"image1\" id=\"img1\"  title=\"单击放大,和缩小。\" onclick=\"imgnew()\" style=\"cursor:pointer;\"/></td></tr></table><div><a href=\"http://www.onezl.com/img.htm\" onclick=\"copyUrl()\">查看原图片</a>拷贝图片码:</div><div>" + ("data:image/" + ext + ";base64," + Convert.ToBase64String(arr)) +"</div></body></html>";
                    return "<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><title>" + Path.GetFileNameWithoutExtension(ImageFileName)
                     + "</title><style>img {max-height:750px;overflow: hidden; } div{word-wrap:break-word;word-break:normal;}</style></head><body><table><tr><td align=\"center\"><img src=\"" + "data:image/" + ext + ";base64," + Convert.ToBase64String(arr) + "\" class=\"imgold\" name=\"image1\" id=\"img1\"  title=\"单击放大,和缩小。\" onclick=\"imgnew()\" style=\"cursor:pointer;\"/></td></tr></table><div><a href=\"http://www.onezl.com/img.htm\" onclick=\"copyUrl()\">查看原图片</a>拷贝图片码:</div><SCRIPT type=\"text/javascript\" LANGUAGE=\"JavaScript\">var oi=document.getElementById(\"img1\");oi.setAttribute(\"height\",oi.height/1.3); function copyUrl(){var tag = document.createElement('input');tag.setAttribute('id', 'cp_zdy_input');var Url2=document.getElementById(\"img1\");tag.value = Url2.getAttribute(\"src\");document.getElementsByTagName('body')[0].appendChild(tag);document.getElementById('cp_zdy_input').select();document.execCommand('copy');document.getElementById('cp_zdy_input').remove();}var isnew=false;function imgnew(){var obt=document.getElementById(\"img1\");if(isnew){obt.setAttribute(\"height\",obt.height/1.3);}else{obt.setAttribute(\"height\",obt.height*1.3);}isnew=!isnew;}</script></body></html>";


                }
            }
            catch (Exception e)
            {
                return "";
            }
            return "";
        }


        public static string Base64(string ImageFileName)
        {
            var ret = "";
            try
            {
                string ext = Path.GetExtension(ImageFileName).ToLower().Substring(1);
                if (Exts.Contains(ext))
                {
                    if (ext == "heic")
                    {
                        using (MagickImage image = new MagickImage(ImageFileName))
                        {
                            image.Format = MagickFormat.Jpeg;
                            image.Write(ImageFileName + ".jpg");
                        }
                        var tempJpg = Base64(ImageFileName + ".jpg");
                        File.Delete(ImageFileName + ".jpg");
                        
                        return tempJpg;
                    }
                    else
                    {
                        using (System.Drawing.Image image = System.Drawing.Image.FromFile(ImageFileName))
                        {
                            using (MemoryStream m = new MemoryStream())
                            {
                                image.Save(m, image.RawFormat);
                                byte[] imageBytes = m.ToArray();
                                ret = Convert.ToBase64String(imageBytes);
                            }
                        }
                    }
                    return "<!DOCTYPE html><html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><title>" + Path.GetFileNameWithoutExtension(ImageFileName)
                              + "</title><style>img {max-height:750px;overflow: hidden; } div{word-wrap:break-word;word-break:normal;}</style></head><body><table><tr><td align=\"center\"><img src=\"" + "data:image/" + ext + ";base64," + ret + "\" class=\"imgold\" name=\"image1\" id=\"img1\"  title=\"单击放大,和缩小。\" onclick=\"imgnew()\" style=\"cursor:pointer;\"/></td></tr></table><div><a href=\"http://www.onezl.com/img.htm\" onclick=\"copyUrl()\">查看原图片</a>拷贝图片码:</div><SCRIPT type=\"text/javascript\" LANGUAGE=\"JavaScript\">var oi=document.getElementById(\"img1\");oi.setAttribute(\"height\",oi.height/1.3); function copyUrl(){var tag = document.createElement('input');tag.setAttribute('id', 'cp_zdy_input');var Url2=document.getElementById(\"img1\");tag.value = Url2.getAttribute(\"src\");document.getElementsByTagName('body')[0].appendChild(tag);document.getElementById('cp_zdy_input').select();document.execCommand('copy');document.getElementById('cp_zdy_input').remove();}var isnew=false;function imgnew(){var obt=document.getElementById(\"img1\");if(isnew){obt.setAttribute(\"height\",obt.height/1.3);}else{obt.setAttribute(\"height\",obt.height*1.3);}isnew=!isnew;}</script></body></html>";
                    ;
                }
            }
            catch (Exception e)
            {
            }
            return "";
        }


        /// <summary>
        /// 将文件转换成byte[] 数组
        /// </summary>
        /// <param name="fileUrl">文件路径文件名称</param>
        /// <returns>byte[]</returns>

        public static byte[] AuthGetFileData(string fileUrl)
        {
            return File.ReadAllBytes(fileUrl);
        }
    }
}
