using UnityEngine;
using UnityEditor;

public class MessagesEditorTools : EditorWindow
{
    int toolbarSelection = 0;
    string[] toolbarTabs = { "Contacts", "Messages" };

    string contactName;
    string lastMessage;
    string dateTime;

    MessageType messageType;
    string messageContent;
    
    [MenuItem ("App Tools/Messages")]
    static void Init()
    {
        MessagesEditorTools window = (MessagesEditorTools) EditorWindow.GetWindow(typeof(MessagesEditorTools));
        window.Show();
    }

    void OnGUI()
    {
        toolbarSelection = GUILayout.Toolbar(toolbarSelection, toolbarTabs);

        switch(toolbarSelection)
        {
            case 0:
                GUILayout.Label("Add New Contact", EditorStyles.boldLabel);
                contactName = EditorGUILayout.TextField("Contact Name", contactName);
                lastMessage = EditorGUILayout.TextField("Last Message", lastMessage);
                dateTime = EditorGUILayout.TextField("Date/Time", dateTime);

                if (GUILayout.Button("Add New Contact"))
                {
                    Debug.Log("added new contact");
                }

                break;
            case 1:
                GUILayout.Label("Add New Message", EditorStyles.boldLabel);
                messageType = (MessageType)EditorGUILayout.EnumPopup("Message Type", messageType);
                messageContent = EditorGUILayout.TextField("Message Content", messageContent);

                if (GUILayout.Button("Add New Message"))
                {
                    Debug.Log("added new message");
                }

                break;
        }

        
    }
}
