using UnityEngine;
using System.Collections;

public class Reward_ItemBox : MonoBehaviour {

    public enum Category
    {
        Item,
        Hero,
        Goods
    }

    [SerializeField]
    UIWidget _size;

    public UIWidget GetBoxWidget() { return _size; }

    [SerializeField]
    UISprite frame_item;

    [SerializeField]
    UISprite black_bg;

    [Header("Hero")]
    [SerializeField]
    UISprite frame_hero;
    [SerializeField]
    UILabel label_starLv;

    [SerializeField]
    UI2DSprite icon_sprite2d;

    [Header("Item")]
    [SerializeField]
    UISprite icon_sprite;
    [SerializeField]
    UILabel label_amount;

    [SerializeField]
    UILabel label_count;

    public string save_itemKind  { get; set; }
    public string save_sprite   { get; set; }
    public string save_itemId   { get; set; }
    public string save_star         { get; set; }
    public string save_count        { get; set; }

    private void Awake()
    {
        save_itemKind = "";
        save_sprite = "";
        save_itemId = "";
        save_star = "";
        save_count = "";
    }
    public void SetSize(Vector2 size)
    {
        _size.SetDimensions((int)size.x, (int)size.y);
        icon_sprite2d.UpdateAnchors();            
    }
    public void SetActiveCount(bool active)
    {
        label_count.gameObject.SetActive(active);
    }
    public void Set(RewardItem reward_Item)
    {
        save_itemKind = reward_Item.itemKind;
        
        if (save_itemKind == "GameItemType")
        {
            frame_item.gameObject.SetActive(true);
            frame_hero.gameObject.SetActive(false);
            icon_sprite.gameObject.SetActive(false);
            icon_sprite2d.gameObject.SetActive(true);
            label_count.gameObject.SetActive(true);
            black_bg.gameObject.SetActive(true);

            save_itemId = reward_Item.itemId;
            ItemTypeData itemData = MyCsvLoad.Instance.GetGameItemTypeByID(save_itemId);
            save_count = reward_Item.count.ToString();
            label_count.text = save_count;

            icon_sprite2d.ConvertPortrait = -1;
            //icon_sprite2d.sprite2D = Main.Instance.GetItemSpriteByName(itemData._sprite);
            if (itemData != null)
            {
                save_sprite = itemData._sprite;
                Utility.ChangeSpriteAspectSnap(icon_sprite2d, Main.Instance.GetItemSpriteByName(save_sprite),
                  new Vector2(icon_sprite2d.width, icon_sprite2d.height));

                //Color tempColor = MyCsvLoad.Instance.GetGameItemGradeByID(itemData._grade)._color;                
                frame_item.color = MyCsvLoad.Instance.GetGameItemGradeByGradeID(itemData._grade)._color;
            }
        }
        else if (save_itemKind == "HeroLetter")
        {
            frame_item.gameObject.SetActive(false);
            frame_hero.gameObject.SetActive(true);
            icon_sprite.gameObject.SetActive(false);
            icon_sprite2d.gameObject.SetActive(true);
            label_count.gameObject.SetActive(true);
            black_bg.gameObject.SetActive(true);

            save_count = reward_Item.count.ToString();
            label_count.text = reward_Item.count.ToString();
            save_itemId = reward_Item.itemId;
            icon_sprite2d.sprite2D = Main.Instance.GetHeroPortraitByName(string.Format("hero_{0}", save_itemId));
            icon_sprite2d.ConvertPortrait = 42.5f;

            //Utility.ChangeSpriteAspectSnap(icon_sprite2d, Main.Instance.GetHeroPortraitByName(string.Format("hero_{0}", reward.itemId)),
            //    new Vector2(icon_sprite2d.width, icon_sprite2d.height));
            label_starLv.gameObject.SetActive(true);
            save_star = reward_Item.star;
            label_starLv.text = save_star;

            icon_sprite2d.color = Color.white;
        }
        else
        {
            save_itemKind = "";
            frame_item.gameObject.SetActive(false);
            frame_hero.gameObject.SetActive(false);
            icon_sprite.gameObject.SetActive(true);
            icon_sprite2d.gameObject.SetActive(false);
            label_count.gameObject.SetActive(false);
            black_bg.gameObject.SetActive(false);

            save_itemId = reward_Item.itemId;
            Utility.ChangeSpriteAspectSnap(icon_sprite, save_itemId, new Vector2(icon_sprite.width, icon_sprite.height));
            save_count = reward_Item.count.ToString();            
            label_amount.text = save_count;

            icon_sprite2d.color = Color.white;
        }
    }

    public void Set(string itemKind, string count, string sprite, string itemId, string star, Color color)
    {
        if (itemKind == "GameItemType")
        {
            frame_item.gameObject.SetActive(true);
            frame_hero.gameObject.SetActive(false);
            icon_sprite.gameObject.SetActive(false);
            icon_sprite2d.gameObject.SetActive(true);
            label_count.gameObject.SetActive(true);
            black_bg.gameObject.SetActive(true);
            
            icon_sprite2d.ConvertPortrait = -1;

            label_count.text = count;

            Utility.ChangeSpriteAspectSnap(icon_sprite2d, Main.Instance.GetItemSpriteByName(sprite), 
                new Vector2(icon_sprite2d.width, icon_sprite2d.height));

            frame_item.color = color;            
        }
        else if (itemKind == "HeroLetter")
        {
            frame_item.gameObject.SetActive(false);
            frame_hero.gameObject.SetActive(true);
            icon_sprite.gameObject.SetActive(false);
            icon_sprite2d.gameObject.SetActive(true);
            label_count.gameObject.SetActive(true);
            black_bg.gameObject.SetActive(true);

            label_count.text = count;

            icon_sprite2d.sprite2D = Main.Instance.GetHeroPortraitByName(string.Format("hero_{0}", itemId));
            icon_sprite2d.ConvertPortrait = 42.5f;

            //Utility.ChangeSpriteAspectSnap(icon_sprite2d, Main.Instance.GetHeroPortraitByName(string.Format("hero_{0}", reward.itemId)),
            //    new Vector2(icon_sprite2d.width, icon_sprite2d.height));
            label_starLv.gameObject.SetActive(true);
            label_starLv.text = star;

            frame_item.color = Color.white;
        }
        else
        {
            frame_item.gameObject.SetActive(false);
            frame_hero.gameObject.SetActive(false);
            icon_sprite.gameObject.SetActive(true);
            icon_sprite2d.gameObject.SetActive(false);
            label_count.gameObject.SetActive(false);
            black_bg.gameObject.SetActive(false);

            Utility.ChangeSpriteAspectSnap(icon_sprite, itemId, new Vector2(icon_sprite.width, icon_sprite.height));                        
            label_amount.text = save_count;

            frame_item.color = Color.white;
        }
    }    

    void OnPress(bool press)
    {
        if(VIPRewardPanel.Instance)
            VIPRewardPanel.Instance.PressItemBox(this, press, save_itemKind);
        else if (MedicineShopPanel.Instance)
            MedicineShopPanel.Instance.PressItemBox(this, press, save_itemKind);
    }

    //System.Action<bool,Reward_ItemBox,string> onPressListener;
}
