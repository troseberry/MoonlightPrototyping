using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageThread : MonoBehaviour
{
    public GameObject incomingMessagePrefab;
    public GameObject outgoingMessagePrefab;

    public GameObject messagesScrollContent;

    private Message[] messages;

    void Start()
    {
        messages = new [] 
        {
            new Message(MessageType.INCOMING, "incoming message 01", "n/a"),
            new Message(MessageType.INCOMING, "incoming message 02", "n/a"),
            new Message(MessageType.OUTGOING, "outgoing message 01", "n/a")
        };
    }

    
    void Update()
    {
        
    }

    public void CreateMessageUIObjects()
    {
        for (int d = 0; d < messagesScrollContent.transform.childCount; d++)
        {
            Destroy(messagesScrollContent.transform.GetChild(d).gameObject);
        }



        for (int i = 0; i < messages.Length; i++)
        {
            GameObject createdMessage;
            //instantiate message prefab
            if (messages[i].GetMessageType() == MessageType.INCOMING)
            {
                createdMessage = Instantiate(incomingMessagePrefab, messagesScrollContent.transform);
            }
            else
            {
                createdMessage = Instantiate(outgoingMessagePrefab, messagesScrollContent.transform);
            }

            //set message prefab obj's MessageUI component's assocMessage
            createdMessage.GetComponent<MessageUI>().SetAssociatedMessage(messages[i]);

            //update ui
            createdMessage.GetComponent<MessageUI>().UpdateMessageUI();
        }
    }
}
