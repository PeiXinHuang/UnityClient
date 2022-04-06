using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    #region 初始化相关
    public void InitView()
    {
        InitHomeView();
        InitUserMgrView();
        InitPoemMgrView();
        InitSelfMgrView();

        AddEventHanler();

        ShowOriView();
    }
    private void AddEventHanler()
    {
        AddHomeEventHandler();
        AddUserMgrEventHandler();
        AddPoemMgrEventHandler();
        AddSelfMgrEventHandler();
    }

    //开始显示首页界面
    public void ShowOriView()
    {
        highlightBtn = homeBtn;
        SetBtnHighLight(highlightBtn,true);
        ShowHomeContent();
    }

    public Button highlightBtn;
    public Button highlightChildBtn;

    private void SetBtnHighLight(Button btn, bool isHighLight)
    {
        if(highlightBtn != null)
        {
            btn.transform.Find("HighLightImg").gameObject.SetActive(isHighLight);
        }
    }

    private void SetChildBtnHighLight(Button btn, bool isHighLight)
    {
        if(highlightChildBtn != null)
        {
            btn.transform.Find("HighLightImg").gameObject.SetActive(isHighLight);
        }
    }

    private void OnClickMgrBtn()
    {
        poemMgrBtn.gameObject.GetComponent<NavBtnMgr>().SetContent();
        SetBtnHighLight(highlightBtn, false);
        highlightBtn = poemMgrBtn;
        SetBtnHighLight(highlightBtn, true);
    }

    #endregion

    #region 首页相关
    public Button homeBtn;
    public Image homeContent;
    private void InitHomeView()
    {
        homeBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/HomeBtn").GetComponent<Button>();
        homeContent = GameObject.Find("UI/MainPanel/MainContent/HomeContent").GetComponent<Image>();
    }
    private void AddHomeEventHandler()
    {
        homeBtn.onClick.AddListener(OnClickHomeBtn);
    }
    private void OnClickHomeBtn()
    {
        SetBtnHighLight(highlightBtn, false);
        highlightBtn = homeBtn;
        SetBtnHighLight(highlightBtn, true);
        homeContent.transform.SetAsLastSibling();
        SetChildBtnHighLight(highlightChildBtn, false);
    }
    private void ShowHomeContent()
    {
        homeContent.transform.SetAsLastSibling();
    }

    #endregion

    #region 用户管理页相关
    public Button userMgrBtn;
    public Button norUserInfoBtn;
    public Button adminUserInfoBtn;
    public Image norUserInfoContent;
    public Image adminUserInfoContent;
    private void InitUserMgrView()
    {
        userMgrBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/UserMgrBtn").GetComponent<Button>();
        norUserInfoBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/UserMgrBtn/Content/norUserInfoBtn").GetComponent<Button>();
        adminUserInfoBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/UserMgrBtn/Content/adminUserInfoBtn").GetComponent<Button>();
        userMgrBtn.gameObject.AddComponent<NavBtnMgr>();

        norUserInfoContent = GameObject.Find("UI/MainPanel/MainContent/UserInfoMgrContent").GetComponent<Image>();
        adminUserInfoContent = GameObject.Find("UI/MainPanel/MainContent/AdminUserMgrContent").GetComponent<Image>();
    }
    private void AddUserMgrEventHandler()
    {
        userMgrBtn.onClick.AddListener(OnClickUserMgrBtn);
        norUserInfoBtn.onClick.AddListener(OnClickUserInfoBtn);
        adminUserInfoBtn.onClick.AddListener(OnClickAdminUserInfoBtn);
    }
    private void OnClickUserMgrBtn()
    {
        userMgrBtn.gameObject.GetComponent<NavBtnMgr>().SetContent();
        SetBtnHighLight(highlightBtn, false);
        highlightBtn = userMgrBtn;
        SetBtnHighLight(highlightBtn, true);
        SetChildBtnHighLight(highlightChildBtn, false);
    }
    private void OnClickUserInfoBtn()
    {
        norUserInfoContent.transform.SetAsLastSibling();

        SetBtnHighLight(highlightBtn, false);

        SetChildBtnHighLight(highlightChildBtn, false);
        highlightChildBtn = norUserInfoBtn;
        SetChildBtnHighLight(highlightChildBtn, true);

    }
    private void OnClickAdminUserInfoBtn()
    {
        adminUserInfoContent.transform.SetAsLastSibling();

        SetBtnHighLight(highlightBtn, false);

        SetChildBtnHighLight(highlightChildBtn, false);
        highlightChildBtn = adminUserInfoBtn;
        SetChildBtnHighLight(highlightChildBtn, true);
    }
    #endregion

    #region 诗词管理页相关
    public Button poemMgrBtn;
    public Button poemServiceMgrBtn;
    public Button poemSpiderBtn;
    public Button poemUploadBtn;

    public Image uploadPoemMgrContent;
    public Image spiderPoemMgrContent;
    public Image poemServiceMgrContent;

    private void InitPoemMgrView()
    {
        poemServiceMgrBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/PoemMgrBtn/Content/PoemServiceMgrBtn").GetComponent<Button>();
        poemSpiderBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/PoemMgrBtn/Content/PoemSpiderBtn").GetComponent<Button>();
        poemUploadBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/PoemMgrBtn/Content/PoemUploadBtn").GetComponent<Button>();
        poemServiceMgrContent = GameObject.Find("UI/MainPanel/MainContent/PoemServiceMgrContent").GetComponent<Image>();
        uploadPoemMgrContent = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent").GetComponent<Image>();
        spiderPoemMgrContent = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent").GetComponent<Image>();

        poemMgrBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/PoemMgrBtn").GetComponent<Button>();
        poemMgrBtn.gameObject.AddComponent<NavBtnMgr>();

    }

    private void AddPoemMgrEventHandler()
    {
        poemMgrBtn.onClick.AddListener(OnClickPoemMgrBtn);
        poemServiceMgrBtn.onClick.AddListener(OnClickPoemServiceMgrBtn);
        poemSpiderBtn.onClick.AddListener(OnClickPoemSpiderBtn);
        poemUploadBtn.onClick.AddListener(OnClickPoemUploadBtn);
    }

    private void OnClickPoemMgrBtn()
    {
        poemMgrBtn.gameObject.GetComponent<NavBtnMgr>().SetContent();
        SetBtnHighLight(highlightBtn, false);
        highlightBtn = poemMgrBtn;
        SetBtnHighLight(highlightBtn, true);
        SetChildBtnHighLight(highlightChildBtn, false);
    }


    private void OnClickPoemServiceMgrBtn()
    {
        poemServiceMgrContent.transform.SetAsLastSibling();

        SetBtnHighLight(highlightBtn, false);

        SetChildBtnHighLight(highlightChildBtn, false);
        highlightChildBtn = poemServiceMgrBtn;
        SetChildBtnHighLight(highlightChildBtn, true);
    }
    private void OnClickPoemSpiderBtn()
    {
        spiderPoemMgrContent.transform.SetAsLastSibling();
        SetBtnHighLight(highlightBtn, false);

        SetChildBtnHighLight(highlightChildBtn, false);
        highlightChildBtn = poemSpiderBtn;
        SetChildBtnHighLight(highlightChildBtn, true);
    }
    private void OnClickPoemUploadBtn()
    {
        uploadPoemMgrContent.transform.SetAsLastSibling();

        SetBtnHighLight(highlightBtn, false);

        SetChildBtnHighLight(highlightChildBtn, false);
        highlightChildBtn = poemUploadBtn;
        SetChildBtnHighLight(highlightChildBtn, true);
    }

    #endregion

    #region 个人中心页相关
    public Button selfMgrBtn;
    public Image selfMgrContent;
    private void InitSelfMgrView()
    {
        selfMgrBtn = GameObject.Find("UI/MainPanel/NavBar/NavBtnContent/SelfMgrBtn").GetComponent<Button>();
        selfMgrContent = GameObject.Find("UI/MainPanel/MainContent/SelfMgrContent").GetComponent<Image>();
    }
    private void AddSelfMgrEventHandler()
    {
        selfMgrBtn.onClick.AddListener(OnClickSelfMgrBtn);
    }
    private void OnClickSelfMgrBtn()
    {
        SetBtnHighLight(highlightBtn, false);
        highlightBtn = selfMgrBtn;
        SetBtnHighLight(highlightBtn, true);
        SetChildBtnHighLight(highlightChildBtn, false);
        selfMgrContent.transform.SetAsLastSibling();
    }
    #endregion

    # region 标题栏管理
    #endregion
}
