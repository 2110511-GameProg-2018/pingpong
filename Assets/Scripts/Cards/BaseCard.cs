using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCard : MonoBehaviour, System.IComparable<BaseCard>
{
    string cardName;
    int id;
    Sprite image;
    string description;
    float ballSpeed;

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

    }
}
