using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class MouseSensitivitySlider : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Awake()
    {
        slider.onValueChanged.AddListener(SlideValueChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SlideValueChanged(float value)
    {
        PlayerPrefs.SetFloat("mouseSensitivity", value/100);
        PlayerPrefs.Save();
    }
}
