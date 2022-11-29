using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGCharacterAnims;

public class TargetLock : MonoBehaviour
{
    public List<GameObject> targetList;
    public GameObject closestTarget;
    public GameObject Player;
    
    void Start()
    {
        Player = GameObject.Find("MainCharacter").transform.Find("RPG-Character").gameObject;
        targetList = new List<GameObject>();
    }

    private void Update() {
        getCloest();

        if(!inTargetList(closestTarget)){
            Player.GetComponent<RPGCharacterController>().ClearTarget();
        }

        if(getCloest()){
            Player.GetComponent<RPGCharacterController>().SetTarget(closestTarget.transform);
        }
        else{
            closestTarget = null;
            Player.GetComponent<RPGCharacterController>().ClearTarget();
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
