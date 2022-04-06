using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCTMgr : MonoBehaviour
{
    #region 单例模式
    private static UserCTMgr _instance = null;
    public static UserCTMgr instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject userCTMgrObj = GameObject.Find("UserCTMgr");
                if (GameObject.Find("UserCTMgr") == null)
                {
                    userCTMgrObj = new GameObject("UserCTMgr");
                }

                _instance = userCTMgrObj.GetComponent<UserCTMgr>();
                if (_instance == null)
                {
                    _instance = userCTMgrObj.AddComponent<UserCTMgr>();
                    _instance.Init();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region MVC
    private UserCTView view = null;
    private UserCTData data = null;
    private void Init()
    {
        view = this.GetComponent<UserCTView>();
        if (view == null)
        {
            view = this.gameObject.AddComponent<UserCTView>();
            view.InitView();
        }
        data = new UserCTData();
        AddEventHandler();

     
    }

    private void AddEventHandler()
    {
        AddUserSearchHandler();
        AddUserDeatilEventHandler();
        AddAdminMgrEventHandler();
    }

    #endregion

    #region 普通用户查询相关

    private void AddUserSearchHandler()
    {
        view.userSearchBtn.onClick.AddListener(ShowSearchContent);
        UserMgr.AddEventListener("getUserListEvent", SetUserList);
        
        view.AddEventListener("showUserDetailEvent", GetUserDetail);
        view.AddEventListener("deleteUserEvent", DeleteUser);

        NetManager.AddMsgListener("MsgDeleteUser", OnDeleteUser);
        NetManager.AddMsgListener("MsgGetUserDetail", OnGetUserDeatil);
        //NetManager.AddMsgListener("MsgDownloadUserBehaivor", OnGetUserDeatil);
    }
    private void SetUserList()
    {
        //设置用户列表数据
        data.SetUserSearchData();
        //显示用户列表
        view.ShowUserSearchView(data.allUsers);
    }

    private void ShowSearchContent()
    {
        string mail = view.userSearchMailInput.text;
        string name = view.userSearchNameInput.text;
        view.ShowUserSearchView(data.GetCurrentSearchData(mail, name));
    }

    private void GetUserDetail(string userMail)
    {
        MsgGetUserDetail msg = new MsgGetUserDetail();
        msg.result = 0;
        msg.userMail = userMail;
        NetManager.Send(msg);
    }


    private void OnGetUserDeatil(MsgBase msgBase)
    {
        MsgGetUserDetail msg = (MsgGetUserDetail)msgBase;
        if (msg.result == 0)
        {
            User user = new User(msg.userMail, msg.userPassword, msg.userName);
            List<UserBehavior> behaviours = UserBehaviorMgr.JsonToUserBehavior(msg.behaviorContent);
            List<Favorite> favorites = FavoriteMgr.JsonToFavorite(msg.favoriteContent);
            view.ShowUserDetailContent(user, behaviours, favorites);

            data.currentUser = user;
        }
        else
        {
            MessageBoxMgr.instance.ShowError("获取用户(" + msg.userMail + ")信息失败：");
        }
    }


    private void DeleteUser(string userMail)
    {
        MsgDeleteUser msg = new MsgDeleteUser();
        msg.result = 1;
        msg.userMail = userMail;
        NetManager.Send(msg);
    }

    private void OnDeleteUser(MsgBase msgBase)
    {
        MsgDeleteUser msg = (MsgDeleteUser)msgBase;
        if (msg.result == 0)
        {
            MessageBoxMgr.instance.ShowInfo("删除用户(" + msg.userMail + ")信息成功");
            data.RemoveUser(msg.userMail);
            ShowSearchContent();
        }
        else
        {
            MessageBoxMgr.instance.ShowError("删除用户(" + msg.userMail + ")信息失败");
        }
    }

    #endregion

    #region  用户详情页面相关
    public void AddUserDeatilEventHandler()
    {
        NetManager.AddMsgListener("MsgModifyUserName", OnModifyUserName);
        NetManager.AddMsgListener("MsgModifyPassword", OnModifyUserPw);

        view.deleteUserBtn.onClick.AddListener(
            () =>
            {
                DeleteUser(data.currentUser.mail);
                view.HideUserDetailContent();  
            }
        );

        view.changeUserInfoBtn.onClick.AddListener(
            () =>
            {
                if (!view.userNameInput.text.Equals(data.currentUser.name))
                {
                    if (string.IsNullOrEmpty(view.userNameInput.text))
                    {
                        MessageBoxMgr.instance.ShowWarnning("用户名不能为空");
                        return;
                    }
                    MsgModifyUserName msg = new MsgModifyUserName();
                    msg.mail = data.currentUser.mail;
                    msg.name = view.userNameInput.text;
                    
                    msg.result = 1;
                    NetManager.Send(msg);
                }
                if (!view.userPwInput.text.Equals(data.currentUser.password))
                {
                    if (string.IsNullOrEmpty(view.userPwInput.text))
                    {
                        MessageBoxMgr.instance.ShowWarnning("用户密码不能为空");
                        return;
                    }
                    MsgModifyPassword msg = new MsgModifyPassword();
                    msg.mail = data.currentUser.mail;
                    msg.password = view.userPwInput.text;
                    msg.result = 1;
                    NetManager.Send(msg);
                }
            }
        );

        view.userBackBtn.onClick.AddListener(ShowSearchContent);
    }


    private void OnModifyUserName(MsgBase msgBase)
    {
        MsgModifyUserName msg = (MsgModifyUserName)msgBase;
        if (msg.result == 0)
        {
            data.ModyifyUserName(msg.mail, msg.name);
            MessageBoxMgr.instance.ShowInfo("修改用户信息成功");
        }
        else
        {
            MessageBoxMgr.instance.ShowError("修改用户信息失败,请重试");
        }
    }

    private void OnModifyUserPw(MsgBase msgBase)
    {
        MsgModifyPassword msg = (MsgModifyPassword)msgBase;
        if (msg.result == 0)
        {
            data.ModyifyUserPw(msg.mail, msg.password);
            SetUserList();
            MessageBoxMgr.instance.ShowError("修改用户信息成功");
        }
        else
        {
            MessageBoxMgr.instance.ShowError("修改用户信息失败,请重试");
        }
    }
    #endregion

    #region 管理员用户页
    private void AddAdminMgrEventHandler()
    {
        AdminUserMgr.AddEventListener("getAdminListEvent", SetAdminList);
        view.AddEventListener("selectAdminEvent", GetAdminDetail);
    }

    private void SetAdminList()
    {
        data.SetAdminData();
        view.SetAdminContent(data.allAdmins);
    }

    private void GetAdminDetail(string  adminMail)
    {

    }
    #endregion
}
