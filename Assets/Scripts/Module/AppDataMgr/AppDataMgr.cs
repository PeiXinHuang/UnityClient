using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class AppDataMgr
{
  
    public static int GetResVersion()
    {
        return GetAppInfoData("resVersion");
    }
    public static void SetResVersion(int version)
    {
       SetAppInfoData("resVersion",version);
    }

 

    private static string appInfoPath = Application.persistentDataPath + "/" + "appInfo.json";
    private static int GetAppInfoData(string key)
    {
        AppInfoData appInfoData = null;
        if (!File.Exists(appInfoPath))
        {
            appInfoData = new AppInfoData();
            string jsonStr = JsonUtility.ToJson(appInfoData);
            StreamWriter writer = new StreamWriter(appInfoPath, false);
            writer.WriteLine(jsonStr,true);
            writer.Close();
        }
        else
        {
            StreamReader reader = new StreamReader(appInfoPath);
            string JsonStr = reader.ReadToEnd();
            appInfoData = JsonUtility.FromJson<AppInfoData>(JsonStr);
            if(appInfoData == null)
            {
                appInfoData = new AppInfoData();
            }
            reader.Close();
        }

        if (key.Equals("resVersion"))
        {
            return appInfoData.resVersion;
        }

        return 0;
    }

    private static void SetAppInfoData(string key, int value)
    {
        AppInfoData appInfoData = null;
        if (!File.Exists(appInfoPath))
        {
            appInfoData = new AppInfoData();
        }
        else
        {
            StreamReader reader = new StreamReader(appInfoPath);
            string JsonStr = reader.ReadToEnd();
            appInfoData = JsonUtility.FromJson<AppInfoData>(JsonStr);
            if(appInfoData == null)
            {
                appInfoData = new AppInfoData();
            }
            reader.Close();
        }

        if (key.Equals("resVersion"))
        {
            appInfoData.resVersion = value;
        }
        StreamWriter writer = new StreamWriter(appInfoPath, false);
        string jsonStr = JsonUtility.ToJson(appInfoData,true);
        writer.WriteLine(jsonStr);
        writer.Close();
    }
}

[Serializable]
class AppInfoData
{
    //public int appVersion;
    public int resVersion = 0;
}
