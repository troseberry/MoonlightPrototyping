using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessageAppController : MonoBehaviour
{
    
    private GameObject contactsView;
    private GameObject messagesView;

    private GameObject appName;
    private GameObject currentContactName;

    private MessageContact currentlySelectedContact;
    
    void Start()
    {
        contactsView = transform.Find("Contacts").gameObject;
        messagesView = transform.Find("Messages").gameObject;

        appName = transform.Find("AppHeader").Find("AppName").gameObject;
        currentContactName = transform.Find("AppHeader").Find("CurrentContactName").gameObject;
    }

    
    void Update()
    {
        
    }

    public void OpenMessageThread()
    {
        contactsView.SetActive(false);
        messagesView.SetActive(true);

        appName.SetActive(false);
        currentContactName.SetActive(true);
        currentContactName.GetComponent<Text>().text = EventSystem.current.currentSelectedGameObject.GetComponent<MessageContact>().GetContactName();
    }

    public void BackButtonPressed()
    {
        if (messagesView.activeSelf)
        {
            contactsView.SetActive(true);
            messagesView.SetActive(false);

            appName.SetActive(true);
            currentContactName.SetActive(false);
        }
    }
}
