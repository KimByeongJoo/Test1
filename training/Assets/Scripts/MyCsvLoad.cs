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

        List<AchivementConditionData> data = new List<AchivementConditionData>();
        
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

    public List<HeroTypeData> GetHeroTypeDatas(HeroPanel.Hero_Element element, HeroPanel.Hero_Kingdom kingdom, HeroPanel.Hero_Class hero_class)
    {
        List<HeroTypeData> findHeroDatas;

        if (element != HeroPanel.Hero_Element.none)
        {
            findHeroDatas = GetHeroDatasElement(heroTypeDatas, element.ToString());
        }
        else
        {
            findHeroDatas = new List<HeroTypeData>(heroTypeDatas);
        }

        if (kingdom != HeroPanel.Hero_Kingdom.none)
        {
            findHeroDatas = findHeroDatas.Where(x => x._kingdom.Contains(kingdom.ToString())).ToList();

            List<HeroTypeData> datas = new List<HeroTypeData>();

            for (int i = 0; i < findHeroDatas.Count; i++)
            {
                string[] str_Split = findHeroDatas[i]._kingdom.Split('\n');

                for(int j = 0; j < str_Split.Length; j++)
                {
                    if (str_Split[j] == kingdom.ToString())
                    {
                        datas.Add(findHeroDatas[i]);
                        break;
                    }
                }                
            }

            findHeroDatas = datas;
        }

        if (hero_class != HeroPanel.Hero_Class.none)
        {
            findHeroDatas = findHeroDatas.Where(x => x._hero_class.Contains(hero_class.ToString())).ToList();
        }       
            
        return findHeroDatas;
    }

    public List<HeroTypeData> GetHeroDatasElement(List<HeroTypeData> lst, string element)
    {        
        //heroTypeDatas = heroTypeDatas.OrderBy(node => node.Value._name).ToDictionary(pair => pair.Key, pair => pair.Value);

        var result = lst.Where(x => x._element.Contains(element)).ToList();
        
        return result;
    }

    public List<HeroTypeData> GetHeroDatasKingdom(List<HeroTypeData> lst, string kingdom)
    {
        var result = lst.Where(x => x._kingdom == kingdom).ToList();
                
        return result;
    }

    public List<HeroTypeData> GetHeroDatasClass(List<HeroTypeData> lst, string hero_class)
    {
        var result = lst.Where(x => x._hero_class == hero_class).ToList();        

        return result;
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

                data.Set(reader[index_id],
                    reader[index_name], reader[index_nickname], reader[index_category],
                    reader[index_kingdom],
                    reader[index_class], reader[index_gender],
                    reader[index_tier], reader[index_rarity],
                    reader[index_portrait], reader[index_playable],
                    reader[index_hide_card], reader[index_disabled],
                    reader[index_element]);

                heroTypeDatas.Add(data);
            }
        }

        // Sort Name
        heroTypeDatas = heroTypeDatas.OrderBy(x => x._name).ToList();

        // Sort Hero Class
        string[] classOrder = { "tank", "paladin", "ranger", "rogue", "wizard" };
        heroTypeDatas = heroTypeDatas.OrderBy(x => System.Array.IndexOf(classOrder, x._hero_class)).ToList();

        // Sort Rarity
        string[] rarityOrder = { "SSS", "SS", "S", "AAA", "AA", "A", "B", "C", "D" };
        heroTypeDatas = heroTypeDatas.OrderBy(x => System.Array.IndexOf(rarityOrder, x._rarity)).ToList();
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
    /*
    void LoadServers()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("servers") as TextAsset;

        MemoryStream ms = new MemoryStream(textAsset.bytes);
        reader = new CsvReader(new StreamReader(ms), true);

        string[] headers = reader.GetFieldHeaders();
        int index_id = System.Array.IndexOf(headers, "id");
        int index_name = System.Array.IndexOf(headers, "name");

        while (reader.ReadNextRecord())
        {
            var go = NGUITools.AddChild(grid.gameObject, button_Prefab);

            ShowServerName comp = go.GetComponent<ShowServerName>();

            comp.Set(reader[index_id], reader[index_name]);
             
             //int r = Random.Range (10, 50);
             //int r2 = Random.Range (40000, 60000);
             //go.transform.Find ("label_top_second").GetComponent<UILabel> ().text = "[00ff00]원활[-]\n[606060]Lv" + r + "킹덤 " + r2;
             
            // 0 == id
            // 1 = name
        }
        grid.Reposition();
    }
    */
}