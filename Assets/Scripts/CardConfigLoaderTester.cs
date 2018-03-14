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
            Debug.Log(CardConfigData.FromJSON(dataAsJson));
        } else
        {
            Debug.LogError("CardConfigJson File does not exist! (value = " + cardConfigJsonPath + ")");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
