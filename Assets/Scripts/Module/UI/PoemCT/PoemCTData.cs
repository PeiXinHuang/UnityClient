using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoemCTData
{
    #region 爬取诗词界面相关

    public SelectSpiderType currentSelectSpiderType = SelectSpiderType.NONE;

    private string selectSpiderAuthor;
    private string selectSpiderDynasty;
    private string selectSpiderBook;
    private string selectSpiderType;

    public List<PoemSpider> spiderPoemList = new List<PoemSpider>();

    public int currentSelectSpiderPoemIndex = -1;

    private event UnityAction<PoemSpider> setCurrentSpiderPoemEvent;
    public void AddEventListener(string eventName, UnityAction<PoemSpider> function)
    {
        if (eventName == "setCurrentSpiderPoemEvent")
            setCurrentSpiderPoemEvent += function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }
    public void RemoveEventListener(string eventName, UnityAction<PoemSpider> function)
    {
        if (eventName == "setCurrentSpiderPoemEvent")
            setCurrentSpiderPoemEvent -= function;
        else
            UnityEngine.Debug.LogWarning("Event: " + eventName + " is not exit");
    }


    //选择的类型
    public enum SelectSpiderType
    {
        NONE,
        AUTHOR,
        DYNASTY,
        BOOK,
        TYPE
    }


    public void SetSelectSpiderType(SelectSpiderType selectType, string name)
    {
        currentSelectSpiderType = selectType;
        selectSpiderAuthor = "";
        selectSpiderDynasty = "";
        selectSpiderBook = "";
        selectSpiderType = "";
        switch (currentSelectSpiderType)
        {
            case SelectSpiderType.NONE:
                break;
            case SelectSpiderType.AUTHOR:
                selectSpiderAuthor = name;
                break;
            case SelectSpiderType.DYNASTY:
                selectSpiderDynasty = name;
                break;
            case SelectSpiderType.BOOK:
                selectSpiderBook = name;
                break;
            case SelectSpiderType.TYPE:
                selectSpiderType = name;
                break;
            default:
                break;
        }
    }

    public string GetSelectSpiderName()
    {
        if (!string.IsNullOrEmpty(selectSpiderAuthor))
        {
            return selectSpiderAuthor;
        }
        if (!string.IsNullOrEmpty(selectSpiderDynasty))
        {
            return selectSpiderDynasty;
        }
        if (!string.IsNullOrEmpty(selectSpiderBook))
        {
            return selectSpiderBook;
        }
        if (!string.IsNullOrEmpty(selectSpiderType))
        {
            return selectSpiderType;
        }
        return "";
    }


    public void AddPoemSpiderPoemList(List<PoemSpider> list)
    { 
        spiderPoemList.Clear();
        spiderPoemList.AddRange(list);
        SetCurrentSelectSpiderPoemIndex(0);
    }

    public void SetCurrentSelectSpiderPoemIndex(int index)
    {
        currentSelectSpiderPoemIndex = index;
        setCurrentSpiderPoemEvent(GetPoemSpider());
    }
    public void AddCurrentSelectSpiderPoemIndex()
    {
        if(currentSelectSpiderPoemIndex < spiderPoemList.Count - 1)
        {
            currentSelectSpiderPoemIndex++;
            setCurrentSpiderPoemEvent(GetPoemSpider());
        }
    }
    public void MuteCurrentSelectSpiderPoemIndex()
    {
        if (currentSelectSpiderPoemIndex > 0)
        {
            currentSelectSpiderPoemIndex -- ;
            setCurrentSpiderPoemEvent(GetPoemSpider());
        }
    }
    public PoemSpider GetPoemSpider()
    {
        if (currentSelectSpiderPoemIndex == -1)
        {
            return null;
        }
        return spiderPoemList[currentSelectSpiderPoemIndex];
    }
    public void SetPoemSpider(PoemSpider poemSpider)
    {
        spiderPoemList[currentSelectSpiderPoemIndex] = poemSpider;
    }

    public void ClearSpiderData()
    {
        spiderPoemList = new List<PoemSpider>();
        currentSelectSpiderPoemIndex = -1;
    }

    #endregion
}
