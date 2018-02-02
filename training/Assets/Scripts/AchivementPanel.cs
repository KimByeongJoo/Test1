using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AchivementPanel : MonoBehaviour {

    [SerializeField]
    UIWrapContent wrap;
    [SerializeField]
    UIScrollView scrollView;
    
    [SerializeField]
    Main.AchivementTab current_tab = Main.AchivementTab.Weekly;

    private void Awake()
    {
        if (wrap != null)
            wrap.onInitializeItem = OnInitializeFriendButton;
    }
    void Start()
    {
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

        wrap.SortBasedOnScrollMovement();
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
        
        string _desciption = typeData[lstIndex]._description;
        _desciption = _desciption.Replace("{0}", conditionData[rand]._counter.ToString());

        int reward_kingdom_or_exp = conditionData[rand]._reward_kingdom_point;
        string sprite_name = "crown";

        if (reward_kingdom_or_exp == 0)
        {
            reward_kingdom_or_exp = conditionData[rand]._reward_exp;
            sprite_name = "player_exp";
        }

        button.Set(typeData[lstIndex]._name, _desciption,
            reward_kingdom_or_exp, sprite_name, reward_value);        
    }

    public void OnClickWeeklyTab()
    {
        current_tab = Main.AchivementTab.Weekly;
        WrapSetting();
    }
    public void OnClickDailyTab()
    {
        current_tab = Main.AchivementTab.Daily;
        WrapSetting();
    }
    public void OnClickHeroTab()
    {
        current_tab = Main.AchivementTab.Hero;
        WrapSetting();
    }
    public void OnClickPlayerTab()
    {
        current_tab = Main.AchivementTab.Player;
        WrapSetting();
    }
    public void OnClickBattleTab()
    {
        current_tab = Main.AchivementTab.Battle;
        WrapSetting();
    }
    public void OnClickConstructTab()
    {
        current_tab = Main.AchivementTab.Construct;
        WrapSetting();
    }
}
