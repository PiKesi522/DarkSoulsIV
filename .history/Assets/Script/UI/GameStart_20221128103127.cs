using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public GameObject GM;
    public Image Mask;
    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("GameManager").gameObject;
        // Debug.Log(GM);
        Mask = GameObject.Find("Mask").GetComponent<Image>();
    }

    public void OnClick(){
        // SceneManager.LoadScene("BattleField");
        Mask.GetComponent<SceneFade>().SceneFadeOut();
        Invoke("SwitchScene", 3f);
    }

    private void OnMouseEnter() {
        this.transform.Find("Image").gameObject.SetActive(true);
    }

    private void OnMouseExit() {
        this.transform.Find("Image").gameObject.SetActive(false);
    }

    void SwitchScene(){
        GM.GetComponent<GameManager>().SetSceneNum("Boss");
    }
}
