using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MessageThread
{
    private List<Message> messages;

    //ideally this coordiates to the save file iD that saves the messages
    private int threadID;

    void Start()
    {
        messages = new List<Message>() 
        {
            new Message(MessageType.INCOMING, "incoming message 01", "n/a"),
            new Message(MessageType.INCOMING, "incoming message 02", "n/a"),
            new Message(MessageType.OUTGOING, "outgoing message 01", "n/a")
        };
    }

    public MessageThread(Message firstMessage, int ID)
    {
        messages = new List<Message> { firstMessage };
        threadID = ID;

        // call saveloadmessageapp method that returns the number of message thread/contacts saved
        // set threadID to be the count 
        // threadID = -1;
    }

    
    public List<Message> GetThreadMessages() { return messages; }

    public void SetThreadMessages(List<Message> newMessages)
    {
        messages = newMessages;
    }

    public int GetThreadID() { return threadID; }

    public void SetThreadID(int newID)
    {
        threadID = newID;
    }

    public void AddMessage(Message toAdd)
    {
        messages.Add(toAdd);
    }
}
