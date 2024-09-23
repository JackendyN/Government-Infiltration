using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeCameraPosition : MonoBehaviour {
    
    public GameObject normalCam;
    public GameObject bossCam;

    public void Toggle() {
        normalCam.SetActive(false);
        bossCam.SetActive(true);
    }

    public void TogglePlayer() {
        normalCam.SetActive(true);
        bossCam.SetActive(false);
    }
}
