using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAudio : MonoBehaviour {

    [SerializeField] AudioSource playerSource;
    [SerializeField] AudioSource hitAltSource;

    [SerializeField] AudioClip prisonFootstep;
    [SerializeField] AudioClip mainFootstep;
    [SerializeField] AudioClip Jump;
    [SerializeField] AudioClip Land;
    [SerializeField] AudioClip Hit;
    [SerializeField] AudioClip Fire;
    [SerializeField] AudioClip Key;
    [SerializeField] AudioClip Item;
    [SerializeField] AudioClip Climb;
    [SerializeField] AudioClip Swing;

    [HideInInspector] public bool canPlayLand = true;

    public void PlayFootstep() {
        if(!GetComponent<PlayerHealth>().AtDeath()) {
            if (SceneManager.GetActiveScene().buildIndex >= 5) {
                playerSource.PlayOneShot(mainFootstep);
            } else {
                playerSource.PlayOneShot(prisonFootstep);
            }
        }
    }

    public void PlayJump() {
        playerSource.PlayOneShot(Jump);
    }

    public void PlayLand() {
        if(canPlayLand) {
            playerSource.PlayOneShot(Land);
        }
    }

    public void PlayHit() {
        hitAltSource.PlayOneShot(Hit);
    }

    public void PlayFire() {
        playerSource.PlayOneShot(Fire, 0.45f);
    }

    public void PlayKey() {
        playerSource.PlayOneShot(Key);
    }

    public void PlayItem() {
        playerSource.PlayOneShot(Item);
    }

    public void PlayClimb() {
        playerSource.PlayOneShot(Climb);
    }

    public void PlaySwing() {
        playerSource.PlayOneShot(Swing, 0.3f);
    }
}
