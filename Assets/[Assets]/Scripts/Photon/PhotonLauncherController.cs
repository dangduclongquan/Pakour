using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

[DisallowMultipleComponent]
public class PhotonLauncherController : MonoBehaviourPunCallbacks
{
    [SerializeField] string GameVersion; 
    public static Dictionary<string, string> REGION_NAME = new Dictionary<string, string>()
    {
        {"asia", "Asia"},
        {"au", "Australia"},
        {"cae", "Canada"},
        {"eu", "Europe"},
        {"in", "India"},
        {"jp", "Japan"},
        {"ru", "Russia"},
        {"rue", "Russia (East)"},
        {"za", "South Africa"},
        {"sa", "Brazil"},
        {"kr", "South Korea"},
        {"tr", "Turkey"},
        {"us", "America (East)"},
        {"usw", "America (West)"},
    };
    public static Dictionary<string, string> REGION_CODE = new Dictionary<string, string>()
    {
        {"Asia", "asia"},
        {"Australia", "au"},
        {"Canada", "cae"},
        {"Europe", "eu"},
        {"India", "in"},
        {"Japan", "jp"},
        {"Russia", "ru"},
        {"Russia (East)", "rue"},
        {"South Africa", "za"},
        {"Brazil", "sa"},
        {"South Korea", "kr"},
        {"Turkey", "tr"},
        {"America (East)", "us"},
        {"America (West)", "usw"},
    };

    public bool Connected
    {
        get
        {
            return PhotonNetwork.IsConnected;
        }
    }
    public bool InLobby
    {
        get
        {
            return PhotonNetwork.InLobby;
        }
    }

    private string _regioncode;
    public string region
    {
        get
        {
            return REGION_NAME[_regioncode];
        }
    }
    public string regioncode
    {
        get
        {
            return _regioncode;
        }
    }

    public string roomname {get; private set;}
    public int ping {get; private set;}

    private void Awake()
    {
        if (GameVersion != null)
        {
            PhotonNetwork.GameVersion = GameVersion;
        }
        _regioncode = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;
    }

    string GetCurrentRegionCode()
    {
        if (PhotonNetwork.CloudRegion == null)
            return null;
        string crc = PhotonNetwork.CloudRegion;
        return crc.Remove(crc.Length - 2, 2);
    }

    public void ConnectToServer(){ConnectToServer(_regioncode);}
    public void ConnectToServer(string regioncode)
    {
        // Making sure we're not in singleplayer mode
        PhotonNetwork.OfflineMode = false;

        var currentRegionCode = GetCurrentRegionCode();
        if (regioncode == currentRegionCode)
        {
            Debug.LogWarning("Region Update Cancelled: Already connected to {region} server.");
            return;
        }
        if (currentRegionCode != null){
            PhotonNetwork.Disconnect();
        }

        if (REGION_NAME.ContainsKey(regioncode))
            PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = regioncode;
        else
        {
            if (regioncode != "")
            {
                Debug.Log("Region code " + regioncode + " is not valid.");
                return;
            }
            else{
                Debug.Log("Connecting using saved settings...");
                regioncode = PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion;
            }
        }
        _regioncode = regioncode;

        Debug.Log($"Connecting to {REGION_NAME[regioncode]} server...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void CreateRoom(string roomname, bool visibility)
    {
        Debug.Log($"Creating room {roomname}, visibility: {visibility}");
        this.roomname = roomname;
        RoomOptions options = new RoomOptions();
        options.IsVisible = visibility;

        PhotonNetwork.CreateRoom(roomname, options);
    }

    public void JoinRoom(string roomname)
    {
        this.roomname = roomname;
        PhotonNetwork.JoinRoom(roomname);
    }

    public void DisconnectFromServer()
    {
        PhotonNetwork.Disconnect();
    }

    public void UpdatePlayerName(string name)
    {
        PhotonNetwork.NickName = name;
    }

    public void StartSingleplayerSession(){
        DisconnectFromServer();

        PhotonNetwork.OfflineMode = true;
        PhotonNetwork.JoinRoom("Singleplayer");
    }


//         if (PhotonNetwork.IsConnectedAndReady)
//         {
//             menucontroller.ShowMenu("Multiplayer Menu");
//         }

    [SerializeField] float updateperiod;
    float timer;
    
    void Start()
    {
        timer = 0;
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= updateperiod)
        {
            timer = 0;
            ping = PhotonNetwork.GetPing();
        }
    }

    //Photon Pun Callbacks
    public override void OnConnectedToMaster()
    {
        var currentRegionCode = GetCurrentRegionCode();
        if (currentRegionCode == null)
        {
            Debug.Log("Instantiating a Singleplayer session");
        }
        else
        {
            Debug.Log($"Connected to {REGION_NAME[currentRegionCode]} Server.");
            Debug.Log("Joining Lobby...");
            PhotonNetwork.JoinLobby();
        }
    }
}