using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbFigure : MonoBehaviour {
    public float sightDis;
    public float speed = 6;
    float attackCooldown;
    Rigidbody2D figureRB;
    Animator ani;
    public Transform stopTrigger;
    public Transform playerTransform;
    public LayerMask playerLayer;
    RaycastHit2D leftPlayerSight;
    RaycastHit2D rightPlayerSight;
    bool seePlayer;
    Player player;
    public OrbMachine thisMachine;
    public int machineElement;
    public Transform linePos1, linePos2;
    float gracePeriod;
    float maxDashTime = 1f;
    float dashTime;

    AudioSource sound;

    // Start is called before the first frame update
    void Start() {
        figureRB = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gracePeriod = 1;
    }

    // Update is called once per frame
    void Update() {

        gracePeriod -= Time.deltaTime;
        if(gracePeriod <= 0) {

            leftPlayerSight = Physics2D.Raycast(transform.position, Vector3.left, sightDis, playerLayer);
            rightPlayerSight = Physics2D.Raycast(transform.position, Vector3.right, sightDis, playerLayer);
            attackCooldown -= Time.deltaTime;
            Vector3 scaler = transform.localScale;

            if(leftPlayerSight.collider != null && attackCooldown <= 0 && player.isGrounded) {
                Attack(-1);
                attackCooldown = 5;
                scaler.x = 5;
                transform.localScale = scaler;

            } else if(rightPlayerSight.collider != null && attackCooldown <= 0 && player.isGrounded) {
                Attack(1);
                attackCooldown = 5;
                scaler.x = -5;
                transform.localScale = scaler;
            }

            if(figureRB.velocity.x != 0 && dashTime <= 0) {
                figureRB.velocity = new Vector2(0, 0);
                ani.SetBool("Dashing", false);
            } else if(figureRB.velocity.x != 0 && dashTime > 0) {
                dashTime -= Time.deltaTime;
            }

            if(thisMachine != null) {
                if(thisMachine.machineOff) {
                    Destroy(gameObject);
                }
            }

        }

    }

    void Attack(int attackDirection) {
        stopTrigger.position = new Vector3(playerTransform.position.x, playerTransform.position.y, playerTransform.position.z);
        figureRB.velocity = new Vector2(speed * attackDirection, figureRB.velocity.y);
        sound.Play();
        ani.SetBool("Dashing", true);
        dashTime = maxDashTime;

        if(machineElement != 10 && thisMachine != null) {
            thisMachine.positions[machineElement] = linePos2;
        } else if(thisMachine != null) {
            thisMachine.originalPosition = linePos2;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if(trigger.gameObject.tag == "StopTrigger") {
            figureRB.velocity = new Vector2(0, 0);
            ani.SetBool("Dashing", false);
            dashTime = 0;
        }

        if(machineElement != 10 && thisMachine != null) {
            thisMachine.positions[machineElement] = linePos1;
        } else if(thisMachine != null) {
            thisMachine.originalPosition = linePos1;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.right * sightDis);
        Gizmos.DrawRay(transform.position, Vector3.left * sightDis);
    }

}
