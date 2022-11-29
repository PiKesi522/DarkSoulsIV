using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public Image Mask;
    [SerializeField] private float alpha;

    void Start(){
        StartCoroutine(FadeIn());
    }

    public void SceneChange(){
        StartCoroutine(FadeOut());
    }
    
    IEnumerator FadeIn(){
        alpha = 1;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime * 0.25f;
            Mask.color = new Color(0,0,0,alpha);
            yield return new WaitForSeconds(0);
        }

        Mask.enabled = false;
    }

    IEnumerator FadeOut(){
        Mask.enabled = false;

        alpha = 0;

        while (alpha < 1)
        {
            alpha -= Time.deltaTime * 0.25f;
            Mask.color = new Color(0,0,0,alpha);
            yield return new WaitForSeconds(0);
        }

    }


}
