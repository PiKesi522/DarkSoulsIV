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

    void Start(){
        // StartCoroutine(FadeIn());
    }

    public void playDeathAnimation(){
        StartCoroutine(FadeIn());
    }
    
    IEnumerator FadeIn(){
        // alpha = 1;

        while (alpha < alphaMax)
        {
            alpha -= Time.deltaTime * 0.25f;
            Mask.color = new Color(0,0,0,alpha);
            yield return new WaitForSeconds(0);
        }

        Mask.enabled = false;
    }

}
