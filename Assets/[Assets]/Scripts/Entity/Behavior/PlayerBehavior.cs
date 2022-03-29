using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActionCrouch), typeof(ActionLook))]
[RequireComponent(typeof(ActionBasicMovement), typeof(ActionHeadlightToggle))]
public class PlayerBehavior : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] ActionCrouch Crouch;
    [SerializeField] ActionBasicMovement Movement;
    [SerializeField] ActionHeadlightToggle HeadlightToggle;
    [SerializeField] ActionLook Look;

    public float sprintspeed = 8f;
    public float walkspeed = 4f;
    public float crouchspeed = 2f;

    public bool IsGrounded { get { return Movement.IsGrounded; } }
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get { return Crouch.IsCrouching; } }
    public bool IsHeadlightOn { get { return HeadlightToggle.IsOn; } }

    bool sprintPaused;
    void Awake()
    {
        sprintPaused = false;
    }

    void Update()
    {
        // Crouch is most important
        if (IsCrouching == true)
        {
            Movement.SetSpeed(crouchspeed);
        }
        else
        {
            // Resume sprint if interrupted 
            if (sprintPaused || IsSprinting)
            {
                Movement.SetSpeed(sprintspeed);
                IsSprinting = true;
                sprintPaused = false;
            }
            else
            {
                Movement.SetSpeed(walkspeed);
            }
        }
    }

    public void DoLook(Vector2 val)
    {
        Look.Look(val);
    }

    public void DoMove(Vector2 val)
    {
        Movement.Move(val);
    }

    public void DoJump()
    {
        Movement.Jump();
    }

    public void DoSprint()
    {
        if (IsCrouching)
        {
            sprintPaused = true;
        }
        else
        {
            sprintPaused = false;
            IsSprinting = true;
        }
    }

    public void StopSprint()
    {
        sprintPaused = false;
        IsSprinting = false;
    }

    public void DoCrouch()
    {
        if (IsSprinting)
            sprintPaused = true;
        IsSprinting = false;
        Crouch.Crouch();
    }

    public void StopCrouch()
    {
        Crouch.StandUp();
        if (sprintPaused)
        {
            sprintPaused = false;
            IsSprinting = true;
        }
        else
        {
            IsSprinting = false;
        }
    }

    public void DoHeadlightToggle()
    {
        HeadlightToggle.Toggle();
    }
}