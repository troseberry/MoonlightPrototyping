using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageUI : MonoBehaviour
{
    public Text messageContent;
    private Message assocMessage;

    public void SetAssociatedMessage(Message message)
    {
        assocMessage = message;
    }

    public void UpdateMessageUI()
    {
        messageContent.text = assocMessage.GetMessageContent();
    }
}
