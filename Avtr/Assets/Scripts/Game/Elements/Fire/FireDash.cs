using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "FireDash", menuName = "Elements/Fire/Fire dash", order = 2)]
    public class FireDash : ElementAbility
    {
        [SerializeField] GameObject firedashPrefab;

        public override ELEMENT element => ELEMENT.Fire;

        public override float cooldown => 3.8f;

        private float dashDur = 0f;
        float dashStrength = 0.5f;
        
        public override void Trigger(AbilityInfo info)
        {
            dashDur = .4f;
        }


        public override void FixedTick(AbilityInfo info)
        {
            if (dashDur > 0)
            {
                dashDur -= Time.deltaTime;
                Vector3 forceToApply = info.playerCamera.transform.forward * dashStrength + info.playerCamera.transform.up * dashStrength / 2;
                info.playerTransform.GetComponent<Rigidbody>().AddForce(forceToApply, ForceMode.Impulse);
            }
        }
    }
}
