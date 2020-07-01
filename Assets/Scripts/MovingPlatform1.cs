using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform1 : MonoBehaviour
{
    // Start is called before the first frame update
    float bottom;
    float top;
    Rigidbody2D rb;
    public float gap = 4f;
    int direction;
    float movement = 1f;
    void Awake()
    {
        bottom = gameObject.transform.position.y;
        direction = 1;
        top = bottom + gap;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

    }

    

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.up * movement*direction;
        if(rb.position.y >= top){
            direction = -1;
        } else if (rb.position.y <= bottom){
            direction = 1;
        }

    }
}
