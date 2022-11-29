using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Debug.Log(newScene);
        SceneManager.LoadScene(newScene);

        // }
    }

    public void SetSceneNum(string SceneName){
        Debug.Log(SceneName);
        Debug.Log(SceneList[1] == SceneName);   
        for(int i = 0; i < SceneList.Length; i++){
            if(SceneList[i] == name){
                Debug.Log("hit");
                this.presentScene = i;
                SceneUpdate(SceneName);
                return;
            }
        }
    }

    public string GetSceneNum(){
        return SceneList[this.presentScene];
    }


}
