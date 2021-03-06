﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessageAppController : AppController
{
    public static MessageAppController Instance;

    public GameObject contactsView;
    public GameObject messagesView;
    public GameObject addContactButton;

    public GameObject appName;
    public GameObject currentContactNameText;

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

    public RectTransform messagesScroll;
    public GameObject playerResponsesGroup;


    private string[] randomFirstNames = {" Michael", "Jennifer", "Christopher", "Amanda", "Jason", "Jessica", "David", "Melissa", "James", "Sarah", "Matthew", "Heather", "Joshua", "Nicole", "John", "Amy", "Robert", "Elizabeth", "Joseph", "Michelle"};

    private string[] randomLastNames = {"Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor"};
    
    void Start()
    {
        Instance = this;

        InitContactsFromSave();
    }

    public override void OpenApp()
    {
        contactsView.SetActive(true);
        messagesView.SetActive(false);

        appName.SetActive(true);
        currentContactNameText.SetActive(false);

        addContactButton.SetActive(true);

        playerResponsesGroup.SetActive(false);

        Invoke("ScrollContactsToTop", 0.1f);

        previousContactObj = null;
        currentContactObj = null;
        currentlySelectedContact = null;
        currentlySelectedMessageThread = null;
    }

    public override void CloseApp()
    {
        gameObject.SetActive(false);
    }

    public override void BackButtonPressed()
    {

        if (playerResponsesGroup.activeSelf)
        {
            TogglePlayerResponses();
        }
        else if (messagesView.activeSelf)
        {
            currentlySelectedContact.SetAssociatedMessageThread(currentlySelectedMessageThread);
            MessageApp.SaveLoad.SaveData();

            contactsView.SetActive(true);
            messagesView.SetActive(false);

            appName.SetActive(true);
            currentContactNameText.SetActive(false);

            addContactButton.SetActive(true);
        }
        else
        {
            CloseApp();
        }
    }

    

    

    public void TogglePlayerResponses()
    {
        if (!playerResponsesGroup.activeSelf)
        {
            playerResponsesGroup.SetActive(true);
            messagesScroll.offsetMin = new Vector2(messagesScroll.offsetMin.x, 770f);
        }
        else
        {
            playerResponsesGroup.SetActive(false);
            messagesScroll.offsetMin = new Vector2(messagesScroll.offsetMin.x, 275f);
        }
        ScrollThreadToBottom();
    }

    public void ClearContacts()
    {
        contactsList.Clear();
        for (int i = 0; i < contactScrollContent.transform.childCount; i++)
        {
            Destroy(contactScrollContent.transform.GetChild(i).gameObject);
        }
    }


    #region CONTACT METHODS
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

        MessageApp.SaveLoad.SaveData();
    }

    public void SetContactsList(List<MessageContact> newContacts)
    {
        contactsList = newContacts;
    }

    public List<MessageContact> GetContactsList() { return contactsList; }

    public void GenerateRandomContact()
    {
        int randomFirst = Random.Range(0, randomFirstNames.Length);
        int randomLast = Random.Range(0, randomLastNames.Length);

        CreateNewContact(
            randomFirstNames[randomFirst] + " " + randomLastNames[randomLast],
            "received message from " + randomFirstNames[randomFirst] + " " + randomLastNames[randomLast],
            "n/a"
        );
    }
    
    void ScrollContactsToTop()
    {
        contactsView.GetComponentInChildren<ScrollRect>().normalizedPosition = new Vector2(0, 1);
    }
    #endregion

    #region MESSAGE METHODS
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

            ScrollThreadToBottom();

            contactsView.SetActive(false);

            appName.SetActive(false);
            currentContactNameText.SetActive(true);
            currentContactNameText.GetComponent<Text>().text = currentlySelectedContact.GetContactName();

            addContactButton.SetActive(false);
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
            GameObject prefabToUse = messagesToPopulate[i].GetMessageType() == MessageType.INCOMING ? incomingMessagePrefab : outgoingMessagePrefab;

            GameObject createdMessage = Instantiate(prefabToUse, messagesScrollContent.transform);

            createdMessage.GetComponent<MessageUI>().SetAssociatedMessage(messagesToPopulate[i]);
            createdMessage.GetComponent<MessageUI>().UpdateMessageUI();
        }
    }

    public void CreateNewMessage(MessageType type, string messageContent, string date)
    {
        Message createdMessage = new Message(type, messageContent, date);

        GameObject prefabToUse = (type == MessageType.INCOMING) ? incomingMessagePrefab : outgoingMessagePrefab;
        GameObject createdMessageObj = Instantiate(prefabToUse, messagesScrollContent.transform); 

        createdMessageObj.GetComponent<MessageUI>().SetAssociatedMessage(createdMessage);
        createdMessageObj.GetComponent<MessageUI>().UpdateMessageUI();

        currentlySelectedMessageThread.AddMessage(createdMessage);

        Invoke("ScrollThreadToBottom", 0.1f);
    }

    public void ScrollThreadToBottom()
    {
        messagesView.GetComponentInChildren<ScrollRect>().normalizedPosition = new Vector2(0, 0);
    }
    #endregion
}
