﻿using UnityEngine;
using System.Collections;

public class HeroInfoPanel : MyPanel {

    private static HeroInfoPanel _instance;

    public static HeroInfoPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(HeroInfoPanel)) as HeroInfoPanel;
            }
            return _instance;
        }
    }

    private void OnDestroy()
    {
        _instance = null;        
    }

    [SerializeField]
    GameObject target;

    [SerializeField]
    UISprite _element;
    [SerializeField]
    UISprite _hero_class;
    [SerializeField]
    UILabel _name;

    [SerializeField]
    UILabel _power;

    [SerializeField]
    UIPanel panel_front;

    [SerializeField]
    UIPanel panel_scrollView;

    Vector2 raw_element_size;
    Vector2 raw_class_size;

    private void Awake()
    {
        base.Awake();
        raw_element_size = new Vector2(41, 43);
        raw_class_size = new Vector2(40, 35);        
    }

    private void Start()
    {
        if(panel_front)
            panel_front.depth = panel.depth + 5;
        if (panel_scrollView)
            panel_scrollView.depth = panel.depth + 6;

    }
    public void Set(HeroPanel.Hero_Element element, HeroPanel.Hero_Class hero_class, string name)    
    {
        Utility.ChangeSpriteAspectSnap(_element, Utility.GetSpriteNameByEnum(element), raw_element_size);
        Utility.ChangeSpriteAspectSnap(_hero_class, Utility.GetSpriteNameByEnum(hero_class, true), raw_class_size);            
        _name.text = name;

        // dummy
        _power.text = Random.Range(0, 10000).ToString();

        //
        //GameObject go = Instantiate(Resources.Load("Unit/tkc-ha_hu_don")) as GameObject;
        //go.transform.parent = target.transform;
        //go.transform.localScale = Vector3.one * 130;
        //go.transform.localPosition = Vector3.zero;
        Main.Instance.MakeObjectToTarget("Unit/tkc-ha_hu_don", target,Vector3.one, Vector3.one * 130);

    }
}
