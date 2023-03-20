using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class WaterBendable : BendableObject, ITargetable
{
    public enum WaterState { Water, Ice }
    public WaterState state { get; protected set; }

    protected GameObject waterObject;
    protected GameObject iceObject;

    public void SwitchState(WaterState state)
    {
        this.state = state;

        waterObject.SetActive(state == WaterState.Water);
        iceObject.SetActive(state == WaterState.Ice);

        OnSwitchState(state);
    }
    protected abstract void OnSwitchState(WaterState newState);

    public bool CastAbility()
    {
        return false;
    }
}
