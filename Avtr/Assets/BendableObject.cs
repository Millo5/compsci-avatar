using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody))]
public class BendableObject : MonoBehaviour
{

    [SerializeField] private ELEMENT element;
    public ELEMENT Element => element;

    public bool grounded => Physics.CheckBox(transform.position, new Vector3(0.3f, 0.05f, 0.3f), transform.rotation, LayerMask.GetMask("Ground"));


    public void setElement(ELEMENT element)
    {
        this.element = element;
    }

}
