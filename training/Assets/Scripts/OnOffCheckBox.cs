using UnityEngine;
using System.Collections;

public class OnOffCheckBox : MonoBehaviour {

    UISprite sprite;

    [SerializeField]
    UILabel label;
    [SerializeField]
    UIToggle onToggle;
    [SerializeField]
    UIToggle offToggle;
    
    private void Start()
    {
        sprite = GetComponent<UISprite>();        
    }    
    
    public void SetActiveContent(bool active)
    {
        if (active)
        {
            sprite.spriteName = "achievement_bg";
            SetActiveToggles(true);
        }
        else
        {
            sprite.spriteName = "achievement_bg_dark";
            SetActiveToggles(false);
        }
    }

    public void SetToggleGroup(int toggleGroup)
    {
        if (onToggle == null || offToggle == null)
        {
            Debug.LogWarning("Please Set On/Off Toggle");
            return;
        }        

        offToggle.group = toggleGroup;
        onToggle.group = toggleGroup;
    }
    public void SetSubjectLabel(string subject)
    {       
        if(label != null)
            label.text = subject;        
    }

    void SetActiveToggles(bool active)
    {
        if (onToggle == null || offToggle == null)
        {
            Debug.LogWarning("Please Set On/Off Toggle");
            return;
        }

        onToggle.enabled = active;
        offToggle.enabled = active;
    }    
}
