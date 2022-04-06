using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{


    #region 单例模式
    private static GameManager _instance = null;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameManagerObj = GameObject.Find("GameManager");
                if (GameObject.Find("GameManager") == null)
                {
                    gameManagerObj = new GameObject("GameManager");
                }

                _instance = gameManagerObj.GetComponent<GameManager>();
                if (_instance == null)
                {
                    _instance = gameManagerObj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region 程序启动运行相关
    private void Start()
    {
        //启动界面
        StartView(); 

        //连接网络
        NetManager.Connect(serverIp, serverPort);

        //开始下载服务端资源
        StartCoroutine(StartDownloadRes());



        //开始爬取网络资源
        StartSpiderProgram();
    }

    private void Update()
    {
        NetManager.Update();
        SpiderMgr.Update();
    }


    private void StartView()
    {
        InitView();

        LoginMgr loginMgr = LoginMgr.instance;
        BaseMgr baseMgr = BaseMgr.instance;
        UserCTMgr userCTMgr = UserCTMgr.instance;
        MessageBoxMgr messageBoxMgr = MessageBoxMgr.instance;

        SetViewShow();

        AddNetEventHandler();
    }

    private IEnumerator StartDownloadRes()
    {
        yield return null; //等待一帧，执行一下Update，开始接收服务器信息，保证已经连接上服务器

        DownloaderMgr.InitDownloaderMgr();

        UserMgr.InitUserMgr();
        UserMgr.GetUserList();
        AdminUserMgr.InitAdminMgr();
        AdminUserMgr.GetAdminList();

        //DownloaderMgr.DownloadUserBehaviourList("");
        DownloaderMgr.BeginDownloadPkg();

        yield return null;
    }

    private void StartSpiderProgram()
    {
        SpiderMgr.Init();

        SpiderMgr.AddEventListener("getAllAuthorEvent", PoemCTMgr.instance.SetSpiderAuthorView);
        Thread GetAuthorThread = new Thread(SpiderMgr.GetAllAuthor);
        GetAuthorThread.Start();

        SpiderMgr.AddEventListener("getAllDynastyEvent", PoemCTMgr.instance.SetSpiderDynastyView);
        Thread GetDynastyThread = new Thread(SpiderMgr.GetAllDynasty);
        GetDynastyThread.Start();


        SpiderMgr.AddEventListener("getAllTypeEvent", PoemCTMgr.instance.SetSpiderTypeView);
        SpiderMgr.AddEventListener("getAllBookEvent", PoemCTMgr.instance.SetSpiderBookView);
        Thread GetTypeThread = new Thread(SpiderMgr.GetAllType);
        GetTypeThread.Start();
    }

    #endregion

    #region 界面相关
    [Header("需要进行初始化的界面")]
    public List<GameObject> views = new List<GameObject>();
    [Header("最初显示的界面")]
    public List<GameObject> viewsShowed =  new List<GameObject>();
    private void InitView()
    {
        foreach (GameObject view in views)
        {
            if(view != null)
            {
                view.SetActive(true);
            }
        }
    }
    private void SetViewShow()
    {
        foreach (GameObject view in views)
        {
            if (view != null)
            {
                view.SetActive(false);
            }
        }
        foreach (GameObject viewShowed in viewsShowed)
        {
            if (viewsShowed != null)
            {
                viewShowed.SetActive(true);
            }
        }
    }
    #endregion

    #region 网络连接相关
    [Header("服务器IP端口")]
    public string serverIp = "120.25.231.117";
    public int serverPort = 22;
    private void AddNetEventHandler()
    {
        NetManager.AddEventListener(NetManager.NetEvent.ConnectSucc, OnConnectSucc);
        NetManager.AddEventListener(NetManager.NetEvent.ConnectFail, OnConnectFail);
        NetManager.AddEventListener(NetManager.NetEvent.Close, OnConnectClose);
    }

    //连接成功回调
    private void OnConnectSucc(string err)
    {
        Debug.Log("成功连接到服务器");
    }

    //连接失败回调
    private void OnConnectFail(string err)
    {
        //Debug.Log("OnConnectFail " + err);
        MessageBoxMgr.instance.ShowError("网络连接失败："+err);
    }

    //关闭连接
    private void OnConnectClose(string err)
    {
        Debug.Log("OnConnectClose");
    }
    #endregion

    #region 全局对象的保存，如当前用户
    private AdminUser adminUser = new AdminUser("", "","");
    public void SetAdminUserData(AdminUser _adminUser)
    {
        adminUser = _adminUser;
    }
    #endregion


}
