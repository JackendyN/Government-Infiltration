using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    static int Health = 5;
    static int maxHealth = 5;
    Player p;
    static float timeUntilHeal;
    SpriteRenderer pr;
    SpriteRenderer br;
    Animator a;
    Rigidbody2D r;
    BoxCollider2D b;
    BackToCheckpoint c;
    public GameObject blackScreen;
    PlayableDirector timeline;
    float timeSinceHit;
    public GameObject TimerUI;
    public Animator hitAnimation;
    public HealthBar hb;
    int tempRegKeys;
    int tempMasterKeys;

    void Start() {
        p = GetComponent<Player>();
        pr = GetComponent<SpriteRenderer>();
        r = GetComponent<Rigidbody2D>();
        a = GetComponent<Animator>();
        b = GetComponent<BoxCollider2D>();
        c = GetComponent<BackToCheckpoint>();
        timeline = GetComponent<PlayableDirector>();
        br = blackScreen.GetComponent<SpriteRenderer>();
        TimerUI = GameObject.FindGameObjectWithTag("Timer");
        hb.SetMax(maxHealth);
        hb.SetValue(Health);
        tempRegKeys = p.GetKeyNumber(true);
        tempMasterKeys = p.GetKeyNumber(false);
    }

    // Update is called once per frame
    void Update() {
        timeSinceHit += Time.deltaTime;
        
        if(Health < maxHealth) {
            if(timeUntilHeal >= 13f) {
                Health += 1;
                timeUntilHeal = 0;
                hb.SetValue(Health);
            } else {
                timeUntilHeal += Time.deltaTime;
            }
        }
        

        if(Health <= 0) {
            p.enabled = false;
            c.enabled = false;
            pr.sortingOrder = 16;
            br.sortingOrder = 15;
            r.velocity = new Vector2(0, 0);
            r.isKinematic = true;
            b.enabled = false;
            TimerUI.SetActive(false);
            hb.gameObject.SetActive(false);
            timeline.Play();
            GetComponent<AudioSource>().Stop();
        }
    }

    public void TakeDamage(int Damage) {
        if(timeSinceHit >= 2) {
            Health -= Damage;
            a.SetTrigger("Hit");
            hitAnimation.SetTrigger("Hit");
            hb.SetValue(Health);
            timeSinceHit = 0;
            
            if(timeUntilHeal > 0) {
                timeUntilHeal -= 6;
            }
        }

        if(c.lastCheckpoint.x != 1000) {
           c.MoveBack();
        } 
        
    }

    public int PlayersMaxHealth() {
        return maxHealth;
    }

    public void ChangeMaxHealth(int newMax) {
        maxHealth = newMax;
        Health = newMax;
        hb.SetMax(maxHealth);
        hb.SetValue(Health);
    }

    public bool AtDeath() {
        return Health <= 0;
    }

    public void Reload() {
        Health = maxHealth;
        TimerUI.SetActive(true);
        hb.gameObject.SetActive(true);
        p.SetKeys(tempRegKeys, tempMasterKeys);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
