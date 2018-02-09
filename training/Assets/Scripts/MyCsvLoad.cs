using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using LumenWorks.Framework.IO.Csv;

public class MyCsvLoad : Singleton<MyCsvLoad> {
	[SerializeField]
	GameObject button_Prefab;

    [SerializeField]
    UIGrid grid;
    
    Dictionary<string, AchivementTypeData> AchivementTypeDatas = new Dictionary<string, AchivementTypeData>();
    Dictionary<string, AchivementConditionData> AchivementConditionDatas = new Dictionary<string, AchivementConditionData>();

    Dictionary<string, List<AchivementConditionData>> cachedByParent;

    List<HeroTypeData> heroTypeDatas = new List<HeroTypeData>();
    
    static string[] rarityOrder = { "SSS", "SS", "S", "AAA", "AA", "A", "B", "C", "D" };

    void Awake()
	{
        //LoadServers ();        
        LoadAchivementTypeDatas();
        LoadAchivementConditionDatas();
        LoadHeroTypeDatas();        
    }


    public List<AchivementConditionData> GetCachedByParent(string key)
    {
        if (cachedByParent == null)
            cachedByParent = new Dictionary<string, List<AchivementConditionData>>();

        List<AchivementConditionData> data = null;// new List<AchivementConditionData>();
        
        if (cachedByParent.TryGetValue(key, out data))
        {
            return data;
        }
        else
        {
            data = GetSelectedConditionList(key);
            cachedByParent.Add(key, data);
        }

        return data;
    }
        

    public List<AchivementConditionData> GetSelectedConditionList(string parent_id)
    {
        List<AchivementConditionData> selectedList = new List<AchivementConditionData>();

        using (var e = AchivementConditionDatas.GetEnumerator())
        {
            while (e.MoveNext())
            {
                var value = e.Current.Value;
                if (value._parent == parent_id)
                {
                    selectedList.Add(value);
                }
            }
        }

        return selectedList;
    }

    public List<AchivementTypeData> GetAchivementTypeDivideByTabName(Main.AchivementTab current_tab)
    {   
        List<AchivementTypeData> items = new List<AchivementTypeData>();
        using (var e = AchivementTypeDatas.GetEnumerator())
        {
            while(e.MoveNext())
            {
                var value = e.Current.Value;

                if(current_tab == Main.AchivementTab.Weekly)
                {
                    if (value._weekly == 1)
                    {
                        items.Add(e.Current.Value);
                    }                
                }
                else if (current_tab == Main.AchivementTab.Daily)
                {
                    if (value._daily == 1)
                    {
                        items.Add(e.Current.Value);
                    }                    
                }
                else if (current_tab == Main.AchivementTab.Hero)
                {
                    if (value._tab == "hero")
                    {
                        items.Add(e.Current.Value);
                    }
                }
                else if (current_tab == Main.AchivementTab.Player)
                {
                    if (value._tab == "player")
                    {
                        items.Add(e.Current.Value);
                    }
                }
                else if (current_tab == Main.AchivementTab.Battle)
                {
                    if (value._tab == "battle")
                    {
                        items.Add(e.Current.Value);
                    }
                }
                else if (current_tab == Main.AchivementTab.Construct)
                {
                    if (value._tab == "construct")
                    {
                        items.Add(e.Current.Value);
                    }
                }

            }
        }
        return items;
    }

    public Dictionary<string,AchivementTypeData> GetAchivementTypeDatas()
    {
        return AchivementTypeDatas;
    }

    public Dictionary<string, AchivementConditionData> GetAchivementConditionDatas()
    {
        return AchivementConditionDatas;
    }

    public List<HeroTypeData> GetHeroTypeDatas()
    {
        return heroTypeDatas;
    }

    public bool CheckHaveKingdom(HeroTypeData data, string kingdom)
    {
        string[] str_Split = data._kingdom.Split('\n');

        for (int j = 0; j < str_Split.Length; j++)
        {
            if (str_Split[j] == kingdom)
            {
                return true;
            }
        }
        return false;
    }

