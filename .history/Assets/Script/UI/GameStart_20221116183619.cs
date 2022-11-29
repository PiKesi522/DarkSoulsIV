using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick(){
        // Debug.Log("Change");
        // SceneManager.LoadScene("BattleField");
        Instance.SetSceneNum("BattleField");
    }
}
