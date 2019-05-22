using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class LevelLoader : MonoBehaviour
{

    UnityWebRequest www;
    AssetBundle bundle;

    float delayTime = 1;

    IEnumerator Start()
    {
        
        if (findObject.Curobj == "First")
        {
            www = UnityWebRequestAssetBundle.GetAssetBundle("http://13.125.111.193/scene1/scene_1");
        } 
        else if (findObject.Curobj == "Second")
        {
            www = UnityWebRequestAssetBundle.GetAssetBundle("http://13.125.111.193/scene2and/scene_2");

        }
        else if(findObject.Curobj == "Third")
        {
            www = UnityWebRequestAssetBundle.GetAssetBundle("http://13.125.111.193/scene3/scene_3");
        }

        yield return www.SendWebRequest();
        bundle = DownloadHandlerAssetBundle.GetContent(www);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {

            if (bundle.isStreamedSceneAssetBundle)
            {
                string[] scenePaths = bundle.GetAllScenePaths();

                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePaths[0]);


                SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);

                yield return new WaitForSeconds(delayTime);
                bundle.Unload(false);
                www.Dispose();
                SceneManager.UnloadSceneAsync("WarningLandscape");
            }
           
        }
    }

}
