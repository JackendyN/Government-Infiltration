using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {
    [SerializeField] Player player;
    Timer timer;
    GameObject pause;

    void Start() {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        pause = GameObject.FindGameObjectWithTag("Pause");
        pause.SetActive(false);
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            player.WipeData();
            player.SetKeys(0, 0);
            timer.ResetTime();
            timer.Pause(false);
            Destroy(timer.gameObject.transform.parent.gameObject);
            File.Delete(Application.persistentDataPath + "Data.save");
            SceneManager.LoadScene(0);
        }
    }
}