    public List<HeroTypeData> GetHeroTypeDatas(HeroPanel.Hero_Element element, HeroPanel.Hero_Kingdom kingdom, HeroPanel.Hero_Class hero_class)
    {
        bool chk_kingdom = false;
        bool chk_element = false;

        List<HeroTypeData> findHeroDatas = new List<HeroTypeData>();
        for (int i = 0; i < heroTypeDatas.Count; i++)
        {
            if (element == HeroPanel.Hero_Element.all && kingdom == HeroPanel.Hero_Kingdom.all && hero_class == HeroPanel.Hero_Class.all)
            {
                findHeroDatas.Add(heroTypeDatas[i]);
                continue;
            }

            // check conditions
            // Kingdom check           
            chk_kingdom = false;
            chk_element = false;

            if (kingdom != HeroPanel.Hero_Kingdom.all)
            {
                chk_kingdom = CheckHaveKingdom(heroTypeDatas[i], kingdom.ToString());
            }
            else
            {
                chk_kingdom = true;
            }

            if (chk_kingdom)
            {
                // element check
                if (element != HeroPanel.Hero_Element.all)
                {
                    if (heroTypeDatas[i]._element == element)
                    {
                        chk_element = true;
                    }
                    else
                    {
                        chk_element = false;
                        continue;
                    }
                }
                else
                {
                    chk_element = true;
                }
                //class check                
                if (chk_element)
                {
                    if (hero_class != HeroPanel.Hero_Class.all)
                    {
                        if (heroTypeDatas[i]._hero_class == hero_class)
                        {
                            findHeroDatas.Add(heroTypeDatas[i]);
                        }
                    }
                    else
                    {
                        findHeroDatas.Add(heroTypeDatas[i]);
                    }
                }
            }
        }
        //findHeroDatas.Sort(CompareHeroDatas);
        return findHeroDatas;
    }    

    public AchivementTypeData GetAchivementTypeDataByID(string id)
    {
        return AchivementTypeDatas[id];
    }

    public AchivementConditionData GetAchivementConditionDataByID(string id)
    {
        return AchivementConditionDatas[id];
    }

    public CsvReader LoadCSVtoPath(string path)
    {
        CsvReader reader;

        TextAsset textAsset = Resources.Load<TextAsset>(path) as TextAsset;

        MemoryStream ms = new MemoryStream(textAsset.bytes);
        return reader = new CsvReader(new StreamReader(ms), true);
    }

    public void LoadHeroTypeDatas()
    {
        CsvReader reader = LoadCSVtoPath("UI/HeroType");
        
        if (reader == null)
            return;

        string[] headers = reader.GetFieldHeaders();
        int index_id = System.Array.IndexOf(headers, "id");
        int index_name = System.Array.IndexOf(headers, "name");
        int index_nickname = System.Array.IndexOf(headers, "nickname");
        int index_category = System.Array.IndexOf(headers, "category");
        int index_kingdom = System.Array.IndexOf(headers, "kingdom");
        int index_class = System.Array.IndexOf(headers, "class");
        int index_gender = System.Array.IndexOf(headers, "gender");
        int index_tier = System.Array.IndexOf(headers, "tier");
        int index_rarity = System.Array.IndexOf(headers, "rarity");
        int index_portrait = System.Array.IndexOf(headers, "portrait");
        int index_playable = System.Array.IndexOf(headers, "playable");
        int index_hide_card = System.Array.IndexOf(headers, "hide_card");
        int index_disabled = System.Array.IndexOf(headers, "disabled");
        int index_element = System.Array.IndexOf(headers, "element");

        heroTypeDatas.Clear();

        while (reader.ReadNextRecord())
        {
            // 제외
            if (reader[index_category] == "hero" && reader[index_playable] == "1" && reader[index_disabled] != "1")
            {
                HeroTypeData data = new HeroTypeData();
                
                HeroPanel.Hero_Class hero_class = GetClassStringToEnum(reader[index_class]);
                HeroPanel.Hero_Element element = GetElementStringToEnum(reader[index_element]);
                 
                data.Set(reader[index_id],
                    reader[index_name], reader[index_nickname], reader[index_category],
                    reader[index_kingdom],
                    hero_class, reader[index_gender],
                    reader[index_tier], reader[index_rarity],
                    reader[index_portrait], reader[index_playable],
                    reader[index_hide_card], reader[index_disabled],
                    element);

                heroTypeDatas.Add(data);
            }
        }

        // Sort
        heroTypeDatas.Sort(CompareHeroDatas);     
    }


    public HeroPanel.Hero_Class GetClassStringToEnum(string str_class)
    {
        switch (str_class)
        {
            case "tank":
                return HeroPanel.Hero_Class.tank;          
            case "rogue":
                return HeroPanel.Hero_Class.rogue;         
            case "ranger":
                return HeroPanel.Hero_Class.ranger;     
            case "paladin":
                return HeroPanel.Hero_Class.paladin;        
            case "wizard":
                return HeroPanel.Hero_Class.wizard;           
            default:
                return HeroPanel.Hero_Class.all;                
        }
    }
    public HeroPanel.Hero_Element GetElementStringToEnum(string str_Element)
    {
        switch (str_Element)
        {
            case "physic":
                return HeroPanel.Hero_Element.physic;
            case "fire":
                return HeroPanel.Hero_Element.fire;
            case "ice":
                return HeroPanel.Hero_Element.ice;
            case "lightning":
                return HeroPanel.Hero_Element.lightning;
            case "poison":
                return HeroPanel.Hero_Element.poison;
            case "dark":
                return HeroPanel.Hero_Element.dark;
            case "divine":
                return HeroPanel.Hero_Element.divine;
            default:
                return HeroPanel.Hero_Element.all;
        }
    }

