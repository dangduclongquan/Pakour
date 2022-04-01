using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionLook : MonoBehaviour
{
    public Transform verticaltransform;
    public Transform horizontaltransform;
    Vector2 value;

    private void OnEnable()
    {
        value = Vector2.zero;
    }

    void Update()
    {
        float x = verticaltransform.localEulerAngles.x;
        if (x > 180)
            x -= 360;
        if (x < -180)
            x += 360;
        horizontaltransform.localEulerAngles = new Vector3(horizontaltransform.localEulerAngles.x, horizontaltransform.localEulerAngles.y + value.x * Time.deltaTime, horizontaltransform.localEulerAngles.z);
        verticaltransform.localEulerAngles = new Vector3(Mathf.Clamp(x - value.y * Time.deltaTime, -90f, 90f), verticaltransform.localEulerAngles.y, verticaltransform.localEulerAngles.z);
    }

    public void Look(Vector2 val)
    {
        value = val* PlayerPrefs.GetFloat("mouseSensitivity");
    }
}
