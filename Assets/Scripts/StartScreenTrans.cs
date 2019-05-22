using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenTrans : MonoBehaviour
{
    public float delayTime = 2;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);
        Application.LoadLevel("MainScreen");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
