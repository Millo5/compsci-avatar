using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Landmine : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject explosion;
    [SerializeField] private float delay;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode(delay);
    }

    public void Explode(float delay = 0f)
    {
        Invoke(nameof(ExplodeTrigger), delay);
    }
    private void ExplodeTrigger()
    {
        PV.RPC(nameof(RPC_Explode), RpcTarget.All);
    }

    [PunRPC]
    private void RPC_Explode()
    {
        FindObjectsOfType<Landmine>().Where(i => i != this && Vector3.Distance(transform.position, i.transform.position) < 2f).ToList().ForEach(i => i.Explode(0.2f));

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
