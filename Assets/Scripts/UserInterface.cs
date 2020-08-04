using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UserInterface : MonoBehaviour
{
    Text playerStats;
    GameObject player;

    void Awake()
    {
        playerStats = gameObject.GetComponent<Text>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText(){
        string nextText = "Heath: ";
        nextText += player.GetComponent<PlayerMovement>().health;
        nextText += "\nlives: ";
        nextText += player.GetComponent<PlayerMovement>().lives;
        playerStats.text = nextText;

    }
}
