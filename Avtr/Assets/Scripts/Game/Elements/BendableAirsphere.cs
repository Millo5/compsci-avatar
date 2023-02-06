using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BendableAirsphere : BendableObject
{


    private float size = 0f;

    private bool destroy = false;
    private float wobble = 0f;

    public ElementController owner;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;
    }


    private void Update()
    {
        // damage nearby players
        GetAllHittables()
            .Where(t => Vector3.Distance(t.transform.position, transform.position) < 1f)
            .ToList().ForEach(x => x.Knockback(Vector3.up));

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

}
