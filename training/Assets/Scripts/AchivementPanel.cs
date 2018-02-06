using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AchivementPanel : MyPanel
{
    private static AchivementPanel _instance;

    public static AchivementPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(AchivementPanel)) as AchivementPanel;                
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }

    [SerializeField]
    UIWrapContent wrap;
    [SerializeField]
    UIGrid grid_tab;
    [SerializeField]
    UIScrollView scrollView;

    [SerializeField]
    Main.AchivementTab current_tab = Main.AchivementTab.Weekly;

    private void Awake()
    {
        base.Awake();        

        if (wrap != null)
            wrap.onInitializeItem = OnInitializeFriendButton;
    }
    
    void Start()
    {
        scrollView.GetComponent<UIPanel>().depth = panel.depth + 5;        

        var buttons = grid_tab.GetComponentsInChildren<UIButton>();

        EventDelegate del1 = new EventDelegate(this, "OnClickTab");
        del1.parameters[0].value = Main.AchivementTab.Weekly;
        buttons[0].onClick.Add(del1);

        EventDelegate del2 = new EventDelegate(this, "OnClickTab");
        del2.parameters[0].value = Main.AchivementTab.Daily;
        buttons[1].onClick.Add(del2);

        EventDelegate del3 = new EventDelegate(this, "OnClickTab");
        del3.parameters[0].value = Main.AchivementTab.Hero;
        buttons[2].onClick.Add(del3);

        EventDelegate del4 = new EventDelegate(this, "OnClickTab");
        del4.parameters[0].value = Main.AchivementTab.Battle;
        buttons[3].onClick.Add(del4);

        EventDelegate del5 = new EventDelegate(this, "OnClickTab");
        del5.parameters[0].value = Main.AchivementTab.Construct;
        buttons[4].onClick.Add(del5);

        EventDelegate del6 = new EventDelegate(this, "OnClickTab");
        del6.parameters[0].value = Main.AchivementTab.Player;
        buttons[5].onClick.Add(del6);

        OnClickTab(current_tab);
        WrapSetting();
    }

    void WrapSetting()
    {
        List<AchivementTypeData> type_Data = MyCsvLoad.Instance.GetAchivementTypeDivideByTabName(current_tab);

        //Dictionary<string, AchivementTypeData> dic_type = MyCsvLoad.Instance.GetAchivementTypeDatas();

        wrap.minIndex = -(type_Data.Count - 1);
        if (type_Data.Count == 1)
        {
            wrap.minIndex = 1;
        }
        wrap.maxIndex = 0;

        Transform trans = wrap.transform;
        if (type_Data.Count < 10)
        {
            for (int i = type_Data.Count; i < trans.childCount; i++)
            {
                trans.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                trans.GetChild(i).gameObject.SetActive(true);
            }
        }

        wrap.SortAlphabetically();
        wrap.WrapContent(true);

        scrollView.ResetPosition();
    }

    void OnInitializeFriendButton(GameObject go, int wrapIndex, int realIndex)
    {
        //Dictionary<string, AchivementTypeData> dic_type = MyCsvLoad.Instance.GetAchivementTypeDatas();
        //Dictionary<string, AchivementConditionData> dic_condition = MyCsvLoad.Instance.GetAchivementConditionDatas();
        List<AchivementTypeData> typeData = MyCsvLoad.Instance.GetAchivementTypeDivideByTabName(current_tab);

        if (typeData.Count <= 0)
            return;
        if (go == null)
            return;

        int lstIndex = realIndex % typeData.Count;

        if (lstIndex < 0)
        {
            lstIndex = -lstIndex;
        }
        AchivementButton button = go.GetComponent<AchivementButton>();

        List<AchivementConditionData> conditionData = MyCsvLoad.Instance.GetCachedByParent(typeData[lstIndex]._id);
        int rand = Random.Range(0, conditionData.Count);

        int reward_value = 0;
        if (conditionData[rand]._reward_cash != 0)
            reward_value = conditionData[rand]._reward_cash;
        else if (conditionData[rand]._reward_food != 0)
            reward_value = conditionData[rand]._reward_food;
        else if (conditionData[rand]._reward_gold != 0)
            reward_value = conditionData[rand]._reward_gold;

        string _description = typeData[lstIndex]._description;
        _description = _description.Replace("{0}", conditionData[rand]._counter.ToString());

        int reward_kingdom_or_exp = conditionData[rand]._reward_kingdom_point;
        string sprite_name = "crown";

        if (reward_kingdom_or_exp == 0)
        {
            reward_kingdom_or_exp = conditionData[rand]._reward_exp;
            sprite_name = "player_exp";
        }

        button.Set(typeData[lstIndex]._name, _description,
            reward_kingdom_or_exp, sprite_name, reward_value);
    }

    public void OnClickTab(Main.AchivementTab tab)
    {
        current_tab = tab;
        WrapSetting();
    }
}
