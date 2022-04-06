using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginMgr : MonoBehaviour
{
    #region 单例模式
    private static LoginMgr _instance = null;
    public static LoginMgr instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject loginMgrObj = GameObject.Find("LoginMgr");
                if (GameObject.Find("LoginMgr") == null)
                {
                    loginMgrObj = new GameObject("LoginMgr");
                }

                _instance = loginMgrObj.GetComponent<LoginMgr>();
                if (_instance == null)
                {
                    _instance = loginMgrObj.AddComponent<LoginMgr>();
                    _instance.Init();
                }
            }
            return _instance;   
        }
    }
    #endregion

    #region MVC
    private LoginView view = null;
    private LoginData data = null;
    private void Init()
    {
        view = this.GetComponent<LoginView>();
        if(view == null)
        {
            view = this.gameObject.AddComponent<LoginView>();
            view.InitView();
        }
        data = new LoginData();

        AddEventHandler();
    }

    private void AddEventHandler()
    {
        AddLoginEventHandler();
        AddRegisterEventHandler();
        AddForgetEventHandler();
    }

    #endregion

    #region 登录相关
    private void AddLoginEventHandler()
    {
        view.loginBtn.onClick.AddListener(Login);
        NetManager.AddMsgListener("MsgAdminLogin", OnMsgLogin);
    }

    private void Login()
    { 

        string adminId = view.loginIdInput.text;
        string password = view.loginPwInput.text;

        if (string.IsNullOrEmpty(adminId))
        {
            MessageBoxMgr.instance.ShowWarnning("请输入用户名");
            return;
        }

        if (string.IsNullOrEmpty(password))
        {
            MessageBoxMgr.instance.ShowWarnning("请输入密码");
            return;
        }

        MsgAdminLogin msg = new MsgAdminLogin();
        msg.mail = adminId;
        msg.password = password;
        NetManager.Send(msg);
    }

    public void OnMsgLogin(MsgBase msgBase)
    {
        MsgAdminLogin msg = (MsgAdminLogin)msgBase;

        if (msg.result == 0)
        {
            AdminUser adminUser = new AdminUser(msg.name, msg.password, msg.mail);
            GameManager.instance.SetAdminUserData(adminUser);
            view.HideView();
            MessageBoxMgr.instance.ShowInfo("登录成功");
        }
        else if(msg.result == 1)
        {
            MessageBoxMgr.instance.ShowError("用户不存在");
        }
        else if (msg.result == 2)
        {
            MessageBoxMgr.instance.ShowError("密码错误");
        }
    }
    #endregion

    #region 注册相关
    private void AddRegisterEventHandler()
    {
        view.registerBtn.onClick.AddListener(Register);
        NetManager.AddMsgListener("MsgAdminRegister", OnMsgRegister);
        view.registerGetIdentifyBtn.onClick.AddListener(SendRegMail);
    }
    private void SendRegMail()
    {
        if (string.IsNullOrEmpty(view.registerMailInput.text))
        {
            Debug.Log("请输入邮箱");
            return;
        }
        bool sendResult = Email.SendRegEmail(data.GenRegIdentifyNum(view.registerMailInput.text),view.registerMailInput.text);
        if (!sendResult)
        {
            Debug.Log("邮箱不存在，发送失败");
            return;
        }
    }
    private void Register()
    {
        string mail = view.registerMailInput.text;
        string name = view.registerNameInput.text;
        string password = view.registerPasswordInput.text;
        string identify = view.registerIdentigyInput.text;
        string key = view.registerKeyInput.text;
        if (string.IsNullOrEmpty(name))
        {
           
            Debug.Log("昵称不能为空");
            return;
        }
        if (string.IsNullOrEmpty(mail))
        {
           
            Debug.Log("邮箱不能为空");
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            Debug.Log("密码不能为空");
            return;
        }
        if (string.IsNullOrEmpty(identify))
        {
            Debug.Log("验证码不能为空");
            return;
        }
        if (!key.Equals("POEMAPPKEY"))
        {
            Debug.Log("管理员密钥错误");
            return;
        }
        if (identify.Equals(data.regIdentifyNum) && mail.Equals(data.regIdentifyMail))
        {
            //发送注册协议到服务器
            MsgAdminRegister msgAdminRegister = new MsgAdminRegister();
            msgAdminRegister.mail = mail;
            msgAdminRegister.name = name;
            msgAdminRegister.password = password;
            NetManager.Send(msgAdminRegister);
        }
        else
        {
            Debug.Log("验证码错误");
        }

    }
    private void OnMsgRegister(MsgBase msgBase)
    {
        MsgAdminRegister msg = (MsgAdminRegister)msgBase;
        if (msg.result == 0)    //服务器端回(0-成功，1-失败(邮箱已经注册)，2-失败)
        {
            view.ShowLoginView();
            Debug.Log("注册成功");
        }
        else if (msg.result == 1)
        {
            Debug.Log("注册失败，邮箱已注册");
        }
        else
        {
            Debug.Log("注册失败");
        }
    }
    #endregion

    #region 忘记密码相关
    private void AddForgetEventHandler()
    {
        view.forgetReGetBtn.onClick.AddListener(ModifyPassword);
        NetManager.AddMsgListener("MsgAdminModifyPassword", OnModifyPassword);
        view.forgetGetIdentifyBtn.onClick.AddListener(SendForgetMail);
    }
    private void SendForgetMail()
    {
        if (string.IsNullOrEmpty(view.forgetMailInput.text))
        {
            Debug.Log("请输入邮箱");
            return;
        }
        bool sendResult = Email.SendForgetEmail(data.GenForgetIdentifyNum(view.forgetMailInput.text), view.forgetMailInput.text);
        if (!sendResult)
        {
            Debug.Log("邮箱不存在，发送失败");
            return;
        }
    }
    private void ModifyPassword()
    {
        string mail = view.forgetMailInput.text;
        string password = view.forgetPasswordInput.text;
        string identify = view.forgetIdentifyInput.text;
        string key = view.forgetKeyInput.text;

        if (string.IsNullOrEmpty(mail))
        {
            Debug.Log("邮箱不能为空");
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            Debug.Log("密码不能为空");
            return;
        }
        if (string.IsNullOrEmpty(identify))
        {
            Debug.Log("验证码不能为空");
            return;
        }
        if (!key.Equals("POEMAPPKEY"))
        {
            Debug.Log("管理员密钥错误");
            return;
        }

        if (identify.Equals(data.forgetIdentifyNum) && mail.Equals(data.forgetIdentifyMail))
        {
            //发送修改密码协议到服务器
            MsgAdminModifyPassword msg = new MsgAdminModifyPassword();
            msg.mail = mail;
            msg.password = password;
            NetManager.Send(msg);
        }
        else
        {
            Debug.Log("验证码错误");
        }
    }
    private void OnModifyPassword(MsgBase msgBase)
    {
        MsgAdminModifyPassword msg = (MsgAdminModifyPassword)msgBase;
        if(msg.result == 0)
        {
            Debug.Log("密码修改成功");
        }
        view.ShowLoginView();
    }
    #endregion
}
