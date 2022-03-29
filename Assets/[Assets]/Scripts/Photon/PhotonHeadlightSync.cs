// using UnityEngine;
// using Photon.Pun;

// public class PhotonHeadlightSync : MonoBehaviour
// {
//     [SerializeField] HeadlightController headlightController;

//     PhotonView photonview;
//     void Awake()
//     {
//         photonview = GetComponent<PhotonView>();

//         if (photonview.IsMine)
//         {
//             headlightController.OnHeadlightToggled.AddListener(OnHeadlightToggled);
//         }
//         else
//         {
//             photonview.RPC("UpdateHeadlight", photonview.Owner);
//         }
//     }
//     void OnHeadlightToggled(bool isOn)
//     {
//         photonview.RPC("SetHeadlight", RpcTarget.Others, isOn);
//     }

//     [PunRPC]
//     void SetHeadlight(bool isOn, PhotonMessageInfo info)
//     {
//         if (info.Sender == photonview.Owner)
//         {
//             headlightController.isOn = isOn;
//         }
//     }
//     [PunRPC]
//     void UpdateHeadlight(PhotonMessageInfo info)
//     {
//         photonview.RPC("SetHeadlight", info.Sender, headlightController.isOn);
//     }
// }