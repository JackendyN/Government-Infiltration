using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGlass : MonoBehaviour {
    
    public int hitsTaken;
    public AudioSource machineGlassAudio;
    [SerializeField] SpriteRenderer spriteR;
    [SerializeField] Sprite[] sprites;
    [SerializeField] ChangeCameraPosition CCP;
    [SerializeField] GameObject machineFog;
    public static event Action OnDestroy;

    // Update is called once per frame
    void Update() {
        if(hitsTaken < 3) {
            spriteR.sprite = sprites[hitsTaken];
        } else if (hitsTaken >= 3) {
            CCP.TogglePlayer();
            Destroy(machineFog);
            Destroy(gameObject);
            OnDestroy?.Invoke();
        }
    }
}
