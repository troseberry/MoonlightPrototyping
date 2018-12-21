using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MessageContact
{
    private string contactName, lastMessage, dateTime;
    private MessageThread associatedThread;

    

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

        associatedThread = new MessageThread(new Message(MessageType.INCOMING, last, "n/a"));
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

    public void SetAssociatedMessageThread(MessageThread newThread)
    {
        associatedThread = newThread;
    }

    public MessageThread GetAssociatedMessageThread() { return associatedThread; }
}
