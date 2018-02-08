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

    public void Set(Sprite sprite, string name, string element, string hero_class)
    {
        if (sprite)
        {
            _sprite.sprite2D = sprite;
        }

        label_name.text = name;
        icon_Element.spriteName = string.Format("{0}{1}", "element_icon_", element);
        icon_Class.spriteName = string.Format("{0}{1}", "class_icon_", hero_class);
    }
}
