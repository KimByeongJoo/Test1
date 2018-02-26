using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MedicineShopPanel : MyPanel
{
    private static MedicineShopPanel _instance;

    public static MedicineShopPanel Instance
    {
        get
        {
            return _instance;
        }
    }

    private void OnDestroy()
    {
        if (Main.Instance)
            Main.Instance.current_panel_depth -= 500;

        _instance = null;
    }

    [SerializeField]
    int card_num = 10;
    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject background_43;
    [SerializeField]
    GameObject background_16;

    [SerializeField]
    GameObject target_43;
    [SerializeField]
    GameObject target_16;

    [SerializeField]
    GameObject grid_star;
    [SerializeField]
    GameObject hero_info;

    [SerializeField]
    UIGrid grid_items;

    [SerializeField]
    GameObject content;

    [SerializeField]
    UIWrapContent wrap;
    [SerializeField]
    UIScrollView scrollView;

    [SerializeField]
    UIScrollView scrollView_rightItems;

    [SerializeField]
    UIScrollBar scrollBar;

    [Header("Panels")]
    [SerializeField]
    UIPanel panel_Scrollbar;
    [SerializeField]
    UIPanel panel_Element_Filter;
    [SerializeField]
    UIPanel panel_Kingdom_Filter;
    [SerializeField]
    UIPanel panel_Class_Filter;

    [SerializeField]
    UIPanel panel_front;

    [SerializeField]
    UIPanel panel_ScrollView;
    [SerializeField]
    UIPanel panel_RightContent;

    [Header("Element")]
    [SerializeField]
    UISprite arrow_Element;
    [SerializeField]
    UISprite icon_Element;
    [SerializeField]
    UILabel label_Element;

    [Header("Kingdom")]
    [SerializeField]
    UISprite arrow_Kingdom;
    [SerializeField]
    UILabel label_Kingdom;

    [Header("Hero_Class")]
    [SerializeField]
    UISprite arrow_Class;
    [SerializeField]
    UISprite icon_Class;
    [SerializeField]
    UILabel label_Class;

    public HeroPanel.Hero_Element hero_Element = HeroPanel.Hero_Element.all;
    public HeroPanel.Hero_Kingdom hero_Kingdom = HeroPanel.Hero_Kingdom.all;
    public HeroPanel.Hero_Class hero_Class = HeroPanel.Hero_Class.all;

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

    List<HeroCard> lst_Cards = new List<HeroCard>();

    Vector3 scrollView_StartPos;
    Vector3 scrollView_SelectedPos;
    Vector2 scrollView_SaveClipOffset;

    string selectedCardID = "";

    [SerializeField]
    UILabel label_Hero_Name;
    
    // Use this for initialization
    private void Awake()
    {
        base.Awake();

        _instance = this;

        if (wrap != null)
            wrap.onInitializeItem = OnInitializeHeroCards;
    }

    void Start()
    {
        // Item x 2
        SetItemDatas();
        SetItemDatas();

        WindowRatioAdjust();

        panel_RightContent.depth = panel.depth + 4;
        panel_ScrollView.depth = panel.depth + 5;
        scrollView_rightItems.panel.depth = panel.depth + 6;
        //panel_Scrollbar.depth = panel.depth + 6;
        panel_front.depth = panel.depth + 7;
        panel_Element_Filter.depth = panel.depth + 8;
        panel_Kingdom_Filter.depth = panel.depth + 9;
        panel_Class_Filter.depth = panel.depth + 10;

        StartCoroutine("initScroll");
    }

    IEnumerator initScroll()
    {
        yield return null;
        AddAllCards();
        WrapSetting();

        scrollView_StartPos = scrollView.panel.transform.localPosition;
        SpringPanel.Begin(scrollView.panel.cachedGameObject, scrollView_StartPos, 8);

        scrollView.ResetPosition();
        scrollView_rightItems.ResetPosition();

        //SetItemButtonWidth();
        StopCoroutine("initScroll");
    }

    public void AddAllCards()
    {
        HeroCard card = AddCard();
        Vector2 cardSize = card.GetCardSize();

        int num = Mathf.CeilToInt(panel_ScrollView.GetViewSize().x / cardSize.x);
        card_num = num + 1;

        for (int i = 0; i < num; i++)
        {
            AddCard();
        }
    }

    public HeroCard AddCard()
    {
        GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/card_bg"), wrap.gameObject);
        HeroCard card = go.GetComponent<HeroCard>();
        card.SetCardHeight(Mathf.CeilToInt(panel_ScrollView.GetViewSize().y - 6));

        lst_Cards.Add(card);
        return card;
    }

    void WindowRatioAdjust()
    {
        if (Utility.CheckScreenRatio4to3())
        {
            background_43.SetActive(true);
            background_16.SetActive(false);
            target = target_43;
        }
        else
        {
            background_43.SetActive(false);
            background_16.SetActive(true);
            target = target_16;
        }
    }    

    void SetItemDatas()
    {
        Transform trans = content.transform;

        //MedicineItemButton[] items = content.GetComponentsInChildren<MedicineItemButton>();

        List<ItemTypeData> datas = MyCsvLoad.Instance.GetGameItemTypeDatas();

        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i] != null)
            {
                if (datas[i]._sub_category == "hero_exp")
                {
                    GameObject go = Main.Instance.MakeObjectToTarget("UI/medicine_item_bg", grid_items.gameObject);
                    MedicineItemButton item = go.GetComponent<MedicineItemButton>();
                    item.Set(datas[i]._sprite, datas[i]._max_stack, datas[i]._name, datas[i]._description);
                }
                //items[i].Set(datas[i]._sprite,datas[i]._max_stack, datas[i]._name, datas[i]._description);
            }
        }
    }

    public void SetItemButtonWidth()
    {
        for (int i = 0; i < grid_items.transform.childCount; i++)
        {
            MedicineItemButton item = grid_items.transform.GetChild(i).GetComponent<MedicineItemButton>();
            if (item != null)
                item.SetWidth((int)scrollView_rightItems.panel.GetViewSize().x);
        }
    }

    [ContextMenu("SortCards")]
    void SortCards()
    {
        for (int i = 0; i < card_num; i++)
        {
            lst_Cards[i].gameObject.transform.SetSiblingIndex(i);
        }
    }

    [ContextMenu("list sort")]
    void ListSortX()
    {
        lst_Cards.Sort(SortHeroCardByPositionX);

        for (int i = 0; i < lst_Cards.Count; i++)
        {
            Debug.Log(lst_Cards[i].transform.localPosition);
        }
    }

    void WrapSetting()
    {
        List<HeroTypeData> type_Data = MyCsvLoad.Instance.GetHeroTypeDatas(hero_Element, hero_Kingdom, hero_Class);

        RemoveSelectedHero(type_Data, selectedCardID);

        wrap.minIndex = 0;
        wrap.maxIndex = type_Data.Count - 1;
        wrap.SortBasedOnScrollMovement();

        lst_Cards.Sort(SortHeroCardByPositionX);
        SortCards();

        Transform trans = wrap.transform;
        if (type_Data.Count < card_num)
        {
            for (int i = 0; i < card_num; i++)
            {
                trans.GetChild(i).gameObject.SetActive((i >= type_Data.Count) ? false : true);
            }
        }
        else
        {
            for (int i = 0; i < card_num; i++)
            {
                trans.GetChild(i).gameObject.SetActive(true);
            }
        }
        wrap.WrapContent(true);
        scrollView.ResetPosition();        
    }

    void OnInitializeHeroCards(GameObject go, int wrapIndex, int realIndex)
    {
        //Debug.Log("real index : " + realIndex + " wrapIndex : " + wrapIndex);
        List<HeroTypeData> typeData = MyCsvLoad.Instance.GetHeroTypeDatas(hero_Element, hero_Kingdom, hero_Class);

        RemoveSelectedHero(typeData, selectedCardID);

        if (typeData.Count <= 0)
            return;
        if (go == null)
            return;

        int lstIndex = realIndex % typeData.Count;

        if (lstIndex < 0)
        {
            lstIndex = -lstIndex;
        }

        HeroCard card = go.GetComponent<HeroCard>();

        card.Set(Main.Instance.GetHeroPortraitByName(typeData[lstIndex]._portrait),
            typeData[lstIndex]._name, typeData[lstIndex]._id, typeData[lstIndex]._element, typeData[lstIndex]._hero_class, false);

    }

    public bool RemoveSelectedHero(List<HeroTypeData> heroDatas, string id)
    {
        if (id == "")
            return false;

        for (int i = heroDatas.Count - 1; i >= 0; i--)
        {
            if (heroDatas[i]._id == id)
            {
                heroDatas.RemoveAt(i);
                return true;
            }
        }
        return false;
    }

    public bool FindHeroByID(List<HeroTypeData> heroDatas, string id)
    {
        if (id == "")
            return false;

        for (int i = heroDatas.Count - 1; i >= 0; i--)
        {
            if (heroDatas[i]._id == id)
            {
                return true;
            }
        }
        return false;
    }
    public void ClickHeroModel()
    {
        scrollView_SelectedPos = scrollView.transform.localPosition;
        scrollView_SaveClipOffset = scrollView.panel.clipOffset;        
        selectedCardID = "";
        WrapSetting();

        Transform trans = target.transform;
        if (trans.childCount > 0)
            trans.DestroyChildren();

        hero_info.SetActive(false);
        SpringPanel.Begin(scrollView.panel.cachedGameObject, scrollView_StartPos, 8);
        
        //SetScrollViewLocalPosition(scrollView, scrollView_SelectedPos);
    }


    public void SetScrollViewLocalPosition(UIScrollView scrollView, Vector3 pos)
    {
        scrollView.panel.cachedTransform.localPosition = pos;
        scrollView.panel.clipOffset = scrollView_SaveClipOffset;
        //scrollView.MoveRelative(Vector3.right);
    }

    public void ClickHeroCard(HeroCard card)
    {
        scrollView_SelectedPos = scrollView.transform.localPosition;

        List<HeroTypeData> typeData = MyCsvLoad.Instance.GetHeroTypeDatas(hero_Element, hero_Kingdom, hero_Class);
        bool existed = FindHeroByID(typeData, selectedCardID);
        bool removed = false;

        selectedCardID = card._id;
        if (!existed)
        {
            removed = RemoveSelectedHero(typeData, selectedCardID);
        }

        if (removed)
        {
            GameObject activeObj = GetLastActiveObject();

            if (activeObj != null)
                activeObj.SetActive(false);

            wrap.maxIndex--;
        }

        label_Hero_Name.text = card.GetHeroName();

        Transform trans = target.transform;
        if (trans.childCount > 0)
            trans.DestroyChildren();

        hero_info.SetActive(true);
        hero_info.transform.position = target.transform.position;

        GameObject go = Main.Instance.MakeObjectToTarget("Unit/tkc-ha_hu_don", target, Vector3.one, Vector3.one * 80);
        Utility.SetSpriteSortingOrderRecursive(go, 1);

        wrap.WrapContent(true);
        Transform wrap_trans = wrap.transform;

        SpringPanel.Begin(scrollView.panel.cachedGameObject, scrollView_SelectedPos, 8);        
    }

    //public Transform GetLastActiveObject()
    //{
    //    Transform trans = wrap.transform;

    //    for (int i = 0; i < trans.childCount; i++)
    //    {
    //        if (trans.GetChild(i).gameObject.activeSelf == false)
    //            return trans.GetChild(i - 1);

    //        if (i == trans.childCount - 1)
    //            return trans.GetChild(i);

    //    }
    //    return null;
    //}

    public GameObject GetLastActiveObject()
    {
        Transform trans = wrap.transform;

        GameObject lastObj;

        List<GameObject> lst_Sort = new List<GameObject>();

        for (int i = 0; i < trans.childCount; i++)
        {
            GameObject child = trans.GetChild(i).gameObject;
            if (child.activeSelf == false)
            {
                continue;
            }
            else
                lst_Sort.Add(child);
        }

        lst_Sort.Sort(SortByLocalPositionX);

        return lst_Sort[lst_Sort.Count - 1];
    }

    static int SortByLocalPositionX(GameObject a, GameObject b)
    {
        if (a == null || b == null)
            return 0;

        return a.transform.localPosition.x.CompareTo(b.transform.localPosition.x);
    }

    static int SortHeroCardByPositionX(HeroCard a, HeroCard b)
    {
        if (a == null || b == null)
            return 0;

        return a.transform.localPosition.x.CompareTo(b.transform.localPosition.x);
    }

    static int SortHeroCardByPositionY(HeroCard a, HeroCard b)
    {
        if (a == null || b == null)
            return 0;

        return a.transform.localPosition.y.CompareTo(b.transform.localPosition.y);
    }

    public void OnOffpanel_Element_Filter()
    {
        if (panel_Element_Filter.alpha == 0)
        {
            AllFilterListClose();
            arrow_Element.flip = UIBasicSprite.Flip.Nothing;
            panel_Element_Filter.alpha = 1;
        }
        else
        {
            arrow_Element.flip = UIBasicSprite.Flip.Vertically;
            panel_Element_Filter.alpha = 0;
        }
    }
    public void OnOffpanel_Kingdom_Filter()
    {
        if (panel_Kingdom_Filter.alpha == 0)
        {
            AllFilterListClose();
            arrow_Kingdom.flip = UIBasicSprite.Flip.Nothing;
            panel_Kingdom_Filter.alpha = 1;
        }
        else
        {
            arrow_Kingdom.flip = UIBasicSprite.Flip.Vertically;
            panel_Kingdom_Filter.alpha = 0;
        }
    }
    public void OnOffpanel_Class_Filter()
    {
        if (panel_Class_Filter.alpha == 0)
        {
            AllFilterListClose();
            arrow_Class.flip = UIBasicSprite.Flip.Nothing;
            panel_Class_Filter.alpha = 1;
        }
        else
        {
            arrow_Class.flip = UIBasicSprite.Flip.Vertically;
            panel_Class_Filter.alpha = 0;
        }
    }

    public void AllFilterListClose()
    {
        panel_Element_Filter.alpha = 0;
        panel_Kingdom_Filter.alpha = 0;
        panel_Class_Filter.alpha = 0;

        arrow_Element.flip = UIBasicSprite.Flip.Vertically;
        arrow_Kingdom.flip = UIBasicSprite.Flip.Vertically;
        arrow_Class.flip = UIBasicSprite.Flip.Vertically;
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

    public void ClickElementToggle()
    {
        Vector2 size = new Vector2(50, 54);

        if (toggle_element_all.value)
        {
            label_Element.alpha = 1;
            icon_Element.alpha = 0;
            hero_Element = HeroPanel.Hero_Element.all;
        }
        else
        {
            ElementIconOn();
        }
        if (toggle_physic.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_physic", size);
            hero_Element = HeroPanel.Hero_Element.physic;
        }
        else if (toggle_fire.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_fire", size);
            hero_Element = HeroPanel.Hero_Element.fire;
        }
        else if (toggle_ice.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_ice", size);
            hero_Element = HeroPanel.Hero_Element.ice;
        }
        else if (toggle_lightning.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_lightning", size);
            hero_Element = HeroPanel.Hero_Element.lightning;
        }
        else if (toggle_poison.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_poison", size);
            hero_Element = HeroPanel.Hero_Element.poison;
        }
        else if (toggle_dark.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_dark", size);
            hero_Element = HeroPanel.Hero_Element.dark;
        }
        else if (toggle_divine.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Element, "element_icon_divine", size);
            hero_Element = HeroPanel.Hero_Element.divine;
        }

        WrapSetting();
        scrollView.ResetPosition();
        SpringPanel.Begin(scrollView.panel.cachedGameObject, scrollView_StartPos, 8);
        OnOffpanel_Element_Filter();
    }

    public void ClickKingdomToggle()
    {
        if (toggle_kingdom_all.value)
        {
            label_Kingdom.text = "국가";
            hero_Kingdom = HeroPanel.Hero_Kingdom.all;
        }
        else if (toggle_wii.value)
        {
            label_Kingdom.text = "위나라";
            hero_Kingdom = HeroPanel.Hero_Kingdom.wii;
        }
        else if (toggle_chock.value)
        {
            label_Kingdom.text = "촉나라";
            hero_Kingdom = HeroPanel.Hero_Kingdom.chock;
        }
        else if (toggle_oh.value)
        {
            label_Kingdom.text = "오나라";
            hero_Kingdom = HeroPanel.Hero_Kingdom.oh;
        }
        else if (toggle_han.value)
        {
            label_Kingdom.text = "한나라";
            hero_Kingdom = HeroPanel.Hero_Kingdom.han;
        }
        else if (toggle_etc.value)
        {
            label_Kingdom.text = "세외";
            hero_Kingdom = HeroPanel.Hero_Kingdom.etc;
        }
        else if (toggle_ancient.value)
        {
            label_Kingdom.text = "춘추전국";
            hero_Kingdom = HeroPanel.Hero_Kingdom.ancient;
        }
        else if (toggle_samurai.value)
        {
            label_Kingdom.text = "사무라이";
            hero_Kingdom = HeroPanel.Hero_Kingdom.samurai;
        }
        else if (toggle_chohan.value)
        {
            label_Kingdom.text = "초한쟁패";
            hero_Kingdom = HeroPanel.Hero_Kingdom.chohan;
        }

        WrapSetting();
        scrollView.ResetPosition();
        SpringPanel.Begin(scrollView.panel.cachedGameObject, scrollView_StartPos, 8);
        OnOffpanel_Kingdom_Filter();
    }

    public void ClickClassToggle()
    {
        Vector2 size = new Vector2(56, 50);

        if (toggle_class_all.value)
        {
            label_Class.alpha = 1;
            icon_Class.alpha = 0;
            hero_Class = HeroPanel.Hero_Class.all;
        }
        else
        {
            ClassIconOn();
        }

        if (toggle_tank.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_tank", size);
            hero_Class = HeroPanel.Hero_Class.tank;
        }
        else if (toggle_rogue.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_rogue", size);
            hero_Class = HeroPanel.Hero_Class.rogue;
        }
        else if (toggle_ranger.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_ranger", size);
            hero_Class = HeroPanel.Hero_Class.ranger;
        }
        else if (toggle_paladin.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_paladin", size);
            hero_Class = HeroPanel.Hero_Class.paladin;
        }
        else if (toggle_wizard.value)
        {
            Utility.ChangeSpriteAspectSnap(icon_Class, "class_icon_wizard", size);
            hero_Class = HeroPanel.Hero_Class.wizard;
        }
        WrapSetting();
        scrollView.ResetPosition();
        SpringPanel.Begin(scrollView.panel.cachedGameObject, scrollView_StartPos, 8);
        OnOffpanel_Class_Filter();
    }

}
