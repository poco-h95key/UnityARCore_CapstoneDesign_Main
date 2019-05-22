using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLandscape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape; // 씬 시작시 가로화면만 나오도록 함
    }

}
