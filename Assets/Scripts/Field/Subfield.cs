using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subfield : MonoBehaviour {

    public Color avaliableFieldColor;
    public Color unavaliableFieldColor;

	bool state = true;
	bool highlightState = false;

	SpriteRenderer sprite;
	SpriteRenderer highlight;

	// Use this for initialization
	void Start () {
		state = true;
		sprite = GetComponent<SpriteRenderer> ();
		sprite.color = avaliableFieldColor;

		highlightState = false;
		foreach (SpriteRenderer sp in GetComponentsInChildren<SpriteRenderer> ()) {
			if (sp.transform.parent == transform) {
				highlight = sp;
				break;
			}
		}
		highlight.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setState(bool newState) {
		state = newState;

		if (sprite != null) {
			if (state) {
				sprite.color = avaliableFieldColor;
			} 
			else {
				sprite.color = unavaliableFieldColor;
			}
		}
	}

	public void setHighlightState(bool newState) {
		if (newState != highlightState) {
			highlightState = newState;
			highlight.enabled = highlightState;
		}
	}
}
