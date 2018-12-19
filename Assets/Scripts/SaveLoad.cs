using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MessageApp {

    public class SaveLoad
    {
        private static string folderPath = Path.DirectorySeparatorChar + "MessageApp";
        private static string masterFilePath = Path.DirectorySeparatorChar + "messageAppSave.dat";
        
        public static void SaveData()
        {
            if (!Directory.Exists(Application.persistentDataPath + folderPath))
            {
                Directory.CreateDirectory(Application.persistentDataPath + folderPath);
            }

            try
            {
                Debug.Log("Saving...");
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + folderPath + masterFilePath);

                SaveableData saveData = new SaveableData();

                //---------------------------Setting Save Data-------------------------------------
                saveData.savedContacts = MessageAppController.Instance.GetContactsList();
                //---------------------------Done Setting Data-------------------------------------
                
                Debug.Log("Saved Here: " + Application.persistentDataPath + folderPath + masterFilePath);

                formatter.Serialize(file, saveData);
                file.Close();
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e.Message);
            }
        }


        public static void LoadData()
        {
            if (File.Exists(Application.persistentDataPath + folderPath + masterFilePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + folderPath + masterFilePath, FileMode.Open);
                SaveableData loadData = (SaveableData)formatter.Deserialize(file);
                file.Close();

                MessageAppController.Instance.SetContactsList(loadData.savedContacts);
            }
            else
            {
                SaveData();
            }
        }

        public static void DeleteData ()
        {
            // File.Delete(Application.persistentDataPath + folderPath + selectedSaveSlot + saveSlotStrings[selectedSaveSlot - 1]);

            File.Delete(Application.persistentDataPath + folderPath + masterFilePath);
            Debug.Log("Deleted Save");
        }
    }
}
