using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {

	private float speed;
	private Direction location;
	public Text speedText;

	// Use this for initialization
	void Start () {
		speed = 0;
		location = Direction.NONE;
		SetSpeedText();
	}
	
	// Update is called once per frame
	void Update () {
		SetSpeedText();
		if (Input.GetKeyDown ("space")) {
			speed += 1;
		}
		if (Input.GetKeyDown ("h")) {
			ToggleSpeedText ();
		}
	}

	public float getSpeed(){
		return speed;
	}

	public void setSpeed(float new_speed){
		speed = new_speed;
	}

	Direction getLocation(){
		return location;
	}

	void setLocation(Direction new_direction){
		location = new_direction;
	}

	void SetSpeedText()
    {
        speedText.text = "Ball speed: " + speed.ToString ();

    }

	void ToggleSpeedText(){
		speedText.enabled = !speedText.enabled;
	}
}
