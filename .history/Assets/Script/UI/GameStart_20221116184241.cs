using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public static GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.Find("GameManager").gameObject;
    }

    public void OnClick(){
        // Debug.Log("Change");
        // SceneManager.LoadScene("BattleField");
        GM.GetComponent<GameManager>().SetSceneNum("BattleField");
    }
}
