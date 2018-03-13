using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour {

	public Subfield frontField;
	public Subfield rightField;
	public Subfield backField;
	public Subfield leftField;

	Direction inputState = Direction.NONE;
	Direction cursorState = Direction.NONE;
	bool[] directionState = {true, true, true, true};
	float width, height;
	Vector3 mousePosition;

	SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
		inputState = Direction.NONE;
		cursorState = Direction.NONE;

		for (int i = 0; i < 4; i++) {
			directionState [i] = true;
		}

		sprite = GetComponent<SpriteRenderer> ();
		width = sprite.size.x;
		height = sprite.size.y;
	}
	
	// Update is called once per frame
	void Update () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		Debug.Log ("Field/Update: mouse position = (" + mousePosition.x + ", " + mousePosition.y + ")");

		if (mousePosition.x > width / 2f || mousePosition.x < -width / 2f ||
		    mousePosition.y > height / 2f || mousePosition.y < -height / 2f) {
			cursorState = Direction.NONE;
		}
		else {
			float m = height / width;
			bool posLine = mousePosition.y > m * mousePosition.x;
			bool negLine = mousePosition.y > -m * mousePosition.x;
			if (posLine && negLine) {
				cursorState = Direction.FRONT;
			}
			else if (!posLine && negLine) {
				cursorState = Direction.RIGHT;
			}
			else if (!posLine && !negLine) {
				cursorState = Direction.BACK;
			}
			else if (posLine && !negLine) {
				cursorState = Direction.LEFT;
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			if (cursorState != Direction.NONE && directionState[(int)cursorState] != false) {
				Debug.Log ("Field/Update: select" + cursorState);
				if (inputState == Direction.NONE) {
					inputState = cursorState;
				}
			}
		}
	}

	public void setDirectionState(bool front, bool right, bool back, bool left) {
		directionState [(int)Direction.FRONT] = front;
		directionState [(int)Direction.RIGHT] = right;
		directionState [(int)Direction.BACK] = back;
		directionState [(int)Direction.LEFT] = left;

		frontField.setState (front);
		rightField.setState (right);
		backField.setState (back);
		leftField.setState (left);
	}

	public Direction getInputDirection() {
		return inputState;
	}

}
