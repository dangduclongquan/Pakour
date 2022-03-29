using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuInteractableController : MonoBehaviour
{
    public delegate void ButtonClicked(string buttonname);
    public static event ButtonClicked ButtonClickedEvent;
    Button button;

    public delegate void InputValueChanged(string inputname, string value);
    public static event InputValueChanged InputValueChangedEvent;
    public delegate void InputEndEdit(string inputname, string value);
    public static event InputEndEdit InputEndEditEvent;
    TMP_InputField input;

    public delegate void ToggleStateChanged(string togglename, bool state);
    public static event ToggleStateChanged ToggleStateChangedEvent;
    Toggle toggle;

    private void Awake()
    {
        if (TryGetComponent<Button>(out button))
        {
            button.onClick.AddListener(OnClicked);
        }
        if (TryGetComponent<TMP_InputField>(out input))
        {
            input.onValueChanged.AddListener(OnValueChanged);
            input.onEndEdit.AddListener(OnEndEdit);
        }
        if (TryGetComponent<Toggle>(out toggle))
        {
            toggle.onValueChanged.AddListener(OnStateChanged);
        }
    }
    void OnClicked()
    {
        if(ButtonClickedEvent != null) ButtonClickedEvent(button.gameObject.name);
    }

    void OnValueChanged(string value)
    {
        if (InputValueChangedEvent != null) InputValueChangedEvent(input.gameObject.name, value);
    }
    void OnEndEdit(string value)
    {
        if (InputEndEditEvent != null) InputEndEditEvent(input.gameObject.name, value);
    }

    void OnStateChanged(bool state)
    {
        if (ToggleStateChangedEvent != null) ToggleStateChangedEvent(toggle.gameObject.name, state);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
