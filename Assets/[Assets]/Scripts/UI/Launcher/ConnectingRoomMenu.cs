using UnityEngine;
using System.Collections;
using TMPro;

public class ConnectingRoomMenu : Menu 
{
    // Most dependencies unused due to code structure changes
    [SerializeField] TMP_Text title;
    [SerializeField] PhotonLauncherController network;
    [SerializeField] MenuManager menuManager;
    [SerializeField] Menu multiplayer;

    public override void OnEnable()
    {
        base.OnEnable();
        title.text = $"Connecting to {network.roomname}";
    }
}
