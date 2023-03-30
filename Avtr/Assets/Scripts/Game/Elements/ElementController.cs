using Elements;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class ElementController : MonoBehaviour
{

    // Element Controller Variables
    [SerializeField] ElementManager elementManager;
    private ELEMENT element;
    private CooldownDisplay cdDisplay;

    List<AbilityIndex> abilities;

    [SerializeField] Material selectedMaterial;

    private PhotonView PV;
    public PlayerController controller { private set; get; }

    // Element Specific Variables
    public BendableAirsphere lastSphere;

    public Rigidbody rb => controller.rb;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        cdDisplay = GetComponent<CooldownDisplay>();
    }

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        if (!RoomManager.Training)
        {
            element = (ELEMENT)Enum.Parse(typeof(ELEMENT), (string)PhotonNetwork.LocalPlayer.CustomProperties["element"]);
        } else
        {
            element = (ELEMENT)Enum.Parse(typeof(ELEMENT), PlayerPrefs.GetString("element"));
        }

        Initialize();
        UpdateAbilities();
    }

    public void ReInitialize() => Initialize();
    private void Initialize()
    {
        lastSphere = null;
    }

    private void UpdateAbilities() {
        abilities = new List<AbilityIndex>();

        initAbility(0,                  elementManager.getAbilities(element).leftClick,     ABILKEY.LC);
        initAbility(1,                  elementManager.getAbilities(element).rightClick,    ABILKEY.RC);
        initAbility(KeyCode.Q,          elementManager.getAbilities(element).q,             ABILKEY.Q);
        initAbility(KeyCode.F,          elementManager.getAbilities(element).f,             ABILKEY.F);
        initAbility(KeyCode.E,          elementManager.getAbilities(element).e,             ABILKEY.E);
        initAbility(KeyCode.LeftShift,  elementManager.getAbilities(element).shift,         ABILKEY.SHIFT);

    }

    public void SetElement(ELEMENT element)
    {
        this.element = element;
        UpdateAbilities();
    }

    private void initAbility(int mouseInd, ElementAbility abil, ABILKEY key)
    {
        if (abil == null) return;
        if (abil.canHold) abilities.Add(new AbilityIndex(() => Input.GetMouseButton(mouseInd), abil, key));
        else abilities.Add(new AbilityIndex(() => Input.GetMouseButtonDown(mouseInd), abil, key));
    }
    private void initAbility(KeyCode key, ElementAbility abil, ABILKEY akey)
    {
        if (abil == null) return;
        if (abil.canHold) abilities.Add(new AbilityIndex(() => Input.GetKey(key), abil, akey));
        else abilities.Add(new AbilityIndex(() => Input.GetKeyDown(key), abil, akey));
    }


    AbilityInfo GetInfo()
    {
        AbilityInfo info = new AbilityInfo();
        info.playerTransform = transform;
        info.playerCamera = GetComponentInChildren<Camera>();
        info.bendables = FindObjectsOfType<BendableObject>();
        info.targetBendable = info.bendables.Where(i => i is ITargetable && i.Element == element)
            .Where(i => Vector3.Dot((i.transform.position - info.playerCamera.transform.position).normalized, info.playerCamera.transform.forward) > 0.95f)
            .OrderBy(i => Vector3.Dot((i.transform.position - info.playerCamera.transform.position).normalized, info.playerCamera.transform.forward))
            .FirstOrDefault();
        info.caster = this;

        if (info.targetBendable != null) info.targetBendable.Select();

        return info;
    }

    public void setCooldown(ABILKEY key, float time)
    {
        print("SET COOLDOWN " + key + " " + time);
        abilities.FirstOrDefault(i => i.abilkey == key).currentCooldown = time;
    }

    private void Update() 
    {
        AbilityInfo info = GetInfo();

        for (int i = 0; i < abilities.Count; i++)
        {
            AbilityIndex abilInd = abilities[i];

            abilInd.ability.Tick(info);

            if (abilInd.currentCooldown > 0f) abilInd.currentCooldown = Mathf.Max(abilInd.currentCooldown - Time.deltaTime, 0f);
            cdDisplay.SetCooldown(abilInd.abilkey, abilInd.currentCooldown, abilInd.ability.cooldown);

            if (!abilInd.key()) continue;
            if (abilInd.currentCooldown > 0f) continue;

            abilInd.ability.triggerIndex = 0;
            abilInd.ability.Trigger(info);

            abilInd.currentCooldown = abilInd.ability.cooldown;
        }
    }

    private void FixedUpdate() => abilities.ForEach(i => i.ability.FixedTick(GetInfo()));


    private class AbilityIndex
    {
        public float currentCooldown;
        public Func<bool> key;
        public ElementAbility ability;
        public ABILKEY abilkey;

        public AbilityIndex(Func<bool> key, ElementAbility ability, ABILKEY abilkey)
        {
            currentCooldown = 0f;
            this.key = key;
            this.ability = ability;
            this.abilkey = abilkey;
        }
    }

}
