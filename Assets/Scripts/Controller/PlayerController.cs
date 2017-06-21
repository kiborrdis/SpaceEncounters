using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody playerRigidbody;

    public float maxSpeed = 10.0f;
    public float cruisingSpeed = 8.0f;
    public float accel= 1.0f;
    public float rotationSpeed = 60.0f;
    public bool decelerateWithoutControls = false;

    private Vector3 angleVelocity;

	// Use this for initialization
	void Start () {
        playerRigidbody = this.GetComponent<Rigidbody>();

        angleVelocity = new Vector3(0, rotationSpeed, 0);

        if (!playerRigidbody)
        {
            Debug.Log("Player's rigidbody haven't found");
        }
	}
	
	// Update is called once per frame
	void Update () {
        applyHeadingChanges();
        applyVelocityChanges();
    }

    void applyHeadingChanges()
    {
        float verticalDelta = Input.GetAxis("Horizontal");
        Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime * verticalDelta);
        playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
    }

    void applyVelocityChanges()
    {
        float horizontalDelta = Input.GetAxis("Vertical");
        Vector3 heading = gameObject.transform.rotation * Vector3.forward;

        if (horizontalDelta > 0.3f)
        {
            if (playerRigidbody.velocity.magnitude < maxSpeed || Mathf.Abs(Vector3.Angle(playerRigidbody.velocity, heading)) > 1.0f)
            {
                playerRigidbody.AddForce(heading * accel * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        else if (horizontalDelta < -0.3f)
        {
            if (playerRigidbody.velocity.magnitude < maxSpeed || Mathf.Abs(Vector3.Angle(playerRigidbody.velocity, -heading)) > 1.0f)
            {
                playerRigidbody.AddForce(-1 * heading * accel / 2 * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        else
        {
            if (playerRigidbody.velocity.magnitude > cruisingSpeed || decelerateWithoutControls)
            {
                Vector3 decelerateDirection = -1 * playerRigidbody.velocity.normalized;

                playerRigidbody.AddForce(decelerateDirection * accel * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
        Debug.DrawLine(transform.position, playerRigidbody.velocity + transform.position, Color.red);
        //Debug.Log(playerRigidbody.velocity);
    }
}
