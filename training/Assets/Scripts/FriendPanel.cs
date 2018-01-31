using UnityEngine;
using System.Text;    
using System.IO;    
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class FriendPanel : Singleton<FriendPanel> {
            
    [SerializeField]
    GameObject button_Prefab;
    [SerializeField]
    UIGrid grid;
    [SerializeField]
    UIWrapContent wrap;
    [SerializeField]
    UIScrollView scrollView;

    [SerializeField]
    UILabel popuplabel;

    [SerializeField]
    UIButton popupButton;
    [SerializeField]
    UISprite presentSendButton;

    [SerializeField]
    UIToggle toggleSortByName;
    
    [SerializeField]
    UIToggle toggleSortByLastLogin;

    [SerializeField]
    GameObject dropDown;
    
    List<FriendButton> lst = new List<FriendButton>();

    List<FriendData> lstDatas = new List<FriendData>();
    
    private void Awake()
    {
        if(wrap != null)
            wrap.onInitializeItem = OnInitializeFriendButton;

        lstDatas = new List<FriendData>();
        
        //wrap dummy data setting
        for (int i = 0; i < 100; i++)
        {
            int rand = Random.Range(0, 100000);
            FriendData data = new FriendData();
            data.SetData(string.Format("id {0}", i), string.Format("name {0}", i), rand, "f1");

            lstDatas.Add(data);
        }
    }
    // Use this for initialization
    void Start ()
    {
        //AddAllButton();
        //AddAllButton2();   

        WrapSetting();        
    }

    void WrapSetting()
    {            
        wrap.minIndex = -(lstDatas.Count - 1);
        if (lstDatas.Count == 1)
        {
            wrap.minIndex = 1;
        }
        wrap.maxIndex = 0;

        if (lstDatas.Count < 10)
        {
            Transform trans = wrap.transform;

            for (int i = lstDatas.Count; i < trans.childCount; i++)
            {
                trans.GetChild(i).gameObject.SetActive(false);
            }
        }

        wrap.SortBasedOnScrollMovement();

        //init sort
        SortingID();

        scrollView.ResetPosition();

        toggleSortByName.value = true;
    }

    void OnInitializeFriendButton(GameObject go, int wrapIndex, int realIndex)
    {
        if (lstDatas.Count <= 0)
            return;
        if (go == null)
            return;

        int lstIndex = realIndex % lstDatas.Count;

        if(lstIndex < 0)
        {
            lstIndex = -lstIndex;
        }
        FriendButton button = go.GetComponent<FriendButton>();        

        button.Set(lstDatas[lstIndex]._id, 
            lstDatas[lstIndex]._name,
            lstDatas[lstIndex]._lastlogin.ToString(),
            lstDatas[lstIndex]._picture,
            lstDatas[lstIndex]._chkBox,
            lstDatas[lstIndex]._deleteMask);
    }    

    void AddAllButton()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("friends");
        JSONNode node = JSON.Parse(textAsset.text);

        for (int i = 0; i < node["data"].Count; i++)
        {
            GameObject tempGameObj = NGUITools.AddChild(grid.gameObject, button_Prefab);
            FriendButton comp = tempGameObj.GetComponent<FriendButton>();
            comp.Set(node["data"][i]["name"],
                node["data"][i]["id"],
                node["data"][i]["lastlogin"],
                node["data"][i]["picture"]);
            lst.Add(comp);
        }
        SortingID();

        toggleSortByName.value = true;

        grid.Reposition();
    }

    // test
    void AddAllButton2()
    {
        int rand = 1;

        for (int i = 0; i < 100; i++)
        {
            GameObject tempGameObj = NGUITools.AddChild(grid.gameObject, button_Prefab);
            FriendButton comp = tempGameObj.GetComponent<FriendButton>();

            rand = Random.Range(0, 100000);
            comp.Set(string.Format("id {0}", i), string.Format("name {0}", i), rand.ToString(), "f1");
            //lst.Add(comp);
        }
        //SortingID();

        //toggleSortByName.value = true;

        //grid.Reposition();
    }

    void SortAllButton()
    {
        for(int i=0; i<grid.GetChildList().Count; i++)
        {
            lst[i].gameObject.transform.SetSiblingIndex(i);
        }
        grid.Reposition();          
    }
    
    public void DropDownOnOff()
    {
        if (dropDown.activeSelf)
            dropDown.SetActive(false);
        else
            dropDown.SetActive(true);        
    }

    public void ChangePopupList()
    {
        if (toggleSortByName.value)
        {
            SortingID();
            UILabel tempLabel = toggleSortByName.gameObject.GetComponent<UILabel>();
            popuplabel.text = tempLabel.text;
        }
        else if (toggleSortByLastLogin.value)
        {
            SortingLastLogin();
            UILabel tempLabel = toggleSortByLastLogin.gameObject.GetComponent<UILabel>();
            popuplabel.text = tempLabel.text;
        }
        else
            dropDown.SetActive(false);

        //popuplabel.text = UIPopupList.current.value; 
    }

    void SortingLastLogin()
    {
        lst.Sort(FriendSortByLastLogin);
        SortAllButton();

        lstDatas.Sort(FriendSortByLastLogin);
        wrap.WrapContent(true);
    }
    void SortingID()
    {        
        lst.Sort(FriendSortByName);
        SortAllButton();

        lstDatas.Sort(FriendSortByName);
        wrap.WrapContent(true);
    }

    static int FriendSortByName(FriendButton x, FriendButton y)
    {
        if (x == null || y == null)
            return 0;

        return x.raw_name.CompareTo(y.raw_name);
    }

    static int FriendSortByName(FriendData x, FriendData y)
    {
        return x._name.CompareTo(y._name);
    }

    static int FriendSortByLastLogin(FriendButton x, FriendButton y)
    {
        if (x == null || y == null)
            return 0;

        return x.lastlogin.CompareTo(y.lastlogin);
    }

    static int FriendSortByLastLogin(FriendData x, FriendData y)
    {       
        return x._lastlogin.CompareTo(y._lastlogin);
    }

    static public void RequestDeleteFriend(string fid)
    {
        if( Instance != null )
        {
            Instance._RequestDeleteFriend(fid);
        }
    }

    void _RequestDeleteFriend(string fid)
    {
        for(int i = lst.Count - 1; i >= 0; i--)
        {
            if(lst[i].raw_friend_id == fid)
            {
                lst[i].DisableButton();
                return;               
            }
        } 
    }

    public void OnClickCheckBox(string raw_friend_id, bool chk)
    {
        FriendData data = FindDataByFriendID(raw_friend_id);

        if(data != null)
            data._chkBox = chk;
    }

    public FriendData FindDataByFriendID(string fid)
    {
        for(int i=0; i< lstDatas.Count; i++)
        {
            if(string.Equals( lstDatas[i]._id,fid))
            {
                return lstDatas[i];
            }
        }
        return null;    
    }

    public List<FriendData> GetPresentSelectedCheckBoxs2()
    {
        List<FriendData> tempLst = new List<FriendData>();

        for (int i = lstDatas.Count - 1; i >= 0; i--)
        {
            if (lstDatas[i]._chkBox == true)
            {
                tempLst.Add(lstDatas[i]);
            }
        }
        return tempLst;
    }

    public List<FriendButton> GetPresentSelectedCheckBoxs()
    {
        List<FriendButton> tempLst = new List<FriendButton>();

        for (int i = lst.Count - 1; i >= 0; i--)
        {
            if (lst[i].present_chk == true)
            {
                tempLst.Add(lst[i]);                
            }
        }
        return tempLst;
    }

    public void SetColorPresentSendButton()
    {
        List<FriendButton> selectedLst = GetPresentSelectedCheckBoxs();

        if(selectedLst.Count > 0)
        {
            presentSendButton.color = Color.white;
        }
        else
        {
            presentSendButton.color = Color.gray;
        }        
    }

    public void OnClickPresentSendButton()
    {
        List<FriendButton> selectedLst = GetPresentSelectedCheckBoxs();

        if (selectedLst.Count > 0)
        {
            AskPanel ask = Main.Instance.MakeAskPanel();
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < selectedLst.Count; i++)
            {
                sb.Append(selectedLst[i].raw_friend_id);
                if(i != selectedLst.Count - 1)
                  sb.Append(", ");
            }            
            sb.Append(" 친구(들)에게 선물을 보내겠습니까?");
            ask.Open("선물 보내기", sb.ToString(), SendPresentYes, null);

            if (ask.GetDescribeLabelSize().y >= 350)
            {
                ask.ScrollViewResetPosition();
            }
        }
        // No check box
        else
        {
            AskPanel confirm = Main.Instance.MakeConfirmPanel();
            confirm.Open("선물 보내기", "선택된 친구가 없습니다.");
        }
    }

    public void OnClickPresentSendButton2()
    {
        List<FriendData> selectedLst = GetPresentSelectedCheckBoxs2();

        if (selectedLst.Count > 0)
        {
            AskPanel ask = Main.Instance.MakeAskPanel();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < selectedLst.Count; i++)
            {
                sb.Append(selectedLst[i]._name);
                if (i != selectedLst.Count - 1)
                    sb.Append(", ");
            }
            sb.Append(" 친구(들)에게 선물을 보내겠습니까?");
            ask.Open("선물 보내기", sb.ToString(), SendPresentYes, null);

            if (ask.GetDescribeLabelSize().y >= 350)
            {
                ask.ScrollViewResetPosition();
            }
        }
        // No check box
        else
        {
            AskPanel confirm = Main.Instance.MakeConfirmPanel();
            confirm.Open("선물 보내기", "선택된 친구가 없습니다.");
        }
    }

    static void SendPresentYes(string param)
    {
        if (Instance != null)
        {
            Instance._SendPresentYes();
        }
    }

    void _SendPresentYes()
    {
        AskPanel confirm = Main.Instance.MakeConfirmPanel();
        confirm.Open("선물 보내기", "선물을 정상적으로 보냈습니다.");        
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
