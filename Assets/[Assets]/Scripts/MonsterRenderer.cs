using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class MonsterRenderer : MonoBehaviour
{
    [SerializeField] Volume volume;

    MeshFilter meshFilter;
    // Start is called before the first frame update
    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }

    bool isVisible;
    float _instensity = 0;
    // Update is called once per frame
    void Update()
    {
        if (Camera.main == null) return;

        isVisible = false;
        foreach (Vector3 vertex in meshFilter.mesh.vertices)
        {
            Vector3 WSvertex = transform.TransformPoint(vertex);
            Vector3 viewportpoint = Camera.main.WorldToViewportPoint(WSvertex);
            int layermask = 1 << 0;

            if (viewportpoint.x >= 0 && viewportpoint.x <= 1 && viewportpoint.y >= 0 && viewportpoint.y <= 1 && viewportpoint.z > 0 && !Physics.Raycast(Camera.main.transform.position, WSvertex - Camera.main.transform.position, Vector3.Distance(Camera.main.transform.position, WSvertex), layermask))
            {
                isVisible = true;
                break;
            }
        }

        if (isVisible)
        {
            _instensity += 0.1f*Time.deltaTime;
        }
        else
        {
            _instensity -= 0.05f*Time.deltaTime;
        }
        _instensity = Mathf.Clamp01(_instensity);
        SetIntensity(_instensity);
    }

    void SetIntensity(float intensity)
    {
        volume.weight = intensity;
    }
}
