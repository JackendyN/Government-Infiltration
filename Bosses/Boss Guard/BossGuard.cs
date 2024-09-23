using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuard : MonoBehaviour {

    Animator ani;
    Rigidbody2D rig;
    public float speed;
    public float playerRadius;
    public Transform radiusStart;
    public LayerMask Player;
    bool isFacingPlayer;
    Vector3 scaler;
    public float bossGuardMove = -1;
    float bossStateChange;
    float timeUntilStateChange = 3;
    int attackState;
    int lastState;
    int stateCount;
    public Transform launchPos;
    public GameObject rock;
    float maximumStateTime = 12;
    float timeUntilNextRocks;
    float rockCooldownTwo;
    int numberOfRocks;
    float rockCooldown;
    public GuardSpawner spawner;

    public AudioSource bossAudio;
    [SerializeField] AudioClip March;
    [SerializeField] AudioClip Shoot;
    [SerializeField] AudioClip Charge;
    public AudioClip Hit;
    bool playedCharge;

    // Start is called before the first frame update
    void Start() {
         if (this.gameObject.scene.buildIndex != 0) {
            rig = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();
            ani.SetTrigger("Start");
            scaler.x *= -1;
            bossGuardMove *= -1;
         }
    }

    // Update is called once per frame
    void Update() {
        if(this.gameObject.scene.buildIndex != 0) {
            BossBehavior();
        }
    }

    void BossBehavior() {
        scaler = transform.localScale;
        isFacingPlayer = Physics2D.OverlapCircle(radiusStart.position, playerRadius, Player);

        if (isFacingPlayer == false) {
            scaler.x *= -1;
            bossGuardMove *= -1;
            transform.localScale = scaler;
        }

        bossStateChange += Time.deltaTime;
        if (bossStateChange >= timeUntilStateChange) {
            RollState();
        }

        switch (attackState) {
            case 1:
            rockCooldown -= Time.deltaTime;
            rig.velocity = new Vector2(bossGuardMove * speed, rig.velocity.y);

            if (rockCooldown <= 0) {
                SpawnRock();
                rockCooldown = 2.5f;
            }

            ani.SetBool("Moving", true);

            break;

            case 2:
            rockCooldownTwo -= Time.deltaTime;
            timeUntilNextRocks -= Time.deltaTime;
            ani.SetBool("Moving", false);

            if (timeUntilNextRocks <= 0) {
                if (rockCooldownTwo <= 0 && numberOfRocks < 3) {
                    SpawnRock();
                    numberOfRocks++;
                    rockCooldownTwo = 0.1f;

                }
                else if (numberOfRocks == 3) {
                    timeUntilNextRocks = 3;
                    playedCharge = false;
                    numberOfRocks = 0;
                }

            }
            else if (!playedCharge) {
                bossAudio.PlayOneShot(Charge);
                playedCharge = true;
            }

            break;
        }
    }

    void RollState() {
        lastState = attackState;
        attackState = Random.Range(1, 3);
        stateCount++;

        if(lastState == attackState && stateCount >= 2) {
            RollState();
            stateCount = 0;
        } else {
            bossStateChange = 0;
            timeUntilStateChange = Random.Range(7f, maximumStateTime);
        }
    }

    public void PlayWalk() {
        bossAudio.PlayOneShot(March);
    }

    void SpawnRock() {
        GameObject spawnedRock = Instantiate(rock, launchPos.position, Quaternion.identity);
        bossAudio.PlayOneShot(Shoot, 0.6f);
        spawnedRock.transform.localScale = new Vector3(spawnedRock.transform.localScale.x * bossGuardMove, spawnedRock.transform.localScale.y, spawnedRock.transform.localScale.z);
    }

    public void PhaseTwo() {
       speed += 1;
       maximumStateTime -= 2;
       ani.SetBool("Half", true);
       spawner.enabled = true;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(radiusStart.position, playerRadius);
    }
}

