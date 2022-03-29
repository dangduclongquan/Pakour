using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] Transform cameratransform;

    public float minZ;
    public float maxZ;

    float value;
    private void OnEnable()
    {
        value = 0;
    }
    // Update is called once per frame
    void Update()
    {
        cameratransform.localPosition = new Vector3(cameratransform.localPosition.x, cameratransform.localPosition.y, Mathf.Clamp(cameratransform.localPosition.z + value * Time.deltaTime, minZ, maxZ));
    }

    void OnZoom(InputValue inputvalue)
    {
        value = inputvalue.Get<float>();
    }
}
