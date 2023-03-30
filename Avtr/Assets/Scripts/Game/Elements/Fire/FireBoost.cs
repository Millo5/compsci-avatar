using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "FireBoost", menuName = "Elements/Fire/Fire boost", order = 3)]
    public class FireBoost : ElementAbility
    {
        [SerializeField] GameObject fireboostPrefab;

        public override ELEMENT element => ELEMENT.Fire;

        public override float cooldown => 8f;

        private float jumping = 0f;

        public override void Trigger(AbilityInfo info)
        {

            jumping = .20f;
        }

        public override void FixedTick(AbilityInfo info)
        {
            if (jumping > 0f)
            {
                jumping -= Time.fixedDeltaTime;
                info.playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, 40f, 0);
                if (jumping <= 0f)
                {
                    info.playerTransform.GetComponent<Rigidbody>().velocity = new Vector3(0, 5f, 0);
                }
            }
        }
    }
}
