using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "AirDash", menuName = "Elements/Air/Air Dash", order = 1)]
    public class AirDash : ElementAbility
    {

        public override ELEMENT element => ELEMENT.Air;
        public override float cooldown => 0.8f;

        private Vector3 dashDir;
        private float dashDist = 0f;


        public override void Tick(AbilityInfo info)
        {

            if (dashDist > 0f)
            {
                float d = 8f * Time.deltaTime;
                dashDist -= d;


                info.caster.controller.SetVelocity(dashDir * d / Time.deltaTime);

                if (dashDist <= 0f)
                {
                    info.caster.controller.SetVelocity(Vector3.zero);
                }
            }

        }

        public override void Trigger(AbilityInfo info)
        {
            Transform transform = info.playerTransform;
            
            if (info.targetBendable != null)
            {
                dashDir = info.targetBendable.transform.position - transform.position;
                dashDist = Vector3.Distance(transform.position, info.targetBendable.transform.position);
                return;
            }

            dashDist = 1f;
            dashDir = info.playerCamera.transform.forward;

        }
    }
}

