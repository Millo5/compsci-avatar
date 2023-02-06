using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAbility : MonoBehaviour
{

    public ABILKEY key;
    public Image cooldownImage;

    private void Start()
    {
        SetCooldown(0f, 1f);
    }

    public void SetCooldown(float cooldown, float maxCooldown)
    {
        cooldownImage.fillAmount = cooldown / maxCooldown;
    }

}
