using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{

    [SerializeField] ElementManager elementManager;
    private ELEMENT element;

    List<AbilityIndex> abilities;

    PhotonView PV;
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        element = (ELEMENT)Enum.Parse(typeof(ELEMENT), (string)PhotonNetwork.LocalPlayer.CustomProperties["element"]);

        abilities = new List<AbilityIndex>();

        // fill abilities with correct ability indexes
        initAbility(0, elementManager.getAbilities(element).leftClick);
        initAbility(1, elementManager.getAbilities(element).rightClick);
        initAbility(KeyCode.Q, elementManager.getAbilities(element).q);
        initAbility(KeyCode.F, elementManager.getAbilities(element).f);
    }

    private void initAbility(int mouseInd, ElementAbility abil)
    {
        if (abil.canHold) abilities.Add(new AbilityIndex(() => Input.GetMouseButton(mouseInd), abil));
        else abilities.Add(new AbilityIndex(() => Input.GetMouseButtonDown(mouseInd), abil));
    }
    private void initAbility(KeyCode key, ElementAbility abil)
    {
        if (abil.canHold) abilities.Add(new AbilityIndex(() => Input.GetKey(key), abil));
        else abilities.Add(new AbilityIndex(() => Input.GetKeyDown(key), abil));
    }


    private void Update()
    {
        for (int i = 0; i < abilities.Count; i++)
        {
            AbilityIndex abilInd = abilities[i];

            abilInd.ability.Update();

            if (abilInd.currentCooldown > 0f) abilInd.currentCooldown = Mathf.Max(abilInd.currentCooldown - Time.deltaTime, 0f);

            if (!abilInd.key()) continue;
            if (abilInd.currentCooldown > 0f) continue;

            AbilityInfo info = new AbilityInfo();
            info.playerTransform = transform;
            info.playerCamera = GetComponentInChildren<Camera>();
            info.bendables = FindObjectsOfType<BendableObject>();
            abilInd.ability.Trigger(info);

            abilInd.currentCooldown = abilInd.ability.cooldown;
        }
    }

    private class AbilityIndex
    {
        public float currentCooldown;
        public Func<bool> key;
        public ElementAbility ability;

        public AbilityIndex(Func<bool> key, ElementAbility ability)
        {
            currentCooldown = 0f;
            this.key = key;
            this.ability = ability;
        }
    }

}
