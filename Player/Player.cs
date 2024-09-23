using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using System.Runtime.InteropServices.WindowsRuntime;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    [HideInInspector] public float moveInput;
    [HideInInspector] public float jumpForce;
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    [HideInInspector] public bool facingRight = false;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    [HideInInspector] public bool canMove = true;
    bool canOpenDoor;
    [HideInInspector] public bool canReadSign;
    Tablet sign;
    public LayerMask doorLayer;
    public LayerMask signLayer;
    static bool comesThroughDoor;
    float glideTimer;
    static Vector2 spawnPos;
    LoadingZone zone;
    static bool usingCustomLoad;

    float launcherCooldown;
    float defaultLauncherCooldown = 0.75f;
    public GameObject rock;
    public Transform launchPos;
    public Transform secondaryLaunchPos;

    float LastYVal;
    public int hitPoint = 5;
    static int numberOfKeys;
    static int numberOfMasterKeys;
    PlayerHealth healthScript;
    BoxHook hookScript;
    CupClimb cupScript;
    PlayerAudio audioScript;
    public Transform barTransform;
    static bool canHook;
    static bool canClimb;
    public static bool hasLauncherUpgrade;
    [HideInInspector] public Vector3 scaler;
    public bool waitingToEnd;
    [SerializeField] LayerMask secondGroundLayer; // because the other layermask doesnt work for something

    [SerializeField] PrisonDust prisonDust;

    static bool enteredUpgrades;


    // Basic player controller by blackthornprod
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(10, 7);
        animator = GetComponent<Animator>();
        healthScript = GetComponent<PlayerHealth>();
        hookScript = GetComponent<BoxHook>();
        cupScript = GetComponent<CupClimb>();
        audioScript = GetComponent<PlayerAudio>();
        scaler = transform.localScale;

        if(hasLauncherUpgrade) {
            UpgradeLauncher();
        }

        if (usingCustomLoad == true) {
            if(spawnPos.x != 0 && spawnPos.y != 0) {
                transform.position = spawnPos; 
            }
            
            usingCustomLoad = false;
        }

        if (this.gameObject.scene.buildIndex == 18) {
            enteredUpgrades = true;
        }
    }

    void FixedUpdate(){
        moveInput = Input.GetAxisRaw("Horizontal");
        if(canMove == true){
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (facingRight == true && moveInput > 0) {
            Flip();
        } else if (facingRight == false && moveInput < 0) {
            Flip();
        }
 }

     
    } 

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        canOpenDoor = Physics2D.OverlapCircle(transform.position, 0.58f, doorLayer);
        canReadSign = Physics2D.OverlapCircle(transform.position, 0.58f, signLayer);
        glideTimer -= Time.deltaTime;
        hookScript.enabled = canHook;
        cupScript.enabled = canClimb;

        if (isGrounded == true && hookScript.isHooked == false && Input.GetKeyDown(KeyCode.Space)) {
            Jump();
        }

          if (Input.GetKey(KeyCode.Space) && isJumping == true) {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if(canReadSign && Input.GetKeyDown(KeyCode.W)){
            sign = GameObject.FindGameObjectWithTag("Sign").GetComponent<Tablet>();
            sign.TriggerTablet();
            SelfDisable();
        }

    if(Input.GetKey(KeyCode.B) && launcherCooldown <= 0 && moveInput == 0 && isGrounded == true){
        launcherCooldown = defaultLauncherCooldown;

        if(hasLauncherUpgrade && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))) {
            Instantiate(rock, secondaryLaunchPos.position, Quaternion.identity);
            animator.SetBool("shootingUp", true);
        } else {
            Instantiate(rock, launchPos.position, Quaternion.identity);
            animator.SetBool("shootingUp", false);
        }

        animator.SetBool("launched", true);
    } else {
        launcherCooldown -= Time.deltaTime;
        animator.SetBool("launched", false);
    }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
        
     if(moveInput < 0 | moveInput > 0 && isGrounded == true) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }

        if(isGrounded == false)  {
            animator.SetBool("isJumping", true);
        } else {
            animator.SetBool("isJumping", false);
        }
     
    }

    public void Jump(){
        isJumping = true;
        jumpTimeCounter = jumpTime;
        rb.velocity += Vector2.up * jumpForce;
        // rb.AddForce(Vector2.up * jumpForce);
        glideTimer = 0.5f;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }

    public void Flip()
    {  
        facingRight = !facingRight;
        scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
        barTransform.localScale = new Vector2(barTransform.localScale.x * -1, barTransform.localScale.y);
    }

    public void ToggleMovement() {
        canMove = !canMove;
        rb.velocity = new Vector2(0,0);
    }

    public void LoadPlayerScene(Vector2 targetPos, int index){
        SceneManager.LoadScene(index);
        spawnPos = targetPos;
        usingCustomLoad = true;
    }

    public void SelfDisable() {
        rb.velocity = new Vector2(0,0);
        animator.SetBool("isJumping", false);
        animator.SetBool("isRunning", false);
        this.enabled = false;
    }

    public void WipeData() {
        canClimb = false;
        canHook = false;
        hasLauncherUpgrade = false;
    }

    public void WaitingForLand() {
        waitingToEnd = true;
    }

    public void UpgradeLauncher() {
        hasLauncherUpgrade = true;
        defaultLauncherCooldown *= 0.8f;
        animator.SetBool("launcherUpgraded", true);
    }

    public bool LauncherUpgrade() {
        return hasLauncherUpgrade;
    }

    public bool Hook() {
        return canHook;
    }

    public bool Cups() {
        return canClimb;
    }

    public int GetKeyNumber(bool regular) => (regular) ? numberOfKeys : numberOfMasterKeys;

    public bool EnteredUpgrades() => enteredUpgrades;

    public void SetKeys(int regularKeys, int masterKeys) {
        numberOfKeys = regularKeys;
        numberOfMasterKeys = masterKeys;
    }

    public void LoadPlayerData(int Scene, bool hasCups, bool hasHook, bool hasUpgrade) {
        canHook = hasHook;
        canClimb = hasCups;
        hasLauncherUpgrade = hasUpgrade;
        SceneManager.LoadScene(Scene);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Gate") {
            Gate g = col.gameObject.GetComponent<Gate>();
            if(numberOfKeys >= g.keysNeeded) {
                numberOfKeys -= g.keysNeeded;
                g.Open();
            }
        } else if(col.gameObject.tag == "MasterGate") {
            Gate mg = col.gameObject.GetComponent<Gate>();
            if(numberOfMasterKeys >= mg.keysNeeded) {
                numberOfMasterKeys -= mg.keysNeeded;
                mg.Open();
                Debug.Log(numberOfMasterKeys);
            }
        } else if(col.gameObject.CompareTag("Wall")) {
            if(canClimb && !isGrounded && cupScript.wallCooldown <= 0) {
                transform.position = new Vector2(cupScript.WallOffset(), transform.position.y);
                cupScript.onWall = true;
                SelfDisable();
            }
        } else if(col.gameObject.name == "Ground" && waitingToEnd) {
            GameObject.FindGameObjectWithTag("SAS").GetComponent<StartandStop>().BossEnd();
        } else if (col.gameObject.name == "Gray" || col.gameObject.name == "Tilemap" || col.gameObject.name == "Ground") {
            audioScript.PlayLand();
            if(prisonDust.enabled) {
                prisonDust.JumpParticle();
            }
            
        } 
    }

    void OnCollisionExit2D(Collision2D col) {
        
    }

    void OnTriggerExit2D(Collider2D interact) {
        if(interact.gameObject.tag == "TipDisplay2") {
            TipSpawn spawn = interact.gameObject.GetComponent<TipSpawn>();
            spawn.ToggleVisibility(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D interact){
        switch(interact.gameObject.tag){
        case "loadingZone":
        zone = interact.gameObject.GetComponent<LoadingZone>();
        LoadPlayerScene(zone.loadpos, zone.index + 1);
        break;  
        case "Key":
        numberOfKeys++;
        audioScript.PlayKey();
        Destroy(interact.gameObject);
        break;
        case "MasterKey":
        numberOfMasterKeys++;
        audioScript.PlayKey();
        Destroy(interact.gameObject);
        Debug.Log(numberOfMasterKeys);
        break;
        case "Lift":
        Lift l = interact.gameObject.GetComponent<Lift>();
        l.StartLift();
        break;    
        case "AutomaticGate":
        AutomaticGate g = interact.gameObject.GetComponent<AutomaticGate>();
        g.Fall();
        break;
        case "CamChange":
        ChangeCameraPosition tc = interact.gameObject.GetComponent<ChangeCameraPosition>();
        BoxCollider2D ccb = interact.gameObject.GetComponent<BoxCollider2D>();
        tc.Toggle();
        ccb.enabled = false;
        break;
        case "BossGuardTrig":
        BossGuardTrigger bgt = interact.gameObject.GetComponent<BossGuardTrigger>();
        bgt.BossTimelineStart();
        break;   
        case "Figure":
        healthScript.TakeDamage(1);
        break;
        case "Hook":
        canHook = true;
        audioScript.PlayItem();
        Destroy(interact.gameObject);
        break;
        case "Cups":
        canClimb = true;
        audioScript.PlayItem();
            Destroy(interact.gameObject);
        break;
        case "Placeholder1":
        BeingManager bm = interact.gameObject.GetComponent<BeingManager>();
        bm.StartBossOpening();
        break;
        case "Laser":
        healthScript.TakeDamage(1);
        break;
        case "Bolt":
        healthScript.TakeDamage(2);
        break;
        case "TipDisplay":
        if(canHook) {
            TipSpawn tSpawn = interact.gameObject.GetComponent<TipSpawn>();
            tSpawn.DisplayTip();
        }
        break;
        case "TipDisplay2":
        TipSpawn TS = interact.gameObject.GetComponent<TipSpawn>();
        TS.ToggleVisibility(true);
        break;
    }

    if(interact.gameObject.name == "Energy Being") {
        healthScript.TakeDamage(1);
    } else if(interact.gameObject.name == "PrisonExit") {
        prisonDust.ExitPrison();
    }
}



}

