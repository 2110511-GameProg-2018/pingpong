using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendCard : BaseCard
{
    public DefendCard(CardData cardData) : base(cardData)
    {
    }
    public override string ToString()
    {
        return "DefendCard[id=" + id + ", cardName=" + cardName + "]";
    }
}
