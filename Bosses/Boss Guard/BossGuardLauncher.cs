using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossGuardLauncher : MonoBehaviour {

    public int Health;
    public HealthBar healthBar;
    public BossGuard Boss;
    public PlayableDirector director;
    bool triggeredPhaseTwo;
    public GameObject[] Halves;
    public GameObject AntiMove;
    public ParticleSystem chip;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMax(100);
        healthBar.SetValue(Health);
    }

    // Update is called once per frame
    void Update() {
        rb.velocity = new Vector2(0, 0);

        if(Health <= 0) {
            Boss.enabled = false;
            chip.Stop();
            AntiMove.SetActive(false);
            healthBar.gameObject.SetActive(false);
            Halves[0].SetActive(true);
            Halves[1].SetActive(true);
            director.Play();
            Boss.bossAudio.Stop();
            Boss.gameObject.GetComponent<Animator>().SetBool("Moving", false);
            Destroy(gameObject);

        } else if(Health <= 50 && triggeredPhaseTwo == false){
            Boss.PhaseTwo();
            triggeredPhaseTwo = true;
        }
    }

    public void takeDamage() {
        Health -= 5;
        chip.Play();
        Boss.bossAudio.PlayOneShot(Boss.Hit, 0.65f);
        healthBar.SetValue(Health);
    }
}
