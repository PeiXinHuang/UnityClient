using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using UnityEngine;


public class Email
{
    /// <summary>
    /// 发送者
    /// </summary>
    public string mailFrom { get; set; }

    /// <summary>
    /// 收件人
    /// </summary>
    public string[] mailToArray { get; set; }

    /// <summary>
    /// 抄送
    /// </summary>
    public string[] mailCcArray { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string mailSubject { get; set; }

    /// <summary>
    /// 正文
    /// </summary>
    public string mailBody { get; set; }

    /// <summary>
    /// 发件人密码
    /// </summary>
    public string mailPwd { get; set; }

    /// <summary>
    /// SMTP邮件服务器
    /// </summary>
    public string host { get; set; }

    /// <summary>
    /// 正文是否是html格式
    /// </summary>
    public bool isbodyHtml { get; set; }

    /// <summary>
    /// 附件
    /// </summary>
    public string[] attachmentsPath { get; set; }

    public bool Send()
    {
        //使用指定的邮件地址初始化MailAddress实例
        MailAddress maddr = new MailAddress(mailFrom);
        //初始化MailMessage实例
        MailMessage myMail = new MailMessage();


        //向收件人地址集合添加邮件地址
        if (mailToArray != null)
        {
            for (int i = 0; i < mailToArray.Length; i++)
            {
                myMail.To.Add(mailToArray[i].ToString());
            }
        }

        //向抄送收件人地址集合添加邮件地址
        if (mailCcArray != null)
        {
            for (int i = 0; i < mailCcArray.Length; i++)
            {
                myMail.CC.Add(mailCcArray[i].ToString());
            }
        }
        //发件人地址
        myMail.From = maddr;

        //电子邮件的标题
        myMail.Subject = mailSubject;

        //电子邮件的主题内容使用的编码
        myMail.SubjectEncoding = Encoding.UTF8;

        //电子邮件正文
        myMail.Body = mailBody;

        //电子邮件正文的编码
        myMail.BodyEncoding = Encoding.Default;

        myMail.Priority = MailPriority.High;

        myMail.IsBodyHtml = isbodyHtml;

        //在有附件的情况下添加附件
        try
        {
            if (attachmentsPath != null && attachmentsPath.Length > 0)
            {
                Attachment attachFile = null;
                foreach (string path in attachmentsPath)
                {
                    attachFile = new Attachment(path);
                    myMail.Attachments.Add(attachFile);
                }
            }
        }
        catch (Exception err)
        {
            throw new Exception("在添加附件时有错误:" + err);
        }

        SmtpClient smtp = new SmtpClient();
        //指定发件人的邮件地址和密码以验证发件人身份
        smtp.Credentials = new System.Net.NetworkCredential(mailFrom, mailPwd);

        //设置SMTP邮件服务器
        smtp.Host = host;

        try
        {
            //将邮件发送到SMTP邮件服务器
            smtp.Send(myMail);
            return true;

        }
        catch (System.Net.Mail.SmtpException)
        {
            return false;
        }

    }

    public static bool SendRegEmail(string identifyNum, string mail)
    {
        Email email = new Email();
        
        email.mailFrom = "1716973911@qq.com";
        email.mailPwd = "bzkfmrmwpujpbcbc";
        email.mailSubject = "诗词App管理员注册验证码";
        email.mailBody = "验证码:" + identifyNum;
        email.isbodyHtml = true;    //是否是HTML
        email.host = "smtp.qq.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
        email.mailToArray = new string[] { mail };//接收者邮件集合
        email.mailCcArray = new string[] { "1716973911@qq.com" };//抄送者邮件集合
        try
        {
            if (email.Send())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
    public static bool SendForgetEmail(string identifyNum, string mail)
    {
        Email email = new Email();

        email.mailFrom = "1716973911@qq.com";
        email.mailPwd = "bzkfmrmwpujpbcbc";
        email.mailSubject = "诗词App管理员注册验证码";
        email.mailBody = "验证码:" + identifyNum;
        email.isbodyHtml = true;    //是否是HTML
        email.host = "smtp.qq.com";//如果是QQ邮箱则：smtp:qq.com,依次类推
        email.mailToArray = new string[] { mail };//接收者邮件集合
        email.mailCcArray = new string[] { "1716973911@qq.com" };//抄送者邮件集合
        try
        {
            if (email.Send())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}