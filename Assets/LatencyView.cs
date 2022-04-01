using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class LatencyView : MonoBehaviour
{
    [SerializeField] TMP_Text tmpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = PhotonNetwork.GetPing().ToString() + " ms";
    }
}
