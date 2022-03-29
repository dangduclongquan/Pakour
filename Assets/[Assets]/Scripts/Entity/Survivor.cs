using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Survivor : MonoBehaviourPunCallbacks
{
    public Transform Head;
    public Transform Location;
    public GameObject FirstPersonModel;
    public GameObject ThirdPersonModel;

    public event Action SurvivorKilledEvent;

    public void Kill()
    {
        // TODO: Clean up the object, do network syncs and checks, etc.
        SurvivorKilledEvent?.Invoke();
    }

    public void DisplayFirstPersonModel()
    {
        FirstPersonModel.SetActive(true);
        ThirdPersonModel.SetActive(false);
    }

    public void DisplayThirdPersonModel()
    {
        ThirdPersonModel.SetActive(true);
        FirstPersonModel.SetActive(false);
    }

    // TODO: Add other relevant methods when needed to expose Survivor data and handle network stuff
}
