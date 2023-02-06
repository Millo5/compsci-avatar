using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody))]
public class BendableObject : MonoBehaviour
{

    [SerializeField] private float bottomDistance = 0f;
    [SerializeField] private ELEMENT element;
    public ELEMENT Element => element;

    public bool grounded => Physics.CheckBox(transform.position - Vector3.up * bottomDistance, new Vector3(0.3f, 0.05f, 0.3f), transform.rotation, LayerMask.GetMask("Ground"));


    public void setElement(ELEMENT element)
    {
        this.element = element;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position - Vector3.up * bottomDistance, new Vector3(0.3f, 0.05f, 0.3f));
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

    protected HittableEntity[] GetAllHittables()
    {
        return FindObjectsOfType<HittableEntity>();
    }

}
