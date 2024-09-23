using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeRoomCameras : MonoBehaviour {

    [SerializeField] GameObject otherCamera;
    
    public void ChangeCamera() {
        otherCamera.SetActive(true);
        this.gameObject.SetActive(false);
    }

}
