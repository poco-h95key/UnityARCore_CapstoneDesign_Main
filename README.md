# ARCore
![53875879-ebd5ef80-4048-11e9-9948-e7333be00c4d](https://user-images.githubusercontent.com/41403898/56416777-d6e9ae80-62cc-11e9-9a57-536e497fa02e.png)
ARCore : https://developers.google.com/ar/ <br>  

# Architecture
<img width="1199" alt="스크린샷 2019-05-22 오후 4 01 07" src="https://user-images.githubusercontent.com/41403898/58153863-23008800-7cab-11e9-9938-22c8fd124b8a.png">

* Server : https://github.com/Esensia/nodejs_basic (main.js) <br> 
* AssetBundle : https://github.com/Esensia/AssetBundle <br>

# Source for Assetbundle
* ObjectToAssetBundle
```C#
[MenuItem("Assets/ Build AssetBundles")]
static void BuildAllAssetBundles()
{
    BuildPipeline.BuildAssetBundles(@"/Users/hongmingi//Desktop/AssetBundle", BuildAssetBundleOptions.ChunkBasedCompression,BuildTarget.Android);
}
```
##### 유니티상에 Asset Labels에서 정의한 이름을 AssetBundle로 만듦 
***
* AssetBundle_Scene_Load
```C#
UnityWebRequest www;
AssetBundle bundle;
float delayTime = 2; // 에셋번들로 가져오는 동안 대기시간을 설정하기 위한 변수

IEnumerator Start()
{
    www = UnityWebRequestAssetBundle.GetAssetBundle("http://13.125.111.193/scene2and/scene_2");
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

            yield return new WaitForSeconds(delayTime); // Warning씬에서 assetbundle unload후 씬도 unload 
            bundle.Unload(false);
            www.Dispose();
            SceneManager.UnloadSceneAsync("Warning");
        }

    }
}
```
***




