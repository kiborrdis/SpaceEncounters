using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour {

    public CameraModel cameraModel;

    public float depth = 10.0f;
    public float height = 10.0f;

    private float actualDepth = 10.0f;
    private float actualHeight = 10.0f;

    private float basicOrthoSize;


    public float cameraOffset = 150.0f;
    public GameObject objectToFollow;
    Camera mainCamera;

	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();
        actualHeight = height;
        actualDepth = depth;
        basicOrthoSize = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate () {
		if (!objectToFollow)
        {
            return;
        }

        handleScaleChanges();
        setNewPosition();
	}

    void setNewPosition()
    {
        actualHeight = height * cameraModel.CurrentScale;
        actualDepth = depth * cameraModel.CurrentScale;

        Vector3 objectPosition = objectToFollow.transform.position;

        //objectPosition.z -= cameraOffset * cameraModel.CurrentScale;

        Vector3 desiredPosition = new Vector3(objectPosition.x, objectPosition.y + actualHeight, objectPosition.z - actualDepth);

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

        if(Mathf.Sign(scrollDelta) > 0)
        {
            cameraModel.ZoomIn();
        } else
        {
            cameraModel.ZoomOut();
        }

        if (mainCamera.orthographic)
        {
            mainCamera.orthographicSize = basicOrthoSize * cameraModel.CurrentScale;
        }
    }
}
