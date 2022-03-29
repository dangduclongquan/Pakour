using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionHeadlightToggle : MonoBehaviour
{
    [SerializeField] Light[] headlights;
    [SerializeField] MeshRenderer headlightbulb;
    [SerializeField] Material lightonmaterial;
    [SerializeField] Material lightoffmaterial;

    public bool IsOn {get; private set;}

    public void Toggle()
    {
        IsOn = !IsOn;
        if (headlightbulb != null)
        {
            if (IsOn)
                headlightbulb.material = lightonmaterial;
            else
                headlightbulb.material = lightoffmaterial;
        }

        foreach (Light headlight in headlights)
            headlight.gameObject.SetActive(IsOn);
    }
}
