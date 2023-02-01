using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerLooks : MonoBehaviour
{

    [SerializeField] PlayerLooksOption[] options;


    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        SetLooks((string)PhotonNetwork.LocalPlayer.CustomProperties["element"]);
    }

    public void SetLooks(string elem)
    {
        if (PV.AmOwner)
        {
            PV.RPC(nameof(RPC_SetLooks), RpcTarget.All, elem);
        }
    }

    [PunRPC]
    public void RPC_SetLooks(string eleme)
    {
        ELEMENT elem = ELEMENT.Fire;// (ELEMENT)Enum.Parse(typeof(ELEMENT), eleme);
        options.ToList().ForEach(i => i.reference.SetActive(false));
        options.FirstOrDefault(i => i.element == elem).reference.SetActive(true);
    }

    [Serializable]
    public struct PlayerLooksOption
    {
        public ELEMENT element;
        public GameObject reference;
    }
}
