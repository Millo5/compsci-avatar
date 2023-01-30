using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [SerializeField] private MenuBehaviour[] menus;

    private void Awake()
    {
        instance = this;
    }

    public void OpenMenu(string menuName) => OpenMenu(menus.First(i => i.menuName == menuName));

    public void OpenMenu(MenuBehaviour menu)
    {
        foreach (MenuBehaviour i in menus.Where(i => i.open))
        {
            CloseMenu(i);
        }
        menu.Open();
    }
    public void CloseMenu(MenuBehaviour menu)
    {
        menu.Close();
    }
}
