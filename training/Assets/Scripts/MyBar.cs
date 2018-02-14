using UnityEngine;
using System.Collections;

public class MyBar : MonoBehaviour
{
    [SerializeField]
    public UIScrollView scrollView;
    [SerializeField]
    public UIWrapContent wrap;
    [SerializeField]
    public UIPanel panel_ScrollView;
    [SerializeField]
    public UIScrollBar scrollBar;

    public int itemNum = 10;
    public int row = 1;
    private Vector3 save_StartLocalPos;
    private float scrollLength;

    private float endPos;

    void Start()
    {
        scrollView.onMomentumMove += UpdateScrollbar;
        
        save_StartLocalPos = scrollView.transform.localPosition;
                        
        if (null != scrollBar.backgroundWidget)
        {
            UIEventListener bg_listen = UIEventListener.Get(scrollBar.backgroundWidget.gameObject);
            if (null != bg_listen)
            {
                bg_listen.onDrag += OnScrollBarDragged;
            }
            UIEventListener fg_listen = UIEventListener.Get(scrollBar.foregroundWidget.gameObject);
            if (null != fg_listen)
            {
                fg_listen.onDrag += OnScrollBarDragged;
            }
        }
    }
    private void Update()
    {
        if (scrollView.isDragging)
            UpdateScrollbar();
    }

    public void Set(int itemNum, bool initValue = true)
    {
        this.itemNum = itemNum;        

        if (initValue)
            scrollBar.value = 0;

        scrollLength = wrap.itemSize * Mathf.CeilToInt(itemNum / (float)row);
        scrollBar.barSize = panel_ScrollView.GetViewSize().y / scrollLength;
    }

    public void SetScrollViewLocalPosition(Vector3 localPos)
    {
        save_StartLocalPos = localPos;
    }

    public void UpdateScrollbar()
    {
        float calc_value = 0.0f;
        if (scrollView.movement == UIScrollView.Movement.Horizontal)
        {
            endPos = (scrollLength - panel_ScrollView.GetViewSize().x) - Mathf.Abs(save_StartLocalPos.x);
            calc_value = Mathf.Clamp((scrollView.transform.localPosition.x - save_StartLocalPos.x) / (save_StartLocalPos.x - endPos), 0, 1f);
        }
        else if (scrollView.movement == UIScrollView.Movement.Vertical)
        {
            endPos = scrollLength - panel_ScrollView.GetViewSize().y;
            calc_value = Mathf.Clamp((scrollView.transform.localPosition.y - save_StartLocalPos.y) / endPos, 0f, 1f);
        }

        scrollBar.value = calc_value;
        //scrollBar.value = Mathf.Lerp(scrollBar.value, calc_value, 0.1f);
    }    

    //void OnScrollBarPressed(GameObject go, bool isPressed)
    //{
    //    if (!isPressed)
    //    {
    //        OnScrollBarChange();
    //    }
    //}

    void OnScrollBarDragged(GameObject go, Vector2 delta)
    {
        OnScrollBarChange();
    }

    void OnScrollBarChange()
    {
        Vector3 newLocalPos = scrollView.transform.localPosition;
        if (scrollView.movement == UIScrollView.Movement.Vertical)
        {
            newLocalPos.y = scrollBar.value * (scrollLength - panel_ScrollView.GetViewSize().y) + save_StartLocalPos.y;
        }
        else if (scrollView.movement == UIScrollView.Movement.Horizontal)
        {
            newLocalPos.x = scrollBar.value * (scrollLength - panel_ScrollView.GetViewSize().x) + save_StartLocalPos.x;
        }
        SpringPanel.Begin(scrollView.panel.cachedGameObject, newLocalPos, 8);
    }    
}

