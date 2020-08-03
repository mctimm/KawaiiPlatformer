using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("nextLevel", 5f);
    }

    void nextLevel(){
        string next = GlobalController.Instance.nextLevel();
        SceneManager.LoadScene(next);
    }
}
