using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NotMine : MonoBehaviour
{
    [SerializeField] PhotonView view;
    [SerializeField] Component[] thingstodestroy;

    private void Awake()
    {
        if(!view.IsMine)
        {
            foreach (Component thingtodestroy in thingstodestroy)
                Destroy(thingtodestroy);
        }
    }
}
