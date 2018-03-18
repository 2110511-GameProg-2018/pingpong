using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	const float cardWidth = 750f/500f;
	const float cardHeight = 1050f/500f;
	const float gap = 0.1f;

	bool arrangeFinish = true;
	float lerpT = 1;
	float lerpSpeed = 1;

	List<BaseCardComponent> cards = new List<BaseCardComponent> ();
	Vector3[] newPosition = new Vector3[10];
	Vector3[] oldPosition = new Vector3[10];
	Quaternion newRotation = Quaternion.identity;
	Quaternion[] oldRotation = new Quaternion[10];
	BaseCardComponent highlightedCard;
	bool[] selectableType = new bool[4];

	BaseCardComponent selectedCard;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ArrangeCard ();
		HandleInput ();
	}

	void HandleInput() {
		if (highlightedCard != null) {
			highlightedCard.SetHighlight (false);
			highlightedCard = null;
		}

		Vector2 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D hitInfo = Physics2D.Raycast (mousePosition, Vector2.zero, 0, LayerMask.GetMask("Hand"));

		if (hitInfo.collider != null) {
			highlightedCard = hitInfo.collider.gameObject.GetComponent<BaseCardComponent> ();
			if (selectableType[(int) highlightedCard.GetBaseCard().GetCardType()]) {
				highlightedCard.SetHighlight (true);
			}
			else {
				highlightedCard = null;
			}
		}

		if (Input.GetMouseButtonDown(0) && selectedCard == null) {
			selectedCard = highlightedCard;
		}
	}

	void ArrangeCard() {
		if (arrangeFinish) {
			return;
		}
		if (lerpT >= 1) {
			for (int i = 0; i < cards.Count; i++) {
				cards [i].transform.position = newPosition [i];
				cards [i].transform.rotation = newRotation;
			}
			arrangeFinish = true;
		}
		else {
			for (int i = 0; i < cards.Count; i++) {
				cards [i].transform.position = Vector3.Lerp (oldPosition [i], newPosition [i], lerpT);
				cards [i].transform.rotation = Quaternion.Lerp (oldRotation [i], newRotation, lerpT);
			}
			lerpT += Time.deltaTime * lerpSpeed;
		}
	}

	void SetArrangePosition() {
		float middle = (cards.Count - 1) / 2f;
		for (int i = 0; i < cards.Count; i++) {
			oldPosition[i] = cards [i].transform.position;
			oldRotation[i] = cards [i].transform.rotation;
			newPosition [i] = new Vector3 ((i - middle) * (cardWidth + gap), transform.position.y, 0);
		}
		lerpT = 0;
		arrangeFinish = false;
	}

	public bool Remove(BaseCardComponent removedCard) {
		if (cards.Remove (removedCard)) {
			removedCard.transform.parent = transform.parent;

			SetArrangePosition ();
			lerpSpeed = 3;
			return true;
		}

		return false;
	}

	public void Insert(BaseCardComponent newCard) {
		cards.Add (newCard);
		newCard.transform.parent = transform;
		BoxCollider2D collider = newCard.gameObject.AddComponent<BoxCollider2D> ();
		collider.size = new Vector2 (cardWidth, cardHeight);

		SetArrangePosition ();
		lerpSpeed = 1;
	}

	public void SetSelectableType(bool attack, bool defend, bool effect, bool serve) {
		selectableType [(int) CardType.Attack] = attack;
		selectableType [(int) CardType.Defend] = defend;
		selectableType [(int) CardType.Effect] = effect;
		selectableType [(int) CardType.Serve] = serve;

		selectedCard = null;
	}

	public BaseCardComponent GetSelectedCard() {
		return selectedCard;
	}
	
	public bool IsArrangeFinish() {
		return arrangeFinish;
	}

}
