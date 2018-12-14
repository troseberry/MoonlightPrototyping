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

    
    public Message[] GetThreadMessages() { return messages; }
}
