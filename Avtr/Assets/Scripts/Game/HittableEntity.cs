

using Photon.Pun;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class HittableEntity : MonoBehaviourPunCallbacks
{

    public Rigidbody rb { protected set; get; }

    [Header("Hittable Entity")]
    [SerializeField] LayerMask whatIsGround;
    protected bool grounded => Physics.CheckBox(transform.position, new Vector3(0.3f, 0.05f, 0.3f), transform.rotation, whatIsGround);


    private float levitationTime = 0f;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        if (grounded) rb.velocity *= 0.9f;
        if (levitationTime > 0f)
        {
            levitationTime -= Time.fixedDeltaTime;
            rb.velocity = Vector3.zero;
        }
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

    public void Levitate(float time)
    {
        levitationTime = time;
    }
}
