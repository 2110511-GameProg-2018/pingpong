using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDatabase {

	public static EffectDatabase instance = new EffectDatabase ();

	List<ExecuteEffect> effects;

	EffectDatabase() {
		effects = new List<ExecuteEffect> ();
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
			Debug.Log("EffectFactory/LoadEffect : this is effect 0");
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

	public IFutureEffect GetIFutureEffect(int EID, int[] parameters, int turnDelay, Phase phase) {
		return new IFutureEffect (EID, effects[EID], parameters, turnDelay, phase);
	}
}
