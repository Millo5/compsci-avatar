using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballScript : MonoBehaviour
{
    
    public float lifeTime = 5f;

    
    private void FixedUpdate()
    {

        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifeTime -= 1 * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision fireballHit)
    {
        if (fireballHit.collider)
        {
            Destroy(gameObject);        
        }
    }
}
