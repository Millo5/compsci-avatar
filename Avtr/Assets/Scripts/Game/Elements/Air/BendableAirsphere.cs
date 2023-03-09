using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BendableAirsphere : BendableObject, ITargetable
{


    private float size = 0f;

    private bool destroy = false;
    private float wobble = 0f;

    public ElementController owner;

    Collider col;
    protected override void Awake()
    {
        base.Awake();
        col = GetComponent<Collider>();
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }


    protected override void Update()
    {
        base.Update();

        // push away nearby players
        hittablesInTrigger
            .Where(i => i != owner.controller).ToList()
            .ForEach(x => x.Knockback(x.transform.position + Vector3.up - transform.position));




        // Rotate
        wobble += Time.deltaTime;
        Vector3 rot = transform.rotation.eulerAngles;
        rot.x = Mathf.Sin(wobble % Mathf.PI * 2f) * 10f;
        transform.rotation = Quaternion.Euler(rot);

        // spawning and despawning
        size = Mathf.Min(size + Time.deltaTime * 4f, 1f);
        if (destroy)
        {
            size -= Time.deltaTime * 8f;
            if (size <= 0f)
            {
                Destroy(gameObject);
            }
        }
        transform.localScale = Vector3.one * size;
    }

    public override void Destroy()
    {
        destroy = true;
    }

    public bool CastAbility()
    {
        return false;
    }
}
