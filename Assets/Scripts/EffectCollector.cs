using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCollector{
    public Dictionary<Phase, Queue<IEffect>> thisTurnEffect = new Dictionary<Phase, Queue<IEffect>>();
    public Dictionary<Phase, Queue<IEffect>> nextTurnEffect = new Dictionary<Phase, Queue<IEffect>>();

    public void AddNextTurnEffect(IFutureEffect effect)
    {
        nextTurnEffect[effect.activationPhase].Enqueue(effect);
    }

    public Queue<IEffect> GetEffectsThisPhase(Phase thisPhase)
    {
        return thisTurnEffect[thisPhase];
    }

    public void ChangeTurn()
    {
        thisTurnEffect = nextTurnEffect;
        nextTurnEffect = new Dictionary<Phase, Queue<IEffect>>();
    }
}