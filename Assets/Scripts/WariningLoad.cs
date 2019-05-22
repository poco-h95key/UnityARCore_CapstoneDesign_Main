using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WariningLoad : MonoBehaviour
{
    public float delayTime = 3;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        GameObject CurPanel;

        if((CurPanel = GameObject.Find("FirstPage")) != null)
        {   //First Building load

            Debug.Log("First page found");

        }else if((GameObject.Find("SecondPage")) != null)
        {   //Second Building load
            Debug.Log("Second page found");
        }
        else if((CurPanel = GameObject.Find("ThirdPage")) != null)
        {   //Third Building load
            Debug.Log("Third page found");
        }
        else
        {   //Expection if any building is not applied

        }

    */
        
    }
}
