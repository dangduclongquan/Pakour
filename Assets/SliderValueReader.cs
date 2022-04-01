using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class SliderValueReader : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text textToDisplayValue;

    private void Awake()
    {
        slider.onValueChanged.AddListener(SliderValueChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        textToDisplayValue.text = ((int)slider.value).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SliderValueChanged(float value)
    {
        textToDisplayValue.text = ((int)value).ToString();
    }
}
