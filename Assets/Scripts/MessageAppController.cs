using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessageAppController : MonoBehaviour
{
    public static MessageAppController Instance;

    private GameObject contactsView;
    private GameObject messagesView;

    private GameObject appName;
    private GameObject currentContactNameText;

    private GameObject previousContactObj;
    private GameObject currentContactObj;
    private MessageContact currentlySelectedContact;
    private MessageThread currentlySelectedMessageThread;

    public GameObject contactScrollContent;
    public GameObject contactPrefab;

    public GameObject messagesScrollContent;
    public GameObject incomingMessagePrefab;
    public GameObject outgoingMessagePrefab;

    

    private List<MessageContact> contactsList;
    private List<MessageThread> threadsList;

    
    void Start()
    {
        Instance = this;

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
        if (currentContactObj == previousContactObj) return;

        for (int d = 0; d < messagesScrollContent.transform.childCount; d++)
        {
            Destroy(messagesScrollContent.transform.GetChild(d).gameObject);
        }


        // instead of getting messages from messagethread component, this should eventually
        // look at the select contact obj's threadid and load messages from a file
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


    // used when a new person "texts" the player
    public void CreateNewContact(string name, string last, string date)
    {
        GameObject createdContact = Instantiate(contactPrefab, contactScrollContent.transform);
        createdContact.GetComponent<MessageContact>().InitContact(name, last, date);
        createdContact.GetComponent<Button>().onClick.AddListener( () => OpenMessageThread());
    }

    public void CreateNewMessage(MessageType type, string messageContent, string date)
    {
        Message createdMessage = new Message(type, messageContent, date); 
        GameObject createdMessageObj;

        //instantiate message prefab
        if (type == MessageType.INCOMING)
        {
            createdMessageObj = Instantiate(incomingMessagePrefab, messagesScrollContent.transform);
        }
        else
        {
            createdMessageObj = Instantiate(outgoingMessagePrefab, messagesScrollContent.transform);
        }

        //set message prefab obj's MessageUI component's assocMessage
        createdMessageObj.GetComponent<MessageUI>().SetAssociatedMessage(createdMessage);

        //update ui
        createdMessageObj.GetComponent<MessageUI>().UpdateMessageUI();
    }





    public void SetContactsList(List<MessageContact> newContacts)
    {
        contactsList = newContacts;
    }

    public List<MessageContact> GetContactsList() { return contactsList; }

    public void SetThreadsList(List<MessageThread> newThreads)
    {
        threadsList = newThreads;
    }

    public List<MessageThread> GetThreadsList() { return threadsList; }
}
