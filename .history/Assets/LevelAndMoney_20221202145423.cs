using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAndMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public int money;

    void Start()
    {
        level = 10;
        money = 1000;
    }

    public void setLevel(int l){
        this.level = l;
        updateLevelText();
    }

    public void setMoney(int m){
        this.money = m;
        updateMoneyText();
    }

}
