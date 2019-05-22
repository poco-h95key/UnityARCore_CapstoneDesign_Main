using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainBtn3 : MonoBehaviour
{
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
        findObject.Curobj = "Third";
        Debug.Log(findObject.Curobj);
        Screen.orientation = ScreenOrientation.Landscape;
        SceneManager.LoadScene("WarningLandscape");
    }


}