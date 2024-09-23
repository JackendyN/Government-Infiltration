using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToCheckpoint : MonoBehaviour {

    SpriteRenderer playerRenderer;
    Animator playerAnimator;
    PlayerHealth playerHP;
    Rigidbody2D playerBody;
    BoxCollider2D playerBox;
    CupClimb playerClimb;
    
    [SerializeField] float backSpeed;
    bool movingBack;
    public Vector2 lastCheckpoint;
    
    void Start() {
        lastCheckpoint = new Vector2(1000, 1000); // Had problems letting it be null, checks if its 1000 instead.
        playerAnimator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerHP = GetComponent<PlayerHealth>();
        playerBody = GetComponent<Rigidbody2D>();
        playerBox = GetComponent<BoxCollider2D>();
        playerClimb = GetComponent<CupClimb>();
    }

    void Update() {
        if(movingBack) {
            transform.position = Vector2.MoveTowards(transform.position, lastCheckpoint, backSpeed * Time.deltaTime);
            UpdateVariables();

            if(Mathf.Round(transform.position.x) == Mathf.Round(lastCheckpoint.x) && Mathf.Round(transform.position.y) == Mathf.Round(lastCheckpoint.y)) {
                movingBack = false;
                UpdateVariables();
                playerClimb.enabled = true;
                GetComponent<Player>().enabled = true;

                if (playerClimb.onWall) {
                    playerClimb.EndClimb(0);
                }
            }
        }
    }

    public void MoveBack() {
        GetComponent<Player>().enabled = false;
        playerClimb.enabled = false;
        playerBody.isKinematic = true;
        playerBody.velocity = new Vector2(0, 0);
        movingBack = true;
    }

    void UpdateVariables() {
        if(movingBack) {
            playerRenderer.color = new Color(playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, 0.6f);
            playerAnimator.SetFloat("speed", 0);
            playerBox.enabled = false;
        } else {
            playerRenderer.color = new Color(playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, 1f);
            playerAnimator.SetFloat("speed", 1);
            playerBox.enabled = true;
            playerBody.isKinematic = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Checkpoint")) {
            lastCheckpoint = new Vector2(collision.gameObject.transform.position.x, collision.gameObject.transform.position.y);
        } else if(collision.gameObject.CompareTag("AntiCheckpoint")) {
            lastCheckpoint = new Vector2(1000, 1000);
        }
    }
}
