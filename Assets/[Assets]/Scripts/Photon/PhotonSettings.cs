using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonSettings : MonoBehaviour
{
    PhotonView photonview;
    private void Awake()
    {
        photonview = GetComponent<PhotonView>();
    }
}
