using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviour {

	public Deck deck;
	public Hand hand;
	int[] cardIDs = { 1, 2, 1, 2, 2 };

	// Use this for initialization
	void Start () {
		deck = Instantiate (deck);
		hand = Instantiate (hand);
		hand.SetSelectableType (false, false, false, false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.S)) {
			deck.Shuffle ();
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			BaseCardComponent card = deck.PopTopCard ();
			if (card != null) {
				hand.Insert(card);
			}
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			deck.LoadDeck (cardIDs);
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			if (hand.IsArrangeFinish()) {
				hand.SetSelectableType (true, false, false, false);
			}
		}

		if (hand.GetSelectedCard() != null) {
			Debug.Log ("DeckTester/Update : " + hand.GetSelectedCard().GetBaseCard().ToString());
			hand.Remove (hand.GetSelectedCard());
			hand.GetSelectedCard ().gameObject.SetActive (false);
			hand.SetSelectableType (false, false, false, false);
		}
	}
}
