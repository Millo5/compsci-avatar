using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStation : MonoBehaviour
{

    public ELEMENT element;


    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ElementController controller))
        {
            controller.SetElement(element);
            PlayerPrefs.SetString("element", element.ToString());
        }

    }

}
