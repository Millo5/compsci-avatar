using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Elements/Element Manager", order = 0)]
public class ElementManager : ScriptableObject
{

    [SerializeField] ElementAbilities airAbilities;
    [SerializeField] ElementAbilities fireAbilities;
    [SerializeField] ElementAbilities waterAbilities;
    [SerializeField] ElementAbilities earthAbilities;

    public ElementAbilities getAbilities(ELEMENT element)
    {
        switch (element)
        {
            case ELEMENT.Air:
                return airAbilities;
            case ELEMENT.Fire:
                return fireAbilities;
            case ELEMENT.Water:
                return waterAbilities;
            case ELEMENT.Earth:
            default:
                return earthAbilities;
        }
    }

}

[System.Serializable]
public struct ElementAbilities
{
    public ElementAbility rightClick;
    public ElementAbility leftClick;
    public ElementAbility f;
    public ElementAbility q;
    public ElementAbility e;
    public ElementAbility shift;
}