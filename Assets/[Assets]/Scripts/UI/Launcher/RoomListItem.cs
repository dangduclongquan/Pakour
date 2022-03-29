using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class RoomListItem : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text roomnametext;
    public TMP_Text playercounttext;

    // Such a shitty way to do it, but it's 2am
    [HideInInspector] public TMP_InputField roomNameInput;
    [HideInInspector] public MultiplayerMenu menu;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 1) {
            roomNameInput.text = roomnametext.text;
        }
        else if (eventData.clickCount > 1) {
            menu.ConnectToRoom();
        }
    }
}
