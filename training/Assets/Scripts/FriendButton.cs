using UnityEngine;
using System.Collections;

public class FriendButton : MonoBehaviour
{
    [SerializeField]
    UILabel labelGuild;
    [SerializeField]
    UILabel labelName;
    [SerializeField]
    UILabel labelLastConnect;
    [SerializeField]
    UITexture icon;

    [SerializeField]
    GameObject disable_Mask;

    [SerializeField]
    GameObject buttons;

    [SerializeField]
    UIToggle present_Toggle;

    UIButton uiButton;

    [System.NonSerialized]
    public uint lastlogin;

    [System.NonSerialized]
    public string raw_name;

    [System.NonSerialized]
    public string raw_friend_id;

    [System.NonSerialized]
    public bool present_chk = false;
    
    private void Awake()
    {
        uiButton = GetComponent<UIButton>();
    }

    public void ShowDebugText()
    {
        Debug.Log(labelGuild.text + " " + labelName.text + "님을 선택하였습니다.");
    }    

    public void Set(string friend_id, string name, string lastConnect, string picture)
    {
        lastlogin = uint.Parse(lastConnect);
        raw_name = name;

        raw_friend_id = friend_id;

        labelGuild.text = friend_id;
        labelName.text = name;
                
        labelLastConnect.text =  Utility.GetTimeUnit(lastlogin);

        string[] str = picture.Split('.');
        Texture texture = Resources.Load<Texture>("friends/" + str[0]);
        icon.mainTexture = texture;
    }

    public void Set(string friend_id, string name, string lastConnect, string picture, bool chkBox, bool deleteMask)
    {
        Set(friend_id, name, lastConnect, picture);

        SetCheckBox(chkBox);        

        if (deleteMask)
            DisableButton();
        else
            EnableButton();        
    }

    public void OnClickDelete()
    {
        AskPanel askPanel = Main.Instance.MakeAskPanel();

        askPanel.Open("친구 삭제", "정말 친구를 삭제하시겠습니까?", OnClickFriendDeleteYes, null, raw_friend_id);
        askPanel.SetEventTarget(gameObject);
    }

    void OnClickFriendDeleteYes(string fid)
    {
        FriendPanel.RequestDeleteFriend(fid);

        // 친구 삭제 시 체크 해제
        present_chk = false;
    }   

    public void DisableButton()
    {
        disable_Mask.SetActive(true);
        buttons.SetActive(false);        
    }

    public void EnableButton()
    {
        disable_Mask.SetActive(false);
        buttons.SetActive(true);
    }

    public void OnClickCheckBox()
    {
        present_chk = present_Toggle.value;
        FriendPanel.Instance.SetColorPresentSendButton();

        FriendPanel.Instance.OnClickCheckBox(raw_friend_id, present_chk);
        SetCheckBox(present_chk);
    }    

    public void SetCheckBox(bool chk)
    {
        present_Toggle.value = chk;
        
        if (chk)
        {
            uiButton.normalSprite = "achievement_bg_active";
        }
        else
        {
            uiButton.normalSprite = "achievement_bg";
        }
    }
}

