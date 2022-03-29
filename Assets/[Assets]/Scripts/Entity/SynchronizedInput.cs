using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(PlayerInput))]
public class SynchronizedInput : MonoBehaviour
{
	[SerializeField] PlayerInput ControlInput;

	void Awake()
	{
		if (LocalStateController.instance != null)
			LocalStateController.instance.ActionMapChangeEvent += ActionMapChangeEvent;
		else
			Debug.LogError("Cannot find global state controller");
	}

    public void ActionMapChangeEvent(string ActionMap)
    {
        ControlInput.SwitchCurrentActionMap(ActionMap);
    }
}