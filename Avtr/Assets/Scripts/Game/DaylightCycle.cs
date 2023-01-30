using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycle : MonoBehaviour
{

    float time;

    private void Update()
    {
        time = DateTime.Now.Second + DateTime.Now.Millisecond / 1000f + DateTime.Now.Minute * 60f + DateTime.Now.Hour * 60f * 60f;


        transform.rotation = Quaternion.Euler(time / 86400f * 360f - 90f, -30f, 0f);
        
    }

}
