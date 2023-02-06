using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour
{

    private void Awake()
    {
        if (!RoomManager.Training)
        {
            // Game isn't setup for training mode
            print("IN TRAINING");

            GameObject ob = new GameObject();
            RoomManager rm = ob.AddComponent<RoomManager>();

        }
    }

}
