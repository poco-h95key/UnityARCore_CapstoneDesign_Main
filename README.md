# ARCore
![53875879-ebd5ef80-4048-11e9-9948-e7333be00c4d](https://user-images.githubusercontent.com/41403898/56416777-d6e9ae80-62cc-11e9-9a57-536e497fa02e.png)
ARCore : https://developers.google.com/ar/ <br>  

# Architecture
<img width="1340" alt="스크린샷 2019-04-19 오후 6 01 04" src="https://user-images.githubusercontent.com/41403898/56416864-1dd7a400-62cd-11e9-97ce-b61c0aa4d912.png">
* Client : <br>
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

* AssetBundle_Furniture_Load
```C#
public void desk1Load()
{
    StartCoroutine(GetAssetBundle("desk1")); // desk1로 저장된 에셋번들 가져오기
}
public void bed3Load()
{
    StartCoroutine(GetAssetBundle("bed3")); // bed3로 저장된 에셋번들 가져오기
}
    
IEnumerator GetAssetBundle(string Name)
{
    www = UnityWebRequestAssetBundle.GetAssetBundle("http://13.125.111.193/scene2and/"+Name); // 지정해놓은 주소에서 객체를 가져옴
    yield return www.SendWebRequest();
    if (www.isNetworkError || www.isHttpError)
    {
        Debug.Log(www.error);
    }
    else
    {
        bundle = DownloadHandlerAssetBundle.GetContent(www); // 가져온 객체를 다운로드함
        GameObject obj = bundle.LoadAsset(Name) as GameObject; // 다운로드 한 객체를 오브젝트화
        obj.name = Name; 
        obj.AddComponent<touchController>(); // 가구에 touchController 클래스를 넣어줌
        var newY = obj.transform.position.y; // 가구에 y값은 고정시켜줌
        var mainCamera = Camera.main.transform; // 카메라의 위치를 받기위한 변수
        obj.transform.position = new Vector3(1.5f * mainCamera.forward.x + mainCamera.position.x, newY, 1.5f *      mainCamera.forward.z + mainCamera.position.z); 
        // y값은 고정이나 카메라의 위치에 따라 사용자 앞에 오브젝트가 생성되어야 하기 때문에 x값과 z값의 변화만 줌
        Instantiate(obj);

        bundle.Unload(false); // 다운로드받은 번들 언로드
        www.Dispose(); // 가져온 객체 언로드
    }
}
```
##### 가져온 AssetBundle에 touchController클래스를 넣어 move,rotate,delete 활성화, 코루틴을 사용하여 객체를 불러옴 
# Source for furniture  
* UITouch class
```C#
public static int phase; // 상태를 나타내는 변수, 0일 때 변화없음, 1일 때 가구 이동 활성화, 2일 때 가구 회전 활성화, 3일 때 가구 삭제 활성화, 4일 때 나가기

public static int getPhase() // 상태를 받기위한 getter
{
    return phase;
}
public void switchSelect()
{
    phase = 0;
}
public void switchMove()
{
    phase = 1;
}

public void switchRotate()
{
    phase = 2;
}
public void switchDelete()
{
    phase = 3;
}
public void Quit()
{
    phase = 4;
    SceneManager.LoadSceneAsync("MainScreen", LoadSceneMode.Single);
    Screen.orientation = ScreenOrientation.Portrait;
}
public void InvokeMove()
{
    phase = -1; // UI, 객체 멀티터치 방지
    Invoke("switchMove", 1);
}
public void InvokeRotate()
{
    phase = -1;
    Invoke("switchRotate", 1);
}
public void InvokeDelete()
{
    phase = -1;
    Invoke("switchDelete", 1);
}
 ```
 ##### Select버튼, Move버튼, Rotate버튼, Delete버튼에 따라 상태를 바꿔 가구의 움직임을 조정가능 <br>
***
* touch_move
```C#
if (UITouch.getPhase() == 1)
{
    var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
    var hitInfo = new RaycastHit();
    if (Physics.Raycast(ray, out hitInfo))
    {
        if (hitInfo.transform.name != transform.name)
            return;
        var newPos = transform.position;
        newPos.x = hitInfo.point.x;
        if (hitInfo.point.z >= 0)
        {
            newPos.z = hitInfo.point.z + 0.2f;
        }
        else
        {
            newPos.z = hitInfo.point.z - 0.2f;
        }
        transform.position = newPos;
    }
}
```
##### y축은 고정하고 x축과 z축 방향으로의 이동을 구현, 사용자와 거리를 두기위해 z이동의 가중치 추가
***
* touch_rotate
```C#
if (UITouch.getPhase() == 2)
{
    var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
    var hitInfo = new RaycastHit();
    float rotSpeed = 20;   
    if (Physics.Raycast(ray, out hitInfo))
    {
        if (hitInfo.transform.name != transform.name)
            return;
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        transform.Rotate(Vector3.up, -rotX);
    }
}
```
##### x축을 기준으로 왼쪽드래그, 오른쪽드래그에 따라 물체의 회전이 가능
*** 
* touch_delete
```C#
if (UITouch.getPhase() == 3)
{
    var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
    var hitInfo = new RaycastHit();
  
    if (Physics.Raycast(ray, out hitInfo))
    {
        if (hitInfo.transform.name != transform.name)
            return;
        Destroy(hitInfo.transform.gameObject);
    }
}
```
##### 삭제하고자 하는 물체 터치시, 객체 파괴

# Execution screen
![Screenshot_20190419-205407_final](https://user-images.githubusercontent.com/41403898/56423076-67cc8400-62e5-11e9-82f0-2a47cc456be5.jpg)



