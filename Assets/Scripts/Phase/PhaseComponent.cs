using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseComponent : MonoBehaviour {
	bool state;
	CanvasRenderer canvas;

    public Color normalColor;
    public Color selectedColor;
	// Use this for initialization
	void Start () {
		state = false;
		canvas = GetComponent<CanvasRenderer> ();
        canvas.SetColor(normalColor);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setState(bool newState){
		state = newState;
		if (canvas != null) {
			if (state) {
				canvas.SetColor (selectedColor);
			}else{
				canvas.SetColor(normalColor);
			}
		}
	}
}
