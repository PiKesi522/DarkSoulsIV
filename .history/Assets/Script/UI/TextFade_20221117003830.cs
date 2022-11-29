using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFade : MonoBehaviour
{
    public Text UIText;
    public float alpha;
    public float alphaMax;
    public float alphaRate;
    public float R;
    public float G;
    public float B;

    void Start(){
        // StartCoroutine(FadeIn());
        // UIText = this.gameObject.GetComponent<Text>();
        // Debug.Log(UIText.color);
    }

    public void TextFadeIn(){
        StartCoroutine(FadeIn());
    }

    public void TextFadeOut(){
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeOut(){
        // Debug.Log(R/255);
        // alpha = 1;
        UIText.enabled = true;

        while (alpha < (alphaMax/ 255))
        {
            alpha += Time.deltaTime * alphaRate;
            UIText.color = new Color(R / 255 , G / 255 , B / 255 , alpha);
            yield return new WaitForSeconds(0);
        }

        // Mask.enabled = false;
    }

    IEnumerator FadeIn(){
        // Debug.Log(R/255);
        // alpha = 1;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * alphaRate;
            UIText.color = new Color(R / 255 , G / 255 , B / 255 , alpha);
            yield return new WaitForSeconds(0);
        }

        UIText.enabled = false;
        // Mask.enabled = false;
    }

}
