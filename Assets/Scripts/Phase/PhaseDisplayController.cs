using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhaseDisplayController : MonoBehaviour {
    public PhaseModel pm;
	private Phase selectedPhase;

	public PhaseComponent Stand_by;
	public PhaseComponent Draw;
	public PhaseComponent Defend;
	public PhaseComponent Guess;
	public PhaseComponent Counter;
	public PhaseComponent Attack;
	public PhaseComponent End;
	RectTransform parent;
	GridLayoutGroup grid;

	// Use this for initialization
	void Start () {
        if (pm == null)
        {
            Debug.LogError("PhaseModel is not set. Please add a PhaseModel and try again.");
        }

        selectedPhase = pm.currentPhase;
		parent = GetComponent<RectTransform> ();
		grid = GetComponent<GridLayoutGroup> ();
		DynamicGrid ();
	}
	
	// Update is called once per frame
	void Update () {
		DynamicGrid ();
		ClearState ();
        selectedPhase = pm.currentPhase;
        switch (selectedPhase) {
			case Phase.STAND_BY:
				Stand_by.setState (true);
				break;
			case Phase.DRAW:
				Draw.setState (true);
				break;
			case Phase.DEFEND:
				Defend.setState (true);
				break;
			case Phase.GUESS:
				Guess.setState (true);
				break;
			case Phase.COUNTER:
				Counter.setState (true);
				break;
			case Phase.ATTACK:
				Attack.setState (true);
				break;
			case Phase.END:
				End.setState (true);
				break;
		}
	}

	void ClearState(){
		Stand_by.setState (false);
		Draw.setState (false);
		Defend.setState (false);
		Guess.setState (false);
		Counter.setState (false);
		Attack.setState (false);
		End.setState (false);
	}

	void DynamicGrid(){
		grid.cellSize = new Vector2 (2 * parent.rect.width / 3, parent.rect.height / 8);
	}

	public void setSelectedPhase(Phase newPhase){
		selectedPhase = newPhase;
	}
}
