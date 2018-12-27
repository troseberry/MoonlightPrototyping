using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    public static HomeScreenController Instance;

    public GameObject messagesApp;
    public GameObject snakeApp;

    void Start()
    {
        Instance = this;
    }

    public void OpenMessagesApp()
    {
        messagesApp.SetActive(true);
        GlobalNavBarController.Instance.SetCurrentOpenApp(messagesApp.GetComponent<MessageAppController>());
    }

    public void OpenSnakeApp()
    {
        snakeApp.SetActive(true);
        GlobalNavBarController.Instance.SetCurrentOpenApp(snakeApp.GetComponent<SnakeAppController>());
        HideCanvas();
    }

    public void HideCanvas()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void ShowCanvas()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
