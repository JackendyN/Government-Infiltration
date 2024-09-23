using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSounds : MonoBehaviour {
    
    AudioSource aud;
    public AudioClip orbLand;
    [SerializeField] AudioClip form;
    [SerializeField] AudioClip launch;
    public AudioClip orbProjectile;
    [SerializeField] AudioClip enemies;
    [SerializeField] AudioClip dash;
    [SerializeField] AudioClip fall;
    [SerializeField] AudioClip glitch;

    void Start() {
        aud = GetComponent<AudioSource>();
    }

    void Update() {
        
    }

    public void PlayLaunch() {
        aud.PlayOneShot(launch);
    }

    public void PlayForm() {
        aud.PlayOneShot(form);
    }

    public void PlayDash() {
        aud.PlayOneShot(dash);
    }

    public void PlayEnemies() {
        aud.PlayOneShot(enemies);
        aud.loop = true;
    }

    public void PlayFall() {
        aud.PlayOneShot(fall);
    }

    public void PlayGlitch() {
        aud.PlayOneShot(glitch);
    }

    public void StopAudio() {
        aud.Stop();
    }

    public void StopLoop() {
        aud.loop = false;
    }
}
