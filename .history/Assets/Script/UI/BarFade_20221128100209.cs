using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFade : MonoBehaviour
{
    public float alpha;
    public float alphaMax;
    public float alphaRate;
    public float R;
    public float G;
    public float B;

    void Start(){

    }

    // public void TextFadeIn(){
    //     StartCoroutine(FadeIn());
    // }

    public void BarFadeOut(){
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeOut(){
        // Debug.Log(R/255);
        // alpha = 1;
        this.gameObject.GetComponent<Slider>().enabled = true;

        while (this.gameObject.GetComponent<Slider>().value < this.gameObject.GetComponent<Slider>().maxValue )
        {
            this.gameObject.GetComponent<Slider>().value += this.gameObject.GetComponent<Slider>().maxValue / 10;
            yield return new WaitForSeconds(0.001f * Time.deltaTime);
        }
        
        this.gameObject.GetComponent<Slider>().value = this.gameObject.GetComponent<Slider>().maxValue;

        // Mask.enabled = false;
    }

    // IEnumerator FadeIn(){
    //     // Debug.Log(R/255);
    //     // alpha = 1;

    //     while (alpha > 0)
    //     {
    //         yield return new WaitForSeconds(0);
    //     }

    //     UIBar.enabled = false;
    //     // Mask.enabled = false;
    // }

}
