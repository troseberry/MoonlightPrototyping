using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MessageType { INCOMING, OUTGOING }

public class Message
{
    private MessageType messageType;
    private string messageContent;
    private string dateTime;


    public Message()
    {

    }

    public Message(MessageType type, string content, string date)
    {
        messageType = type;
        messageContent = content;
        dateTime = date;
    }

    public void SetMessageType(MessageType type)
    {
        messageType = type;
    }

    public MessageType GetMessageType() { return messageType; }

    public void SetMessageContent(string content)
    {
        messageContent = content;
    }

    public string GetMessageContent() { return messageContent; }

    public void SetDateTime(string date)
    {
        dateTime = date;
    }

    public string GetDateTime() { return dateTime; }
}
