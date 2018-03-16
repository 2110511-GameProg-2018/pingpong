using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardConfigLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string cardConfigJsonPath = Application.dataPath + "/Config/cards_config.json";
        if (File.Exists(cardConfigJsonPath))
        {
            string dataAsJson = File.ReadAllText(cardConfigJsonPath);
            CardConfigData ccd = CardConfigData.FromJSON(dataAsJson);
            foreach (CardData cd in ccd.cards)
            {
                CardFactory.instance.AddNewCardPrototype(cd);
            }
        } else
        {
            Debug.LogError("CardConfigJson File does not exist! (value = " + cardConfigJsonPath + ")");
        }
        Debug.Log("CardFactory.Count = " + CardFactory.instance.CountCardPrototypes());

        Debug.Log("CardFactory.GetNewCard(1) = " + CardFactory.instance.GetNewCard(1));
        Debug.Log("CardFactory.GetNewCard(2) = " + CardFactory.instance.GetNewCard(2));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
