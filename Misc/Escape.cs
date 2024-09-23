using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour {

    public bool startingFall;
    public bool finishingFall;
    public Transform bar;
    public Transform barPoint;
    Vector3 point;
    float timeUntilStop; 
    public float firstFallTime;
    public float secondFallTime;
    public GameObject TimerUI;
    public GameObject healthBar;
    public GameObject[] TipUI;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip Falling;
    [SerializeField] AudioClip Fell;
    bool fellDown;

    void Start() {
        point = new Vector3(barPoint.position.x, barPoint.position.y, barPoint.position.z);
    }

    void FixedUpdate() {
        if(startingFall){
            timeUntilStop += Time.deltaTime;
            if(timeUntilStop <= firstFallTime) {
               bar.RotateAround(point, new Vector3(0f, 0f, 1f), 22.5f * Time.deltaTime);
            } else {
                startingFall = false;
                timeUntilStop = 0;
            }
        } else if(finishingFall){
            timeUntilStop += Time.deltaTime;
            if(timeUntilStop <= secondFallTime) {
               bar.RotateAround(point, new Vector3(0f, 0f, 1f), 55f * Time.deltaTime);
            } else {
                finishingFall = false;
                timeUntilStop = 0;
                audioSource.Stop();

                if(!fellDown) {
                    audioSource.PlayOneShot(Fell);
                    fellDown = false;
                }
            }
        }
    }
     
    public void StartGame() {
        Player play = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        play.enabled = true;
        Vector3 scaler = play.gameObject.transform.localScale;
        scaler.x *= -1;
        play.gameObject.transform.localScale = scaler;
        TimerUI.SetActive(true);
        TipUI[0].SetActive(true);
        TipUI[1].SetActive(true);
        healthBar.SetActive(true);
    }

    public void StartFall() {
        startingFall = true;
        audioSource.PlayOneShot(Falling, 0.7f);
    }

    public void FinishFall() {
        finishingFall = true;
    }

}
