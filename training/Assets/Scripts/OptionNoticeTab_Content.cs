using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionNoticeTab_Content : MonoBehaviour {

    List<OnOffCheckBox> lst_CheckBoxes;
    
    [SerializeField]
    UIGrid grid;

	// Use this for initialization
	void Start () {
        lst_CheckBoxes = new List<OnOffCheckBox>();

        OnOffCheckBox[] chk_Boxes = grid.GetComponentsInChildren<OnOffCheckBox>();
        
        for (int i = 0; i < chk_Boxes.Length; i++)
        {
            chk_Boxes[i].SetToggleGroup(10 + i);

            lst_CheckBoxes.Add(chk_Boxes[i]);
        }
    }

    /// <summary>
    /// 첫번째는 제외
    /// </summary>
    public void SetDisableAllCheckBoxes()
    {
        if (lst_CheckBoxes.Count <= 0)
            return;

        for (int i = 1; i < lst_CheckBoxes.Count; i++)
        {
            lst_CheckBoxes[i].SetActiveContent(false);
        }
    }
    public void SetEnableAllCheckBoxes()
    {
        if (lst_CheckBoxes.Count <= 0)
            return;

        for (int i = 1; i < lst_CheckBoxes.Count; i++)
        {
            lst_CheckBoxes[i].SetActiveContent(true);
        }
    }
}
