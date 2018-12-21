using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagesDebug : MonoBehaviour
{
    private int messageDebugCount = 0;
    public Canvas messageDebugMenu;
    
    public void OpenMessageDebug()
    {
        // Debug.Log("[Message Debug Count: " + messageDebugCount + " ]");
        if (messageDebugCount < 4) messageDebugCount ++;
        else
        {
            messageDebugMenu.enabled = true;
        }
    }

    public void CloseMessageDebug()
    {
        messageDebugMenu.enabled = false;
        messageDebugCount = 0;
    }

    public void DeleteMessageSave()
    {
        MessageApp.SaveLoad.DeleteData();
        MessageAppController.Instance.ClearContacts();
    }
}
