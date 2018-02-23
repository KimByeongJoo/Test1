using UnityEngine;
using System.Collections;

public class PlayerHUDPanel : MyPanel
{
    public int _level { get; set; }
    public int _exp { get; set; }
    public int _exp_max { get; set; }
        
    public int _kingdomPoint { get; set; }
    
    public int exp_Plus_Amount = 1;
    public int kingdomPoint_Plus_Amount = 1;

    [SerializeField]
    UIPanel panel_Mask;

    [SerializeField]
    UI2DSprite sprite_hero;

    [SerializeField]
    UI2DSprite icon_playerTitle;

    [SerializeField]
    UILabel label_kingdomPoint;
    [SerializeField]
    UILabel label_level;
    [SerializeField]
    UISprite exp_bar;

    Vector2 raw_icon_size;

    private void Awake()
    {
        base.Awake();
        raw_icon_size.x = icon_playerTitle.width;
        raw_icon_size.y = icon_playerTitle.height;
    }
    // Use this for initialization
    void Start () {

        //init
        _level = 1;
        _exp = 0;
        _exp_max = MyCsvLoad.Instance.GetLevelExp(_level);

        _kingdomPoint = 0;
    }
	
	// Update is called once per frame
	void Update () {
        _exp += exp_Plus_Amount;
        _kingdomPoint += kingdomPoint_Plus_Amount;

        // exp
        if (_exp >= _exp_max)
        {
            _level++;
            _exp = _exp - _exp_max;

            _exp_max = MyCsvLoad.Instance.GetLevelExp(_level);

            label_level.text = _level.ToString();
        }
        exp_bar.fillAmount = Mathf.Clamp((float)_exp / _exp_max, 0, 1);

        // kingdom point

        label_kingdomPoint.text = _kingdomPoint.ToString();
        TitleInfo info = MyCsvLoad.Instance.GetTitleInfoByKingdomPoint(_kingdomPoint);
        Sprite sprite;
        if (info != null)
        {
            sprite = Main.Instance.GetPlayerTitleSpriteByName(info._sprite);
            if (sprite != null)
                Utility.ChangeSpriteAspectSnap(icon_playerTitle, sprite, raw_icon_size);
        }

        
        //Sprite sprite = Main.Instance.GetPlayerTitleSpriteByName(info._sprite);

    }
}
