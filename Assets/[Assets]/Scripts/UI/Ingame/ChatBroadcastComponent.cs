using UnityEngine;
using System.Collections;
using TMPro;

public class ChatBroadcastComponent : MonoBehaviour
{
    [SerializeField] PhotonEventComponent sender;
    [SerializeField] TMP_InputField input;

    public void SendChatMessage()
    {
        string message = input.text;
        if (message == "") return;

        sender.RaiseEvent(new object[] {"chat", message});
        input.text = "";
    }
}