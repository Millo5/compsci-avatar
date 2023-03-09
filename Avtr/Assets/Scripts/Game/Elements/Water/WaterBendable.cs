using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class WaterBendable : BendableObject, ITargetable
{
    public enum WaterState { Water, Ice }
    public WaterState state { get; protected set; }

    public void SwitchState(WaterState state)
    {
        this.state = state;
        OnSwitchState(state);
    }
    protected abstract void OnSwitchState(WaterState newState);

    public bool CastAbility()
    {
        return false;
    }
}
