using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

	const float cardWidth = 750f/500f;
	const float cardHeight = 1050f/500f;
	const float gap = 0.1f;

	bool arrangeFinish = true;
	float lerpT = 1;

	List<BaseCardComponent> cards = new List<BaseCardComponent> ();
	Vector3[] newPosition = new Vector3[6];
	Vector3[] oldPosition = new Vector3[6];
	Quaternion newRotation = Quaternion.identity;
	Quaternion[] oldRotation = new Quaternion[6];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
			lerpT += Time.deltaTime;
		}
	}

	public void Insert(BaseCardComponent newCard) {
		cards.Add (newCard);

		float middle = (cards.Count - 1) / 2f;
		for (int i = 0; i < cards.Count; i++) {
			oldPosition[i] = cards [i].transform.position;
			oldRotation[i] = cards [i].transform.rotation;
			newPosition [i] = new Vector3 ((i - middle) * (cardWidth + gap), transform.position.y, 0);
		}
		lerpT = 0;
		arrangeFinish = false;
	}
}
