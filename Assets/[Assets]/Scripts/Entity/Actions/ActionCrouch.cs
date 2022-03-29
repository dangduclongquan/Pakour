using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ActionCrouch : MonoBehaviour
{
    public LayerMask uncrouchlayermask;
    [SerializeField] Transform CameraMountTransform;
    [SerializeField] CharacterController character;

    [Header("Configurations")]
    [SerializeField] float crouchheight = 1.2f;
    [SerializeField] float transitionspeed = 1f;

    public bool IsCrouching {get; private set;}

    bool standingUp;
    float defaultHeight;
    Vector3 targetPosition;
    float height
    {
        get { return character.height;}
        set
        {
            character.height = value;
            character.center = new Vector3(character.center.x, character.height / 2, character.center.z);
        }
    }

    void Awake()
    {
        defaultHeight = height;
        standingUp = false;
        targetPosition = CameraMountTransform.localPosition;
    }

    void Update()
    {
        if (standingUp)
            TryUncrouch();

        CameraMountTransform.localPosition = Vector3.MoveTowards(CameraMountTransform.localPosition, targetPosition, transitionspeed * Time.deltaTime);
        height = CameraMountTransform.localPosition.y;
        
        if (height == defaultHeight)
        {
            IsCrouching = false;
            standingUp = false;
        }
        else
            IsCrouching = true; 
    }

    public void Crouch()
    {
        standingUp = false;
        targetPosition = new Vector3(targetPosition.x, crouchheight, targetPosition.z);
        IsCrouching = true;
    }

    public void StandUp()
    {
        standingUp = true;
    }

    public bool TryUncrouch()
    {
        Vector3 toppos = (character.transform.position + new Vector3(character.center.x, defaultHeight / 2, character.center.z)) + Vector3.up * (defaultHeight / 2);
        Vector3 botpos = (character.transform.position + new Vector3(character.center.x, defaultHeight / 2, character.center.z)) - Vector3.up * (defaultHeight / 2 - character.radius);
        
        if (!Physics.CheckCapsule(toppos, botpos, character.radius-character.skinWidth, uncrouchlayermask))
        {
            targetPosition = new Vector3(targetPosition.x, defaultHeight, targetPosition.z);
            return true;
        }
        else
        {
            targetPosition = new Vector3(targetPosition.x, crouchheight, targetPosition.z);
            Debug.DrawLine(toppos, botpos, Color.red);
            return false;
        }
    }
}
