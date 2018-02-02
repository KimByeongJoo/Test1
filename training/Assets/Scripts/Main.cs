using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : Singleton<Main> {
    public GameObject uiTarget;
    
    public enum AchivementTab
    {
        Weekly,
        Daily,
        Hero,
        Player,
        Battle,
        Construct
    }

    public AskPanel MakeAskPanel()
    {
        AskPanel askPanel = MakeObjectToTarget("Ask_Panel").GetComponent<AskPanel>();
        askPanel.gameObject.SetActive(true);
        return askPanel;
    }

    public AskPanel MakeConfirmPanel()
    {
        AskPanel askPanel = MakeObjectToTarget("Confirm_Panel").GetComponent<AskPanel>();
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
    public GameObject MakeObjectToTarget(string path, GameObject target, Vector3 local_pos)
    {
        GameObject go = Instantiate(Resources.Load(path) as GameObject);
        Transform trans = go.transform;
        trans.parent = target.transform;
        trans.localScale = Vector3.one;
        trans.localPosition = local_pos;
        return go;
    }
}
