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

    public void Set(Sprite sprite, string name, string element, string hero_class)
    {
        if (sprite)
        {
            _sprite.sprite2D = sprite;
        }

        label_name.text = name;
        Utility.ChangeSpriteAspectSnap(icon_Element, string.Format("element_icon_{0}", element), raw_ClassIconSize);
        Utility.ChangeSpriteAspectSnap(icon_Class, string.Format("class_icon_{0}", hero_class), raw_ClassIconSize);
        //icon_Element.spriteName = ;
        //icon_Class.spriteName = string.Format("{0}{1}", "class_icon_", hero_class);
    }
}
