using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    [SerializeField] GameObject[] visuals;
    private void Awake()
    {
        foreach (GameObject i in visuals)
        {
            Destroy(i);
        }
    }

}
