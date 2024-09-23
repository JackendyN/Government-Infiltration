using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupClimb : MonoBehaviour {

    [SerializeField] Player main;
    Animator animator;
    float defaultScale;

    [HideInInspector] public bool onWall;
    [HideInInspector] public float wallCooldown = 1f;
    public float wallDistance;

    float moveInput;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask edgeLayers;
    [SerializeField] float checkDistance;
    [SerializeField] float extraCheckDistance;

    [SerializeField] Transform eyeLevel;
    [SerializeField] Transform edgeLevel;
    [SerializeField] LayerMask layersToCheck;
    [SerializeField] float firstForce, secondForce;

    [SerializeField] bool notAtEdge;

    void Start() {
        animator = GetComponent<Animator>();
        defaultScale = main.rb.gravityScale;
    }
    

    void FixedUpdate() {

        if(onWall) {
            main.rb.gravityScale = 0;
            animator.SetBool("isClimbing", true);

            moveInput = Input.GetAxisRaw("Vertical");
            main.rb.velocity = new Vector2(0, moveInput * main.speed * 0.75f);
            CheckForGround();
            CheckForEnd();
            UpdateAnimation();

        } else {
            wallCooldown -= Time.deltaTime;
        }

    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && onWall) {
            EndClimb(1);
        }
    }

    float ScalerResult() => (main.scaler.x / Mathf.Abs(main.scaler.x));
    public float WallOffset() => transform.position.x - (wallDistance * ScalerResult());

    void CheckForGround() {
        bool onGround = Physics2D.Raycast(transform.position, Vector2.down, checkDistance, groundLayer);
        notAtEdge = Physics2D.Raycast(edgeLevel.position, Vector2.right * ScalerResult(), checkDistance, edgeLayers);
        if (onGround || !notAtEdge) {
            EndClimb(0);
        }
    }

    void UpdateAnimation() {
        if (moveInput != 0) {
            animator.SetBool("climbPaused", false);
        } else {
            animator.SetBool("climbPaused", true);
        }
    }

    void CheckForEnd() {
        bool notAtEnd = Physics2D.Raycast(eyeLevel.position, Vector2.right * ScalerResult(), wallDistance + 0.2f, layersToCheck);
        if(!notAtEnd) {
            animator.SetTrigger("climbDone");
            main.rb.gravityScale = defaultScale;
            onWall = false;
        }
    }

    public void EndClimb(int jumpedOff) {
        animator.SetBool("climbPaused", false);
        animator.SetBool("isClimbing", false);
        main.rb.gravityScale = defaultScale;
        main.enabled = true;
        onWall = false;
        wallCooldown = 1f;

        if (jumpedOff == 1) {
            main.Jump();
            main.Flip();
            main.rb.AddForce(new Vector2(7f, 0));
        } 

    }

    void AddForces(int forceNumber) {
        if(forceNumber == 1) {
            main.rb.AddForce(new Vector2(0, firstForce));
        } else {
            main.rb.AddForce(new Vector2(secondForce * ScalerResult(), 0));
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, ScalerResult() * wallDistance * Vector2.right);
        Gizmos.DrawRay(edgeLevel.position, Vector2.right * ScalerResult() * checkDistance);
        Gizmos.DrawRay(transform.position, Vector2.down * checkDistance);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(onWall) {
            main.OnTriggerEnter2D(collision);
        }
    }

}
