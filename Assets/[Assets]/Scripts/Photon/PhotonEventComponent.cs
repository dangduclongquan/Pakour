using ExitGames.Client.Photon;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using System.Collections;
using System;

// A component that allows networked access based on an EventName channel
// All object sharing the same channel (EventName) needs to share the same message object structure
public class PhotonEventComponent : MonoBehaviour, IOnEventCallback
{
    // Interface implementation that handles the network request
    [SerializeField] byte channel = 1;
    [SerializeField] string EventName;

    private event Action<string, object[]> ReceiveEvent;

    void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    private void Relay(string sender, object[] data)
    {
        ReceiveEvent?.Invoke(sender, data);
    }

    public void AddListener(Action<string, object[]> listener)
    {
        ReceiveEvent += listener;
    }

    public void RemoveListener(Action<string, object[]> listener)
    {
        ReceiveEvent -= listener;
    }

    public void RaiseEvent(object[] data, string target = "")
    {
        RaiseEventOptions options;
        if (target != "")
        {
            Debug.LogWarning($"Target {target} is not implemented. Defaulting to all");
        }

        options = new RaiseEventOptions { Receivers = ReceiverGroup.All }; 
        PhotonNetwork.RaiseEvent(channel, new object[] {EventName, data}, options, SendOptions.SendReliable);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code != channel)
            return;

        object[] eventData = (object[])photonEvent.CustomData;
        string eventName = (string) eventData[0];
        object[] data = (object[]) eventData[1];
        int senderID = photonEvent.Sender; 
        
        string senderName;
        string sender;

        if (senderID == 0)
        {
            sender = "Server";
            Debug.LogWarning("Server sent a message. Is this intended?");
        }
        else
        {
            senderName = PhotonNetwork.CurrentRoom.GetPlayer(senderID).NickName;
            sender = $"(ID:{senderID}){senderName}";
        }
        Relay(sender, data);
    }
}