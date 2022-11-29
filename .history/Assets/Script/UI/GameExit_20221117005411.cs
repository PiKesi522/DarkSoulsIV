using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameExit : MonoBehaviour
{
    public void OnClick(){
        Debug.Log("exit");
        #if UNITY_EDITOR //编辑器中退出游戏
            UnityEditor.EditorApplication.isPlaying = false;
        #else //应用程序中退出游戏
            UnityEngine.Application.Quit();
        #endif
    }
}
