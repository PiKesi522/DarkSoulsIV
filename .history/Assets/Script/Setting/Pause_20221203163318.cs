using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        // {
        //     pause();
        //     Debug.Log("GetPause");
        // }
        // else if(Input.GetKeyDown(KeyCode.Escape)&& isPaused)
        // {
        //     unpause();
        //     Debug.Log("GetUnpause");
        // }
    }

    
    private void pause()
    {
        isPaused = true;
        // UIPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    
    private void unpause()
    {
        isPaused = false;
        // UIPanel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
