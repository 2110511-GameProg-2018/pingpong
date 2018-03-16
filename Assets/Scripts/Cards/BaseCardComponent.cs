using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCardComponent : MonoBehaviour {

	BaseCard card;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetBaseCard(BaseCard card) {
		this.card = card;
	}
}
