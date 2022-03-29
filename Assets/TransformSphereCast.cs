using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformSphereCast : MonoBehaviour
{
    [SerializeField] Transform origin;
    [SerializeField] Transform destination;
    [SerializeField] Transform hitoutput;

    [SerializeField] float radius;
    [SerializeField] LayerMask layermask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycasthit;
        if (Physics.SphereCast(origin.position, radius, destination.position - origin.position, out raycasthit, Vector3.Distance(origin.position, destination.position), layermask))
        {
            hitoutput.position = origin.position + (destination.position - origin.position).normalized * raycasthit.distance;
        }
        else
        {
            hitoutput.position = destination.position;
        }
    }
}
