using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallReset : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform headTransform;
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
            characterController.enabled = false;
            characterController.transform.position= Vector3.zero;
            characterController.transform.rotation = Quaternion.identity;
            headTransform.rotation = Quaternion.identity;
            characterController.enabled = true;
        }
    }

}
