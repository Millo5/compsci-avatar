using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Element Ability", order = 1)]
public abstract class ElementAbility : ScriptableObject
{

    public abstract ELEMENT element { get; }// What element the ability is
    public abstract float cooldown { get; } // The cooldown in seconds between uses
    public virtual bool canHold { get; }    // Can you hold the key down to repeatedly trigger the ability

    public abstract void Trigger(AbilityInfo info);

    public virtual void Update() { }

}

public struct AbilityInfo
{
    public Transform playerTransform;
    public Camera playerCamera;
    public BendableObject[] bendables;
}