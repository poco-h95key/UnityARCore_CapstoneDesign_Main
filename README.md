# Purpose
* Users can experience architecture and allocate furnitures by using mobile phone
* ARCore helps you to render AR  
![Screenshot_20190522-170004_scene_1](https://user-images.githubusercontent.com/41403898/58165871-9ca47000-7cc3-11e9-9d90-e9e7626b03e7.jpg)

# ARCore
![53875879-ebd5ef80-4048-11e9-9948-e7333be00c4d](https://user-images.githubusercontent.com/41403898/56416777-d6e9ae80-62cc-11e9-9a57-536e497fa02e.png)
ARCore : https://developers.google.com/ar/ <br>  
***

# Architecture
<img width="1199" alt="스크린샷 2019-05-22 오후 4 01 07" src="https://user-images.githubusercontent.com/41403898/58153863-23008800-7cab-11e9-9938-22c8fd124b8a.png">

* Server : https://github.com/Esensia/nodejs_basic (main.js) <br> 
* AssetBundle : https://github.com/Esensia/AssetBundle <br>
***
## ObjectToAssetBundle
```C#
[MenuItem("Assets/ Build AssetBundles")]
static void BuildAllAssetBundles()
{
    BuildPipeline.BuildAssetBundles(@"/Users/hongmingi//Desktop/AssetBundle", BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.Android);
}
```
***

# MainScreen
<img width="939" alt="스크린샷 2019-05-22 오후 4 16 17" src="https://user-images.githubusercontent.com/41403898/58154602-f8173380-7cac-11e9-880f-48003c26ea53.png">

* Sunflower    (scene1) : https://github.com/Esensia/UnityARCore_Capstone_Scene1 <br>
* CristalView  (scene2)<br>
* Office       (scene3)<br>

***

## AssetBundle_Scene_Load
```C#
UnityWebRequest www;
AssetBundle bundle;
float delayTime = 1; // time to delay

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
            //SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
            
            yield return new WaitForSeconds(delayTime); 
            
            // AssetBundle Unload and Warning Scene Unload 
            bundle.Unload(false);
            www.Dispose();
            SceneManager.UnloadSceneAsync("WarningLandscape");
        }

    }
}
```
