using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{

    [SerializeField] TMP_InputField usernameInput;


    private void Awake()
    {
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("username"))
        {
            usernameInput.text = PlayerPrefs.GetString("username");
        } else
        {
            usernameInput.text = "Player #" + Random.Range(0, 9999).ToString("0000");
        }
        OnUsernameInputValueChanged();

    }

    public void OnUsernameInputValueChanged()
    {
        if (usernameInput.text.Length > 0 && usernameInput.text.Length <= 64)
        {
            PhotonNetwork.NickName = usernameInput.text;
            PlayerPrefs.SetString("username", usernameInput.text);
        } else
        {
            usernameInput.text = PhotonNetwork.NickName;
        }
    }
    
}
