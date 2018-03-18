using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTester : MonoBehaviour {

	public Player player;

	// Use this for initialization
	void Start () {
		player = Instantiate (player);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.I)) {
			player.Initialize ();
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			if (player.IsDrawFinished()) {
				player.Draw ();
			}
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			if (player.IsSelectFinished()) {
				player.SetHandSelectableType (true, false, false, false);
			}
		}

		if (player.SelectCard () != null) {
			Debug.Log ("PlayerTester/Update : " + player.SelectCard ().GetBaseCard().ToString());
			if (player.IsSelectFinished()) {
				player.SetHandSelectableType (false, false, false, false);
			}
		}
	}
}
