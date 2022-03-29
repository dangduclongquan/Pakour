using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SynchronizedInput))]
public class OverlayController : MonoBehaviour
{
	[SerializeField] GameObject Menu;

    void OnMenu()
    {
    	Menu.SetActive(true);
    	LocalStateController.instance.Menu();
    }

    void OnCloseMenu()
    {
    	Menu.SetActive(false);
    	LocalStateController.instance.ResumePlay();
    }
}