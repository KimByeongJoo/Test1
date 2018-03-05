using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VIPRewardPanel : MyPanel {

    private static VIPRewardPanel _instance;

    public static VIPRewardPanel Instance
    {
        get
        {
            return _instance;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    List<AchivementConditionData> lst_VipReward;
    List<VIPInfo> lst_VipRewardDaily;

    bool[] vipReward;

    int button_num;

    [SerializeField]
    UIWrapContent wrap;
    [SerializeField]
    UIScrollView scrollView;

    [SerializeField]
    UIPanel panel_popup;

    UIPanel panel_ScrollView;

    ItemBoxPopup popup;

    private void Awake()
    {
        base.Awake();
        _instance = this;

        if (wrap != null)
            wrap.onInitializeItem = OnInitializeHeroCards;
    }

    private void Start()
    {
        lst_VipRewardDaily = MyCsvLoad.Instance.GetVIPInfoDatas();
        lst_VipReward = MyCsvLoad.Instance.GetCachedByParent("vip-level");

        vipReward = new bool[lst_VipReward.Count];

        panel_ScrollView = scrollView.panel;

        AddAllButton();
        WrapSetting();

        panel_ScrollView.depth = panel.depth + 5;

        if(Utility.CheckScreenRatio4to3())
        {
            StartCoroutine("InitScroll");
        }
    }

    public bool CheckVipRewarded(int index)
    {
        return vipReward[index];
    }

    public void SetVipRewarded(int index, bool rewarded)
    {
        vipReward[index] = rewarded;
    }

    IEnumerator InitScroll()
    {
        yield return null;
        scrollView.ResetPosition();
        StopCoroutine("InitScroll");
    }

    void WrapSetting()
    {    
        
        wrap.minIndex = -(lst_VipReward.Count - 1);
        if (lst_VipReward.Count == 1)
        {
            wrap.minIndex = 1;
        }
        wrap.maxIndex = 0;

        wrap.SortBasedOnScrollMovement();        

        Transform trans = wrap.transform;
        if (lst_VipReward.Count < button_num)
        {
            for (int i = 0; i < button_num; i++)
            {
                trans.GetChild(i).gameObject.SetActive((i >= lst_VipReward.Count) ? false : true);
            }
        }
        else
        {
            for (int i = 0; i < button_num; i++)
            {
                trans.GetChild(i).gameObject.SetActive(true);
            }
        }
        wrap.WrapContent(true);
        scrollView.ResetPosition();        
    }

    void OnInitializeHeroCards(GameObject go, int wrapIndex, int realIndex)
    {
        if (lst_VipReward.Count <= 0)
            return;
        if (go == null)
            return;

        int lstIndex = realIndex % lst_VipReward.Count;

        if (lstIndex < 0)
        {
            lstIndex = -lstIndex;
        }

        VipButton button = go.GetComponent<VipButton>();

        button.Set(lst_VipReward[lstIndex]._order, lst_VipReward[lstIndex], lst_VipRewardDaily[lstIndex]);
       //     typeData[lstIndex]._name, typeData[lstIndex]._id, typeData[lstIndex]._element, typeData[lstIndex]._hero_class, false);
    }

    public void AddAllButton()
    {
        // add 1
        VipButton button = AddButton();
        Vector2 buttonSize = button.GetSize();

        int num = Mathf.CeilToInt(panel_ScrollView.GetViewSize().y / buttonSize.y);
        
        // add 1
        num += 1;

        button_num = num + 1;

        for (int i = 0; i < num; i++)
        {
            AddButton();
        }
    }

    public VipButton AddButton()
    {
        GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/VipButton"), wrap.gameObject);
        
        return go.GetComponent<VipButton>();
        //return card;
    }
         
    public void PressItemBox(Reward_ItemBox itemBox, bool press, string itemKind)
    {
        if (itemKind.Length != 0)
        {
            if (press)
            {                
                if (popup == null)
                {
                    GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/ItemBox_Popup"), panel_popup.gameObject);
                    popup = go.GetComponent<ItemBoxPopup>();
                    popup.Set(itemBox, itemKind);
                    panel_popup.depth = panel.depth + 10;
                }
                else
                {
                    popup.Set(itemBox, itemKind);
                }
                Utility.CalcPopupPosition(panel_popup, popup, itemBox);
                
                panel_popup.gameObject.SetActive(true);
                //StartCoroutine(ActiveAfterOneFrame());
            }
            else
            {
                panel_popup.gameObject.SetActive(false);                
            }
        }          
    }

    //IEnumerator DrawBox(Vector3 topLeft, Vector3 topRight, Vector3 bottomRight, Vector3 bottomLeft, Color color)
    //{
    //    for (int i=0; i<30; i++)
    //    {            
    //        Debug.DrawLine(topLeft, topRight, color);
    //        Debug.DrawLine(topRight, bottomRight, color);
    //        Debug.DrawLine(bottomRight, bottomLeft, color);
    //        Debug.DrawLine(bottomLeft, topLeft, color);

    //        yield return null;
    //    }
    //}    
}