    static int CompareHeroDatas(HeroTypeData x, HeroTypeData y)
    {
        if (x == null || y == null)
            return 0;

        int result = CompareHeroRarity(x, y);
        
        if(result == 0)
        {
            result = CompareHeroClass(x, y);
        }

        if (result == 0)
        {
            result = CompareHeroName(x,y);
        }

        return result;
    }
    static int CompareHeroName(HeroTypeData x, HeroTypeData y)
    {
        if (x == null || y == null)
            return 0;

        return x._name.CompareTo(y._name);
    }

    static int CompareHeroClass(HeroTypeData x, HeroTypeData y)
    {
        if (x == null || y == null)
            return 0;

        return x._hero_class.CompareTo(y._hero_class); 
    }

    static int CompareHeroRarity(HeroTypeData x, HeroTypeData y)
    {
        if (x == null || y == null)
            return 0;

        return System.Array.IndexOf(rarityOrder, x._rarity).CompareTo(System.Array.IndexOf(rarityOrder, y._rarity));
    }

    public void LoadAchivementTypeDatas()
    {
        CsvReader reader = LoadCSVtoPath("UI/AchievementType");

        if (reader == null)
            return;

        string[] headers = reader.GetFieldHeaders();
        int index_id = System.Array.IndexOf(headers, "id");
        int index_name = System.Array.IndexOf(headers, "name");
        int index_order = System.Array.IndexOf(headers, "order");
        int index_daily = System.Array.IndexOf(headers, "daily");
        int index_weekly = System.Array.IndexOf(headers, "weekly");
        int index_tab = System.Array.IndexOf(headers, "tab");
        int index_category = System.Array.IndexOf(headers, "category");
        int index_description = System.Array.IndexOf(headers, "description");
        int index_option = System.Array.IndexOf(headers, "option");
        int index_disabled = System.Array.IndexOf(headers, "disabled");
        int index_sprite = System.Array.IndexOf(headers, "sprite");
               

        AchivementTypeDatas.Clear();

        while (reader.ReadNextRecord())
        {
            // 제외
            if (reader[index_disabled] == "1")
            {
                continue;
            }
            else if (reader[index_option] == "no_achv")
            {
                continue;
            }
            else if (!(reader[index_category] == "counter"))
            {
                continue;
            }

            AchivementTypeData data = new AchivementTypeData();
                        
            data.Set(reader[index_id],
                reader[index_name], reader[index_order],reader[index_daily], 
                reader[index_weekly],
                reader[index_tab], reader[index_category],
                reader[index_description], reader[index_option],               
                reader[index_disabled], reader[index_sprite]);

            AchivementTypeDatas.Add(reader[index_id], data);            
        }
    }

    public void LoadAchivementConditionDatas()
    {
        CsvReader reader = LoadCSVtoPath("UI/AchievementCondition");

        if (reader == null)
            return;

        string[] headers = reader.GetFieldHeaders();
        int index_id = System.Array.IndexOf(headers, "id");
        int index_parent = System.Array.IndexOf(headers, "parent");
        int index_order = System.Array.IndexOf(headers, "order");
        int index_level = System.Array.IndexOf(headers, "level");
        int index_counter = System.Array.IndexOf(headers, "counter");
        int index_condition = System.Array.IndexOf(headers, "condition");
        int index_reward_kingdom_point = System.Array.IndexOf(headers, "reward kingdom_point");
        int index_reward_exp = System.Array.IndexOf(headers, "reward exp");
        int index_reward_gold = System.Array.IndexOf(headers, "reward gold");
        int index_reward_cash = System.Array.IndexOf(headers, "reward cash");
        int index_reward_food = System.Array.IndexOf(headers, "reward food");                
        int index_option = System.Array.IndexOf(headers, "option");

        AchivementConditionDatas.Clear();

        while (reader.ReadNextRecord())
        {
            AchivementConditionData data = new AchivementConditionData();

            data.Set(reader[index_id],
                reader[index_parent], reader[index_order], reader[index_level],
                reader[index_counter],
                reader[index_condition], reader[index_reward_kingdom_point],
                reader[index_reward_exp], reader[index_reward_gold],
                reader[index_reward_cash], reader[index_reward_food],
                reader[index_option]);

            AchivementConditionDatas.Add(reader[index_id], data);
        }
    }    
}