using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using MEC;

public class ChatBoard : MonoBehaviour
{
    [SerializeField] GameObject ChatEntryPrefab;
    [SerializeField] PhotonEventComponent receiver;
    [SerializeField] int MaxMessageCount = 200;

    void Awake()
    {
        receiver.AddListener(OnChatMessageReceived);
    }

    void OnChatMessageReceived(string sender, object[] data)
    {
        string messageType = (string)data[0];
        string message = (string)data[1];
        if (transform.childCount >= MaxMessageCount)
            Destroy(transform.GetChild(0).gameObject);

        GameObject newMessage = Instantiate(ChatEntryPrefab, transform);
        if (messageType == "chat")
            newMessage.GetComponent<TMP_Text>().text = $"{sender}: {message}";
        else if (messageType == "event")
            newMessage.GetComponent<TMP_Text>().text = $"{message}";
        else
            Debug.LogError($"Received unknown message: {message}; type:{messageType}");

        SortChatMessages();
        //LayoutRebuilder.ForceRebuildLayoutImmediate(transform);
    }

    void SortChatMessages()
    {
        Timing.CallDelayed(Timing.WaitForOneFrame, () =>
        {
            GameObject instantiated = Instantiate(ChatEntryPrefab, transform);
            instantiated.GetComponent<TMP_Text>().text = "";
            Destroy(instantiated);
        });
    }
}