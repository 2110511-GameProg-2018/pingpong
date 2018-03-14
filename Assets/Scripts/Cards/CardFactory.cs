using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Singleton + Factory Design Pattern
public class CardFactory {

    // Member Variables
    private List<CardData> cardPrototypes;

    
    // Singleton Design Pattern
    private static CardFactory _instance = new CardFactory();

    public static CardFactory instance
    {
        get { return _instance; }
    }

    // Use private to hide this class from being created externally
    private CardFactory()
    {
        cardPrototypes = new List<CardData>();
    }


    // Adds a new card prototype to internal list
    public void AddNewCardPrototype(CardData cd)
    {
        cardPrototypes.Add(cd);
    }

    // Return the total count of all card prototypes
    public int CountCardPrototypes()
    {
        return cardPrototypes.Count;
    }

    // generates a new card from the internal list of prototypes
    public BaseCard GetNewCard(int cid)
    {
        CardData c = cardPrototypes.Find((CardData cd) => cd.cid == cid);
        if (c == null)
        {
            throw new CardException("Cannot find card with id = " + cid);
        } else
        {
            switch(c.type.ToLower())
            {
                case "attack":
                    return new AttackCard(c);
                case "defend":
                case "defense":
                    return new DefendCard(c);
                case "counter":
                case "effect":
                    return new EffectCard(c);
                case "serve":
                    return new ServeCard(c);
                default:
                    throw new CardException("Unknown card type with type = " + c.type);
            }
        }
    }
}
