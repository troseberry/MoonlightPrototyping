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
    private GameObject currentContactNameText;

    private GameObject previousContactObj;
    private GameObject currentContactObj;
    private MessageContact currentlySelectedContact;
    private MessageThread currentlySelectedMessageThread;

    public GameObject messagesScrollContent;
    public GameObject incomingMessagePrefab;
    public GameObject outgoingMessagePrefab;
    
    void Start()
    {
        contactsView = transform.Find("Contacts").gameObject;
        messagesView = transform.Find("Messages").gameObject;

        appName = transform.Find("AppHeader").Find("AppName").gameObject;
        currentContactNameText = transform.Find("AppHeader").Find("CurrentContactName").gameObject;
    }

    
    void Update()
    {
        
    }

    public void OpenMessageThread()
    {
        if (currentContactObj) previousContactObj = currentContactObj;
        currentContactObj = EventSystem.current.currentSelectedGameObject;

        currentlySelectedContact = currentContactObj.GetComponent<MessageContact>();
        currentlySelectedMessageThread = currentContactObj.GetComponent<MessageThread>();

        if (currentlySelectedContact.GetContactName().Length > 0)
        {
            messagesView.SetActive(true);
            CreateMessageUIObjects();

            contactsView.SetActive(false);

            appName.SetActive(false);
            currentContactNameText.SetActive(true);
            currentContactNameText.GetComponent<Text>().text = currentlySelectedContact.GetContactName();
        }
        
    }

    public void CreateMessageUIObjects()
    {
        if (currentContactObj == previousContactObj) Debug.Log("opened same thread");

        for (int d = 0; d < messagesScrollContent.transform.childCount; d++)
        {
            Destroy(messagesScrollContent.transform.GetChild(d).gameObject);
        }


        Message[] messagesToPopulate = currentlySelectedMessageThread.GetThreadMessages();
        for (int i = 0; i < messagesToPopulate.Length; i++)
        {
            GameObject createdMessage;
            //instantiate message prefab
            if (messagesToPopulate[i].GetMessageType() == MessageType.INCOMING)
            {
                createdMessage = Instantiate(incomingMessagePrefab, messagesScrollContent.transform);
            }
            else
            {
                createdMessage = Instantiate(outgoingMessagePrefab, messagesScrollContent.transform);
            }

            //set message prefab obj's MessageUI component's assocMessage
            createdMessage.GetComponent<MessageUI>().SetAssociatedMessage(messagesToPopulate[i]);

            //update ui
            createdMessage.GetComponent<MessageUI>().UpdateMessageUI();
        }
    }




    public void BackButtonPressed()
    {
        if (messagesView.activeSelf)
        {
            contactsView.SetActive(true);
            messagesView.SetActive(false);

            appName.SetActive(true);
            currentContactNameText.SetActive(false);
        }
    }
}
