using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollFightMode : MonoBehaviour {
    public float minSize = 15.0f;
    public float maxSize = 30.0f;


    Camera camera;

	void Start () {
        camera = GetComponent<Camera>();

        if (camera.orthographicSize < minSize)
        {
            camera.orthographicSize = minSize;
        } else if (camera.orthographicSize > maxSize)
        {
            camera.orthographicSize = maxSize;
        }

    }
	
	void Update () {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta == 0)
        {
            return;
        }

        float newOrthoSize = camera.orthographicSize - Mathf.Sign(scrollDelta);

        if (newOrthoSize >= minSize && newOrthoSize <= maxSize)
        {
            camera.orthographicSize = newOrthoSize;
        }
    }
}
