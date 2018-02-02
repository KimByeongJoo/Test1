using UnityEngine;
using System.Collections;

[System.Serializable]
public class AchivementConditionData {
    public string       _id;
    public string       _parent;
    public int          _order;
    public int          _level;
    public int          _counter;
    public string       _condition;
    public int          _reward_kingdom_point;
    public int          _reward_exp;
    public int          _reward_gold;
    public int          _reward_cash;
    public int          _reward_food;
    public string       _option;

    public void Set(string id, string parent, string order, string level, string counter, string condition, string reward_kingdom_point,
        string reward_exp, string reward_gold, string reward_cash, string reward_food, string option)
    {
        _id = id;
        _parent = parent;
        if (order.Length != 0)
            _order = int.Parse(order);

        if (level.Length != 0)
            _level = int.Parse(level);
        if (counter.Length != 0)
            _counter = int.Parse(counter);
        
        _condition = condition;

        if (reward_kingdom_point.Length != 0)
            _reward_kingdom_point = int.Parse(reward_kingdom_point);        
        if (reward_exp.Length != 0)
            _reward_exp = int.Parse(reward_exp);
        if (reward_gold.Length != 0)
            _reward_gold = int.Parse(reward_gold);
        if (reward_cash.Length != 0)
            _reward_cash = int.Parse(reward_cash);
        if (reward_food.Length != 0)
            _reward_food = int.Parse(reward_food);
                        
        _option = option;
    }
}                                               
