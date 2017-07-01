using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionController : MonoBehaviour {

    public MotionModel model;
    Rigidbody objectRigidbody;

    bool rotateTowardsHeading(Vector3 desiredHeading)
    {
        Vector3 currentHeading = transform.rotation * Vector3.forward;
        currentHeading.Normalize();

        if (Vector3.Angle(currentHeading, desiredHeading) > 0.1f)
        {
            model.applyRotationByVectors(currentHeading, desiredHeading);
            Vector3 newHeading = Vector3.RotateTowards(currentHeading, desiredHeading, model.RotationSpeed * Time.deltaTime, 10);

            objectRigidbody.MoveRotation(Quaternion.LookRotation(newHeading));
        }
        else
        {
            model.rotation = MotionModel.Rotation.stale;
        }


        return Vector3.Angle(transform.rotation * Vector3.forward, desiredHeading) < 30.0f;
    }

    void changeVelocity(Vector3 desiredVelocity, Vector3 desiredHeading)
    {
        if ((!rotateTowardsHeading(desiredHeading) || Vector3.Distance(desiredVelocity, objectRigidbody.velocity) < 0.5f) && desiredVelocity.magnitude != 0)
        {
            model.applyModelAccordingDelta(Vector3.zero);
            return;
        }

        //Debug.Log("Accelerate... " + Vector3.Distance(desiredVelocity, engine.velocity));
        float frameDeltaVelocity = model.accel * Time.deltaTime;

        Vector3 delta = desiredVelocity - objectRigidbody.velocity;
        float deltaVelocity = delta.magnitude;

        objectRigidbody.AddForce(delta.normalized * (deltaVelocity > frameDeltaVelocity ? frameDeltaVelocity : deltaVelocity), ForceMode.VelocityChange);

        frameDeltaVelocity = deltaVelocity > frameDeltaVelocity ? 0 : (frameDeltaVelocity - deltaVelocity);

        model.applyModelAccordingDelta(transform.InverseTransformDirection(delta.normalized));
    }

    // Use this for initialization
    void Start () {
        objectRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        changeVelocity(model.DesiredVelocity, model.DesiredHeading);
    }
}
