using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalStateController : MonoBehaviour
{
    public event Action<string> ActionMapChangeEvent;
    public string currentGlobalActionMap {get; private set;}
    public bool InGame {get; private set;}
    public bool Alive {get; private set;}
    public bool InMenu {get; private set;}

    public static LocalStateController instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
        InGame = false;
    	InMenu = false;
    	Alive = false;
    }

    private void ChangeActionMap(string name)
    {
    	ActionMapChangeEvent?.Invoke(name);
    	currentGlobalActionMap = name;
    }

    public void Launcher()
    {
    	ChangeActionMap("Launcher");
    	InMenu = false;
    	Alive = false;
        InGame = false;
    }

    public void Menu()
    {
    	ChangeActionMap("Menu");
    	InMenu = true;
    }

    public void ResumePlay()
    {
    	if (Alive)
	    	ChangeActionMap("Player");
    	else
    		ChangeActionMap("Spectator");
    	InMenu = false;
    }

    public void GameStart()
    {
    	ChangeActionMap("Player");
    	Alive = true;
        InGame = true;
    }

    public void Died()
    {
    	Alive = false;
    	if (InMenu == false)
	    	ChangeActionMap("Spectator");
    }
}