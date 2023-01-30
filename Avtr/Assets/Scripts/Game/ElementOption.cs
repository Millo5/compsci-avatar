using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public enum ELEMENT
{
    Fire,
    Air,
    Water,
    Earth,
    None
}

public class ElementOption : MonoBehaviour
{

    [SerializeField] Color color;
    public Color Color => color;
    [SerializeField] ELEMENT element;
    public ELEMENT Element => element;


}
