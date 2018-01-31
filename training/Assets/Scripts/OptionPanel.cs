using UnityEngine;
using System.Collections;

public class OptionPanel : MonoBehaviour {

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
        switch(index)
        {
            case OptionTab.Account :
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
        Main.Instance.MakeObjectToTarget("Option_account_tab_content", contentTarget);
    }

    public void ClickGameTab()
    {
        DeleteAllContents();
        SetTabLabelColor(OptionTab.Game);        
        Main.Instance.MakeObjectToTarget("Option_game_tab_content", contentTarget);        
    }

    public void ClickNoticeTab()
    {
        DeleteAllContents();
        SetTabLabelColor(OptionTab.Notice);
        Main.Instance.MakeObjectToTarget("Option_notice_tab_content", contentTarget);
        //Main.Instance.MakeObjectToTarget("Option_game_tab_content", contentTarget);
    }

    public void ClickEtcTab()
    {
        DeleteAllContents();
        SetTabLabelColor(OptionTab.ETC);        

        Main.Instance.MakeObjectToTarget("Option_etc_tab_content", contentTarget);
        //Main.Instance.MakeObjectToTarget("Option_game_tab_content", contentTarget);
    }

    public void DeleteAllContents()
    {
        contentTarget.transform.DestroyChildren();
    }    	

    public void DestroySelf()
    {
        Destroy(gameObject);
    }    
}
