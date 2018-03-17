using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

	List<BaseCard> cards = new List<BaseCard> ();
	int count = 0;

	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		sprite = GetComponentInChildren<SpriteRenderer> ();
		sprite.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Shuffle() {
		for (int i = 0; i < cards.Count; i++) {
			int index = Random.Range (i, cards.Count);
			BaseCard temp = cards [i];
			cards [i] = cards [index];
			cards [index] = temp;
		}
	}

	public void LoadDeck(int[] cardIDs) {
		foreach (int cardID in cardIDs) {
			cards.Add(CardFactory.instance.GetNewCard (cardID));
		}

		if (cardIDs.Length > 0) {
			sprite.enabled = true;
		}
	}

	public BaseCardComponent PopTopCard() {
		BaseCardComponent cardObject;
		SpriteRenderer spriteRenderer;
		if (cards.Count <= 0) {
			return null;
		}

		BaseCard card = cards[0];
		cards.RemoveAt (0);
		cardObject = new GameObject ("card" + count).AddComponent<BaseCardComponent> ();
		spriteRenderer = cardObject.gameObject.AddComponent<SpriteRenderer> ();
		cardObject.SetBaseCard (card);
		cardObject.SetSpriteRenderer (spriteRenderer);
		cardObject.SetBackSprite (sprite.sprite);
		cardObject.SetFrontSprite (card.GetSprite());
		cardObject.transform.position = new Vector3 (transform.position.x, transform.position.y, 0);

		if (cards.Count <= 0) {
			sprite.enabled = false;
		}

		count++;
		Debug.Log ("Deck/PopTopCard : " + card.ToString());
		return cardObject;
	}
}
