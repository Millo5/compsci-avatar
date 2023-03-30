using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "EarthKick", menuName = "Elements/Earth/Earth Kick", order = 1)]
    public class EarthKick : ElementAbility
    {
        public override ELEMENT element => ELEMENT.Earth;

        public override float cooldown => 0.5f;

        public override void Trigger(AbilityInfo info)
        {

            info.bendables.Where(i => i.Element == ELEMENT.Earth && Vector3.Distance(i.transform.position, info.playerTransform.position) < 2f && Vector3.Dot(i.transform.position - info.playerTransform.position, info.playerTransform.forward) > 0f).ToList().ForEach(i => i.GetComponent<Rigidbody>().AddForce(info.playerCamera.transform.forward * 1000f + Vector3.up * 300f));
        }
    }
}

