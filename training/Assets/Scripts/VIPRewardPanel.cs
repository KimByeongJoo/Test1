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

    List<AchivementConditionData> lst_VipReward;
    List<VIPInfo> lst_VipRewardDaily;

    private void OnDestroy()
    {
        if (Main.Instance)
            Main.Instance.current_panel_depth -= 500;

        _instance = null;
    }

    int button_num;

    [SerializeField]
    UIWrapContent wrap;
    [SerializeField]
    UIScrollView scrollView;

    UIPanel panel_ScrollView;

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

        panel_ScrollView = scrollView.panel;

        AddAllButton();
        WrapSetting();

        panel_ScrollView.depth = panel.depth + 5;
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
        VipButton button = AddButton();
        Vector2 buttonSize = button.GetSize();

        int num = Mathf.CeilToInt(panel_ScrollView.GetViewSize().y / buttonSize.y);
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
}
