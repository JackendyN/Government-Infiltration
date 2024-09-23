using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    
    public Slider bar;

    public void SetMax(int Max) {
        bar.maxValue = Max;
    }

    public void SetValue(int Health) {
        bar.value = Health;
    }

}
