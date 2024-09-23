using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGenerator : MonoBehaviour {

    [SerializeField] GameObject laser;
    [SerializeField] float laserStartDelay;
    [SerializeField] float laserCooldown;
    [SerializeField] float activateDelay;
    [SerializeField] AudioSource audioSource;
    float laserActiveTime;
    float laserCooldownTimer;
    float laserStartTime;
    bool laserActive;

    // Update is called once per frame
    void Update() {
        laserStartTime += Time.deltaTime;
        if(laserStartTime >= laserStartDelay) {

            if(laserCooldownTimer <= 0) {
                laserActive = !laserActive;
                laserCooldownTimer = laserCooldown;
            } else {
                laserCooldownTimer -= Time.deltaTime;
            }

            if(laserActive) {
                laserActiveTime += Time.deltaTime;
                if(laserActiveTime >= activateDelay) {
                    laser.SetActive(true);
                    audioSource.UnPause();
                    laserActiveTime = 0;
                }
            } else {
                laser.SetActive(false);
                audioSource.Pause();
            }

        }
    }

}
