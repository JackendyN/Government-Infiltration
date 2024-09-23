using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager : MonoBehaviour {
    
    // Save system taken from Game Dev Guide

    public static bool Save(string saveName, object saveData) {
        string path = Application.persistentDataPath + saveName + ".save";
        BinaryFormatter formatter = GetBinaryFormatter();

        if (File.Exists(path)) { 
            File.Delete(path);
        }

        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);
        file.Close();
        return true;
    }

    public static object Load(string path) {
        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        } catch {
            Debug.LogErrorFormat($"Failed to load file at {path}");
            file.Close();
            return null;
        }

    }

    public static BinaryFormatter GetBinaryFormatter() {
        BinaryFormatter formatter = new BinaryFormatter();

        return formatter;
    }
}
