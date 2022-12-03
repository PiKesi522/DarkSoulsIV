using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
// using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject GM;
    public Image Mask;
    public Image Select;
    public GameObject AS;

    // Start is called before the first frame update
    void Awake()
    {
        AS = GameObject.Find("ButtonClick").gameObject;
        GM = GameObject.Find("GameManager").gameObject;
        // Debug.Log(GM);
        Mask = GameObject.Find("Mask").GetComponent<Image>();
        Select = this.transform.Find("Image").GetComponent<Image>();

    }

    public void OnClick(){
        // SceneManager.LoadScene("BattleField");
        // Debug.Log("Click");
        Mask.GetComponent<SceneFade>().SceneFadeOut();
        AS.Play();
        Invoke("SwitchScene", 3f);
    }

 
	public void OnPointerEnter(PointerEventData eventData)
	{
        Select.enabled = true;
	}
 
	public void OnPointerExit(PointerEventData eventData)
	{
        Select.enabled = false;
    }

    void SwitchScene(){
        GM.GetComponent<GameManager>().SetSceneNum("Boss");
    }
}
