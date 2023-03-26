using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "AirTornado", menuName = "Elements/Air/Air Tornado", order = 1)]
    public class AirTornado : ElementAbility
    {

        [SerializeField] GameObject tornadoPrefab;


        public override ELEMENT element => ELEMENT.Air;
        public override float cooldown => 1.2f;

        public override void Trigger(AbilityInfo info)
        {
            Vector3 pos = info.playerTransform.position + info.playerTransform.forward * 1f;

            GameObject ob = Instantiate(tornadoPrefab, pos, info.playerTransform.rotation);
        }
    }
}

