using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class RoomNameView : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = PhotonNetwork.CurrentRoom.Name;
    }
}
