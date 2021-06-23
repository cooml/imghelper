using System;
using System.Collections.Generic;
using System.Text;

namespace imgtobase64
{
  public class SentEmil
  {

    public static string fnSendMailLive(string StrTo, string StrBody, string strSubjec)
    {

      System.Net.Mail.MailMessage onemail = new System.Net.Mail.MailMessage();

      string myEmail = "a@onezl.com"; //发送邮件t的?邮箱?地?址

      string myPwd = "123"; //

      onemail.BodyEncoding = System.Text.Encoding.UTF8;

      onemail.IsBodyHtml = true;


      onemail.From = new System.Net.Mail.MailAddress(myEmail);
      string[] ss = StrTo.Split(';');
      for (int i = 0; i < ss.Length; i++)
      {
        onemail.To.Add(new System.Net.Mail.MailAddress(ss[i]));
      }

      onemail.Subject = strSubjec;

      onemail.Body = StrBody;

      System.Net.Mail.SmtpClient clint = new System.Net.Mail.SmtpClient("smtp.mxhichina.com", 25);//发送邮件t的?服t务?器

      clint.Credentials = new System.Net.NetworkCredential(myEmail, myPwd);

      clint.EnableSsl = true;

      clint.Timeout = 15000;

      try { clint.Send(onemail); }
      catch (Exception ex) { return ex.Message; }

      return "发送成功!";

    }

    public static string fnSendMailQQ(string StrTo, string StrBody, string strSubjec)
    {

      System.Net.Mail.MailMessage onemail = new System.Net.Mail.MailMessage();

      string myEmail = "764607205@qq.com"; //发送邮件t的?邮箱?地?址

      string myPwd = "000"; //发送邮件t的?邮箱?密码?

      onemail.BodyEncoding = System.Text.Encoding.UTF8;

      onemail.IsBodyHtml = true;


      onemail.From = new System.Net.Mail.MailAddress(myEmail);
      string[] ss = StrTo.Split(';');
      for (int i = 0; i < ss.Length; i++)
      {
        onemail.To.Add(new System.Net.Mail.MailAddress(ss[i]));
      }

      onemail.Subject = strSubjec;

      onemail.Body = StrBody;

      System.Net.Mail.SmtpClient clint = new System.Net.Mail.SmtpClient("smtp.qq.com", 25);//发送邮件t的?服t务?器

      clint.Credentials = new System.Net.NetworkCredential(myEmail, myPwd);

      clint.EnableSsl = true;

      clint.Timeout = 15000;

      try { clint.Send(onemail); }
      catch (Exception ex) { return ex.Message; }

      return "发送成功!";

    }

  }
}
