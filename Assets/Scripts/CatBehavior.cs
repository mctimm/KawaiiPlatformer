using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehavior : EnemyBasics
{
    // Start is called before the first frame update
    float totalMovement = 0.0f;
    public float MOVEMENTCAP = 5.0f;
    int direction = 0;
    float speed = 3f;

    bool dead = false;

    bool canPounce;
    Animator animator;

    Rigidbody2D rb;

    void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    } 
    void Start()
    {
        direction = -1;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if(dead){
            return;
        }
        if(totalMovement > MOVEMENTCAP || rb.velocity.x == 0){
            direction = -direction;
            totalMovement = 0.0f;
        }
        totalMovement += Mathf.Abs(Time.deltaTime * rb.velocity.x) ;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        gameObject.GetComponent<SpriteRenderer>().flipX = direction > 0;
        //animator.SetFloat("Speed", Mathf.Abs(direction));

        if(canPounce && Random.Range(0,100) > 98){
            Pounce();
        }

    }

    void Pounce(){
        rb.velocity = new Vector2(rb.velocity.x, 5f);
        canPounce = false;
    }

    void OnCollisionEnter2D(Collision2D col){
        //print(col.gameObject.tag);
        if(!col.gameObject.tag.Equals("Player") && !col.gameObject.tag.Equals("Untagged")){
                direction = -direction;
                totalMovement = 0.0f;
        }
    }

    public override void Death(){
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        animator.SetBool("Dead", true);
        Destroy(gameObject, 0.5f);
        dead = true;
    }
}
