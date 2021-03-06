﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Game Design Idea: use this for moving platforms
public class DivingEnemyBehavior : EnemyBasics
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Animator animator;

    float attackTime = 3f;
    float currentTime = 0f;

    float swoopDown = -7f;
    float swoopSpeed = -5f;
    bool isSwooping;
    float highPosition;

    public bool dead;

    void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        highPosition = gameObject.transform.position.y;
    }
    void Start()
    {
        rb.gravityScale = 0;
        isSwooping = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        
    }

    void FixedUpdate(){
        //if(rb.velocity.y == 0){
            //rb.gravityScale = -2.5f;
        //}
        currentTime += Time.deltaTime;
        if(currentTime >= attackTime && !dead){
            Swoop();
            currentTime = 0f;
            isSwooping = true;
        }

        //print(rb.velocity);
        //print(swoopSpeed);
        //print(isSwooping);
        if(isSwooping && (rb.position.y >= highPosition)){
            //print("here");
            Vector3 temp = gameObject.transform.position;
            temp.y = highPosition;
            gameObject.transform.position = temp;
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            swoopSpeed = -swoopSpeed;
            isSwooping = false;
        }

    }

    void Swoop(){
        rb.position = new Vector2(rb.position.x, rb.position.y - 0.1f);
        rb.gravityScale = -1;
        rb.velocity = new Vector2(swoopSpeed,swoopDown);
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
