using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField] static float Seconds;
    [SerializeField] static int Minutes;
    [SerializeField] Text timerText;
    static bool Paused;

    // Start is called before the first frame update
    void Awake () {
        DontDestroyOnLoad(this.gameObject.transform.parent);
    }

    void FixedUpdate() {
        if(!Paused) {
            Seconds += Time.deltaTime;
        
            if(Seconds >= 60) {
                Seconds = 0;
                Minutes++;
            }
        }

        if (Seconds < 10 && Minutes >= 10) {
            timerText.text = Minutes + ":0" + (int)Seconds;
        }
        else if (Seconds >= 10 && Minutes >= 10) {
            timerText.text = Minutes + ":" + (int)Seconds;
        }
        else if (Seconds < 10 && Minutes < 10) {
            timerText.text = "0" + Minutes + ":0" + (int)Seconds;
        }
        else if (Seconds >= 10 && Minutes < 10) {
            timerText.text = "0" + Minutes + ":" + (int)Seconds;
        }

    }

    public void Pause(bool Pausing) {
        if(Pausing) {
            Paused = true;
        } else {
            Paused = false;
        }
    }

    public int GetMinutes() {
        return Minutes;
    }

    public int GetSeconds() {
        return (int)Seconds;
    }

    public void ResetTime() {
        Seconds = 0;
        Minutes = 0;
    }

    public void SetTime(int setMinutes, int setSeconds) {
        Seconds = setSeconds;
        Minutes = setMinutes;
    }
}
