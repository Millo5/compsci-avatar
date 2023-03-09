using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Rigidbody))]
public class BendableObject : MonoBehaviour
{

    [SerializeField] private float bottomDistance = 0f;
    [SerializeField] private ELEMENT element;
    [SerializeField] private Renderer selectedRenderer;
    public ELEMENT Element => element;

    public bool grounded => Physics.CheckBox(transform.position - Vector3.up * bottomDistance, new Vector3(0.3f, 0.05f, 0.3f), transform.rotation, LayerMask.GetMask("Ground"));

    protected List<HittableEntity> hittablesInTrigger = new List<HittableEntity>();

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        selectedRenderer.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();

    }
    protected virtual void Update()
    {
        if (selected > 0)
        {
            selected--;
            if (selected == 0)
            {
                selectedRenderer.gameObject.SetActive(false);
            }
        }
    }



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


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out HittableEntity e))
        {
            if (!hittablesInTrigger.Contains(e)) hittablesInTrigger.Add(e);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out HittableEntity e))
        {
            hittablesInTrigger.Remove(e);
        }
    }


    private int selected = 0;
    internal void Select()
    {
        selected = 2;
        selectedRenderer.gameObject.SetActive(true);
    }


}
