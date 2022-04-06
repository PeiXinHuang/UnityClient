using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UserCTView : MonoBehaviour
{

    #region 事件相关
    private event UnityAction<string> showUserDetailEvent;
    private event UnityAction<string> deleteUserEvent;
    private event UnityAction<string> selectAdminEvent;
    public void AddEventListener(string eventName, UnityAction<string> function)
    {
        if (eventName == "showUserDetailEvent")
            showUserDetailEvent += function;
        else if (eventName == "deleteUserEvent")
            deleteUserEvent += function;
        else if (eventName == "selectAdminEvent")
            selectAdminEvent += function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    public void RemoveEventListener(string eventName, UnityAction<string> function)
    {
        if (eventName == "showUserDetailEvent")
            showUserDetailEvent -= function;
        else if (eventName == "deleteUserEvent")
            deleteUserEvent -= function;
        else if (eventName == "selectAdminEvent")
            selectAdminEvent -= function;

        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    #endregion

    #region 用户管理界面相关

    public void InitView()
    {
        InitUserSearchView();
        InitUserDetailView();
        InitAdminUserView();
        AddEventHandler();
    }

    private void AddEventHandler()
    {
        AddUserDetailEventHandler();
        AddAdminUserEventHandler();
    }

    #endregion


    #region 普通用户界面相关
    public GameObject userItemPrefab;
    public Transform userContent;
    public Button userSearchBtn;
    public TMP_InputField userSearchMailInput;
    public TMP_InputField userSearchNameInput;
    

    private void InitUserSearchView()
    {
        userContent = GameObject.Find("UI/MainPanel/MainContent/UserInfoMgrContent/UsersContentOutline/UsersContent/Scroll View/Viewport/Content").GetComponent<Transform>();
        userItemPrefab = Resources.Load<GameObject>("Prefabs/UserItemPrefab");

        userSearchBtn = GameObject.Find("UI/MainPanel/MainContent/UserInfoMgrContent/UsersContentOutline/UsersContent/SearchBtn").GetComponent<Button>();
        userSearchMailInput = GameObject.Find("UI/MainPanel/MainContent/UserInfoMgrContent/UsersContentOutline/UsersContent/SearchMailInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
        userSearchNameInput = GameObject.Find("UI/MainPanel/MainContent/UserInfoMgrContent/UsersContentOutline/UsersContent/SearchNameInputContent/InputField (TMP)").GetComponent<TMP_InputField>();
    }

    public void ShowUserSearchView(List<User> users)
    {
        HideUserDetailContent();

        //把里面原来有的删掉
        for (int i = userContent.childCount - 1; i >= 0; i--)
        {
            Destroy(userContent.GetChild(i).gameObject);
        }

        foreach (User user in users)
        {
           
            GameObject userSearchItem = GameObject.Instantiate(userItemPrefab, userContent);
            userSearchItem.transform.Find("Mail").Find("MailText").GetComponent<TMP_Text>().text = user.mail;
            userSearchItem.transform.Find("Name").Find("NameText").GetComponent<TMP_Text>().text = user.name;
            //userSearchItem.transform.Find("RegisterTime").Find("RegisterTimeText").GetComponent<TMP_Text>().text = registerTimeDir[user.mail];
            userSearchItem.transform.Find("CTBtns").Find("DetailBtn").GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    showUserDetailEvent(user.mail);
                }
            );
            userSearchItem.transform.Find("CTBtns").Find("DeleteBtn").GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    deleteUserEvent(user.mail);
                }
            );
        }
    }
    #endregion

    #region 用户详情界面相关
    public Button userBackBtn;

    public Image userDetailContent;
    public TMP_InputField userNameInput;
    public TMP_InputField userMailInput;
    public TMP_InputField userPwInput;
  
    public Button changeUserInfoBtn;
    public Button deleteUserBtn;


    public Transform behaviorContent;
    public Transform favoriteContent;

    public GameObject behaviorPrefab;
    public GameObject favoritePrefab;

    private void InitUserDetailView()
    {

        userDetailContent = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent").GetComponent<Image>();

        userNameInput = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/UserInfoContent/NameInput").GetComponent<TMP_InputField>();
        userMailInput = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/UserInfoContent/MailInput").GetComponent<TMP_InputField>();
        userPwInput = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/UserInfoContent/PwInput ").GetComponent<TMP_InputField>();
        changeUserInfoBtn = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/UserInfoContent/ChangeBtn").GetComponent<Button>();
        deleteUserBtn = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/UserInfoContent/DeleteBtn").GetComponent<Button>();

        userBackBtn = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/BackBtn").GetComponent<Button>();

        behaviorContent = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/BehaviorContent/Scroll View/Viewport/Content").GetComponent<Transform>();
        favoriteContent = GameObject.Find("UI/MainPanel/MainContent/UserDetailInfoMgrContent/UsersContentOutline/UsersContent/FavoriteContent/Scroll View/Viewport/Content").GetComponent<Transform>();

        behaviorPrefab = Resources.Load<GameObject>("Prefabs/BehaviorItemPrefab");
        favoritePrefab = Resources.Load<GameObject>("Prefabs/FavoriteItemPrefab");

        HideUserDetailContent();

    }
    private void AddUserDetailEventHandler()
    {
        
    }

    public void ShowUserDetailContent(User user, List<UserBehavior> behaviours, List<Favorite> favorites)
    {
        userNameInput.text = user.name;
        userMailInput.text = user.mail;
        userPwInput.text = user.password;

        //把里面原来有的删掉
        for (int i = behaviorContent.childCount - 1; i >= 0; i--)
        {
            Destroy(behaviorContent.GetChild(i).gameObject);
        }
        foreach (var item in behaviours)
        {   
            GameObject behaviorItem = GameObject.Instantiate(behaviorPrefab, behaviorContent);
            behaviorItem.transform.GetChild(0).GetComponent<TMP_Text>().text = item.behavior;
            behaviorItem.transform.GetChild(1).GetComponent<TMP_Text>().text = item.date;
        }


        //把里面原来有的删掉
        for (int i = favoriteContent.childCount - 1; i >= 0; i--)
        {
            Destroy(favoriteContent.GetChild(i).gameObject);
        }
        foreach (var item in favorites)
        {
            if(item.isFavorite)
            {
                GameObject favoriteItem = GameObject.Instantiate(favoritePrefab, favoriteContent);
                favoriteItem.transform.GetChild(0).GetComponent<TMP_Text>().text = item.poemTitle;
            }  
        }

        userDetailContent.transform.SetAsLastSibling();
    }

    public void HideUserDetailContent()
    {
        userDetailContent.transform.SetAsFirstSibling();
    }

    #endregion

    #region 管理员界面相关

    public Transform adminBarContent;
    public TMP_InputField adminUserNameInput;
    public TMP_InputField adminUserMailInput;
    public Image adminBehaviorContent;

    public GameObject adminItemPrefab;
    public GameObject adminBehaviorItemPrefab;

    private void InitAdminUserView()
    {

    }
    private void AddAdminUserEventHandler()
    {

    }
    public void SetAdminContent(List<AdminUser> adminUsers)
    {
        //把里面原来有的删掉
        for (int i = adminBarContent.childCount - 1; i >= 0; i--)
        {
            Destroy(adminBarContent.GetChild(i).gameObject);
        }
        foreach (var item in adminUsers)
        { 
            GameObject obj = GameObject.Instantiate(adminItemPrefab, adminBarContent);
            obj.transform.GetChild(0).GetComponent<TMP_Text>().text = item.mail;
            obj.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    selectAdminEvent(item.mail);
                }
            );
        }

    }

    #endregion

}
