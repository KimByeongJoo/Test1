using UnityEngine;
using System.Collections;

[System.Serializable]
public class TitleInfo {
    public string       _id;
    public int          _value_min;
    public int          _value_max;
    public int          _order;
    public string       _category;
    public string       _title;
    public string       _sprite; 
    
    public void Set(string id, string value, string order, string category, string title, string sprite)
    {
        _id = id;

        if (value.Length != 0)
        {
            string[] values = value.Split('-');
            _value_min = int.Parse(values[0]);
            _value_max = int.Parse(values[1]);
        }

        if (order.Length != 0)
            _order = int.Parse(order);        
        _category = category;
        _title = title;
        _sprite = sprite;
    }	
}
