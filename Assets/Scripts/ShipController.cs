using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {
    Rigidbody physicUnit;
    public float speed = 10.0f;
    public float rotationSpeed = 60.0f;

	// Use this for initialization
	void Start () {
        physicUnit = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        float horizontalDelta = Input.GetAxis("Horizontal");
        float verticalDelta = Input.GetAxis("Vertical");
        float straifDelta = Input.GetAxis("Straif");

       // Debug.Log(verticalDelta * Time.deltaTime * Vector3.forward);
       // Debug.Log(Time.deltaTime * verticalDelta);

        Vector3 heading = gameObject.transform.rotation * Vector3.forward;
        heading.Normalize();
        Vector3 altHeading = Vector3.Cross(Vector3.up, heading);
        altHeading.Normalize();
        transform.position = transform.position + heading * verticalDelta * Time.deltaTime * speed;
        //physicUnit.AddForce(verticalDelta * Time.deltaTime * heading * 3, ForceMode.VelocityChange);
        //physicUnit.AddForce(straifDelta * Time.deltaTime * altHeading, ForceMode.VelocityChange);
        gameObject.transform.Rotate(Vector3.up, Time.deltaTime * horizontalDelta * rotationSpeed);
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.rotation.eulerAngles.y, 0);

	}
}
