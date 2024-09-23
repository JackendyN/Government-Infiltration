using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadGame : MonoBehaviour {
    
    [SerializeField] Player playerScript;
    [SerializeField] PlayerHealth playerHealthScript;
    [SerializeField] Timer timer;
    [SerializeField] GameObject pause;

    void Awake() {
        string filePath = Application.persistentDataPath + "Data.save";
        if (File.Exists(filePath)) {
            SaveData data = (SaveData)SerializationManager.Load(filePath);
            playerHealthScript.ChangeMaxHealth(data.savedHealth);
            timer.transform.parent.gameObject.SetActive(true);
            timer.SetTime(data.savedMinutes, data.savedSeconds);
            pause.SetActive(true);
            playerScript.LoadPlayerData(data.savedScene, data.savedCups, data.savedHook, data.savedUpgrade);
        }
    }

}
