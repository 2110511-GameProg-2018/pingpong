using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldTester : MonoBehaviour {

	public Field testField;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			testField.setDirectionState (true, true, true, true);
		}
		else if (Input.GetKeyDown(KeyCode.Keypad1)) {
			testField.setDirectionState (true, true, false, true);
		}
		else if (Input.GetKeyDown(KeyCode.Keypad2)) {
			testField.setDirectionState (true, false, false, false);
		}
		else if (Input.GetKeyDown(KeyCode.Keypad3)) {
			testField.setDirectionState (false, false, false, false);
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			testField.gameObject.SetActive(true);
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			testField.gameObject.SetActive(false);
		}

		if (testField.gameObject.active && testField.getInputDirection() != Direction.NONE) {
			Debug.Log ("FieldTester/Update: field send " + testField.getInputDirection());
			testField.gameObject.SetActive(false);
		}
	}
}
