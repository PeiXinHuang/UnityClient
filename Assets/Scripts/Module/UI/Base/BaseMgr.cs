using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMgr : MonoBehaviour
{
    private static BaseMgr _instance = null;
    public static BaseMgr instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject BaseMgrObj = GameObject.Find("BaseMgr");
                if (GameObject.Find("BaseMgr") == null)
                {
                    BaseMgrObj = new GameObject("BaseMgr");
                }

                BaseMgr _instance = BaseMgrObj.GetComponent<BaseMgr>();
                if (_instance == null)
                {
                    _instance = BaseMgrObj.AddComponent<BaseMgr>();
                    _instance.Init();
                }
            }
            return _instance;
        }
    }

    private BaseView view = null;
    private BaseData data = null;
    private void Init()
    {
        view = this.GetComponent<BaseView>();
        if (view == null)
        {
            view = this.gameObject.AddComponent<BaseView>();
            view.InitView();
        }
        data = new BaseData();

        AddEventHanler();
    }

    private void AddEventHanler()
    {
    }

}
