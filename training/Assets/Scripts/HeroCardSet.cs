using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroCardSet : MonoBehaviour {

    [SerializeField]
    UIGrid grid;

    [SerializeField]
    public List<HeroCard> cards;
    
    public void AddCard()
    {
        GameObject go = Main.Instance.MakeObjectToTarget("UI/card_bg", grid.gameObject, cards.Count);
        cards.Add(go.GetComponent<HeroCard>());
        grid.Reposition();       
    }

    public void RemoveCardByIndex(int index)
    {
        cards.RemoveAt(index);
        Destroy(cards[index].gameObject);
    }

    public void SetCardNumber(int card_num)
    {
        int addCount = card_num - cards.Count;
        int removeCount = cards.Count - card_num;

        if (addCount > 0)
        {
            for (int i = 0; i < addCount; i++)
            {
                AddCard();
            }
        }
        else if (removeCount > 0)
        {
            for (int i = 0; i < removeCount; i++)
            {
                RemoveCardByIndex(i);
            }
        }
    }
}
