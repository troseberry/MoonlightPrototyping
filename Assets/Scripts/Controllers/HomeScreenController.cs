using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScreenController : MonoBehaviour
{
    public GameObject messagesApp;


    public void OpenMessagesApp()
    {
        messagesApp.SetActive(true);
        GlobalNavBarController.Instance.SetCurrentOpenApp(messagesApp.GetComponent<MessageAppController>());
    }
}
