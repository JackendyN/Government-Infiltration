using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBeing : MonoBehaviour {

    public LineRenderer machineLine;
    public Transform playerPosition;
    public Transform defaultPosition;
    public int currentAttack;
    int lastAttack;
    public int attackStep;
    int attacksDone;
    public Animator animator;
    int maxDashes = 3;
    int dashesDone;
    float dashDirection = 1;
    float waitingTime;
    public Transform bounceHeight;
    Vector2 bounceTarget;
    int maxBounces = 6;
    int bouncesDone;
    public Transform ceilingPosition;
    Vector3 defaultUp;
    public Transform[] projectilePositions;
    public GameObject projectile;
    int timesShot;
    int maxShots = 7;
    Vector3 defaultScaler;
    Vector3 scaler;
    public GameObject chargedEnergy;
    public Transform energyPosition;
    GameObject chargingEnergy;
    ChargedEnergy energyScript;
    public float playerRadius;
    public Transform radiusStart;
    public LayerMask Player;
    bool isFacingPlayer;
    public Transform Target1, Target2;
    Transform currentTarget;
    int maxPossibleAttack;
    [SerializeField] BeingManager manager;
    [SerializeField] GameObject dashStopPosition;
    bool glassIsDestroyed = false;
    bool dashSounded;
    bool ballSounded;

    AudioSource source;
    [SerializeField] AudioClip Form;
    [SerializeField] AudioClip Dash;
    [SerializeField] AudioClip Projectiles;
    [SerializeField] AudioClip Ball;
    [SerializeField] AudioClip Bounce;
    [SerializeField] AudioClip Charge;

    void Start() {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        defaultUp = transform.up;
        scaler = transform.localScale;
        defaultScaler = scaler;
        MachineGlass.OnDestroy += ChangeDashPosition;
    }

    // Update is called once per frame
    void Update() {
        if(currentAttack == 1 && dashesDone < maxDashes) {
            switch(attackStep) {
                case 1:
                transform.localScale = scaler;
                transform.Translate(Vector2.left * dashDirection * 13f * Time.deltaTime, Space.World);
                animator.SetBool("Dashing", true);
                animator.SetBool("OnWall", false);

                if(!dashSounded) {
                    source.PlayOneShot(Dash);
                    dashSounded = true;
                }

                break;
                case 2:
                animator.SetBool("OnWall", true);
                waitingTime += Time.deltaTime;
                if(waitingTime >= 0.75f) {
                    dashesDone++;
                    dashSounded = false;
                    waitingTime = 0;
                    attackStep = 1;
                }
                break;
            }

            if(dashesDone == maxDashes) {
                animator.SetBool("OnWall", false);
                animator.SetBool("Dashing", false);
                Reroll(4);
                source.Stop();
                dashesDone = 0;
            }

        } else if(currentAttack == 2) {
            switch(attackStep) {
                case 1:
                animator.SetBool("Rolling", true);
                if(transform.position.y < bounceHeight.position.y) {
                    transform.Translate(Vector2.up * 10f * Time.deltaTime, Space.World);
                } 
                waitingTime += Time.deltaTime;
                if(waitingTime >= 1.7f) {
                    attackStep++;
                    waitingTime = 0;
                }

                if(!ballSounded) {
                    source.PlayOneShot(Ball);
                    ballSounded = true;
                }

                break;
                case 2:
                bounceTarget = new Vector2(playerPosition.position.x, 28.61f);
                source.Stop();
                attackStep++;
                break;
                case 3:
                transform.position = Vector2.MoveTowards(transform.position, bounceTarget, 18.5f * Time.deltaTime);
                break;
                case 4:
                if(bouncesDone == maxBounces) {
                    attackStep++;
                    bouncesDone = 0;
                    ballSounded = false;
                } else if(transform.position.y < bounceHeight.position.y) {
                    transform.Translate(Vector2.up * 8f * Time.deltaTime, Space.World);
                } else {
                    attackStep = 2;
                }
                break;
                case 5:
                transform.position = Vector2.Lerp(transform.position, new Vector2(defaultPosition.position.x, defaultPosition.position.y), Time.deltaTime * 1.5f);
                waitingTime += Time.deltaTime;
                GetComponent<BoxCollider2D>().enabled = false;
                if (waitingTime >= 1.5f) {
                    attackStep++;
                    waitingTime = 0;
                }
                break;
                case 6:
                animator.SetBool("Idle", true);
                animator.SetBool("Rolling", false);
                waitingTime += Time.deltaTime;
                if(waitingTime >= 2.5f) {
                    Reroll(maxPossibleAttack);
                    ballSounded = true;
                    source.Stop();
                    waitingTime = 0;
                }

                if (!ballSounded) {
                    source.PlayOneShot(Ball);
                    ballSounded = true;
                }

                break;
            }

        } else if(currentAttack == 3) {
            switch (attackStep) {
            case 1:
            transform.localScale = defaultScaler;
            transform.position = Vector2.MoveTowards(transform.position, ceilingPosition.position, 18f * Time.deltaTime);
            transform.up = ceilingPosition.position - transform.position;
            animator.SetBool("DashingUp", true);

                if (!dashSounded) {
                    source.PlayOneShot(Dash);
                    dashSounded = true;
                }

                break;
            case 2:
            dashSounded = false;
            animator.SetBool("DashingUp", false);
            animator.SetBool("onCeiling", true);
                for (int i = 0; i < 3; i++) {
                    Instantiate(projectile, projectilePositions[i].position, projectilePositions[i].rotation);
                    source.PlayOneShot(Projectiles, 0.3f);
                }
            timesShot++;
            attackStep++;
            break;
            case 3:
            if(timesShot == maxShots) {
                attackStep = 6;
                timesShot = 0;
            } else {
                waitingTime += Time.deltaTime;
                if(waitingTime >= 0.5f) {
                    attackStep++;
                    waitingTime = 0;
                }
            }
            break;
            case 4:
                for (int i = 3; i < 7; i++) {
                    Instantiate(projectile, projectilePositions[i].position, projectilePositions[i].transform.rotation);
                    source.PlayOneShot(Projectiles, 0.3f);
                }
            timesShot++;
            attackStep++;
            break;
            case 5:
            if(timesShot == maxShots) {
                timesShot = 0;
                attackStep = 6;
            } else {
                waitingTime += Time.deltaTime;
                if(waitingTime >= 0.5f) {
                    attackStep = 2;
                    waitingTime = 0;
                }
            }
            break;
            case 6:
            transform.position = Vector2.MoveTowards(transform.position, defaultPosition.position, 11f * Time.deltaTime);
            transform.up = defaultPosition.position - transform.position;
            animator.SetBool("DashingUp", true);
            animator.SetBool("onCeiling", false);

                if (!dashSounded) {
                    source.PlayOneShot(Dash);
                    dashSounded = true;
                }

            break;

            case 7:
            waitingTime += Time.deltaTime;
            if(waitingTime >= 0.8f) {
                Reroll(maxPossibleAttack);
                source.Stop();
                waitingTime = 0;
            }
            break;
            }

        } else if(currentAttack == 4) {
            switch(attackStep) {
                case 1:
                attacksDone = 0;
                animator.SetBool("Charging", true);
                chargingEnergy = Instantiate(chargedEnergy, energyPosition.position, new Quaternion(0, 0, 0, 0));
                Target1.position = new Vector2(Target1.position.x, chargingEnergy.transform.position.y);
                Target2.position = new Vector2(Target2.position.x, chargingEnergy.transform.position.y);
                energyScript = chargingEnergy.GetComponent<ChargedEnergy>();
                GetComponent<BoxCollider2D>().enabled = false;
                energyScript.creator = this.gameObject;
                source.PlayOneShot(Charge);
                attackStep++;
                break;
                case 2:
                if(chargingEnergy.transform.localScale.x >= energyScript.maxScale.x) {
                    energyScript.released = true;
                    animator.SetBool("Idle", true);
                    animator.SetBool("Charging", false);
                    source.Stop();
                    transform.localScale = scaler;
                } else {
                    chargingEnergy.transform.position = energyPosition.position;
                    isFacingPlayer = Physics2D.OverlapCircle(radiusStart.position, playerRadius, Player);

                    if(isFacingPlayer == false){
                        Flip();
                    }

                    if(transform.localScale.x > 0) {
                        currentTarget = Target1;
                    } else {
                        currentTarget = Target2;
                    }

                    chargingEnergy.transform.right = currentTarget.position - chargingEnergy.transform.position;
                }
                break;
            }
        }

        if(attacksDone < 2) {
            maxPossibleAttack = 4;
        } else {
            maxPossibleAttack = 5;
        }



    }

    public void Reroll(int attackMax) {
        source.Stop();
        lastAttack = currentAttack;
        GetComponent<BoxCollider2D>().enabled = true;
        if (attacksDone >= 3 && lastAttack != 1) {
            currentAttack = 4;
        } else {
            currentAttack = Random.Range(1, attackMax);
        } 

       if(lastAttack == currentAttack) {
            source.Stop();
            Reroll(attackMax);
       } else {
            attacksDone++;
            transform.localScale = defaultScaler;
            ballSounded = false;
            dashSounded = false;
        }

       animator.SetBool("Idle", false);
       attackStep = 1;
       
    }

    public void LineEnable() {
        machineLine.enabled = true;
    }

    public void ChangePosition(int positionElement) {
        manager.currentPos = manager.beingPositions[positionElement];
    }

    void Flip() {
        dashDirection *= -1;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public void ChangeDashPosition() {
        glassIsDestroyed = true;
        Destroy(dashStopPosition);
    }

    public void FormSound() {
        source.PlayOneShot(Form);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "DashStop" && currentAttack == 1) {
            Flip();

            if(transform.localScale.x < 0) {
                transform.position = new Vector2(6.72f, 30.12f);
            } else {
                if(!glassIsDestroyed) {
                    transform.position = new Vector2(31.2f, 30.12f);
                } else {
                    transform.position = new Vector2(33.68f, 30.12f);
                }
                
            }

            waitingTime = 0;
            attackStep++;
        }

        if(col.gameObject.name == "Floor" && currentAttack == 2) {
            bouncesDone++;
            source.PlayOneShot(Bounce);
            attackStep = 4;
        } else if(col.gameObject.name == "Ceiling" && currentAttack == 3) {
            attackStep++;
            transform.up = defaultUp;
        } else if(col.gameObject.name == "DefaultPosition" && currentAttack == 3) {
            attackStep = 7;
            transform.up = defaultUp;
            animator.SetBool("DashingUp", false);
            animator.SetBool("Idle", true);
            transform.position = new Vector2(defaultPosition.position.x, defaultPosition.position.y);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(radiusStart.position, playerRadius);
    }
}
