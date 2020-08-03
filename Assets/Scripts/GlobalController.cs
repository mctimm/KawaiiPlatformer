using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour 
{
    public static GlobalController Instance;

    public int health;
    public int lives;

    //starting at level 2 because level 1 is currently named improperly
    public int level = 2;

    
    void Awake ()   
       {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy (gameObject);
        }
      }

    public string nextLevel(){
        level++;
        if(level <= 5) return "Level" + level;
        else return "GameOver";
        
    }
}
