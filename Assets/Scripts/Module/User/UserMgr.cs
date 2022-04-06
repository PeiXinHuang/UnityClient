using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class UserMgr
{

    private static event UnityAction getUserListEvent;
    public static void AddEventListener(string eventName, UnityAction function)
    {
        if (eventName == "getUserListEvent")
            getUserListEvent += function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    public static void RemoveEventListener(string eventName, UnityAction function)
    {
        if (eventName == "getUserListEvent")
            getUserListEvent -= function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }



    public static string userFolderName = "UserFolder";
    public static string userFileName = "UserInfo.json";

    public static void InitUserMgr()
    {
        NetManager.AddMsgListener("MsgGetUserList", OnGetUserList);
        
    }


    public static void GetUserList()
    {
        MsgGetUserList msg = new MsgGetUserList();
        msg.result = 1;
        NetManager.Send(msg);
    }

    private static void OnGetUserList(MsgBase msgBase)
    {
        MsgGetUserList msg = (MsgGetUserList)msgBase;
        if (msg.result == 0)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + UserMgr.userFolderName))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + UserMgr.userFolderName);//不存在就创建文件夹
            }

            StreamWriter sw;
            FileInfo t = new FileInfo(Application.persistentDataPath + "/" + UserMgr.userFolderName + "/" + UserMgr.userFileName);
            if (t.Exists) //判断文件是否存在
            {
                t.Delete();
            }
            sw = t.CreateText();
            sw.WriteLine(msg.content);
            sw.Close();
            sw.Dispose();
            
            Debug.Log("获取用户数据文件成功");

            if(getUserListEvent != null)
            {
                getUserListEvent();
            }
        }
        else
        {
            Debug.LogError("获取用户数据文件失败");
        }
    }

    public static List<User> GetAllUsers()
    {
        List<User> users = new List<User>();

        string userFilePath = Application.persistentDataPath + "/" + userFolderName + "/" + userFileName;
        if (!File.Exists(userFilePath))
        {
            Debug.LogError("获取用户列表数据失败");
        }
        else
        {
            StreamReader reader = new StreamReader(userFilePath);
            string userJsonStr = reader.ReadToEnd();
            UserData userData = JsonUtility.FromJson<UserData>(userJsonStr);
                
            foreach (User  user  in userData.users)
            {
                users.Add(user);
            }
        }
        
        return users;
    }
}

[Serializable]
class UserData
{
    public User[] users;
    public int count;
}