using UnityEngine;
using System.Collections;

public class HeroCard : MonoBehaviour {

    [SerializeField]
    UI2DSprite _sprite_hero;

    [SerializeField]
    UISprite _sprite_card;

    [SerializeField]
    UILabel label_name;

    [SerializeField]
    UISprite icon_Element;
    [SerializeField]
    UISprite icon_Class;

    Vector2 raw_ClassIconSize = new Vector2(26, 26);

    HeroPanel.Hero_Element _element;
    HeroPanel.Hero_Class _hero_class;

    public string _id { get; set; }

    [SerializeField]
    bool _isHeroPanel = true;

    void Start()
    {
        UIEventListener event_Listen = UIEventListener.Get(gameObject);
        if (_isHeroPanel)
        {
            event_Listen.onClick += MakeHeroInfoPanel;
        }
        else
        {
            event_Listen.onClick += ClicKMedicineHeroCard;
        }
        _sprite_card = GetComponent<UISprite>();
    }

    public Vector2 GetCardSize()
    {
        return new Vector2(_sprite_card.width, _sprite_card.height);
    }
    public void SetCardHeight(int height)
    {
        _sprite_card.height = height;
    }

    public void Set(Sprite sprite, string name, string id, HeroPanel.Hero_Element element, HeroPanel.Hero_Class hero_class, bool isHeroPanel = true)
    {
        if (sprite)
        {
            _sprite_hero.sprite2D = sprite;
        }

        label_name.text = name;
        _element = element;
        _hero_class = hero_class;
        _id = id;

        Utility.ChangeSpriteAspectSnap(icon_Element, Utility.GetSpriteNameByEnum(element), raw_ClassIconSize);
        Utility.ChangeSpriteAspectSnap(icon_Class, Utility.GetSpriteNameByEnum(hero_class), raw_ClassIconSize);
        
        _isHeroPanel = isHeroPanel;
        //icon_Element.spriteName = ;
        //icon_Class.spriteName = string.Format("{0}{1}", "class_icon_", hero_class);
    }

    public void MakeHeroInfoPanel(GameObject go)
    {
        if (HeroInfoPanel.Instance == null)
        {
            GameObject tempGo = Main.Instance.MakeObjectToTarget("UI/HeroInfo_Panel");
            HeroInfoPanel info = tempGo.GetComponent<HeroInfoPanel>();
            Main.Instance.AddPanel(info);
            info.Set(_element, _hero_class, label_name.text);
        }
    }

    public void ClicKMedicineHeroCard(GameObject go)
    {
        MedicineShopPanel.Instance.ClickHeroCard(this);
    }

    public string GetHeroName()
    {
        return label_name.text;
    }
}
