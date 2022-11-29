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
        // Debug.Log(newScene);
        SceneManager.LoadScene(newScene);
        if(newScene == "BattleField"){
            Cursor.lockState = CursorLockMode.Locked;//隐藏鼠标指针
        }
        else{
            Cursor.lockState = CursorLockMode.None;//显示鼠标指针
        }
    }

    public void SetSceneNum(string SceneName){ 
        for(int i = 0; i < SceneList.Length; i++){
            if(SceneList[i] == SceneName){
                // Debug.Log("hit");
                this.presentScene = i;
                SceneUpdate(SceneName);
                return;
            }
        }
        Debug.LogError("Not Find Scene");
    }

    public string GetSceneNum(){
        return SceneList[this.presentScene];
    }


}
