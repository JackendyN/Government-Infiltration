using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherMachine : MonoBehaviour {

    Player playerScript;
    Animator animationScript;
    [SerializeField] GameObject spawnedTip;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    void Start() {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animationScript = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    public void StartAnimation() {
        animationScript.SetTrigger("Start");
        audioSource.Play();
        playerScript.SelfDisable();
    }

    public void EndAnimation() {
        playerScript.enabled = true;
        spawnedTip.SetActive(true);
        boxCollider.enabled = false;
        playerScript.UpgradeLauncher();
    }

    public void EndAudio() {
        audioSource.Stop();
    }

}
