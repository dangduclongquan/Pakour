using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSwitch : MonoBehaviour
{
    [SerializeField] GameObject thingToSwitch;
    [SerializeField] KeyCode switchKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(switchKey))
        {
            thingToSwitch.SetActive(!thingToSwitch.activeSelf);
        }
    }
}
