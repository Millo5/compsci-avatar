using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameDisplay : MonoBehaviour
{

    [SerializeField] PhotonView PV;
    TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        text.text = PV.Owner.NickName;
    }

}
