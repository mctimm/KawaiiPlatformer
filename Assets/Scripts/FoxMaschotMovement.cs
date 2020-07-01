using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMaschotMovement : EnemyBasics
{
    // Start is called before the first frame update
    float totalMovement = 0.0f;
    public float MOVEMENTCAP = 10.0f;
    int direction = 0;
    float speed = 3f;

    bool dead = false;

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
        if(totalMovement > MOVEMENTCAP || Mathf.Abs(rb.velocity.x) < 0.01){
            direction = -direction;
            totalMovement = 0.0f;
        }
        totalMovement += Mathf.Abs(Time.deltaTime * rb.velocity.x) ;
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        gameObject.GetComponent<SpriteRenderer>().flipX = direction > 0;
        //animator.SetFloat("Speed", Mathf.Abs(direction));
        print(totalMovement > MOVEMENTCAP || Mathf.Abs(rb.velocity.x) < 0.01);
        print(direction);

    }

    void OnCollisionEnter2D(Collision2D col){
        print(col.gameObject.tag);
        if(!col.gameObject.tag.Equals("Player") && !col.gameObject.tag.Equals("Untagged")){
                direction = -direction;
                totalMovement = 0.0f;
        }
    }

    public override void Death(){
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        //animator.SetBool("Dead", true);
        Destroy(gameObject, 0.5f);
        dead = true;
    }
}
