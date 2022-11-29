using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveInteractionUI : MonoBehaviour
{
    // public GameObject Player;
    public Canvas UI;
    // Start is called before the first frame update
    void Start()
    {
       UI = GameObject.Find("UI").transform.Find("Interaction");
       Debug.Log(UI);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
