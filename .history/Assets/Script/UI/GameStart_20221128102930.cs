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

    void SwitchScene(){
        GM.GetComponent<GameManager>().SetSceneNum("Boss");
    }

        //============点击行为================
    #region
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        Press();
    }
    private void Press()
    {
        m_MyOnClick.Invoke();
    }


    //=========================================
    #endregion;
    //============移入行为================
    #region
    public void OnPointerEnter(PointerEventData eventData)
    {
        Enter();
    }
    private void Enter()
    {
        m_MyOnEnter.Invoke();
    }
    #endregion

    //============移出行为================
    #region
    public void OnPointerExit(PointerEventData eventData)
    {
        Exit();
    }
    private void Exit()
    {
        m_MyOnExit.Invoke();
    }
    #endregion 作者：MirMirror https://www.bilibili.com/read/cv15728835 出处：bilibili
}
