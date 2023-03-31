using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "WaterStream", menuName = "Elements/Water/Water Stream", order = 1)]
    public class WaterStream : ElementAbility
    {

        [SerializeField] GameObject waterStreamPrefab;

        public override ELEMENT element => ELEMENT.Water;

        public override float cooldown => 1.2f;

        public override void Trigger(AbilityInfo info)
        {

            Vector3 pos = info.playerCamera.transform.position + info.playerCamera.transform.forward * 2f;

            Instantiate(waterStreamPrefab, pos, info.playerCamera.transform.rotation);

        }
    }
}

