using UnityEngine;
using System.Collections;
using TMPro;

public class MainMenu : Menu
{
    [SerializeField] Menu connectRegion;
    [SerializeField] Menu multiplayer;
    [SerializeField] MenuManager menuManager;
    [SerializeField] PhotonLauncherController network;

    public void Multiplayer()
    {
        if (network.Connected == false)
            network.ConnectToServer();
        else if (network.InLobby == false)
            network.JoinLobby();
        else
        {
            Debug.LogWarning("Player did not leave lobby properly");
            network.LeaveLobby();
            network.JoinLobby();
        }
        menuManager.RequestActiveMenu(connectRegion);
    }

    public void SinglePlayer(){
        network.StartSingleplayerSession();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
