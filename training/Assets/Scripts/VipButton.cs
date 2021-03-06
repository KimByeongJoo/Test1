﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VipButton : MonoBehaviour {

    [SerializeField]
    UIGrid grid;

    [SerializeField]
    UIGrid grid_daily;
    [SerializeField]
    UILabel label_daily;

    [SerializeField]
    UISprite sprite;

    [SerializeField]
    UILabel label_Vip_Level;
    
    [SerializeField]
    GameObject mask;

    List<Reward_ItemBox> lst_ItemBoxes;
    List<Reward_ItemBox> lst_ItemBoxes_Daily;

    Vector2 size = new Vector2(65,65);

    private void OnDestroy()
    {
        if(ObjectPool.Instance)
            ObjectPool.Instance.RemovePrefab("UI/Reward_ItemBox");
    }

    private void Awake()
    {
        lst_ItemBoxes = new List<Reward_ItemBox>();
        lst_ItemBoxes_Daily = new List<Reward_ItemBox>();
    }
    public Vector2 GetSize()
    {
        return new Vector2(sprite.width, sprite.height);
    }    
    
    public void Set(int order, AchivementConditionData conditionData, VIPInfo vipInfo)
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

        // check overlap
        items.Sort(SortByID);        
        for(int j = items.Count - 1; j > 0; j--)
        {                
            if(items[j].itemId == items[j - 1].itemId)
            {
                items[j - 1].count += items[j].count;
                items.Remove(items[j]);
            }
        }

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
        goods.itemKind = "";
        items.Add(goods);        
        SetRewardItemBox(reward);

        SetDaily(vipInfo);

        // mask
        if(!VIPRewardPanel.Instance.CheckVipRewarded(order - 1))
        {
            mask.SetActive(false);
        }
        else
        {
            mask.SetActive(true);
        }
    }

    public void SetDaily(VIPInfo vipInfo)
    {
        if (vipInfo._daily_item_count <= 0)
        {
            label_daily.gameObject.SetActive(false);
            grid_daily.gameObject.SetActive(false);
            return;
        }
        else
        {
            label_daily.gameObject.SetActive(true);
            grid_daily.gameObject.SetActive(true);            
        }

        int diffCount = 2 - grid_daily.GetChildList().Count;

        for (int i = 0; i < lst_ItemBoxes_Daily.Count; i++)
        {
            lst_ItemBoxes_Daily[i].gameObject.SetActive(true);
        }

        while (diffCount > 0)
        {
            GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/Reward_ItemBox"), grid_daily.gameObject);
            lst_ItemBoxes_Daily.Add(go.GetComponent<Reward_ItemBox>());
            diffCount--;
        }
                
        RewardItem reward_Item = new RewardItem();
        reward_Item.itemKind = "GameItemType";
        reward_Item.itemId = vipInfo._daily_item_name;
        reward_Item.count = vipInfo._daily_item_count;
        lst_ItemBoxes_Daily[0].SetSize(size);
        lst_ItemBoxes_Daily[0].Set(reward_Item);

        RewardItem reward_Item2 = new RewardItem();
        reward_Item.itemKind = "";
        reward_Item2.itemId = "icon_bread";
        reward_Item2.count = vipInfo._daily_food;
        lst_ItemBoxes_Daily[1].SetSize(size);
        lst_ItemBoxes_Daily[1].Set(reward_Item2);
    }

    public void SetRewardItemBox(Reward reward)
    {
        List<RewardItem> lst_items = reward.GetItems();        
        
        int childCount = grid.GetChildList().Count;

        int diffChild = childCount - lst_items.Count;

        for(int i = 0; i < lst_ItemBoxes.Count; i++)
        {
            lst_ItemBoxes[i].gameObject.SetActive(true);
        }

        if(diffChild < 0)
        {
            while (diffChild < 0)
            {
                GameObject go = Main.Instance.MakeObjectToTarget(ObjectPool.Instance.GetPrefab("UI/Reward_ItemBox"), grid.gameObject);
                lst_ItemBoxes.Add(go.GetComponent<Reward_ItemBox>());
                diffChild++;
            }
        }
        else if(diffChild > 0)
        {
            int index = childCount;
            while (diffChild > 0)
            {
                lst_ItemBoxes[index - 1].gameObject.SetActive(false);                
                diffChild--;
                index--;
            }
        }
        
        for(int i=0; i< lst_items.Count; i++)
        {
            ItemTypeData itemData = MyCsvLoad.Instance.GetGameItemTypeByID(lst_items[i].itemId);
            lst_ItemBoxes[i].SetSize(size);
            lst_ItemBoxes[i].Set(lst_items[i]);

        }
        grid.Reposition();     
    }

    int SortByID(RewardItem a, RewardItem b)
    {
        if (a == null || b == null)
            return 0;

        return a.itemId.CompareTo(b.itemId);
    }

    public void ClickButton()
    {
        mask.gameObject.SetActive(true);

        VIPRewardPanel.Instance.SetVipRewarded(int.Parse(label_Vip_Level.text) - 1, true);
    }
}
