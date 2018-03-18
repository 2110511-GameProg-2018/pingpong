using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IFutureEffect : IEffect {

    public Phase activationPhase;

    public IFutureEffect(int effectType, int effectParameter, int turn, Phase activationPhase) : base(effectType, effectParameter, turn)
    {
        this.activationPhase = activationPhase;
    }

}
