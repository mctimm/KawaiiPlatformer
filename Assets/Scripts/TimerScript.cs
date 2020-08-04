using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerScript : MonoBehaviour
{
    Text timeText;
    float totalTime = 0.0f;

    void Awake()
    {
        timeText = gameObject.GetComponent<Text>();
        
    }
    void Start()
    {
        totalTime = GlobalController.Instance.timer;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    void UpdateText(){ 
        totalTime += Time.deltaTime;
        timeText.text = truncate(totalTime).ToString();
    }

    float truncate(float n){
        return ((int)(n*10))/10.0f;
    }

    public float getTotal(){
        return totalTime;
    }
}
