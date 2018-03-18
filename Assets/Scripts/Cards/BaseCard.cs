using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCard:  System.IComparable<BaseCard>
{
    protected string cardName;
    protected int id;
    protected Sprite image;
    protected string description;
    protected float ballSpeed;
	protected CardType cardType;

    // Generic Constructor //
    public BaseCard(string cardName, int id, Sprite image, string description, float ballSpeed)
    {
        this.cardName = cardName;
        this.id = id;
        this.image = image;
        this.description = description;
        this.ballSpeed = ballSpeed;
    }

    // CardData Constructor //
    public BaseCard(CardData cardData)
    {
        this.cardName = cardData.nameTh;
        this.id = cardData.cid;
        this.image = ImageUtil.LoadSprite(cardData.imagePath);
        this.description = cardData.descriptionTh;
        this.ballSpeed = 0; // TODO Unimplemented
    }
    
    public int CompareTo(BaseCard other)
    {
        if (other == null) return 1;
        return id.CompareTo(other.id);
    }

    public override string ToString()
    {
        return "BaseCard[id=" + id + ", cardName=" + cardName + "]";
    }

	public Sprite GetSprite() {
		return image;
	}

	public void SetCardType(CardType cardType) {
		this.cardType = cardType;
	}

	public CardType GetCardType() {
		return cardType;
	}
}
