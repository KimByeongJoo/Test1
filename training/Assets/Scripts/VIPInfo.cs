using UnityEngine;
using System.Collections;

[System.Serializable]
public class VIPInfo {

    public string       _id;
    public uint         _cash_purchase_count;
    public uint         _daily_food;
    public string       _daily_item_name;
    public uint         _daily_item_count;

    public void Set(string id, string cash_purchase_count, string daily_food, string daily_items)
    {
        _id = id;
        if (cash_purchase_count.Length != 0)
            _cash_purchase_count = uint.Parse(cash_purchase_count);
        if (daily_food.Length != 0)
            _daily_food = uint.Parse(daily_food);

        if (daily_items.Length != 0)
        {
            string[] tempStr = daily_items.Split(' ');
            _daily_item_name = tempStr[0];
            if (tempStr[1] != null)
                _daily_item_count = uint.Parse(tempStr[1]);
        }
    }

}
