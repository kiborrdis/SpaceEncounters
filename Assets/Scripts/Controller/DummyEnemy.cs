using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyEnemy : MonoBehaviour {

    public Vector3 heading = Vector3.left;
    public float speed = 3;
    public float rotationSpeed = 30;
    public float accel = 1;
    private float rotationSpeedRad;
    private Vector3 lastSpeed;
    bool wasMoving = false;
    Rigidbody engine;

    public Vector3 Speed
    {
        get
        {
            return engine.velocity;
        }
    }

    // Use this for initialization
    void Start () {
        heading.Normalize();
        rotationSpeedRad = rotationSpeed * Mathf.Deg2Rad;
        engine = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 currentHeading = transform.rotation * Vector3.forward;

        Vector3 newHeading = Vector3.RotateTowards(currentHeading, heading, rotationSpeedRad * Time.deltaTime, 0);
        transform.rotation = Quaternion.LookRotation(newHeading);

        float angleToTarget = Vector3.Angle(newHeading, heading);

        wasMoving = false;
        if (Vector3.Angle(newHeading, heading) < 1.0f)
        {
            //transform.position = transform.position + newHeading * speed * Time.deltaTime;
            //transform.Translate(newHeading * speed * Time.deltaTime);

            if (engine.velocity.magnitude < speed)
            {
                engine.AddForce(newHeading * accel * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }
}
