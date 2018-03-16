using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class CardConfigData {

    // Version of this card config format
    public string version;

    // Stored cards
    public List<CardData> cards;

    // Base path for card images
    public string cardsImageBasePath;
    
    public override string ToString()
    {
        string card_str = "";
        foreach (CardData c in cards) {
            card_str += c.ToString() + ", ";
        }
        
        
        return "CardConfigData[v" + version + " cards: " + card_str + "]";
    }

    public static CardConfigData FromJSON(string jsonString)
    {
        return JsonUtility.FromJson<CardConfigData>(jsonString);
    }

}
