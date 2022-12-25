using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoader
{
    private static readonly string _filePath = Path.Combine(Application.persistentDataPath, "save.dat");
    public static GameData TryToLoadData()
    {
        return ReadSaveDataFromFile<GameData>(_filePath);
    }

    public static void SaveData(GameData data)
    {
        WriteSaveDataToFile<GameData>(_filePath, data);
    }

    private static T ReadSaveDataFromFile<T>(string filePath)
    {
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }

    private static void WriteSaveDataToFile<T>(string filePath, GameData saveDataToWrite, bool append = false)
    {
        using(Stream stream = File.Open(filePath,append ? FileMode.Append : FileMode.Create))
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, saveDataToWrite);
        }
    }
}
