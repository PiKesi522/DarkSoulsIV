using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }//单例

    public string[] SceneList;
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

        SceneList = new string[2] {"HeadPage", "BattleField"};
        presentScene = 0;
    }

    private void SceneUpdate(string newScene){
        // if(newScene == "Battle"){

        // }
    }

    public void SetSceneNum(string SceneName){
        for(int i = 0; i < SceneList.Length; i++){
            if(SceneList[i] == name){
                this.presentScene = i;
                SceneUpdate();
                return;
            }
        }
    }

    public string GetSceneNum(){
        return SceneList[this.presentScene];
    }


}
