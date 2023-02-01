using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "AirSphere", menuName = "Elements/Air/Air Sphere", order = 1)]
    public class AirSphere : ElementAbility
    {

        [SerializeField] GameObject airspherePrefab;

        public override ELEMENT element => ELEMENT.Air;

        public override float cooldown => 1.2f;

        public override void Trigger(AbilityInfo info)
        {
            
            Vector3 pos = info.playerCamera.transform.position + info.playerCamera.transform.forward * 2f;

            GameObject ob = Instantiate(airspherePrefab, pos, info.playerCamera.transform.rotation);
            ob.GetComponent<Rigidbody>().AddForce(info.playerCamera.transform.forward * 5f);
            info.caster.lastSphere = ob.GetComponent<BendableAirsphere>();

        }
    }
}

