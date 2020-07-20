using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2 : MonoBehaviour
{
    // Start is called before the first frame update
    public bool goingRight = true;
    float left;
    float right;
    Rigidbody2D rb;
    public float gap = 4f;
    int direction;
    public float movement = 1f;
    void Awake()
    {
        if(goingRight)
        {
            left = gameObject.transform.position.x;
            direction = 1;
            right = left + gap;
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        else
        {
            right = gameObject.transform.position.x;
            direction = -1;
            left = right - gap;
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        
    }

    void FixedUpdate(){
        rb.MovePosition(rb.position + (Vector2.right * movement*direction*Time.deltaTime));
        if(rb.position.x >= right){
            direction = -1;
        } else if (rb.position.x <= left){
            direction = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = Vector2.right * movement*direction;
        // if(rb.position.x >= right){
        //     direction = -1;
        // } else if (rb.position.x <= left){
        //     direction = 1;
        // }
        

    }
}
