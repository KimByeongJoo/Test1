using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroPanel : MyPanel {

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
    
    Sprite[] sprites;

    public enum Hero_Element
    {
        none,
        physic,
        fire,
        ice,
        lightning,
        poison,
        dark,
        divine
    }
    public enum Hero_Kingdom
    {
        none,
        wii,
        han,
        oh,
        chock,
        samurai,
        chohan,
        ancient,
        etc
    }
    public enum Hero_Class
    {
        none,
        tank,
        rogue,
        ranger,
        wizard,
        paladin
    }

    [SerializeField]
    UIWrapContent wrap;

    [SerializeField]
    UIScrollView scrollView;

    public Hero_Element hero_Element    = Hero_Element.none;
    public Hero_Kingdom hero_Kingdom    = Hero_Kingdom.none;
    public Hero_Class   hero_Class      = Hero_Class.none;

    private void Awake()
    {
        base.Awake();
        
        if (wrap != null)
            wrap.onInitializeItem = OnInitializeHeroCards;
    }
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("portraits");

        WrapSetting();
    }

    void WrapSetting()
    {
        List<HeroTypeData> type_Data = MyCsvLoad.Instance.GetHeroTypeDatas(hero_Element, hero_Kingdom, hero_Class);

        //List<AchivementTypeData> type_Data = MyCsvLoad.Instance.GetAchivementTypeDivideByTabName(current_tab);

        //Dictionary<string, AchivementTypeData> dic_type = MyCsvLoad.Instance.GetAchivementTypeDatas();
        int column = type_Data.Count / 7;

        if (type_Data.Count % 7 > 0)
        {
            column++;
        }

        wrap.minIndex = -(column - 1);
        if (column == 1)
        {
            wrap.minIndex = 1;
        }
        wrap.maxIndex = 0;

        Transform trans = wrap.transform;
        if (column < 10)
        {
            for (int i = column; i < trans.childCount; i++)
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

    void OnInitializeHeroCards(GameObject go, int wrapIndex, int realIndex)
    {
        List<HeroTypeData> typeData = MyCsvLoad.Instance.GetHeroTypeDatas(hero_Element, hero_Kingdom, hero_Class);
        //List<HeroTypeData> typeData = MyCsvLoad.Instance.GetHeroDatasElement(MyCsvLoad.Instance.GetHeroTypeDatas(), Hero_Element.fire.ToString()); 

        if (typeData.Count <= 0)
            return;
        if (go == null)
            return;

        int column = typeData.Count / 7;
        int remain = typeData.Count % 7;

        if (remain > 0)
        {
            column++;
        }

        int lstIndex = realIndex % column;

        if (lstIndex < 0)
        {
            lstIndex = -lstIndex;
        }
        Hero7CardSet cardSet = go.GetComponent<Hero7CardSet>();

        int card_num = 7;
        
        for (int j = 0; j < card_num; j++)
        {
            if (lstIndex == column - 1 && j >= remain)
            {
                cardSet.cards[j].gameObject.SetActive(false);
            }
            else
            {
                cardSet.cards[j].gameObject.SetActive(true);
                HeroTypeData data = typeData[7 * lstIndex + j];
                cardSet.cards[j].Set(GetCardSpriteByName(data._portrait), data._element, data._hero_class);
            }
        }        
        //button.Set(typeData[lstIndex]._name, _description,
        //    reward_kingdom_or_exp, sprite_name, reward_value);
    }

    //번쾌
    public Sprite GetCardSpriteByName(string fileName)
    {
        if (sprites == null)
            return null;

        for (int i=0; i< sprites.Length; i++)
        {
            if(sprites[i].name == fileName)
            {
                return sprites[i];
            }
        }
        return null;
    }
}
