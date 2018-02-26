using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Reward {

    public uint gold { get; set; }
    public uint food { get; set; }
    public uint cash { get; set; }

    List<RewardItem> items;
    
    public List<RewardItem> GetItems()
    {
        if(items == null)
            items = new List<RewardItem>();
        
        return items;
    }
}
