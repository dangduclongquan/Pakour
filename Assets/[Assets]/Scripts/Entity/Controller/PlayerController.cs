using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent( typeof(PlayerBehavior) )]
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerBehavior Behavior;
    // [SerializeField] CameraController Camera;

    void OnLook(InputValue inputvalue)
    {
        Behavior.DoLook(inputvalue.Get<Vector2>());
    }

    void OnAction(InputValue inputvalue)
    {
        // TO DO... something?
    }

    void OnMove(InputValue inputvalue)
    {
        Behavior.DoMove(inputvalue.Get<Vector2>());
    }

    void OnJump(InputValue inputvalue)
    {
        Behavior.DoJump();
    }

    void OnSprint(InputValue inputvalue)
    {
        if (inputvalue.isPressed)
            Behavior.DoSprint();
        else
            Behavior.StopSprint();
    }

    void OnCrouch(InputValue inputvalue)
    {
        if (inputvalue.isPressed)
            Behavior.DoCrouch();
        else
            Behavior.StopCrouch();
    }

    void OnHeadlightToggle(InputValue inputvalue)
    {
        Behavior.DoHeadlightToggle();
    }
}
