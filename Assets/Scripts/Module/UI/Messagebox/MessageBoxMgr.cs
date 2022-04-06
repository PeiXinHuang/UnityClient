using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxMgr : MonoBehaviour
{
    #region 单例
    private static MessageBoxMgr _instance;
    public static MessageBoxMgr instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (MessageBoxMgr)FindObjectOfType(typeof(MessageBoxMgr));
                if (_instance == null)
                {
                    GameObject MessageBoxMgrObj = new GameObject("MessageBoxMgr");
                    _instance = (MessageBoxMgr)MessageBoxMgrObj.AddComponent(typeof(MessageBoxMgr));
                }
                _instance.InitMessageBoxMgr();
            }

            return _instance;
        }

    }
    #endregion

    private MessageBoxView view;
    public MessageBoxData data;
    public void InitMessageBoxMgr()
    {
        view = gameObject.AddComponent<MessageBoxView>();
        view.InitView();
        data = new MessageBoxData();
    }

    public void ShowInfo(string infoText)
    {
        view.ShowInfo(infoText);
    }

    public void ShowWarnning(string warnningText)
    {
        view.ShowWarnning(warnningText);
    }

    public void ShowError(string errorText)
    {
        view.ShowError(errorText);
    }
}
