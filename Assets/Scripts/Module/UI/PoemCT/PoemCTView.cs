using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PoemCTView : MonoBehaviour
{
    #region 初始化相关
    public void InitView()
    {
        InitSpiderView();
        InitUploadPoemView();

        AddEventHandler();
    }

    private void AddEventHandler()
    {
        AddSpiderHandler();
        AddUploadPoemHandler();
    }
    #endregion

    #region 事件相关
    private event UnityAction<PoemCTData.SelectSpiderType, string> selectSpiderTypeEvent;
    public void AddEventListener(string eventName, UnityAction<PoemCTData.SelectSpiderType, string> function)
    {
        if (eventName == "selectSpiderTypeEvent")
            selectSpiderTypeEvent += function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    public void RemoveEventListener(string eventName, UnityAction<PoemCTData.SelectSpiderType, string> function)
    {
        if (eventName == "selectSpiderTypeEvent")
            selectSpiderTypeEvent -= function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    #endregion

    #region 爬取诗词界面相关
    public Image spiderAuthorContent;
    public Image spiderTypeContent;
    public Image spiderDynastyContent;
    public Image spiderBookContent;
    public GameObject spiderListButtonItemPrefab;

    public Button spiderMainBtn;
    public Button spiderTranslationBtn;
    public Button spiderAnnotationBtn;
    public Button spiderAppreciationBtn;

    public TMP_InputField spiderTitleInput;
    public TMP_InputField spiderAuthorInput;
    public TMP_InputField spiderDynastyInput;
    public TMP_InputField spiderTypeInput;
    public TMP_InputField spiderBookInput;
    public TMP_InputField spiderContentInput;
    public TMP_InputField spiderTranslationInput;
    public TMP_InputField spiderAnnotationInput;
    public TMP_InputField spiderAppreciationInput;

    public Image contentSpiderMain;
    public Image contentSpiderTranslation;
    public Image contentSpiderAnnotation;
    public Image contentSpiderAppreciation;

    public Button beginSpiderBtn;
    public Button spiderLastBtn;
    public Button spiderNextBtn;
    public Button spiderUploadBtn;
    public Button getTranslationBtn;
    public Button getAppreciationBtn;
    public Button getAnnotationBtn;

    public Button spiderClearBtn;

    public Text urlPathText;

    private Button spiderHighLightBtn;

    private void InitSpiderView()
    {
        spiderAuthorContent =  GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderListOutline/SpiderList/Content/AuthorContent").GetComponent<Image>();
        spiderTypeContent =  GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderListOutline/SpiderList/Content/TypeContent").GetComponent<Image>();
        spiderDynastyContent =  GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderListOutline/SpiderList/Content/DynastyContent").GetComponent<Image>();
        spiderBookContent =  GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderListOutline/SpiderList/Content/BookContent").GetComponent<Image>();
        spiderListButtonItemPrefab = Resources.Load<GameObject>("Prefabs/SpiderListButtonItemPrefab");

        spiderMainBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Bar/spiderMainBtn").GetComponent<Button>();
        spiderTranslationBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Bar/spiderTranslationBtn").GetComponent<Button>();
        spiderAnnotationBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Bar/spiderAnnotationBtn").GetComponent<Button>();
        spiderAppreciationBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Bar/spiderAppreciationBtn").GetComponent<Button>();

        contentSpiderMain = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadMain").GetComponent<Image>();
        contentSpiderTranslation = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadTranslation").GetComponent<Image>();
        contentSpiderAnnotation = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadAnnotation").GetComponent<Image>();
        contentSpiderAppreciation = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadAppreciation").GetComponent<Image>();

        spiderTitleInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadMain/TitleContent/InputField (TMP)").GetComponent<TMP_InputField>();
        spiderDynastyInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadMain/DynastyContent/InputField (TMP)").GetComponent<TMP_InputField>();
        spiderAuthorInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadMain/AuthorContent/InputField (TMP)").GetComponent<TMP_InputField>();
        spiderTypeInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadMain/TypeContent/InputField (TMP)").GetComponent<TMP_InputField>();
        spiderBookInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadMain/BookContent/InputField (TMP)").GetComponent<TMP_InputField>();
        spiderContentInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadMain/ContentOutline/Content/InputField (TMP) (1)").GetComponent<TMP_InputField>();
        spiderTranslationInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadTranslation/InputField (TMP)").GetComponent<TMP_InputField>();
        spiderAnnotationInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadAnnotation/InputField (TMP)").GetComponent<TMP_InputField>();
        spiderAppreciationInput = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderContent/Content/contentUploadAppreciation/InputField (TMP)").GetComponent<TMP_InputField>();

        beginSpiderBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/BeginSpiderBtn").GetComponent<Button>();

        spiderLastBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/LastBtn").GetComponent<Button>();
        spiderNextBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/NextBtn").GetComponent<Button>();
        spiderUploadBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/UploadBtn").GetComponent<Button>();
        spiderClearBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/ClearBtn").GetComponent<Button>();
        getTranslationBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/GetTranslationBtn").GetComponent<Button>();
        getAppreciationBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/GetAppreciationBtn").GetComponent<Button>();
        getAnnotationBtn = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/GetAnnotationBtn").GetComponent<Button>();

        urlPathText = GameObject.Find("UI/MainPanel/MainContent/SpiderPoemMgrContent/SpiderCTPanel/UrlPathText").GetComponent<Text>();

        spiderHighLightBtn = spiderMainBtn;
    }

    public void SetSpiderAuthorView(List<string> authors)
    {
        //把里面原来有的删掉
        for (int i = spiderAuthorContent.transform.childCount - 1; i > 0; i--)
        {
            Destroy(spiderAuthorContent.transform.GetChild(i).gameObject);
        }
        //加入新的
        foreach (string author in authors)
        {
            GameObject spiderListButtonObj = Instantiate(spiderListButtonItemPrefab, spiderAuthorContent.transform);
            spiderListButtonObj.transform.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    SetSpiderTypeHighLight(spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>());
                    selectSpiderTypeEvent(PoemCTData.SelectSpiderType.AUTHOR, author);
                }
            );
            spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>().text = author;
        }
    }

    public void SetSpiderDynastyView(List<string> dynastys)
    {
        //把里面原来有的删掉
        for (int i = spiderDynastyContent.transform.childCount - 1; i > 0; i--)
        {
            Destroy(spiderDynastyContent.transform.GetChild(i).gameObject);
        }
        //加入新的
        foreach (string dynasty in dynastys)
        {
            GameObject spiderListButtonObj = Instantiate(spiderListButtonItemPrefab, spiderDynastyContent.transform);
            spiderListButtonObj.transform.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    SetSpiderTypeHighLight(spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>());
                    selectSpiderTypeEvent(PoemCTData.SelectSpiderType.DYNASTY, dynasty);
                }
            );
            spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>().text = dynasty;
        }
    }

    public void SetSpiderTypeView(List<string> types)
    {
        //把里面原来有的删掉
        for (int i = spiderTypeContent.transform.childCount - 1; i > 0; i--)
        {
            Destroy(spiderTypeContent.transform.GetChild(i).gameObject);
        }
        //加入新的
        foreach (string type in types)
        {
            GameObject spiderListButtonObj = Instantiate(spiderListButtonItemPrefab, spiderTypeContent.transform);
            spiderListButtonObj.transform.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    SetSpiderTypeHighLight(spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>());
                    selectSpiderTypeEvent(PoemCTData.SelectSpiderType.TYPE, type);
                }
            );
            spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>().text = type;
        }
    }

    public void SetSpiderBookView(List<string> books)
    {
        //把里面原来有的删掉
        for (int i = spiderBookContent.transform.childCount - 1; i > 0; i--)
        {
            Destroy(spiderBookContent.transform.GetChild(i).gameObject);
        }
        //加入新的
        foreach (string book in books)
        {
            GameObject spiderListButtonObj = Instantiate(spiderListButtonItemPrefab, spiderBookContent.transform);
            spiderListButtonObj.transform.GetComponent<Button>().onClick.AddListener(
                () =>
                {
                    SetSpiderTypeHighLight(spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>());
                    selectSpiderTypeEvent(PoemCTData.SelectSpiderType.BOOK, book);
                }
            );
            spiderListButtonObj.transform.GetChild(0).GetComponent<TMP_Text>().text = book;
        }
    }

    private void AddSpiderHandler()
    {
        spiderMainBtn.onClick.AddListener(ShowSpiderMainContent);
        spiderTranslationBtn.onClick.AddListener(ShowSpiderTranslationContent);
        spiderAnnotationBtn.onClick.AddListener(ShowSpiderAnnotationContent);
        spiderAppreciationBtn.onClick.AddListener(ShowSpiderAppreciationContent);
    }

    //高亮选中的类型（作者，朝代，书籍，类型），原来的高亮取消
    private TMP_Text old_tMP_Text;
    private void SetSpiderTypeHighLight(TMP_Text tMP_Text)
    {
        if(old_tMP_Text != null)
        {
            old_tMP_Text.color = Color.black;
        }
        tMP_Text.color = Color.green;
        old_tMP_Text = tMP_Text;
    }

    public void ShowSpiderPoemContent(PoemSpider poem)
    {
        spiderTitleInput.text = poem.title;
        spiderAuthorInput.text = poem.author;
        spiderDynastyInput.text = poem.dynasty;
        spiderContentInput.text = poem.content;
        spiderTypeInput.text = poem.type;
        spiderBookInput.text = poem.book;
        spiderTranslationInput.text = poem.translation;
        spiderAnnotationInput.text = poem.annotation;
        spiderAppreciationInput.text = poem.appreciation;
    }

    private void ShowSpiderMainContent()
    {
        highLightSpiderBtn(spiderMainBtn);
        contentSpiderMain.transform.SetAsLastSibling();
    }
    private void ShowSpiderTranslationContent()
    {
        highLightSpiderBtn(spiderTranslationBtn);
        contentSpiderTranslation.transform.SetAsLastSibling();
    }
    private void ShowSpiderAnnotationContent()
    {
        highLightSpiderBtn(spiderAnnotationBtn);
        contentSpiderAnnotation.transform.SetAsLastSibling();
    }
    private void ShowSpiderAppreciationContent()
    {
        highLightSpiderBtn(spiderAppreciationBtn);
        contentSpiderAppreciation.transform.SetAsLastSibling();
    }

    private void highLightSpiderBtn(Button button)
    {
        spiderHighLightBtn.transform.Find("highLightImage").gameObject.SetActive(false);
        spiderHighLightBtn = button;
        spiderHighLightBtn.transform.Find("highLightImage").gameObject.SetActive(true);
    }

    public void ClearSpider()
    {
        spiderTitleInput.text = "";
        spiderDynastyInput.text = "";
        spiderAuthorInput.text = "";
        spiderTypeInput.text = "";
        spiderBookInput.text = "";
        spiderContentInput.text = "";
        spiderTranslationInput.text = "";
        spiderAnnotationInput.text = "";
        spiderAppreciationInput.text = "";
        urlPathText.text = "";
    }


    #endregion

    #region 上传诗词界面相关
    public Button btnUploadMain;
    public Button btnUploadTranslation;
    public Button btnUploadAnnotation;
    public Button btnUploadAppreciation;

    public Image contentUploadMain;
    public Image contentUploadTranslation;
    public Image contentUploadAnnotation;
    public Image contentUploadAppreciation;

    public TMP_InputField inputUploadTitle;
    public TMP_InputField inputUploadDynasty;
    public TMP_InputField inputUploadAuthor;
    public TMP_InputField inputUploadType;
    public TMP_InputField inputUploadBook;
    public TMP_InputField inputUploadContent;
    public TMP_InputField inputUploadTranslation;
    public TMP_InputField inputUploadAnnotation;
    public TMP_InputField inputUploadAppreciation;

    public Button uploadBtn;
    public Button resetBtn;

    private Button uploadHighLightBtn;

    private void InitUploadPoemView()
    {

        btnUploadMain = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Bar/PoemMainBtn").GetComponent<Button>();
        btnUploadTranslation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Bar/PoemTranslationBtn").GetComponent<Button>();
        btnUploadAnnotation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Bar/PoemAnnotationBtn").GetComponent<Button>();
        btnUploadAppreciation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Bar/PoemAppreciationBtn").GetComponent<Button>();

        contentUploadMain = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemMainContent").GetComponent<Image>();
        contentUploadTranslation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemTranslationContent").GetComponent<Image>();
        contentUploadAnnotation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemAnnotationContent").GetComponent<Image>();
        contentUploadAppreciation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemAppreciationContent").GetComponent<Image>();

        inputUploadTitle = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemMainContent/TitleContent/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadDynasty = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemMainContent/DynastyContent/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadAuthor = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemMainContent/AuthorContent/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadType = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemMainContent/TypeContent/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadBook = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemMainContent/BookContent/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadContent = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemMainContent/ContentOutline/Content/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadTranslation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemTranslationContent/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadAnnotation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemAnnotationContent/InputField (TMP)").GetComponent<TMP_InputField>();
        inputUploadAppreciation = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/OutlineContent/Content/Content/PoemAppreciationContent/InputField (TMP)").GetComponent<TMP_InputField>();

        uploadBtn = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/MgrContent/Content/uploadBtn").GetComponent<Button>();
        resetBtn = GameObject.Find("UI/MainPanel/MainContent/UploadPoemMgrContent/MgrContent/Content/resetBtn").GetComponent<Button>();

        uploadHighLightBtn = btnUploadMain;
    }

    private void AddUploadPoemHandler()
    {
        btnUploadMain.onClick.AddListener(ShowUploadMainContent);
        btnUploadTranslation.onClick.AddListener(ShowUploadTranslationContent);
        btnUploadAnnotation.onClick.AddListener(ShowUploadAnnotationContent);
        btnUploadAppreciation.onClick.AddListener(ShowUploadAppreciationContent);

        resetBtn.onClick.AddListener(ClearUpload);
    }

    private void ShowUploadMainContent()
    {
        highLightUploadBtn(btnUploadMain);
        contentUploadMain.transform.SetAsLastSibling();
    }
    private void ShowUploadTranslationContent()
    {
        highLightUploadBtn(btnUploadTranslation);
        contentUploadTranslation.transform.SetAsLastSibling();
    }
    private void ShowUploadAnnotationContent()
    {
        highLightUploadBtn(btnUploadAnnotation);
        contentUploadAnnotation.transform.SetAsLastSibling();
    }
    private void ShowUploadAppreciationContent()
    {
        highLightUploadBtn(btnUploadAppreciation);
        contentUploadAppreciation.transform.SetAsLastSibling();
    }
    public void ClearUpload()
    {
        inputUploadTitle.text = "";
        inputUploadDynasty.text = "";
        inputUploadAuthor.text = "";
        inputUploadType.text = "";
        inputUploadBook.text = "";
        inputUploadContent.text = "";
        inputUploadTranslation.text = "";
        inputUploadAnnotation.text = "";
        inputUploadAppreciation.text = "";  
    }

    private void highLightUploadBtn(Button button)
    {
        uploadHighLightBtn.transform.Find("highLightImage").gameObject.SetActive(false);
        uploadHighLightBtn = button;
        uploadHighLightBtn.transform.Find("highLightImage").gameObject.SetActive(true);
    }
    #endregion
}
