using UnityEngine;
using System.Collections;

public class MainMenuBootstrap : MonoBehaviour 
{
    [SerializeField] MenuManager defaultMenuManager;
    [SerializeField] Menu defaultMenu;
    
    void Awake()
    {
        defaultMenuManager.RequestActiveMenu(defaultMenu);
    }
}
