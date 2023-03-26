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

        private Vector3 targetPos;
        private float dashTime = 0f;
        private Vector3 startPos;
        private bool targeted = false;

        public override void FixedTick(AbilityInfo info)
        {
            if (dashTime > 0f)
            {
                dashTime -= Time.fixedDeltaTime * 5f;
                info.caster.rb.MovePosition(Vector3.Lerp(targetPos, startPos, dashTime));
                if (dashTime <= 0f)
                {
                    info.caster.transform.position = targetPos;
                    info.caster.rb.velocity = Vector3.zero;

                    if (targeted)
                    {
                        info.caster.controller.Levitate(1f);
                        info.caster.setCooldown(ABILKEY.SHIFT, 0.2f);
                    }
                }
            }
        }

        public override void Trigger(AbilityInfo info)
        {
            dashTime = 1f;
            startPos = info.playerTransform.position;

            targetPos = info.playerTransform.position + info.playerCamera.transform.forward * 2.5f;
            targeted = false;
            if (info.targetBendable != null)
            {
                targeted = true;
                targetPos = info.targetBendable.transform.position;
            }
        }
    }
}

