using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFutureEffect : IEffect {

	InnerPhase activationPhase;
	int turnDelay;

	public IFutureEffect(int EID, ExecuteEffect effect, int[] parameters, int turnDelay, InnerPhase activationPhase) : base(EID, effect, parameters)
    {
        this.activationPhase = activationPhase;
		this.turnDelay = turnDelay;
    }

	public override void Execute ()
	{
		//will be implemented in future
	}

	public void FutureExecute() {
		base.Execute ();
	}

	public InnerPhase GetPhase() {
		return activationPhase;
	}

}
