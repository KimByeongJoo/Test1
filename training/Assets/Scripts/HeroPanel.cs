using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroPanel : MyPanel {

    private static HeroPanel _instance;

    public static HeroPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(HeroPanel)) as HeroPanel;
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
        chock,
        oh,
        han,
        etc,
        ancient,
        samurai,
        chohan
    }
    public enum Hero_Class
    {
        none,
        tank,
        rogue,
        ranger,
        paladin,
        wizard
    }

    [SerializeField]
    UIWrapContent wrap;
        
    [SerializeField]
    UIScrollView scrollView;

    [Header("Panels")]
    [SerializeField]
    UIPanel popup_Panel1;
    [SerializeField]
    UIPanel popup_Panel2;
    [SerializeField]
    UIPanel popup_Panel3;

    [Header("Element")]
    [SerializeField]
    UIPanel elementList;
    [SerializeField]
    UISprite arrow_Element;
    [SerializeField]
    UISprite icon_Element;
    [SerializeField]
    UILabel label_Element;

    [Header("Kingdom")]
    [SerializeField]
    UIPanel kingdomList;
    [SerializeField]
    UISprite arrow_Kingdom;
    [SerializeField]
    UILabel label_Kingdom;

    [Header("Hero_Class")]
    [SerializeField]
    UIPanel classList;
    [SerializeField]
    UISprite arrow_Class;
    [SerializeField]
    UISprite icon_Class;
    [SerializeField]
    UILabel label_Class;

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

        scrollView.GetComponent<UIPanel>().depth = panel.depth + 5;
        popup_Panel1.depth = panel.depth + 6;
        popup_Panel2.depth = panel.depth + 7;
        popup_Panel3.depth = panel.depth + 8;

    }
    public void AllPopupListClose()
    {
        elementList.alpha = 0;
        kingdomList.alpha = 0;
        classList.alpha = 0;

        arrow_Element.flip = UIBasicSprite.Flip.Vertically;
        arrow_Kingdom.flip = UIBasicSprite.Flip.Vertically;
        arrow_Class.flip = UIBasicSprite.Flip.Vertically;
    }

    public void OnOffElementList()
    {
        if (elementList.alpha == 0)
        {
            AllPopupListClose();
            arrow_Element.flip = UIBasicSprite.Flip.Nothing;            
            elementList.alpha = 1;
        }
        else
        {
            arrow_Element.flip = UIBasicSprite.Flip.Vertically;
            elementList.alpha = 0;
        }
    }
    public void OnOffKingdomList()
    {
        if (kingdomList.alpha == 0)
        {
            AllPopupListClose();            
            arrow_Kingdom.flip = UIBasicSprite.Flip.Nothing;
            kingdomList.alpha = 1;
        }
        else
        {
            arrow_Kingdom.flip = UIBasicSprite.Flip.Vertically;
            kingdomList.alpha = 0;
        }
    }
    public void OnOffClassList()
    {
        if (classList.alpha == 0)
        {
            AllPopupListClose();            
            arrow_Class.flip = UIBasicSprite.Flip.Nothing;
            classList.alpha = 1;
        }
        else
        {
            arrow_Class.flip = UIBasicSprite.Flip.Vertically;
            classList.alpha = 0;
        }
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
            for (int i = 0; i < trans.childCount; i++)
            {
                if(i >= column)
                    trans.GetChild(i).gameObject.SetActive(false);
                else
                    trans.GetChild(i).gameObject.SetActive(true);
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
        else if (column != 0 && remain == 0)
        {
            remain = 7;
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
                cardSet.cards[j].Set(GetCardSpriteByName(data._portrait), data._name, data._element, data._hero_class);
            }
        }        
        //button.Set(typeData[lstIndex]._name, _description,
        //    reward_kingdom_or_exp, sprite_name, reward_value);
    }

    public void ClickElementToggle()
    {
        UIToggle selectedToggle = UIToggle.GetActiveToggle(20);
        if (selectedToggle != null && selectedToggle.value)
        {            
            string[] element = selectedToggle.name.Split('_');
            hero_Element = (Hero_Element)int.Parse(element[1]);

            if (hero_Element != Hero_Element.none)
            {
                label_Element.alpha = 0;
                icon_Element.alpha = 1;

                Vector2 size = new Vector2(50, 54);
                Utility.ChangeSpriteAspectSnap(icon_Element, string.Format("{0}{1}", "element_icon_", hero_Element.ToString()), size);
                //icon_Element.spriteName = string.Format("{0}{1}", "element_icon_", hero_Element.ToString());
            }
            else
            {
                label_Element.alpha = 1;
                icon_Element.alpha = 0;
            }
            WrapSetting();
            OnOffElementList();
        }
    }

    public void ClickKingdomToggle()
    {
        UIToggle selectedToggle = UIToggle.GetActiveToggle(21);
        if (selectedToggle != null && selectedToggle.value)
        {            
            string[] kingdom = selectedToggle.name.Split('_');
            hero_Kingdom = (Hero_Kingdom)int.Parse(kingdom[1]);

            // ...
            switch(hero_Kingdom)
            {
                case Hero_Kingdom.none:
                    label_Kingdom.text = "국가";
                    break;
                case Hero_Kingdom.wii:
                    label_Kingdom.text = "위나라";
                    break;
                case Hero_Kingdom.chock:
                    label_Kingdom.text = "촉나라";
                    break;
                case Hero_Kingdom.oh:
                    label_Kingdom.text = "오나라";
                    break;
                case Hero_Kingdom.han:
                    label_Kingdom.text = "한나라";
                    break;
                case Hero_Kingdom.etc:
                    label_Kingdom.text = "세외";
                    break;
                case Hero_Kingdom.ancient:
                    label_Kingdom.text = "춘추전국";
                    break;
                case Hero_Kingdom.samurai:
                    label_Kingdom.text = "사무라이";
                    break;
                case Hero_Kingdom.chohan:
                    label_Kingdom.text = "초한쟁패";
                    break;
            }

            WrapSetting();
            OnOffKingdomList();
        }
    }

    public void ClickClassToggle()
    {
        UIToggle selectedToggle = UIToggle.GetActiveToggle(22);
        if (selectedToggle != null && selectedToggle.value)
        {            
            string[] heroClass = selectedToggle.name.Split('_');
            hero_Class= (Hero_Class)int.Parse(heroClass[1]);

            if (hero_Class!= Hero_Class.none)
            {
                label_Class.alpha = 0;
                icon_Class.alpha = 1;

                Vector2 size = new Vector2(56, 50);
                Utility.ChangeSpriteAspectSnap(icon_Class, string.Format("{0}{1}", "class_icon_", hero_Class.ToString()), size);
                //icon_Class.spriteName = string.Format("{0}{1}", "class_icon_", hero_Class.ToString());
            }
            else
            {
                label_Class.alpha = 1;
                icon_Class.alpha = 0;
            }
            WrapSetting();            
            OnOffClassList();
        }
    }


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
