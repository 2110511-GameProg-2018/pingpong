using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeCard : BaseCard
{
    public ServeCard(CardData cardData) : base(cardData)
    {
    }

    public override string ToString()
    {
        return "ServeCard[id=" + id + ", cardName=" + cardName + "]";
    }
}
