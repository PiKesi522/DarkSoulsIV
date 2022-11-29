using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject GM;
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("GameManager").gameObject;
        // Debug.Log(GM);
    }

    public void OnClick(){
        // SceneManager.LoadScene("BattleField");
        GM.GetComponent<GameManager>().SetSceneNum("BattleField");
    }
}
