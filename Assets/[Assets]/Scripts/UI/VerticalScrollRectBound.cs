using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalScrollRectBound : MonoBehaviour
{
    [SerializeField] bool boundpositive;
    ScrollRect scrollRect;


    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(Vector2 value)
    {
        if(scrollRect.content.anchoredPosition.y > 0 == boundpositive)
        {
            scrollRect.content.anchoredPosition=new Vector2(scrollRect.content.anchoredPosition.x, 0);
        }
    }
}
