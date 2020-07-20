using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatform  : MonoBehaviour
{
    // Start is called before the first frame update
    public bool goingUp = true;
    float bottom;
    float top;
    Rigidbody2D rb;
    public float gap = 9f;
    int direction;
    public float movement = 1f;
    void Awake()
    {
        if(goingUp)
        {
            bottom = gameObject.transform.position.y;
            direction = 1;
            top = bottom + gap;
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        else
        {
            top = gameObject.transform.position.y;
            direction = -1;
            bottom = top - gap;
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
    }

    void FixedUpdate(){
        
        rb.MovePosition(rb.position + (Vector2.up * movement * direction * Time.deltaTime));
        if(rb.position.y > top){
            Destroy(gameObject, 0f);
         } else if (rb.position.y < bottom){
             Destroy(gameObject, 0f);
         }
    }

    // Update is called once per frame
    // void Update()
    // {
    //     rb.velocity = Vector2.up * movement*direction;
    //     if(rb.position.y > top){
    //         Destroy(gameObject, 0f);
    //     } else if (rb.position.y < bottom){
    //         Destroy(gameObject, 0f);
    //     }

    // }
}
