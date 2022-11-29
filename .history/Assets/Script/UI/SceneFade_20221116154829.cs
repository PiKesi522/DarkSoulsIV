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
    
    IEnumerator FadeIn(){
        alpha = 4;

        while (true)
        {
            alpha -= Time.deltaTime;
            Mask.color = new Color(0,0,0,alpha/4);
            yield return new WaitForSeconds(0);
        }
    }

}
