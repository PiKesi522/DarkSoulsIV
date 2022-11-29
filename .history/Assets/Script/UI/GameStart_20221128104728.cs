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

    // Start is called before the first frame update
    void Awake()
    {
        GM = GameObject.Find("GameManager").gameObject;
        // Debug.Log(GM);
        Mask = GameObject.Find("Mask").GetComponent<Image>();
        Select = this.transform.Find("Image").GetComponent<Image>();
    }

    public void OnClick(){
        // SceneManager.LoadScene("BattleField");
        // Debug.Log("Click");
        Mask.GetComponent<SceneFade>().SceneFadeOut();
        Invoke("SwitchScene", 3f);
    }

 
	public void OnPointerEnter(PointerEventData eventData)
	{
        Debug.Log("Enter");
		// tipsBg.SetActive(true);
	}
 
	public void OnPointerExit(PointerEventData eventData)
	{
        Debug.Log("Exit");
		// tipsBg.SetActive(false);
    }

    void SwitchScene(){
        GM.GetComponent<GameManager>().SetSceneNum("Boss");
    }
}
