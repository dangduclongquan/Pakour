using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;

public class PhotonPlayerBoard : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject PlayerEntryPrefab;
    [SerializeField] PhotonEventComponent ChatBroadcast;

    void UpdateRoomState()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            GameObject entry = Instantiate(PlayerEntryPrefab, transform);
            entry.GetComponent<PlayerListItem>().Initiate(player, ChatBroadcast);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        UpdateRoomState();
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        UpdateRoomState();
        ChatBroadcast.RaiseEvent(new object[] {"event", $"(ID:{player.ActorNumber}){player.NickName} has joined the room"});
    }

    public override void OnPlayerLeftRoom(Player player)
    {
        UpdateRoomState();
        ChatBroadcast.RaiseEvent(new object[] {"event", $"(ID:{player.ActorNumber}){player.NickName} has left the room"});
    }
}