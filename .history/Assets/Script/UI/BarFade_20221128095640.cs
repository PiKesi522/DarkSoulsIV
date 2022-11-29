using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFade : MonoBehaviour
{
    public GameObject UIBar;
    public float alpha;
    public float alphaMax;
    public float alphaRate;
    public float R;
    public float G;
    public float B;

    void Start(){

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
        UIBar.enabled = true;

        while (UIBar.GetComponent<Slider>().value < UIBar.GetComponent<Slider>().maxValue )
        {
            value += maxValue / 10;
            yield return new WaitForSeconds(0.1);
        }
        
        UIBar.GetComponent<Slider>().value = UIBar.GetComponent<Slider>().maxValue;

        // Mask.enabled = false;
    }

    IEnumerator FadeIn(){
        // Debug.Log(R/255);
        // alpha = 1;

        while (alpha > 0)
        {
            yield return new WaitForSeconds(0);
        }

        UIBar.enabled = false;
        // Mask.enabled = false;
    }

}
