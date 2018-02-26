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
    
    public void Set(RewardItem reward)
    {
        if (reward.itemKind == "GameItemType")
        {
            frame_item.gameObject.SetActive(true);
            frame_hero.gameObject.SetActive(false);
            icon_sprite.gameObject.SetActive(false);
            icon_sprite2d.gameObject.SetActive(true);
            label_count.gameObject.SetActive(true);
            black_bg.gameObject.SetActive(true);

            ItemTypeData itemData = MyCsvLoad.Instance.GetGameItemTypeByID(reward.itemId);
            label_count.text = reward.count.ToString();

            //icon_sprite2d.sprite2D = Main.Instance.GetItemSpriteByName(itemData._sprite);

            Utility.ChangeSpriteAspectSnap(icon_sprite2d, Main.Instance.GetItemSpriteByName(itemData._sprite),
                new Vector2(icon_sprite2d.width, icon_sprite2d.height));            
        }
        else if (reward.itemKind == "HeroLetter")
        {
            frame_item.gameObject.SetActive(false);
            frame_hero.gameObject.SetActive(true);
            icon_sprite.gameObject.SetActive(false);
            icon_sprite2d.gameObject.SetActive(true);
            label_count.gameObject.SetActive(true);
            black_bg.gameObject.SetActive(true);

            label_count.text = reward.count.ToString();

            icon_sprite2d.sprite2D = Main.Instance.GetHeroPortraitByName(string.Format("hero_{0}", reward.itemId));

            //Utility.ChangeSpriteAspectSnap(icon_sprite2d, Main.Instance.GetHeroPortraitByName(string.Format("hero_{0}", reward.itemId)),
            //    new Vector2(icon_sprite2d.width, icon_sprite2d.height));
            label_starLv.gameObject.SetActive(true);
            label_starLv.text = reward.star;            
        }
        else
        {
            frame_item.gameObject.SetActive(false);
            frame_hero.gameObject.SetActive(false);
            icon_sprite.gameObject.SetActive(true);
            icon_sprite2d.gameObject.SetActive(false);
            label_count.gameObject.SetActive(false);
            black_bg.gameObject.SetActive(false);

            Utility.ChangeSpriteAspectSnap(icon_sprite, reward.itemId, new Vector2(icon_sprite.width, icon_sprite.height));
            label_amount.text = reward.count.ToString();
        }
    }
}
