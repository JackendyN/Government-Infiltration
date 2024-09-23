using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {
    
    public GameObject Lasers;

    void ActivateLaser() {
        Lasers.SetActive(true);
    }
}
