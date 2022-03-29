using UnityEngine;
using System.Collections;
using TMPro;

public class ConnectingRegionMenu : Menu
{
    [SerializeField] TMP_Text title;
    [SerializeField] PhotonLauncherController network;
    [SerializeField] MenuManager menuManager;
    [SerializeField] Menu multiplayer;
    [SerializeField] Menu main;

    public override void OnEnable()
    {
        base.OnEnable();
        title.text = $"Connecting to {network.region} server...";
    }

    public override void OnJoinedLobby()
    {
        menuManager.RequestActiveMenu(multiplayer);
        Debug.Log("Joined Lobby.");
        Debug.Log("Opened Multiplayer Menu.");
    }

    public void CancelConnection(){
        network.DisconnectFromServer();
        menuManager.RequestActiveMenu(main);
    }
}

