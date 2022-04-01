using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform headTransform;

    public List<Transform> checkpoints;

    public float yResetValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(characterController.transform.position.y<=yResetValue)
        {
            Transform checkpoint = checkpoints[checkpoints.Count - 1];

            characterController.enabled = false;
            characterController.transform.position= checkpoint.position;
            characterController.transform.rotation = checkpoint.rotation; 
            headTransform.rotation = checkpoint.rotation;
            characterController.enabled = true;
        }
    }

}
