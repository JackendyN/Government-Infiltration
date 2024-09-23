using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    bool canUpgrade;
    bool hasUpgraded;
    LauncherMachine machine;
    [SerializeField] PlayerHealth playerHealth;

    void Update() {
        if(canUpgrade && Input.GetKeyDown(KeyCode.W)) {
            machine.StartAnimation();
            hasUpgraded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.name == "Orb Camera" || collision.gameObject.name == "Main Camera") {
            collision.GetComponent<UpgradeRoomCameras>().ChangeCamera();

        } else if(collision.gameObject.name == "Launcher Machine" && !hasUpgraded) {
            machine = collision.GetComponent<LauncherMachine>();
            canUpgrade = true;

        } else if(collision.gameObject.name == "Pipe Machine" && playerHealth.PlayersMaxHealth() != 6) {
            collision.GetComponent<PipeMachine>().StartMachine();
        }
    }

    void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.name == "Launcher Machine") {
            canUpgrade = false;
        }
    }

}
