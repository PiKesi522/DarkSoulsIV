using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLock : MonoBehaviour
{
    private List<GameObject> targetList;
    public GameObject closestTarget;
    public GameObject Player;

    public GameObject target;
    public GameObject targetArea;
    public bool LookAt;
    
    void Start()
    {
        Player = GameObject.Find("MainCharacter").transform.GetChild(0).gameObject;
        targetList = new List<GameObject>();
        targetArea = this.gameObject;
    }

    private void Update() {
        getCloest();

        
        if(LookAt){
            if(inTargetList(target)){
                LookAt = false;
                target = null;
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
