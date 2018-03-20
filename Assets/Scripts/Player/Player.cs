using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int[] cardIDs = { 1, 2, 1, 2, 2 };

	int score = 0;
	Deck deck;
	Hand hand;

	float lerpT = 1;
	float lerpSpeed = 3;
	Vector2 oldPosition;
	BaseCardComponent selectedCard;

	bool drawFinished = true;
	bool selectFinished = true;
	bool moveFinished = true;

	// Use this for initialization
	void Start () {
		deck = GetComponentInChildren<Deck> ();
		hand = GetComponentInChildren<Hand> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!drawFinished && hand.IsArrangeFinish()) {
			drawFinished = true;
		}
		if (!selectFinished && (moveFinished && hand.IsArrangeFinish())) {
			selectFinished = true;
		}

		MoveCard ();
	}

	void MoveCard() {
		if (moveFinished) {
			return;
		}
		if (lerpT >= 1) {
			selectedCard.transform.position = transform.position;
			moveFinished = true;
		}
		else {
			lerpT += Time.deltaTime * lerpSpeed;
			selectedCard.transform.position = Vector2.Lerp (selectedCard.transform.position, transform.position, lerpT);
		}
	}

	public void Initialize() {
		deck.LoadDeck (cardIDs);
		deck.Shuffle ();
		Draw ();
		Draw ();
	}

	public bool Draw() {
		BaseCardComponent card = deck.PopTopCard ();
		if (card != null) {
			hand.Insert (card);
			drawFinished = false;
			return true;
		}
		return false;
	}

	public BaseCardComponent SelectCard() {
		selectedCard = hand.GetSelectedCard ();
		if (selectedCard != null && hand.Remove (selectedCard)) {
			selectedCard.transform.parent = transform;
			oldPosition = selectedCard.transform.position;
			lerpT = 0;
			moveFinished = false;
			selectFinished = false;
		}
		return selectedCard;
	}

	public void SetScore(int score) {
		this.score = score;
	}
	public int GetScore() {
		return score;
	}

	public bool IsDrawFinished() {
		return drawFinished;
	}
	public bool IsSelectFinished() {
		return selectFinished;
	}

	public void SetHandSelectableType(bool attack, bool defend, bool effect, bool serve) {
		if (selectFinished) {
			hand.SetSelectableType (attack, defend, effect, serve);
		}
	}
}
