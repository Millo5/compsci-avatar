using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItemBehaviour : MonoBehaviour
{

    [SerializeField] TMP_Text text;

    public RoomInfo info { private set; get; }

    public void Setup(RoomInfo info)
    {
        this.info = info;
        text.text = info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(info);
    }

}
