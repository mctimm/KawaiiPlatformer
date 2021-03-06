﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxBearBehavior : EnemyBasics
{
    // Start is called before the first frame update
    Animator animator;
    bool dead = false;

    public GameObject fireBall;

    public Transform firePoint;

    float timeCurrent;

    bool casting = false;
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("Dead", dead);
        if(dead){
            return;
        }
        animator.SetBool("isCasting", casting);
        casting = false;
        timeCurrent += Time.deltaTime;
        if(timeCurrent > 2){
            Cast();
            timeCurrent = 0;
        }
    }

    void Cast(){
        casting = true;
        Invoke("InstantiateFire", 0.35f);
    }

    private void InstantiateFire(){
        Instantiate(fireBall, firePoint.position, firePoint.rotation);
    }


    public override void Death(){
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        timeCurrent = 0;
        dead = true;
        Destroy(gameObject, 0.5f);
        
        
    }
}
