using UnityEngine;
using System.Collections;

public class Hero7CardSet : MonoBehaviour {

    [SerializeField]
    UIGrid grid;

    [SerializeField]
    public HeroCard[] cards = new HeroCard[7];

    private void Awake()
    {
        for (int i = 0; i < grid.GetChildList().Count; i++)
        {
            cards[i] = grid.GetChild(i).gameObject.GetComponent<HeroCard>();
        }
    }
}
