using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonControl : MonoBehaviour
{
    public Button[] btn;
    public Button[] Tbtn;

    public void toggle()
    {
        for(int i = 0; i < Tbtn.Length; i++)
        {
            Tbtn[i].gameObject.SetActive(false);
            btn[i].gameObject.SetActive(true);
        }
       
    }


}
