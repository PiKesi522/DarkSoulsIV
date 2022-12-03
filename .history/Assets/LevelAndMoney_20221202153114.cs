using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAndMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public int money;
    public Text LevelText;
    public Text MoneyText;

    void Start()
    {
        LevelText = this.transform.Find("Canvas/PlayerElite/Level/Text").GetComponent<Text>();
        MoneyText = this.transform.Find("Canvas/Money").GetComponent<Text>();
        setLevel(10);
        setMoney(1000);
    }

    public void setLevel(int l){
        this.level = l;
        // updateLevelText();
        LevelText.GetComponent<Text>().text = "" + level;
    }

    public void setMoney(int m){
        this.money = m;
        MoneyText.GetComponent<Text>().text = "" + money;
        // updateMoneyText();
    }

}
