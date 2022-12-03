using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackSmithEvent : MonoBehaviour
{
    public GameObject UI;
    public GameObject Interaction;
    public GameObject Dialog;
    public LevelAndMoney player;
    private bool canUpgrade;

    void Start()
    {
       UI = GameObject.Find("UI").gameObject;
       Interaction = UI.transform.Find("Interaction").gameObject;
       Dialog = UI.transform.Find("Dialog").gameObject;
       player = GameObject.Find("MainCharacter/RPG-Character").gameObject.GetComponent<LevelAndMoney>();
    }


    private void OnTriggerStay(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            Interaction.GetComponent<Canvas>().enabled = true;
            if(Input.GetKeyDown(KeyCode.F)){
                startDialog();
            }

            if(canUpgrade){

                if(Input.GetKeyDown(KeyCode.Y)){
                    player.GetComponent<LevelAndMoney>().trySharpenWeapon();
                    renewInfo();
                }
                
                if(Input.GetKeyDown(KeyCode.Escape)){
                    leaveDialog();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "PlayerCore"){
            Interaction.GetComponent<Canvas>().enabled = false;
            leaveDialog();
        }
    }

    void startDialog(){
        int money = player.GetComponent<LevelAndMoney>().getMoney();
        int cost = player.GetComponent<LevelAndMoney>().getMoneyCost();
        Dialog.transform.Find("Background").gameObject.SetActive(true);
        Dialog.transform.Find("BlackSmithDialog").gameObject.SetActive(true);
        Dialog.transform.Find("BlackSmithDialog/UpgradeInfo").gameObject.GetComponent<Text>().text="持有灵魂：" + money + "\n升级所需灵魂：" + cost;
        int level = player.GetComponent<LevelAndMoney>().getWeaponLevel();
        Dialog.transform.Find("BlackSmithDialog/WeaponLevel").gameObject.GetComponent<Text>().text="当前等级：" + level;
        // GameObject DialogList = Dialog.transform.Find("GuiderList").gameObject;
        canUpgrade = true;
    }

    void renewInfo(){
        int money = player.GetComponent<LevelAndMoney>().getMoney();
        int cost = player.GetComponent<LevelAndMoney>().getWeaponMoneyCost();
        Dialog.transform.Find("BlackSmithDialog/UpgradeInfo").gameObject.GetComponent<Text>().text="持有灵魂：" + money + "\n升级所需灵魂：" + cost;
    }

    void leaveDialog(){
        // Dialog.transform.Find("Background").enabled = false;
        Dialog.transform.Find("Background").gameObject.SetActive(false);
        Dialog.transform.Find("BlackSmithDialog").gameObject.SetActive(false);
        canUpgrade = false;
    }
}
