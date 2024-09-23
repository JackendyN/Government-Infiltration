using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalTime : MonoBehaviour {

    [SerializeField] Text timeDisplay;

    void Start() {
        Timer timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        int totalMinutes = timer.GetMinutes();
        int totalSeconds = timer.GetSeconds();
        timer.gameObject.SetActive(false);

        if(totalSeconds < 10 && totalMinutes < 10) {
            timeDisplay.text = "0" + totalMinutes + ":0" + totalSeconds + "!";
        } else if(totalSeconds > 10 && totalMinutes < 10) {
            timeDisplay.text = "0" + totalMinutes + ":" + totalSeconds + "!";
        } else if(totalSeconds < 10 && totalMinutes > 10) {
            timeDisplay.text = totalMinutes + ":0" + totalSeconds + "!";
        } else if(totalSeconds > 10 && totalMinutes > 10) {
            timeDisplay.text = totalMinutes + ":" + totalSeconds + "!";
        }
    }

}
