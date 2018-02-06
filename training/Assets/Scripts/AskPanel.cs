using UnityEngine;
using System.Collections;
using System;

public class AskPanel : MonoBehaviour {

    System.Action<string> _onYes;
    System.Action _onNo;

    GameObject target;

    [SerializeField]
    UILabel subject;

    [SerializeField]
    UILabel describe;

    [SerializeField]
    UIScrollView scrollView;

    string parameter;

    public void SetEventTarget(GameObject obj)
    {
         target = obj;
    }

    public void Open(string title, string desc)
    {
        subject.text = title;
        describe.text = desc;        
    }
    public void Open(string title, string desc, Action<string> onYes, Action onNo)
    {
        Open(title, desc);        
        _onYes = onYes;
        _onNo = onNo;
    }
    public void Open(string title, string desc, Action<string> onYes, Action onNo, string param)
    {
        Open(title, desc, onYes, onNo);        
        parameter = param;
    }

    public Vector2 GetDescribeLabelSize()
    {
        return describe.localSize;
    }

    public void ClickYesButton()
    {        
        if (_onYes != null)
        {
            _onYes(parameter);
        }
        Destroy(gameObject);
    }

    public void ClickNoButton()
    {     
        if (_onNo != null)
        {
            _onNo();
        }
        Destroy(gameObject);        
    }

    public void ScrollViewResetPosition()
    {
        if(scrollView != null)
            scrollView.ResetPosition();
    }
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}