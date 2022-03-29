using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class MultiplayerMenu : Menu 
{
    // User inputs
    [SerializeField] TMP_InputField playername;
    [SerializeField] TMP_InputField createRoomName;
    [SerializeField] TMP_InputField joinRoomName;
    [SerializeField] TMP_Dropdown regionDropdown;
    [SerializeField] Toggle privateRoom;

    //Menu items
    [SerializeField] TMP_Text latency;
    [SerializeField] VerticalLayoutGroup roomlist;
    [SerializeField] GameObject roomListPrefab;
    
    // Network handler
    [SerializeField] PhotonLauncherController network;

    // Interconnected menus + the manager
    [SerializeField] MenuManager menuManager;
    [SerializeField] Menu changeRegion;
    [SerializeField] Menu createRoom;
    [SerializeField] Menu connectRoom;
    [SerializeField] Menu mainMenu;

    void Awake()
    {
        if(PlayerPrefs.HasKey("Player Name"))
        {
            var name = PlayerPrefs.GetString("Player Name");
            playername.text = name;
            network.UpdatePlayerName(name);
        }
        regionDropdown.AddOptions(PhotonLauncherController.REGION_NAME.Values.ToList());
        regionDropdown.onValueChanged.AddListener(ChangeRegion);
        playername.onValueChanged.AddListener(OnPlayerNameChanged);
    }

    void Update()
    {
        latency.text = $"{network.ping} ms";
    }

    public override void OnEnable()
    {
        base.OnEnable();
        regionDropdown.value = regionDropdown.options.FindIndex(option => option.text == network.region);
    }

    public void CreateRoom()
    {
        network.CreateRoom(createRoomName.text, privateRoom.isOn);
        menuManager.RequestActiveMenu(createRoom);
    }

    public void ConnectToRoom()
    {
        network.JoinRoom(joinRoomName.text);
        menuManager.RequestActiveMenu(connectRoom);
    }

    public void ChangeRegion(int dropdownValue)
    {
        string regionname = regionDropdown.options[regionDropdown.value].text;
        if (regionname != network.region){
            network.ConnectToServer(PhotonLauncherController.REGION_CODE[regionname]);
            menuManager.RequestActiveMenu(changeRegion);
        }
    }

    public void ReturnToMainMenu()
    {
        menuManager.RequestActiveMenu(mainMenu);
        network.DisconnectFromServer();
    }

    public void OnPlayerNameChanged(string name){
        Debug.Log($"Player name changed to {name}");
        network.UpdatePlayerName(name);

        PlayerPrefs.SetString("Player Name", name);
        PlayerPrefs.Save();
    }

    public override void OnRoomListUpdate(List<RoomInfo> newlist)
    {
        Debug.Log($"Room List Updated. Room Count: {newlist.Count}");

        foreach (Transform child in roomlist.transform)
            Destroy(child.gameObject);
        
        foreach (RoomInfo room in newlist)
        {
            if (!room.RemovedFromList && room.IsOpen)
            {
                GameObject newroom = Instantiate(roomListPrefab, roomlist.gameObject.transform);
                var roomItemController = newroom.GetComponent<RoomListItem>();

                // Inject dependencies
                roomItemController.roomNameInput = joinRoomName;
                roomItemController.menu = this;

                // Manual data settings
                roomItemController.roomnametext.text = room.Name;
                var max = room.MaxPlayers == 0 ? "inf" : room.MaxPlayers.ToString();
                roomItemController.playercounttext.text = $"{room.PlayerCount}/{max}";
            }
            else
            {
                Debug.Log($"{room.Name}: IsOpen={room.IsOpen}, RemovedFromList={room.RemovedFromList}");
            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        WindowController.instance.ShowErrorMessage(message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        if (cause.ToString() != "None" && cause.ToString() != "DisconnectByClientLogic")
        {
            menuManager.RequestActiveMenu(mainMenu);
        }
    }
}
