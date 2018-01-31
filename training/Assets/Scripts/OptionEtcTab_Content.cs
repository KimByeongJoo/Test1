using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OptionEtcTab_Content : MonoBehaviour {

    List<OnOffCheckBox> lst_CheckBoxes;

    [SerializeField]
    UIGrid grid;

    // Use this for initialization
    void Start()
    {
        lst_CheckBoxes = new List<OnOffCheckBox>();

        OnOffCheckBox[] chk_Boxes = grid.GetComponentsInChildren<OnOffCheckBox>();

        for (int i = 0; i < chk_Boxes.Length; i++)
        {
            chk_Boxes[i].SetToggleGroup(20 + i);

            lst_CheckBoxes.Add(chk_Boxes[i]);
        }
    }
}
