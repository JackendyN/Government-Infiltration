using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReEntry : MonoBehaviour {

    [SerializeField] int entryNumber;
    public UnityEvent OnEntry;
    Player boolScript;
    PlayerHealth healthScript;

    void Awake() {
        boolScript = GetComponent<Player>();
        healthScript = GetComponent<PlayerHealth>();

        if (entryNumber == 1 && boolScript.Hook() || entryNumber == 2 && boolScript.LauncherUpgrade() || entryNumber == 3 && boolScript.LauncherUpgrade() || entryNumber == 4 && healthScript.PlayersMaxHealth() == 6 || entryNumber == 5 && boolScript.Cups()) {
            OnEntry.Invoke();
        }
    }

}
