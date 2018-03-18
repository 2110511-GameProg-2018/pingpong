using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseModel : MonoBehaviour {

    private Phase _currentPhase;

    public Phase currentPhase
    {
        get {return _currentPhase;}
        set { _currentPhase = value; }
    }
	// Use this for initialization
	void Start () {
        currentPhase = Phase.STAND_BY;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
