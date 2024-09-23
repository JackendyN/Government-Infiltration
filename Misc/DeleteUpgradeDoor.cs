using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteUpgradeDoor : MonoBehaviour {

    [SerializeField] Player player;
    [SerializeField] GameObject upgradeDoor;
    void Start() {
        if(player.EnteredUpgrades() && upgradeDoor != null) {
            Destroy(upgradeDoor);
        }
    }

}
