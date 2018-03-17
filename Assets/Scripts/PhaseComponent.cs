using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseComponent : MonoBehaviour {
	bool state;
	CanvasRenderer canvas;
	// Use this for initialization
	void Start () {
		state = false;
		canvas = GetComponent<CanvasRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setState(bool newState){
		state = newState;
		if (canvas != null) {
			if (state) {
				canvas.SetColor (Color.green);
			}else{
				canvas.SetColor(Color.white);
			}
		}
	}
}
