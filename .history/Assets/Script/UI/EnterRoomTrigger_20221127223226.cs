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
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            // this.gameObject.GetComponents<BoxCollider>()[1].enabled = false;
            AM.GetComponent<AudioManager>().AudioPlayBoss();
            UI.transform.Find("BossName").GetComponent<TextFade>().TextFadeOut();
            UI.transform.Find("BossName").GetComponent<TextFade>().TextFadeOut();
        }
    }
}
