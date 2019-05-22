using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainBtn : MonoBehaviour
{
    public static string CurPage = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene()
    {
        findObject.Curobj = "First";
        Debug.Log(findObject.Curobj);
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("WarningLandscape");
    }

    /*
    public void FindObj()
    {

        GameObject Curobj = null;

        if ((Curobj = transform.Find("Panel/ScrollPanel/FirstPage")) != null)
        {   //First Building load

            Debug.Log("First page found");
            CurPage = "FirstPage";

        }
        else if ((Curobj = GameObject.Find("Panel/ScrollPanel/SecondPage")) != null)
        {   //Second Building load
            Debug.Log("Second page found");
            CurPage = "SecondPage";
        }
        else if ((Curobj = GameObject.Find("Panel/ScrollPanel/ThirdPage")) != null)
        {   //Third Building load
            Debug.Log("Third page found");
            CurPage = "ThirdPage";
        }
        else
        {   //Expection if any building is not applied

        }
    }
    */
}
