using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

class AdminUserMgr
{
    private static event UnityAction getAdminListEvent;
    public static void AddEventListener(string eventName, UnityAction function)
    {
        if (eventName == "getAdminListEvent")
            getAdminListEvent += function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    public static void RemoveEventListener(string eventName, UnityAction function)
    {
        if (eventName == "getAdminListEvent")
            getAdminListEvent -= function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }



    public static string userFolderName = "UserFolder";
    public static string userFileName = "adminUserInfo.json";

    public static void InitAdminMgr()
    {
        NetManager.AddMsgListener("MsgGetAdminUser", OnGetAdminList);
    }


    public static void GetAdminList()
    {
        MsgGetAdminList msg = new MsgGetAdminList();
        msg.result = 1;
        NetManager.Send(msg);
    }

    private static void OnGetAdminList(MsgBase msgBase)
    {
        MsgGetAdminList msg = (MsgGetAdminList)msgBase;
        if (msg.result == 0)
        {
            if (!Directory.Exists(Application.persistentDataPath + "/" + userFolderName))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + userFolderName);//不存在就创建文件夹
            }

            StreamWriter sw;
            FileInfo t = new FileInfo(Application.persistentDataPath + "/" + userFolderName + "/" + userFileName);
            if (t.Exists) //判断文件是否存在
            {
                t.Delete();
            }
            sw = t.CreateText();
            sw.WriteLine(msg.content);
            sw.Close();
            sw.Dispose();

            Debug.Log("获取管理员数据文件成功");

            if (getAdminListEvent != null)
            {
                getAdminListEvent();
            }
        }
        else
        {
            Debug.LogError("获取管理员数据文件失败");
        }
    }

    public static List<AdminUser> GetAllAdminUsers()
    {
        List<AdminUser> users = new List<AdminUser>();

        string userFilePath = Application.persistentDataPath + "/" + userFolderName + "/" + userFileName;
        if (!File.Exists(userFilePath))
        {
            Debug.LogError("获取管理员列表数据失败");
        }
        else
        {
            StreamReader reader = new StreamReader(userFilePath);
            string userJsonStr = reader.ReadToEnd();
            AdminUserData userData = JsonUtility.FromJson<AdminUserData>(userJsonStr);

            foreach (AdminUser user in userData.users)
            {
                users.Add(user);
            }
        }

        return users;
    }

}

[Serializable]
class AdminUserData
{
    public AdminUser[] users;
    public int count;
}