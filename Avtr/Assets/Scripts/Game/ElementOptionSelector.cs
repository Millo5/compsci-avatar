using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ElementOptionSelector : MonoBehaviour
{
    public static ElementOptionSelector Instance;

    [SerializeField] Image cursorImage;
    ELEMENT selectedElement;

    [SerializeField] ElementDictValues options;
    public ElementDictValues Options => options;
    [SerializeField] Launcher launcher;

    private void Awake()
    {
        Instance = this;
    }

    public ELEMENT GetElement()
    {
        return selectedElement;
    }
    public void SelectElement(ElementOption element)
    {
        SelectElement(element.Element);
    }
    public void SelectElement(ELEMENT element)
    {
        selectedElement = element;
        PlayerPrefs.SetString("element", element.ToString());


        Hashtable hash = new Hashtable();
        hash.Add("element", selectedElement.ToString());
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("element"))
        {
            selectedElement = (ELEMENT)Enum.Parse(typeof(ELEMENT), PlayerPrefs.GetString("element"));
            SelectElement(selectedElement);
        } else
        {
            SelectElement(ELEMENT.Fire);
        }
    }

    private void Update()
    {
        cursorImage.transform.position = Vector3.Lerp(cursorImage.transform.position, options[selectedElement].value.transform.position, Time.deltaTime * 20f);
        cursorImage.color = options[selectedElement].value.Color;
    }

    [Serializable]
    public class ElementDictValues
    {
        public ElementDictValue[] elements;

        public ElementDictValue this[ELEMENT ind]
        {
            get => elements.FirstOrDefault(i => i.key == ind);
        }
    }

    [Serializable]
    public struct ElementDictValue
    {
        public ELEMENT key;
        public ElementOption value;
        public Sprite sprite;
    }

}
