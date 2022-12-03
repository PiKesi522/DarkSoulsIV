using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : MonoBehaviour
{
    public Image UIImage;
    public float alpha;
    public float alphaMax;
    public float alphaRate;
    public float R;
    public float G;
    public float B;

    void Start(){
        // StartCoroutine(FadeIn());
    }

    public void playDeathAnimation(){
        StartCoroutine(FadeOut());
    }

    public void playBossDefeatAnimation(){
        StartCoroutine(FadeOut());
    }

    public void stopBossDefeatAnimation(){
        StartCoroutine(FadeIn());
    }
    
    IEnumerator FadeOut(){
        // alpha = 1;
        UIImage.enabled = true;

        while (alpha < alphaMax)
        {
            alpha += Time.deltaTime * alphaRate;
            UIImage.color = new Color(R / 255, G / 255, B / 255, alpha / 255);
            yield return new WaitForSeconds(0);
        }

        // Mask.enabled = false;
    }

    IEnumerator FadeIn(){
        // alpha = 1;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * alphaRate;
            UIImage.color = new Color(R / 255, G / 255, B / 255, alpha / 255);
            yield return new WaitForSeconds(0);
        }

        UIImage.enabled = false;
        // Mask.enabled = false;
    }

}
