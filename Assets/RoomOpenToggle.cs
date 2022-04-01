using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class RoomOpenToggle : MonoBehaviour
{
    [SerializeField] Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom != null)
        {
            toggle.interactable = PhotonNetwork.IsMasterClient;
            toggle.enabled = PhotonNetwork.CurrentRoom.IsOpen;
        }
    }

    void ToggleStateChanged(bool value)
    {
        PhotonNetwork.CurrentRoom.IsOpen = value; 
    }
}
