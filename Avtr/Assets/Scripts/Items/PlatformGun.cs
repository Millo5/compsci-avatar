using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlatformGun : Gun
{

    [SerializeField] Camera cam;
    [SerializeField] GameObject objectPrefab;

    PhotonView PV;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public override void Use()
    {
        Shoot();
    }

    private void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            PV.RPC("RPC_Shoot", RpcTarget.All, hit.point + hit.normal * 0.5f);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition)
    {
        GameObject ob = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Landmine"), hitPosition, Quaternion.identity);
        Destroy(ob, 10f);
    }
}
