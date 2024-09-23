using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData {
    static SaveData _current;
    public static SaveData current {
        get {
            if(_current == null) {
                _current = new SaveData();
            }

            return _current;
        }
    }

    public int savedScene;
    public int savedHealth;
    public bool savedHook;
    public bool savedCups;
    public bool savedUpgrade;
    public int savedMinutes;
    public int savedSeconds;
}
