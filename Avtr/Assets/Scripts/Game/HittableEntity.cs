

using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class HittableEntity : MonoBehaviourPunCallbacks
{

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
    }

    public virtual void Knockback(Vector3 force)
    {
        rb.AddForce(force);
    }
}
