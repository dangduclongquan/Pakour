using UnityEngine;
using Photon.Pun;
using System.Collections;

[DisallowMultipleComponent]
public abstract class Menu : MonoBehaviourPunCallbacks
{
    
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    // Override if menu requires clean-up/saving/etc before closing
    public virtual bool Close()
    {
        gameObject.SetActive(false);
        return true;
    }
}
