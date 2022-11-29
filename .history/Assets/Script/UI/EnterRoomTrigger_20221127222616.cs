using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject AM;
    public GameObject UI;

    void Start()
    {
        AM = GameObject.Find("AudioManager").gameObject;
        // UI = GameObject.Find("UI").transform.Find("Boss").gameObject;
        // UI.transform.Find("BossName").GetComponent<Text>().enabled = false;
        // UI.transform.Find("BossName").GetComponent<Text>().enabled = false;
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            this.gameObject.GetComponent<BoxCollider>()[0].enabled = false;
            this.gameObject.GetComponent<BoxCollider>()[1].enabled = true;
            AM.GetComponent<AudioManager>().AudioPlayBoss();
            UI.transform.Find("BossName").GetComponent<TextFade>().TextFadeOut();
            UI.transform.Find("BossName").GetComponent<TextFade>().TextFadeOut();
        }
    }
}
