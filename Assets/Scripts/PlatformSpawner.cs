using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject platform;

    float timeCurrent = 0;

    float platformTime = 3f;
    

    // Update is called once per frame
    void Update()
    {
        if(timeCurrent > platformTime){
            Spawn();
            timeCurrent = 0f;
        }else{
            timeCurrent += Time.deltaTime;
        }
    }

    void Spawn(){
        Instantiate(platform, gameObject.transform.position, Quaternion.identity);
    }
}
