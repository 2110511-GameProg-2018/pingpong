using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private float speed;
	private Direction location;

	// Use this for initialization
	void Start () {
		speed = 0;
		location = Direction.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	int getLocation(){
		return location;
	}

	void setLocation(Direction new){
		location = new
	}
}
