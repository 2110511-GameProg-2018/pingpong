using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCard : BaseCard
{
    public AttackCard(CardData cardData) : base(cardData)
    {
        
    }

    public override string ToString()
    {
        return "AttackCard[id=" + id + ", cardName=" + cardName + "]";
    }
}
