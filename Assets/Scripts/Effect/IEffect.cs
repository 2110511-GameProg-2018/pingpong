using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEffect {

	protected int EID;
	protected ExecuteEffect effect;
	protected int[] parameters;

	public IEffect(int EID, ExecuteEffect effect, int[] parameters) {
		this.EID = EID;
		this.effect = effect;
		this.parameters = parameters;
	}

	public virtual void Execute() {
		effect (parameters);
	}
}
