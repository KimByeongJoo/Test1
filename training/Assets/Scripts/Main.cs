using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : Singleton<Main> {
    public GameObject uiTarget;

    public int current_panel_depth;

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
        //lst_OpenedPanel = new List<MySingletonPanel>();
        MakeObjectToTarget("UI/Global_Navigation_Panel");
        current_panel_depth = 0;
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
        current_panel_depth += 50;
        panel.SetPanelDepth(current_panel_depth);
    }

    //public int GetTopDepth()
    //{
    //    if (lst_OpenedPanel.Count == 0)
    //        return 0;

    //    int top_depth = 0;

    //    for(int i = 0; i < lst_OpenedPanel.Count; i++)
    //    {
    //        int panel_depth = lst_OpenedPanel[i].GetPanelDepth();

    //        if (panel_depth > top_depth)
    //        {
    //            top_depth = panel_depth;
    //        }
    //    }

    //    return top_depth;
    //}

    public void DeletePanelAndDepth(GameObject go_panel)
    {
        //lst_OpenedPanel.Remove(panel);
        Destroy(go_panel);
        current_panel_depth -= 50;
    }

    public void DeletePanel(GameObject go_panel)
    {
        //lst_OpenedPanel.Remove(panel);
        Destroy(go_panel);
    }
}
