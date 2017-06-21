using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCameraView : MonoBehaviour {

    GameObject cameraToFace;

	// Use this for initialization
	void Start () {
        cameraToFace = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 desiredHeading = cameraToFace.transform.rotation * Vector3.back;
        Quaternion newRotation = Quaternion.LookRotation(desiredHeading);


        transform.localPosition = transform.parent.InverseTransformDirection(desiredHeading)*3;
        transform.rotation = newRotation;
    }
}
