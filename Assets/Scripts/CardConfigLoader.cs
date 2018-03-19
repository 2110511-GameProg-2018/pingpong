using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CardConfigLoader : MonoBehaviour {

	GameObject object1;

	// Use this for initialization
	void Start () {
        string cardConfigJsonPath = Application.dataPath + "/Config/cards_config_v2.json";
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

		// test create GameObject with BaseCardComponent **must change BaseCardComponent.card to public
//		object1 = new GameObject ("card1");
//		object1.AddComponent<BaseCardComponent> ().SetBaseCard(CardFactory.instance.GetNewCard(1));
//		string test = object1.GetComponent<BaseCardComponent> ().card.ToString ();
//		Debug.Log("BaseCardComponent.card = " + test);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
