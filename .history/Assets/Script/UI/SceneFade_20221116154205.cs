using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFade : MonoBehaviour
{
    public Image Mask;
    [SerializeField] private float alpha;
    
    void FadeIn(){
        alpha = 1;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            Mask.color = new Color(0,0,0,alpha);
        }
    }
}
