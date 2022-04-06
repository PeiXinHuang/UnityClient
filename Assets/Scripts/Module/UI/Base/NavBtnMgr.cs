using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBtnMgr : MonoBehaviour
{
    public RectTransform childContentTran;
    public Button navBtn;
    public bool isShowContent = false;

    private void Start()
    {
        navBtn = GetComponent<Button>();
        childContentTran = transform.Find("Content").GetComponent<RectTransform>();
        childContentTran.gameObject.SetActive(isShowContent);
    }

    public void SetContent()
    {
        isShowContent = !isShowContent;
        childContentTran.gameObject.SetActive(isShowContent);
        if (isShowContent)
        {
            navBtn.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, (childContentTran.childCount + 1) * 60);
            childContentTran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, childContentTran.childCount * 60);
        }
        else
        {
            navBtn.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 60);
            childContentTran.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
        }
    }


}
