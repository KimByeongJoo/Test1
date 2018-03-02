using UnityEngine;
using System.Collections;

public class MedicineItemButton : MonoBehaviour {

    [SerializeField]
    UISprite sprite_button;

    [SerializeField]
    UI2DSprite sprite_icon;
    [SerializeField]
    UILabel label_count;
    [SerializeField]
    UILabel label_name;
    [SerializeField]
    UILabel label_desciption;

    [SerializeField]
    Reward_ItemBox reward_ItemBox;

    Vector2 rawSize = Vector2.one;

    private void Awake()
    {
        rawSize.x = 66;
        rawSize.y = 64;
    }

    public void Set(ItemTypeData data)
    {
        //Utility.ChangeSpriteAspectSnap(sprite_icon, Main.Instance.GetItemSpriteByName(data._sprite), rawSize);
                
        label_count.text = data._max_stack.ToString();
        label_name.text = data._name;
        label_desciption.text = data._description;

        //Color tempColor = MyCsvLoad.Instance.GetGameItemGradeByGradeID(data._grade)._color;
        RewardItem item = new RewardItem();
        item.itemId = data._id;
        item.count = data._max_stack;
        item.itemKind = "GameItemType";
        item.star = data._star;

        reward_ItemBox.Set(item);
    }
    public void SetWidth(int x)
    {
        sprite_button.width = x;        
    }

    public void SetHeight(int y)
    {
        sprite_button.height = y;
    }
    
}
