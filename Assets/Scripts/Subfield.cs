using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subfield : MonoBehaviour {

	bool state = true;

	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		state = true;
		sprite = GetComponent<SpriteRenderer> ();
		sprite.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setState(bool newState) {
		state = newState;

		if (sprite != null) {
			if (state) {
				sprite.color = Color.green;
			} 
			else {
				sprite.color = Color.red;
			}
		}
	}
}
