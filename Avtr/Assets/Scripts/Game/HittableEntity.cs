

using Photon.Pun;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class HittableEntity : MonoBehaviourPunCallbacks
{

    protected Rigidbody rb;

    [Header("Hittable Entity")]
    [SerializeField] LayerMask whatIsGround;
    protected bool grounded => Physics.CheckBox(transform.position, new Vector3(0.3f, 0.05f, 0.3f), transform.rotation, whatIsGround);

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity *= 0.9f;
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.velocity = vel;
    }
    public virtual void Knockback(Vector3 force, float magnitude = 1f)
    {
        rb.AddForce(force * magnitude, ForceMode.Impulse);
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }
}
