using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryObject : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisonEnter (Collision collision)
    {
        print(collision);

        if (collision.transform.TryGetComponent(out HittableEntity victim))
        {
            Destroy(gameObject);
        }
     
    }
}
