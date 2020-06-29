using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn2 : MonoBehaviour
{
     // Start is called before the first frame update
    public List<GameObject> enemies = new List<GameObject>();

    public List<Transform> enemyPositions = new List<Transform>();
    BoxCollider2D hitbox;

    void Awake(){
        hitbox = gameObject.GetComponent<BoxCollider2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col){
        if(col.gameObject.tag.Equals("Player")){
            for(int i = 0; i < enemies.Count; i++ ){
                Instantiate(enemies[i], enemyPositions[i].position, Quaternion.identity);
                hitbox.enabled = false;
            }

        }
    }
}
