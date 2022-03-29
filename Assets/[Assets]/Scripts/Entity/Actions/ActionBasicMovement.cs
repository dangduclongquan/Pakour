using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ActionBasicMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform relativetransform;
    [SerializeField] Transform head;
    public Transform groundCheck;
    public LayerMask groundMask;

    [Header("Configurations")]
    public float defaultspeed = 4f;
    public float gravity = -10f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.4f;

    Vector2 value;
    float verticalvelocity;
    public float Speed { get; private set; }
    public bool IsGrounded { get; private set; }

    void Awake()
    {
        Speed = defaultspeed;
    }

    void OnEnable()
    {
        value = Vector2.zero;
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void Update()
    {
        IsGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (IsGrounded && verticalvelocity < 0)
            verticalvelocity = -2f;
        if (isHeadbumping && verticalvelocity > 0)
            verticalvelocity = 0;
        verticalvelocity += gravity * Time.deltaTime;

        characterController.Move((relativetransform.right * value.x + relativetransform.forward * value.y).normalized * Speed * Time.deltaTime);
        characterController.Move(relativetransform.up * verticalvelocity * Time.deltaTime);
    }

    public void Move(Vector2 val)
    {
        value = val;
    }

    public void Jump()
    {
        if (IsGrounded && !isHeadbumping)
        {
            verticalvelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    bool isHeadbumping
    {
        get
        {
            return Physics.CheckSphere(head.position, characterController.radius, groundMask);
        }
    }
}