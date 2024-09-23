using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    // Also used to skip the intro 

    [SerializeField] bool inOpening;
    static bool gamePaused;
    [SerializeField] Player player;
    bool disablingPlayer;

    void Awake() {
        if(!inOpening) {
            DontDestroyOnLoad(transform.parent.gameObject);
        }
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Return)) {
            if(inOpening) {
                SceneManager.LoadScene(1);
            } else {
                gamePaused = !gamePaused;
                if(player == null) {
                    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                }

                if(gamePaused && player.enabled) {
                    disablingPlayer = true;
                }
            }
        }

        if(gamePaused) {
            Time.timeScale = 0;
            if(disablingPlayer) {
                player.enabled = false;
            }
        } else {
            Time.timeScale = 1;
            if(disablingPlayer) {
                player.enabled = true;
                disablingPlayer = false;
            }
        }

    }
}
