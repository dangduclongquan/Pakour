using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

// Currently unused
public class PhotonGameController : MonoBehaviourPunCallbacks
{
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void ToggleRoomVisibility(bool state)
    {
        PhotonNetwork.CurrentRoom.IsOpen = state;
    }

    public bool IsOnlineSession()
    {
        // TODO
        return false;
    }

    public int GetPing()
    {
        return PhotonNetwork.GetPing();
    }
}
