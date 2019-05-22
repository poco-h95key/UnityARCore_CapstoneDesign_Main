using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
public class FurnitureControl : MonoBehaviour
{
    AssetBundle bundle;
    UnityWebRequest www;

    public void SelectiveLoad()
    {
        name = EventSystem.current.currentSelectedGameObject.name;
        StartCoroutine(GetAssetBundle(name));
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
            obj.transform.localScale = new Vector3(obj.transform.localScale.x*0.8f, obj.transform.localScale.y*0.8f, obj.transform.localScale.z*0.8f);
            obj.transform.position = new Vector3(2.0f * mainCamera.forward.x + mainCamera.position.x, newY, 2.0f * mainCamera.forward.z + mainCamera.position.z); 
            // y값은 고정이나 카메라의 위치에 따라 사용자 앞에 오브젝트가 생성되어야 하기 때문에 x값과 z값의 변화만 줌
            Instantiate(obj);

            bundle.Unload(false); // 다운로드받은 번들 언로드
            www.Dispose(); // 가져온 객체 언로드
        }

    }
}
