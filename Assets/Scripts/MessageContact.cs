using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageContact : MonoBehaviour
{
    // [SerializeField]
    public string contactName, lastMessage, dateTime;
    // private MessageContact thisContact;

    void Start()
    {
        // thisContact = new MessageContact();
    }

    public MessageContact()
    {
        contactName = "";
        lastMessage = "";
        dateTime = "";
    }

    public MessageContact(string name, string last, string date)
    {
        contactName = name;
        lastMessage = last;
        dateTime = date;
    }

    public void InitContact (string name, string last, string date)
    {
        contactName = name;
        lastMessage = last;
        dateTime = date;
    }

    public void SetContactName(string name)
    {
        contactName = name;
    }

    public string GetContactName() { return contactName; }

    public void SetLastMessage(string last)
    {
        lastMessage = last;
    }

    public string GetLastMessage() { return lastMessage; }

    public void SetDateTime(string date)
    {
        dateTime = date;
    }

    public string GetDateTime() { return dateTime; }
}
