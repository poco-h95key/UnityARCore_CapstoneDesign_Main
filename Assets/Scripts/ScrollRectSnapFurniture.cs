using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnapFurniture : MonoBehaviour
{

    public RectTransform panel;     //to hold the ScrollPanel
    public Button[] btn;            //to hold all buttons
    public RectTransform center;    //compare the distance between the 'CentertoCaompre' to buttons

    private float[] distance;       //distance from button to center panel
    private bool dragging = false;      //dragging = true, if we drag the panel
    private int btndistance;        //mean the distance between the buttons
    private int minButtonNum;

    private void Start()
    {
        int btnlength = btn.Length;
        distance = new float[btnlength];
        btndistance = (int)Mathf.Abs(btn[1].GetComponent<RectTransform>().anchoredPosition.x - btn[0].GetComponent<RectTransform>().anchoredPosition.x);
    }

    private void Update()
    {
        for (int i = 0; i < btn.Length; i++)
        {
            distance[i] = Mathf.Abs(center.transform.position.x - btn[i].transform.position.x);
        }

        //Find the shortest distance from distance array
        float minDistance = Mathf.Min(distance);

        for (int j = 0; j < btn.Length; j++)
        {
            if (minDistance == distance[j])
            {
                minButtonNum = j;
            }
        }

        if (!dragging)
        {
            LerpToBtn(minButtonNum * -btndistance);
        }
    }

    void LerpToBtn(int position)
    {
        float newX = Mathf.Lerp(panel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, panel.anchoredPosition.y);

        panel.anchoredPosition = newPosition;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}
