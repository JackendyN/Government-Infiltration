using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDisplay : MonoBehaviour {

    [SerializeField] Text display;
    [SerializeField] Player player;
    [SerializeField] bool regularDisplay;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] RectTransform parentTransform;
    
    void Update() {
        int Keys;
        Keys = player.GetKeyNumber(regularDisplay);

        if(Keys != 0) {
            display.text = Keys.ToString();
            rectTransform.localScale = new Vector3(1, 1, 1);
            parentTransform.localScale = new Vector3(1, 1, 1);
        } else {
            rectTransform.localScale = new Vector3(0, 0, 0);
            parentTransform.localScale = new Vector3(0, 0, 0);
        }
    }
}
