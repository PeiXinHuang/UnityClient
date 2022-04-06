using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginData 
{
    //注册验证码
    public string regIdentifyNum;
    public string regIdentifyMail;
    public string GenRegIdentifyNum(string mail)
    {
        System.Random rd = new System.Random();
        regIdentifyNum = rd.Next(100000, 999999).ToString();
        regIdentifyMail = mail;
        return regIdentifyNum;
    }

    //找回密码验证码
    public string forgetIdentifyNum;
    public string forgetIdentifyMail;
    public string GenForgetIdentifyNum(string mail)
    {
        System.Random rd = new System.Random();
        forgetIdentifyNum = rd.Next(100000, 999999).ToString();
        forgetIdentifyMail = mail;
        return forgetIdentifyNum;
    }

}
