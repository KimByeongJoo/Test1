using UnityEngine;
using System.Collections;

public class OptionPanel : MyPanel
{
    private static OptionPanel _instance;

    public static OptionPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(OptionPanel)) as OptionPanel;
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
    
    [SerializeField]
    GameObject contentTarget;

    [SerializeField]
    UILabel tab_account_label;
    [SerializeField]
    UILabel tab_game_label;
    [SerializeField]
    UILabel tab_notice_label;
    [SerializeField]
    UILabel tab_etc_label;

    public enum OptionTab
    {
        Account,
        Game,
        Notice,
        ETC,
        End
    }
    public void SetTabLabelColor(OptionTab index)
    {
        switch (index)
        {
            case OptionTab.Account:
                {
                    Utility.SetLabelColor(tab_account_label, Color.white);
                    Utility.SetLabelColor(tab_game_label, Color.gray);
                    Utility.SetLabelColor(tab_notice_label, Color.gray);
                    Utility.SetLabelColor(tab_etc_label, Color.gray);
                }
                break;
            case OptionTab.Game:
                {
                    Utility.SetLabelColor(tab_account_label, Color.gray);
                    Utility.SetLabelColor(tab_game_label, Color.white);
                    Utility.SetLabelColor(tab_notice_label, Color.gray);
                    Utility.SetLabelColor(tab_etc_label, Color.gray);
                }
                break;
            case OptionTab.Notice:
                {
                    Utility.SetLabelColor(tab_account_label, Color.gray);
                    Utility.SetLabelColor(tab_game_label, Color.gray);
                    Utility.SetLabelColor(tab_notice_label, Color.white);
                    Utility.SetLabelColor(tab_etc_label, Color.gray);
                }
                break;
            case OptionTab.ETC:
                {
                    Utility.SetLabelColor(tab_account_label, Color.gray);
                    Utility.SetLabelColor(tab_game_label, Color.gray);
                    Utility.SetLabelColor(tab_notice_label, Color.gray);
                    Utility.SetLabelColor(tab_etc_label, Color.white);
                }
                break;
        }
    }

    private void Start()
    {
        ClickAccountTab();
    }
    public void ClickAccountTab()
    {
        DeleteAllContents();
        SetTabLabelColor(OptionTab.Account);
        GameObject go = Main.Instance.MakeObjectToTarget("UI/Option_account_tab_content", contentTarget);
        go.GetComponent<UIPanel>().depth = panel.depth + 5;
    }

    public void ClickGameTab()
    {
        DeleteAllContents();
        SetTabLabelColor(OptionTab.Game);
        GameObject go = Main.Instance.MakeObjectToTarget("UI/Option_game_tab_content", contentTarget);
        go.GetComponent<UIPanel>().depth = panel.depth + 5;
    }

    public void ClickNoticeTab()
    {
        DeleteAllContents();
        SetTabLabelColor(OptionTab.Notice);
        GameObject go = Main.Instance.MakeObjectToTarget("UI/Option_notice_tab_content", contentTarget);
        go.GetComponent<UIPanel>().depth = panel.depth + 5;        
    }

    public void ClickEtcTab()
    {
        DeleteAllContents();
        SetTabLabelColor(OptionTab.ETC);

        GameObject go = Main.Instance.MakeObjectToTarget("UI/Option_etc_tab_content", contentTarget);
        go.GetComponent<UIPanel>().depth = panel.depth + 5;
        
    }

    public void DeleteAllContents()
    {
        contentTarget.transform.DestroyChildren();
    }
}
