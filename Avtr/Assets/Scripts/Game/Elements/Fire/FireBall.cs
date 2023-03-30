using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "FireBall", menuName = "Elements/Fire/Fireball", order = 1)]
    public class FireBall : ElementAbility
    {

        [SerializeField] GameObject fireballPrefab;
        public override ELEMENT element => ELEMENT.Fire;

        public override float cooldown => 0.6f;

        public override void Trigger(AbilityInfo info)
        {

            Vector3 pos = info.playerCamera.transform.position + info.playerCamera.transform.forward * 2f;

            Instantiate(fireballPrefab, pos, info.playerCamera.transform.rotation)
                .GetComponent<Rigidbody>().AddForce(info.playerCamera.transform.forward * 800f);

            
        }       
    }
}

