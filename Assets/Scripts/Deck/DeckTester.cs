using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviour {

	public Deck deck;
	int[] cardIDs = { 1, 2, 1, 2, 2 };

	// Use this for initialization
	void Start () {
		deck = Instantiate (deck);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) {
			deck.Shuffle ();
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			deck.PopTopCard ();
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			deck.LoadDeck (cardIDs);
		}
	}
}
