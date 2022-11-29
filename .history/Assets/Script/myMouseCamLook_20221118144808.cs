using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myMouseCamLook : MonoBehaviour
{
    public float mouseSensitivity = 0.01f;
    
    public GameObject target;
    public GameObject targetArea;
    public bool LookAt;

    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        targetArea = GameObject.Find("TargetLockArea").gameObject;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(22f, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);

        
        if(targetArea.GetComponent<TargetLock>().getCloest()){
            target = targetArea.GetComponent<TargetLock>().closestTarget;
        }
        else{
            target = null;
            Debug.Log("too far to lock the target");
        }

        if(Input.GetKeyDown(KeyCode.Q)){
            LookAt = !LookAt;
            if(LookAt && (target != null)){
                Debug.Log(target);
            }
        }
    }
}