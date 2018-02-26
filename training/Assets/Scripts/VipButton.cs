using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VipButton : MonoBehaviour {

    [SerializeField]
    UIGrid grid;

    [SerializeField]
    UISprite sprite;

    [SerializeField]
    UILabel label_Vip_Level;

    private void OnDestroy()
    {
        if(ObjectPool.Instance)
            ObjectPool.Instance.RemovePrefab("UI/Reward_ItemBox");
    }

    private void Start()
    {
    }
    public Vector2 GetSize()
    {
        return new Vector2(sprite.width, sprite.height);
    }    
    
    public void Set(int order, AchivementConditionData conditionData, VIPInfo VipInfo)
    {
        Reward reward = new Reward();
        List<RewardItem> items = reward.GetItems();

        label_Vip_Level.text = order.ToString();

        if (conditionData._reward_items.Length != 0)
        {
            // split items
            string[] str_Items = conditionData._reward_items.Split('\n');

            for (int i = 0; i < str_Items.Length; i++)
            {
                RewardItem rewardItem = new RewardItem();

                //split itemKind, Id, count
                string[] item = str_Items[i].Split(' ');
                
                if (item[0] == "HeroLetter")
                {
                    rewardItem.itemKind = "HeroLetter";
                    rewardItem.itemId = item[1];
                    rewardItem.star = item[2];
                    rewardItem.count = 1;
                }
                else
                {
                    rewardItem.itemKind = "GameItemType";
                    rewardItem.itemId = item[0];
                    rewardItem.count = uint.Parse(item[1]);

                }
                items.Add(rewardItem);
            }
        }

        //split end

        items.Sort(SortByID);

        // check overlap
        for(int j = items.Count - 1; j > 0; j--)
        {                
            if(items[j].itemId == items[j - 1].itemId)
            {
                items[j - 1].count += items[j].count;
                items.Remove(items[j]);
            }
        }

        //for (int j = 0; j < items.Count; j++)
        //{
        //    if (items[j].itemId == rewardItem.itemId)
        //    {

        //    }
        //}

        RewardItem goods = new RewardItem();

        if (conditionData._reward_gold > 0)
        {
            goods.count = conditionData._reward_gold;
            goods.itemId = "hud_gold";
        }
        else if(conditionData._reward_food > 0)
        {
            goods.count = conditionData._reward_food;
            goods.itemId = "icon_bread";
        }
        else if(conditionData._reward_cash > 0)
        {
            goods.count = conditionData._reward_cash;
            goods.itemId = "ui_cash";
        }

        items.Add(goods);
        SetRewardItemBox(reward);
    }

    public void SetRewardItemBox(Reward reward)
    {
        List<RewardItem> lst_items = reward.GetItems();        
        
        int childCount = grid.GetChildList().Count;

        int tempChild = childCount - lst_items.Count;

        if(tempChild < 0)
        {
            while (tempChild < 0)
            {
                GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/Reward_ItemBox"), grid.gameObject);
                tempChild++;
            }
        }
        else if(tempChild > 0)
        {
            int tempCount = childCount;
            while (tempChild > 0)
            {
                Destroy(grid.GetChild(tempCount - 1).gameObject);
                tempChild--;
                tempCount--;
            }
        }

        Reward_ItemBox[] itemBoxes = grid.GetComponentsInChildren<Reward_ItemBox>();

        for(int i=0; i< lst_items.Count; i++)
        {
            ItemTypeData itemData = MyCsvLoad.Instance.GetGameItemTypeByID(lst_items[i].itemId);
            itemBoxes[i].Set(lst_items[i]);
        }
        grid.Reposition();     
    }

    int SortByID(RewardItem a, RewardItem b)
    {
        if (a == null || b == null)
            return 0;

        return a.itemId.CompareTo(b.itemId);
    }
}
