using UnityEngine;
using System.Collections;

[System.Serializable]
public class HeroTypeData {

    public string _id;
    public string _name;
    public string _nickname;
    public string _category;
    public string _kingdom;
    public HeroPanel.Hero_Class _hero_class;
    public string _gender;
    public string _tier;
    public string _rarity;
    public string _portrait;
    public int      _playable;
    public int      _hide_card;
    public int      _disabled;
    public HeroPanel.Hero_Element _element;

    public void Set(string id, string name, string nickname, string category, string kingdom, HeroPanel.Hero_Class hero_class,
        string gender, string tier, string rarity, string portrait, string playable, string hide_card, string disabled, HeroPanel.Hero_Element element)
    {
        _id = id;
        _name = name;
        
        _nickname = nickname;
        _category = category;
        _kingdom = kingdom;
        _hero_class = hero_class;
        _gender = gender;
        _tier = tier;
        _rarity = rarity;
        _portrait = portrait;
        
        if (playable.Length != 0)
            _playable = int.Parse(playable);
        if (hide_card.Length != 0)
            _hide_card = int.Parse(hide_card);
        if (disabled.Length != 0)
            _disabled = int.Parse(disabled);

        _element = element;
    }
}
