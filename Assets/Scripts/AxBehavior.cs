using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    
    void Awake(){
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Vector2 fortyfive = transform.right * -2f;
        fortyfive.y = 8f;
        rb.velocity = fortyfive;
        
    }

    void FixedUpdate(){
         rb.rotation -= 10f;
         
    }
       void OnCollisionEnter2D(Collision2D col){
        if(!col.gameObject.tag.Equals("EditorOnly") && !col.gameObject.tag.Equals("Enemy")){
            print(col.gameObject.tag);
            Destroy(gameObject);
        }
    }
}
