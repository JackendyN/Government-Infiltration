using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BossGuardTrigger : MonoBehaviour {

    Player player;
    public PlayableDirector tl;
    public BossGuard BG;
    BoxCollider2D BC;
    public BoxCollider2D Barrier;
    public GameObject HealthUI;
    public PrisonDust ps;
    Timer timer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        BC = GetComponent<BoxCollider2D>();
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
    }

    public void BossTimelineStart() {
       BC.enabled = false;
       player.SelfDisable();
       tl.Play();
       ps.particle.Stop();
       ps.enabled = false;
       timer.Pause(true);
    }

    public void BossStart() {
        player.enabled = true;
        BG.enabled = true;
        HealthUI.SetActive(true);
        timer.Pause(false);
        Destroy(gameObject);
        Barrier.enabled = false;
        ps.enabled = true;
    }
}
