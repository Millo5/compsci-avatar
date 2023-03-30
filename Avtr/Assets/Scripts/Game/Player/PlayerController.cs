using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : HittableEntity
{
    [Header("References")]
    [SerializeField] GameObject localClient;
    [SerializeField] GameObject notLocalClient;
    [SerializeField] Image healthbarImage;

    [SerializeField] GameObject cameraHolder;

    [Header("Move Stats")]
    [SerializeField] float mouseSensitivity;
    [SerializeField] float sprintSpeed, walkSpeed, jumpForce, smoothTime, smoothTimeAirborne;
    


    int itemIndex;
    int previousItemIndex = -1;

    float verticalLookRotation;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;


    PhotonView PV;

    private const float MaxHealth = 100f;
    private float health = MaxHealth;

    PlayerManager playerManager;

    private bool isMine = false;

    protected override void Awake()
    {
        base.Awake();

        PV = GetComponent<PhotonView>();

        if (!RoomManager.Training)
        {
            playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
            isMine = PV.IsMine;
        } else
        {
            playerManager = FindObjectOfType<PlayerManager>();
            isMine = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
    ~PlayerController()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        if (isMine)
        {
            EquipItem(0);
            Destroy(notLocalClient);
        } else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
            Destroy(localClient);
        }
    }

    private void Update()
    {
        if (!isMine)
            return;

        Look();
        Move();

        /* Old item code, left in for network referencing
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
        {
            EquipItem(itemIndex + 1);
        } else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
        {
            EquipItem(itemIndex - 1);
        }

        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].Use();
        } */

        if (transform.position.y < -10f)
        {
            Die();
        }
    }


    private void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, grounded ? smoothTime : smoothTimeAirborne);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce);
    }

    private void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    private void EquipItem(int _index)
    {
        /* Old item code, left in for network referencing
        if (_index == previousItemIndex) return;
        if (_index < 0) _index = items.Length - 1;
        if (_index >= items.Length) _index = 0;

        itemIndex = _index;
        items[itemIndex].itemObject.SetActive(true);

        if (previousItemIndex > -1)
        {
            items[previousItemIndex].itemObject.SetActive(false);
        }

        previousItemIndex = itemIndex;

        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }*/
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!isMine && targetPlayer == PV.Owner)
        {
            if (changedProps.ContainsKey("itemIndex"))
            {
                EquipItem((int)changedProps["itemIndex"]);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!isMine)
            return;

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public void TakeDamage(float damage)
    {
        PV.RPC(nameof(RPC_TakeDamage), PV.Owner, damage);
    }

    [PunRPC]
    void RPC_TakeDamage(float damage, PhotonMessageInfo info)
    {
        health -= damage;

        healthbarImage.fillAmount = health / MaxHealth;

        if (health <= 0f)
        {
            Die();
            PlayerManager.Find(info.Sender).GetKill();
        }
    }

    private void Die()
    {
        playerManager.Die();
    }



}

