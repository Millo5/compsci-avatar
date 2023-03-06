using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyController : HittableEntity
{
    private float health = 0f;

    private Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < -10f)
        {
            transform.position = startPos;
            rb.velocity = Vector3.zero;
        }
    }

    public void TakeDamage(float damageInfo)
    {
        health -= damageInfo;
    }
}
