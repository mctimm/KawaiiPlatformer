using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnder : MonoBehaviour
{
    // Start is called before the first frame update

    Animator animator;
    bool animationFinished= false;
    bool accessor = false;
    void Awake(){
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("Collected", false);
    }
    void OnTriggerEnter2D(Collider2D col){
        
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("Collected", true);
        GlobalController.Instance.health = col.gameObject.GetComponent<PlayerMovement>().health;
        GlobalController.Instance.timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerScript>().getTotal();
        Invoke("nextLevel", 3f);
        
    }

    void Update(){
       

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("StarCelebration")){
            animationFinished = false;
            accessor = true;
            animator.SetBool("Collected", false);
        }else if (accessor){
            animationFinished = true;
        }

         if(animationFinished){
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void nextLevel(){
        SceneManager.LoadScene("LevelComplete");
    }
}
