using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : Singleton<Main> {
    public GameObject uiTarget;

    public int current_panel_depth;

    [SerializeField]
    UIRoot ui_Root;

    Sprite[] sprites_portraits;
    Sprite[] sprites_items;
    Sprite[] sprites_playertitle;

    //List<MySingletonPanel> lst_OpenedPanel;

    public enum AchivementTab
    {
        Weekly,
        Daily,
        Hero,
        Player,
        Battle,
        Construct
    }
    private void Start()
    {
        sprites_portraits = Resources.LoadAll<Sprite>("portraits");
        sprites_items = Resources.LoadAll<Sprite>("allitems");
        sprites_playertitle = Resources.LoadAll<Sprite>("playertitle");

        //lst_OpenedPanel = new List<MySingletonPanel>();
        MakeObjectToTarget("UI/Global_Navigation_Panel");
        current_panel_depth = 0;      
        
        if(Utility.CheckScreenRatio4to3())
        {
            ui_Root.manualHeight = 1024;
        }  
    }

    //Vector3 topLeft = new Vector3(-1, 1, 1);
    //Vector3 topRight = new Vector3(1, 1, 1);
    //Vector3 bottomRight = new Vector3(1, -1, 1);
    //Vector3 bottomLeft = new Vector3(-1, -1, 1);

    //private void Update()
    //{
    //    Debug.DrawLine(topLeft, topRight, Color.red);
    //    Debug.DrawLine(topRight, bottomRight, Color.red);
    //    Debug.DrawLine(bottomRight, bottomLeft, Color.red);
    //    Debug.DrawLine(bottomLeft, topLeft, Color.red);
    //}

    public Sprite GetHeroPortraitByName(string fileName)
    {
        if (sprites_portraits == null)
            return null;

        for (int i = 0; i < sprites_portraits.Length; i++)
        {
            if (sprites_portraits[i].name == fileName)
            {
                return sprites_portraits[i];
            }
        }
        return null;
    }
    public Sprite GetItemSpriteByName(string fileName)
    {
        if (sprites_items == null)
            return null;

        for (int i = 0; i < sprites_items.Length; i++)
        {
            if (sprites_items[i].name == fileName)
            {
                return sprites_items[i];
            }
        }
        return null;
    }

    public Sprite GetPlayerTitleSpriteByName(string fileName)
    {
        if (sprites_playertitle == null)
            return null;

        for (int i = 0; i < sprites_playertitle.Length; i++)
        {
            if (sprites_playertitle[i].name == fileName)
            {
                return sprites_playertitle[i];
            }
        }
        return null;
    }

    public AskPanel MakeAskPanel()
    {   
        AskPanel askPanel = MakeObjectToTarget("UI/Ask_Panel").GetComponent<AskPanel>();
        askPanel.gameObject.SetActive(true);

        return askPanel;
    }

    public AskPanel MakeConfirmPanel()
    {
        AskPanel askPanel = MakeObjectToTarget("UI/Confirm_Panel").GetComponent<AskPanel>();
        askPanel.gameObject.SetActive(true);        

        return askPanel;
    }

    /// <summary>    
    /// Scale = Vector3.one
    /// </summary>
    /// <param name="path">"Resources/..."</param>
    public GameObject MakeObjectToTarget(string path)
    {
        GameObject go = Instantiate(Resources.Load(path) as GameObject);
        Transform trans = go.transform;        
        trans.parent = uiTarget.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = Vector3.zero;
        return go;
    }

    /// <summary>    
    /// Scale = Vector3.one
    /// </summary>
    /// <param name="path">"Resources/..."</param>
    /// <param name="target">parent target</param>
    public GameObject MakeObjectToTarget(string path, GameObject target)
    {
        GameObject go = Instantiate(Resources.Load(path) as GameObject);
        Transform trans = go.transform;        
        trans.parent = target.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = Vector3.zero;
        return go;
    }

    /// <summary>    
    /// Scale = Vector3.one, localPosition = Vector3.zero
    /// </summary>
    /// <param name="target">parent target</param>
    public GameObject MakeObjectToTarget(GameObject prefab, GameObject target)
    {
        GameObject go = Instantiate(prefab);
        Transform trans = go.transform;
        trans.parent = target.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = Vector3.zero;
        return go;
    }

    /// <summary>    
    /// Scale = Vector3.one
    /// </summary>
    /// <param name="path">"Resources/..."</param>
    /// <param name="target">parent target</param>
    /// <param name="nameNumber"> object name + nameNumber </param>
    public GameObject MakeObjectToTarget(string path, GameObject target, int nameNumber)
    {
        GameObject go = Instantiate(Resources.Load(path) as GameObject);
        Transform trans = go.transform;
        trans.parent = target.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = Vector3.zero;
        go.name = string.Format ("{0}{1}", go.name, nameNumber);
        return go;
    }

    /// <summary>    
    /// Scale = Vector3.one
    /// </summary>
    /// <param name="path">"Resources/..."</param>
    /// <param name="target">parent target</param>
    /// <param name="local_pos">pos</param>
    public GameObject MakeObjectToTarget(string path, GameObject target, Vector3 local_pos)
    {
        GameObject go = Instantiate(Resources.Load(path) as GameObject);
        Transform trans = go.transform;
        trans.parent = target.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = local_pos;
        return go;
    }

    /// <summary>    
    /// Scale = Vector3.one
    /// </summary>
    /// <param name="path">"Resources/..."</param>
    /// <param name="target">parent target</param>
    public GameObject MakeObjectToTarget(string path, GameObject target, Vector3 local_pos, Vector3 localScale)
    {
        GameObject go = Instantiate(Resources.Load(path) as GameObject);
        Transform trans = go.transform;
        trans.parent = target.transform;
        trans.localScale = localScale;
        trans.localPosition = Vector3.zero;
        return go;
    }

    /// <summary>    
    /// Scale = Vector3.one
    /// </summary>
    /// <param name="path">"Resources/..."</param>
    /// <param name="target">parent target</param>
    /// <param name="local_pos">pos</param>
    public GameObject MakeObjectToTargetAndSetPanelDepth(string path, GameObject target, Vector3 local_pos, int panel_depth)
    {
        GameObject go = Instantiate(Resources.Load(path) as GameObject);
        go.GetComponent<UIPanel>().depth = panel_depth;
        Transform trans = go.transform;
        trans.parent = target.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = local_pos;
        return go;
    }

    public void AddPanel(MyPanel panel)
    {
        //lst_OpenedPanel.Add(panel);
        current_panel_depth += panel.plusDepth;
        panel.SetPanelDepth(current_panel_depth);
    }        

    public void DeletePanelAndDepth(MyPanel myPanel)
    {
        //lst_OpenedPanel.Remove(panel);

        current_panel_depth -= myPanel.plusDepth;
        Destroy(myPanel.gameObject);
    }

    public void DeletePanel(GameObject go_panel)
    {
        //lst_OpenedPanel.Remove(panel);
        Destroy(go_panel);
    }
}
