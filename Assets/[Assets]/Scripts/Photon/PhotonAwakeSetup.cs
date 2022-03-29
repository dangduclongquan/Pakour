// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Photon.Pun;

// public class PhotonAwakeSetup : MonoBehaviour
// {
//     [SerializeField] SurvivorData SurvivorData;
//     [SerializeField] HeadlightController HeadlightController;
//     [SerializeField] PhotonCollider photoncollider;
//     public bool enablecollisionbetweenplayers;
//     PhotonView photonview;
//     private void Awake()
//     {
//         photonview = GetComponent<PhotonView>();
//         if(photonview.IsMine)
//         {
//             HeadlightController.isOn = true;
//             SurvivorData.isControllable = true;
//         }
//         else
//         {
//             SurvivorData.isControllable = false;
//             photoncollider.enablecollisionbetweenplayers = enablecollisionbetweenplayers;
//         }

//         Destroy(this);
//     }
// }
