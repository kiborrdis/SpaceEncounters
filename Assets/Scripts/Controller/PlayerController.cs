using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    Rigidbody playerRigidbody;

    public TestEditorClass foo;

    public bool decelerateWithoutControls = false;

    public MotionModel motionModel;

    private Vector3 angleVelocity;

	// Use this for initialization
	void Start () {
        playerRigidbody = this.GetComponent<Rigidbody>();

        angleVelocity = new Vector3(0, motionModel.rotationSpeed, 0);

        if (!playerRigidbody)
        {
            Debug.Log("Player's rigidbody haven't found");
        }
	}
	
	// Update is called once per frame
	void Update () {
        applyHeadingChanges();
        applyVelocityChanges();

        GameModel.Instance.Velocity = playerRigidbody.velocity.magnitude;
    }

    void applyModelData(MotionModel.Acceleration accel)
    {
        if (motionModel && motionModel.acceleration != accel)
        {
            motionModel.acceleration = accel;
        } 
    }

    void applyModelData(MotionModel.Rotation rotation)
    {
        if (motionModel && motionModel.rotation != rotation)
        {
            motionModel.rotation = rotation;
        }
    }

    void applyModelAccordingDelta(Vector3 delta)
    {
        Vector3 localDirection = transform.InverseTransformDirection(delta);

        localDirection.x = Mathf.Round(localDirection.x * 10) / 10;
        localDirection.y = Mathf.Round(localDirection.y * 10) / 10;
        localDirection.z = Mathf.Round(localDirection.z * 10) / 10;

        if (localDirection.x > 0 && localDirection.z > 0)
        {
            applyModelData(MotionModel.Acceleration.forwardRight);
        }
        else if (localDirection.x > 0 && localDirection.z < 0)
        {
            applyModelData(MotionModel.Acceleration.backwardRight);
        }
        else if (localDirection.x < 0 && localDirection.z > 0)
        {
            applyModelData(MotionModel.Acceleration.forwardLeft);
        }
        else if (localDirection.x < 0 && localDirection.z < 0)
        {
            applyModelData(MotionModel.Acceleration.backwardLeft);
        }
        else if (localDirection.x == 0)
        {
            applyModelData(localDirection.z > 0 ? MotionModel.Acceleration.forward : MotionModel.Acceleration.backward);
        }
        else if (localDirection.z == 0)
        {
            applyModelData(localDirection.x > 0 ? MotionModel.Acceleration.right : MotionModel.Acceleration.left);
        }
    }

    void applyHeadingChanges()
    {
        float verticalDelta = Input.GetAxis("Horizontal");
        if (verticalDelta != 0)
        {
            applyModelData(Mathf.Sign(verticalDelta) > 0 ? MotionModel.Rotation.right : MotionModel.Rotation.left);
            Quaternion deltaRotation = Quaternion.Euler(angleVelocity * Time.deltaTime * verticalDelta);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * deltaRotation);
        } else
        {
            applyModelData(MotionModel.Rotation.stale);
        }
    }

    void applyVelocityChanges()
    {
        float horizontalDelta = Input.GetAxis("Vertical");
        Vector3 heading = gameObject.transform.rotation * Vector3.forward;
        Vector3 currVelocity = playerRigidbody.velocity;
        float frameDeltaVelocity = motionModel.accel * Time.deltaTime;

        if (Mathf.Abs(horizontalDelta) > 0.3f)
        {
            applyModelData(horizontalDelta > 0 ? MotionModel.Acceleration.forward : MotionModel.Acceleration.backward);

            if (Vector3.Angle(heading, currVelocity) > 0)
            {
                Vector3 desiredVelocity = heading.normalized * motionModel.maxSpeed * Mathf.Sign(horizontalDelta);
                Vector3 delta = desiredVelocity - currVelocity;
                float deltaVelocity = delta.magnitude;

                applyModelAccordingDelta(delta.normalized);

                playerRigidbody.AddForce(delta.normalized*(deltaVelocity > frameDeltaVelocity ? frameDeltaVelocity : deltaVelocity), ForceMode.VelocityChange);

                frameDeltaVelocity = deltaVelocity > frameDeltaVelocity ? 0 : (frameDeltaVelocity - deltaVelocity);
            }

            if (frameDeltaVelocity > 0 && playerRigidbody.velocity.magnitude < motionModel.maxSpeed)
            {
                applyModelAccordingDelta(heading);
                playerRigidbody.AddForce(heading * frameDeltaVelocity * Mathf.Sign(horizontalDelta), ForceMode.VelocityChange);
            }
        } else 
        {
            float currMagnitude = playerRigidbody.velocity.magnitude;
            if (currMagnitude > motionModel.cruisingSpeed || (decelerateWithoutControls && currMagnitude > 0))
            {
                Vector3 decelerateDirection = -1 * playerRigidbody.velocity.normalized;

                applyModelAccordingDelta(decelerateDirection);

                Vector3 velocityToAdd = decelerateDirection * motionModel.accel * Time.deltaTime;

                playerRigidbody.AddForce(velocityToAdd.magnitude > currMagnitude ? (-1  * playerRigidbody.velocity) : velocityToAdd, ForceMode.VelocityChange);
            }
            else
            {
                applyModelData(MotionModel.Acceleration.stale);
            }
        }
        Debug.DrawLine(transform.position, playerRigidbody.velocity + transform.position, Color.red);
        //Debug.Log(playerRigidbody.velocity);
    }
}
