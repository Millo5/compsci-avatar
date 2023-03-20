using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "WaterChangeState", menuName = "Elements/Water/Water Change State", order = 1)]
    public class WaterChangeState : ElementAbility
    {

        public override ELEMENT element => ELEMENT.Water;

        public override float cooldown => 1.2f;

        public override void Trigger(AbilityInfo info)
        {

            if (info.targetBendable is WaterBendable)
            {
                WaterBendable waterBendable = (WaterBendable)info.targetBendable;
                if (waterBendable.state == WaterBendable.WaterState.Water)
                    waterBendable.SwitchState(WaterBendable.WaterState.Ice);
                else
                    waterBendable.SwitchState(WaterBendable.WaterState.Water);
            }

        }
    }
}

