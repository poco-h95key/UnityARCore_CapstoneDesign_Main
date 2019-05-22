using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class touchController : MonoBehaviour
{

    public void OnMouseDrag()
    {
        if (Input.touchCount > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
            else
            {
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
                        newPos.z = hitInfo.point.z + 0.2f;
                        transform.position = newPos;

                    }
                }
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
         }
      }
   }  
}