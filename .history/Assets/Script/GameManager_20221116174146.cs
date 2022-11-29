using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string[] ScenceList;
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

        ScenceList = new string[2] {"HeadPage", "BattleField"};
        presentScene = 0;
    }

    private void SceneUpdate(){
        
    }

    public void SetSceneNum(string SceneName){
        for(int i = 0; i < ScenceList.Length; i++){
            if(ScenceList[i] == name){
                this.presentScene = i;
                return;
            }
        }
    }

    public string GetSceneNum(){
        return ScenceList[this.presentScene];
    }


}
