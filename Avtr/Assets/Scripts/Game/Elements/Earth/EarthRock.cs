using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "EarthRock", menuName = "Elements/Earth/Rock", order = 1)]
    public class EarthRock : ElementAbility
    {

        [SerializeField] GameObject rockPrefab;

        public override ELEMENT element => ELEMENT.Earth;

        public override float cooldown => 1.2f;

        public override void Trigger(AbilityInfo info)
        {

            Vector3 spawnPos = info.playerTransform.position + info.playerTransform.forward * 1f + Vector3.up;

            Rigidbody rb = Instantiate(rockPrefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(info.playerCamera.transform.forward * 7f + Vector3.up * 5f, ForceMode.VelocityChange);

        }
    }
}

