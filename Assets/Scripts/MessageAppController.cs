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
    

    private List<MessageContact> contactsList = new List<MessageContact>();

    
    void Start()
    {
        Instance = this;

        contactsView = transform.Find("Contacts").gameObject;
        messagesView = transform.Find("Messages").gameObject;

        appName = transform.Find("AppHeader").Find("AppName").gameObject;
        currentContactNameText = transform.Find("AppHeader").Find("CurrentContactName").gameObject;

        InitContactsFromSave();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            MessageApp.SaveLoad.SaveData();
        }
    }

    public void OpenMessageThread()
    {
        if (currentContactObj) previousContactObj = currentContactObj;
        currentContactObj = EventSystem.current.currentSelectedGameObject;

        currentlySelectedContact = currentContactObj.GetComponent<MessageContactUI>().GetAssociatedContact();
        currentlySelectedMessageThread = currentlySelectedContact.GetAssociatedMessageThread();

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


        List<Message> messagesToPopulate = currentlySelectedMessageThread.GetThreadMessages();
        for (int i = 0; i < messagesToPopulate.Count; i++)
        {
            GameObject createdMessage;
            if (messagesToPopulate[i].GetMessageType() == MessageType.INCOMING)
            {
                createdMessage = Instantiate(incomingMessagePrefab, messagesScrollContent.transform);
            }
            else
            {
                createdMessage = Instantiate(outgoingMessagePrefab, messagesScrollContent.transform);
            }

            createdMessage.GetComponent<MessageUI>().SetAssociatedMessage(messagesToPopulate[i]);
            createdMessage.GetComponent<MessageUI>().UpdateMessageUI();
        }
    }


    public void BackButtonPressed()
    {
        if (messagesView.activeSelf)
        {
            currentlySelectedContact.SetAssociatedMessageThread(currentlySelectedMessageThread);

            contactsView.SetActive(true);
            messagesView.SetActive(false);

            appName.SetActive(true);
            currentContactNameText.SetActive(false);
        }
    }

    void InitContactsFromSave()
    {
        MessageApp.SaveLoad.LoadData();
        if (contactsList.Count > 0)
        {
            for (int i = 0; i < contactsList.Count; i++)
            {
                GameObject createdContact = Instantiate(contactPrefab, contactScrollContent.transform);
                createdContact.GetComponent<MessageContactUI>().SetAssociatedContact(contactsList[i]);
                createdContact.GetComponent<Button>().onClick.AddListener( () => OpenMessageThread());
            }
        }
    }


    // used when a new person "texts" the player
    public void CreateNewContact(string name, string last, string date)
    {
        GameObject createdContact = Instantiate(contactPrefab, contactScrollContent.transform);
        createdContact.GetComponent<MessageContactUI>().SetAssociatedContact
        (
            new MessageContact(name, last, date)
        );

        createdContact.GetComponent<Button>().onClick.AddListener( () => OpenMessageThread());

        contactsList.Add(createdContact.GetComponent<MessageContactUI>().GetAssociatedContact());
    }




    public void CreateNewMessage(MessageType type, string messageContent, string date)
    {
        Message createdMessage = new Message(type, messageContent, date); 
        GameObject createdMessageObj;

        if (type == MessageType.INCOMING)
        {
            createdMessageObj = Instantiate(incomingMessagePrefab, messagesScrollContent.transform);
        }
        else
        {
            createdMessageObj = Instantiate(outgoingMessagePrefab, messagesScrollContent.transform);
        }

        createdMessageObj.GetComponent<MessageUI>().SetAssociatedMessage(createdMessage);
        createdMessageObj.GetComponent<MessageUI>().UpdateMessageUI();

        currentlySelectedMessageThread.AddMessage(createdMessage);
    }





    public void SetContactsList(List<MessageContact> newContacts)
    {
        contactsList = newContacts;
    }

    public List<MessageContact> GetContactsList() { return contactsList; }
}
