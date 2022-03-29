// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using TMPro;
// using Photon.Pun;
// public class SpectatorController : MonoBehaviour
// {
//     int mod(int a, int b)
//     {
//         int r = a % b;
//         return r < 0 ? r + b : r;
//     }

//     [SerializeField] SpectatorSettings SpectatorSettings;
//     [SerializeField] HeadlightController spectatorheadlight;

//     [SerializeField] bool canSwitchToThirdPersonWhilePlaying;
//     [SerializeField] bool canSwitchToThirdPersonWhileSpectating;
//     bool disallowSwitching;

//     PlayerInput spectatorinput;
    
//     private void OnEnable()
//     {
//         Survivor.SurvivorSpawnedEvent += OnSurvivorSpawned;
//         Survivor.SurvivorDyingEvent += OnSurvivorDying;
//         //PlayerView.PlayerSpawnedEvent += OnPlayerSpawned;
//     }
//     private void OnDisable()
//     {
//         Survivor.SurvivorSpawnedEvent -= OnSurvivorSpawned;
//         Survivor.SurvivorDyingEvent -= OnSurvivorDying;
//         //PlayerView.PlayerSpawnedEvent += OnPlayerSpawned;
//     }
//     void OnSurvivorSpawned(Survivor survivor)
//     {
//         survivorlist.Add(survivor.GetInstanceID(), survivor);

//         if (survivor.controller == PhotonNetwork.LocalPlayer)
//         {
//             disallowSwitching = true;

//             if (!canSwitchToThirdPersonWhilePlaying) SpectatorSettings.viewmode = ViewMode.FirstPerson;

//             Spectate(survivor);
//         }
//     }
//     void OnSurvivorDying(Survivor survivor)
//     {
//         if (survivor == Survivor.mine)
//         {
//             disallowSwitching = false;

//             spectatorinput.enabled = canSwitchToThirdPersonWhileSpectating;
//             if (!canSwitchToThirdPersonWhileSpectating) SpectatorSettings.viewmode = ViewMode.FirstPerson;
//         }
//         if (survivor.GetInstanceID() == spectatedID)
//             OnNextPlayer();

//         survivorlist.Remove(survivor.GetInstanceID());
//     }

//     SortedList<int, Survivor> survivorlist = new SortedList<int, Survivor>();
//     int spectatedID;

//     private void Update()
//     {
//         if (survivorlist.ContainsKey(spectatedID))
//             spectatorheadlight.isOn = survivorlist[spectatedID].isHeadlightOn;
//     }

//     void OnPrevPlayer()
//     {
//         if (disallowSwitching) return;
//         if (survivorlist.ContainsKey(spectatedID))
//             Spectate(survivorlist.Values[mod(survivorlist.IndexOfKey(spectatedID) - 1, survivorlist.Count)]);
//     }
//     void OnNextPlayer()
//     {
//         if (disallowSwitching) return;
//         if (survivorlist.ContainsKey(spectatedID))
//             Spectate(survivorlist.Values[mod(survivorlist.IndexOfKey(spectatedID) + 1, survivorlist.Count)]);
//     }

//     void Spectate(Survivor survivor)
//     {
//         survivor.SpectateUsingSettings(SpectatorSettings);
//         spectatedID = survivor.GetInstanceID();
//     }
//     /*
//     void OnPlayerSpawned(PlayerView player)
//     {

//     }
//     void OnPlayerDied(PlayerView player)
//     {

//     }
    


//     [SerializeField] Canvas SpectatorHUD;
//     [SerializeField] TMP_Text survivorslist;
//     public KeyCode previousplayerkey;
//     public KeyCode nextplayerkey;
//     // Start is called before the first frame update
//     void Start()
//     {

//     }

//     int currentindex;
//     // Update is called once per frame
//     void Update()
//     {
//         if (PlayerView.instances.Length == 0) return;

//         if (PlayerView.local != null)
//         {
//             PlayerView.local.isOnFirstPersonView = true;
//             SpectatorHUD.enabled = false;
//             return;
//         }

//         if (Input.GetKeyDown(previousplayerkey))
//             currentindex--;
//         if (Input.GetKeyDown(nextplayerkey))
//             currentindex++;
//         currentindex = mod(currentindex, PlayerView.instances.Length);

//         PlayerView.instances[currentindex].isOnFirstPersonView = true;

//         SpectatorHUD.enabled = true;
//         string survivors = "";
//         foreach (PlayerView player in PlayerView.instances)
//         {
//             if (player.isOnFirstPersonView)
//                 survivors += "<color=#00FFFF>\n[" + player.playerinfo.ActorNumber + "]" + player.playerinfo.NickName + "</color>";
//             else
//                 survivors += "<color=yellow>\n[" + player.playerinfo.ActorNumber + "]" + player.playerinfo.NickName + "</color>";
//         }
//         survivorslist.text = survivors;
//     */
// }
