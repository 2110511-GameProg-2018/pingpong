﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardData {

    // Card ID
    public int cid;

    // Card name in Thai
    public string nameTh;

    // Card Type
    public string type;
    
    // Author of card
    public string author;

    // imagePath
    public string imagePath;

    // description in Thai
    public string descriptionTh;

    // id of associated effect
    public int effectID;

	public int[] parameters;

    public override string ToString()
    {
        return "Card[cid=" + cid + ", nameTh=" + nameTh + ", descTh=" + descriptionTh + "]";
    }

}
