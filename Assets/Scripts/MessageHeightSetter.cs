using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MessageHeightSetter : MonoBehaviour
{
    public RectTransform messageBackgroundRect;
    
    private RectTransform messageObjectRect;

    void Start()
    {
        Canvas.ForceUpdateCanvases();
        messageObjectRect = GetComponent<RectTransform>();

        messageObjectRect.sizeDelta = new Vector2(messageObjectRect.sizeDelta.x,    messageBackgroundRect.rect.height);
    }
}
