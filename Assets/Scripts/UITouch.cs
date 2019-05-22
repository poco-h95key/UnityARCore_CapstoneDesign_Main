using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UITouch : MonoBehaviour
{

    public static int phase; // 상태를 나타내는 변수, 0일 때 변화없음, 1일 때 가구 이동 활성화, 2일 때 가구 회전 활성화, 3일 때 가구 삭제 활성화, 4일 때 나가기



    public static int getPhase() // 상태를 받기위한 getter
    {
        return phase;
    }

    public void switchSelect()
    {
        phase = 0;
    }
    public void switchMove()
    {
        phase = 1;
    }

    public void switchRotate()
    {
        phase = 2;

    }
    public void switchDelete()
    {
        phase = 3;
    }
    public void Quit()
    {
        phase = 4;
        SceneManager.LoadSceneAsync("MainScreen", LoadSceneMode.Single);
        Screen.orientation = ScreenOrientation.Portrait;
    }
    public void InvokeSelect()
    {
        phase = -1;
        Invoke("switchSelect", 0.5f);
    }
    public void InvokeMove()
    {
        phase = -1;
        Invoke("switchMove", 0.5f);
    }
    public void InvokeRotate()
    {
        phase = -1;
        Invoke("switchRotate", 0.5f);
    }
    public void InvokeDelete()
    {
        phase = -1;
        Invoke("switchDelete", 0.5f);
    }
    public void InvokeMaintain()
    {
        phase = -1;
    }
}
