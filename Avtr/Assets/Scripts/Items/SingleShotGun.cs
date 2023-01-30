using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{

    [SerializeField] Camera cam;

    PhotonView PV;
    PlayerManager playerManager;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();

        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    public override void Use()
    {
        Shoot();
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
            PV.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.1f);
        if (colliders.Length != 0)
        {
            GameObject ob = Instantiate(((GunInfo)itemInfo).bulletImpactPrefab, hitPosition + hitNormal * 0.001f, Quaternion.LookRotation(-hitNormal, Vector3.up));
            ob.transform.SetParent(colliders[0].transform);
            Destroy(ob, 60f);
        }
    }
}
