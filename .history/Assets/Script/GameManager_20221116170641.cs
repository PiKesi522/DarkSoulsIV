using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }//单例
    public enum SceneNum{
        HeadScene = 0,
        BattleField = 1,
    }
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

    public void setSceneNum(){

    }

}
