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
		OnEnable();

		for (int i = 0; i < 4; i++) {
			directionState [i] = true;
		}

		sprite = GetComponent<SpriteRenderer> ();
		width = sprite.size.x;
		height = sprite.size.y;
	}

	void OnEnable() {
		inputState = Direction.NONE;
		cursorState = Direction.NONE;
	}
	
	// Update is called once per frame
	void Update () {
		mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//		Debug.Log ("Field/Update: mouse position = (" + mousePosition.x + ", " + mousePosition.y + ")");

		Direction newCursorState = Direction.NONE;
		if (mousePosition.x > width / 2f || mousePosition.x < -width / 2f ||
		    mousePosition.y > height / 2f || mousePosition.y < -height / 2f) {
			newCursorState = Direction.NONE;
		}
		else {
			float m = height / width;
			bool posLine = mousePosition.y > m * mousePosition.x;
			bool negLine = mousePosition.y > -m * mousePosition.x;
			if (posLine && negLine) {
				newCursorState = Direction.FRONT;
			}
			else if (!posLine && negLine) {
				newCursorState = Direction.RIGHT;
			}
			else if (!posLine && !negLine) {
				newCursorState = Direction.BACK;
			}
			else if (posLine && !negLine) {
				newCursorState = Direction.LEFT;
			}
		}

		if (newCursorState != Direction.NONE && directionState [(int)newCursorState] == false) {
			newCursorState = Direction.NONE;
		}
		if (cursorState != newCursorState) {
			switch (cursorState) {
			case (Direction.NONE):
				break;
			case(Direction.FRONT):
				frontField.setHighlightState (false);
				break;
			case(Direction.RIGHT):
				rightField.setHighlightState (false);
				break;
			case(Direction.BACK):
				backField.setHighlightState (false);
				break;
			case(Direction.LEFT):
				leftField.setHighlightState (false);
				break;
			}

			switch (newCursorState) {
			case (Direction.NONE):
				break;
			case(Direction.FRONT):
				frontField.setHighlightState (true);
				break;
			case(Direction.RIGHT):
				rightField.setHighlightState (true);
				break;
			case(Direction.BACK):
				backField.setHighlightState (true);
				break;
			case(Direction.LEFT):
				leftField.setHighlightState (true);
				break;
			}
			cursorState = newCursorState;
		}

		if (Input.GetMouseButtonDown(0)) {
			Debug.Log ("Field/Update: select" + cursorState);
			if (inputState == Direction.NONE) {
				inputState = cursorState;
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
