using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonCollider : MonoBehaviour
{
    [SerializeField] PhotonView photonview;
    [SerializeField] PhotonTransformView head;
    [SerializeField] CharacterController character;


    bool _enablecollisionbetweenplayers;
    public bool enablecollisionbetweenplayers
    {
        get
        {
            return _enablecollisionbetweenplayers;
        }
        set
        {
            if (!photonview.IsMine)
            {
                character.enabled = value;
                if (value)
                {
                    character.height = head.transform.localPosition.y + character.radius;
                    character.center = new Vector3(character.center.x, character.height / 2, character.center.z);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enablecollisionbetweenplayers)
        {
            character.height = head.transform.localPosition.y + character.radius;
            character.center = new Vector3(character.center.x, character.height / 2, character.center.z);
        }
    }
}
