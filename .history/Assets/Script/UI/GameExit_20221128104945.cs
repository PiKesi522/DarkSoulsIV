using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameExit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image Select;

    // Start is called before the first frame update
    void Awake()
    {
        Select = this.transform.Find("Image").GetComponent<Image>();
    }

    public void OnClick(){
        Debug.Log("exit");
        #if UNITY_EDITOR //编辑器中退出游戏
            UnityEditor.EditorApplication.isPlaying = false;
        #else //应用程序中退出游戏
            UnityEngine.Application.Quit();
        #endif
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
        Select.enabled = true;
	}
 
	public void OnPointerExit(PointerEventData eventData)
	{
        Select.enabled = false;
    }
}
