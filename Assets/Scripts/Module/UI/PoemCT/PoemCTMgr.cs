using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class PoemCTMgr : MonoBehaviour
{
    #region 单例模式
    private static PoemCTMgr _instance = null;
    public static PoemCTMgr instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject poemCTMgrObj = GameObject.Find("PoemCTMgr");
                if (GameObject.Find("PoemCTMgr") == null)
                {
                    poemCTMgrObj = new GameObject("PoemCTMgr");
                }

                _instance = poemCTMgrObj.GetComponent<PoemCTMgr>();
                if (_instance == null)
                {
                    _instance = poemCTMgrObj.AddComponent<PoemCTMgr>();
                    _instance.Init();
                }
            }
            return _instance;
        }
    }
    #endregion

    #region MVC
    private PoemCTView view = null;
    private PoemCTData data = null;
    private void Init()
    {
        view = this.GetComponent<PoemCTView>();
        if (view == null)
        {
            view = this.gameObject.AddComponent<PoemCTView>();
            view.InitView();
        }
        data = new PoemCTData();
        AddEventHandler();
    }

    private void AddEventHandler()
    {
        AddSpiderViewEventHandler();
        AddUploadPoemEventHandler();
    }
    #endregion

    #region 爬取诗词界面相关

    public void AddSpiderViewEventHandler()
    {
        view.beginSpiderBtn.onClick.AddListener(BeginSpiderProgram);
        view.spiderNextBtn.onClick.AddListener(data.AddCurrentSelectSpiderPoemIndex);
        view.spiderLastBtn.onClick.AddListener(data.MuteCurrentSelectSpiderPoemIndex);
        view.spiderUploadBtn.onClick.AddListener(UploadSpiderPoem);

        view.inputUploadTitle.onEndEdit.AddListener(ChangeSpiderPoem);
        view.inputUploadDynasty.onEndEdit.AddListener(ChangeSpiderPoem);
        view.inputUploadBook.onEndEdit.AddListener(ChangeSpiderPoem);
        view.inputUploadType.onEndEdit.AddListener(ChangeSpiderPoem);
        view.inputUploadTranslation.onEndEdit.AddListener(ChangeSpiderPoem);
        view.inputUploadAuthor.onEndEdit.AddListener(ChangeSpiderPoem);
        view.inputUploadAppreciation.onEndEdit.AddListener(ChangeSpiderPoem);
        view.inputUploadAnnotation.onEndEdit.AddListener(ChangeSpiderPoem);

        view.spiderClearBtn.onClick.AddListener(ClearSpiderPoems);


        view.AddEventListener("selectSpiderTypeEvent", SetSelectSpiderType);
        data.AddEventListener("setCurrentSpiderPoemEvent", view.ShowSpiderPoemContent);

        SpiderMgr.AddEventListener("getPoemEvent", data.AddPoemSpiderPoemList);
        NetManager.AddMsgListener("MsgUploadSpiderPoem", OnUploadSpiderPoem);

    }

    public void SetSpiderAuthorView(List<string> authors)
    {
        view.SetSpiderAuthorView(authors);
    }

    public void SetSpiderDynastyView(List<string> dynastys)
    {
        view.SetSpiderDynastyView(dynastys);
    }

    public void SetSpiderTypeView(List<string> types)
    {
        view.SetSpiderTypeView(types);
    }

    public void SetSpiderBookView(List<string> books)
    {
        view.SetSpiderBookView(books);
    }

    public void SetSelectSpiderType(PoemCTData.SelectSpiderType selectSpiderType, string selectName)
    {
        data.SetSelectSpiderType(selectSpiderType, selectName);
    }

    public void BeginSpiderProgram()
    {
        switch (data.currentSelectSpiderType)
        {
            case PoemCTData.SelectSpiderType.NONE:
                MessageBoxMgr.instance.ShowWarnning("请选择要爬取的诗词作者，朝代，标题，或者类型");
                break;
            case PoemCTData.SelectSpiderType.AUTHOR:
                Thread GetPoemByAuthor = new Thread(SpiderMgr.GetPoemByAuthor);
                GetPoemByAuthor.Start(data.GetSelectSpiderName());
                break;
            case PoemCTData.SelectSpiderType.DYNASTY:
                Thread GetPoemByDynasty = new Thread(SpiderMgr.GetPoemByDynasty);
                GetPoemByDynasty.Start(data.GetSelectSpiderName());
                break;
            case PoemCTData.SelectSpiderType.BOOK:
                Thread GetPoemByBook = new Thread(SpiderMgr.GetPoemByBook);
                GetPoemByBook.Start(data.GetSelectSpiderName());
                break;
            case PoemCTData.SelectSpiderType.TYPE:
                Thread GetPoemByType = new Thread(SpiderMgr.GetPoemByType);
                GetPoemByType.Start(data.GetSelectSpiderName());
                break;
            default:
                break;
        }  
    }

    public void UploadSpiderPoem()
    {
        Poem poem = new Poem(
           view.spiderTitleInput.text,
           view.spiderAuthorInput.text,
           view.spiderDynastyInput.text,
           view.spiderContentInput.text,
           view.spiderTypeInput.text,
           view.spiderBookInput.text,
           view.spiderAnnotationInput.text,
           view.spiderTranslationInput.text,
           view.spiderAppreciationInput.text
       );

        if (string.IsNullOrEmpty(poem.title))
        {
            MessageBoxMgr.instance.ShowWarnning("上传诗词失败，诗词标题为空");
            return;
        }
        if (string.IsNullOrEmpty(poem.content))
        {
            MessageBoxMgr.instance.ShowWarnning("上传诗词失败，诗词内容为空");
            return;
        }
        MsgUploadSpiderPoem msg = new MsgUploadSpiderPoem();
        msg.poemContent = PoemMgr.PoemToJson(poem);
        msg.result = 0;
        NetManager.Send(msg);
    }

    public void OnUploadSpiderPoem(MsgBase msgBase)
    {
        MsgUploadSpiderPoem msg = (MsgUploadSpiderPoem)msgBase;
        if(msg.result == 0)
        {
            MessageBoxMgr.instance.ShowInfo("诗词上传成功");
        }
        else if(msg.result == 1)
        {
            MessageBoxMgr.instance.ShowInfo("服务器已存在该诗词，已经修改至最新状态");
        }
        else
        {
            MessageBoxMgr.instance.ShowError("上传失败，请重试");
        }
    }

    public void ChangeSpiderPoem(string changeValue)
    {
        PoemSpider poemSpider = new PoemSpider();
        poemSpider.title = view.spiderTitleInput.text;
        poemSpider.author = view.spiderAuthorInput.text;
        poemSpider.dynasty = view.spiderDynastyInput.text;
        poemSpider.book = view.spiderBookInput.text;
        poemSpider.type = view.spiderTypeInput.text;
        poemSpider.content = view.spiderContentInput.text;
        poemSpider.translation = view.spiderTranslationInput.text;
        poemSpider.appreciation = view.spiderAppreciationInput.text;
        poemSpider.annotation = view.spiderAnnotationInput.text;

        data.SetPoemSpider(poemSpider);
    }

    public void ClearSpiderPoems()
    {
        data.ClearSpiderData();
        view.ClearSpider();
    }

    #endregion

    #region 上传诗词界面相关
    public void AddUploadPoemEventHandler()
    {
        NetManager.AddMsgListener("MsgUploadPoem", OnUploadPoemToService);
        view.uploadBtn.onClick.AddListener(UploadPoem);

    }

    private void UploadPoem()
    {
        Poem poem = new Poem(
            view.inputUploadTitle.text,
            view.inputUploadAuthor.text,    
            view.inputUploadDynasty.text,
            view.inputUploadContent.text,
            view.inputUploadType.text,
            view.inputUploadBook.text,
            view.inputUploadAnnotation.text,
            view.inputUploadTranslation.text,
            view.inputUploadAppreciation.text
        );

        if (string.IsNullOrEmpty(poem.title))
        {
            MessageBoxMgr.instance.ShowWarnning("上传诗词失败，诗词标题为空");
            return;
        }
        if (string.IsNullOrEmpty(poem.content))
        {
            MessageBoxMgr.instance.ShowWarnning("上传诗词失败，诗词内容为空");
            return;
        }
        MsgUploadPoem msgUploadPoem = new MsgUploadPoem();
        msgUploadPoem.poemContent = PoemMgr.PoemToJson(poem);
        msgUploadPoem.result = 1;
        NetManager.Send(msgUploadPoem);
    }

    private void OnUploadPoemToService(MsgBase msgBase)
    {
        MsgUploadPoem msg = (MsgUploadPoem)msgBase;
        if(msg.result == 0)
        {
            MessageBoxMgr.instance.ShowInfo("诗词上传成功");
            view.ClearUpload();
        }
        else if(msg.result == 1)
        {
            MessageBoxMgr.instance.ShowWarnning("诗词已经存在，跟新至最新状态");
            view.ClearUpload();
        }
        else
        {
            MessageBoxMgr.instance.ShowError("诗词上传失败，请重试");
        }


    }
    #endregion
}
