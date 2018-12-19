using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageThread : MonoBehaviour
{
    private Message[] messages;

    //ideally this coordiates to the save file iD that saves the messages
    private int threadID;

    void Start()
    {
        messages = new [] 
        {
            new Message(MessageType.INCOMING, "incoming message 01", "n/a"),
            new Message(MessageType.INCOMING, "incoming message 02", "n/a"),
            new Message(MessageType.OUTGOING, "outgoing message 01", "n/a")
        };
    }

    public MessageThread(Message firstMessage)
    {
        messages = new[] { firstMessage };
        // call saveloadmessageapp method that returns the number of message thread/contacts saved
        // set threadID to be the count 
        // threadID = -1;
    }

    
    public Message[] GetThreadMessages() { return messages; }

    public void SetThreadMessages(Message[] newMessages)
    {
        messages = newMessages;
    }

    public int GetThreadID() { return threadID; }

    public void SetThreadID(int newID)
    {
        threadID = newID;
    }
}
