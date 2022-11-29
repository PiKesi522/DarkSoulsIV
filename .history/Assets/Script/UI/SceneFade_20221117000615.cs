using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public Image Mask;
    [SerializeField] private float alpha;
    [SerializeField] private float alphaMax;
    [SerializeField] private float FadeInAlphaRate;
    [SerializeField] private float FadeOutAlphaRate;
    // [SerializeField] private float alphaRate;

    void Start(){
        SceneFadeIn();
        // Mask = this.gameObject.GetComponent<Image>();
        // Debug.Log(Mask);
    }

    public void SceneFadeIn(){
        StartCoroutine(FadeIn());
    }

    public void SceneFadeOut(){
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeIn(){
        alpha = alphaMax / 255;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * FadeInAlphaRate;
            Mask.color = new Color(0,0,0,alpha);
            yield return new WaitForSeconds(0);
        }

        Mask.enabled = false;
    }

    IEnumerator FadeOut(){
        Mask.enabled = true;

        alpha = 0;

        while (alpha < (alphaMax / 255))
        {
            alpha += Time.deltaTime * FadeOutAlphaRate;
            Mask.color = new Color(0,0,0,alpha);
            yield return new WaitForSeconds(0);
        }

    }


}
