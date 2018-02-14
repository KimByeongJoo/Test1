using UnityEngine;
using System.Collections;

public class HeroCard : MonoBehaviour {

    [SerializeField]
    UI2DSprite _sprite;
    [SerializeField]
    UILabel label_name;

    [SerializeField]
    UISprite icon_Element;
    [SerializeField]
    UISprite icon_Class;

    Vector2 raw_ClassIconSize = new Vector2(26, 26);

    HeroPanel.Hero_Element _element;
    HeroPanel.Hero_Class _hero_class;

    void Start()
    {
        UIEventListener event_Listen = UIEventListener.Get(gameObject);
        event_Listen.onClick += MakeHeroInfoPanel;
    }

    public void Set(Sprite sprite, string name, HeroPanel.Hero_Element element, HeroPanel.Hero_Class hero_class)
    {
        if (sprite)
        {
            _sprite.sprite2D = sprite;
        }

        label_name.text = name;
        _element = element;
        _hero_class = hero_class;

        Utility.ChangeSpriteAspectSnap(icon_Element, Utility.GetSpriteNameByEnum(element), raw_ClassIconSize);
        Utility.ChangeSpriteAspectSnap(icon_Class, Utility.GetSpriteNameByEnum(hero_class), raw_ClassIconSize);
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
}
