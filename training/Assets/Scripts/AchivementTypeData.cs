using UnityEngine;
using System.Collections;

[System.Serializable]
public class AchivementTypeData {

    public string   _id;
    public string   _name;
    public int      _order;
    public int      _daily;
    public int      _weekly;
    public string      _tab;
    public string   _category;
    public string   _description;
    public string   _option;    
    public int      _disabled;
    public string   _sprite;

    public void Set(string id, string name, string order, string daily, string weekly, string tab, string category, string description,
        string option, string disabled, string sprite)
    {
        _id = id;
        _name = name;

        if(order.Length != 0)
            _order = int.Parse(order);
        if (daily.Length != 0)
            _daily = int.Parse(daily);
        if (weekly.Length != 0)
            _weekly = int.Parse(weekly);

        _tab = tab;                
        _category = category;
        _description = description;
        _option = option;

        if (disabled.Length != 0)
            _disabled = int.Parse(disabled);
        
        _sprite = sprite;
    }
}
