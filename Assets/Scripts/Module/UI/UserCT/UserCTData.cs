using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCTData
{
    #region 普通用户查询界面
    public List<User> allUsers = null;
    public void SetUserSearchData()
    {
        if (allUsers == null)
        {
            allUsers = UserMgr.GetAllUsers();
        }
    }

    public List<User> GetCurrentSearchData(string userMail,string userName)
    {
        List<User> userTemp = new List<User>();
        if (string.IsNullOrEmpty(userMail))
        {
            userTemp = allUsers;
        }
        else
        {
            foreach (var user in allUsers)
            {
                if (user.mail.Contains(userMail))
                {
                    userTemp.Add(user);
                }
            }
        }

        List<User> currentUsers = new List<User>();
        if (string.IsNullOrEmpty(userName))
        {
            currentUsers = userTemp;
        }
        else
        {
            foreach (var user in userTemp)
            {
                if (user.mail.Contains(userName))
                {
                    currentUsers.Add(user);
                }
            }
        }   
        return currentUsers;
    }

    public void RemoveUser(string userMail)
    {
        User toRemove = null;
        foreach (var user in allUsers)
        {
            if (user.mail.Equals(userMail))
            {
                toRemove = user;
            }
        }
        if(toRemove  != null)
        {
            allUsers.Remove(toRemove);
        }
        
    }

    #endregion

    #region 用户详情界面
    public User currentUser = null;

    public void ModyifyUserName(string userMail,string userName)
    {
        foreach (var user in allUsers)
        {
            if (user.mail.Equals(userMail))
            {
                user.name = userName;
            }
        }
    }

    public void ModyifyUserPw(string userMail, string pw)
    {
        foreach (var user in allUsers)
        {
            if (user.mail.Equals(userMail))
            {
                user.password  = pw;
            }
        }
    }
    #endregion

    #region 管理员界面
    public List<AdminUser> allAdmins = null;
    public void SetAdminData()
    {
        if(allAdmins == null)
        {
            allAdmins = AdminUserMgr.GetAllAdminUsers();
        }
    }
    public AdminUser currentAdminUser;
    #endregion
}
