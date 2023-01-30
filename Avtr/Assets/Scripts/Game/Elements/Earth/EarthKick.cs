using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "EarthKick", menuName = "Elements/Earth/Kick", order = 1)]
    public class EarthKick : ElementAbility
    {
        public override ELEMENT element => ELEMENT.Earth;

        public override float cooldown => 1f;

        public override void Trigger(AbilityInfo info)
        {
            info.bendables.Where(
                i => Vector3.Distance(i.transform.position, info.playerTransform.position) < 2f &&
                Vector3.Dot(i.transform.position - info.playerTransform.position, info.playerTransform.forward) > 0f)
                .ToList().ForEach(i =>
            {
                if (i.grounded) i.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
                else i.GetComponent<Rigidbody>().AddForce(info.playerTransform.forward * 8f); 
            });
        }
    }
}

