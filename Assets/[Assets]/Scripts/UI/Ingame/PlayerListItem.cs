using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text playername;
    [SerializeField] TMP_Text playerID;
    [SerializeField] Toggle ownertoggle;
    [SerializeField] Button kickbutton;

    private PhotonEventComponent ChatBroadcast;
    private Player player;

    public void Initiate(Player playerinfo, PhotonEventComponent chat)
    {
        player = playerinfo;
        playername.text = playerinfo.NickName;
        playerID.text = playerinfo.ActorNumber.ToString();
        ChatBroadcast = chat;
        ownertoggle.isOn = playerinfo.IsMasterClient == true;
        kickbutton.gameObject.SetActive(PhotonNetwork.IsMasterClient == true);

    }

    public void Kick()
    {
        ChatBroadcast.RaiseEvent(new object[] {"event", $"(ID:{player.ActorNumber}){player.NickName} has been kicked"});
        PhotonNetwork.CloseConnection(player);
    }
}
