using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class AirTornadoBehaviour : BendableObject
{

    Vector3 dir;
    float lifetime = 0f;
    private void Start()
    {
        dir = transform.forward;
    }

    private void FixedUpdate()
    {
        transform.position += dir * 0.05f * Time.fixedDeltaTime;
    }

    protected override void Update()
    {
        base.Update();
        lifetime += Time.deltaTime;
        if (lifetime > 10f)
        {
            Destroy(gameObject);
        }
    }

}
