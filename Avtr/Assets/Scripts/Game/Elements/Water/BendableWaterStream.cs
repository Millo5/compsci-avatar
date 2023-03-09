using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendableWaterStream : WaterBendable
{

    Vector3 position;
    Vector3 startPosition;
    Vector3 targetPosition;
    float time;
    float targetTime;

    Vector3 offsetDirection;

    private void Start()
    {
        position = transform.position;
        startPosition = position;
        targetPosition = transform.position + transform.forward * 10f;

        targetTime = Vector3.Distance(startPosition, targetPosition) / 5f;

        offsetDirection = Quaternion.AngleAxis(Random.Range(0f, 360f), transform.forward) * Vector3.Cross(startPosition - targetPosition, Vector3.up);


    }



    private void FixedUpdate()
    {
        if (state == WaterState.Water)
        {
            time += Time.fixedDeltaTime;

            position = Vector3.Lerp(startPosition, targetPosition, time / targetTime);
            position += Mathf.Sin(time / targetTime * Mathf.PI) * offsetDirection;

            rb.MovePosition(position);
        }
    }

    protected override void OnSwitchState(WaterState newState)
    {

    }
}
