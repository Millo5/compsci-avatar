using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisiondetector : MonoBehaviour
{
    public float lifeTime = 5f;

    private void FixedUpdate()
    {
        if(lifeTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifeTime -= 1 * Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision pebbleCollison)
    {
        if(pebbleCollison.collider)
        {
            Destroy(gameObject);
        }
    }
}
