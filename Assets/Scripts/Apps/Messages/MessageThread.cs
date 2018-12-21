using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MessageThread
{
    private List<Message> messages;


    public MessageThread(Message firstMessage)
    {
        messages = new List<Message> { firstMessage };
    }

    
    public List<Message> GetThreadMessages() { return messages; }

    public void SetThreadMessages(List<Message> newMessages)
    {
        messages = newMessages;
    }

    public void AddMessage(Message toAdd)
    {
        messages.Add(toAdd);
    }
}
