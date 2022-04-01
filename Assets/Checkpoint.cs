using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        FallReset fallReset = other.GetComponent<FallReset>();
        if (fallReset!=null)
        {
            if (!fallReset.checkpoints.Contains(transform))
                fallReset.checkpoints.Add(transform);
        }
    }
}
