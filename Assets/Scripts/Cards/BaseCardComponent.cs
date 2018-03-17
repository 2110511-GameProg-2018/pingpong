using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardComponent : MonoBehaviour {

	BaseCard card;
	SpriteRenderer spriteRenderer;
	Sprite frontSprite;
	Sprite backSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.rotation.eulerAngles.y > 270) {
			spriteRenderer.sprite = frontSprite;
		}
		else if (transform.rotation.eulerAngles.y > 90) {
			spriteRenderer.sprite = backSprite;
		}
		else {
			spriteRenderer.sprite = frontSprite;
		}
	}

	public void SetBaseCard(BaseCard card) {
		this.card = card;
	}

	public void SetSpriteRenderer(SpriteRenderer spriteRenderer) {
		this.spriteRenderer = spriteRenderer;
	}

	public void SetFrontSprite(Sprite sprite) {
		frontSprite = sprite;
	}

	public void SetBackSprite(Sprite sprite) {
		backSprite = sprite;
	}
}
