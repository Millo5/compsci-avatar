using UnityEngine;

namespace Elements
{
    [CreateAssetMenu(fileName = "EarthPebble", menuName = "Elements/Earth/Earth Pebble", order = 1)]
    public class EarthPebble : ElementAbility
    {

        [SerializeField] GameObject rockPrefab;

        public override ELEMENT element => ELEMENT.Earth;

        public override float cooldown => 2.0f;

        public override void Trigger(AbilityInfo info)
        {

            Vector3 spawnPos = info.playerTransform.position + info.playerTransform.forward * 1f + Vector3.up;

            Rigidbody rb = Instantiate(rockPrefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(info.playerCamera.transform.forward * 20f, ForceMode.VelocityChange);

        }

    }
}

