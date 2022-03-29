// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Pun;
// using Photon.Realtime;
// using MEC;

// public class GameNetworkController : MonoBehaviourPunCallbacks
// {
//     [SerializeField] GameMenuController menucontroller;

//     Dictionary<string, System.Action> actions;

//     PhotonView photonview;
//     private void Awake()
//     {
//         actions = new Dictionary<string, System.Action>
//         {
//             {"Leave Room Button", ()=>{  Leave();} },
//             {"Send Chat Message Button", ()=>{ SendChatMessage(); } }
//         };

//         photonview = GetComponent<PhotonView>();
//     }
//     void Leave()
//     {
//         SceneController.instance.LeaveRoom();
//     }
//     void SendChatMessage()
//     {
//         string chatmessage = menucontroller.GetInput("Chat Input");
//         if (chatmessage == "") return;
//         //EventManager.SendChatToAllPlayers(message);

//         photonview.RPC("Chat", RpcTarget.All, chatmessage);

//         menucontroller.SetInput("Chat Input", "");
//     }
//     [PunRPC]
//     void Chat(string chatmessage, PhotonMessageInfo info)
//     {
//         menucontroller.AddChatMessageItem("[" + info.Sender.ActorNumber.ToString() + "]" + info.Sender.NickName + ": " + chatmessage);
//     }

//     public override void OnEnable()
//     {
//         base.OnEnable();

//         GameMenuController.MenuVisibilityChangedEvent += OnMenuVisibilityChanged;
//         MenuInteractableController.ButtonClickedEvent += OnButtonClicked;
//         MenuInteractableController.InputEndEditEvent += OnInputEndEdit;
//         MenuInteractableController.ToggleStateChangedEvent += OnToggleStatChanged;

//         RoomManager.GameStartingEvent += OnGameStarting;
//     }
//     public override void OnDisable()
//     {
//         base.OnDisable();

//         GameMenuController.MenuVisibilityChangedEvent -= OnMenuVisibilityChanged;
//         MenuInteractableController.ButtonClickedEvent -= OnButtonClicked;
//         MenuInteractableController.InputEndEditEvent -= OnInputEndEdit;
//         MenuInteractableController.ToggleStateChangedEvent -= OnToggleStatChanged;

//         RoomManager.GameStartingEvent -= OnGameStarting;
//     }
//     void OnButtonClicked(string buttonname)
//     {
//         if (!actions.ContainsKey(buttonname))
//         {
//             Debug.Log("Game Network: Action " + buttonname + " does not exists.");
//             return;
//         }
//         actions[buttonname].Invoke();
//     }
//     void OnInputEndEdit(string inputname, string value)
//     {
//         if (inputname == "Chat Input")
//             SendChatMessage();
//     }
//     void OnToggleStatChanged(string togglename, bool state)
//     {
//         if (togglename == "Public Toggle")
//             PhotonNetwork.CurrentRoom.IsOpen = state;
//     }
//     void OnGameStarting()
//     {
//         menucontroller.SetButtonVisibility("Start Button", false);
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         UpdateRoomState();
//     }

//     [SerializeField] float updateperiod;
//     float timer = 0;
//     // Update is called once per frame
//     void Update()
//     {
//         timer += Time.deltaTime;
//         if (timer >= updateperiod)
//         {
//             timer = 0;
//             menucontroller.SetLatency(PhotonNetwork.GetPing());
//         }
//     }

//     void UpdateRoomState()
//     {
//         menucontroller.ResetPlayerList();
//         if (PhotonNetwork.CurrentRoom == null)
//         {
//             menucontroller.SetText("Room Name Display", "You are currently not in a room.");
//             return;
//         }

//         menucontroller.SetText("Room Name Display", PhotonNetwork.CurrentRoom.Name);
//         menucontroller.SetToggleState("Open Toggle", PhotonNetwork.CurrentRoom.IsOpen);
//         menucontroller.SetInteractables(PhotonNetwork.IsMasterClient);

//         foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
//         {
//             menucontroller.AddPlayerListItem(player);
//         }

//         Debug.Log("Updated room info.");
//     }

//     void OnMenuVisibilityChanged(string menuname, bool visibilitystate)
//     {
//         if (menuname == "Popup Menu" && visibilitystate == true)
//         {
//             UpdateRoomState();
//         }
//     }
//     public override void OnPlayerEnteredRoom(Player player)
//     {
//         if (menucontroller.GetMenuVisibility("Popup Menu"))
//         {
//             UpdateRoomState();
//         }

//         menucontroller.AddChatMessageItem("Player " + player.NickName + " has joined the room and has been assigned with ID " + player.ActorNumber.ToString() + ".");


//     }
//     public override void OnPlayerLeftRoom(Player player)
//     {
//         if (menucontroller.GetMenuVisibility("Popup Menu"))
//         {
//             UpdateRoomState();
//         }

//         menucontroller.AddChatMessageItem("Player " + player.NickName + " with ID " + player.ActorNumber.ToString() + " has left the room.");
//     }
//     public override void OnMasterClientSwitched(Player newMasterClient)
//     {
//         if (menucontroller.GetMenuVisibility("Popup Menu"))
//         {
//             UpdateRoomState();
//         }
//     }
//     public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
//     {
//         if (menucontroller.GetMenuVisibility("Popup Menu"))
//         {
//             UpdateRoomState();
//         }
//     }
//     public override void OnRoomListUpdate(List<RoomInfo> roomList)
//     {
//         if (menucontroller.GetMenuVisibility("Popup Menu"))
//         {
//             UpdateRoomState();
//         }
//     }
// }
