using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemTypeData {

    public string _id;
    public string _name;
    public string _description;
    public string _short_description;
    public string _category;
    public string _sub_category;
    public string _grade;
    public string _star;
    public string _sprite;
    public int _max_stack;
    public string _option;
    public string _option_client_only;
    public int _price;
    public int _sell_price;
    public int _cash;
    public int _ladder_point;
    public int _boss_point;
    public int _crusade_point;
    public int _arena_point;
    public int _worth;
    public int _disabled;

    public void Set(string id, string name, string description, string short_description, string category, string sub_category, string grade,
        string star, string sprite, string max_stack, string option, string option_client_only, string price, string sell_price, string cash,
        string ladder_point, string boss_point, string crusade_point, string arena_point, string worth, string disabled)
    {
        _id = id;
        _name = name;
        _description = description;            
        _short_description = short_description;
        _category = category;
        _sub_category = sub_category;
        _grade = grade;
        _star = star;
        _sprite = sprite;

        if (max_stack.Length != 0)
            _max_stack = int.Parse(max_stack);

        _option = option;
        _option_client_only = option_client_only;

        if (price.Length != 0)
            _price = int.Parse(price);
        if (sell_price.Length != 0)
            _sell_price = int.Parse(sell_price);
        if (cash.Length != 0)
            _cash = int.Parse(cash);
        if (ladder_point.Length != 0)
            _ladder_point = int.Parse(ladder_point);
        if (boss_point.Length != 0)
            _boss_point = int.Parse(boss_point);
        if (crusade_point.Length != 0)
            _crusade_point = int.Parse(crusade_point);
        if (arena_point.Length != 0)
            _arena_point = int.Parse(arena_point);
        if (worth.Length != 0)
            _worth = int.Parse(worth);
        if (disabled.Length != 0)
            _disabled = int.Parse(disabled);   
    }
}
