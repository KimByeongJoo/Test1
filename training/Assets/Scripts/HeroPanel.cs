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
    
    public enum Hero_Element
    {
        all,
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
        all,
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
        all,
        tank,
        rogue,
        ranger,
        paladin,
        wizard
    }
    
    Sprite[] sprites;

    [SerializeField]
    UIWrapContent wrap;
        
    [SerializeField]
    UIScrollView scrollView;

    [SerializeField]
    UIPanel panel_Scrollbar;

    [Header("Panels")]
    [SerializeField]
    UIPanel panel_Element_Filter;
    [SerializeField]
    UIPanel panel_Kingdom_Filter;
    [SerializeField]
    UIPanel panel_Class_Filter;

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

    public Hero_Element hero_Element    = Hero_Element.all;
    public Hero_Kingdom hero_Kingdom    = Hero_Kingdom.all;
    public Hero_Class   hero_Class      = Hero_Class.all;

    [Header("Toggle Element")]
    [SerializeField]
    UIToggle toggle_element_all;
    [SerializeField]
    UIToggle toggle_physic;
    [SerializeField]
    UIToggle toggle_fire;
    [SerializeField]
    UIToggle toggle_ice;
    [SerializeField]
    UIToggle toggle_lightning;
    [SerializeField]
    UIToggle toggle_poison;
    [SerializeField]
    UIToggle toggle_dark;
    [SerializeField]
    UIToggle toggle_divine;

    [Header("Toggle Kingdom")]
    [SerializeField]
    UIToggle toggle_kingdom_all;
    [SerializeField]
    UIToggle toggle_wii;
    [SerializeField]
    UIToggle toggle_chock;
    [SerializeField]
    UIToggle toggle_oh;
    [SerializeField]
    UIToggle toggle_han;
    [SerializeField]
    UIToggle toggle_etc;
    [SerializeField]
    UIToggle toggle_ancient;
    [SerializeField]
    UIToggle toggle_samurai;
    [SerializeField]
    UIToggle toggle_chohan;

    [Header("Toggle Class")]
    [SerializeField]
    UIToggle toggle_class_all;
    [SerializeField]
    UIToggle toggle_tank;
    [SerializeField]
    UIToggle toggle_rogue;
    [SerializeField]
    UIToggle toggle_ranger;
    [SerializeField]
    UIToggle toggle_paladin;
    [SerializeField]
    UIToggle toggle_wizard;
    
    int card_num = 7;

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
        panel_Scrollbar.depth = panel.depth + 6;
        panel_Element_Filter.depth = panel.depth + 7;
        panel_Kingdom_Filter.depth = panel.depth + 8;
        panel_Class_Filter.depth = panel.depth + 9;

    }
    public void AllFilterListClose()
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
            AllFilterListClose();
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
            AllFilterListClose();            
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
            AllFilterListClose();            
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
        
        int column = type_Data.Count / card_num;

        if (type_Data.Count % card_num > 0)
        {
            column++;
        }

        if (column == 1)
        {
            wrap.minIndex = -1;
        }
        else
        {
            wrap.minIndex = -(column - 1);
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
        //Debug.Log("real index : " + realIndex + " wrapIndex : " + wrapIndex);
        List<HeroTypeData> typeData = MyCsvLoad.Instance.GetHeroTypeDatas(hero_Element, hero_Kingdom, hero_Class);
        
        if (typeData.Count <= 0)
            return;
        if (go == null)
            return;
        
        int column = typeData.Count / card_num;
        int remain = typeData.Count % card_num;

        if (remain > 0)
        {
            column++;
        }
        else if (column != 0 && remain == 0)
        {
            remain = card_num;
        }
        
        int lstIndex = realIndex % column;

        if (lstIndex < 0)
        {
            lstIndex = -lstIndex;
        }
        Hero7CardSet cardSet = go.GetComponent<Hero7CardSet>();
        
        for (int j = 0; j < card_num; j++)
        {
            if (lstIndex == column - 1 && j >= remain)
            {
                cardSet.cards[j].gameObject.SetActive(false);
            }
            else
            {
                cardSet.cards[j].gameObject.SetActive(true);
                HeroTypeData data = typeData[card_num * lstIndex + j];
                cardSet.cards[j].Set(GetCardSpriteByName(data._portrait), data._name, data._element.ToString(), data._hero_class.ToString());
            }
        }
        
        //button.Set(typeData[lstIndex]._name, _description,
        //    reward_kingdom_or_exp, sprite_name, reward_value);
    }

    public void ClickElementToggle()
    {        
        Vector2 size = new Vector2(50, 54);

        if (toggle_element_all.value)
        {
            label_Element.alpha = 1;
            icon_Element.alpha = 0;
            hero_Element = Hero_Element.all;
        }
        else if (toggle_physic.value)
        {
            ElementIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_physic", size);
            hero_Element = Hero_Element.physic;
        }
        else if (toggle_fire.value)
        {
            ElementIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_fire", size);
            hero_Element = Hero_Element.fire;
        }        
        else if (toggle_ice.value)
        {
            ElementIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_ice", size);
            hero_Element = Hero_Element.ice;
        }
        else if (toggle_lightning.value)
        {
            ElementIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_lightning", size);
            hero_Element = Hero_Element.lightning;
        }
        else if (toggle_poison.value)
        {
            ElementIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_poison", size);
            hero_Element = Hero_Element.poison;
        }
        else if (toggle_dark.value)
        {
            ElementIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_dark", size);
            hero_Element = Hero_Element.dark;
        }
        else if (toggle_divine.value)
        {
            ElementIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_divine", size);
            hero_Element = Hero_Element.divine;
        }

        WrapSetting();
        OnOffElementList();
        
    }
    public void ElementIconOn()
    {
        label_Element.alpha = 0;
        icon_Element.alpha = 1;
    }
    public void ClassIconOn()
    {
        label_Class.alpha = 0;
        icon_Class.alpha = 1;
    }

    public void ClickKingdomToggle()
    {
        Vector2 size = new Vector2(50, 54);

        if (toggle_kingdom_all.value)
        {                        
            label_Kingdom.text = "국가";
            hero_Kingdom = Hero_Kingdom.all;
        }
        else if (toggle_wii.value)
        {          
            label_Kingdom.text = "위나라";
            hero_Kingdom = Hero_Kingdom.wii;
        }
        else if (toggle_chock.value)
        {         
            label_Kingdom.text = "촉나라";
            hero_Kingdom = Hero_Kingdom.chock;
        }
        else if (toggle_oh.value)
        {         
            label_Kingdom.text = "오나라";
            hero_Kingdom = Hero_Kingdom.oh;
        }
        else if (toggle_han.value)
        {
            label_Kingdom.text = "한나라";
            hero_Kingdom = Hero_Kingdom.han;
        }
        else if (toggle_etc.value)
        {
            label_Kingdom.text = "세외";
            hero_Kingdom = Hero_Kingdom.etc;
        }
        else if (toggle_ancient.value)
        {
            label_Kingdom.text = "춘추전국";
            hero_Kingdom = Hero_Kingdom.ancient;
        }
        else if (toggle_samurai.value)
        {
            label_Kingdom.text = "사무라이";
            hero_Kingdom = Hero_Kingdom.samurai;
        }
        else if (toggle_chohan.value)
        {
            label_Kingdom.text = "초한쟁패";
            hero_Kingdom = Hero_Kingdom.chohan;
        }

        WrapSetting();
        OnOffKingdomList();
    }

    public void ClickClassToggle()
    {
        Vector2 size = new Vector2(56, 50);

        if (toggle_class_all.value)
        {
            label_Class.alpha = 1;
            icon_Class.alpha = 0;
            hero_Class = Hero_Class.all;
        }
        else if (toggle_tank.value)
        {
            ClassIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_tank", size);
            hero_Class = Hero_Class.tank;
        }
        else if (toggle_rogue.value)
        {
            ClassIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_rogue", size);
            hero_Class = Hero_Class.rogue;
        }
        else if (toggle_ranger.value)
        {
            ClassIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_ranger", size);
            hero_Class = Hero_Class.ranger;
        }
        else if (toggle_paladin.value)
        {
            ClassIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_paladin", size);
            hero_Class = Hero_Class.paladin;
        }
        else if (toggle_wizard.value)
        {
            ClassIconOn();
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_wizard", size);
            hero_Class = Hero_Class.wizard;
        }        
        WrapSetting();            
        OnOffClassList();        
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
