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
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + folderPath + masterFilePath);

                SaveableData saveData = new SaveableData();

                //---------------------------Setting Save Data-------------------------------------
                saveData.savedContacts = new List<MessageContact>();
                saveData.savedThreads = new List<MessageThread>();
                //---------------------------Done Setting Data-------------------------------------

                formatter.Serialize(file, saveData);
                file.Close();
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e.Message);
            }
        }
    }
}
