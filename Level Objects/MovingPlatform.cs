using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    
    public Transform position1, position2;
    float yPos1, yPos2;
    int Direction = 1;
    bool Moving = true;
    float waitingTime;
    bool Waiting;
    [SerializeField] float speed;
    Rigidbody2D body;

    Rigidbody2D playerBody;
    bool playerIsOn;
    bool playerIsBelow;
    bool playerisOnTop;
    [SerializeField] Transform topCheck;
    [SerializeField] float topCheckDistance;
    [SerializeField] float playerCheckRadius;
    [SerializeField] Transform playerCheck;
    float playerTurnTimer = 3;

    void Start() {
        yPos1 = position1.position.y;
        yPos2 = position2.position.y;
        playerBody = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        playerisOnTop = Physics2D.Raycast(topCheck.position, Vector3.left, topCheckDistance, 1024);
        playerIsBelow = Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, 1024);
        if(playerIsBelow && playerTurnTimer <= 0 && Direction == -1) {
            Direction *= -1;
            playerTurnTimer = 3;
        } else {
            playerTurnTimer -= Time.deltaTime;
        }

        if(Moving) {
            // transform.Translate(1.8f * Direction * speed * Time.deltaTime * Vector3.up);
            body.velocity = new Vector2(body.velocity.x, 1.8f * Direction * speed);
            
            if(transform.position.y > yPos1 && Direction == 1 || transform.position.y < yPos2 && Direction == -1) {
                Moving = false;
                Waiting = true;
            }

        }

        if(Waiting) {
            waitingTime += Time.deltaTime;
            body.velocity = new Vector2(0, 0);

            if (waitingTime >= 1.5f) {
                Waiting = false;
                Direction *= -1;
                Moving = true;
                waitingTime = 0;
            }
        }

         if(playerIsOn) {
            playerBody.velocity = new Vector2(playerBody.velocity.x, (playerBody.velocity.y + body.velocity.y) / 2);
         }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player") && playerBody.GetComponent<Player>().isGrounded && playerisOnTop) {
            playerIsOn = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")) {
            playerIsOn = false;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerCheck.position, playerCheckRadius);
        Gizmos.DrawRay(topCheck.position, Vector3.left * topCheckDistance);
    }
}
