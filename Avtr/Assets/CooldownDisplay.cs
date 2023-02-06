using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CooldownDisplay : MonoBehaviour
{

    [SerializeField] private UIAbility[] abilities;

    public void SetCooldown(ABILKEY abilkey, float currentCooldown, float cooldown) =>
        abilities.FirstOrDefault(i => i.key == abilkey).SetCooldown(currentCooldown, cooldown);

}
