using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI_Text : MonoBehaviour
{
    public Text UIText;
    public float alpha;
    public float alphaMax;
    public float alphaRate;
    public int R;
    public int G;
    public int B;

    void Start(){
        // StartCoroutine(FadeIn());
        // UIText = this.gameObject.GetComponent<Text>();
        // Debug.Log(UIText.color);
    }

    public void playDeathAnimation(){
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeOut(){
        Debug.Log(UIText);
        // alpha = 1;
        UIText.enabled = true;

        while (alpha < alphaMax)
        {
            alpha += Time.deltaTime * alphaRate;
            UIText.color = new Color(R / 255, G / 255, B / 255, alpha / 255);
            yield return new WaitForSeconds(0);
        }

        // Mask.enabled = false;
    }

}
