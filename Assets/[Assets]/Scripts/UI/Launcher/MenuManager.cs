using UnityEngine;
using System.Collections;
using System;

// A class to coordinate a large menu group
public class MenuManager : MonoBehaviour 
{
    private Menu _activeMenu;

    public void RequestActiveMenu(Menu menu)
    { 
        if (this._activeMenu != null){
            bool closed = _activeMenu.Close();
            if (closed == false){
                return;
            }
        }
        this._activeMenu = menu;
        menu.Open();
    }

    public void RequestClose()
    {
        this.RequestActiveMenu(null);
    }
}
