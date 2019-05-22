using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

	//public Transform Cube;

	void Update ()
	{
        Vector3 newPosition = Camera.main.transform.position;
		newPosition.y = transform.position.y;
		transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, Camera.main.transform.eulerAngles.y, 0f);
	}

}
