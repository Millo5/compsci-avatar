using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyController : HittableEntity
{
    private float health = 0f;

    public void TakeDamage(float damageInfo)
    {
        health -= damageInfo;
    }
}
