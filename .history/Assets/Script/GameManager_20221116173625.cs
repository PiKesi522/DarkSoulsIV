using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }//单例
    public static SceneNum numTable;
    public int presentScene;
 
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        presentScene = 0;
    }

    private void SceneUpdate(){
        
    }

    public void SetSceneNum(string SceneName){
        // for 
        // this.presentScene = (SceneNum)SceneName;
        // Debug.Log(presentScene);
    }

    public string GetSceneNum(){
        return (string)(this.presentScene);
    }


}
