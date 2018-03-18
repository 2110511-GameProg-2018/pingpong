using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Singleton + Factory Design Pattern
public class CardFactory {

	public static CardFactory instance = new CardFactory();

    // Member Variables
    private List<CardData> cardPrototypes;

    public CardFactory()
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
			BaseCard newCard;
            switch(c.type.ToLower())
            {
				case "attack":
					newCard = new AttackCard (c);
					newCard.SetCardType (CardType.Attack);
					return newCard;
                case "defend":
                case "defense":
					newCard = new DefendCard (c);
					newCard.SetCardType (CardType.Defend);
					return newCard;
                case "counter":
                case "effect":
					newCard = new EffectCard (c);
					newCard.SetCardType (CardType.Effect);
					return newCard;
                case "serve":
					newCard = new ServeCard (c);
					newCard.SetCardType (CardType.Serve);
					return newCard;
                default:
                    throw new CardException("Unknown card type with type = " + c.type);
            }
        }
    }
}
