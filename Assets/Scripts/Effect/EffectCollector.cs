using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollector {

	public static EffectCollector instance = new EffectCollector ();

	Dictionary<int, List<IFutureEffect>> effectQueue;

	EffectCollector() {
		effectQueue = new Dictionary<int, List<IFutureEffect>> ();
	}

	public void Enqueue(int turn, IFutureEffect effect) {
		if (!effectQueue.ContainsKey (turn)) {
			effectQueue.Add (turn, new List<IFutureEffect> ());
		}
		effectQueue [turn].Add (effect);
	}

	public void Execute(int turn, InnerPhase phase) {
		if (!effectQueue.ContainsKey (turn)) {
			return;
		}
		List<IFutureEffect> effects = effectQueue [turn];
		for (int i = effects.Count - 1; i >= 0; i--) {
			if (effects[i].GetPhase() == phase) {
				effects [i].FutureExecute ();
				effects.RemoveAt (i);
			}
		}
		if (effects.Count == 0) {
			effectQueue.Remove (turn);
		}
	}
}