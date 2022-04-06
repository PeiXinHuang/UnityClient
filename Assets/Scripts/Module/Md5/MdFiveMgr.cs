using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class MdFiveMgr
{
    public static string GetMD5HashFromFile(string filePath)
    {
        try
        {
            FileStream file = new FileStream(filePath, System.IO.FileMode.Open);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(file);
            file.Close();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            Debug.LogWarning("GetMD5HashFromFile() fail,error:" + ex.Message);
        }
        return "";
    }
}
