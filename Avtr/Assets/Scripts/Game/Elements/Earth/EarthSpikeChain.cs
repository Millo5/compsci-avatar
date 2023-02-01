using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "EarthSpikeChain", menuName = "Elements/Earth/Earth Spike Chain", order = 1)]
    public class EarthSpikeChain : ElementAbility
    {

        [SerializeField] GameObject spikePrefab;
        private Vector3 position;
        private Vector3 direction;

        public override ELEMENT element => ELEMENT.Earth;

        public override float cooldown => 1.2f;

        public override void FixedUpdate(AbilityInfo info)
        {
            triggerIndex += 1f;

            if (triggerIndex < 50f)
            {
                if (triggerIndex % 5 == 0)
                {
                    Instantiate(spikePrefab, position, Quaternion.identity);
                }

                position += direction;

                direction = Vector3.Lerp(direction, info.playerTransform.forward, 0.1f).normalized;
            }
        }

        public override void Trigger(AbilityInfo info)
        {
            position = info.playerTransform.position;
            direction = info.playerTransform.forward;
        }
    }
}

