using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    public float depth = 10.0f;
    public float height = 10.0f;

    public float minSize = 100.0f;
    public float maxSize = 1000.0f;

    public float sizeStep = 50.0f;

    public GameObject objectToFollow;
    Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();

        if (mainCamera.orthographicSize < minSize)
        {
            mainCamera.orthographicSize = minSize;
        }
        else if (mainCamera.orthographicSize > maxSize)
        {
            mainCamera.orthographicSize = maxSize;
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (!objectToFollow)
        {
            return;
        }


        setNewPosition();
        handleScaleChanges();
	}

    void setNewPosition()
    {
        Vector3 objectPosition = objectToFollow.transform.position;
        Vector3 desiredPosition = new Vector3(objectPosition.x, objectPosition.y + height, objectPosition.z - depth);

        transform.LookAt(objectPosition);
        transform.position = desiredPosition;
    }

    void handleScaleChanges()
    {
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");

        if (scrollDelta == 0)
        {
            return;
        }

        float newOrthoSize = mainCamera.orthographicSize - Mathf.Sign(scrollDelta) * sizeStep;

        if (newOrthoSize < minSize)
        {
            mainCamera.orthographicSize = minSize;
        }
        else if (newOrthoSize > maxSize)
        {
            mainCamera.orthographicSize = maxSize;
        } else
        {
            mainCamera.orthographicSize = newOrthoSize;
        }
    }
}
