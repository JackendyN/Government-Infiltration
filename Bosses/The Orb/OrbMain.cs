using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbMain : MonoBehaviour {

    int AttackState;
    int lastAttack;
    Animator ani;
    [HideInInspector] public AudioSource orbSource;
    [HideInInspector] public OrbSounds orbSound;
    public Transform playerTransform;
    public int currentHealth;
    public int maxHealth;
    bool hasDoneTransition;
    public bool canCheck;
    public Rigidbody2D r;
    int maxAttack = 6;
    public GameObject Orb;
    SpriteRenderer orbSpriteRenderer;
    [SerializeField] StartandStop sas;
    [SerializeField] HealthBar hb;
    [SerializeField] ParticleSystem defeatParticles;
    [SerializeField] Transform[] lightPositions;
    [SerializeField] Transform lightTransform;

    public GameObject projectile;
    public Transform pos1, pos2;
    public GameObject lastStop;
    public Transform[] shockPositions;
    public GameObject Waves;

    public int EnemyRounds;
    public int maxEnemyRounds;
    public GameObject[] currentEnemies;
    public Transform[] handPositions;
    public Transform[] enemyPositions;
    public LineRenderer[] lines;

    public Transform laserPosition;
    public GameObject laser;

    public Transform launchPosition;
    public GameObject rock;

    Vector2 spikePosition;
    public Transform spikeHandPosition;
    public GameObject spike;
    GameObject currentSpike;
    public Transform spikeHand;

    public GameObject bolt;
    public int maxBolts;
    int strikeAmount;
    public Transform[] boltPositions;
    public float downSpeed;

    public Animator leaderAnimator;

    // Start is called before the first frame update
    void Start() {
        ani = GetComponent<Animator>();
        r = GetComponent<Rigidbody2D>();
        orbSource = GetComponent<AudioSource>();
        orbSound = GetComponent<OrbSounds>();
        orbSpriteRenderer = Orb.GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
        hb.SetMax(maxHealth);
        Roll();
    }

    void Update() {
        hb.SetValue(currentHealth);
    }

    public void Roll() {
        KinematicSwitch(true);
        EnableOrb();
        SetRotation();
        ChangeLightPosition(0);
        orbSource.Stop();

        if (currentHealth <= 15) {
            defeatParticles.Play();
        }

        if (currentHealth < (maxHealth / 2) && !hasDoneTransition) {
            ani.SetTrigger("Half");
            hasDoneTransition = true;
            maxAttack = 7;
            return;
        } else if(currentHealth <= 0) {
            Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            if(player.isGrounded) {
                sas.BossEnd();
            } else {
                DisableOrb();
                player.WaitingForLand();
            }

            ani.SetTrigger("Defeated");
            Orb.SetActive(true);
            AttackState = 0;
            ChangeLightPosition(4);
            return;
        }

        lastAttack = AttackState;
        strikeAmount = 0;
        canCheck = false;
        AttackState = Random.Range(1, maxAttack);

        if(lastAttack == AttackState || lastAttack == 2 && AttackState == 6 || lastAttack == 6 && AttackState == 2) {
            Roll();
        }

        ani.SetInteger("Attack", AttackState);
        ani.SetTrigger("NextAtk");
    }

    public void ChangeLightPosition(int lightPosition) {
        lightTransform.position = lightPositions[lightPosition].position;
    }

    public void KinematicSwitch(bool State) {
        r.isKinematic = State;
    }

    public void EnableOrb() {
        orbSpriteRenderer.enabled = true;
    }

    void DisableOrb() {
        orbSpriteRenderer.enabled = false;
    }

    void SpawnProjectiles() {
        Instantiate(projectile, pos1.position, pos1.rotation);
        Instantiate(projectile, pos2.position, pos2.rotation);
        orbSource.PlayOneShot(orbSound.orbProjectile);
    }

    void Shockwaves () {
        if(hasDoneTransition) {
            Instantiate(Waves, shockPositions[0].position, shockPositions[0].rotation);
            Instantiate(Waves, shockPositions[1].position, shockPositions[1].rotation);
        }

        orbSource.PlayOneShot(orbSound.orbLand);
    }

    void NextRound() {
        EnemyRounds++;

        if(EnemyRounds == maxEnemyRounds) {
            ani.SetTrigger("Next");
            EnemyRounds = 0;
        } else {
            ani.SetTrigger("NextRound");
        }

        lines[0].enabled = false;
        lines[1].enabled = false;
        Destroy(currentEnemies[0]);
        Destroy(currentEnemies[1]);
    }

    void SpawnRock() {
        Instantiate(rock, launchPosition.position, transform.rotation);
    }

    void SetRotation() {
        // probably shouldnt keep this, just a temp solution
        transform.eulerAngles = new Vector3 (0, 0, 0);
    }

    void SpawnSpike() {
        spikePosition = new Vector2(playerTransform.position.x, 5.28f);

        currentSpike = Instantiate(spike, spikePosition, transform.rotation);
        lines[0].enabled = true;
        spikeHandPosition = currentSpike.transform.GetChild(0);
        lines[0].SetPosition(0, spikeHand.position);
        lines[0].SetPosition(1, spikeHandPosition.position);
        orbSound.PlayForm();
    }

    void SummonLightning() {
        Instantiate(bolt, new Vector2(playerTransform.position.x, -9.144707f), transform.rotation);
    }

    void CheckSide() {
        strikeAmount++;
        if(strikeAmount >= maxBolts) {
            ani.SetTrigger("Next");
            return;
        }

        if(playerTransform.position.x < transform.position.x) {
            ani.SetInteger("Side", 1);
       } else {
            ani.SetInteger("Side", 2);
       }
    }

    void LeaderButton() {
        leaderAnimator.SetTrigger("HalfHealth");
    }

    void DisableLines() {
        lines[0].enabled = false;
        lines[1].enabled = false;
    }

    void OnCollisionEnter2D(Collision2D coll) {
        if(canCheck) {
            if(coll.gameObject.name == "Ground" && AttackState == 2) {
                Roll();
                KinematicSwitch(false);
            } else if(coll.gameObject.name == "Ground" && AttackState == 6) {
                Roll();
                KinematicSwitch(false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "DashStop2" && AttackState == 1 || col.gameObject.tag == "DashStop" && AttackState == 1) {
            if(col.gameObject != lastStop) {
                ani.SetTrigger("Next");
                lastStop = col.gameObject;
            }     
        }
    }

}
