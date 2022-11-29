using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class TargetLock : MonoBehaviour
{
    private List<GameObject> targetList;
    public GameObject closestTarget;
    public GameObject Player;

    public GameObject targetArea;
    public bool LookAt;
    
    void Start()
    {
        Player = GameObject.Find("MainCharacter").transform.Find("RPG-Character").gameObject;
        targetList = new List<GameObject>();
        targetArea = this.gameObject;
    }

    private void Update() {
        getCloest();

        
        if(LookAt){
            if(!inTargetList(target)){
                LookAt = false;
                Player.GetComponent<RPGCharacterController>().ClearTarget();
            }
        }

        
        if(Input.GetKeyDown(KeyCode.L)){
            LookAt = !LookAt;
            // 进行锁定
            if(LookAt){
                // 存在可锁定目标
                if(getCloest()){
                    // Debug.Log(this.transform.parent.gameObject);
                    Player.GetComponent<RPGCharacterController>().SetTarget(closestTarget.transform);

                }
                else{
                    Player.GetComponent<RPGCharacterController>().ClearTarget();
                    LookAt = false;
                }
            }
        }
    }

    public bool getCloest(){
        if(targetList.Count > 0){
            float distance = 100.0f;
            GameObject target = this.gameObject;
            foreach (GameObject item in targetList)
            {
                // 死亡对象不检测
                float newDistance = (Player.transform.position - item.transform.position).magnitude;
                if(newDistance < distance){
                    distance = newDistance;
                    target = item;
                }
            }
            closestTarget = target;
            return true;
        }
        else{
            return false;
        }
    }

    public bool inTargetList(GameObject go){
        foreach (GameObject item in targetList)
        {
            if(go == item){
                return true;
            }
        }
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "EnemyCore"){
            targetList.Add(other.gameObject);
            // Debug.Log(targetList);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.name == "EnemyCore"){
            targetList.Remove(other.gameObject);
        }
    }
}
