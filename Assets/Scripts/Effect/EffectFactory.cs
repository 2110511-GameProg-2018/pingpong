using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory {

	public static EffectFactory instance = new EffectFactory ();

	List<ExecuteEffect> effects;
	GameController gc;

	EffectFactory() {
		effects = new List<ExecuteEffect> ();
		gc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		LoadEffect ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadEffect() {
		effects.Add (delegate(int[] parameters) {
			Debug.Log("EffectFactory/LoadEffect : this is effect 0 with parameter = " + parameters[0]);
			gc.ball.setSpeed(gc.ball.getSpeed() + parameters[0]);
		});
		effects.Add (delegate(int[] parameters) {
			Debug.Log("EffectFactory/LoadEffect : this is effect 1");
		});
		effects.Add (delegate(int[] parameters) {
			Debug.Log("EffectFactory/LoadEffect : this is effect 2");
		});
		effects.Add (delegate(int[] parameters) {
			Debug.Log("EffectFactory/LoadEffect : this is effect 3");
		});
	}

	public IEffect GetIEffect(int EID, int[] parameters) {
		return new IEffect (EID, effects[EID], parameters);
	}

	public IFutureEffect GetIFutureEffect(int EID, int[] parameters, int turnDelay, InnerPhase phase) {
		return new IFutureEffect (EID, effects[EID], parameters, turnDelay, phase);
	}
}
