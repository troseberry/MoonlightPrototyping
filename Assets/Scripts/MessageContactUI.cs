using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageContactUI : MonoBehaviour
{
    public Text contactName, lastMessage, dateTime;
    private MessageContact assocContact;


    public void SetAssociatedContact(MessageContact contact)
    {
        assocContact = contact;

        contactName.text = assocContact.GetContactName();
        lastMessage.text = assocContact.GetLastMessage();
        dateTime.text = assocContact.GetDateTime();
    }

    public MessageContact GetAssociatedContact() { return assocContact; }
}
