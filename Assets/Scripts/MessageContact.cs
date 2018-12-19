using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageContact : MonoBehaviour
{
    public string contactName, lastMessage, dateTime;
    //have int that is the associated message thread's id

    

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
