using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public static class DownloaderMgr
{
    public static void InitDownloaderMgr()
    {
        NetManager.AddMsgListener("MsgDownloadPkg", OnDwonloadPkgMd5); 
    }

    public static void BeginDownloadPkg()
    {
        DownloadPkg();
    }

    #region 下载用户文件
    //private static event UnityAction<List<UserBehavior>> getUserBehaviourListEvent;
    //public static void AddEventListener(string eventName, UnityAction<List<UserBehavior>> function)
    //{
    //    if (eventName == "getUserBehaviourListEvent")
    //        getUserBehaviourListEvent += function;
    //    else
    //        UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    //}
    //public static void RemoveEventListener(string eventName, UnityAction<List<UserBehavior>> function)
    //{
    //    if (eventName == "getUserBehaviourListEvent")
    //        getUserBehaviourListEvent -= function;
    //    else
    //        UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    //}


    //public static void DownloadUserBehaviourList(string mail)
    //{
    //    MsgDownloadUserBehaivor msg = new MsgDownloadUserBehaivor();
    //    msg.content = "";
    //    msg.userMail = mail;
    //    msg.result = 0;
    //    NetManager.Send(msg);
    //}

    //public static void OnDownloadUserBehaviourList(MsgBase msgBase)
    //{
    //    MsgDownloadUserBehaivor msg = (MsgDownloadUserBehaivor)msgBase;

    //    if(msg.result ==0)
    //    {

    //        if (!Directory.Exists(Application.persistentDataPath + "/" + UserBehaviorMgr.folderName))
    //        {
    //            Directory.CreateDirectory(Application.persistentDataPath + "/" + UserBehaviorMgr.folderName);//不存在就创建文件夹
    //        }

    //        StreamWriter sw;
    //        FileInfo t = new FileInfo(Application.persistentDataPath + "/" + UserBehaviorMgr.folderName + "/" + UserBehaviorMgr.fileName);
    //        if (t.Exists) //判断文件是否存在
    //        {
    //            t.Delete();
    //        }

    //        sw = t.CreateText();
    //        sw.WriteLine(msg.content);
    //        sw.Close();
    //        sw.Dispose();

    //        UserBehaviorData userBehaviorData = JsonUtility.FromJson<UserBehaviorData>(msg.content);
    //        List<UserBehavior> userBehaviors = new List<UserBehavior>();
    //        if(userBehaviorData != null)
    //        {
    //            userBehaviors.AddRange(userBehaviorData.behaviors);
    //            if (getUserBehaviourListEvent != null)
    //                getUserBehaviourListEvent(userBehaviors);
    //            Debug.Log("获取用户行为文件成功");
    //        }
    //        else
    //        {
    //            Debug.Log("用户行为userBehaviorData为null");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("获取用户行为数据失败");
    //    }

    //}
    #endregion
    /*
 #region 下载诗词文件

 private static event UnityAction<List<Poem>> getPoemAuthorEvent;
 private static event UnityAction<List<Poem>> getPoemDynastyEvent;
 private static event UnityAction<List<Poem>> getPoemTypeEvent;
 private static event UnityAction<List<Poem>> getPoemBookEvent;
 public static void AddEventListener(string eventName, UnityAction<List<Poem>> function)
 {
     if (eventName == "getPoemAuthorEvent")
         getPoemAuthorEvent += function;
     else if (eventName == "getPoemDynastyEvent")
         getPoemDynastyEvent += function;
     else if (eventName == "getPoemTypeEvent")
         getPoemTypeEvent += function;
     else if (eventName == "getPoemBookEvent")
         getPoemBookEvent += function;
     else
         UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
 }
 public static void RemoveEventListener(string eventName, UnityAction<List<Poem>> function)
 {
     if (eventName == "getPoemAuthorEvent")
         getPoemAuthorEvent -= function;
     else if (eventName == "getPoemDynastyEvent")
         getPoemDynastyEvent -= function;
     else if (eventName == "getPoemTypeEvent")
         getPoemTypeEvent -= function;
     else if (eventName == "getPoemBookEvent")
         getPoemBookEvent -= function;
     else
         UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
 }

 private static Dictionary<string, string> authorMd5Dir = new Dictionary<string, string>();
 public static void DownloadPoemByAuthor(string author)
 {
     MsgDownloadPoemAuthor msg = new MsgDownloadPoemAuthor();
     msg.author = author;
     msg.md5 = "";
     msg.result = 0;
     NetManager.Send(msg);
 }
 public static void OnDwonloadPoemByAuthorMd5(MsgBase msgBase)
 {
     MsgDownloadPoemAuthor msg = (MsgDownloadPoemAuthor)msgBase;
     if (msg.result == 0)
     {
         authorMd5Dir[msg.author] = msg.md5;
         PkgManager.BeginReceivePkgAuthor(msg.author);

     }
 }
 public static void OnDownloadPoemByAuthor(string filePath)
 {
     Debug.Log(filePath + "下载成功");
     //MsgDownloadPoemAuthor msg = (MsgDownloadPoemAuthor)msgBase;
     //if (msg.result == 0)
     //{
     //    if (!Directory.Exists(Application.persistentDataPath + "/" + PoemMgr.folderName))
     //    {
     //        Directory.CreateDirectory(Application.persistentDataPath + "/" + PoemMgr.folderName);//不存在就创建文件夹
     //    }
     //    PoemData poemOldData = new PoemData();
     //    StreamWriter sw;
     //    FileInfo t = new FileInfo(Application.persistentDataPath + "/" + PoemMgr.folderName + "/" + msg.author + ".json");
     //    if (t.Exists) //判断文件是否存在
     //    {
     //        StreamReader reader = new StreamReader(Application.persistentDataPath + "/" + PoemMgr.folderName + "/" + msg.author + ".json");
     //        string jsonStr = reader.ReadToEnd();
     //        poemOldData = JsonUtility.FromJson<PoemData>(jsonStr);
     //        t.Delete();
     //    }

     //    PoemData poemNewData = JsonUtility.FromJson<PoemData>(msg.content);
     //    var array = poemOldData.poems.ToList();
     //    var array1 = poemNewData.poems.ToList();
     //    array.AddRange(array1);

     //    PoemData poemData = new PoemData();
     //    poemData.poems =  array.ToArray();
     //    poemData.count = poemOldData.count + poemNewData.count;

     //    sw = t.CreateText();
     //    sw.WriteLine(msg.content);
     //    sw.Close();
     //    sw.Dispose();

     //    if (poemData.count >= msg.totalCount)
     //    {
     //        List<Poem> poems = new List<Poem>();
     //        poems.AddRange(poemData.poems);
     //        if (getPoemAuthorEvent != null)
     //            getPoemAuthorEvent(poems);
     //        Debug.Log("获取诗词数据成功");
     //    }      
     //}
     //else
     //{
     //    Debug.Log("获取诗词数据失败");
     //}
 }


 public static void DownloadPoemByDynasty(string dynasty)
 {
     MsgDownloadPoemDynasty msg = new MsgDownloadPoemDynasty();
     msg.dynasty = dynasty;
     msg.content = "";
     msg.result = 0;
     NetManager.Send(msg);

 }

 public static void OnDownloadPoemByDynasty(MsgBase msgBase)
 {
     Debug.Log("OnDownloadPoemByDynasty");
     MsgDownloadPoemDynasty msg = (MsgDownloadPoemDynasty)msgBase;
     if (msg.result == 0)
     {
         if (!Directory.Exists(Application.persistentDataPath + "/" + PoemMgr.folderName))
         {
             Directory.CreateDirectory(Application.persistentDataPath + "/" + PoemMgr.folderName);//不存在就创建文件夹
         }

         StreamWriter sw;

         FileInfo t = new FileInfo(Application.persistentDataPath + "/" + PoemMgr.folderName + "/" + msg.dynasty + ".json");
         if (t.Exists) //判断文件是否存在
         {
             t.Delete();
         }

         sw = t.CreateText();
         sw.WriteLine(msg.content);
         sw.Close();
         sw.Dispose();

         PoemData poemData = JsonUtility.FromJson<PoemData>(msg.content);
         List<Poem> poems = new List<Poem>();
         poems.AddRange(poemData.poems);
         if (getPoemDynastyEvent != null)
             getPoemDynastyEvent(poems);
         Debug.Log("获取诗词数据成功");
     }
     else
     {
         Debug.Log("获取诗词数据失败");
     }
 }

 #endregion
 */
    #region 下载版本文件
    public static string downloadPkgMd5 = "";
    public static int downloadPkgVersion = 0;
    public static void DownloadPkg()
    {
        MsgDownloadPkg msg = new MsgDownloadPkg();
        msg.result = 1;
        NetManager.Send(msg);
    }
    private static void OnDwonloadPkgMd5(MsgBase msgBase)
    {
        MsgDownloadPkg msg = (MsgDownloadPkg)msgBase;
        if(msg.result == 0)
        {
            downloadPkgMd5 = msg.md5;
            downloadPkgVersion = msg.version;
            if(downloadPkgVersion != AppDataMgr.GetResVersion())
            {
                PkgManager.BeginReceivePkg();
            }
            else
            {
                Debug.Log("资源文件版本一致，不需要重新下载资源");
            }
           
        }
    }
    public static void OnDownloadPkg(string savePath)
    {
        if (File.Exists(savePath))
        {
            if (downloadPkgMd5.Equals(MdFiveMgr.GetMD5HashFromFile(savePath)))
            {
                Debug.Log("资源MD5校验完成");

                PkgManager.UnZipPkg();
                AppDataMgr.SetResVersion(downloadPkgVersion);
            }
            else
            {
                Debug.LogError("版本文件下载失败，md5不正确，开始重新下载" +
                    " " + downloadPkgMd5 + " " + MdFiveMgr.GetMD5HashFromFile(PkgManager.pkgSavePath));
                DownloaderMgr.DownloadPkg();
            }
        }
    }
    #endregion
}
