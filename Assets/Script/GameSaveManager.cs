using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public string levelString;
    public List<ScriptableObject> objects = new List<ScriptableObject>();
    public PlayerInventory playerInventory;
    public Date date;
    public string mapLevelName;
    public StringValue previousLevelName;

    private void Start()
    {
        if (date != null)
        {
            date.UpdateDate(0);
        }
    }

    private void OnEnable()
    {
        LoadScriptables();
        if (date != null)
        {
            date.UpdateDate(0);
        }
    }

    private void OnDisable()
    {
        SaveScriptables();
    }

    public void SaveScriptables()
    {
        for (int i =0; i < objects.Count; i++)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                string.Format("/{0}{1}.dat", levelString, i));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(objects[i]);
            binary.Serialize(file, json);
            file.Close();
        }

        if (playerInventory != null)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                FileStream file = File.Create(Application.persistentDataPath +
                    string.Format("/Inventory{0}.dat", i));
                BinaryFormatter binary = new BinaryFormatter();
                var json = JsonUtility.ToJson(playerInventory.myInventory[i]);
                binary.Serialize(file, json);
                file.Close();
            }
        }

        if (date != null)
        {
            FileStream file = File.Create(Application.persistentDataPath +
                    string.Format("/{0}.dat", "date"));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(date);
            binary.Serialize(file, json);
            file.Close();
        }

        if (mapLevelName != null && previousLevelName != null)
        {
            previousLevelName.initialValue = mapLevelName;
            FileStream file = File.Create(Application.persistentDataPath +
                    string.Format("/{0}.dat", "mapLevelName"));
            BinaryFormatter binary = new BinaryFormatter();
            var json = JsonUtility.ToJson(previousLevelName);
            binary.Serialize(file, json);
            file.Close();
        }

        
    }

    public void LoadScriptables()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath +
                string.Format("/{0}{1}.dat", levelString, i)))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                string.Format("/{0}{1}.dat", levelString, i), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), objects[i]);
                file.Close();
            }
        }

        
        if (playerInventory != null)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (File.Exists(Application.persistentDataPath +
                    string.Format("/Inventory{0}.dat", i)))
                {
                    FileStream file = File.Open(Application.persistentDataPath +
                    string.Format("/Inventory{0}.dat", i), FileMode.Open);
                    BinaryFormatter binary = new BinaryFormatter();
                    JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), playerInventory.myInventory[i]);
                    playerInventory.myInventory[i].itemImage = Resources.Load<Sprite>(playerInventory.myInventory[i].imagePath);
                    playerInventory.myInventory[i].reaction = Resources.Load<Reaction>(playerInventory.myInventory[i].reactionPath);
                    file.Close();
                    File.Delete(Application.persistentDataPath + string.Format("/Inventory{0}.dat", i));
                }
            }
        }

        if (date != null)
        {
            if (File.Exists(Application.persistentDataPath +
                    string.Format("/{0}.dat", "date")))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                string.Format("/{0}.dat", "date"), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), date);
                date.dateSignal = Resources.Load<MySignal>("DateSignal");
                file.Close();
            }
        }

        if (previousLevelName != null)
        {
            if (File.Exists(Application.persistentDataPath +
                    string.Format("/{0}.dat", "mapLevelName")))
            {
                FileStream file = File.Open(Application.persistentDataPath +
                string.Format("/{0}.dat", "mapLevelName"), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), previousLevelName);
                file.Close();
            }
        }

    }
}
