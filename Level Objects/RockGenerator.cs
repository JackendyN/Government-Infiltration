using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour {

    [SerializeField] GameObject rock;
    [SerializeField] Transform spawnPosition;
    [SerializeField] float startDelay;
    [SerializeField] float cooldown;
    [SerializeField] AudioSource audioSource;
    float cooldownTimer;
    float startTime;
    

    // Update is called once per frame
    void Update() {
        startTime += Time.deltaTime;
        if(startTime >= startDelay) {

            if(cooldownTimer <= 0) {
                Instantiate(rock, spawnPosition.position, transform.rotation);
                audioSource.Play();
                cooldownTimer = cooldown;
            } else {
                cooldownTimer -= Time.deltaTime;
            }

        }
    }
}
