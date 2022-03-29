using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindowController : MonoBehaviour
{
    [SerializeField] Canvas canvas;

    public static WindowController instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

        canvas.gameObject.SetActive(true);
        errormessage = canvas.GetComponentInChildren<TMP_Text>(true);
        errorwindow = canvas.transform.GetChild(0).gameObject;
    }

    TMP_Text errormessage;
    GameObject errorwindow;

    public void ShowErrorMessage(string message)
    {
        if (errormessage != null) errormessage.text = message;
        if (errorwindow != null) errorwindow.SetActive(true);
    }
}
