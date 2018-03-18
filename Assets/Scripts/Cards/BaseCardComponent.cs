using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardComponent : MonoBehaviour {

	BaseCard card;
	SpriteRenderer spriteRenderer;
	Sprite frontSprite;
	Sprite backSprite;
	SpriteRenderer highlight;

	// Use this for initialization
	void Start () {
		GameObject child = new GameObject ("highlight");
		child.transform.parent = transform;
		child.transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1);
		highlight = child.AddComponent<SpriteRenderer> ();
		highlight.sprite = Resources.Load<Sprite> ("Sprites/Cards/highlight");
		highlight.enabled = false;
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

	public BaseCard GetBaseCard() {
		return card;
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

	public void SetHighlight(bool b) {
		highlight.enabled = b;
	}
}
