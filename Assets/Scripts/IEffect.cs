using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEffect
{
    public int effectType;
    //1 change speed
    //2 other
    public int effectParameter;
    public int turn;

    public IEffect(int effectType, int effectParameter,int turn)
    {
        this.effectType = effectType;
        this.effectParameter = effectParameter;
        this.turn = turn;
    }
}
