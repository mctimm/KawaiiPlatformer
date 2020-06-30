using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    bool isGrounded = true;
    bool flipped = false;
    bool invincible = false;
    float currentDirection;
    
    bool gameOver = false;

    public int health = 5;

    float BLINKTIMETOTAL = 3.0f;
    float BLINKDURATION = 0.2f;
    float blinkTimeCurrent = 0.0f;
    float blinkTimeCurrentmini = 0.0f;

    bool isBlinking;

    bool locked;

    float currentLockTime = 0f;
    float lockTime = 1f;
    
    public Transform groundCheck;
    public Transform groundCheckL;
    public Transform groundCheckR;
    float speed = 6f;
    float walkSpeed  = 4f;
    float runSpeed = 6f;
    float accel = 4f;
    float slowDown = 2f;
    float jumpSpeed = 7.5f;
    Animator animator;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        currentDirection = -1;
        locked = false;
    }

    void FixedUpdate(){
        //float direction = Input.GetAxis("Horizontal");
        if(gameOver){
            return;
        }
        isGrounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        isGrounded |= Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"));
        isGrounded |= Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"));
        //print(LayerMask.NameToLayer("Ground"));
        //print(isGrounded);

        float direction = Input.GetAxis("Horizontal");
        float currentAccel = accel *direction;
        
        if(locked && (currentLockTime < lockTime) && !isGrounded){
            currentLockTime += Time.deltaTime;
            return;
        }else {
            locked = false;
        }

        if(Input.GetKey(KeyCode.L)){
            speed = runSpeed;
        }else{
            speed = walkSpeed;
        }

        animator.SetFloat("Speed", Mathf.Abs(direction));
        
        if(!(currentDirection * direction >= 0)){
            Flip();
            currentDirection = direction;
        }

        

        
        animator.SetFloat("JumpSpeed", rb.velocity.y);
        animator.SetBool("IsGrounded", isGrounded);
        float absSpeed = Mathf.Abs(rb.velocity.x);

        if(rb.velocity.y < 0 || !Input.GetKey(KeyCode.Space)){
            rb.gravityScale = 2;
        }else {
            rb.gravityScale = 1;
        }
        
        if(!isGrounded)
        {
            currentAccel /= 12;
        }
        else {
            if(absSpeed < slowDown){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else if(rb.velocity.x > 0){
                rb.velocity = new Vector2(rb.velocity.x - slowDown, rb.velocity.y);
            }else if (rb.velocity.x < 0){
                rb.velocity = new Vector2(rb.velocity.x + slowDown, rb.velocity.y);
            }
        }
        rb.velocity = new Vector2(rb.velocity.x + currentAccel, rb.velocity.y);
        
        absSpeed = Mathf.Abs(rb.velocity.x);
        //print("before check: " + (absSpeed >= speed));
        if(absSpeed >= speed){
            if(rb.velocity.x > 0){
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }else{
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }

        //print("After check: " + rb.velocity);
        

    }

    void Flip(){
        transform.Rotate(0f,180f, 0f);
        flipped = !flipped;
    }

    // Update is called once per frame
    void Update()
    {
        if(isBlinking){
            SpriteBlink();
        }
        if(health <= 0){
            sprite.enabled = false;
            isBlinking = false;
            gameOver = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            rb.velocity = Vector2.zero;
            Invoke("EndGame", 2.0f);
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && rb.velocity.y <= 0.1){
            rb.velocity = new Vector2(rb.velocity.x,jumpSpeed);
        }
    }

    void EndGame(){
            SceneManager.LoadScene("GameOver");        
    }

    private void Recoil(){
        rb.position += new Vector2(0f, 0.3f);
        if(flipped){
            rb.velocity = new Vector2(-3f,5f);
        }else{
            rb.velocity = new Vector2(3f,5f);
        }
        locked = true;
        currentLockTime = 0;
    }

    private void SpriteBlink(){
        blinkTimeCurrent += Time.deltaTime;
        blinkTimeCurrentmini += Time.deltaTime;
        if(blinkTimeCurrent > BLINKTIMETOTAL){
            isBlinking = false;
            sprite.enabled = true;
            invincible = false;
        }else{
            if(blinkTimeCurrentmini > BLINKDURATION){
                sprite.enabled = !sprite.enabled;
                blinkTimeCurrentmini = 0.0f;
            }
        }
    }

    public void takeDamage(){
        if(invincible){
            return;
        }
        health--;
        Recoil();
        isBlinking = true;
        blinkTimeCurrentmini = 0.0f;
        blinkTimeCurrent = 0.0f;
        invincible = true;
    }

    void OnCollisionEnter2D(Collision2D col){

        if(col.gameObject.tag.Equals("Enemy") && !invincible ){
            takeDamage();
        }

        if(col.gameObject.tag.Equals("KillZone")){
            health = 0;
        }

    }
}
