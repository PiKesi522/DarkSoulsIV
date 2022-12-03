using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAndMoney : MonoBehaviour
{
    // Start is called before the first frame update
    public int level;
    public int weapon_level;
    public int money;
    public int LevelUPMoney;
    public Text LevelText;
    public Text MoneyText;
    public GameObject AM;

    void Start()
    {
        LevelText = this.transform.Find("Canvas/PlayerElite/Level/Text").GetComponent<Text>();
        MoneyText = this.transform.Find("Canvas/Money").GetComponent<Text>();
        AM = GameObject.Find("AudioManager/SoundEffect").gameObject;
        setLevel(1);
        setMoney(100000);
    }

    void Update(){
        // if(Input.GetKeyDown(KeyCode.Y)){
        //     tryLevelUP();
        // }
    }

    public void tryLevelUP(){
        if(money >= LevelUPMoney){
            setMoney(money - LevelUPMoney);
            setLevel(level + 1);
            this.gameObject.GetComponent<CharacterStateBeta>().LevelUp(level);
            AM.GetComponent<AudioSoundEffect>().AudioPlayLevelUp();
        }
    }

    public void setLevel(int l){
        this.level = l;
        this.LevelUPMoney = this.level * this.level * 50;
        // updateLevelText();
        LevelText.GetComponent<Text>().text = "" + level;
    }

    public int getLevel(){
        return this.level;
    }

    public void setMoney(int m){
        this.money = m;
        MoneyText.GetComponent<Text>().text = "" + money;
        // updateMoneyText();
    }

    public int getMoney(){
        return this.money;
    }

    public void addMoney(int m){
        setMoney(this.money + m);
    }

    public int getMoneyCost(){
        return this.LevelUPMoney;
    }
}
