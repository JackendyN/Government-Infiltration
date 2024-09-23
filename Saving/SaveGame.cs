using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour{

    [SerializeField] Player playerScript;
    [SerializeField] PlayerHealth playerHealthScript;
    Timer timer;

    void Start() {
        SaveData.current.savedScene = SceneManager.GetActiveScene().buildIndex;
        SaveData.current.savedHealth = playerHealthScript.PlayersMaxHealth();
        SaveData.current.savedHook = playerScript.Hook();
        SaveData.current.savedCups = playerScript.Cups();
        SaveData.current.savedUpgrade = playerScript.LauncherUpgrade();

        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        SaveData.current.savedMinutes = timer.GetMinutes();
        SaveData.current.savedSeconds = timer.GetSeconds();

        SerializationManager.Save("Data", SaveData.current);
    }

}
