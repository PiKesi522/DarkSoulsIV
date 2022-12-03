using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    // PlayerPrefs.SetInt("PlayerHPLEVEL", 1);
    // PlayerPrefs.SetInt("PlayerEnduranceLEVEL", 1);
    public GameObject player;

    private void Awake() {
        player = GameObject.Find("MainCharacter").transform.Find("RPG-Character").gameObject;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("LEVEL", player.GetComponent<LevelAndMoney>().getLevel());
        PlayerPrefs.SetInt("MONEY", player.GetComponent<LevelAndMoney>().getMoney());   
        PlayerPrefs.Save();
    }

    public void Load(){
        player.GetComponent<LevelAndMoney>().setLevel(PlayerPrefs.GetInt("LEVEL"));
        player.GetComponent<LevelAndMoney>().setMoney(PlayerPrefs.GetInt("MONEY"));
    }
}
