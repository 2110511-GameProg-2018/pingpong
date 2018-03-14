using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCard : BaseCard
{
    public EffectCard(CardData cardData) : base(cardData)
    {
    }

    public override string ToString()
    {
        return "EffectCard[id=" + id + ", cardName=" + cardName + "]";
    }
}
