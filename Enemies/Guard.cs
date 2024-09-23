using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour {

    Rigidbody2D rigid;
    Animator animator;
    int idleState = 2;
    float timeUntilStateChange = 3;
    float stateChangeCounter;
    public float guardMove = -1;
    public Transform launchPoint;
    public GameObject rock;
    float rockCooldown;
    public float maxCooldown = 2;
    bool Attacking;
    public LayerMask Player;
    public float sightDistance;
    RaycastHit2D playerSight;
    RaycastHit2D colSight;
    public float colDistance;

    AudioSource audioSource;
    [SerializeField] AudioClip prisonWalk;
    [SerializeField] AudioClip mainWalk;
    [SerializeField] AudioClip Launch;
    public AudioClip guardHit;
    public AudioClip guardFall;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        if (this.gameObject.scene.buildIndex != 0) {
            rigid = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update() {
        if (this.gameObject.scene.buildIndex != 0) {
            GuardBehavior();
        }
    }

    void GuardBehavior() {
        rockCooldown -= Time.deltaTime;
        stateChangeCounter += Time.deltaTime;
        if (stateChangeCounter >= timeUntilStateChange) {
            animator.SetBool("Shooting", false);
            idleState = Random.Range(1, 3);
            if (idleState == 1) {
                TurnAround();
            }
            stateChangeCounter = 0;
            timeUntilStateChange = Random.Range(1f, 2.5f);
        }
        if (idleState == 1 && Attacking == false) {
            rigid.velocity = new Vector2(guardMove * 3.5f, rigid.velocity.y);
            animator.SetBool("isMarching", true);
        }
        else if (idleState == 2 && Attacking == false) {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("isMarching", false);
        }

        if (guardMove == -1) {
            playerSight = Physics2D.Raycast(launchPoint.position, Vector3.left, sightDistance, Player);
        }
        else if (guardMove == 1) {
            playerSight = Physics2D.Raycast(launchPoint.position, Vector3.right, sightDistance, Player);
        }
        if (playerSight.collider != null && rockCooldown <= 0) {
            Attacking = true;
            idleState = 0;
            stateChangeCounter = 0;
            rigid.velocity = new Vector2(0, rigid.velocity.y);
            animator.SetBool("Shooting", true);
            animator.SetBool("isMarching", false);
            GameObject spawnedRock = Instantiate(rock, launchPoint.position, Quaternion.identity);
            spawnedRock.transform.localScale = new Vector3(spawnedRock.transform.localScale.x * guardMove, spawnedRock.transform.localScale.y, spawnedRock.transform.localScale.z);
            audioSource.PlayOneShot(Launch, 0.4f);
            rockCooldown = maxCooldown;
        }
        else {
            Attacking = false;
        }
    }

    void TurnAround() {
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        guardMove *= -1;
        transform.localScale = scaler;
    }

    public void WalkAudio() {
        if(SceneManager.GetActiveScene().buildIndex <= 6) {
            audioSource.PlayOneShot(prisonWalk);
        } else {
            audioSource.PlayOneShot(mainWalk);
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Gate" || col.gameObject.tag == "Guard") {
            TurnAround();
        }
    }

    void OnTriggerEnter2D(Collider2D collis) {
        if(collis.gameObject.tag == "GuardTurn") {
            TurnAround();
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        if(guardMove == -1) {
            Gizmos.DrawRay(launchPoint.position, Vector3.left * sightDistance);
            Gizmos.DrawRay(launchPoint.position, Vector3.left * colDistance);
        } else if(guardMove == 1) {
            Gizmos.DrawRay(launchPoint.position, Vector3.right * sightDistance);
            Gizmos.DrawRay(launchPoint.position, Vector3.right * colDistance);
        }
    }

}
