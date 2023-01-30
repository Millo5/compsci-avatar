using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
using UnityEngine.UI;

public class PlayerListItemBehaviour : MonoBehaviourPunCallbacks
{

    [SerializeField] TMP_Text text;
    [SerializeField] Image image;
    Player _player;

    public void Setup(Player player)
    {
        _player = player;
        text.text = player.NickName;
        if (player == PhotonNetwork.LocalPlayer)
        {
            text.fontStyle = FontStyles.Bold;
        }
        if (player == PhotonNetwork.MasterClient)
        {
            text.text = "•" + text.text;
        }

        string elemName = (string)_player.CustomProperties["element"];
        if (elemName == null) elemName = "Fire";
        ELEMENT element = (ELEMENT)Enum.Parse(typeof(ELEMENT), elemName);
        image.sprite = ElementOptionSelector.Instance.Options[element].sprite;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        print("leave room");
        if (_player == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        print("leave room 2");
        Destroy(gameObject);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == _player)
        {
            if (changedProps.ContainsKey("element"))
            {
                ELEMENT element = (ELEMENT)Enum.Parse(typeof(ELEMENT), (string)changedProps["element"]);
                image.sprite = ElementOptionSelector.Instance.Options[element].sprite;
            }
        }
    }
}
