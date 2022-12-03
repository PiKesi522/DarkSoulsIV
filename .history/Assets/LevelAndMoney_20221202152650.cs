using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAndMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public int money;
    public GameObject LevelText;
    public GameObject MoneyText;

    void Start()
    {
        level = 10;
        money = 1000;
    }

    public void setLevel(int l){
        this.level = l;
        // updateLevelText();
        LevelText.GetComponent<Text>().text = level;
    }

    public void setMoney(int m){
        this.money = m;
        MoneyText.GetComponent<Text>().text = money;
        // updateMoneyText();
    }

}
