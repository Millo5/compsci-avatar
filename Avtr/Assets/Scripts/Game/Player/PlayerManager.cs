using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using System.Linq;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    GameObject controller;

    public int killCount;

    private bool isMine = false;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();

        isMine = PV.IsMine;

        if (RoomManager.Training) isMine = true;
    }

    private void Start()
    {
        killCount = 0;
        if (isMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        if (!RoomManager.Training)
        {
            Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
            controller = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), spawnpoint.position, spawnpoint.rotation, 0, new object[] { PV.ViewID });
        } else
        {
            Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();
            Object prefab = Resources.Load(Path.Combine("PhotonPrefabs", "PlayerController"));
            controller = (GameObject)Instantiate(prefab, spawnpoint.position, spawnpoint.rotation);
        }
    }

    public void Die()
    {
        if (!RoomManager.Training)
        {
            PhotonNetwork.Destroy(controller);

            Invoke("CreateController", 0.5f);
        } else
        {
            Destroy(controller);

            Invoke("CreateController", 0.5f);
        }
    }

    public void GetKill()
    {
        PV.RPC(nameof(RPC_GetKill), PV.Owner);
    }

    [PunRPC]
    void RPC_GetKill()
    {
        killCount++;

        Hashtable hash = new Hashtable();
        hash.Add("kills", killCount);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
    }
}
